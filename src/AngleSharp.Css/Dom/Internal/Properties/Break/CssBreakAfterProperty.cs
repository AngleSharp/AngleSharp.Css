namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-after
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakAfterProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(BreakModeConverter, AssignInitial(BreakMode.Auto));

        #endregion

        #region ctor

        internal CssBreakAfterProperty()
            : base(PropertyNames.BreakAfter)
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
