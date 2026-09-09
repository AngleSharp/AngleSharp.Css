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

        public static void Set(IElement element, String pseudoClass, Boolean value) =>
            _states.GetValue(element, _ => new Dictionary<String, Boolean>(StringComparer.OrdinalIgnoreCase))[pseudoClass] = value;

        public static Boolean TryGet(IElement element, String pseudoClass, out Boolean value)
        {
            if (_states.TryGetValue(element, out var state) && state.TryGetValue(pseudoClass, out value))
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
