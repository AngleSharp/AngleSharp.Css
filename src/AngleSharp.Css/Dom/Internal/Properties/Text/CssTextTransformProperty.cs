namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform
    /// Gets the selected text transformation mode.
    /// </summary>
    sealed class CssTextTransformProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(TextTransformConverter, AssignInitial(TextTransform.None));

        #endregion

        #region ctor

        internal CssTextTransformProperty()
            : base(PropertyNames.TextTransform, PropertyFlags.Inherited)
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
