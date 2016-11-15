﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom
    /// </summary>
    sealed class CssBorderBottomProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = WithAny(
            LineWidthConverter.Option().For(PropertyNames.BorderBottomWidth),
            LineStyleConverter.Option().For(PropertyNames.BorderBottomStyle),
            CurrentColorConverter.Option().For(PropertyNames.BorderBottomColor)
        ).OrDefault();

        #endregion

        #region ctor

        internal CssBorderBottomProperty()
            : base(PropertyNames.BorderBottom, PropertyFlags.Animatable)
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
