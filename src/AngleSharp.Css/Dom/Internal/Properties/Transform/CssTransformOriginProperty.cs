namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// Gets how far from the left edge of the box the origin of the
    /// transform is set.
    /// Gets how far from the top edge of the box the origin of the
    /// transform is set.
    /// Gets how far from the user eye the z = 0 origin is set.
    /// </summary>
    sealed class CssTransformOriginProperty : CssProperty
    {
        #region Fields

        private static IValueConverter StyleConverter = Or(
            WithOrder(
                Or(
                    LengthOrPercentConverter, 
                    Assign(CssKeywords.Center, Point.Center),
                    WithAny(Or(LengthOrPercentConverter, Assign(CssKeywords.Left, Length.Zero), Assign(CssKeywords.Right, Length.Full), Assign(CssKeywords.Center, Length.Half).Option(Length.Half)), 
                    Or(LengthOrPercentConverter, Assign(CssKeywords.Top, Length.Zero), Assign(CssKeywords.Bottom, Length.Full), Assign(CssKeywords.Center, Length.Half).Option(Length.Half))),
                    WithAny(Or(LengthOrPercentConverter, Assign(CssKeywords.Top, Length.Zero), Assign(CssKeywords.Bottom, Length.Full), Assign(CssKeywords.Center, Length.Half).Option(Length.Half)),
                    Or(LengthOrPercentConverter, Assign(CssKeywords.Left, Length.Zero), Assign(CssKeywords.Right, Length.Full), Assign(CssKeywords.Center, Length.Half).Option(Length.Half)))).Required(),
                LengthConverter.Option(Length.Zero)),
            AssignInitial(Point.Center));

        #endregion

        #region ctor

        internal CssTransformOriginProperty()
            : base(PropertyNames.TransformOrigin, PropertyFlags.Animatable)
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
