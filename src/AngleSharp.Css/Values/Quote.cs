namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    public struct Quote
    {
        private readonly String _open;
        private readonly String _close;

        public Quote(String open, String close)
        {
            _open = open;
            _close = close;
        }

        public String Open
        {
            get { return _open; }
        }

        public String Close
        {
            get { return _close; }
        }

        public override String ToString()
        {
            return String.Concat(_open.CssString(), " ", _close.CssString());
        }
    }
}
