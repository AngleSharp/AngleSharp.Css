#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
#if NET5_0_OR_GREATER
    using System.Diagnostics.CodeAnalysis;
#endif

    /// <summary>
    /// A set of helpers for dealing with metric values.
    /// </summary>
    static class CssMetricValueExtensions
    {
        /// <summary>
        /// Creates a new metric value of the same type as the given template, but
        /// carrying the provided value.
        /// </summary>
        /// <param name="template">The value determining the type to create.</param>
        /// <param name="value">The value to use for the created instance.</param>
        /// <returns>The newly created metric value.</returns>
#if NET5_0_OR_GREATER
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssAngleValue))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssFrequencyValue))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssIntegerValue))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssLengthValue))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssNumberValue))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssPercentageValue))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssResolutionValue))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(CssTimeValue))]
        [UnconditionalSuppressMessage("Trimming", "IL2072",
            Justification = "The constructors of the metric values shipped with AngleSharp.Css are preserved via " +
                "DynamicDependency. Metric values implemented outside of AngleSharp.Css have to preserve their " +
                "public constructor taking a single Double themselves, e.g., via DynamicDependency.")]
#endif
        public static ICssValue WithValue(this ICssMetricValue template, Double value) =>
            (ICssValue)Activator.CreateInstance(template.GetType(), value);
    }
}
