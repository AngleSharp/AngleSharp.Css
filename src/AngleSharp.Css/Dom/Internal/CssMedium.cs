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
    sealed class CssMedium : ICssMedium
    {
        #region Fields

        private readonly List<IMediaFeature> _features;
        private readonly String _type;
        private readonly Boolean _exclusive;
        private readonly Boolean _inverse;

        #endregion

        #region ctor

        public CssMedium(String type, Boolean inverse, Boolean exclusive)
            : this(type, inverse, exclusive, Enumerable.Empty<IMediaFeature>())
        {
        }

        public CssMedium(String type, Boolean inverse, Boolean exclusive, IEnumerable<IMediaFeature> features)
        {
            _features = new List<IMediaFeature>(features);
            _type = type;
            _inverse = inverse;
            _exclusive = exclusive;
        }

        #endregion

        #region Properties

        public IEnumerable<IMediaFeature> Features
        {
            get { return _features; }
        }

        public String Type
        {
            get { return _type; }
        }

        public Boolean IsExclusive
        {
            get { return _exclusive; }
        }

        public Boolean IsInverse
        {
            get { return _inverse; }
        }

        public String Constraints
        {
            get 
            {
                var constraints = Features.Select(m => m.ToCss());
                return String.Join(" and ", constraints);
            }
        }

        #endregion

        #region Methods

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

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Medium(IsExclusive, IsInverse, Type, Features));
        }

        #endregion
    }
}
