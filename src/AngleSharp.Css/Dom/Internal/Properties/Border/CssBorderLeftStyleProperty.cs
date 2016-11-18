namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-style
    /// </summary>
    sealed class CssBorderLeftStyleProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        #endregion

        #region ctor

        internal CssBorderLeftStyleProperty()
            : base(PropertyNames.BorderLeftStyle)
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
