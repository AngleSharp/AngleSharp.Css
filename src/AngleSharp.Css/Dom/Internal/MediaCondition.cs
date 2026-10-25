namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// A parenthesized media condition inside another one, e.g. the
    /// ((min-height: 600px) or (orientation: landscape)) of
    /// (min-width: 500px) and ((min-height: 600px) or (orientation: landscape)).
    /// It keeps its own connector and negation instead of being flattened into
    /// the enclosing condition.
    /// </summary>
    sealed class MediaCondition : IMediaFeature
    {
        #region ctor

        public MediaCondition(CssMedium medium)
        {
            Medium = medium;
        }

        #endregion

        #region Properties

        public CssMedium Medium { get; }

        public String Name => String.Empty;

        public Boolean IsMinimum => false;

        public Boolean IsMaximum => false;

        public String Value => String.Empty;

        public Boolean HasValue => false;

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(Symbols.RoundBracketOpen);
            Medium.ToCss(writer, formatter);
            writer.Write(Symbols.RoundBracketClose);
        }

        #endregion
    }
}
