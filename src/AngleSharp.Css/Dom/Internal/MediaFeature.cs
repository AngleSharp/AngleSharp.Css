#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
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
        private Tuple<IValueConverter, ICssValue> _conversion;
        private MediaFeature[] _bounds;

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

        /// <summary>
        /// Converts the value of the feature with the given converter. Media queries are
        /// evaluated again for every style computation, so the result of the last converter
        /// is kept instead of serializing and parsing the value over and over.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        /// <returns>The converted value, if valid.</returns>
        internal ICssValue ConvertValue(IValueConverter converter)
        {
            var conversion = _conversion;

            if (conversion is null || !ReferenceEquals(conversion.Item1, converter))
            {
                conversion = Tuple.Create(converter, converter.Convert(Value));
                _conversion = conversion;
            }

            return conversion.Item2;
        }

        /// <summary>
        /// Gets the feature a comparison of the range syntax is evaluated as, i.e. the
        /// min-, max-, or plain form of the feature with the value of the comparison.
        /// </summary>
        /// <param name="index">The index of the comparison.</param>
        /// <param name="prefix">The prefix: min-, max-, or none.</param>
        /// <returns>The feature to validate.</returns>
        internal MediaFeature GetBound(Int32 index, String prefix)
        {
            var kind = prefix.Length == 0 ? 0 : prefix[1] == 'i' ? 1 : 2;
            var bounds = _bounds ??= new MediaFeature[_comparisons.Count * 3];
            var slot = index * 3 + kind;
            return bounds[slot] ??= new MediaFeature(prefix + Name, _comparisons[index].Value);
        }

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
