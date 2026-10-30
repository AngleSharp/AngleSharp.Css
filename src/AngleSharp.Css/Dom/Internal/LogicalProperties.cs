namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Maps the flow-relative (logical) properties, e.g. padding-inline-start, to the physical
    /// ones they stand for in the writing mode and direction of the element (CSS Logical
    /// Properties 1 §4). A logical property and its physical counterpart share one cascade: the
    /// one declared later wins, unless only the other one is important.
    /// </summary>
    static class LogicalProperties
    {
        private static readonly Dictionary<String, SideMapping> Sides = CreateSides();

        private static readonly Dictionary<String, SizeMapping> Sizes = new(StringComparer.OrdinalIgnoreCase)
        {
            { PropertyNames.InlineSize, new SizeMapping("", true) },
            { PropertyNames.BlockSize, new SizeMapping("", false) },
            { PropertyNames.MinInlineSize, new SizeMapping("min-", true) },
            { PropertyNames.MinBlockSize, new SizeMapping("min-", false) },
            { PropertyNames.MaxInlineSize, new SizeMapping("max-", true) },
            { PropertyNames.MaxBlockSize, new SizeMapping("max-", false) },
        };

        /// <summary>
        /// Applies the logical properties of the given (cascaded) declarations, which are in
        /// cascade order, to their physical counterparts.
        /// </summary>
        public static void Apply(CssStyleDeclaration declarations, ICssComputeContext context)
        {
            List<ICssProperty>? logical = null;
            var order = new Dictionary<String, Int32>(StringComparer.OrdinalIgnoreCase);

            // logical properties are not inherited, so only the local declarations matter
            foreach (var property in declarations.GetLocalDeclarations())
            {
                order[property.Name] = order.Count;

                if (Sides.ContainsKey(property.Name) || Sizes.ContainsKey(property.Name))
                {
                    (logical ??= new List<ICssProperty>()).Add(property);
                }
            }

            if (logical is null)
            {
                return;
            }

            var writingMode = declarations.GetPropertyValue(PropertyNames.WritingMode);
            var vertical = writingMode.StartsWith("vertical", StringComparison.OrdinalIgnoreCase) ||
                writingMode.StartsWith("sideways", StringComparison.OrdinalIgnoreCase) ||
                writingMode.StartsWith("tb", StringComparison.OrdinalIgnoreCase);
            var blockFromLeft = writingMode.EndsWith("-lr", StringComparison.OrdinalIgnoreCase);
            var inlineReversed = declarations.GetPropertyValue(PropertyNames.Direction).Isi(CssKeywords.Rtl) != writingMode.Isi("sideways-lr");

            foreach (var property in logical)
            {
                var physical = Sides.TryGetValue(property.Name, out var side)
                    ? side.Prefix + MapSide(side.Side, vertical, blockFromLeft, inlineReversed) + side.Suffix
                    : Sizes[property.Name].Prefix + (Sizes[property.Name].IsInline != vertical ? "width" : "height");
                var existing = order.TryGetValue(physical, out var physicalIndex) ? declarations.GetProperty(physical) : null;

                if (existing is null || (property.IsImportant == existing.IsImportant ? order[property.Name] > physicalIndex : property.IsImportant))
                {
                    var info = context.Context.GetDeclarationInfo(physical);
                    // a value from a shorthand with var() is picked from it by the logical name
                    var value = property.RawValue is CssChildValue ? property.Compute(context).RawValue : property.RawValue;

                    if (existing is not null)
                    {
                        declarations.RemoveProperty(existing);
                    }

                    declarations.AddProperty(new CssProperty(physical, info.Converter, info.Flags, value, property.IsImportant));
                }
            }
        }

        // the inline axis runs left to right (top to bottom when vertical) unless reversed by rtl
        private static String MapSide(String side, Boolean vertical, Boolean blockFromLeft, Boolean inlineReversed) => side switch
        {
            "block-start" => !vertical ? "top" : blockFromLeft ? "left" : "right",
            "block-end" => !vertical ? "bottom" : blockFromLeft ? "right" : "left",
            "inline-start" => vertical ? (inlineReversed ? "bottom" : "top") : (inlineReversed ? "right" : "left"),
            _ => vertical ? (inlineReversed ? "top" : "bottom") : (inlineReversed ? "left" : "right"),
        };

        private static Dictionary<String, SideMapping> CreateSides()
        {
            var sides = new Dictionary<String, SideMapping>(StringComparer.OrdinalIgnoreCase);

            AddGroup("margin-", "margin-", "");
            AddGroup("padding-", "padding-", "");
            AddGroup("inset-", "", "");
            AddGroup("scroll-margin-", "scroll-margin-", "");
            AddGroup("scroll-padding-", "scroll-padding-", "");
            AddGroup("border-", "border-", "-width");
            AddGroup("border-", "border-", "-style");
            AddGroup("border-", "border-", "-color");

            return sides;

            void AddGroup(String logicalPrefix, String physicalPrefix, String suffix)
            {
                foreach (var side in new[] { "block-start", "block-end", "inline-start", "inline-end" })
                {
                    sides[logicalPrefix + side + suffix] = new SideMapping(physicalPrefix, side, suffix);
                }
            }
        }

        private readonly struct SideMapping
        {
            public SideMapping(String prefix, String side, String suffix)
            {
                Prefix = prefix;
                Side = side;
                Suffix = suffix;
            }

            public String Prefix { get; }
            public String Side { get; }
            public String Suffix { get; }
        }

        private readonly struct SizeMapping
        {
            public SizeMapping(String prefix, Boolean isInline)
            {
                Prefix = prefix;
                IsInline = isInline;
            }

            public String Prefix { get; }
            public Boolean IsInline { get; }
        }
    }
}
