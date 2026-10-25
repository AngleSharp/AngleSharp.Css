#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Determines if the subject of a selector (i.e., its rightmost compound)
    /// targets a pseudo-element. Only such selectors may apply to a pseudo-element,
    /// otherwise the rules of the host would also be applied to its pseudo-elements.
    /// </summary>
    sealed class PseudoElementSubjectVisitor : ISelectorVisitor
    {
        private static readonly HashSet<string> LegacyPseudoElements = new(System.StringComparer.OrdinalIgnoreCase)
        {
            PseudoElementNames.Before,
            PseudoElementNames.After,
            PseudoElementNames.FirstLine,
            PseudoElementNames.FirstLetter,
        };

        private bool _result;

        private PseudoElementSubjectVisitor()
        {
        }

        /// <summary>
        /// Checks if the name is one of the pseudo-elements of CSS 2, which can also be
        /// written like a pseudo-class with a single colon, e.g. li:after.
        /// </summary>
        public static bool IsLegacyPseudoElement(string name) => LegacyPseudoElements.Contains(name);

        public static bool TargetsPseudoElement(ISelector selector)
        {
            var visitor = new PseudoElementSubjectVisitor();
            selector?.Accept(visitor);
            return visitor._result;
        }

        void ISelectorVisitor.Attribute(string name, string op, string value)
        {
        }

        void ISelectorVisitor.Type(string name)
        {
        }

        void ISelectorVisitor.Id(string value)
        {
        }

        void ISelectorVisitor.Child(string name, int step, int offset, ISelector selector)
        {
        }

        void ISelectorVisitor.Class(string name)
        {
        }

        void ISelectorVisitor.PseudoClass(string name)
        {
            _result = _result || IsLegacyPseudoElement(name);
        }

        void ISelectorVisitor.PseudoElement(string name)
        {
            _result = true;
        }

        void ISelectorVisitor.List(IEnumerable<ISelector> selectors)
        {
            _result = selectors.Any(TargetsPseudoElement);
        }

        void ISelectorVisitor.Combinator(IEnumerable<ISelector> selectors, IEnumerable<string> symbols)
        {
            _result = TargetsPseudoElement(selectors.LastOrDefault());
        }

        void ISelectorVisitor.Many(IEnumerable<ISelector> selectors)
        {
            _result = selectors.Any(TargetsPseudoElement);
        }
    }
}
