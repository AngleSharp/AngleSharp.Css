namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// A collection of useful information regarding a CSS declaration.
    /// </summary>
    public class DeclarationInfo
    {
        /// <summary>
        /// Constructs a new declaration info.
        /// </summary>
        /// <param name="converter">The value converter.</param>
        /// <param name="aggregator">The value aggregator, if any.</param>
        /// <param name="flags">The property flags.</param>
        /// <param name="parent">The name of the associated shorthand declaration, if any.</param>
        /// <param name="children">The names of the associated longhand declarations, if any.</param>
        public DeclarationInfo(IValueConverter converter, IValueAggregator aggregator = null, PropertyFlags flags = PropertyFlags.None, String parent = null, String[] children = null)
        {
            Converter = converter;
            Aggregator = aggregator;
            Flags = flags;
            Parent = parent;
            Children = children ?? new String[0];
        }

        /// <summary>
        /// Gets the associated value converter.
        /// </summary>
        public IValueConverter Converter { get; }

        /// <summary>
        /// Gets the associated value aggregator.
        /// </summary>
        public IValueAggregator Aggregator { get; }

        /// <summary>
        /// Gets the flags of the declaration.
        /// </summary>
        public PropertyFlags Flags { get; }

        /// <summary>
        /// Gets the name of the related shorthand declaration, if any.
        /// </summary>
        public String Parent { get; }

        /// <summary>
        /// Gets the names of related longhand declarations.
        /// </summary>
        public String[] Children { get; }
    }
}
