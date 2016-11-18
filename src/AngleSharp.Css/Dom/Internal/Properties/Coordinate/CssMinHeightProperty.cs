namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-height
    /// Gets the minimum height of the element.
    /// </summary>
    sealed class CssMinHeightProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        #endregion

        #region ctor

        internal CssMinHeightProperty()
            : base(PropertyNames.MinHeight, PropertyFlags.Animatable)
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
