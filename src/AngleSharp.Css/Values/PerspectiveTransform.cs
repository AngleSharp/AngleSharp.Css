namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the distance transformation.
    /// </summary>
    class PerspectiveTransform : ITransform, ICssFunctionValue
    {
        #region Fields

        private readonly ICssValue _distance;

        #endregion

        #region ctor

        internal PerspectiveTransform(ICssValue distance)
        {
            _distance = distance;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get { return FunctionNames.Perspective; }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get { return new [] { _distance }; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return Name.CssFunction(_distance.CssText); }
        }

        /// <summary>
        /// Gets the distance from the origin.
        /// </summary>
        public ICssValue Distance
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
            var distance = _distance as Length? ?? Length.Zero;
            return new TransformMatrix(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 / distance.ToPixel());
        }

        #endregion
    }
}
