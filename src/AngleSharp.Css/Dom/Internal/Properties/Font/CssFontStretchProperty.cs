namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// Gets the selected font stretch setting.
    /// </summary>
    sealed class CssFontStretchProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(FontStretchConverter, AssignInitial(FontStretch.Normal));

        #endregion

        #region ctor

        internal CssFontStretchProperty()
            : base(PropertyNames.FontStretch, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
