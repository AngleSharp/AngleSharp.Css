namespace AngleSharp.Css
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents the an CSS3 markup formatter with inserted intends.
    /// </summary>
    public class PrettyStyleFormatter : IStyleFormatter
    {
        #region Fields

        private String _intendString;
        private String _newLineString;
        private Int32 _intends;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the pretty style formatter.
        /// </summary>
        public PrettyStyleFormatter()
        {
            _intendString = "\t";
            _newLineString = "\n";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the indentation string.
        /// </summary>
        public String Indentation
        {
            get { return _intendString; }
            set { _intendString = value; }
        }

        /// <summary>
        /// Gets or sets the newline string.
        /// </summary>
        public String NewLine
        {
            get { return _newLineString; }
            set { _newLineString = value; }
        }

        #endregion

        #region Methods

        String IStyleFormatter.Sheet(IEnumerable<IStyleFormattable> rules)
        {
            var sb = StringBuilderPool.Obtain();
            var sep = _newLineString + _newLineString;

            using (var writer = new StringWriter(sb))
            {
                foreach (var rule in rules)
                {
                    rule.ToCss(writer, this);
                    writer.Write(sep);
                }
            }

            var pos = sb.Length - sep.Length;

            if (pos > 0)
            {
                sb.Remove(pos, sep.Length);
            }

            return sb.ToPool();
        }

        String IStyleFormatter.BlockRules(IEnumerable<IStyleFormattable> rules)
        {
            var sb = StringBuilderPool.Obtain();
            var ind = String.Concat(Enumerable.Repeat(_intendString, _intends));
            var sep = _newLineString + ind;
            sb.Append('{').Append(' ');
            ++_intends;

            using (var writer = new StringWriter(sb))
            {
                foreach (var rule in rules)
                {
                    writer.Write(sep);
                    writer.Write(_intendString);
                    rule.ToCss(writer, this);
                    writer.Write(sep);
                }
            }

            --_intends;
            return sb.Append('}').ToPool();
        }

        String IStyleFormatter.Declaration(String name, String value, Boolean important)
        {
            return CssStyleFormatter.Instance.Declaration(name, value, important);
        }

        String IStyleFormatter.BlockDeclarations(IEnumerable<IStyleFormattable> declarations)
        {
            var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);
            var ind = String.Concat(Enumerable.Repeat(_intendString, _intends));
            var sep = _newLineString + ind;

            foreach (var declaration in declarations)
            {
                sb.Append(sep).Append(_intendString);
                sb.Append(declaration).Append(Symbols.Semicolon);
            }

            return sb.Append(sep).Append(Symbols.CurlyBracketClose).ToPool();
        }

        String IStyleFormatter.Rule(String name, String value)
        {
            return CssStyleFormatter.Instance.Rule(name, value);
        }

        String IStyleFormatter.Rule(String name, String prelude, String rules)
        {
            return CssStyleFormatter.Instance.Rule(name, prelude, rules);
        }

        String IStyleFormatter.Comment(String data)
        {
            return CssStyleFormatter.Instance.Comment(data);
        }

        #endregion
    }
}
