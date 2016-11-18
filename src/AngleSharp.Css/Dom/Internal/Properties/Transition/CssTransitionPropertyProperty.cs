namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-property
    /// Gets the names of the selected properties.
    /// </summary>
    sealed class CssTransitionPropertyProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(AnimatableConverter.FromList(), None, AssignInitial(CssKeywords.All));
        
        #endregion

        #region ctor

        internal CssTransitionPropertyProperty()
            : base(PropertyNames.TransitionProperty)
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
