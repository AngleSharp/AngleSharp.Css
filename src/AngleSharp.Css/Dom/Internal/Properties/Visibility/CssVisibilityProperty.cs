namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Dom;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// Gets the visibility mode.
    /// </summary>
    sealed class CssVisibilityProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(VisibilityConverter, AssignInitial(Visibility.Visible));

        #endregion

        #region ctor

        internal CssVisibilityProperty()
            : base(PropertyNames.Visibility, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
