namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/bottom
    /// </summary>
    sealed class CssBottomProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto));

        #endregion

        #region ctor

        internal CssBottomProperty()
            : base(PropertyNames.Bottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
