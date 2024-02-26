namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a feature expression within a media query.
    /// </summary>
    sealed class MediaFeature : IMediaFeature
    {
        #region Fields

        private readonly Boolean _min;
        private readonly Boolean _max;
        private readonly ICssValue _name;
        private readonly ICssValue _value;
        private readonly String _op;

        #endregion

        #region ctor

        internal MediaFeature(String name)
            : this(name, null)
        {}

        internal MediaFeature(String name, ICssValue value)
        {
            _name = new CssAnyValue(name);
            _value = value;
            _min = name.StartsWith("min-");
            _max = name.StartsWith("max-");
        }

        internal MediaFeature(String name, ICssValue value, String op)
            : this(new CssAnyValue(name), value, op)
        {}

        internal MediaFeature(ICssValue name, ICssValue value, String op)
        {
            _name = name;
            _value = value;
            _op = op;
        }

        #endregion

        #region Properties

        public String Name => _name?.CssText ?? String.Empty;

        public Boolean IsMinimum => _min;

        public Boolean IsMaximum => _max;

        public String Value => _value?.CssText ?? String.Empty;

        public Boolean HasValue => _value != null;

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(Symbols.RoundBracketOpen);
            writer.Write(Name);

            if (_op is not null)
            {
                writer.Write(" ");
                writer.Write(_op);
                writer.Write(" ");
                writer.Write(Value);
            }
            else if (_value is not null)
            {
                writer.Write(": ");
                writer.Write(Value);
            }

            writer.Write(Symbols.RoundBracketClose);
        }

        #endregion
    }
}
