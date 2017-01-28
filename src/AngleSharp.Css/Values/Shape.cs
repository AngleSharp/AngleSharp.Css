namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS shape.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
    /// </summary>
    public sealed class Shape : ICssValue
    {
        #region Fields

        private readonly Length _top;
        private readonly Length _right;
        private readonly Length _bottom;
        private readonly Length _left;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new shape value.
        /// </summary>
        /// <param name="top">The top position.</param>
        /// <param name="right">The right position.</param>
        /// <param name="bottom">The bottom position.</param>
        /// <param name="left">The left position.</param>
        public Shape(Length top, Length right, Length bottom, Length left)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
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
        /// Gets the top side.
        /// </summary>
        public Length Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the right side.
        /// </summary>
        public Length Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the bottom side.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the left side.
        /// </summary>
        public Length Left
        {
            get { return _left; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representing the shapce.
        /// </summary>
        /// <returns>The string.</returns>
        public override String ToString()
        {
            var parts = new[]
            {
                _top.ToString(),
                _right.ToString(),
                _bottom.ToString(),
                _left.ToString(),
            };
            var fn = FunctionNames.Rect;
            var args = String.Join(", ", parts);
            return fn.CssFunction(args);
        }

        #endregion
    }
}
