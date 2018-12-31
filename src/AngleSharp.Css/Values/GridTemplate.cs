namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS grid template definition.
    /// </summary>
    public sealed class GridTemplate : ICssValue
    {
        private readonly ICssValue _rows;
        private readonly ICssValue _columns;
        private readonly ICssValue _areas;

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
        }

        /// <summary>
        /// Gets the value for the template rows.
        /// </summary>
        public ICssValue TemplateRows
        {
            get { return _rows; }
        }

        /// <summary>
        /// Gets the value for the template columns.
        /// </summary>
        public ICssValue TemplateColumns
        {
            get { return _columns; }
        }

        /// <summary>
        /// Gets the value for the template areas.
        /// </summary>
        public ICssValue TemplateAreas
        {
            get { return _areas; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var rows = _rows.CssText;
                var cols = _columns.CssText;

                if (String.IsNullOrEmpty(cols))
                {
                    return rows;
                }

                return String.Concat(rows, " / ", cols);
            }
        }
    }
}
