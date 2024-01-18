namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS background definition.
    /// </summary>
    sealed class CssBackgroundValue : ICssCompositeValue, IEquatable<CssBackgroundValue>
    {
        #region Fields

        private readonly ICssValue _layers;
        private readonly ICssValue _color;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS background definition.
        /// </summary>
        /// <param name="layers">The used layers.</param>
        /// <param name="color">The set color.</param>
        public CssBackgroundValue(ICssValue layers, ICssValue color)
        {
            _layers = layers;
            _color = color;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used color.
        /// </summary>
        public ICssValue Color => _color;

        /// <summary>
        /// Gets the defined layers.
        /// </summary>
        public ICssValue Layers => _layers;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                if (_layers != null)
                {
                    var layer = _layers.CssText;

                    if (_color != null && _color is CssInitialValue == false)
                    {
                        var sep = layer.Length > 0 ? " " : "";
                        var color = _color.CssText;
                        return String.Concat(layer, sep, color);
                    }

                    return layer;
                }

                return _color?.CssText ?? String.Empty;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssBackgroundValue other)
        {
            return _color.Equals(other._color) && _layers.Equals(other._layers);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssBackgroundValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var layers = _layers.Compute(context);
            var color = _color.Compute(context);
            return new CssBackgroundValue(layers, color);
        }

        #endregion
    }
}
