#nullable enable
namespace AngleSharp.Css
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Tracks pseudo-class states forced onto individual elements, keyed by element identity so
    /// that no changes to <see cref="IElement"/> itself are required.
    /// </summary>
    static class PseudoClassStateStore
    {
        private static readonly ConditionalWeakTable<IElement, Dictionary<String, Boolean>> _states = new();

        // WithCss() wraps every non-:focus pseudo-class selector in ForcingPseudoClassSelector, so
        // TryGet runs once per pseudo-class match attempt on every element - a page that never
        // calls SetPseudoClass still paid a ConditionalWeakTable probe for every :hover, :disabled,
        // :checked, ... match. This flag only ever moves from "nothing forced" to "something
        // forced": Remove/Clear cannot prove every forced state everywhere has been undone (there
        // is no per-element or per-process count to check), so it deliberately never goes back to
        // false. Once anything has been forced, the process pays the old per-match cost again.
        private static volatile Boolean _anyForced;

        public static void Set(IElement element, String pseudoClass, Boolean value)
        {
            _anyForced = true;
            _states.GetValue(element, _ => new Dictionary<String, Boolean>(StringComparer.OrdinalIgnoreCase))[pseudoClass] = value;
        }

        public static Boolean TryGet(IElement element, String pseudoClass, out Boolean value)
        {
            if (_anyForced && _states.TryGetValue(element, out var state) && state.TryGetValue(pseudoClass, out value))
            {
                return true;
            }

            value = default;
            return false;
        }

        public static void Remove(IElement element, String pseudoClass)
        {
            if (_states.TryGetValue(element, out var state))
            {
                state.Remove(pseudoClass);
            }
        }

        public static void Clear(IElement element) => _states.Remove(element);
    }
}
