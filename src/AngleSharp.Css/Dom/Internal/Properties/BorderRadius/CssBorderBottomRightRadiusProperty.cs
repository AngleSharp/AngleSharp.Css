namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-right-radius
    /// </summary>
    sealed class CssBorderBottomRightRadiusProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(BorderRadiusLonghandConverter, AssignInitial(Length.Zero));

        #endregion

        #region ctor

        internal CssBorderBottomRightRadiusProperty()
            : base(PropertyNames.BorderBottomRightRadius, PropertyFlags.Animatable)
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
