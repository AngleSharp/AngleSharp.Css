namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-timing-function
    /// Gets the enumeration over all timing functions.
    /// </summary>
    sealed class CssAnimationTimingFunctionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = TransitionConverter.FromList().OrDefault(Map.TimingFunctions[CssKeywords.Ease]);

        #endregion

        #region ctor

        internal CssAnimationTimingFunctionProperty()
            : base(PropertyNames.AnimationTimingFunction)
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
