namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-timing-function
    /// Gets the enumeration over all timing functions.
    /// </summary>
    sealed class CssTransitionTimingFunctionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(TransitionConverter.FromList(), AssignInitial(Map.TimingFunctions[CssKeywords.Ease]));

        #endregion

        #region ctor

        internal CssTransitionTimingFunctionProperty()
            : base(PropertyNames.TransitionTimingFunction)
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
