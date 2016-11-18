namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// Gets an iteration over all defined directions.
    /// </summary>
    sealed class CssAnimationDirectionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(AnimationDirectionConverter.FromList(), AssignInitial(AnimationDirection.Normal));

        #endregion

        #region ctor

        internal CssAnimationDirectionProperty()
            : base(PropertyNames.AnimationDirection)
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
