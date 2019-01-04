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
    class Grid : ICssValue
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
        public Grid(ICssValue rows, ICssValue columns, IEnumerable<ICssValue> sizes, Boolean dense)
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
    }
}
