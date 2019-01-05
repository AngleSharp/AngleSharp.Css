namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    sealed class StandardValueConverter<T> : IValueConverter
    {
        private readonly T _defaultValue;

        public StandardValueConverter(T defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public ICssValue Convert(StringSource source)
        {
            var ident = source.ParseIdent();

            if (ident.Isi(CssKeywords.Initial))
            {
                return new Initial<T>(_defaultValue);
            }
            else if (ident.Isi(CssKeywords.Inherit))
            {
                return Inherit.Instance;
            }
            else if (ident.Isi(CssKeywords.Unset))
            {
                return new Unset<T>(_defaultValue);
            }
            else if (ident.Isi(FunctionNames.Var) && source.Current == Symbols.RoundBracketOpen)
            {
                source.SkipCurrentAndSpaces();
                return source.ParseVar();
            }
            
            return null;
        }
    }
}
