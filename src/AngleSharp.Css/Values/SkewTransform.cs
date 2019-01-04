namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the skew transformation.
    /// </summary>
    class SkewTransform : ITransform, ICssFunctionValue
    {
        #region Fields

        private readonly ICssValue _alpha;
        private readonly ICssValue _beta;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new skew transform.
        /// </summary>
        /// <param name="alpha">The alpha skewing angle.</param>
        /// <param name="beta">The beta skewing angle.</param>
        public SkewTransform(ICssValue alpha, ICssValue beta)
        {
            _alpha = alpha;
            _beta = beta;
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
                if (_alpha != _beta)
                {
                    if (_alpha == null)
                    {
                        return FunctionNames.SkewY;
                    }
                    else if (_beta == null)
                    {
                        return FunctionNames.SkewX;
                    }
                }

                return FunctionNames.Skew;
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get { return new ICssValue[] { }; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var args = _alpha?.CssText ?? String.Empty;

                if (_alpha != _beta)
                {
                    if (_alpha == null)
                    {
                        args = _beta.CssText;
                    }
                    else if (_beta != null)
                    {
                        args = String.Concat(args, ", ", _beta.CssText);
                    }
                }

                return Name.CssFunction(args);
            }
        }

        /// <summary>
        /// Gets the value of the first angle.
        /// </summary>
        public ICssValue Alpha
        {
            get { return _alpha; }
        }

        /// <summary>
        /// Gets the value of the second angle.
        /// </summary>
        public ICssValue Beta
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
            var a = Math.Tan((_alpha as Angle ? ?? Angle.Zero).ToRadian());
            var b = Math.Tan((_beta as Angle? ?? Angle.Zero).ToRadian());
            return new TransformMatrix(1.0, a, 0.0, b, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
        }

        #endregion
    }
}
