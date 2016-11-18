namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CssPaddingProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(
            LengthOrPercentConverter.Periodic(PropertyNames.PaddingTop, PropertyNames.PaddingRight, PropertyNames.PaddingBottom, PropertyNames.PaddingLeft),
            AssignInitial(Length.Zero));

        #endregion

        #region ctor

        internal CssPaddingProperty()
            : base(PropertyNames.Padding)
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
