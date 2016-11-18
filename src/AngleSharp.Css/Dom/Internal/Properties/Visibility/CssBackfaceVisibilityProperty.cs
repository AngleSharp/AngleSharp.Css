namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/backface-visibility
    /// Gets if the back face is visible, allowing the front face to be
    /// displayed mirrored. Otherwise the back face is not visible, hiding
    /// the front face.
    /// </summary>
    sealed class CssBackfaceVisibilityProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(BackfaceVisibilityConverter, AssignInitial(true));

        #endregion

        #region ctor

        internal CssBackfaceVisibilityProperty()
            : base(PropertyNames.BackfaceVisibility)
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
