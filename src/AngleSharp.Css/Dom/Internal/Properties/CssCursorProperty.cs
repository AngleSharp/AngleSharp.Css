namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    sealed class CssCursorProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(Or(ImageSourceConverter,
            WithOrder(
                ImageSourceConverter.Required(),
                NumberConverter.Required(),
                NumberConverter.Required())).RequiresEnd(
            Map.SystemCursors.ToConverter()), AssignInitial(SystemCursor.Auto));

        #endregion

        #region ctor

        internal CssCursorProperty()
            : base(PropertyNames.Cursor, PropertyFlags.Inherited)
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
