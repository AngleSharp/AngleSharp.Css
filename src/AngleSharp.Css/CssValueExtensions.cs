namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    static class CssValueExtensions
    {
        public static Single AsNumber(this ICssValue value)
        {
            return 0f;
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

        public static String ToString<T>(this T value, IDictionary<String, T> map)
        {
            foreach (var entry in map)
            {
                if (entry.Value.Equals(value))
                {
                    return entry.Key;
                }
            }

            return value.ToString();
        }
    }
}
