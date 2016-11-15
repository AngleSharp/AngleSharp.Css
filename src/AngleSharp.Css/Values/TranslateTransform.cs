namespace AngleSharp.Css.Values
{
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
            return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz, 0f, 0f, 0f);
        }

        #endregion
    }
}
