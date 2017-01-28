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

        internal TranslateTransform(Length x, Length y, Length z)
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
            get { return ToString(); }
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
        /// Serializes to the translate function.
        /// </summary>
        public override String ToString()
        {
            var fn = FunctionNames.Translate3d;
            var args = String.Empty;

            if (_x != Length.Zero && _y == Length.Zero && _z == Length.Zero)
            {
                fn = FunctionNames.TranslateX;
                args = _x.ToString();
            }
            else if (_x == Length.Zero && _y != Length.Zero && _z == Length.Zero)
            {
                fn = FunctionNames.TranslateY;
                args = _y.ToString();
            }
            else if (_x == Length.Zero && _y == Length.Zero && _z != Length.Zero)
            {
                fn = FunctionNames.TranslateZ;
                args = _z.ToString();
            }
            else if (_z == Length.Zero)
            {
                fn = FunctionNames.Translate;
                args = String.Join(", ", new[]
                {
                    _x.ToString(),
                    _y.ToString()
                });
            }
            else
            {
                args = String.Join(", ", new[]
                {
                    _x.ToString(),
                    _y.ToString(),
                    _z.ToString()
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
            var dx = _x.ToPixel();
            var dy = _y.ToPixel();
            var dz = _z.ToPixel();
            return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz, 0f, 0f, 0f);
        }

        #endregion
    }
}
