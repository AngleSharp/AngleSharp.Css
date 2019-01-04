namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS grid template definition.
    /// </summary>
    class GridTemplate : ICssValue
    {
        #region Fields

        private readonly ICssValue _rows;
        private readonly ICssValue _columns;
        private readonly ICssValue _areas;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS grid template definition.
        /// </summary>
        /// <param name="rows">The rows value to use.</param>
        /// <param name="columns">The columns value to use.</param>
        /// <param name="areas">The areas value to use.</param>
        public GridTemplate(ICssValue rows, ICssValue columns, ICssValue areas)
        {
            _rows = rows;
            _columns = columns;
            _areas = areas;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the template rows.
        /// </summary>
        public ICssValue TemplateRows => _rows;

        /// <summary>
        /// Gets the value for the template columns.
        /// </summary>
        public ICssValue TemplateColumns => _columns;

        /// <summary>
        /// Gets the value for the template areas.
        /// </summary>
        public ICssValue TemplateAreas => _areas;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var rows = String.Empty;
                var cols = _columns?.CssText;

                if (_areas != null)
                {
                    var areas = ((CssTupleValue)_areas).Items;
                    var rowItems = ((CssTupleValue)_rows).Items;
                    var newRows = new List<ICssValue>();

                    for (var i = 0; i < rowItems.Length; i++)
                    {
                        var area = areas[i];
                        var item = rowItems[i] as CssTupleValue;

                        if (item != null && area != null)
                        {
                            var newItems = new List<ICssValue>(item.Items);
                            newItems.Insert(1, area);
                            newRows.Add(new CssTupleValue(newItems.ToArray()));
                        }
                    }

                    rows = new CssTupleValue(newRows.ToArray()).CssText;
                }
                else if (_rows != null)
                {
                    rows = _rows.CssText;
                }

                if (!String.IsNullOrEmpty(cols))
                {
                    return String.Concat(rows, " / ",cols);
                }

                return rows;
            }

            #endregion
        }
    }
}
