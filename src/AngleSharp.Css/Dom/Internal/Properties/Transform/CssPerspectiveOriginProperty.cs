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

        private static readonly IValueConverter PerspectiveConverter = Or(
            LengthOrPercentConverter,
            Assign(CssKeywords.Left, new Point(Length.Zero, Length.Half)),
            Assign(CssKeywords.Center, new Point(Length.Half, Length.Half)),
            Assign(CssKeywords.Right, new Point(Length.Full, Length.Half)),
            Assign(CssKeywords.Top, new Point(Length.Half, Length.Zero)),
            Assign(CssKeywords.Bottom, new Point(Length.Half, Length.Full)),
            WithAny(
                Or(LengthOrPercentConverter, Assign(CssKeywords.Left, Length.Zero), Assign(CssKeywords.Right, Length.Full), Assign(CssKeywords.Center, Length.Half)).Option(Length.Half),
                Or(LengthOrPercentConverter, Assign(CssKeywords.Top, Length.Zero), Assign(CssKeywords.Bottom, Length.Full), Assign(CssKeywords.Center, Length.Half)).Option(Length.Half)),
            AssignInitial(Point.Center));

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
