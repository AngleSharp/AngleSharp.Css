namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    sealed class CssBorderImageWidthProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(BorderImageWidthConverter, AssignInitial(Length.Full));

        #endregion

        #region ctor

        internal CssBorderImageWidthProperty()
            : base(PropertyNames.BorderImageWidth)
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
