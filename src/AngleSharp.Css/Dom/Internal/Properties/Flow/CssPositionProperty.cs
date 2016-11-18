namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// Gets the currently selected position mode.
    /// </summary>
    sealed class CssPositionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(PositionModeConverter, AssignInitial(PositionMode.Static));

        #endregion

        #region ctor

        internal CssPositionProperty()
            : base(PropertyNames.Position)
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
