#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a position component given as an offset from an edge, e.g., right 10px.
    /// https://drafts.csswg.org/css-values-4/#position
    /// </summary>
    sealed class CssEdgeOffsetValue : ICssCompositeValue, IEquatable<CssEdgeOffsetValue>
    {
        #region Fields

        private readonly String _edge;
        private readonly ICssValue _offset;

        #endregion

        #region ctor

        public CssEdgeOffsetValue(String edge, ICssValue offset)
        {
            _edge = edge;
            _offset = offset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the edge keyword, i.e., left, right, top, or bottom.
        /// </summary>
        public String Edge => _edge;

        /// <summary>
        /// Gets the offset from the edge.
        /// </summary>
        public ICssValue Offset => _offset;

        public String CssText => String.Concat(_edge, " ", _offset.CssText);

        #endregion

        #region Methods

        public Boolean Equals(CssEdgeOffsetValue other) =>
            other is not null && _edge.Is(other._edge) && _offset.Equals(other._offset);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssEdgeOffsetValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var offset = _offset.Compute(context);
            return offset is null ? null : new CssEdgeOffsetValue(_edge, offset);
        }

        #endregion
    }
}
