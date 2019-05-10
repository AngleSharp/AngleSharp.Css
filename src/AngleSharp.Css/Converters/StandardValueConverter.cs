namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    sealed class StandardValueConverter : IValueConverter
    {
        private readonly ICssValue _defaultValue;

        public StandardValueConverter(ICssValue defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public ICssValue Convert(StringSource source)
        {
            var ident = source.ParseIdent();

            if (ident.Isi(CssKeywords.Initial))
            {
                return new CssInitialValue(_defaultValue);
            }
            else if (ident.Isi(CssKeywords.Inherit))
            {
                return CssInheritValue.Instance;
            }
            else if (ident.Isi(CssKeywords.Unset))
            {
                return new CssUnsetValue(_defaultValue);
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
