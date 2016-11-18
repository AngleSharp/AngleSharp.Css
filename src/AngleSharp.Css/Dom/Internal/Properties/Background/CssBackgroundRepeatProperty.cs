namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-repeat
    /// Gets an enumeration with the horizontal repeat modes.
    /// Gets an enumeration with the vertical repeat modes.
    /// </summary>
    sealed class CssBackgroundRepeatProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(BackgroundRepeatsConverter.FromList(), AssignInitial(BackgroundRepeat.Repeat));

        #endregion

        #region ctor

        internal CssBackgroundRepeatProperty()
            : base(PropertyNames.BackgroundRepeat)
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
