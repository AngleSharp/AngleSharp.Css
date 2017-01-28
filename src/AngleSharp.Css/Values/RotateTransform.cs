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

        private readonly Single _x;
        private readonly Single _y;
        private readonly Single _z;
        private readonly Angle _angle;

        #endregion

        #region ctor

        internal RotateTransform(Single x, Single y, Single z, Angle angle)
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
            get { return ToString(); }
        }

        /// <summary>
        /// Gets the value of the x-component of the rotation vector.
        /// </summary>
        public Single X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the value of the y-component of the rotation vector.
        /// </summary>
        public Single Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the value of the z-component of the rotation vector.
        /// </summary>
        public Single Z
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
        /// Serializes to the rotate function.
        /// </summary>
        public override String ToString()
        {
            var fn = FunctionNames.Rotate3d;
            var args = _angle.ToString();

            if (Single.IsNaN(_x) && Single.IsNaN(_y) && Single.IsNaN(_z))
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

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var norm = 1f / (Single)Math.Sqrt(_x * _x + _y * _y + _z * _z);
            var sina = (Single)Math.Sin(_angle.ToRadian());
            var cosa = (Single)Math.Cos(_angle.ToRadian());
            var l = _x * norm;
            var m = _y * norm;
            var n = _z * norm;
            var omc = (1f - cosa);
            return new TransformMatrix(
                l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion
    }
}
