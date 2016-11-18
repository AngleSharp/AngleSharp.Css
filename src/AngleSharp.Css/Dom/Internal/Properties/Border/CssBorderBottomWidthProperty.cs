namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-width
    /// </summary>
    sealed class CssBorderBottomWidthProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LineWidthConverter, AssignInitial(Length.Medium));

        #endregion

        #region ctor

        internal CssBorderBottomWidthProperty()
            : base(PropertyNames.BorderBottomWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
