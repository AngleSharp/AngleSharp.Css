namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a medium rule. More information available at:
    /// http://www.w3.org/TR/css3-mediaqueries/
    /// </summary>
    public sealed class CssMedium : ICssMedium
    {
        #region Fields

        private readonly List<IMediaFeature> _features;
        private readonly String _type;
        private readonly Boolean _exclusive;
        private readonly Boolean _inverse;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS medium.
        /// </summary>
        /// <param name="type">The type of the media rule.</param>
        /// <param name="inverse">Specifies if it should be inverted.</param>
        /// <param name="exclusive">Specifies if the rule is exclusive.</param>
        public CssMedium(String type, Boolean inverse, Boolean exclusive)
            : this(type, inverse, exclusive, Enumerable.Empty<IMediaFeature>())
        {
        }

        /// <summary>
        /// Creates a new CSS medium.
        /// </summary>
        /// <param name="type">The type of the media rule.</param>
        /// <param name="inverse">Specifies if it should be inverted.</param>
        /// <param name="exclusive">Specifies if the rule is exclusive.</param>
        /// <param name="features">The features of the medium.</param>
        public CssMedium(String type, Boolean inverse, Boolean exclusive, IEnumerable<IMediaFeature> features)
        {
            _features = new List<IMediaFeature>(features);
            _type = type;
            _inverse = inverse;
            _exclusive = exclusive;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the feature demands (constraints) of the medium.
        /// </summary>
        public IEnumerable<IMediaFeature> Features => _features;

        /// <summary>
        /// Gets the type of the medium.
        /// </summary>
        public String Type => _type;

        /// <summary>
        /// Gets if the medium is exclusive to other media.
        /// </summary>
        public Boolean IsExclusive => _exclusive;

        /// <summary>
        /// Gets if the medium should be inverted.
        /// </summary>
        public Boolean IsInverse => _inverse;

        /// <summary>
        /// Gets the constraints - i.e., the stringified features.
        /// </summary>
        public String Constraints => String.Join(" and ", Features.Select(m => m.ToCss()));

        #endregion

        #region Methods

        /// <inheritdoc />
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssMedium;

            if (other != null && 
                other.IsExclusive == IsExclusive && 
                other.IsInverse == IsInverse && 
                other.Type.Is(Type) && 
                other.Features.Count() == Features.Count())
            {
                foreach (var feature in other.Features)
                {
                    var isShared = Features.Any(m => m.Name.Is(feature.Name) && m.Value.Is(feature.Value));

                    if (!isShared)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override Int32 GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Writes the medium as CSS.
        /// </summary>
        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var offset = 0;
            var hasType = !String.IsNullOrEmpty(_type);
            var hasFeature = _features.Count > 0;

            if (_exclusive || _inverse)
            {
                writer.Write(_exclusive ? CssKeywords.Only : CssKeywords.Not);

                if (hasType || hasFeature)
                {
                    writer.Write(Symbols.Space);
                }
            }

            if (hasType)
            {
                writer.Write(_type);
            }
            else if (hasFeature)
            {
                offset = 1;
                _features[0].ToCss(writer, formatter);
            }

            for (var i = offset; i < _features.Count; i++)
            {
                writer.Write(" and ");
                _features[i].ToCss(writer, formatter);
            }
        }

        #endregion
    }
}
