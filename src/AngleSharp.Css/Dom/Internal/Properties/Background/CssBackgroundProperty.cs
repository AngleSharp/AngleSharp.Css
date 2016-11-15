﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    sealed class CssBackgroundProperty : CssShorthandProperty
    {
        #region Fields

        //[ <bg-layer> , ]* <final-bg-layer> where: 
        //  <bg-layer> = 
        //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> 
        //  <final-bg-layer> = 
        //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> || <background-color>

        private static readonly IValueConverter NormalLayerConverter = WithAny(
            OptionalImageSourceConverter.Option().For(PropertyNames.BackgroundImage),
            WithOrder(
                PointConverter.Option().For(PropertyNames.BackgroundPosition),
                BackgroundSizeConverter.StartsWithDelimiter().Option().For(PropertyNames.BackgroundSize)),
            BackgroundRepeatsConverter.Option().For(PropertyNames.BackgroundRepeat),
            BackgroundAttachmentConverter.Option().For(PropertyNames.BackgroundAttachment),
            BoxModelConverter.Option().For(PropertyNames.BackgroundOrigin),
            BoxModelConverter.Option().For(PropertyNames.BackgroundClip));

        private static readonly IValueConverter FinalLayerConverter = WithAny(
            OptionalImageSourceConverter.Option().For(PropertyNames.BackgroundImage),
            WithOrder(
                PointConverter.Option().For(PropertyNames.BackgroundPosition),
                BackgroundSizeConverter.StartsWithDelimiter().Option().For(PropertyNames.BackgroundSize)),
            BackgroundRepeatsConverter.Option().For(PropertyNames.BackgroundRepeat),
            BackgroundAttachmentConverter.Option().For(PropertyNames.BackgroundAttachment),
            BoxModelConverter.Option().For(PropertyNames.BackgroundOrigin),
            BoxModelConverter.Option().For(PropertyNames.BackgroundClip),
            CurrentColorConverter.Option().For(PropertyNames.BackgroundColor));

        private static readonly IValueConverter StyleConverter = NormalLayerConverter.RequiresEnd(FinalLayerConverter).OrDefault();

        #endregion

        #region ctor

        internal CssBackgroundProperty()
            : base(PropertyNames.Background, PropertyFlags.Animatable)
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
