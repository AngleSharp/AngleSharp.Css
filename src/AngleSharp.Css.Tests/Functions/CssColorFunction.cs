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
    }
}

