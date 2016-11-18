namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Dom;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-align
    /// Gets the selected horizontal alignment mode.
    /// </summary>
    sealed class CssTextAlignProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(HorizontalAlignmentConverter, AssignInitial(HorizontalAlignment.Left));

        #endregion

        #region ctor

        internal CssTextAlignProperty()
            : base(PropertyNames.TextAlign, PropertyFlags.Inherited)
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
