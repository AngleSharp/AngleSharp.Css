﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// Gets the value of the floating property.
    /// </summary>
    sealed class CssFloatProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = ValueConverters.FloatingConverter.OrDefault(Floating.None);

        #endregion

        #region ctor

        internal CssFloatProperty()
            : base(PropertyNames.Float)
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
