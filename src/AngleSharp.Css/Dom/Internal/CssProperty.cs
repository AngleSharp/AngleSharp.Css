namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    abstract class CssProperty : ICssProperty
    {
        #region Fields

        private readonly PropertyFlags _flags;
        private readonly String _name;

        private Boolean _important;
        private ICssValue _value;

        #endregion

        #region ctor

        internal CssProperty(String name, PropertyFlags flags = PropertyFlags.None)
        {
            _name = name;
            _flags = flags;
        }

        #endregion

        #region Properties
        
        public String Value
        {
            get { return _value?.CssText ?? CssKeywords.Initial; }
            set { _value = ValueConverters.Inherit.Convert(value) ?? Converter.Convert(value); }
        }

        public Boolean HasValue
        {
            get { return _value != null; }
        }

        public Boolean IsInherited
        {
            get { return (((_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited) && IsInitial) || (HasValue && _value.CssText.Is(CssKeywords.Inherit)); }
        }
        public Boolean CanBeInherited
        {
            get { return (_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited; }
        }

        public Boolean IsAnimatable
        {
            get { return (_flags & PropertyFlags.Animatable) == PropertyFlags.Animatable; }
        }

        public Boolean IsInitial
        {
            get { return !HasValue || _value.CssText.Is(CssKeywords.Initial); }
        }

        public String Name
        {
            get { return _name; }
        }

        public Boolean IsImportant
        {
            get { return _important; }
            set { _important = value; }
        }

        public String CssText
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Internal Properties

        internal Boolean CanBeHashless
        {
            get { return (_flags & PropertyFlags.Hashless) == PropertyFlags.Hashless; }
        }

        internal Boolean CanBeUnitless
        {
            get { return (_flags & PropertyFlags.Unitless) == PropertyFlags.Unitless; }
        }

        internal Boolean IsShorthand
        {
            get { return (_flags & PropertyFlags.Shorthand) == PropertyFlags.Shorthand; }
        }

        internal abstract IValueConverter Converter
        {
            get;
        }

        #endregion

        #region String Representation

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Declaration(Name, Value, IsImportant));
        }

        #endregion
    }
}
