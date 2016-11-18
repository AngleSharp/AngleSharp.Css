namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-clip
    /// Gets an enumeration with the desired clip settings.
    /// </summary>
    sealed class CssBackgroundClipProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(BoxModelConverter.FromList(), AssignInitial(BoxModel.BorderBox));

        #endregion

        #region ctor

        internal CssBackgroundClipProperty()
            : base(PropertyNames.BackgroundClip)
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
