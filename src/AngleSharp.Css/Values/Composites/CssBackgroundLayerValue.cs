namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssBackgroundLayerValue : ICssCompositeValue
    {
        private readonly ICssImageValue _image;
        private readonly Point? _position;
        private readonly CssBackgroundSizeValue _size;
        private readonly CssImageRepeatsValue _repeat;
        private readonly ICssValue _attachment;
        private readonly ICssValue _origin;
        private readonly ICssValue _clip;

        /// <summary>
        /// Creates a new background image layer.
        /// </summary>
        public CssBackgroundLayerValue(ICssImageValue image, Point? position, CssBackgroundSizeValue size, CssImageRepeatsValue repeat, ICssValue attachment, ICssValue origin, ICssValue clip)
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
        public ICssImageValue Image => _image;

        /// <summary>
        /// Gets the position of the background image.
        /// </summary>
        public Point? Position => _position;

        /// <summary>
        /// Gets the size of the background image.
        /// </summary>
        public CssBackgroundSizeValue Size => _size;

        /// <summary>
        /// Gets the repeat mode of the background image.
        /// </summary>
        public CssImageRepeatsValue Repeat => _repeat;

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

                if (Image != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Image.CssText);
                }

                if (Position != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Position.Value.CssText);

                    if (Size != null)
                    {
                        sb.Append(" / ");
                        sb.Append(Size.CssText);
                    }
                }

                if (Repeat != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Repeat.CssText);
                }

                if (Attachment != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Attachment.CssText);
                }

                if (Origin != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Origin.CssText);
                }

                if (Clip != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Clip.CssText);
                }

                return sb.ToPool();
            }
        }
    }
}
