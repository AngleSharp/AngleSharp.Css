namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Text;
    using System;

    [DomName("CSS")]
    public static class CssHelpers
    {
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
    }
}
