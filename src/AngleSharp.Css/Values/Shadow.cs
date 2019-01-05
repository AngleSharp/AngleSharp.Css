namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The shadow class for holding information about a box or text-shadow.
    /// </summary>
    class Shadow : ICssValue
    {
        #region Fields

        private readonly Boolean _inset;
        private readonly ICssValue _offsetX;
        private readonly ICssValue _offsetY;
        private readonly ICssValue _blurRadius;
        private readonly ICssValue _spreadRadius;
        private readonly Color _color;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS shadow.
        /// </summary>
        /// <param name="inset">If the shadow is an inset.</param>
        /// <param name="offsetX">The x-coordinate offset.</param>
        /// <param name="offsetY">The y-coordinate offset.</param>
        /// <param name="blurRadius">The blur radius of the shadow.</param>
        /// <param name="spreadRadius">The spread radius of the shadow.</param>
        /// <param name="color">The color of the shadow.</param>
        public Shadow(Boolean inset, ICssValue offsetX, ICssValue offsetY, ICssValue blurRadius, ICssValue spreadRadius, Color color)
        {
            _inset = inset;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _blurRadius = blurRadius;
            _spreadRadius = spreadRadius;
            _color = color;
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var parts = new List<String>();

                if (_inset)
                {
                    parts.Add(CssKeywords.Inset);
                }

                parts.Add(_offsetX.CssText);
                parts.Add(_offsetY.CssText);

                if (_blurRadius != null && !_blurRadius.Equals(Length.Zero))
                {
                    parts.Add(_blurRadius.CssText);
                }

                if (_spreadRadius != null && !_spreadRadius.Equals(Length.Zero))
                {
                    parts.Add(_spreadRadius.CssText);
                }

                if (_color != Color.Black)
                {
                    parts.Add(_color.CssText);
                }

                return String.Join(" ", parts);
            }
        }

        /// <summary>
        /// Gets the color of the shadow.
        /// </summary>
        public Color Color => _color;

        /// <summary>
        /// Gets the horizontal offset.
        /// </summary>
        public ICssValue OffsetX => _offsetX;

        /// <summary>
        /// Gets the vertical offset.
        /// </summary>
        public ICssValue OffsetY => _offsetY;

        /// <summary>
        /// Gets the blur radius.
        /// </summary>
        public ICssValue BlurRadius => _blurRadius;

        /// <summary>
        /// Gets the spread radius.
        /// </summary>
        public ICssValue SpreadRadius => _spreadRadius;

        /// <summary>
        /// Gets if the shadow is inset.
        /// </summary>
        public Boolean IsInset => _inset;

        #endregion
    }
}
