namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// The base class of the single functions of a transform, e.g., translate().
    /// https://www.w3.org/TR/css-typed-om-1/#csstransformcomponent
    /// </summary>
    [DomName("CSSTransformComponent")]
    public abstract class CssTransformComponent
    {
        internal CssTransformComponent(Boolean is2D)
        {
            Is2D = is2D;
        }

        /// <summary>
        /// Gets or sets if the component is a 2D transformation, which ignores
        /// the components of the third dimension.
        /// </summary>
        [DomName("is2D")]
        public virtual Boolean Is2D { get; set; }

        /// <summary>
        /// Computes the matrix of the transformation.
        /// </summary>
        /// <returns>The transformation matrix.</returns>
        /// <exception cref="ArgumentException">A value cannot be converted to an absolute unit, e.g., em or %.</exception>
        [DomName("toMatrix")]
        public abstract TransformMatrix ToMatrix();

        /// <summary>
        /// Serializes the transformation, e.g., translate(10px, 20px).
        /// </summary>
        public abstract override String ToString();

        internal static CssNumericValue Require(CssNumericValue value, String name, Func<CssNumericType, Boolean> matches, String expected)
        {
            if (value is null)
                throw new ArgumentNullException(name);

            if (!matches(value.NumericType))
                throw new ArgumentException($"The value has to be {expected}.", name);

            return value;
        }

        internal static Boolean IsLengthPercentage(CssNumericType type) => type.MatchesLengthPercentage();

        internal static Boolean IsLength(CssNumericType type) => type.Matches(CssNumericBaseType.Length);

        internal static Boolean IsAngle(CssNumericType type) => type.Matches(CssNumericBaseType.Angle);

        internal static Boolean IsNumber(CssNumericType type) => type.IsNumber;

        internal static String Join(params CssNumericValue[] values) => String.Join(", ", (Object[])values);

        /// <summary>
        /// Creates the matrix from the given (row-major) values.
        /// </summary>
        internal static TransformMatrix CreateMatrix(Double[,] m)
        {
            var values = new Double[16];

            for (var col = 0; col < 4; col++)
            {
                for (var row = 0; row < 4; row++)
                {
                    values[col * 4 + row] = m[row, col];
                }
            }

            return new TransformMatrix(values);
        }

        internal static Double[,] Identity() => new Double[,]
        {
            { 1.0, 0.0, 0.0, 0.0 },
            { 0.0, 1.0, 0.0, 0.0 },
            { 0.0, 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };

        internal static TransformMatrix Multiply(TransformMatrix a, TransformMatrix b)
        {
            var m = new Double[4, 4];

            for (var row = 0; row < 4; row++)
            {
                for (var col = 0; col < 4; col++)
                {
                    var sum = 0.0;

                    for (var k = 0; k < 4; k++)
                    {
                        sum += a[row, k] * b[k, col];
                    }

                    m[row, col] = sum;
                }
            }

            return CreateMatrix(m);
        }
    }
}
