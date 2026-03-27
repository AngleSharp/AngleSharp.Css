namespace AngleSharp.Css.Tests.Parsing
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class LargeValues
    {
        [Test]
        public void LargePositiveZIndexShouldNotErrorAndBeCapped()
        {
            var source = @"div#preview-toolbar {
    z-index: 9999999999999
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.That(style.Style.GetZIndex(), Is.EqualTo("2147483647"));
        }

        [Test]
        public void LargeNegativeZIndexShouldNotErrorAndBeCapped()
        {
            var source = @"div#preview-toolbar {
    z-index: -9999999999999
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.That(style.Style.GetZIndex(), Is.EqualTo("-2147483648"));
        }

        [Test]
        public void SilentlyIgnoresErrorsWhenInvokedWithoutCss_Issue15()
        {
            var p = new HtmlParser();
            var dom = p.ParseDocument(@"<html><body><div style=""color: black"">Test</div></body></html>");
            var div = dom.QuerySelector("div");
            var style = div.GetStyle();
            Assert.IsNull(style);
        }
    }
}
