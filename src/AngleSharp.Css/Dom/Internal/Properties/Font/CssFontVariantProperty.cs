namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// Gets the selected font variant transformation, if any.
    /// </summary>
    sealed class CssFontVariantProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(FontVariantConverter, AssignInitial(FontVariant.Normal));

        #endregion

        #region ctor

        internal CssFontVariantProperty()
            : base(PropertyNames.FontVariant, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
