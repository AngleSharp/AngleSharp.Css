﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-indent
    /// Gets the indentation, which is either a percentage of the
    /// containing block width or specified as fixed length. Negative
    /// values are allowed.
    /// </summary>
    sealed class CssTextIndentProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = LengthOrPercentConverter.OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssTextIndentProperty()
            : base(PropertyNames.TextIndent, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
