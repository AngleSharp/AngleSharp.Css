namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    public sealed class Cursor : ICssValue
    {
        private readonly ICssValue[] _definitions;
        private readonly ICssValue _cursor;

        public Cursor(ICssValue[] definitions, ICssValue cursor)
        {
            _definitions = definitions;
            _cursor = cursor;
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
        {
            var sb = StringBuilderPool.Obtain();

            foreach (var definition in _definitions)
            {
                sb.Append(definition).Append(", ");
            }

            sb.Append(_cursor.CssText);
            return sb.ToPool();
        }
    }
}
