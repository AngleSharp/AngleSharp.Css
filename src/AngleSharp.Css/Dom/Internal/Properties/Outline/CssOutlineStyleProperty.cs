namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-style
    /// Gets the selected outline style.
    /// </summary>
    sealed class CssOutlineStyleProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        #endregion

        #region ctor

        internal CssOutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
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
