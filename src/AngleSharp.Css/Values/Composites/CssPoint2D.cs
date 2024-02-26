namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a point value consisting of two distances.
    /// </summary>
    public readonly struct CssPoint2D : ICssCompositeValue, IEquatable<CssPoint2D>
    {
        #region Basic values

        /// <summary>
        /// Gets the (50%, 50%) point.
        /// </summary>
        public static readonly CssPoint2D Center = new(CssLengthValue.Half, CssLengthValue.Half);

        /// <summary>
        /// Gets the (0, 0) point.
        /// </summary>
        public static readonly CssPoint2D LeftTop = new(CssLengthValue.Zero, CssLengthValue.Zero);

        /// <summary>
        /// Gets the (100%, 0) point.
        /// </summary>
        public static readonly CssPoint2D RightTop = new(CssLengthValue.Full, CssLengthValue.Zero);

        /// <summary>
        /// Gets the (100%, 100%) point.
        /// </summary>
        public static readonly CssPoint2D RightBottom = new(CssLengthValue.Full, CssLengthValue.Full);

        /// <summary>
        /// Gets the (0, 100%) point.
        /// </summary>
        public static readonly CssPoint2D LeftBottom = new(CssLengthValue.Zero, CssLengthValue.Full);

        /// <summary>
        /// Gets the (0, 50%) point.
        /// </summary>
        public static readonly CssPoint2D Left = new(CssLengthValue.Zero, CssLengthValue.Half);

        /// <summary>
        /// Gets the (100%, 50%) point.
        /// </summary>
        public static readonly CssPoint2D Right = new(CssLengthValue.Full, CssLengthValue.Half);

        /// <summary>
        /// Gets the (50%, 100%) point.
        /// </summary>
        public static readonly CssPoint2D Bottom = new(CssLengthValue.Half, CssLengthValue.Full);

        /// <summary>
        /// Gets the (50%, 0) point.
        /// </summary>
        public static readonly CssPoint2D Top = new(CssLengthValue.Half, CssLengthValue.Zero);

        #endregion

        #region Fields

        private readonly ICssValue _x;
        private readonly ICssValue _y;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Point.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public CssPoint2D(ICssValue x, ICssValue y)
        {
            _x = x;
            _y = y;
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
                if (Equals(Center))
                {
                    return CssKeywords.Center;
                }
                else if (Equals(Bottom))
                {
                    return CssKeywords.Bottom;
                }
                else if (Equals(Top))
                {
                    return CssKeywords.Top;
                }
                else if (Equals(Left))
                {
                    return CssKeywords.Left;
                }
                else if (Equals(Right))
                {
                    return CssKeywords.Right;
                }
                else if (Equals(LeftTop))
                {
                    return CssKeywords.LeftTop;
                }
                else if (Equals(RightTop))
                {
                    return CssKeywords.RightTop;
                }
                else if (Equals(RightBottom))
                {
                    return CssKeywords.RightBottom;
                }
                else if (Equals(LeftBottom))
                {
                    return CssKeywords.LeftBottom;
                }
                else if (_y.Equals(CssLengthValue.Half))
                {
                    return _x.CssText;
                }

                return String.Concat(_x.CssText, " ", _y.CssText);
            }
        }

        /// <summary>
        /// Gets the value for the x-coordinate.
        /// </summary>
        public ICssValue X => _x;

        /// <summary>
        /// Gets the value for the y-coordinate.
        /// </summary>
        public ICssValue Y => _y;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssPoint2D other)
        {
            var comparer = EqualityComparer<ICssValue>.Default;
            return comparer.Equals(_x, other._x) && comparer.Equals(_y, other._y);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var x = _x.Compute(context);
            var y = _x.Compute(context);

            if (x != _x || y != _y)
            {
                return new CssPoint2D(x, y);
            }

            return this;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssPoint2D value && Equals(value);

        #endregion
    }
}
