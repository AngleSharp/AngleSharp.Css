namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Extensions;
    using static ValueConverters;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// Gets the selected font-size.
    /// </summary>
    sealed class CssFontSizeProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(FontSizeConverter, AssignInitial(FontSize.Medium.ToLength()));

        #endregion

        #region ctor

        internal CssFontSizeProperty()
            : base(PropertyNames.FontSize, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
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
