namespace AngleSharp.Css.Parser
{
    using System;

    static class CssParseErrorExtensions
    {
        public static Int32 GetCode(this CssParseError code)
        {
            return (Int32)code;
        }
    }
}
