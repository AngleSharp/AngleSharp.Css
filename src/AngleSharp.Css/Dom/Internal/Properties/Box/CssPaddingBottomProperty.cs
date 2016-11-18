namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-bottom
    /// Gets the padding relative to the height of the containing block or a
    /// fixed height.
    /// </summary>
    sealed class CssPaddingBottomProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        #endregion

        #region ctor

        internal CssPaddingBottomProperty()
            : base(PropertyNames.PaddingBottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
