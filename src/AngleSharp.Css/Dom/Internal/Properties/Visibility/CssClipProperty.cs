namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;

    /// <summary>
    /// More information can be found:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clip
    /// Gets the shape of the selected clipping region. If this value is
    /// null, then the clipping is determined automatically.
    /// </summary>
    sealed class CssClipProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.ShapeConverter.OrDefault();

        #endregion

        #region ctor

        internal CssClipProperty()
            : base(PropertyNames.Clip, PropertyFlags.Animatable)
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
