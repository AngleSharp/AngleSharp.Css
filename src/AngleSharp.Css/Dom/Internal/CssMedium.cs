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

        #endregion

        #region ctor

        public CssMedium()
        {
            _features = new List<IMediaFeature>();
        }

        #endregion

        #region Properties

        public IEnumerable<IMediaFeature> Features
        {
            get { return _features; }
        }

        public String Type
        {
            get;
            set;
        }

        public Boolean IsExclusive
        {
            get;
            set;
        }

        public Boolean IsInverse
        {
            get;
            set;
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

        public void Add(IMediaFeature feature)
        {
            _features.Add(feature);
        }

        public void Remove(IMediaFeature feature)
        {
            _features.Remove(feature);
        }

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
