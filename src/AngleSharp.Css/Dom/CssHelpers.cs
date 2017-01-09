namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    [DomName("CSS")]
    public static class CssHelpers
    {
        /// <summary>
        /// Returns a string containing the escaped string passed as parameter,
        /// mostly for use as part of a CSS selector.
        /// </summary>
        /// <param name="str">The string to be escaped.</param>
        /// <returns>The escaped string.</returns>
        [DomName("escape")]
        public static String Escape(String str)
        {
            if (str.Length != 1 || str[0] != Symbols.Minus)
            {
                var sb = StringBuilderPool.Obtain();

                for (var i = 0; i < str.Length; i++)
                {
                    var ch = str[i];

                    if (ch == Symbols.Null)
                    {
                        ch = Symbols.Replacement;
                    }
                    else if (ch == 0x7f ||
                            (ch.IsInRange(0x01, 0x1f)) ||
                            (ch.IsDigit() && (i == 0 || (i == 1 && str[0] == Symbols.Minus))))
                    {
                        sb.Append(Symbols.ReverseSolidus);
                        sb.Append(ch.ToHex());
                        sb.Append(Symbols.Space);
                        continue;
                    }
                    else if (ch < 0x80 && 
                            !ch.IsOneOf(Symbols.Minus,Symbols.Underscore) && 
                            !ch.IsAlphanumericAscii())
                    {
                        sb.Append(Symbols.ReverseSolidus);
                    }

                    sb.Append(ch);
                }

                return sb.ToPool();
            }

            return @"\-";
        }

        /// <summary>
        /// Returns a boolean value indicating if the browser supports a given CSS feature,
        /// or not.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property to check.</param>
        /// <param name="value">The value of the CSS property to check.</param>
        /// <returns>True if the CSS feature is supported, otherwise false.</returns>
        [DomName("supports")]
        public static Boolean Supports(String propertyName, String value)
        {
            var condition = new DeclarationCondition(propertyName, value);
            return condition.Check(null);
        }

        /// <summary>
        /// Returns a boolean value indicating if the browser supports a given CSS feature,
        /// or not.
        /// </summary>
        /// <param name="conditionText">The condition to check.</param>
        /// <returns>True if the CSS feature is supported, otherwise false.</returns>
        [DomName("supports")]
        public static Boolean Supports(String conditionText)
        {
            var condition = ConditionParser.Parse(conditionText);
            return condition.Check(null);
        }
    }
}
