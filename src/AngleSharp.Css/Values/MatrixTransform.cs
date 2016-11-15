namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents the matrix3d transformation.
    /// </summary>
    public sealed class MatrixTransform : ITransform
    {
        #region Fields

        private readonly Single[] _values;

        #endregion

        #region ctor

        internal MatrixTransform(Single[] values)
        {
            _values = values;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the given index.
        /// </summary>
        /// <param name="index">The index to look for.</param>
        /// <returns>The value.</returns>
        public Single this[Int32 index]
        {
            get { return _values[index]; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the stored matrix.
        /// </summary>
        /// <returns>The current transformation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(_values);
        }

        #endregion
    }
}
