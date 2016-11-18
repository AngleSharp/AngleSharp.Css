namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-origins
    /// Gets an enumeration with the desired origin settings.
    /// </summary>
    sealed class CssBackgroundOriginProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(BoxModelConverter.FromList(), AssignInitial(BoxModel.PaddingBox));

        #endregion

        #region ctor

        internal CssBackgroundOriginProperty()
            : base(PropertyNames.BackgroundOrigin)
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
