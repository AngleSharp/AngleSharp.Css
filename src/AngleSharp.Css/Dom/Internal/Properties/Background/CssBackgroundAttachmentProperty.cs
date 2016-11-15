namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-attachment
    /// Gets an enumeration with the desired attachment settings.
    /// </summary>
    sealed class CssBackgroundAttachmentProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter AttachmentConverter = BackgroundAttachmentConverter.FromList().OrDefault(BackgroundAttachment.Scroll);

        #endregion

        #region ctor

        internal CssBackgroundAttachmentProperty()
            : base(PropertyNames.BackgroundAttachment)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return AttachmentConverter; }
        }

        #endregion
    }
}
