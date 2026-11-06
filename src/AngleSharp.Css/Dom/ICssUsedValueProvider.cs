#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Implemented by what lays out a document, as script expects some properties of a computed
    /// style to be what was found by that (CSSOM 9), e.g. width in pixels instead of auto.
    /// </summary>
    public interface ICssUsedValueProvider
    {
        /// <summary>
        /// Gets the used value of a property of an element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="propertyName">The name of the property, e.g. width.</param>
        /// <returns>The value, or null if the computed value is the right one.</returns>
        String? GetUsedValue(IElement element, String propertyName);
    }

    /// <summary>
    /// Connects a browsing context with what lays out its documents, which comes into being
    /// together with its first layout and hence is no service of the configuration.
    /// </summary>
    public static class CssUsedValueExtensions
    {
        private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<IBrowsingContext, ICssUsedValueProvider> Providers = new System.Runtime.CompilerServices.ConditionalWeakTable<IBrowsingContext, ICssUsedValueProvider>();

        /// <summary>
        /// Sets what gives the used values of the documents of the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        public static void SetUsedValueProvider(this IBrowsingContext context, ICssUsedValueProvider provider)
        {
            Providers.Remove(context);
            Providers.Add(context, provider);
        }

        internal static ICssUsedValueProvider? GetUsedValueProvider(this IBrowsingContext context) =>
            Providers.TryGetValue(context, out var provider) ? provider : null;
    }
}
