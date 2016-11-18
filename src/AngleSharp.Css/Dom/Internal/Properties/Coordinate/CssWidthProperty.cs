namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/width
    /// </summary>
    sealed class CssWidthProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(AutoLengthOrPercentConverter, AssignInitial(CssKeywords.Auto));

        #endregion

        #region ctor

        internal CssWidthProperty()
            : base(PropertyNames.Width, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
