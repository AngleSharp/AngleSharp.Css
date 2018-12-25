namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the translate3d transformation.
    /// </summary>
    public sealed class TranslateTransform : ITransform
    {
        #region Fields

        private readonly Length _x;
        private readonly Length _y;
        private readonly Length _z;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new translate transform.
        /// </summary>
        /// <param name="x">The x shift.</param>
        /// <param name="y">The y shift.</param>
        /// <param name="z">The z shift.</param>
        public TranslateTransform(Length x, Length y, Length z)
        {
            _x = x;
            _y = y;
            _z = z;
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
                var fn = FunctionNames.Translate3d;
                var args = String.Empty;

                if (_x != Length.Zero && _y == Length.Zero && _z == Length.Zero)
                {
                    fn = FunctionNames.TranslateX;
                    args = _x.CssText;
                }
                else if (_x == Length.Zero && _y != Length.Zero && _z == Length.Zero)
                {
                    fn = FunctionNames.TranslateY;
                    args = _y.CssText;
                }
                else if (_x == Length.Zero && _y == Length.Zero && _z != Length.Zero)
                {
                    fn = FunctionNames.TranslateZ;
                    args = _z.CssText;
                }
                else if (_z == Length.Zero)
                {
                    fn = FunctionNames.Translate;
                    args = String.Join(", ", new[]
                    {
                        _x.CssText,
                        _y.CssText
                    });
                }
                else
                {
                    args = String.Join(", ", new[]
                    {
                        _x.CssText,
                        _y.CssText,
                        _z.CssText
                    });
                }

                return fn.CssFunction(args);
            }
        }

        /// <summary>
        /// Gets the shift in x-direction.
        /// </summary>
        public Length ShiftX
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the shift in y-direction.
        /// </summary>
        public Length ShiftY
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the shift in z-direction.
        /// </summary>
        public Length ShiftZ
        {
            get { return _z; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var dx = _x.ToPixel();
            var dy = _y.ToPixel();
            var dz = _z.ToPixel();
            return new TransformMatrix(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, dx, dy, dz, 0.0, 0.0, 0.0);
        }

        #endregion
    }
}
