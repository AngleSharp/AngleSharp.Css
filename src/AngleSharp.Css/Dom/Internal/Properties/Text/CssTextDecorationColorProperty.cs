namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-color
    /// Gets the selected text-decoration color.
    /// </summary>
    sealed class CssTextDecorationColorProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(ColorConverter, AssignInitial(Color.Black));

        #endregion

        #region ctor

        internal CssTextDecorationColorProperty()
            : base(PropertyNames.TextDecorationColor, PropertyFlags.Animatable)
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
