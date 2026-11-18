#nullable enable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the paint function, i.e., an image drawn by a paint definition that
    /// has been registered under the given name, e.g., paint(checkerboard, 10px).
    /// The arguments are kept as written, since only the paint definition knows their
    /// syntax: parsing the value never requires the paint definition to be available.
    /// https://www.w3.org/TR/css-paint-api-1/#paint-notation
    /// </summary>
    public sealed class CssPaintValue : ICssImageValue, IEquatable<CssPaintValue>
    {
        #region Fields

        private readonly String _painterName;
        private readonly ICssValue[] _arguments;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new paint function.
        /// </summary>
        /// <param name="painterName">The name of the paint definition.</param>
        /// <param name="arguments">The (unparsed) arguments for the paint definition.</param>
        public CssPaintValue(String painterName, ICssValue[] arguments)
        {
            _painterName = painterName ?? throw new ArgumentNullException(nameof(painterName));
            _arguments = arguments ?? Array.Empty<ICssValue>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function, i.e., paint.
        /// </summary>
        public String Name => FunctionNames.Paint;

        /// <summary>
        /// Gets the name of the paint definition (the ident of the function), which
        /// is looked up in the paint worklet of the document when the image is drawn.
        /// </summary>
        public String PainterName => _painterName;

        /// <summary>
        /// Gets the arguments following the name, each one as written, e.g., the
        /// two arguments 10px and "a, b" of paint(x, 10px, "a, b").
        /// </summary>
        public ICssValue[] Arguments => _arguments;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var parts = new String[_arguments.Length + 1];
                parts[0] = _painterName;

                for (var i = 0; i < _arguments.Length; i++)
                {
                    parts[i + 1] = _arguments[i].CssText;
                }

                return Name.CssFunction(String.Join(", ", parts));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssPaintValue? other) =>
            other is not null &&
            _painterName.Is(other._painterName) &&
            _arguments.Length == other._arguments.Length &&
            _arguments.Select((m, i) => m.CssText.Is(other._arguments[i].CssText)).All(m => m);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssPaintValue value && Equals(value);

        /// <summary>
        /// The computed value is as specified: the arguments are only interpreted by
        /// the paint definition, when the image is drawn.
        /// </summary>
        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
