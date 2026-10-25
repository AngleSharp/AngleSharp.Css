namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Linq;

    sealed class CssComputeContext : ICssComputeContext, ILocalComputeContext, IInheritingComputeContext, IFontComputeContext
    {
        private readonly IRenderDevice _device;
        private readonly IBrowsingContext? _context;
        private readonly CssCustomPropertyResolver _variables;
        private readonly ICssProperties? _parent;
        private Double? _fontSize;

        public CssComputeContext(IRenderDevice device, IBrowsingContext? context, ICssProperties properties, ICssProperties? parent = null)
        {
            _device = device ?? new DefaultRenderDevice();
            _context = context;
            _variables = new CssCustomPropertyResolver(properties, parent);
            _parent = parent;
            Properties = properties;
        }

        public ICssProperties Properties { get; }

        public IRenderDevice Device => _device;

        public IBrowsingContext? Context => _context;

        public IValueConverter? Converter => null;

        public ICssValue? Resolve(String name) => _variables.Resolve(name);

        public ICssValue? InheritedValue(String name) => _parent?.FirstOrDefault(m => m.Name == name)?.RawValue;

        public Double ParentFontSize => ToPixels(InheritedValue(PropertyNames.FontSize)) ?? DefaultFontSize;

        public Double FontSize
        {
            get
            {
                if (!_fontSize.HasValue)
                {
                    // The font size of the element is computed against the font size of the parent.
                    var declared = Properties.FirstOrDefault(m => m.Name == PropertyNames.FontSize);
                    var computed = declared?.Compute(this).RawValue;
                    _fontSize = computed is CssInitialValue ? DefaultFontSize : ToPixels(computed) ?? ParentFontSize;
                }

                return _fontSize.Value;
            }
        }

        public Double RootFontSize => _parent is null ? FontSize : (_parent as CssStyleDeclaration)?.RootFontSize ?? DefaultFontSize;

        // rem in the font-size of the root refers to the initial font size (CSS Values 4, 6.1.1)
        public Double FontSizeRemBase => _parent is null ? DefaultFontSize : RootFontSize;

        private Double DefaultFontSize => _device.FontSize > 0 ? _device.FontSize : 16.0;

        private static Double? ToPixels(ICssValue? value) =>
            value is CssLengthValue length && length.Type == CssLengthValue.Unit.Px ? length.Value : null;
    }

}
