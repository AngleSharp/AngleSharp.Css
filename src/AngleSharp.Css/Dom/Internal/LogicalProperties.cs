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
        private static readonly Dictionary<String, (String Prefix, String Side, String Suffix)> Sides = CreateSides();

        private static readonly Dictionary<String, (String Prefix, Boolean IsInline)> Sizes = new(StringComparer.OrdinalIgnoreCase)
        {
            { PropertyNames.InlineSize, ("", true) },
            { PropertyNames.BlockSize, ("", false) },
            { PropertyNames.MinInlineSize, ("min-", true) },
            { PropertyNames.MinBlockSize, ("min-", false) },
            { PropertyNames.MaxInlineSize, ("max-", true) },
            { PropertyNames.MaxBlockSize, ("max-", false) },
        };

        /// <summary>
        /// Applies the logical properties of the given (cascaded) declarations, which are in
        /// cascade order, to their physical counterparts.
        /// </summary>
        public static void Apply(CssStyleDeclaration declarations, ICssComputeContext context)
        {
            List<ICssProperty>? logical = null;
            var order = new Dictionary<String, Int32>(StringComparer.OrdinalIgnoreCase);

            foreach (var property in declarations.Declarations)
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

        private static Dictionary<String, (String, String, String)> CreateSides()
        {
            var sides = new Dictionary<String, (String, String, String)>(StringComparer.OrdinalIgnoreCase);
            var groups = new[]
            {
                ("margin-", "margin-", ""),
                ("padding-", "padding-", ""),
                ("inset-", "", ""),
                ("scroll-margin-", "scroll-margin-", ""),
                ("scroll-padding-", "scroll-padding-", ""),
                ("border-", "border-", "-width"),
                ("border-", "border-", "-style"),
                ("border-", "border-", "-color"),
            };

            foreach (var (logicalPrefix, physicalPrefix, suffix) in groups)
            {
                foreach (var side in new[] { "block-start", "block-end", "inline-start", "inline-end" })
                {
                    sides[logicalPrefix + side + suffix] = (physicalPrefix, side, suffix);
                }
            }

            return sides;
        }
    }
}
