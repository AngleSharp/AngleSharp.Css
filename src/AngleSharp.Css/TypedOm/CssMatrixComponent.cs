namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents an explicit transformation matrix, i.e., matrix() or matrix3d().
    /// https://www.w3.org/TR/css-typed-om-1/#cssmatrixcomponent
    /// </summary>
    [DomName("CSSMatrixComponent")]
    public sealed class CssMatrixComponent : CssTransformComponent
    {
        private TransformMatrix _matrix;

        /// <summary>
        /// Creates a new matrix component.
        /// </summary>
        /// <param name="matrix">The transformation matrix.</param>
        /// <param name="is2D">If the matrix is a 2D matrix, i.e., matrix().</param>
        [DomConstructor]
        public CssMatrixComponent(TransformMatrix matrix, Boolean is2D = false)
            : base(is2D)
        {
            _matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
        }

        /// <summary>
        /// Gets or sets the transformation matrix.
        /// </summary>
        [DomName("matrix")]
        public TransformMatrix Matrix
        {
            get => _matrix;
            set => _matrix = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets the matrix, reduced to its 2D part if the component is 2D.
        /// </summary>
        public override TransformMatrix ToMatrix()
        {
            if (!Is2D)
            {
                return _matrix;
            }

            var m = Identity();
            m[0, 0] = _matrix[0, 0];
            m[0, 1] = _matrix[0, 1];
            m[1, 0] = _matrix[1, 0];
            m[1, 1] = _matrix[1, 1];
            m[0, 3] = _matrix[0, 3];
            m[1, 3] = _matrix[1, 3];
            return CreateMatrix(m);
        }

        /// <summary>
        /// Serializes the matrix, i.e., matrix(a, b, c, d, e, f) or matrix3d(...).
        /// </summary>
        public override String ToString()
        {
            if (Is2D)
            {
                var values = new[] { _matrix[0, 0], _matrix[1, 0], _matrix[0, 1], _matrix[1, 1], _matrix[0, 3], _matrix[1, 3] };
                return String.Concat("matrix(", String.Join(", ", values.Select(m => m.CssStringify())), ")");
            }

            var all = new Double[16];

            for (var col = 0; col < 4; col++)
            {
                for (var row = 0; row < 4; row++)
                {
                    all[col * 4 + row] = _matrix[row, col];
                }
            }

            return String.Concat("matrix3d(", String.Join(", ", all.Select(m => m.CssStringify())), ")");
        }
    }
}
