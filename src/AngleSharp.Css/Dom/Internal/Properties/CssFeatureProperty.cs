namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using System;

    /// <summary>
    /// Represents an wrapper property for media feature instances.
    /// </summary>
    sealed class CssFeatureProperty : CssProperty
    {
        #region ctor

        internal CssFeatureProperty(String name)
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
