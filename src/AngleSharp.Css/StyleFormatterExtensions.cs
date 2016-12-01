namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;

    static class StyleFormatterExtensions
    {
        public static String Style(this IStyleFormatter formatter, String selectorText, IEnumerable<IStyleFormattable> style)
        {
            var block = formatter.BlockDeclarations(style);
            return formatter.Rule(selectorText, null, block);
        }
    }
}
