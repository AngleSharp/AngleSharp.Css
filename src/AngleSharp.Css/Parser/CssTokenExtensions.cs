namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Parser.Tokens;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Extensions to be used exclusively by the tokenizer.
    /// </summary>
    static class CssTokenExtensions
    {
        public static Boolean IsPotentiallyNested(this CssToken token)
        {
            if (token.Is(CssTokenType.Hash, CssTokenType.Colon, CssTokenType.SquareBracketOpen))
            {
                return true;
            }
            else if (token.Type == CssTokenType.Delim && token.Data.Length == 1)
            {
                return token.Data[0] switch
                {
                    Symbols.Asterisk or Symbols.Plus or Symbols.Dollar or Symbols.Dot or Symbols.Tilde or Symbols.GreaterThan or Symbols.Ampersand => true,
                    _ => false,
                };
            }

            return false;
        }

        /// <summary>
        /// Checks if the provided token is either of the first or the second 
        /// type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to match.</param>
        /// <param name="b">The alternative match for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean Is(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type == a || type == b;
        }

        /// <summary>
        /// Checks if the provided token is one of the list of tokens.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The 1st type to match.</param>
        /// <param name="b">The 2nd type to match.</param>
        /// <param name="c">The 3rd type to match.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean Is(this CssToken token, CssTokenType a, CssTokenType b, CssTokenType c)
        {
            var type = token.Type;
            return type == a || type == b || type == c;
        }

        /// <summary>
        /// Checks if the provided token is one of the list of tokens.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The 1st type to match.</param>
        /// <param name="b">The 2nd type to match.</param>
        /// <param name="c">The 3rd type to match.</param>
        /// <param name="d">The 4th type to match.</param>
        /// <param name="e">The 5th type to match.</param>
        /// <param name="f">The 6th type to match.</param>
        /// <param name="g">The 7th type to match.</param>
        /// <param name="h">The 8th type to match.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean Is(this CssToken token, CssTokenType a, CssTokenType b, CssTokenType c, CssTokenType d, CssTokenType e, CssTokenType f, CssTokenType g, CssTokenType h)
        {
            var type = token.Type;
            return type == a || type == b || type == c || type == d || type == e || type == f || type == g || type == h;
        }

        /// <summary>
        /// Checks if the provided token is neither of the first nor the second 
        /// type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to unmatch.</param>
        /// <param name="b">The alternative unmatch for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean IsNot(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type != a && type != b;
        }

        /// <summary>
        /// Checks if the provided token is neither of the first, nor the
        /// second nor the third type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to unmatch.</param>
        /// <param name="b">The alternative unmatch for the token.</param>
        /// <param name="c">The final unmatch for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean IsNot(this CssToken token, CssTokenType a, CssTokenType b, CssTokenType c)
        {
            var type = token.Type;
            return type != a && type != b && type != c;
        }
    }
}
