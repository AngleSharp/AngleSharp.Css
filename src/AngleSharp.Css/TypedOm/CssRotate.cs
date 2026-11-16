namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents a rotation, i.e., rotate() or rotate3d().
    /// https://www.w3.org/TR/css-typed-om-1/#cssrotate
    /// </summary>
    [DomName("CSSRotate")]
    public sealed class CssRotate : CssTransformComponent
    {
        private CssNumericValue _x;
        private CssNumericValue _y;
        private CssNumericValue _z;
        private CssNumericValue _angle;

        /// <summary>
        /// Creates a new 2D rotation.
        /// </summary>
        /// <param name="angle">The angle of the rotation.</param>
        /// <exception cref="ArgumentException">The value is not an angle.</exception>
        [DomConstructor]
        public CssRotate(CssNumericValue angle)
            : base(true)
        {
            _x = 0.0;
            _y = 0.0;
            _z = 1.0;
            _angle = Require(angle, nameof(angle), IsAngle, "an angle");
        }

        /// <summary>
        /// Creates a new 3D rotation around the given vector.
        /// </summary>
        /// <param name="x">The x-component of the rotation axis.</param>
        /// <param name="y">The y-component of the rotation axis.</param>
        /// <param name="z">The z-component of the rotation axis.</param>
        /// <param name="angle">The angle of the rotation.</param>
        /// <exception cref="ArgumentException">A value has the wrong type.</exception>
        [DomConstructor]
        public CssRotate(CssNumericValue x, CssNumericValue y, CssNumericValue z, CssNumericValue angle)
            : base(false)
        {
            _x = Require(x, nameof(x), IsNumber, "a number");
            _y = Require(y, nameof(y), IsNumber, "a number");
            _z = Require(z, nameof(z), IsNumber, "a number");
            _angle = Require(angle, nameof(angle), IsAngle, "an angle");
        }

        /// <summary>
        /// Gets or sets the x-component of the rotation axis.
        /// </summary>
        [DomName("x")]
        public CssNumericValue X
        {
            get => _x;
            set => _x = Require(value, nameof(value), IsNumber, "a number");
        }

        /// <summary>
        /// Gets or sets the y-component of the rotation axis.
        /// </summary>
        [DomName("y")]
        public CssNumericValue Y
        {
            get => _y;
            set => _y = Require(value, nameof(value), IsNumber, "a number");
        }

        /// <summary>
        /// Gets or sets the z-component of the rotation axis.
        /// </summary>
        [DomName("z")]
        public CssNumericValue Z
        {
            get => _z;
            set => _z = Require(value, nameof(value), IsNumber, "a number");
        }

        /// <summary>
        /// Gets or sets the angle of the rotation.
        /// </summary>
        [DomName("angle")]
        public CssNumericValue Angle
        {
            get => _angle;
            set => _angle = Require(value, nameof(value), IsAngle, "an angle");
        }

        /// <summary>
        /// Computes the matrix of the rotation.
        /// </summary>
        public override TransformMatrix ToMatrix()
        {
            var alpha = _angle.To("rad").Value;
            var x = Is2D ? 0.0 : _x.To("number").Value;
            var y = Is2D ? 0.0 : _y.To("number").Value;
            var z = Is2D ? 1.0 : _z.To("number").Value;
            var length = Math.Sqrt(x * x + y * y + z * z);

            if (length == 0.0)
            {
                // a rotation around no axis does not rotate
                return CreateMatrix(Identity());
            }

            var l = x / length;
            var m = y / length;
            var n = z / length;
            var sin = Math.Sin(alpha);
            var cos = Math.Cos(alpha);
            var omc = 1.0 - cos;
            var matrix = Identity();
            matrix[0, 0] = l * l * omc + cos;
            matrix[0, 1] = m * l * omc - n * sin;
            matrix[0, 2] = n * l * omc + m * sin;
            matrix[1, 0] = l * m * omc + n * sin;
            matrix[1, 1] = m * m * omc + cos;
            matrix[1, 2] = n * m * omc - l * sin;
            matrix[2, 0] = l * n * omc - m * sin;
            matrix[2, 1] = m * n * omc + l * sin;
            matrix[2, 2] = n * n * omc + cos;
            return CreateMatrix(matrix);
        }

        /// <summary>
        /// Serializes the rotation.
        /// </summary>
        public override String ToString() => Is2D ?
            String.Concat("rotate(", _angle.ToString(), ")") :
            String.Concat("rotate3d(", Join(_x, _y, _z, _angle), ")");
    }
}
