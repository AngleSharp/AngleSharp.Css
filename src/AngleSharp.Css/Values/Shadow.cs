namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The shadow class for holding information about
    /// a box or text-shadow.
    /// </summary>
    public sealed class Shadow : ICssValue
    {
        #region Fields

        private readonly Boolean _inset;
        private readonly Length _offsetX;
        private readonly Length _offsetY;
        private readonly Length _blurRadius;
        private readonly Length _spreadRadius;
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
        public Shadow(Boolean inset, Length offsetX, Length offsetY, Length blurRadius, Length spreadRadius, Color color)
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
            get { return ToString(); }
        }

        /// <summary>
        /// Gets the color of the shadow.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets the horizontal offset.
        /// </summary>
        public Length OffsetX
        {
            get { return _offsetX; }
        }

        /// <summary>
        /// Gets the vertical offset.
        /// </summary>
        public Length OffsetY
        {
            get { return _offsetY; }
        }

        /// <summary>
        /// Gets the blur radius.
        /// </summary>
        public Length BlurRadius
        {
            get { return _blurRadius; }
        }

        /// <summary>
        /// Gets the spread radius.
        /// </summary>
        public Length SpreadRadius
        {
            get { return _spreadRadius; }
        }

        /// <summary>
        /// Gets if the shadow is inset.
        /// </summary>
        public Boolean IsInset
        {
            get { return _inset; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representing the point.
        /// </summary>
        /// <returns>The string.</returns>
        public override String ToString()
        {
            var parts = new List<String>();

            if (_inset)
            {
                parts.Add(CssKeywords.Inset);
            }

            parts.Add(_offsetX.ToString());
            parts.Add(_offsetY.ToString());

            if (_blurRadius != Length.Zero)
            {
                parts.Add(_blurRadius.ToString());
            }

            if (_spreadRadius != Length.Zero)
            {
                parts.Add(_spreadRadius.ToString());
            }

            if (_color != Color.Black)
            {
                parts.Add(_color.ToString());
            }

            return String.Join(" ", parts);
        }

        #endregion
    }
}
