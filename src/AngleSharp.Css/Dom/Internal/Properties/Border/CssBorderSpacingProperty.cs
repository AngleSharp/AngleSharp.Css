namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-spacing
    /// </summary>
    sealed class CssBorderSpacingProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LengthConverter.Many(1, 2), AssignInitial(Length.Zero));

        #endregion

        #region ctor

        internal CssBorderSpacingProperty()
            : base(PropertyNames.BorderSpacing, PropertyFlags.Inherited)
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
