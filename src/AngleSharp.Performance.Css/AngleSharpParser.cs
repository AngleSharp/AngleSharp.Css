namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Css.Parser;
    using System;

    class AngleSharpParser : ITestee
    {
        private static readonly CssParserOptions options = new CssParserOptions
        {
            IsIncludingUnknownDeclarations = true,
            IsIncludingUnknownRules = true,
            IsToleratingInvalidSelectors = true
        };
        private static readonly CssParser parser = new CssParser(options);

        public String Name
        {
            get { return "AngleSharp"; }
        }

        public Type Library
        {
            get { return typeof(BrowsingContext); }
        }

        public void Run(String source)
        {
            parser.ParseStyleSheet(source);
        }
    }
}
