namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Linq;

    sealed class CssComputeContext : ICssComputeContext
    {
        private readonly IRenderDevice _device;
        private readonly IBrowsingContext? _context;
        private readonly ICssProperties _properties;

        public CssComputeContext(IRenderDevice device, IBrowsingContext? context, ICssProperties properties)
        {
            _device = device ?? new DefaultRenderDevice();
            _context = context;
            _properties = properties;
        }

        public IRenderDevice Device => _device;

        public IBrowsingContext? Context => _context;

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

}
