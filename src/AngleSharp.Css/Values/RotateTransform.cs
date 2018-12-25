namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the rotate3d transformation.
    /// </summary>
    public sealed class RotateTransform : ITransform
    {
        #region Fields

        private readonly Double _x;
        private readonly Double _y;
        private readonly Double _z;
        private readonly Angle _angle;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new rotate transform.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="angle">The angle of rotation.</param>
        public RotateTransform(Double x, Double y, Double z, Angle angle)
        {
            _x = x;
            _y = y;
            _z = z;
            _angle = angle;
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
                var fn = FunctionNames.Rotate3d;
                var args = _angle.CssText;

                if (Double.IsNaN(_x) && Double.IsNaN(_y) && Double.IsNaN(_z))
                {
                    fn = FunctionNames.Rotate;
                }
                else if (_x == 1f && _y == 0f && _z == 0f)
                {
                    fn = FunctionNames.RotateX;
                }
                else if (_x == 0f && _y == 1f && _z == 0f)
                {
                    fn = FunctionNames.RotateY;
                }
                else if (_x == 0f && _y == 0f && _z == 1f)
                {
                    fn = FunctionNames.RotateY;
                }
                else
                {
                    args = String.Join(", ", new[]
                    {
                        _x.ToString(CultureInfo.InvariantCulture),
                        _y.ToString(CultureInfo.InvariantCulture),
                        _z.ToString(CultureInfo.InvariantCulture),
                        args
                    });
                }

                return fn.CssFunction(args);
            }
        }

        /// <summary>
        /// Gets the value of the x-component of the rotation vector.
        /// </summary>
        public Double X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the value of the y-component of the rotation vector.
        /// </summary>
        public Double Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the value of the z-component of the rotation vector.
        /// </summary>
        public Double Z
        {
            get { return _z; }
        }

        /// <summary>
        /// Gets the angle.
        /// </summary>
        public Angle Angle
        {
            get { return _angle; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var norm = 1.0 / Math.Sqrt(_x * _x + _y * _y + _z * _z);
            var sina = Math.Sin(_angle.ToRadian());
            var cosa = Math.Cos(_angle.ToRadian());
            var l = _x * norm;
            var m = _y * norm;
            var n = _z * norm;
            var omc = (1.0 - cosa);
            return new TransformMatrix(
                l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
        }

        #endregion
    }
}
