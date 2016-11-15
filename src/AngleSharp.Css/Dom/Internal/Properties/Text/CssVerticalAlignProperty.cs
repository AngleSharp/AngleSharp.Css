﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Dom;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align
    /// Gets the alignment of of the element's baseline at the given length
    /// above the baseline of its parent or like absolute values, with the
    /// percentage being a percent of the line-height property.
    /// Gets the selected vertical alignment mode.
    /// </summary>
    sealed class CssVerticalAlignProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = LengthOrPercentConverter.Or(
            VerticalAlignmentConverter).OrDefault(VerticalAlignment.Baseline);

        #endregion

        #region ctor

        internal CssVerticalAlignProperty()
            : base(PropertyNames.VerticalAlign, PropertyFlags.Animatable)
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
