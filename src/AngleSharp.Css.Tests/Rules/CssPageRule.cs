namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class CssPageRuleTests
    {
        [Test]
        public void SemiColonsShouldNotMissInPageRule_Issue135()
        {
            var html = "<html><body><style>@page { margin-top: 25mm; margin-right: 25mm }</style></body></html>";
            var document = html.ToHtmlDocument(Configuration.Default.WithCss());
            var styleSheet = document.StyleSheets.OfType<ICssStyleSheet>().First();
            var css = styleSheet.ToCss();

            Assert.AreEqual("@page { margin-top: 25mm; margin-right: 25mm }", css);
        }

        [Test]
        public void PageRuleShouldRecombineShorthandDeclaration_Issue135()
        {
            var html = "<html><body><style>@page { margin: 25mm }</style></body></html>";
            var document = html.ToHtmlDocument(Configuration.Default.WithCss());
            var styleSheet = document.StyleSheets.OfType<ICssStyleSheet>().First();
            var css = styleSheet.ToCss();

            Assert.AreEqual("@page { margin: 25mm }", css);
        }
    }
}
