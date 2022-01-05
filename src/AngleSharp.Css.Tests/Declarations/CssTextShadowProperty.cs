namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    [TestFixture]
    public class CssTextShadowProperty
    {
        [Test]
        public void ColorNotIncluded_Issue97()
        {
            var html = @"<div style=""text-shadow: 2px 2px 2px #000"">test</div>";
            var parser = new HtmlParser(new HtmlParserOptions(), BrowsingContext.New(Configuration.Default.WithCss(new CssParserOptions())));
            var dom = parser.ParseDocument(html);
            var div = dom.QuerySelector("div");
            var style = div.GetStyle();
            var css = style.CssText;
            Assert.AreEqual("text-shadow: 2px 2px 2px rgba(0, 0, 0, 1)", css);
        }
    }
}
