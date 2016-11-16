namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    static class MultipleValueExtensions
    {
        public static String Join(this ICssValue[] values, String separator)
        {
            var buffer = StringBuilderPool.Obtain();
            var previous = false;

            for (var i = 0; i < values.Length; i++)
            {
                var str = values[i].CssText;

                if (!String.IsNullOrEmpty(str))
                {
                    if (previous)
                    {
                        buffer.Append(separator);
                    }

                    buffer.Append(values[i].CssText);
                    previous = true;
                }
            }

            return buffer.ToPool();
        }
    }
}
