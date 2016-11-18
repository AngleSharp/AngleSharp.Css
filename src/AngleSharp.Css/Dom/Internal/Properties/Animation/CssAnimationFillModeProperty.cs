namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// Gets an iteration over all defined fill modes.
    /// </summary>
    sealed class CssAnimationFillModeProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(AnimationFillStyleConverter.FromList(), AssignInitial(AnimationFillStyle.None));

        #endregion

        #region ctor

        internal CssAnimationFillModeProperty()
            : base(PropertyNames.AnimationFillMode)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion
    }
}
