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

        public ICssProperty Compute(ICssComputeContext context)
        {
            var propertyContext = new PropertyComputeContext(context, _converter, _name);
            var computedValue = _name.StartsWith("--", StringComparison.Ordinal) ?
                context.Resolve(_name) ?? CssInvalidValue.Instance :
                _value is CssChildValue child ? child.Compute(propertyContext, _name) :
                _value is CssReferenceValue reference ? reference.ComputeSubstituted(propertyContext) : _value?.Compute(propertyContext);

            if (computedValue != _value)
            {
                return new CssProperty(_name, _converter, _flags, computedValue, _important);
            }

            return this;
        }

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

            public IRenderDevice Device => _parent.Device;

            public ICssProperties Properties => _parent is ILocalComputeContext local ? local.Properties : null;

            public IBrowsingContext Context => _parent.Context;

            public IValueConverter Converter => _converter;

            public ICssValue Resolve(String name) =>_parent.Resolve(name);

            public ICssValue InheritedValue(String name) => (_parent as IInheritingComputeContext)?.InheritedValue(name);

            private IFontComputeContext Fonts => _parent as IFontComputeContext;

            // em in font-size refers to the font size of the parent, everywhere else to the one of the element.
            public Double? EmBase => _name.Is(PropertyNames.FontSize) ? Fonts?.ParentFontSize : Fonts?.FontSize;

            public Double? RootFontSize => _name.Is(PropertyNames.FontSize) ? Fonts?.FontSizeRemBase : Fonts?.RootFontSize;

            // Except for font-size and line-height, percentages refer to a box (or a length) that is only known during layout.
            public Boolean KeepsPercentages => Fonts is not null && PercentageBasis is null;

            public Double? PercentageBasis =>
                _name.Is(PropertyNames.FontSize) ? Fonts?.ParentFontSize :
                _name.Is(PropertyNames.LineHeight) ? Fonts?.FontSize :
                null;
        }

        #endregion

        #region String Representation

        public void ToCss(TextWriter writer, IStyleFormatter formatter) => writer.Write(formatter.Declaration(CssUtilities.Escape(Name), Value, IsImportant));

        #endregion
    }
}
