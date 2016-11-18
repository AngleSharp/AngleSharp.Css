namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// Gets the width of a single columns.
    /// </summary>
    sealed class CssColumnWidthProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(AutoLengthConverter, AssignInitial(CssKeywords.Auto));

        #endregion

        #region ctor

        internal CssColumnWidthProperty()
            : base(PropertyNames.ColumnWidth, PropertyFlags.Animatable)
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
