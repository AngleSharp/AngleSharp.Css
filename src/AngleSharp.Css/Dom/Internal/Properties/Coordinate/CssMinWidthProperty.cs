﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-width
    /// Gets the minimum height of the element.
    /// </summary>
    sealed class CssMinWidthProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.LengthOrPercentConverter.OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssMinWidthProperty()
            : base(PropertyNames.MinWidth, PropertyFlags.Animatable)
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
