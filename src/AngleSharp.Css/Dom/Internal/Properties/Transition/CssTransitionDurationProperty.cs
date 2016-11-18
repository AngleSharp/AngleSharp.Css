namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-duration
    /// Gets the durations for the transitions.
    /// </summary>
    sealed class CssTransitionDurationProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(TimeConverter.FromList(), AssignInitial(Time.Zero));

        #endregion

        #region ctor

        internal CssTransitionDurationProperty()
            : base(PropertyNames.TransitionDuration)
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
