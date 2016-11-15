﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/widows
    /// Gets the number of lines, which must be left on top
    /// of a new page, on a paged media.
    /// </summary>
    sealed class CssWidowsProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.IntegerConverter.OrDefault(2);

        #endregion

        #region ctor

        internal CssWidowsProperty()
            : base(PropertyNames.Widows, PropertyFlags.Inherited)
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
