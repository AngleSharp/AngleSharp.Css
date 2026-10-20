#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the image-set function, i.e., a set of images for different resolutions.
    /// https://drafts.csswg.org/css-images-4/#image-set-notation
    /// </summary>
    sealed class CssImageSetValue : ICssImageValue, IEquatable<CssImageSetValue>
    {
        #region Fields

        private readonly CssTupleValue[] _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image-set function.
        /// </summary>
        /// <param name="options">The options, each an image with an optional resolution and type.</param>
        public CssImageSetValue(CssTupleValue[] options)
        {
            _options = options;
        }

        #endregion

        #region Properties

        public String Name => FunctionNames.ImageSet;

        public ICssValue[] Arguments => _options;

        public String CssText => Name.CssFunction(Arguments.Join(", "));

        #endregion

        #region Methods

        public Boolean Equals(CssImageSetValue other) =>
            other is not null && _options.Length == other._options.Length && _options.Select((m, i) => m.CssText.Is(other._options[i].CssText)).All(m => m);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssImageSetValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) =>
            new CssImageSetValue(_options.Select(m => (CssTupleValue)((ICssValue)m).Compute(context)).ToArray());

        #endregion

        #region Type

        /// <summary>
        /// Represents the type() of an image-set option, i.e., the MIME type of the image.
        /// </summary>
        internal sealed class TypeValue : ICssValue
        {
            private readonly ICssValue _type;

            public TypeValue(ICssValue type)
            {
                _type = type;
            }

            public String CssText => FunctionNames.Type.CssFunction(_type.CssText);

            public ICssValue Compute(ICssComputeContext context) => this;

            public Boolean Equals(ICssValue other) => other is TypeValue value && value._type.Equals(_type);
        }

        #endregion
    }
}
