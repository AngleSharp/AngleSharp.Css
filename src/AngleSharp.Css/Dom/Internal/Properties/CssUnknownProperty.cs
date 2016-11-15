namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using System;

    /// <summary>
    /// Represents an unknown property that takes every value.
    /// </summary>
    sealed class CssUnknownProperty : CssProperty
    {
        #region ctor

        internal CssUnknownProperty(String name)
            : base(name)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ValueConverters.Any; }
        }

        #endregion
    }
}
