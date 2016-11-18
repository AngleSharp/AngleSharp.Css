namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-timing-function
    /// Gets the enumeration over all timing functions.
    /// </summary>
    sealed class CssAnimationTimingFunctionProperty : CssProperty
    {
        #region Fields

        private static readonly ITimingFunction Ease = Map.TimingFunctions[CssKeywords.Ease];
        private static readonly IValueConverter ListConverter = Or(TransitionConverter.FromList(), AssignInitial(Ease));

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
