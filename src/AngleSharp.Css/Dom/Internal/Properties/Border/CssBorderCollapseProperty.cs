namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-collapse
    /// </summary>
    sealed class CssBorderCollapseProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(BorderCollapseConverter, AssignInitial(true));

        #endregion

        #region ctor

        internal CssBorderCollapseProperty()
            : base(PropertyNames.BorderCollapse, PropertyFlags.Inherited)
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
