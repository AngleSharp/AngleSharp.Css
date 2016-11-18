namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CssOutlineProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(
            WithAny(
                LineWidthConverter.Option().For(PropertyNames.OutlineWidth),
                LineStyleConverter.Option().For(PropertyNames.OutlineStyle),
                InvertedColorConverter.Option().For(PropertyNames.OutlineColor)),
            Initial);

        #endregion

        #region ctor

        internal CssOutlineProperty()
            : base(PropertyNames.Outline, PropertyFlags.Animatable)
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
