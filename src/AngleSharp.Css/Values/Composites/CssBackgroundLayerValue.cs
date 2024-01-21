namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Creates a new background image layer.
    /// </summary>
    sealed class CssBackgroundLayerValue(ICssValue? image, ICssValue? position, ICssValue? size, ICssValue? repeat, ICssValue? attachment, ICssValue? origin, ICssValue? clip) : ICssCompositeValue, IEquatable<CssBackgroundLayerValue>
    {
        #region Fields

        private readonly ICssValue? _image = image;
        private readonly ICssValue? _position = position;
        private readonly ICssValue? _size = size;
        private readonly ICssValue? _repeat = repeat;
        private readonly ICssValue? _attachment = attachment;
        private readonly ICssValue? _origin = origin;
        private readonly ICssValue? _clip = clip;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the background image.
        /// </summary>
        public ICssValue? Image => _image;

        /// <summary>
        /// Gets the position of the background image.
        /// </summary>
        public ICssValue? Position => _position;

        /// <summary>
        /// Gets the size of the background image.
        /// </summary>
        public ICssValue? Size => _size;

        /// <summary>
        /// Gets the repeat mode of the background image.
        /// </summary>
        public ICssValue? Repeat => _repeat;

        /// <summary>
        /// Gets the mode of the background image.
        /// </summary>
        public ICssValue? Attachment => _attachment;

        /// <summary>
        /// Gets the origin of the background image.
        /// </summary>
        public ICssValue? Origin => _origin;

        /// <summary>
        /// Gets the clipping mode of the background image.
        /// </summary>
        public ICssValue? Clip => _clip;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                if (_image != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(_image.CssText);
                }

                if (_position != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(_position.CssText);

                    if (_size != null)
                    {
                        sb.Append(" / ");
                        sb.Append(_size.CssText);
                    }
                }

                if (_repeat != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(_repeat.CssText);
                }

                if (_attachment != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(_attachment.CssText);
                }

                if (_origin != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(_origin.CssText);
                }

                if (_clip != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(_clip.CssText);
                }

                return sb.ToPool();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssBackgroundLayerValue? other)
        {
            return other is not null &&
                _clip.Is(other._clip) &&
                _repeat.Is(other._repeat) &&
                _attachment.Is(other._attachment) &&
                _origin.Is(other._origin) &&
                _size.Is(other._size);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssBackgroundLayerValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var image = _image?.Compute(context);
            var position = _position?.Compute(context);
            var size = _size?.Compute(context);
            var repeat = _repeat?.Compute(context);
            var attachment = _attachment?.Compute(context);
            var origin = _origin?.Compute(context);
            var clip = _clip?.Compute(context);
            return new CssBackgroundLayerValue(image, position, size, repeat, attachment, origin, clip);
        }

        #endregion
    }
}
