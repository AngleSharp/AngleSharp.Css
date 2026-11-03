namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// The presentational hints of HTML elements (HTML 15.3 - 15.4), i.e. attributes like the
    /// width of an img, which map to CSS declarations. These take part in the cascade like
    /// author rules with a specificity of zero, which precede all other author rules.
    /// </summary>
    static class PresentationalHints
    {
        /// <summary>
        /// Gets the declarations the attributes of the element map to, if any.
        /// </summary>
        public static String? GetDeclarations(IElement element)
        {
            if (element is not IHtmlElement)
            {
                return null;
            }

            var hints = new StringBuilder();

            if (element.Owner?.CompatMode == "BackCompat")
            {
                AddQuirks(hints, element);
            }

            // a cell gets its hints from the table, too
            if (element.Attributes.Length == 0 && element.LocalName is not ("td" or "th"))
            {
                return hints.Length > 0 ? hints.ToString() : null;
            }

            switch (element.LocalName)
            {
                case "img":
                case "embed":
                case "object":
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddImageSpacing(hints, element);
                    AddImageAlign(hints, element);
                    break;
                case "video":
                case "iframe":
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddImageSpacing(hints, element);

                    if (element.GetAttribute("frameborder")?.Trim() is "0" or "no")
                    {
                        hints.Append("border: 0;");
                    }

                    break;
                case "input" when element.GetAttribute("type").Isi("image"):
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddImageSpacing(hints, element);
                    AddImageAlign(hints, element);
                    break;
                case "table":
                    AddDimension(hints, element, "width", "width", ignoreZero: true);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddColor(hints, element, "bgcolor", "background-color");
                    AddBackground(hints, element);
                    AddTableAlign(hints, element);
                    AddLength(hints, element, "cellspacing", "border-spacing");
                    AddTableBorder(hints, element);
                    break;
                case "td":
                case "th":
                    AddDimension(hints, element, "width", "width", ignoreZero: true);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddColor(hints, element, "bgcolor", "background-color");
                    AddBackground(hints, element);
                    AddCellAlign(hints, element);
                    AddTableCellHints(hints, element);

                    if (element.HasAttribute("nowrap"))
                    {
                        hints.Append("white-space: nowrap;");
                    }

                    break;
                case "tr":
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddColor(hints, element, "bgcolor", "background-color");
                    AddBackground(hints, element);
                    AddCellAlign(hints, element);
                    break;
                case "thead":
                case "tbody":
                case "tfoot":
                    AddColor(hints, element, "bgcolor", "background-color");
                    AddBackground(hints, element);
                    AddCellAlign(hints, element);
                    break;
                case "col":
                case "colgroup":
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    break;
                case "hr":
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    AddRuleHints(hints, element);
                    break;
                case "body":
                    AddColor(hints, element, "bgcolor", "background-color");
                    AddColor(hints, element, "text", "color");
                    AddBackground(hints, element);
                    AddLength(hints, element, "marginheight", "margin-top");
                    AddLength(hints, element, "marginheight", "margin-bottom");
                    AddLength(hints, element, "marginwidth", "margin-left");
                    AddLength(hints, element, "marginwidth", "margin-right");
                    AddLength(hints, element, "topmargin", "margin-top");
                    AddLength(hints, element, "bottommargin", "margin-bottom");
                    AddLength(hints, element, "leftmargin", "margin-left");
                    AddLength(hints, element, "rightmargin", "margin-right");
                    break;
                case "font":
                    AddColor(hints, element, "color", "color");
                    AddFontHints(hints, element);
                    break;
                case "div":
                    AddBlockAlign(hints, element, moveBlocks: true);
                    break;
                case "p":
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "h5":
                case "h6":
                    AddBlockAlign(hints, element, moveBlocks: false);
                    break;
                case "ul":
                case "ol":
                case "li":
                    AddListType(hints, element);
                    break;
            }

            return hints.Length > 0 ? hints.ToString() : null;
        }

        /// <summary>
        /// The rules of the quirks mode sheet (HTML 15.3.10, 15.3.13), which come before the hints.
        /// </summary>
        private static void AddQuirks(StringBuilder hints, IElement element)
        {
            switch (element.LocalName)
            {
                case "table":
                    hints.Append("white-space: normal; line-height: normal; font-weight: normal; font-size: medium; font-variant: normal; font-style: normal; text-align: start;");
                    AddColor(hints, element.Owner!.Body ?? element, "text", "color");
                    break;
                case "form":
                    hints.Append("margin-bottom: 1em;");
                    break;
                case "textarea":
                case "input" when !element.GetAttribute("type").Isi("image"):
                    hints.Append("box-sizing: border-box;");
                    break;
                case "img" when element.GetAttribute("align").Isi("left"):
                    hints.Append("margin-right: 3px;");
                    break;
                case "img" when element.GetAttribute("align").Isi("right"):
                    hints.Append("margin-left: 3px;");
                    break;
            }
        }

        /// <summary>
        /// A whole number of pixels, or a percentage, as the property.
        /// </summary>
        private static void AddLength(StringBuilder hints, IElement element, String attribute, String property)
        {
            var length = ParseLength(element.GetAttribute(attribute));

            if (length is not null)
            {
                hints.Append(property).Append(": ").Append(length).Append(';');
            }
        }

        private static String? ParseLength(String? value)
        {
            value = value?.Trim();

            if (String.IsNullOrEmpty(value))
            {
                return null;
            }

            var index = 0;

            while (index < value!.Length && value[index].IsDigit())
            {
                index++;
            }

            if (index == 0)
            {
                return null;
            }

            return value.Substring(0, index) + (index < value.Length && value[index] == Symbols.Percent ? "%" : "px");
        }

        private static void AddBackground(StringBuilder hints, IElement element)
        {
            var value = element.GetAttribute("background")?.Trim();

            if (value is { Length: > 0 } && value.IndexOfAny(new[] { '"', '\\', '\n', ';', '{', '}' }) < 0)
            {
                hints.Append("background-image: url(\"").Append(value).Append("\");");
            }
        }

        private static void AddTableCellHints(StringBuilder hints, IElement cell)
        {
            var table = cell.ParentElement;

            while (table is not null && table.LocalName != "table")
            {
                table = table.ParentElement;
            }

            if (table is null)
            {
                return;
            }


            // the attribute only knows pixels, whatever follows the number
            if (ParseLength(table.GetAttribute("cellpadding")) is { } padding)
            {
                hints.Append("padding: ").Append(padding.TrimEnd('%', 'p', 'x')).Append("px;");
            }

            if (table.HasAttribute("border") && ParseLength(table.GetAttribute("border")) is not ("0px" or "0%"))
            {
                hints.Append("border: 1px inset ").Append(GetBorderColor(table)).Append(';');
            }
        }

        private static void AddTableBorder(StringBuilder hints, IElement table)
        {
            if (table.HasAttribute("border"))
            {
                var length = ParseLength(table.GetAttribute("border")) ?? "1px";
                hints.Append("border: ").Append(length.EndsWith("%") ? "1px" : length).Append(" outset ").Append(GetBorderColor(table)).Append(';');
            }
        }

        private static String GetBorderColor(IElement table)
        {
            var color = table.GetAttribute("bordercolor")?.Trim();
            return color is { Length: > 0 } && color.IndexOfAny(new[] { ';', '{', '}', '!' }) < 0 ? color : "gray";
        }

        private static void AddTableAlign(StringBuilder hints, IElement table)
        {
            switch (table.GetAttribute("align")?.Trim().ToLowerInvariant())
            {
                case "left":
                    hints.Append("float: left;");
                    break;
                case "right":
                    hints.Append("float: right;");
                    break;
                case "center":
                case "middle":
                    hints.Append("margin-left: auto; margin-right: auto;");
                    break;
            }
        }

        private static void AddCellAlign(StringBuilder hints, IElement element)
        {
            AddBlockAlign(hints, element, moveBlocks: true);
            var valign = element.GetAttribute("valign")?.Trim().ToLowerInvariant();

            if (valign is "top" or "middle" or "bottom" or "baseline")
            {
                hints.Append("vertical-align: ").Append(valign).Append(';');
            }
        }

        private static void AddBlockAlign(StringBuilder hints, IElement element, Boolean moveBlocks)
        {
            var prefix = moveBlocks ? "-webkit-" : String.Empty;

            switch (element.GetAttribute("align")?.Trim().ToLowerInvariant())
            {
                case "left":
                    hints.Append("text-align: ").Append(prefix).Append("left;");
                    break;
                case "right":
                    hints.Append("text-align: ").Append(prefix).Append("right;");
                    break;
                case "center":
                case "middle":
                    hints.Append("text-align: ").Append(prefix).Append("center;");
                    break;
                case "justify":
                    hints.Append("text-align: justify;");
                    break;
            }
        }

        private static void AddImageSpacing(StringBuilder hints, IElement element)
        {
            AddLength(hints, element, "hspace", "margin-left");
            AddLength(hints, element, "hspace", "margin-right");
            AddLength(hints, element, "vspace", "margin-top");
            AddLength(hints, element, "vspace", "margin-bottom");

            if (element.LocalName != "iframe" && ParseLength(element.GetAttribute("border")) is { } border && !border.EndsWith("%"))
            {
                hints.Append("border: ").Append(border).Append(" solid;");
            }
        }

        private static void AddImageAlign(StringBuilder hints, IElement element)
        {
            switch (element.GetAttribute("align")?.Trim().ToLowerInvariant())
            {
                case "left":
                    hints.Append("float: left;");
                    break;
                case "right":
                    hints.Append("float: right;");
                    break;
                case "top":
                    hints.Append("vertical-align: top;");
                    break;
                case "bottom":
                case "baseline":
                    hints.Append("vertical-align: baseline;");
                    break;
                case "middle":
                    hints.Append("vertical-align: -webkit-baseline-middle;");
                    break;
                case "absmiddle":
                    hints.Append("vertical-align: middle;");
                    break;
                case "texttop":
                    hints.Append("vertical-align: text-top;");
                    break;
                case "absbottom":
                    hints.Append("vertical-align: bottom;");
                    break;
            }
        }

        private static void AddRuleHints(StringBuilder hints, IElement rule)
        {
            switch (rule.GetAttribute("align")?.Trim().ToLowerInvariant())
            {
                case "left":
                    hints.Append("margin-left: 0; margin-right: auto;");
                    break;
                case "right":
                    hints.Append("margin-left: auto; margin-right: 0;");
                    break;
                case "center":
                    hints.Append("margin-left: auto; margin-right: auto;");
                    break;
            }

            if (rule.HasAttribute("color"))
            {
                hints.Append("border-style: solid;");
                AddColor(hints, rule, "color", "color");
                AddColor(hints, rule, "color", "background-color");
            }
            else if (rule.HasAttribute("noshade"))
            {
                hints.Append("border-style: solid; background-color: gray; border-color: gray;");
            }

            if (Int32.TryParse(rule.GetAttribute("size")?.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out var size) && size > 0)
            {
                // the 1px border on either side is part of its size
                hints.Append(size == 1 ? "border-bottom-width: 0; height: 0;" : $"height: {size - 2}px;");
            }
        }

        private static void AddFontHints(StringBuilder hints, IElement font)
        {
            var size = font.GetAttribute("size")?.Trim();

            if (size is { Length: > 0 })
            {
                var isRelative = size[0] == '+' || size[0] == '-';

                if (Int32.TryParse(isRelative ? size.Substring(1) : size, NumberStyles.None, CultureInfo.InvariantCulture, out var number))
                {
                    var level = Math.Min(7, Math.Max(1, !isRelative ? number : size[0] == '+' ? 3 + number : 3 - number));
                    hints.Append("font-size: ").Append(FontSizes[level - 1]).Append(';');
                }
            }

            var face = font.GetAttribute("face")?.Trim();

            if (face is { Length: > 0 } && face.IndexOfAny(new[] { ';', '{', '}', '!', '\\' }) < 0)
            {
                hints.Append("font-family: ").Append(face).Append(';');
            }
        }

        // the sizes of the legacy font size attribute (HTML 15.3.4)
        private static readonly String[] FontSizes = { "x-small", "small", "medium", "large", "x-large", "xx-large", "48px" };

        private static void AddListType(StringBuilder hints, IElement element)
        {
            var value = element.GetAttribute("type")?.Trim();
            var type = value switch
            {
                "1" => "decimal",
                "a" => "lower-alpha",
                "A" => "upper-alpha",
                "i" => "lower-roman",
                "I" => "upper-roman",
                _ => value?.ToLowerInvariant() is "circle" or "disc" or "square" or "none" ? value.ToLowerInvariant() : null,
            };

            if (type is not null)
            {
                hints.Append("list-style-type: ").Append(type).Append(';');
            }
        }

        /// <summary>
        /// Maps a dimension value (e.g. "100" or "50%") to the property (HTML 2.4.4.4).
        /// </summary>
        private static void AddDimension(StringBuilder hints, IElement element, String attribute, String property, Boolean ignoreZero)
        {
            var value = element.GetAttribute(attribute);

            if (value is null)
            {
                return;
            }

            var index = 0;

            while (index < value.Length && value[index].IsSpaceCharacter())
            {
                index++;
            }

            var start = index;

            while (index < value.Length && (value[index].IsDigit() || value[index] == Symbols.Dot))
            {
                index++;
            }

            if (index == start || !Double.TryParse(value.Substring(start, index - start), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var number) || (ignoreZero && number == 0))
            {
                return;
            }

            var unit = index < value.Length && value[index] == Symbols.Percent ? "%" : "px";
            hints.Append(property).Append(": ").Append(number.ToString(CultureInfo.InvariantCulture)).Append(unit).Append(';');
        }

        /// <summary>
        /// Maps a color (e.g. "#ffffff" or "white") to the property.
        /// </summary>
        private static void AddColor(StringBuilder hints, IElement element, String attribute, String property)
        {
            var value = element.GetAttribute(attribute)?.Trim();

            if (value is { Length: > 0 } && value.IndexOfAny(new[] { ';', '{', '}', '!' }) < 0)
            {
                hints.Append(property).Append(": ").Append(value).Append(';');
            }
        }
    }
}
