namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class ContinuousValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public ContinuousValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var options = new List<ICssValue>();

            while (!source.IsDone)
            {
                var option = _converter.Convert(source);

                if (option == null)
                {
                    return null;
                }

                source.SkipSpaces();
                options.Add(option);
            }
            
            return new OptionsValue(source.Substring(start), options.ToArray());
        }

        private sealed class OptionsValue : BaseValue
        {
            private readonly ICssValue[] _options;

            public OptionsValue(String value, ICssValue[] options)
                : base(value)
            {
                _options = options;
            }
        }
    }
}
