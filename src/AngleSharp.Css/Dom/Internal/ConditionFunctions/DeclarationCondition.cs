namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using System;
    using System.IO;

    sealed class DeclarationCondition : IConditionFunction
    {
        private readonly String _name;
        private readonly String _value;
        private readonly IBrowsingContext _context;

        public DeclarationCondition(IBrowsingContext context, String name, String value)
        {
            _context = context;
            _name = name;
            _value = value;
        }

        public Boolean Check(IRenderDevice device)
        {
            var factory = _context?.GetService<IDeclarationFactory>() ?? Factory.Declaration;
            var info = factory?.Create(_name);

            if (info != null && !Object.Equals(info.Converter, ValueConverters.Any))
            {
                var normedValue = Normalize(_value);
                var result = info.Converter.Convert(normedValue);
                return result != null;
            }

            return false;
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter) =>
            writer.Write(formatter.Declaration(_name, _value, false));

        private static String Normalize(String value)
        {
            var keyword = CssKeywords.BangImportant;

            if (value.EndsWith(keyword))
            {
                return value.Remove(value.Length - keyword.Length).Trim();
            }

            return value;
        }
    }
}
