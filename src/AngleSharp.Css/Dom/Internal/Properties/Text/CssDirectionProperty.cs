namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Dom;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// Gets the selected text direction.
    /// </summary>
    sealed class CssDirectionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(DirectionModeConverter, AssignInitial(DirectionMode.Ltr));

        #endregion

        #region ctor

        internal CssDirectionProperty()
            : base(PropertyNames.Direction, PropertyFlags.Inherited)
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
