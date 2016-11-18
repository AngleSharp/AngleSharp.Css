namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-inside
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssPageBreakInsideProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(Assign(CssKeywords.Auto, BreakMode.Auto), Assign(CssKeywords.Avoid, BreakMode.Avoid), AssignInitial(BreakMode.Auto));

        #endregion

        #region ctor

        internal CssPageBreakInsideProperty()
            : base(PropertyNames.PageBreakInside)
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
