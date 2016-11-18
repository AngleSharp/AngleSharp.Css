namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-type
    /// Gets the selected style for the list.
    /// </summary>
    sealed class CssListStyleTypeProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(ListStyleConverter, AssignInitial(ListStyle.Disc));

        #endregion

        #region ctor

        internal CssListStyleTypeProperty()
            : base(PropertyNames.ListStyleType, PropertyFlags.Inherited)
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
