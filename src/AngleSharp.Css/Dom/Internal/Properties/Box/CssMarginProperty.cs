namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    sealed class CssMarginProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(
            AutoLengthOrPercentConverter.Periodic(PropertyNames.MarginTop, PropertyNames.MarginRight, PropertyNames.MarginBottom, PropertyNames.MarginLeft),
            AssignInitial(Length.Zero));

        #endregion

        #region ctor

        internal CssMarginProperty()
            : base(PropertyNames.Margin)
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
