﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-top
    /// Gets the margin relative to the height of the containing block or a
    /// fixed height, if any.
    /// Gets if the margin is automatically determined.
    /// </summary>
    sealed class CssMarginTopProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.AutoLengthOrPercentConverter.OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssMarginTopProperty()
            : base(PropertyNames.MarginTop, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
