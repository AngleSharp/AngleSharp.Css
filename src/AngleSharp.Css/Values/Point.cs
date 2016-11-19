namespace AngleSharp.Css.Values
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a point value consisting of two distances.
    /// </summary>
    public struct Point : IEquatable<Point>, IComparable<Point>, IFormattable
    {
        #region Basic values

        /// <summary>
        /// Gets the (50%, 50%) point.
        /// </summary>
        public static readonly Point Center = new Point(Length.Half, Length.Half);

        /// <summary>
        /// Gets the (0, 0) point.
        /// </summary>
        public static readonly Point LeftTop = new Point(Length.Zero, Length.Zero);

        /// <summary>
        /// Gets the (100%, 0) point.
        /// </summary>
        public static readonly Point RightTop = new Point(Length.Full, Length.Zero);

        /// <summary>
        /// Gets the (100%, 100%) point.
        /// </summary>
        public static readonly Point RightBottom = new Point(Length.Full, Length.Full);

        /// <summary>
        /// Gets the (0, 100%) point.
        /// </summary>
        public static readonly Point LeftBottom = new Point(Length.Zero, Length.Full);

        #endregion

        #region Fields

        private readonly Length _x;
        private readonly Length _y;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Point.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public Point(Length x, Length y)
        {
            _x = x;
            _y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the x-coordinate.
        /// </summary>
        public Length X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the value for the y-coordinate.
        /// </summary>
        public Length Y
        {
            get { return _y; }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two points.
        /// </summary>
        public static Boolean operator >=(Point a, Point b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two points.
        /// </summary>
        public static Boolean operator >(Point a, Point b)
        {
            return a.CompareTo(b) == 1;
        }

        /// <summary>
        /// Compares the magnitude of two points.
        /// </summary>
        public static Boolean operator <=(Point a, Point b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two points.
        /// </summary>
        public static Boolean operator <(Point a, Point b)
        {
            return a.CompareTo(b) == -1;
        }

        /// <summary>
        /// Compares the current point against the given one.
        /// </summary>
        /// <param name="other">The point to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Point other)
        {
            var x = _x.CompareTo(other._x);
            var y = _y.CompareTo(other._y);
            
            if (x == 0 && y == 0)
            {
                return 0;
            }
            else if (x > 0 && y > 0)
            {
                return 1;
            }
            else if (x < 0 && y < 0)
            {
                return -1;
            }
            else if (x > 0 || y > 0)
            {
                return 1;
            }

            return -1;
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks the equality of the two given points.
        /// </summary>
        /// <param name="a">The left point.</param>
        /// <param name="b">The right point.</param>
        /// <returns>True if both points are equal, otherwise false.</returns>
        public static Boolean operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks the inequality of the two given points.
        /// </summary>
        /// <param name="a">The left point.</param>
        /// <param name="b">The right point.</param>
        /// <returns>True if both points are not equal, otherwise false.</returns>
        public static Boolean operator !=(Point a, Point b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Checks if both points are actually equal.
        /// </summary>
        /// <param name="other">The other point to compare to.</param>
        /// <returns>True if both points are equal, otherwise false.</returns>
        public Boolean Equals(Point other)
        {
            return _x == other._x && _y == other._y;
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Point?;

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

        #region String Representation

        /// <summary>
        /// Returns a string representing the point.
        /// </summary>
        /// <returns>The string.</returns>
        public override String ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a formatted string representing the point.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            if (Equals(Center))
            {
                return CssKeywords.Center;
            }
            else if (Equals(LeftTop))
            {
                return "left top";
            }
            else if (Equals(RightTop))
            {
                return "right top";
            }
            else if (Equals(RightBottom))
            {
                return "right bottom";
            }
            else if (Equals(LeftBottom))
            {
                return "left bottom";
            }

            return String.Concat(_x.ToString(), " ", _y.ToString());
        }

        #endregion
    }
}
