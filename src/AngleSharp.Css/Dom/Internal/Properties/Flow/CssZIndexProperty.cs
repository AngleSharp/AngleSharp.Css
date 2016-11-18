namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/z-index
    /// Gets the index in the stacking order, if any.
    /// </summary>
    sealed class CssZIndexProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(OptionalIntegerConverter, Initial);

        #endregion

        #region ctor

        internal CssZIndexProperty()
            : base(PropertyNames.ZIndex, PropertyFlags.Animatable)
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
