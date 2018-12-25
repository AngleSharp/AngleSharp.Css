namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public sealed class Point3 : ICssValue
    {
        private readonly Length _x;
        private readonly Length _y;
        private readonly Length _z;

        public Point3(Length x, Length y, Length z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
        {
            var pt = new Point(_x, _y).ToString();
            return _z != Length.Zero ? String.Concat(pt, " ", _z.ToString()) : pt;
        }
    }
}
