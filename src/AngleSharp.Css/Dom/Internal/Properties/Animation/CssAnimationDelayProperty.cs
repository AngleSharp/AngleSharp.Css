namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-delay
    /// Gets the delays for the animations.
    /// </summary>
    sealed class CssAnimationDelayProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(TimeConverter.FromList(), AssignInitial(Time.Zero));

        #endregion

        #region ctor

        internal CssAnimationDelayProperty()
            : base(PropertyNames.AnimationDelay)
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
