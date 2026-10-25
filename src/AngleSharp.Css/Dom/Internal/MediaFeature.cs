#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
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
        private readonly IReadOnlyList<RangeComparison> _comparisons;
        private readonly String _rangeText;

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

        /// <summary>
        /// Creates a feature in the range syntax of Media Queries 4, e.g.,
        /// (width >= 600px) or (400px &lt; width &lt;= 700px).
        /// </summary>
        /// <param name="name">The name of the feature, e.g., width.</param>
        /// <param name="comparisons">The comparisons, each reading "name op value".</param>
        /// <param name="rangeText">The range as written, used for serialization.</param>
        internal MediaFeature(String name, IReadOnlyList<RangeComparison> comparisons, String rangeText)
        {
            _name = new CssAnyValue(name);
            _value = comparisons[0].Value;
            _comparisons = comparisons;
            _rangeText = rangeText;
        }

        #endregion

        #region Properties

        public String Name => _name?.CssText ?? String.Empty;

        public Boolean IsMinimum => _min;

        public Boolean IsMaximum => _max;

        public String Value => _value?.CssText ?? String.Empty;

        public Boolean HasValue => _value != null;

        /// <summary>
        /// Gets the comparisons of a feature in the range syntax, otherwise null.
        /// </summary>
        internal IReadOnlyList<RangeComparison> Comparisons => _comparisons;

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(Symbols.RoundBracketOpen);

            if (_rangeText is not null)
            {
                writer.Write(_rangeText);
                writer.Write(Symbols.RoundBracketClose);
                return;
            }

            writer.Write(Name);

            if (_value is not null)
            {
                writer.Write(": ");
                writer.Write(Value);
            }

            writer.Write(Symbols.RoundBracketClose);
        }

        #endregion

        #region Range

        /// <summary>
        /// A comparison of the feature against a value, reading "feature op value".
        /// </summary>
        internal readonly struct RangeComparison
        {
            public RangeComparison(String op, ICssValue value)
            {
                Operator = op;
                Value = value;
            }

            /// <summary>
            /// Gets the operator: &lt;, &lt;=, &gt;, &gt;=, or =.
            /// </summary>
            public String Operator { get; }

            public ICssValue Value { get; }
        }

        #endregion
    }
}
