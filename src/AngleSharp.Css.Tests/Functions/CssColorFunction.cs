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
    }
}

