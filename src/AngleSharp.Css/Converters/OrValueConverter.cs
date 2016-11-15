namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;

    sealed class OrValueConverter : IValueConverter
    {
        private readonly IValueConverter _previous;
        private readonly IValueConverter _next;

        public OrValueConverter(IValueConverter previous, IValueConverter next)
        {
            _previous = previous;
            _next = next;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var result = _previous.Convert(source);

            if (result == null)
            {
                source.BackTo(start);
                result = _next.Convert(source);
            }

            return result;
        }
    }
}
