namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    public sealed class Counters : ICssValue
    {
        private readonly CounterValue[] _counters;

        public Counters()
        {
            _counters = null;
        }

        public Counters(CounterValue[] counters)
        {
            _counters = counters;
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return _counters != null ? _counters.Join(" ") : CssKeywords.None;
        }
    }
}
