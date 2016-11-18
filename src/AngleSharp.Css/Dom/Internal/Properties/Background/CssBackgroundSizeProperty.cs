namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-size
    /// </summary>
    sealed class CssBackgroundSizeProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(BackgroundSizeConverter.FromList(), Initial);

        #endregion

        #region ctor

        internal CssBackgroundSizeProperty()
            : base(PropertyNames.BackgroundSize, PropertyFlags.Animatable)
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
