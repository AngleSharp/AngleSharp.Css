namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the distance transformation.
    /// </summary>
    public sealed class PerspectiveTransform : ITransform
    {
        #region Fields

        private readonly Length _distance;

        #endregion

        #region ctor

        internal PerspectiveTransform(Length distance)
        {
            _distance = distance;
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
                var fn = FunctionNames.Perspective;
                return fn.CssFunction(_distance.CssText);
            }
        }

        /// <summary>
        /// Gets the distance from the origin.
        /// </summary>
        public Length Distance
        {
            get { return _distance; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 / _distance.ToPixel());
        }

        #endregion
    }
}
