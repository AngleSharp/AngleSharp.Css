namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// Gets the value of the display mode.
    /// </summary>
    sealed class CssDisplayProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(DisplayModeConverter, AssignInitial(DisplayMode.Inline));

        #endregion

        #region ctor

        internal CssDisplayProperty()
            : base(PropertyNames.Display)
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
