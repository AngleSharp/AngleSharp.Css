namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    /// <summary>
    /// A collection of useful information regarding a CSS declaration.
    /// </summary>
    /// <remarks>
    /// Constructs a new declaration info.
    /// </remarks>
    /// <param name="name">The name of the declaration.</param>
    /// <param name="converter">The value converter.</param>
    /// <param name="flags">The property flags.</param>
    /// <param name="initialValue">The initial value, if any.</param>
    /// <param name="shorthands">The names of the associated shorthand declarations, if any.</param>
    /// <param name="longhands">The names of the associated longhand declarations, if any.</param>
    public class DeclarationInfo(String name, IValueConverter converter, PropertyFlags flags = PropertyFlags.None, ICssValue? initialValue = null, String[]? shorthands = null, String[]? longhands = null)
    {

        /// <summary>
        /// Gets the declaration name.
        /// </summary>
        public String Name { get; } = name;

        /// <summary>
        /// Gets the initial value of the declaration, if any.
        /// </summary>
        public ICssValue? InitialValue { get; } = initialValue;

        /// <summary>
        /// Gets the associated value converter.
        /// </summary>
        public IValueConverter Converter { get; } = initialValue != null ? Or(converter, AssignInitial(initialValue)) : converter;

        /// <summary>
        /// Gets the value aggregator, if any.
        /// </summary>
        public IValueAggregator? Aggregator { get; } = converter as IValueAggregator;

        /// <summary>
        /// Gets the flags of the declaration.
        /// </summary>
        public PropertyFlags Flags { get; } = flags;

        /// <summary>
        /// Gets the names of related shorthand declarations.
        /// </summary>
        public String[] Shorthands { get; } = shorthands ?? [];

        /// <summary>
        /// Gets the names of related required longhand declarations.
        /// </summary>
        public String[] Longhands { get; } = longhands ?? [];
    }
}
