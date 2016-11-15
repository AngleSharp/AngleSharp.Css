namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using System;
    using System.IO;

    static class CssConstructionFunctions
    {
        internal static IHtmlDocument ParseDocument(String source)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var parser = new HtmlParser(context);
            return parser.ParseDocument(source);
        }

        internal static IHtmlDocument ParseDocument(String source, CssParserOptions options)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss(options));
            var parser = new HtmlParser(context);
            return parser.ParseDocument(source);
        }

        internal static CssStyleRule ParseStyle(String source)
        {
            var parser = new CssParser();
            return parser.ParseStyleSheet(source).Rules[0] as CssStyleRule;
        }

        internal static IFeatureValidator CreateMediaFeatureValidator(String name)
        {
            var factory = new DefaultFeatureValidatorFactory();
            return factory.Create(name);
        }

        internal static CssStyleSheet ParseStyleSheet(String source)
        {
            var parser = new CssParser();
            return parser.ParseStyleSheet(source) as CssStyleSheet;
        }

        internal static CssStyleSheet ParseStyleSheet(Stream source)
        {
            var parser = new CssParser();
            return parser.ParseStyleSheet(source) as CssStyleSheet;
        }

        internal static CssStyleSheet ParseStyleSheet(String source, CssParserOptions options)
        {
            var parser = new CssParser(options);
            return parser.ParseStyleSheet(source) as CssStyleSheet;
        }

        internal static CssRule ParseRule(String source)
        {
            var parser = new CssParser();
            var sheet = parser.ParseStyleSheet(String.Empty);
            return parser.ParseRule(sheet, source) as CssRule;
        }

        internal static CssProperty ParseDeclaration(String source)
        {
            var parser = new CssParser();
            return parser.ParseProperty(source) as CssProperty;
        }

        internal static CssProperty ParseDeclaration(String source, CssParserOptions options)
        {
            var parser = new CssParser(options);
            return parser.ParseProperty(source) as CssProperty;
        }

        internal static CssStyleDeclaration ParseDeclarations(String declarations)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var parser = new CssParser(context);
            var style = new CssStyleDeclaration(context);
            style.Update(declarations);
            return style;
        }

        internal static CssKeyframeRule ParseKeyframeRule(String source)
        {
            var parser = new CssParser();
            var sheet = parser.ParseStyleSheet(String.Empty);
            return parser.ParseKeyframeRule(sheet, source) as CssKeyframeRule;
        }

        internal static CssImportRule ParseImportRule(String source)
        {
            ICssParser parser = new CssParser();
            var sheet = parser.ParseStyleSheet(String.Empty);
            return parser.ParseRule(sheet, source) as CssImportRule;
        }
    }
}
