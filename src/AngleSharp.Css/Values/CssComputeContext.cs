namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Linq;

    sealed class CssComputeContext : ICssComputeContext
    {
        private readonly IRenderDevice _device;
        private readonly IBrowsingContext? _context;
        private readonly CssCustomPropertyResolver _variables;
        private readonly ICssProperties? _parent;

        public CssComputeContext(IRenderDevice device, IBrowsingContext? context, ICssProperties properties, ICssProperties? parent = null)
        {
            _device = device ?? new DefaultRenderDevice();
            _context = context;
            _variables = new CssCustomPropertyResolver(properties, parent);
            _parent = parent;
        }

        public IRenderDevice Device => _device;

        public IBrowsingContext? Context => _context;

        public IValueConverter? Converter => null;

        public ICssValue? Resolve(String name) => _variables.Resolve(name);

        internal ICssValue? InheritedValue(String name) => _parent?.FirstOrDefault(m => m.Name == name)?.RawValue;
    }

}
