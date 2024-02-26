namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
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

        #endregion

        #region ctor

        internal CssProperty(String name, IValueConverter converter, PropertyFlags flags = PropertyFlags.None, ICssValue value = null, Boolean important = false)
        {
            _name = name.StartsWith("--") ? name : name.ToLowerInvariant();
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
            set => _value = value;
        }

        public String Value
        {
            get => _value?.CssText ?? String.Empty;
            set => _value = _converter.Convert(value);
        }

        public Boolean HasValue => _value != null;

        public PropertyFlags Flags => _flags;

        public Boolean IsInherited => (((_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited) && IsInitial) || (HasValue && _value.CssText.Is(CssKeywords.Inherit));

        public Boolean CanBeInherited => (_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited;

        public Boolean IsAnimatable => (_flags & PropertyFlags.Animatable) == PropertyFlags.Animatable;

        public Boolean IsInitial => !HasValue || _value.CssText.Is(CssKeywords.Initial);

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

        public ICssProperty Compute(ICssComputeContext context)
        {
            var propertyContext = new PropertyComputeContext(context, _converter);
            var computedValue = _value?.Compute(propertyContext);

            if (computedValue != _value)
            {
                return new CssProperty(_name, _converter, _flags, computedValue, _important);
            }

            return this;
        }

        #endregion

        #region Compute Context

        sealed class PropertyComputeContext : ICssComputeContext
        {
            private readonly ICssComputeContext _parent;
            private readonly IValueConverter _converter;

            public PropertyComputeContext(ICssComputeContext parent, IValueConverter converter)
            {
                _parent = parent;
                _converter = converter;
            }

            public IRenderDevice Device => _parent.Device;

            public IBrowsingContext Context => _parent.Context;

            public IValueConverter Converter => _converter;

            public ICssValue Resolve(String name) =>_parent.Resolve(name);
        }

        #endregion

        #region String Representation

        public void ToCss(TextWriter writer, IStyleFormatter formatter) => writer.Write(formatter.Declaration(CssUtilities.Escape(Name), Value, IsImportant));

        #endregion
    }
}
