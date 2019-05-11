namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    /// <summary>
    /// A collection of useful information regarding a CSS declaration.
    /// </summary>
    public class DeclarationInfo
    {
        /// <summary>
        /// Constructs a new declaration info.
        /// </summary>
        /// <param name="name">The name of the declaration.</param>
        /// <param name="converter">The value converter.</param>
        /// <param name="flags">The property flags.</param>
        /// <param name="initialValue">The initial value, if any.</param>
        /// <param name="shorthands">The names of the associated shorthand declarations, if any.</param>
        /// <param name="longhands">The names of the associated longhand declarations, if any.</param>
        public DeclarationInfo(String name, IValueConverter converter, PropertyFlags flags = PropertyFlags.None, ICssValue initialValue = null, String[] shorthands = null, String[] longhands = null)
        {
            Name = name;
            Converter = initialValue != null ? Or(converter, AssignInitial(initialValue)) : converter;
            Aggregator = converter as IValueAggregator;
            Flags = flags;
            InitialValue = initialValue;
            Shorthands = shorthands ?? Array.Empty<String>();
            Longhands = longhands ?? Array.Empty<String>();
        }

        /// <summary>
        /// Gets the declaration name.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the initial value of the declaration, if any.
        /// </summary>
        public ICssValue InitialValue { get; }

        /// <summary>
        /// Gets the associated value converter.
        /// </summary>
        public IValueConverter Converter { get; }

        /// <summary>
        /// Gets the value aggregator, if any.
        /// </summary>
        public IValueAggregator Aggregator { get; }

        /// <summary>
        /// Gets the flags of the declaration.
        /// </summary>
        public PropertyFlags Flags { get; }

        /// <summary>
        /// Gets the names of related shorthand declarations.
        /// </summary>
        public String[] Shorthands { get; }

        /// <summary>
        /// Gets the names of related required longhand declarations.
        /// </summary>
        public String[] Longhands { get; }
    }
}
