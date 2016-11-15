namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-name
    /// Gets the names of the animations to trigger.
    /// </summary>
    sealed class CssAnimationNameProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = IdentifierConverter.FromList().OrNone().OrDefault();

        #endregion

        #region ctor

        internal CssAnimationNameProperty()
            : base(PropertyNames.AnimationName)
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
