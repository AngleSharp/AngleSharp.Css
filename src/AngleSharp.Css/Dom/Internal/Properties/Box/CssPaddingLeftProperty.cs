﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-left
    /// Gets the padding relative to the width of the containing block or a
    /// fixed width.
    /// </summary>
    sealed class CssPaddingLeftProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.LengthOrPercentConverter.OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssPaddingLeftProperty()
            : base(PropertyNames.PaddingLeft, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
