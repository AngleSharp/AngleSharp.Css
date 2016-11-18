namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-style
    /// Gets the selected column-rule line style.
    /// </summary>
    sealed class CssColumnRuleStyleProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        #endregion

        #region ctor

        internal CssColumnRuleStyleProperty()
            : base(PropertyNames.ColumnRuleStyle)
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
