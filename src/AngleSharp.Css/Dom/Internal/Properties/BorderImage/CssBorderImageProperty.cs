namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CssBorderImageProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter ImageConverter = Or(
            WithAny(
                OptionalImageSourceConverter.Option().For(PropertyNames.BorderImageSource),
                WithOrder(
                    BorderImageSliceConverter.Option().For(PropertyNames.BorderImageSlice),
                    BorderImageWidthConverter.StartsWithDelimiter().Option().For(PropertyNames.BorderImageWidth),
                    BorderImageOutsetConverter.StartsWithDelimiter().Option().For(PropertyNames.BorderImageOutset)),
                BorderImageRepeatConverter.Option().For(PropertyNames.BorderImageRepeat)),
            Initial);
        
        #endregion

        #region ctor

        internal CssBorderImageProperty()
            : base(PropertyNames.BorderImage)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ImageConverter; }
        }

        #endregion
    }
}
