namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssBackgroundLayerValue : ICssCompositeValue
    {
        private readonly ICssValue _image;
        private readonly ICssValue _position;
        private readonly ICssValue _size;
        private readonly ICssValue _repeat;
        private readonly ICssValue _attachment;
        private readonly ICssValue _origin;
        private readonly ICssValue _clip;

        /// <summary>
        /// Creates a new background image layer.
        /// </summary>
        public CssBackgroundLayerValue(ICssValue image, ICssValue position, ICssValue size, ICssValue repeat, ICssValue attachment, ICssValue origin, ICssValue clip)
        {
            _image = image;
            _position = position;
            _size = size;
            _repeat = repeat;
            _attachment = attachment;
            _origin = origin;
            _clip = clip;
        }

        /// <summary>
        /// Gets the background image.
        /// </summary>
        public ICssValue Image => _image;

        /// <summary>
        /// Gets the position of the background image.
        /// </summary>
        public ICssValue Position => _position;

        /// <summary>
        /// Gets the size of the background image.
        /// </summary>
        public ICssValue Size => _size;

        /// <summary>
        /// Gets the repeat mode of the background image.
        /// </summary>
        public ICssValue Repeat => _repeat;

        /// <summary>
        /// Gets the mode of the background image.
        /// </summary>
        public ICssValue Attachment => _attachment;

        /// <summary>
        /// Gets the origin of the background image.
        /// </summary>
        public ICssValue Origin => _origin;

        /// <summary>
        /// Gets the clipping mode of the background image.
        /// </summary>
        public ICssValue Clip => _clip;

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
    }
}
