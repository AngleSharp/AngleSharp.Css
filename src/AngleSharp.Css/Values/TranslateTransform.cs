namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the translate3d transformation.
    /// </summary>
    public sealed class TranslateTransform : ITransform
    {
        #region Fields

        private readonly ICssValue _x;
        private readonly ICssValue _y;
        private readonly ICssValue _z;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new translate transform.
        /// </summary>
        /// <param name="x">The x shift.</param>
        /// <param name="y">The y shift.</param>
        /// <param name="z">The z shift.</param>
        public TranslateTransform(ICssValue x, ICssValue y, ICssValue z)
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

                if (_x != null && _y == null && _z == null)
                {
                    fn = FunctionNames.TranslateX;
                    args = _x.CssText;
                }
                else if (_x == null && _y != null && _z == null)
                {
                    fn = FunctionNames.TranslateY;
                    args = _y.CssText;
                }
                else if (_x == null && _y == null && _z != null)
                {
                    fn = FunctionNames.TranslateZ;
                    args = _z.CssText;
                }
                else if (_z == null)
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
        public ICssValue ShiftX
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the shift in y-direction.
        /// </summary>
        public ICssValue ShiftY
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the shift in z-direction.
        /// </summary>
        public ICssValue ShiftZ
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
            var x = _x as Length? ?? Length.Zero;
            var y = _y as Length? ?? Length.Zero;
            var z = _z as Length? ?? Length.Zero;
            var dx = x.ToPixel();
            var dy = y.ToPixel();
            var dz = z.ToPixel();
            return new TransformMatrix(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, dx, dy, dz, 0.0, 0.0, 0.0);
        }

        #endregion
    }
}
