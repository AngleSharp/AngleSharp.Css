#nullable enable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Converts the display property, including the multi-keyword syntax
    /// `[ &lt;display-outside&gt; || &lt;display-inside&gt; ] | &lt;display-listitem&gt;`
    /// of CSS Display 3. Multi-keyword values serialize in their shortest form,
    /// e.g., `inline flex` as `inline-flex` and `block flow-root` as `flow-root`.
    /// </summary>
    sealed class DisplayValueConverter : IValueConverter
    {
        private readonly IValueConverter _keywords = Map.DisplayModes.ToConverter();

        public ICssValue? Convert(StringSource source)
        {
            var start = source.Index;
            var outer = default(String);
            var inner = default(String);
            var listItem = false;
            var count = 0;

            while (count < 3)
            {
                var pos = source.Index;

                if (count > 0)
                {
                    source.SkipSpacesAndComments();
                }

                var ident = source.ParseIdent()?.ToLowerFast();

                if (ident is null)
                {
                    source.BackTo(pos);
                    break;
                }
                else if (outer is null && (ident.Is(CssKeywords.Block) || ident.Is(CssKeywords.Inline)))
                {
                    outer = ident;
                }
                else if (inner is null && IsInner(ident))
                {
                    inner = ident;
                }
                else if (!listItem && ident.Is(CssKeywords.ListItem))
                {
                    listItem = true;
                }
                else
                {
                    source.BackTo(pos);
                    break;
                }

                count++;
            }

            if (count > 1 || (count == 1 && inner is not null))
            {
                var value = Normalize(outer, inner, listItem);

                if (value is not null)
                {
                    return value;
                }
            }

            source.BackTo(start);
            return _keywords.Convert(source);
        }

        private static Boolean IsInner(String ident) =>
            ident.Is(CssKeywords.Flow) || ident.Is(CssKeywords.FlowRoot) || ident.Is(CssKeywords.Table) ||
            ident.Is(CssKeywords.Flex) || ident.Is(CssKeywords.Grid) || ident.Is(CssKeywords.Ruby);

        private static ICssValue? Normalize(String? outer, String? inner, Boolean listItem)
        {
            if (listItem)
            {
                if (inner is not null && !inner.Is(CssKeywords.Flow) && !inner.Is(CssKeywords.FlowRoot))
                {
                    return null;
                }

                var text = CssKeywords.ListItem;

                if (inner is not null && inner.Is(CssKeywords.FlowRoot))
                {
                    text = String.Concat(inner, " ", text);
                }

                if (outer is not null && outer.Is(CssKeywords.Inline))
                {
                    text = String.Concat(outer, " ", text);
                }

                return new CssConstantValue<DisplayMode>(text, DisplayMode.ListItem);
            }

            // A ruby container is inline by default, every other one is block-level.
            var isInline = outer is not null ? outer.Is(CssKeywords.Inline) : inner is not null && inner.Is(CssKeywords.Ruby);
            inner ??= CssKeywords.Flow;

            if (inner.Is(CssKeywords.Flow))
            {
                return isInline ? Create(CssKeywords.Inline, DisplayMode.Inline) : Create(CssKeywords.Block, DisplayMode.Block);
            }
            else if (inner.Is(CssKeywords.FlowRoot))
            {
                return isInline ? Create(CssKeywords.InlineBlock, DisplayMode.InlineBlock) : Create(CssKeywords.FlowRoot, DisplayMode.FlowRoot);
            }
            else if (inner.Is(CssKeywords.Table))
            {
                return isInline ? Create(CssKeywords.InlineTable, DisplayMode.InlineTable) : Create(CssKeywords.Table, DisplayMode.Table);
            }
            else if (inner.Is(CssKeywords.Flex))
            {
                return isInline ? Create(CssKeywords.InlineFlex, DisplayMode.InlineFlex) : Create(CssKeywords.Flex, DisplayMode.Flex);
            }
            else if (inner.Is(CssKeywords.Grid))
            {
                return isInline ? Create(CssKeywords.InlineGrid, DisplayMode.InlineGrid) : Create(CssKeywords.Grid, DisplayMode.Grid);
            }
            else if (inner.Is(CssKeywords.Ruby))
            {
                return isInline ? Create(CssKeywords.Ruby, DisplayMode.Ruby) : Create(String.Concat(CssKeywords.Block, " ", CssKeywords.Ruby), DisplayMode.Ruby);
            }

            return null;
        }

        private static ICssValue Create(String keyword, DisplayMode mode) => new CssConstantValue<DisplayMode>(keyword, mode);
    }
}
