namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    struct CursorDefinition : ICssValue
    {
        public IImageSource Source;
        public Point? Position;

        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
        {
            var sb = StringBuilderPool.Obtain();

            sb.Append(Source.ToString());

            if (Position.HasValue)
            {
                sb.Append(Symbols.Space);
                sb.Append(Position.Value.ToString());
            }

            return sb.ToPool();
        }
    }
}
