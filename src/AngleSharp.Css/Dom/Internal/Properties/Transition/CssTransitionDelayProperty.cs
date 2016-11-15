namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-delay
    /// Gets the delays for the transitions.
    /// </summary>
    sealed class CssTransitionDelayProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = ValueConverters.TimeConverter.FromList().OrDefault(Time.Zero);

        #endregion

        #region ctor

        internal CssTransitionDelayProperty()
            : base(PropertyNames.TransitionDelay)
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
