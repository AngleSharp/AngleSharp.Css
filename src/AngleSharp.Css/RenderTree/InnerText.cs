#nullable enable
namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Collects the rendered text of an element (innerText) from its render tree:
    /// https://html.spec.whatwg.org/multipage/dom.html#the-innertext-idl-attribute
    /// </summary>
    static class InnerText
    {
        /// <summary>
        /// Gets the rendered text of the element, from a partial render tree that only
        /// styles what is needed, e.g. not the content of hidden elements.
        /// </summary>
        public static String Get(IElement element)
        {
            var window = element.Owner?.DefaultView;

            if (window is null)
            {
                return element.TextContent;
            }

            var builder = RenderTreeBuilder.GetInstance(window);
            // nobody else sees this tree: what has been walked can be released on the way
            return Get(builder.RenderPartial(element, builder.DefaultDevice), true);
        }

        /// <summary>
        /// Gets the rendered text of the element of the given render node.
        /// </summary>
        public static String Get(ElementRenderNode node) => Get(node, false);

        /// <summary>
        /// Gets the rendered text of the element of the given render node, releasing the
        /// children of every node once it is walked if the tree is the walk's own: a tree
        /// that stays reachable until the end makes the garbage collector copy all of it.
        /// </summary>
        private static String Get(ElementRenderNode node, Boolean release)
        {
            var element = node.Ref;
            var display = node.ComputedStyle.GetDisplay();
            var hidden = !String.IsNullOrEmpty(display) ? display == CssKeywords.None : (element as IHtmlElement)?.IsHidden ?? false;

            // an element that is not being rendered gives its text content
            if (hidden)
            {
                return element.TextContent;
            }

            var sb = StringBuilderPool.Obtain();
            var requiredLineBreakCounts = new Dictionary<Int32, Int32>();
            var textStyleCache = new Dictionary<ICssStyleDeclaration, TextStyleInfo>();
            Collect(node, null, sb, requiredLineBreakCounts, textStyleCache, release);

            // Remove any runs of consecutive required line break count items at the start or end of results.
            requiredLineBreakCounts.Remove(0);
            requiredLineBreakCounts.Remove(sb.Length);
            var offset = 0;

            // SortedDictionary would be nicer
            foreach (var keyval in requiredLineBreakCounts.OrderBy(kv => kv.Key))
            {
                var index = keyval.Key + offset;
                sb.Insert(index, new String(Symbols.LineFeed, keyval.Value));
                offset += keyval.Value;
            }

            return sb.ToPool();
        }

        private static void Collect(IRenderNode node, ElementRenderNode? nextElement, StringBuilder sb, Dictionary<Int32, Int32> requiredLineBreakCounts, Dictionary<ICssStyleDeclaration, TextStyleInfo> textStyleCache, Boolean release)
        {
            if (!HasCssBox(node.Ref))
            {
                return;
            }

            var style = (node as ElementRenderNode)?.ComputedStyle;
            var hidden = new Nullable<Boolean>();

            if (style is not null)
            {
                var display = style.GetDisplay();
                var visibility = style.GetVisibility();

                if (!String.IsNullOrEmpty(display))
                {
                    hidden = display == CssKeywords.None;
                }

                if (!String.IsNullOrEmpty(visibility) && hidden != true)
                {
                    hidden = visibility != CssKeywords.Visible;
                }
            }

            // the children of a hidden element are never read, i.e., not even built
            if (hidden ?? (node.Ref as IHtmlElement)?.IsHidden ?? false)
            {
                return;
            }

            var startIndex = sb.Length;

            if (node is ElementRenderNode element)
            {
                var children = GetChildren(element);

                for (var i = 0; i < children.Count; i++)
                {
                    Collect(children[i], NextElement(children, i), sb, requiredLineBreakCounts, textStyleCache, release);
                }

                // the siblings stay, as the next sibling's style decides on the separator
                if (release)
                {
                    element.ReleaseContent();
                }
            }

            if (node is TextRenderNode text)
            {
                var next = text.Ref.NextSibling;
                var lastLine = next is null || String.IsNullOrEmpty(next.TextContent) || next is IHtmlBreakRowElement;
                var styleInfo = GetTextStyleInfo((text.Parent as ElementRenderNode)?.ComputedStyle, textStyleCache);
                ProcessText(text.Ref.Data, sb, styleInfo.WhiteSpace, styleInfo.TextTransform, lastLine, requiredLineBreakCounts);
            }
            else if (node.Ref is IHtmlBreakRowElement)
            {
                sb.Append(Symbols.LineFeed);
            }
            else if (style is not null && IsTableCell(node.Ref, style))
            {
                if (nextElement is not null && IsTableCell(nextElement.Ref, nextElement.ComputedStyle))
                {
                    sb.Append(Symbols.Tab);
                }
            }
            else if (style is not null && IsTableRow(node.Ref, style))
            {
                if (nextElement is not null && IsTableRow(nextElement.Ref, nextElement.ComputedStyle))
                {
                    sb.Append(Symbols.LineFeed);
                }
            }
            else if (node.Ref is IHtmlParagraphElement)
            {
                RequireLineBreaks(requiredLineBreakCounts, startIndex, 2);
                RequireLineBreaks(requiredLineBreakCounts, sb.Length, 2);
            }

            if ((style is not null && IsBlockLevelDisplay(style.GetDisplay())) || IsBlockLevel(node.Ref))
            {
                RequireLineBreaks(requiredLineBreakCounts, startIndex, 1);
                RequireLineBreaks(requiredLineBreakCounts, sb.Length, 1);
            }
        }

        /// <summary>
        /// Gets the render nodes of the element's own child nodes, in tree order. Generated
        /// content is left out, and so is the shadow tree of a shadow host: its light children
        /// count if they are rendered, i.e., assigned to a slot of the shadow tree.
        /// </summary>
        private static List<IRenderNode> GetChildren(ElementRenderNode node)
        {
            var element = node.Ref;

            if (element is not IPseudoElement && element.ShadowRoot is not null)
            {
                var rendered = FindRenderedChildren(node);
                var children = new List<IRenderNode>();

                foreach (var child in element.ChildNodes)
                {
                    if (rendered.TryGetValue(child, out var renderNode))
                    {
                        children.Add(renderNode);
                    }
                }

                return children;
            }

            return node.Content;
        }

        /// <summary>
        /// Finds the render nodes of the host's own child nodes, which are placed in the slots
        /// anywhere in its shadow tree.
        /// </summary>
        private static Dictionary<INode, IRenderNode> FindRenderedChildren(ElementRenderNode host)
        {
            var rendered = new Dictionary<INode, IRenderNode>();
            var pending = new Stack<IRenderNode>(host.Content);

            while (pending.Count > 0)
            {
                var current = pending.Pop();

                if (Object.ReferenceEquals(current.Ref.Parent, host.Ref))
                {
                    rendered[current.Ref] = current;
                }
                else if (current is ElementRenderNode element)
                {
                    foreach (var child in element.Content)
                    {
                        pending.Push(child);
                    }
                }
            }

            return rendered;
        }

        private static ElementRenderNode? NextElement(List<IRenderNode> children, Int32 index)
        {
            for (var i = index + 1; i < children.Count; i++)
            {
                if (children[i] is ElementRenderNode element)
                {
                    return element;
                }
            }

            return null;
        }

        private static Boolean IsTableCell(INode node, ICssStyleDeclaration style) =>
            (node is IHtmlTableCellElement && String.IsNullOrEmpty(style.GetDisplay())) || style.GetDisplay() == CssKeywords.TableCell;

        private static Boolean IsTableRow(INode node, ICssStyleDeclaration style) =>
            (node is IHtmlTableRowElement && String.IsNullOrEmpty(style.GetDisplay())) || style.GetDisplay() == CssKeywords.TableRow;

        private static void RequireLineBreaks(Dictionary<Int32, Int32> requiredLineBreakCounts, Int32 index, Int32 count)
        {
            requiredLineBreakCounts.TryGetValue(index, out var current);

            if (current < count)
            {
                requiredLineBreakCounts[index] = count;
            }
        }

        private static Boolean HasCssBox(INode node)
        {
            return node.NodeName switch
            {
                "CANVAS" or "COL" or "COLGROUP" or "DETAILS" or "FRAME" or "FRAMESET" or "IFRAME" or "IMG" or "INPUT" or "LINK" or "METER" or "PROGRESS" or "TEMPLATE" or "TEXTAREA" or "VIDEO" or "WBR" or "SCRIPT" or "STYLE" or "NOSCRIPT" => false,
                _ => true,
            };
        }

        private static Boolean IsBlockLevelDisplay(String display)
        {
            // https://www.w3.org/TR/css-display-3/#display-value-summary
            // https://hg.mozilla.org/mozilla-central/file/0acceb224b7d/servo/components/layout/query.rs#l1016
            return display switch
            {
                "block" or "flow-root" or "flex" or "grid" or "table" or "table-caption" => true,
                _ => false,
            };
        }

        private static Boolean IsBlockLevel(INode node)
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTML/Block-level_elements
            return node.NodeName switch
            {
                "ADDRESS" or "ARTICLE" or "ASIDE" or "BLOCKQUOTE" or "CANVAS" or "DD" or "DIV" or "DL" or "DT" or "FIELDSET" or "FIGCAPTION" or "FIGURE" or "FOOTER" or "FORM" or "H1" or "H2" or "H3" or "H4" or "H5" or "H6" or "HEADER" or "GROUP" or "HR" or "LI" or "MAIN" or "NAV" or "NOSCRIPT" or "OL" or "OPTION" or "OUTPUT" or "P" or "PRE" or "SECTION" or "TABLE" or "TFOOT" or "UL" or "VIDEO" => true,
                _ => false,
            };
        }

        private static Boolean IsWhiteSpace(Char c) => Char.IsWhiteSpace(c) && c != Symbols.NoBreakSpace;

        private static TextStyleInfo GetTextStyleInfo(ICssStyleDeclaration? style, Dictionary<ICssStyleDeclaration, TextStyleInfo> cache)
        {
            if (style is null)
            {
                return default;
            }

            if (cache.TryGetValue(style, out var info))
            {
                return info;
            }

            info = new TextStyleInfo(style.GetWhiteSpace(), style.GetTextTransform());
            cache[style] = info;
            return info;
        }

        private static void ProcessText(String text, StringBuilder sb, String? whiteSpace, String? textTransform, Boolean lastLine, Dictionary<Int32, Int32> requiredLineBreakCounts)
        {
            var startIndex = sb.Length;
            var isWhiteSpace = startIndex <= 0 || IsWhiteSpace(sb[startIndex - 1]) || (requiredLineBreakCounts.ContainsKey(startIndex) && IsWhiteSpace(text[0]));

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (Char.IsWhiteSpace(c) && c != Symbols.NoBreakSpace)
                {
                    // https://drafts.csswg.org/css-text/#white-space-property
                    switch (whiteSpace)
                    {
                        case "pre":
                        case "pre-wrap":
                            break;
                        case "pre-line":
                            if (c == Symbols.Space || c == Symbols.Tab)
                            {
                                if (isWhiteSpace)
                                {
                                    continue;
                                }

                                c = Symbols.Space;
                            }
                            break;
                        case "nowrap":
                        case "normal":
                        default:
                            if (isWhiteSpace)
                            {
                                continue;
                            }

                            c = Symbols.Space;
                            break;
                    }

                    isWhiteSpace = true;
                }
                else
                {
                    // https://drafts.csswg.org/css-text/#propdef-text-transform
                    switch (textTransform)
                    {
                        case "uppercase":
                            c = Char.ToUpperInvariant(c);
                            break;
                        case "lowercase":
                            c = Char.ToLowerInvariant(c);
                            break;
                        case "capitalize":
                            if (isWhiteSpace)
                            {
                                c = Char.ToUpperInvariant(c);
                            }
                            break;
                        case "none":
                        default:
                            break;
                    }

                    isWhiteSpace = false;
                }

                sb.Append(c);
            }

            // ended with whitespace
            if (isWhiteSpace && lastLine)
            {
                for (var offset = sb.Length - 1; offset >= startIndex; offset--)
                {
                    var c = sb[offset];

                    if (!Char.IsWhiteSpace(c) || c == Symbols.NoBreakSpace)
                    {
                        sb.Remove(offset + 1, sb.Length - 1 - offset);
                        break;
                    }
                }
            }
        }

        private readonly struct TextStyleInfo
        {
            public TextStyleInfo(String? whiteSpace, String? textTransform)
            {
                WhiteSpace = whiteSpace;
                TextTransform = textTransform;
            }

            public String? WhiteSpace { get; }

            public String? TextTransform { get; }
        }
    }
}
