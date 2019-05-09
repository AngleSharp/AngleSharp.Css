namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    sealed class CssCustomCursorValue : ICssCompositeValue
    {
        private readonly ICssImageValue _source;
        private readonly Point? _position;

        /// <summary>
        /// Creates a new custom cursor value.
        /// </summary>
        /// <param name="source">The image source to display.</param>
        /// <param name="position">The position offset, if any.</param>
        public CssCustomCursorValue(ICssImageValue source, Point? position)
        {
            _source = source;
            _position = position;
        }

        /// <summary>
        /// Gets the image to display as cursor.
        /// </summary>
        public ICssImageValue Source => _source;

        /// <summary>
        /// Gets the positional offset, if any.
        /// </summary>
        public Point? Position => _position;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                sb.Append(_source.CssText);

                if (_position.HasValue)
                {
                    sb.Append(Symbols.Space);
                    sb.Append(_position.Value.CssText);
                }

                return sb.ToPool();
            }
        }
    }
}
