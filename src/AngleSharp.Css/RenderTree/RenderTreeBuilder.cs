namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    sealed class RenderTreeBuilder
    {
        private static readonly ConditionalWeakTable<IWindow, RenderTreeBuilder> _windowRenderTrees = [];

        private readonly IBrowsingContext _context;
        private readonly IWindow _window;
        private readonly List<ICssStyleSheet> _defaultSheets;
        private readonly Dictionary<IElement, ICssStyleDeclaration> _cascadedStyles = [];

        private RenderTreeBuilder(IWindow window)
        {
            var ctx = window.Document.Context;
            var defaultStyleSheetProvider = ctx.GetServices<ICssDefaultStyleSheetProvider>();
            _context = ctx;
            _defaultSheets = defaultStyleSheetProvider.Select(m => m.Default).ToList();
            _window = window;
        }

        public IRenderDevice DefaultDevice => _context.GetService<IRenderDevice>() ?? new DefaultRenderDevice();

        public static RenderTreeBuilder GetInstance(IWindow window) => _windowRenderTrees.GetValue(window, (IWindow w) => new RenderTreeBuilder(w));

        public ElementRenderNode RenderDocument(IRenderDevice? device = null)
        {
            var document = _window.Document;
            var currentSheets = document.GetStyleSheets().OfType<ICssStyleSheet>();
            var stylesheets = _defaultSheets.Concat(currentSheets).ToList();
            var collection = new StyleCollection(stylesheets, device ?? DefaultDevice);
            //var rootStyle = collection.ComputeCascadedStyle(document.DocumentElement);
            //var rootFontSize = ((CssLengthValue?)rootStyle.GetProperty(PropertyNames.FontSize)?.RawValue)?.Value ?? 16;
            //return RenderElement(rootFontSize, document.DocumentElement, collection);

            //TODO

            var root = _window.Document.DocumentElement;

            if (_cascadedStyles.TryGetValue(root, out var cascadedStyle))
            {
                var context = new CssComputeContext(collection.Device, _context, cascadedStyle);
                var computedStyle = cascadedStyle.Compute(context);
                return new ElementRenderNode(root, null, Enumerable.Empty<IRenderNode>(), cascadedStyle, computedStyle);
            }

            return new ElementRenderNode(root, null, Enumerable.Empty<IRenderNode>(), new CssStyleDeclaration(_context), new CssStyleDeclaration(_context));
        }

        public ICssStyleDeclaration GetElementStyle(IElement element, IRenderDevice? device = null)
        {
            //TODO compute if not available
            return _cascadedStyles[element];
        }

        public ElementRenderNode RenderElement(IElement element, IRenderDevice device)
        {
            //var document = _window.Document;
            //var currentSheets = document.GetStyleSheets().OfType<ICssStyleSheet>();
            //var stylesheets = _defaultSheets.Concat(currentSheets).ToList();
            //var styles = new StyleCollection(stylesheets, device);
            //var nodes = element.GetAncestors().OfType<IElement>().Reverse();

            //foreach (var node in nodes)
            //{

            //    computedStyle.UpdateDeclarations(styles.ComputeCascadedStyle(node));
            //}
            return new ElementRenderNode(element, null, Enumerable.Empty<IRenderNode>(), new CssStyleDeclaration(_context), new CssStyleDeclaration(_context));
        }

        //private ElementRenderNode RenderElement(double rootFontSize, IElement reference, StyleCollection collection, ICssStyleDeclaration? parent = null)
        //{
        //    var style = collection.ComputeCascadedStyle(reference);
        //    var computedStyle = Compute(rootFontSize, style, parent);
        //    if (parent != null)
        //    {
        //        computedStyle.UpdateDeclarations(parent);
        //    }
        //    var children = new List<IRenderNode>();

        //    foreach (var child in reference.ChildNodes)
        //    {
        //        if (child is IText text)
        //        {
        //            children.Add(RenderText(text));
        //        }
        //        else if (child is IElement element)
        //        {
        //            children.Add(RenderElement(rootFontSize, element, collection, computedStyle));
        //        }
        //    }

        //    // compute unitless line-height after rendering children
        //    if (computedStyle.GetProperty(PropertyNames.LineHeight).RawValue is CssLengthValue { Type: CssLengthValue.Unit.None } unitlessLineHeight)
        //    {
        //        var fontSize = computedStyle.GetProperty(PropertyNames.FontSize).RawValue is CssLengthValue { Type: CssLengthValue.Unit.Px } fontSizeLength ? fontSizeLength.Value : rootFontSize;
        //        var pixelValue = unitlessLineHeight.Value * fontSize;
        //        var computedLineHeight = new CssLengthValue(pixelValue, CssLengthValue.Unit.Px);

        //        // create a new property because SetProperty would change the parent value
        //        var lineHeightProperty = _context.CreateProperty(PropertyNames.LineHeight);
        //        lineHeightProperty.RawValue = computedLineHeight;
        //        computedStyle.SetDeclarations(new[] { lineHeightProperty });
        //    }

        //    var node = new ElementRenderNode(reference, children, style, computedStyle);

        //    foreach (var child in children)
        //    {
        //        if (child is ElementRenderNode elementChild)
        //        {
        //            elementChild.Parent = node;
        //        }
        //        else if (child is TextRenderNode textChild)
        //        {
        //            textChild.Parent = node;
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException();
        //        }
        //    }

        //    return node;
        //}

        //private IRenderNode RenderText(IText text) => new TextRenderNode(text);

        //private CssStyleDeclaration Compute(Double rootFontSize, ICssStyleDeclaration style, ICssStyleDeclaration? parentStyle)
        //{
        //    var computedStyle = new CssStyleDeclaration(_context);
        //    var parentFontSize = ((CssLengthValue?)parentStyle?.GetProperty(PropertyNames.FontSize)?.RawValue)?.ToPixel(_device) ?? rootFontSize;
        //    var fontSize = parentFontSize;
        //    // compute font-size first because other properties may depend on it
        //    if (style.GetProperty(PropertyNames.FontSize) is { RawValue: not null } fontSizeProperty)
        //    {
        //        fontSize = GetFontSizeInPixels(fontSizeProperty.RawValue);
        //    }
        //    var declarations = style.OfType<CssProperty>().Select(property =>
        //    {
        //        var name = property.Name;
        //        var value = property.RawValue;
        //        if (name == PropertyNames.FontSize)
        //        {
        //            // font-size was already computed
        //            value = new CssLengthValue(fontSize, CssLengthValue.Unit.Px);
        //        }
        //        else if (value is CssLengthValue { IsAbsolute: true, Type: not CssLengthValue.Unit.Px } absoluteLength)
        //        {
        //            value = new CssLengthValue(absoluteLength.ToPixel(_device), CssLengthValue.Unit.Px);
        //        }
        //        else if (value is CssLengthValue { Type: CssLengthValue.Unit.Percent } percentLength)
        //        {
        //            if (name == PropertyNames.VerticalAlign || name == PropertyNames.LineHeight)
        //            {
        //                var pixelValue = percentLength.Value / 100 * fontSize;
        //                value = new CssLengthValue(pixelValue, CssLengthValue.Unit.Px);
        //            }
        //            else
        //            {
        //                // TODO: compute for other properties that should be absolute
        //            }
        //        }
        //        else if (value is CssLengthValue { IsRelative: true, Type: not CssLengthValue.Unit.None } relativeLength)
        //        {
        //            var pixelValue = relativeLength.Type switch
        //            {
        //                CssLengthValue.Unit.Em => relativeLength.Value * fontSize,
        //                CssLengthValue.Unit.Rem => relativeLength.Value * rootFontSize,
        //                _ => relativeLength.ToPixel(_device),
        //            };
        //            value = new CssLengthValue(pixelValue, CssLengthValue.Unit.Px);
        //        }

        //        return new CssProperty(name, property.Converter, property.Flags, value, property.IsImportant);
        //    });

        //    computedStyle.SetDeclarations(declarations);

        //    return computedStyle;

        //    Double GetFontSizeInPixels(ICssValue value) => value switch
        //    {
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.XxSmall => 9D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.XSmall => 10D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.Small => 13D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.Medium => 16D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.Large => 18D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.XLarge => 24D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.XxLarge => 32D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.XxxLarge => 48D / 16 * rootFontSize,
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.Smaller => ComputeRelativeFontSize(constLength),
        //        CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.Larger => ComputeRelativeFontSize(constLength),
        //        CssLengthValue { Type: CssLengthValue.Unit.Px } length => length.Value,
        //        CssLengthValue { IsAbsolute: true } length => length.ToPixel(_device),
        //        CssLengthValue { Type: CssLengthValue.Unit.Vh or CssLengthValue.Unit.Vw or CssLengthValue.Unit.Vmax or CssLengthValue.Unit.Vmin } length => length.ToPixel(_device),
        //        CssLengthValue { IsRelative: true } length => ComputeRelativeFontSize(length),
        //        ICssSpecialValue specialValue when specialValue.CssText == CssKeywords.Inherit || specialValue.CssText == CssKeywords.Unset => parentFontSize,
        //        ICssSpecialValue specialValue when specialValue.CssText == CssKeywords.Initial => rootFontSize,
        //        _ => throw new InvalidOperationException("Font size must be a length"),
        //    };

        //    Double ComputeRelativeFontSize(ICssValue value)
        //    {
        //        var ancestorValue = parentStyle?.GetProperty(PropertyNames.FontSize)?.RawValue;
        //        var ancestorPixels = ancestorValue switch
        //        {
        //            CssLengthValue { IsAbsolute: true } ancestorLength => ancestorLength.ToPixel(_device),
        //            null => rootFontSize,
        //            _ => throw new InvalidOperationException(),
        //        };

        //        // set a minimum size of 9px for relative sizes
        //        return Math.Max(9, value switch
        //        {
        //            CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.Smaller => ancestorPixels / 1.2,
        //            CssConstantValue<CssLengthValue> constLength when constLength.CssText == CssKeywords.Larger => ancestorPixels * 1.2,
        //            CssLengthValue { Type: CssLengthValue.Unit.Rem } length => length.Value * rootFontSize,
        //            CssLengthValue { Type: CssLengthValue.Unit.Em } length => length.Value * ancestorPixels,
        //            CssLengthValue { Type: CssLengthValue.Unit.Percent } length => length.Value / 100 * ancestorPixels,
        //            _ => throw new InvalidOperationException(),
        //        });
        //    }
        //}

        #region Context

        sealed class CssComputeContext(IRenderDevice device, IBrowsingContext context, ICssProperties properties) : ICssComputeContext
        {
            private readonly IRenderDevice _device = device ?? new DefaultRenderDevice();
            private readonly IBrowsingContext _context = context;
            private readonly ICssProperties _properties = properties;

            public IRenderDevice Device => _device;

            public IBrowsingContext Context => _context;

            public IValueConverter? Converter => null;

            public ICssValue? Resolve(String name)
            {
                if (name.StartsWith("--"))
                {
                    var property = _properties.FirstOrDefault(m => m.Name.Equals(name, StringComparison.Ordinal));
                    return property?.RawValue;
                }

                return null;
            }
        }

        #endregion
    }
}
