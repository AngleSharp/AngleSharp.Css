#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    internal class CssProperty : ICssProperty
    {
        #region Fields

        private readonly PropertyFlags _flags;
        private readonly String _name;
        private readonly IValueConverter _converter;

        private Boolean _important;
        private ICssValue _value;
        private KeywordState _keyword;
        // the last computed declaration with what its value depended on, see Compute
        private ComputedMemo _computed;

        #endregion

        #region ctor

        internal CssProperty(String name, IValueConverter converter, PropertyFlags flags = PropertyFlags.None, ICssValue value = null, Boolean important = false)
        {
            _name = name.StartsWith("--", StringComparison.Ordinal) ? name : name.ToLowerFast();
            _converter = converter;
            _flags = flags;
            _value = value;
            _important = important;
        }

        #endregion

        #region Properties

        public ICssValue RawValue
        {
            get => _value;
            set => SetValue(value);
        }

        public String Value
        {
            get => _value?.CssText ?? String.Empty;
            set
            {
                if (IsShorthand && value.IndexOf(FunctionNames.Var, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var source = new StringSource(value);
                    source.SkipSpacesAndComments();
                    var parsed = _converter.Convert(source);
                    source.SkipSpacesAndComments();

                    if (parsed != null && source.IsDone && parsed is not CssVarValue)
                    {
                        SetValue(parsed);
                        return;
                    }
                }

                SetValue(_converter.Convert(value));
            }
        }

        public Boolean HasValue => _value != null;

        public PropertyFlags Flags => _flags;

        public Boolean IsInherited
        {
            get
            {
                var keyword = GetKeyword();
                return keyword == KeywordState.Inherit || (keyword == KeywordState.Initial && CanBeInherited);
            }
        }

        public Boolean CanBeInherited => (_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited;

        public Boolean IsAnimatable => (_flags & PropertyFlags.Animatable) == PropertyFlags.Animatable;

        public Boolean IsInitial => GetKeyword() == KeywordState.Initial;

        public Boolean IsShorthand => (_flags & PropertyFlags.Shorthand) == PropertyFlags.Shorthand;

        public String Name => _name;

        public Boolean IsImportant
        {
            get => _important;
            set => _important = value;
        }

        public String CssText => this.ToCss();

        #endregion

        #region Internal Properties

        internal Boolean CanBeHashless => (_flags & PropertyFlags.Hashless) == PropertyFlags.Hashless;

        internal Boolean CanBeUnitless => (_flags & PropertyFlags.Unitless) == PropertyFlags.Unitless;

        internal IValueConverter Converter => _converter;

        #endregion

        #region Methods

        private void SetValue(ICssValue value)
        {
            _value = value;
            _keyword = KeywordState.Unknown;
            _computed = null;
        }

        private KeywordState GetKeyword()
        {
            if (_keyword == KeywordState.Unknown)
            {
                _keyword = _value switch
                {
                    null => KeywordState.Initial,
                    CssInitialValue => KeywordState.Initial,
                    CssInheritValue => KeywordState.Inherit,
                    _ when !MaySerializeAsKeyword(_value) => KeywordState.Other,
                    _ => _value.CssText switch
                    {
                        var text when text.Is(CssKeywords.Initial) => KeywordState.Initial,
                        var text when text.Is(CssKeywords.Inherit) => KeywordState.Inherit,
                        _ => KeywordState.Other,
                    },
                };
            }

            return _keyword;
        }

        /// <summary>
        /// Gets if the text of the value may be a CSS-wide keyword (e.g., initial), which is
        /// false for the kinds of values serialized from numbers, lengths, colors or functions:
        /// telling it from their text would serialize them (for every element computed).
        /// </summary>
        internal static Boolean MaySerializeAsKeyword(ICssValue value)
        {
            switch (value)
            {
                case null:
                case ICssFunctionValue:
                case CssLengthValue:
                case CssColorValue:
                case CssNumberValue:
                case CssPercentageValue:
                case CssIntegerValue:
                case CssAngleValue:
                case CssTimeValue:
                case CssStringValue:
                    return false;
                case ICssMultipleValue multiple:
                    // the text of a multiple value is made of the texts of its items
                    for (var i = 0; i < multiple.Count; i++)
                    {
                        if (MaySerializeAsKeyword(multiple[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                default:
                    return true;
            }
        }

        public ICssProperty Compute(ICssComputeContext context)
        {
            if (CssComputedValues.IsComputed(_value) && !_name.StartsWith("--", StringComparison.Ordinal))
            {
                // e.g., a color or a keyword: computing it would just give a copy of it
                return this;
            }

            // the declaration of a rule is computed for every element the rule matches, mostly
            // to the same value, e.g., `margin: 1em` for all elements with the same font size
            var memo = Volatile.Read(ref _computed);

            if (memo is not null && memo.Important == _important && memo.Matches(context, _name))
            {
                return memo.Result;
            }

            var propertyContext = new PropertyComputeContext(context, _converter, _name);
            var computedValue = _name.StartsWith("--", StringComparison.Ordinal) ?
                context.Resolve(_name) ?? CssInvalidValue.Instance :
                _value is CssChildValue child ? child.Compute(propertyContext, _name) :
                _value is CssReferenceValue reference ? reference.ComputeSubstituted(propertyContext) : _value?.Compute(propertyContext);
            var result = computedValue != _value ? new CssProperty(_name, _converter, _flags, computedValue, _important) : this;

            if (propertyContext.IsRemembered && context is CssComputeContext && _value is not CssChildValue && _value is not CssReferenceValue && !_name.StartsWith("--", StringComparison.Ordinal))
            {
                Volatile.Write(ref _computed, new ComputedMemo(propertyContext, _important, result));
            }

            return result;
        }

        /// <summary>
        /// A computed declaration and what the computation read from the context: as the value
        /// is computed from these (e.g., em from the font size), the same declaration is the
        /// computed one for every element giving the same.
        /// </summary>
        private sealed class ComputedMemo
        {
            private readonly ReadInputs _reads;
            private readonly Double? _emBase;
            private readonly Double? _rootFontSize;
            private readonly Boolean _keepsPercentages;
            private readonly Double? _percentageBasis;

            public ComputedMemo(PropertyComputeContext context, Boolean important, ICssProperty result)
            {
                _reads = context.Reads;
                _emBase = context.ReadEmBase;
                _rootFontSize = context.ReadRootFontSize;
                _keepsPercentages = context.ReadKeepsPercentages;
                _percentageBasis = context.ReadPercentageBasis;
                Important = important;
                Result = result;
            }

            public Boolean Important { get; }

            public ICssProperty Result { get; }

            public Boolean Matches(ICssComputeContext context, String name)
            {
                if (context is not CssComputeContext)
                {
                    return false;
                }

                var fonts = context as IFontComputeContext;
                return ((_reads & ReadInputs.EmBase) == 0 || _emBase == PropertyComputeContext.GetEmBase(fonts, name)) &&
                    ((_reads & ReadInputs.RootFontSize) == 0 || _rootFontSize == PropertyComputeContext.GetRootFontSize(fonts, name)) &&
                    ((_reads & ReadInputs.KeepsPercentages) == 0 || _keepsPercentages == PropertyComputeContext.GetKeepsPercentages(fonts, name)) &&
                    ((_reads & ReadInputs.PercentageBasis) == 0 || _percentageBasis == PropertyComputeContext.GetPercentageBasis(fonts, name));
            }
        }

        [Flags]
        private enum ReadInputs : Byte
        {
            None = 0,
            EmBase = 1,
            RootFontSize = 2,
            KeepsPercentages = 4,
            PercentageBasis = 8,
            // anything else, e.g., other properties or the device: not remembered
            Other = 128,
        }

        /// <summary>
        /// Computes a value as the value of a property with the given name and converter,
        /// e.g., the value of a registered custom property against its syntax.
        /// </summary>
        internal static ICssValue ComputeValue(ICssValue value, IValueConverter converter, String name, ICssComputeContext context) =>
            value.Compute(new PropertyComputeContext(context, converter, name));

        #endregion

        #region Keyword State

        private enum KeywordState : Byte
        {
            Unknown,
            Initial,
            Inherit,
            Other,
        }

        #endregion

        #region Compute Context

        sealed class PropertyComputeContext : ICssComputeContext, ILocalComputeContext, IInheritingComputeContext, IRelativeComputeContext
        {
            private readonly ICssComputeContext _parent;
            private readonly IValueConverter _converter;
            private readonly String _name;

            public PropertyComputeContext(ICssComputeContext parent, IValueConverter converter, String name)
            {
                _parent = parent;
                _converter = converter;
                _name = name;
            }

            /// <summary>
            /// Gets what has been read, i.e., what the computed value depends on.
            /// </summary>
            public ReadInputs Reads { get; private set; }

            public Boolean IsRemembered => (Reads & ReadInputs.Other) == 0;

            public Double? ReadEmBase { get; private set; }

            public Double? ReadRootFontSize { get; private set; }

            public Boolean ReadKeepsPercentages { get; private set; }

            public Double? ReadPercentageBasis { get; private set; }

            public IRenderDevice Device
            {
                get
                {
                    Reads |= ReadInputs.Other;
                    return _parent.Device;
                }
            }

            public ICssProperties Properties
            {
                get
                {
                    Reads |= ReadInputs.Other;
                    return _parent is ILocalComputeContext local ? local.Properties : null;
                }
            }

            public IBrowsingContext Context
            {
                get
                {
                    Reads |= ReadInputs.Other;
                    return _parent.Context;
                }
            }

            public IValueConverter Converter => _converter;

            public ICssValue Resolve(String name)
            {
                Reads |= ReadInputs.Other;
                return _parent is CssComputeContext context && _name.Is(PropertyNames.FontSize) ? context.ResolveForFontSize(name) : _parent.Resolve(name);
            }

            public ICssValue InheritedValue(String name)
            {
                Reads |= ReadInputs.Other;
                return (_parent as IInheritingComputeContext)?.InheritedValue(name);
            }

            private IFontComputeContext Fonts => _parent as IFontComputeContext;

            public Double? EmBase
            {
                get
                {
                    Reads |= ReadInputs.EmBase;
                    return ReadEmBase = GetEmBase(Fonts, _name);
                }
            }

            public Double? RootFontSize
            {
                get
                {
                    Reads |= ReadInputs.RootFontSize;
                    return ReadRootFontSize = GetRootFontSize(Fonts, _name);
                }
            }

            public Boolean KeepsPercentages
            {
                get
                {
                    Reads |= ReadInputs.KeepsPercentages;
                    return ReadKeepsPercentages = GetKeepsPercentages(Fonts, _name);
                }
            }

            public Double? PercentageBasis
            {
                get
                {
                    Reads |= ReadInputs.PercentageBasis;
                    return ReadPercentageBasis = GetPercentageBasis(Fonts, _name);
                }
            }

            // em in font-size refers to the font size of the parent, everywhere else to the one of the element.
            public static Double? GetEmBase(IFontComputeContext fonts, String name) =>
                name.Is(PropertyNames.FontSize) ? fonts?.ParentFontSize : fonts?.FontSize;

            public static Double? GetRootFontSize(IFontComputeContext fonts, String name) =>
                name.Is(PropertyNames.FontSize) ? fonts?.FontSizeRemBase : fonts?.RootFontSize;

            // Except for font-size and line-height, percentages refer to a box (or a length) that is only known during layout.
            public static Boolean GetKeepsPercentages(IFontComputeContext fonts, String name) =>
                fonts is not null && GetPercentageBasis(fonts, name) is null;

            public static Double? GetPercentageBasis(IFontComputeContext fonts, String name) =>
                name.Is(PropertyNames.FontSize) ? fonts?.ParentFontSize :
                name.Is(PropertyNames.LineHeight) ? fonts?.FontSize :
                null;
        }

        #endregion

        #region String Representation

        public void ToCss(TextWriter writer, IStyleFormatter formatter) => writer.Write(formatter.Declaration(EscapeName(Name), Value, IsImportant));

        // a property name is a plain identifier almost always, which needs no escaping (the
        // escaping costs more than the rest of serializing a declaration of a style attribute)
        private static String EscapeName(String name)
        {
            if (name.Length == 0 || name[0] is >= '0' and <= '9' || (name.Length > 1 && name[0] == '-' && name[1] is >= '0' and <= '9') || name == "-")
            {
                return CssUtilities.Escape(name);
            }

            foreach (var ch in name)
            {
                if (!(ch is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9' or '-' or '_' || ch >= 0x80))
                {
                    return CssUtilities.Escape(name);
                }
            }

            return name;
        }

        #endregion
    }
}
