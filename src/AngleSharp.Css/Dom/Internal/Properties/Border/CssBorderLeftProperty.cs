namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left
    /// </summary>
    sealed class CssBorderLeftProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(
            WithAny(
                LineWidthConverter.Option().For(PropertyNames.BorderLeftWidth),
                LineStyleConverter.Option().For(PropertyNames.BorderLeftStyle),
                CurrentColorConverter.Option().For(PropertyNames.BorderLeftColor)),
            Initial);

        #endregion

        #region ctor

        internal CssBorderLeftProperty()
            : base(PropertyNames.BorderLeft, PropertyFlags.Animatable)
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
