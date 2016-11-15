namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class DeclarationCondition : IConditionFunction
    {
        private readonly ICssProperty _property;
        private readonly String _value;

        public DeclarationCondition(ICssProperty property, String value)
        {
            _property = property;
            _value = value;
        }

        public Boolean Check()
        {
            if (_property is CssUnknownProperty == false)
            {
                _property.Value = _value;
                return _property.Value.Is(_value);
            }

            return false;
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write("(");
            writer.Write(formatter.Declaration(_property.Name, _value, _property.IsImportant));
            writer.Write(")");
        }
    }
}
