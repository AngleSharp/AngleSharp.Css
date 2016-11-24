namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;

    static class StyleFormatterExtensions
    {
        public static String Style(this IStyleFormatter formatter, String selectorText, IEnumerable<IStyleFormattable> style)
        {
            var value = formatter.Block(style);
            return formatter.Rule(selectorText, null, value);
        }
    }
}
