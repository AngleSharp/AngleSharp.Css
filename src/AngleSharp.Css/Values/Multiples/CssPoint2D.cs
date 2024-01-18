namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a point value consisting of two distances.
    /// </summary>
    public readonly struct CssPoint2D : IEquatable<CssPoint2D>, ICssPrimitiveValue
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

        #endregion

        #region Equality

        /// <summary>
        /// Checks the equality of the two given points.
        /// </summary>
        /// <param name="a">The left point.</param>
        /// <param name="b">The right point.</param>
        /// <returns>True if both points are equal, otherwise false.</returns>
        public static Boolean operator ==(CssPoint2D a, CssPoint2D b) => a.Equals(b);

        /// <summary>
        /// Checks the inequality of the two given points.
        /// </summary>
        /// <param name="a">The left point.</param>
        /// <param name="b">The right point.</param>
        /// <returns>True if both points are not equal, otherwise false.</returns>
        public static Boolean operator !=(CssPoint2D a, CssPoint2D b) => !a.Equals(b);

        /// <summary>
        /// Checks if both points are actually equal.
        /// </summary>
        /// <param name="other">The other point to compare to.</param>
        /// <returns>True if both points are equal, otherwise false.</returns>
        public Boolean Equals(CssPoint2D other) => _x.Equals(other._x) && _y.Equals(other._y);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssPoint2D?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current point.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            var hash = 17 * 37 + _x.GetHashCode();
            return hash * 37 + _y.GetHashCode();
        }

        #endregion
    }
}
