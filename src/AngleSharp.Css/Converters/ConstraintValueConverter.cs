namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class ConstraintValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;
        private readonly String[] _labels;

        public ConstraintValueConverter(IValueConverter converter, String[] labels)
        {
            _converter = converter;
            _labels = labels;
        }

        public ICssValue Convert(StringSource source)
        {
            var result = _converter.Convert(source);
            return result != null ? new TransformationValueConverter(result, _labels) : null;
        }

        private sealed class TransformationValueConverter : ICssValue
        {
            private readonly ICssValue _data;
            private readonly String[] _labels;

            public TransformationValueConverter(ICssValue data, String[] labels)
            {
                _data = data;
                _labels = labels;
            }

            public String CssText
            {
                get { return _data.CssText; }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                _data.ToCss(writer, formatter);
            }
        }
    }
}
