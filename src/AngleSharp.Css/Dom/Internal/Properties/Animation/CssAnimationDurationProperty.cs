namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-duration
    /// Gets the durations for the animations.
    /// </summary>
    sealed class CssAnimationDurationProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = ValueConverters.TimeConverter.FromList().OrDefault(Time.Zero);

        #endregion

        #region ctor

        internal CssAnimationDurationProperty()
            : base(PropertyNames.AnimationDuration)
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
