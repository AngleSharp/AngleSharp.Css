namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    struct BackgroundLayer : ICssValue
    {
        public ICssValue Source;
        public Point? Position;
        public ICssValue Size;
        public ImageRepeats? Repeat;
        public ICssValue Attachment;
        public ICssValue Origin;
        public ICssValue Clip;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                if (Source != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Source.CssText);
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
                    sb.Append(Repeat.Value.CssText);
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
