namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-width
    /// </summary>
    sealed class CssBorderWidthProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = LineWidthConverter.Periodic(
            PropertyNames.BorderTopWidth, PropertyNames.BorderRightWidth, PropertyNames.BorderBottomWidth, PropertyNames.BorderLeftWidth).OrDefault();

        #endregion

        #region ctor

        internal CssBorderWidthProperty()
            : base(PropertyNames.BorderWidth, PropertyFlags.Animatable)
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
