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

        /// <summary>
        /// Creates a new skew transform.
        /// </summary>
        /// <param name="alpha">The alpha skewing angle.</param>
        /// <param name="beta">The beta skewing angle.</param>
        public SkewTransform(Angle alpha, Angle beta)
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
            get
            {
                var fn = FunctionNames.Skew;
                var args = _alpha.CssText;

                if (_alpha != _beta)
                {
                    if (_alpha == Angle.Zero)
                    {
                        fn = FunctionNames.SkewY;
                        args = _beta.CssText;
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
                            _beta.CssText
                        });
                    }
                }

                return fn.CssFunction(args);
            }
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
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var a = Math.Tan(_alpha.ToRadian());
            var b = Math.Tan(_beta.ToRadian());
            return new TransformMatrix(1.0, a, 0.0, b, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
        }

        #endregion
    }
}
