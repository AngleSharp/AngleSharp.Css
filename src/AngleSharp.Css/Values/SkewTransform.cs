namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the skew transformation.
    /// </summary>
    public sealed class SkewTransform : ITransform
    {
        #region Fields

        private readonly Angle _alpha;
        private readonly Angle _beta;

        #endregion

        #region ctor

        internal SkewTransform(Angle alpha, Angle beta)
        {
            _alpha = alpha;
            _beta = beta;
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
        /// Gets the value of the first angle.
        /// </summary>
        public Angle Alpha
        {
            get { return _alpha; }
        }

        /// <summary>
        /// Gets the value of the second angle.
        /// </summary>
        public Angle Beta
        {
            get { return _beta; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes to the skew function.
        /// </summary>
        public override String ToString()
        {
            var fn = FunctionNames.Skew;
            var args = _alpha.ToString();

            if (_alpha != _beta)
            {
                if (_alpha == Angle.Zero)
                {
                    fn = FunctionNames.SkewY;
                    args = _beta.ToString();
                }
                else if (_beta == Angle.Zero)
                {
                    fn = FunctionNames.SkewX;
                }
                else
                {
                    args = String.Join(", ", new[]
                    {
                        args,
                        _beta.ToString()
                    });
                }
            }

            return fn.CssFunction(args);
        }

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var a = (Single)Math.Tan(_alpha.ToRadian());
            var b = (Single)Math.Tan(_beta.ToRadian());
            return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion
    }
}
