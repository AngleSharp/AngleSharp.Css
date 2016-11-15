namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents the scale3d transformation.
    /// </summary>
    public sealed class ScaleTransform : ITransform
    {
        #region Fields

        private readonly Single _sx;
        private readonly Single _sy;
        private readonly Single _sz;

        #endregion

        #region ctor

        internal ScaleTransform(Single sx, Single sy, Single sz)
        {
            _sx = sx;
            _sy = sy;
            _sz = sz;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the scaling in x-direction.
        /// </summary>
        public Single ScaleX
        {
            get { return _sx; }
        }

        /// <summary>
        /// Gets the scaling in y-direction.
        /// </summary>
        public Single ScaleY
        {
            get { return _sy; }
        }

        /// <summary>
        /// Gets the scaling in z-direction.
        /// </summary>
        public Single ScaleZ
        {
            get { return _sz; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion
    }
}
