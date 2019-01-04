namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the scale3d transformation.
    /// </summary>
    class ScaleTransform : ITransform, ICssFunctionValue
    {
        #region Fields

        private readonly Double _sx;
        private readonly Double _sy;
        private readonly Double _sz;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new scale transform.
        /// </summary>
        /// <param name="sx">The x scaling factor.</param>
        /// <param name="sy">The y scaling factor.</param>
        /// <param name="sz">The z scaling factor.</param>
        public ScaleTransform(Double sx, Double sy, Double sz)
        {
            _sx = sx;
            _sy = sy;
            _sz = sz;
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
                if (_sz == 1f)
                {
                    if (_sx != _sy)
                    {
                        if (_sx == 1f)
                        {
                            return FunctionNames.ScaleY;
                        }
                        else if (_sy == 1f)
                        {
                            return FunctionNames.ScaleX;
                        }
                    }

                    return FunctionNames.Scale;
                }

                return FunctionNames.Scale3d;
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                return new ICssValue[]
                {
                    new Length(_sx, Length.Unit.None),
                    new Length(_sy, Length.Unit.None),
                    new Length(_sz, Length.Unit.None),
                };
            }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var args = String.Empty;

                if (_sz == 1.0)
                {
                    if (_sx == _sy || _sy == 1.0)
                    {
                        args = _sx.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (_sx == 1.0)
                    {
                        args = _sy.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        args = String.Concat(
                            _sx.ToString(CultureInfo.InvariantCulture), ", ",
                            _sy.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else if (_sx != _sy || _sx != _sz)
                {
                    args = String.Concat(
                        _sx.ToString(CultureInfo.InvariantCulture), ", ",
                        _sy.ToString(CultureInfo.InvariantCulture), ", ",
                        _sz.ToString(CultureInfo.InvariantCulture));
                }

                return Name.CssFunction(args);
            }
        }

        /// <summary>
        /// Gets the scaling in x-direction.
        /// </summary>
        public Double ScaleX
        {
            get { return _sx; }
        }

        /// <summary>
        /// Gets the scaling in y-direction.
        /// </summary>
        public Double ScaleY
        {
            get { return _sy; }
        }

        /// <summary>
        /// Gets the scaling in z-direction.
        /// </summary>
        public Double ScaleZ
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
