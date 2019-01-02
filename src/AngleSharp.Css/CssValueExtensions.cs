namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;

    static class CssValueExtensions
    {
        public static Double AsNumber(this ICssValue value)
        {
            var length = value as Length?;

            if (length.HasValue && length.Value.Type == Length.Unit.None)
            {
                return length.Value.Value;
            }

            return 0.0;
        }

        public static Double AsPixel(this ICssValue value)
        {
            var length = value as Length?;

            if (length.HasValue && length.Value.Type != Length.Unit.None)
            {
                return length.Value.ToPixel();
            }

            return 0.0;
        }

        public static Int32 AsInteger(this ICssValue value)
        {
            return (Int32)value.AsNumber();
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
