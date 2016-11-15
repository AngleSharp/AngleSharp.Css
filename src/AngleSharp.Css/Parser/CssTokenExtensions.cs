namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Parser.Tokens;
    using System;

    /// <summary>
    /// Extensions to be used exclusively by the tokenizer.
    /// </summary>
    static class CssTokenExtensions
    {
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
