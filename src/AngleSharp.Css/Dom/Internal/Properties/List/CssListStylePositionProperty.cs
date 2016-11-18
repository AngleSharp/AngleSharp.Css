namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// Gets the selected position.
    /// </summary>
    sealed class CssListStylePositionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(ListPositionConverter, AssignInitial(ListPosition.Outside));

        #endregion

        #region ctor

        internal CssListStylePositionProperty()
            : base(PropertyNames.ListStylePosition, PropertyFlags.Inherited)
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
