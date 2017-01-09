namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using System;
    using System.IO;

    sealed class DeclarationCondition : IConditionFunction
    {
        private readonly String _name;
        private readonly String _value;

        public DeclarationCondition(String name, String value)
        {
            _name = name;
            _value = value;
        }

        public Boolean Check(IRenderDevice device)
        {
            var factory = device?.Context?.GetService<IConverterFactory>() ?? Factory.Converter;
            var converter = factory?.Create(_name);

            if (converter != null && !Object.Equals(converter, ValueConverters.Any))
            {
                var normedValue = Normalize(_value);
                var result = converter.Convert(normedValue);
                return result != null;
            }

            return false;
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Declaration(_name, _value, false));
        }

        private static String Normalize(String value)
        {
            var important = "!important";

            if (value.EndsWith(important))
            {
                return value.Remove(value.Length - important.Length).Trim();
            }

            return value;
        }
    }
}
