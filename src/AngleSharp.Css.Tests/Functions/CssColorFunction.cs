namespace AngleSharp.Css.Tests.Functions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssColorFunctionTests
    {
        [Test]
        public void ColorNotParsedCorrectly_Issue109()
        {
            var html = @"<p style='color: RGB(0,17,0)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(0, 17, 0, 1)", color);
        }

        [Test]
        public void ParseRgbWithSpacesL4_Issue131()
        {
            var html = @"<p style='color: rgb(255 122 127 / 80%)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(255, 122, 127, 0.8)", color);
        }

        [Test]
        public void ParseRgbWithSpacesInPercentL4_Issue131()
        {
            var html = @"<p style='color: rgb(100% 10% 50% / 0.7)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(255, 26, 128, 0.7)", color);
        }

        [Test]
        public void ParseRgbWithSpacesAndNoneL4_Issue131()
        {
            var html = @"<p style='color: rgb(100% none 50% / 35%)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(255, 0, 128, 0.35)", color);
        }

        [Test]
        public void ParseOklabToRgb_First()
        {
            var html = @"<p style='color: oklab(40.1% 0.1143 0.045)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(125, 35, 40, 1)", color);
        }

        [Test]
        public void ParseOklabToRgb_Second()
        {
            var html = @"<p style='color: oklab(59.69% 0.1007 0.1191);'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 7, 1)", color);
        }

        [Test]
        public void ParseOklabToRgb_Alpha()
        {
            var html = @"<p style='color: oklab(59.69% 0.1007 0.1191 / 0.5);'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 7, 0.5)", color);
        }

        [Test]
        public void ParseOklchToRgb_First()
        {
            var html = @"<p style='color: oklch(40.1% 0.123 21.57)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(125, 35, 40, 1)", color);
        }

        [Test]
        public void ParseOklchToRgb_Second()
        {
            var html = @"<p style='color: oklch(59.69% 0.156 49.77)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 7, 1)", color);
        }

        [Test]
        public void ParseOklchToRgb_Alpha()
        {
            var html = @"<p style='color: oklch(59.69% 0.156 49.77 / .5)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 7, 0.5)", color);
        }

        [Test]
        public void ParseLabToRgb_First()
        {
            var html = @"<p style='color: lab(29.2345% 39.3825 20.0664);'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(125, 35, 40, 1)", color);
        }

        [Test]
        public void ParseLabToRgb_Second()
        {
            var html = @"<p style='color: lab(52.2345% 40.1645 59.9971);'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 6, 1)", color);
        }

        [Test]
        public void ParseLabToRgb_Alpha()
        {
            var html = @"<p style='color: lab(52.2345% 40.1645 59.9971 / .5);'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 6, 0.5)", color);
        }

        [Test]
        public void ParseLchToRgb_First()
        {
            var html = @"<p style='color: lch(29.2345% 44.2 27);'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(125, 35, 40, 1)", color);
        }

        [Test]
        public void ParseLchToRgb_Second()
        {
            var html = @"<p style='color: lch(52.2345% 72.2 56.2)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 6, 1)", color);
        }

        [Test]
        public void ParseLchToRgb_Alpha()
        {
            var html = @"<p style='color: lch(52.2345% 72.2 56.2 / .5)'>Text</p>";
            var dom = ParseDocument(html);
            var p = dom.QuerySelector("p");
            var s = p.GetStyle();
            var color = s.GetColor();
            Assert.AreEqual("rgba(198, 93, 6, 0.5)", color);
        }
    }
}

