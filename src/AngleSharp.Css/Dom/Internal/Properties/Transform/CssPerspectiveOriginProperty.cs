namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// Gets the position of the abscissa of the vanishing point.
    /// Gets the position of the ordinate of the vanishing point.
    /// </summary>
    sealed class CssPerspectiveOriginProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter PerspectiveConverter = LengthOrPercentConverter.Or(
            CssKeywords.Left, new Point(Length.Zero, Length.Half)).Or(
            CssKeywords.Center, new Point(Length.Half, Length.Half)).Or(
            CssKeywords.Right, new Point(Length.Full, Length.Half)).Or(
            CssKeywords.Top, new Point(Length.Half, Length.Zero)).Or(
            CssKeywords.Bottom, new Point(Length.Half, Length.Full)).Or(
            WithAny(
                LengthOrPercentConverter.Or(CssKeywords.Left, Length.Zero).Or(CssKeywords.Right, Length.Full).Or(CssKeywords.Center, Length.Half).Option(Length.Half),
                LengthOrPercentConverter.Or(CssKeywords.Top, Length.Zero).Or(CssKeywords.Bottom, Length.Full).Or(CssKeywords.Center, Length.Half).Option(Length.Half))).
            OrDefault(Point.Center);

        #endregion

        #region ctor

        internal CssPerspectiveOriginProperty()
            : base(PropertyNames.PerspectiveOrigin, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return PerspectiveConverter; }
        }

        #endregion
    }
}
