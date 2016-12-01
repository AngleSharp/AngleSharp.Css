namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public struct ImageRepeats : IFormattable
    {
        private readonly BackgroundRepeat _horizontal;
        private readonly BackgroundRepeat _vertical;

        public ImageRepeats(BackgroundRepeat horizontal, BackgroundRepeat vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        public override String ToString()
        {
            if (_horizontal == BackgroundRepeat.Repeat && _vertical == BackgroundRepeat.NoRepeat)
            {
                return CssKeywords.RepeatX;
            }
            else if (_vertical == BackgroundRepeat.Repeat && _horizontal == BackgroundRepeat.NoRepeat)
            {
                return CssKeywords.RepeatY;
            }
            else if (_horizontal == _vertical)
            {
                return _horizontal.ToString(Map.BackgroundRepeats);
            }

            return String.Concat(_horizontal.ToString(Map.BackgroundRepeats), " ", _vertical.ToString(Map.BackgroundRepeats));
        }

        public String ToString(String format, IFormatProvider formatProvider)
        {
            return ToString();
        }
    }
}
