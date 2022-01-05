namespace AngleSharp.Css.Tests.Parsing
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class PseudoElements
    {
        [Test]
        public void VendorPrefixedPseudoElementShouldBeAvailable_Issue98()
        {
            var html = @"<html><head><style>
    p::-webkit-scrollbar-thumb {
        background: #888
    }
    </style></head><body><p>test</p></body></html>";
            var parser = new HtmlParser(new HtmlParserOptions(), BrowsingContext.New(new Configuration().WithCss(new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidSelectors = true,
            })));
            var document = parser.ParseDocument(html);
            var stylesheet = document.StyleSheets.OfType<ICssStyleSheet>().First();
            var rule = stylesheet.Rules.OfType<ICssStyleRule>().First();
            var selectorText = rule.SelectorText;
            Assert.AreEqual("p::-webkit-scrollbar-thumb", selectorText);
        }
    }
}
