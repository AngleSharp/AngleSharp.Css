namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;

    static class CssValueExtensions
    {
        public static Double AsNumber(this ICssValue value)
        {
            return 0.0;
        }

        public static Int32 AsInteger(this ICssValue value)
        {
            return 0;
        }

        public static Boolean AsBoolean(this ICssValue value)
        {
            return false;
        }

        public static T AsEnum<T>(this ICssValue value)
            where T : struct, IComparable
        {
            return default(T);
        }

        public static Boolean Is(this ICssValue value, String keyword)
        {
            return false;
        }
    }
}
