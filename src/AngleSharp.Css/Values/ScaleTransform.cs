namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

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
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

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
        /// Serializes to the scale function.
        /// </summary>
        public override String ToString()
        {
            var fn = FunctionNames.Scale3d;
            var args = _sx.ToString(CultureInfo.InvariantCulture);

            if (_sz == 1f)
            {
                fn = FunctionNames.Scale;

                if (_sx != _sy)
                {
                    if (_sx == 1f)
                    {
                        fn = FunctionNames.ScaleY;
                        args = _sy.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (_sy == 1f)
                    {
                        fn = FunctionNames.ScaleX;
                    }
                    else
                    {
                        args = args + ", " + _sy.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            else if (_sx != _sy || _sx != _sz)
            {
                args = String.Join(", ", new[]
                {
                    args,
                    _sy.ToString(CultureInfo.InvariantCulture),
                    _sz.ToString(CultureInfo.InvariantCulture)
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
            return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion
    }
}
