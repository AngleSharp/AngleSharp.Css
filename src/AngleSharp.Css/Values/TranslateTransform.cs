namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the translate3d transformation.
    /// </summary>
    class TranslateTransform : ITransform, ICssFunctionValue
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
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get
            {
                if (_x != null && _y == null && _z == null)
                {
                    return FunctionNames.TranslateX;
                }
                else if (_x == null && _y != null && _z == null)
                {
                    return FunctionNames.TranslateY;
                }
                else if (_x == null && _y == null && _z != null)
                {
                    return FunctionNames.TranslateZ;
                }
                else if (_z == null)
                {
                    return FunctionNames.Translate;
                }

                return FunctionNames.Translate3d;
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                return new[]
                {
                    _x,
                    _y,
                    _z
                };
            }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return Name.CssFunction(Arguments.Join(", ")); }
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
