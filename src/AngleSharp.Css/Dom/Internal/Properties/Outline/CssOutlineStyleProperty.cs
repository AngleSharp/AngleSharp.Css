﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-style
    /// Gets the selected outline style.
    /// </summary>
    sealed class CssOutlineStyleProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.LineStyleConverter.OrDefault(LineStyle.None);

        #endregion

        #region ctor

        internal CssOutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
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
