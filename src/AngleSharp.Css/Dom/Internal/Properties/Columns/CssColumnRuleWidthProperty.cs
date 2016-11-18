namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-width
    /// Gets the width of the column-rule.
    /// </summary>
    sealed class CssColumnRuleWidthProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LineWidthConverter, AssignInitial(Length.Medium));

        #endregion

        #region ctor

        internal CssColumnRuleWidthProperty()
            : base(PropertyNames.ColumnRuleWidth, PropertyFlags.Animatable)
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
