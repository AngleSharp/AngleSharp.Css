namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CssColumnsProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(WithAny(
            AutoLengthConverter.Option().For(PropertyNames.ColumnWidth),
            OptionalIntegerConverter.Option().For(PropertyNames.ColumnCount)), Initial);

        #endregion

        #region ctor

        internal CssColumnsProperty()
            : base(PropertyNames.Columns, PropertyFlags.Animatable)
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
