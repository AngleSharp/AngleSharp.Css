namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// Gets the selected image.
    /// </summary>
    sealed class CssListStyleImageProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(OptionalImageSourceConverter, Initial);

        #endregion

        #region ctor

        internal CssListStyleImageProperty()
            : base(PropertyNames.ListStyleImage, PropertyFlags.Inherited)
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
