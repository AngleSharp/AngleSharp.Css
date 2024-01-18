namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS grid definition.
    /// </summary>
    sealed class CssGridValue : ICssCompositeValue, IEquatable<CssGridValue>
    {
        #region Fields

        private readonly ICssValue _rows;
        private readonly ICssValue _columns;
        private readonly ICssValue[] _sizes;
        private readonly Boolean _dense;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS grid.
        /// </summary>
        public CssGridValue(ICssValue rows, ICssValue columns, IEnumerable<ICssValue> sizes, Boolean dense)
        {
            _rows = rows;
            _columns = columns;
            _sizes = sizes.ToArray();
            _dense = dense;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned grid rows.
        /// </summary>
        public ICssValue Rows => _rows;

        /// <summary>
        /// Gets the assigned grid columns.
        /// </summary>
        public ICssValue Columns => _columns;

        /// <summary>
        /// Gets the assigned grid sizes.
        /// </summary>
        public ICssValue[] Sizes => _sizes;

        /// <summary>
        /// Gets if the grid is dense, otherwise loose.
        /// </summary>
        public Boolean IsDense => _dense;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var rows = _rows?.CssText;
                var cols = _columns?.CssText;
                var flow = CssKeywords.AutoFlow;
                var auto = _sizes.Join(" ");
                var head = _dense ? String.Concat(flow, " ", CssKeywords.Dense) : flow;

                if (!String.IsNullOrEmpty(auto))
                {
                    head = String.Concat(head, " ", auto);
                }

                if (String.IsNullOrEmpty(rows))
                {
                    rows = head;
                }
                else
                {
                    cols = head;
                }

                return String.Concat(rows, " / ", cols);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssGridValue other)
        {
            if (_rows.Equals(other._rows) && _columns.Equals(other._columns) && _dense == other._dense)
            {
                var l = _sizes.Length;

                if (l == other._sizes.Length)
                {
                    for (var i = 0; i < l; i++)
                    {
                        var a = _sizes[i];
                        var b = other._sizes[i];

                        if (!a.Equals(b))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var rows = _rows.Compute(context);
            var columns = _columns.Compute(context);
            var sizes = _sizes.Select(s => s.Compute(context));
            return new CssGridValue(rows, columns, sizes, _dense);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssGridValue value && Equals(value);

        #endregion
    }
}
