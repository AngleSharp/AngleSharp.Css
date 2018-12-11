namespace AngleSharp.Css.Extensions
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A set of useful extension methods for the Element class.
    /// </summary>
    [DomExposed("Element")]
    public static class ElementExtensions
    {
        /// <summary>
        /// Creates a pseudo element for the current element.
        /// </summary>
        /// <param name="element">The element to extend.</param>
        /// <param name="pseudoElement">
        /// The element to create (e.g. ::after).
        /// </param>
        /// <returns>The created element or null, if not possible.</returns>
        [DomName("pseudo")]
        public static IPseudoElement Pseudo(this IElement element, String pseudoElement)
        {
            var factory = element.Owner?.Context.GetService<IPseudoElementFactory>();
            return factory?.Create(element, pseudoElement);
        }

        public static String GetInnerText(this IElement element)
        {
            return element.TextContent;
        }

        public static void SetInnerText(this IElement element, String value)
        {
            element.TextContent = value;
        }

        /*
        [DomName("innerText")]
        [DomAccessor(Accessors.Getter)]

        public static String GetInnerText(this IElement element)
        {
            bool? hidden = null;

            if (element.Owner == null)
            {
                hidden = true;
            }

            if (!hidden.HasValue)
            {
                var css = element.ComputeCurrentStyle();

                if (!String.IsNullOrEmpty(css?.Display))
                {
                    hidden = css.Display == "none";
                }
            }

            if (!hidden.HasValue)
            {
                hidden = element.IsHidden;
            }

            if (hidden.Value)
            {
                return element.TextContent;
            }

            var sb = Pool.NewStringBuilder();
            var requiredLineBreakCounts = new Dictionary<Int32, Int32>();

            InnerTextCollection(element, sb, requiredLineBreakCounts, element.ParentElement?.ComputeCurrentStyle());

            // Remove any runs of consecutive required line break count items at the start or end of results.
            requiredLineBreakCounts.Remove(0);
            requiredLineBreakCounts.Remove(sb.Length);
            var offset = 0;

            foreach (var keyval in requiredLineBreakCounts.OrderBy(kv => kv.Key)) // SortedDictionary would be nicer
            {
                var index = keyval.Key + offset;
                sb.Insert(index, new String(Symbols.LineFeed, keyval.Value));
                offset += keyval.Value;
            }

            return sb.ToPool();
        }

        [DomName("innerText")]
        [DomAccessor(Accessors.Setter)]

        public static void SetInnerText(this IElement element, String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                element.ReplaceAll(null, false);
            }
            else
            {
                var fragment = new DocumentFragment(element.Owner);
                var sb = Pool.NewStringBuilder();

                for (var i = 0; i < value.Length; i++)
                {
                    var c = value[i];

                    if (c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                    {
                        if (c == Symbols.CarriageReturn && i + 1 < value.Length && value[i + 1] == Symbols.LineFeed)
                        {
                            continue; // ignore carriage return if the next char is a line feed
                        }

                        if (sb.Length > 0)
                        {
                            fragment.AppendChild(new TextNode(element.Owner, sb.ToPool()));
                            sb = Pool.NewStringBuilder();
                        }

                        fragment.AppendChild(new HtmlBreakRowElement(element.Owner));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }

                var remaining = sb.ToPool();

                if (remaining.Length > 0)
                {
                    fragment.Append(new TextNode(element.Owner, remaining));
                }

                element.ReplaceAll(fragment, false);
            }
        }

        private static void InnerTextCollection(INode node, StringBuilder sb, Dictionary<Int32, Int32> requiredLineBreakCounts, ICssStyleDeclaration parentStyle)
        {
            if (!HasCssBox(node))
            {
                return;
            }

            var elementCss = (node as IElement)?.ComputeCurrentStyle();

            bool? elementHidden = null;

            if (elementCss != null)
            {
                if (!String.IsNullOrEmpty(elementCss.Display))
                {
                    elementHidden = elementCss.Display == "none";
                }
                if (!String.IsNullOrEmpty(elementCss.Visibility) && elementHidden != true)
                {
                    elementHidden = elementCss.Visibility != "visible";
                }
            }

            if (!elementHidden.HasValue)
            {
                elementHidden = (node as IHtmlElement)?.IsHidden ?? false;
            }

            if (elementHidden.Value)
            {
                return;
            }

            var startIndex = sb.Length;

            foreach (var child in node.ChildNodes)
            {
                InnerTextCollection(child, sb, requiredLineBreakCounts, elementCss);
            }

            if (node is IText)
            {
                var textElement = (IText)node;

                ProcessText(textElement.Data, sb, parentStyle);
            }
            else if (node is IHtmlBreakRowElement)
            {
                sb.Append(Symbols.LineFeed);
            }
            else if ((node is IHtmlTableCellElement && String.IsNullOrEmpty(elementCss.Display)) || elementCss.Display == "table-cell")
            {
                var nextSibling = node.NextSibling as IElement;
                if (nextSibling != null)
                {
                    var nextSiblingCss = nextSibling.ComputeCurrentStyle();
                    if (nextSibling is IHtmlTableCellElement && String.IsNullOrEmpty(nextSiblingCss.Display) || nextSiblingCss.Display == "table-cell")
                    {
                        sb.Append(Symbols.Tab);
                    }
                }
            }
            else if ((node is IHtmlTableRowElement && String.IsNullOrEmpty(elementCss.Display)) || elementCss.Display == "table-row")
            {
                var nextSibling = node.NextSibling as IElement;

                if (nextSibling != null)
                {
                    var nextSiblingCss = nextSibling.ComputeCurrentStyle();

                    if (nextSibling is IHtmlTableRowElement && String.IsNullOrEmpty(nextSiblingCss.Display) || nextSiblingCss.Display == "table-row")
                    {
                        sb.Append(Symbols.LineFeed);
                    }
                }
            }
            else if (node is IHtmlParagraphElement)
            {
                var startIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(startIndex, out startIndexCount);

                if (startIndexCount < 2)
                {
                    requiredLineBreakCounts[startIndex] = 2;
                }

                var endIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(sb.Length, out endIndexCount);

                if (endIndexCount < 2)
                {
                    requiredLineBreakCounts[sb.Length] = 2;
                }
            }

            bool? isBlockLevel = null;

            if (elementCss != null)
            {
                if (IsBlockLevelDisplay(elementCss.Display))
                {
                    isBlockLevel = true;
                }
            }

            if (!isBlockLevel.HasValue)
            {
                isBlockLevel = IsBlockLevel(node);
            }

            if (isBlockLevel.Value)
            {
                var startIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(startIndex, out startIndexCount);

                if (startIndexCount < 1)
                {
                    requiredLineBreakCounts[startIndex] = 1;
                }

                var endIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(sb.Length, out endIndexCount);

                if (endIndexCount < 1)
                {
                    requiredLineBreakCounts[sb.Length] = 1;
                }
            }
        }

        private static Boolean HasCssBox(INode node)
        {
            switch (node.NodeName)
            {
                case "CANVAS":
                case "COL":
                case "COLGROUP":
                case "DETAILS":
                case "FRAME":
                case "FRAMESET":
                case "IFRAME":
                case "IMG":
                case "INPUT":
                case "LINK":
                case "METER":
                case "PROGRESS":
                case "TEMPLATE":
                case "TEXTAREA":
                case "VIDEO":
                case "WBR":
                case "SCRIPT":
                case "STYLE":
                case "NOSCRIPT":
                    return false;
                default:
                    return true;
            }
        }

        private static Boolean IsBlockLevelDisplay(String display)
        {
            // https://www.w3.org/TR/css-display-3/#display-value-summary
            // https://hg.mozilla.org/mozilla-central/file/0acceb224b7d/servo/components/layout/query.rs#l1016
            switch (display)
            {
                case "block":
                case "flow-root":
                case "flex":
                case "grid":
                case "table":
                case "table-caption":
                    return true;
                default:
                    return false;
            }
        }

        private static Boolean IsBlockLevel(INode node)
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTML/Block-level_elements
            switch (node.NodeName)
            {
                case "ADDRESS":
                case "ARTICLE":
                case "ASIDE":
                case "BLOCKQUOTE":
                case "CANVAS":
                case "DD":
                case "DIV":
                case "DL":
                case "DT":
                case "FIELDSET":
                case "FIGCAPTION":
                case "FIGURE":
                case "FOOTER":
                case "FORM":
                case "H1":
                case "H2":
                case "H3":
                case "H4":
                case "H5":
                case "H6":
                case "HEADER":
                case "GROUP":
                case "HR":
                case "LI":
                case "MAIN":
                case "NAV":
                case "NOSCRIPT":
                case "OL":
                case "OPTION":
                case "OUTPUT":
                case "P":
                case "PRE":
                case "SECTION":
                case "TABLE":
                case "TFOOT":
                case "UL":
                case "VIDEO":
                    return true;
                default:
                    return false;
            }
        }

        private static void ProcessText(String text, StringBuilder sb, ICssStyleDeclaration style)
        {
            var startIndex = sb.Length;
            var whiteSpace = style?.WhiteSpace;
            var textTransform = style?.TextTransform;

            var isWhiteSpace = startIndex > 0 ? Char.IsWhiteSpace(sb[startIndex - 1]) && sb[startIndex - 1] != Symbols.NoBreakSpace : true;
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
                                if (!isWhiteSpace)
                                {
                                    c = Symbols.Space;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            break;
                        case "nowrap":
                        case "normal":
                        default:
                            if (!isWhiteSpace)
                            {
                                c = Symbols.Space;
                            }
                            else
                            {
                                continue;
                            }
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

            if (isWhiteSpace) // ended with whitespace
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
        }*/
    }
}
