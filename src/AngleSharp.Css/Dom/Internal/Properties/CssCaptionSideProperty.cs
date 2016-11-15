namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/caption-side
    /// Gets if the caption box will be above the table.
    /// Otherwise the caption box will be below the table.
    /// </summary>
    sealed class CssCaptionSideProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.CaptionSideConverter.OrDefault(true);

        #endregion

        #region ctor

        internal CssCaptionSideProperty()
            : base(PropertyNames.CaptionSide)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
