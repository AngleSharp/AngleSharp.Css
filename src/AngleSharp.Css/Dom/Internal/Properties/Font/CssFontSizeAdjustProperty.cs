namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information:
    /// http://dev.w3.org/csswg/css-fonts/#propdef-font-size-adjust
    /// Gets the aspect value specified by the property, if any.
    /// </summary>
    sealed class CssFontSizeAdjustProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(OptionalNumberConverter, Initial);

        #endregion

        #region ctor

        internal CssFontSizeAdjustProperty()
            : base(PropertyNames.FontSizeAdjust, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
