namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssFillPropertyTests
    {
        [Test]
        public void CssFillColorFromHexLegal()
        {
            var snippet = "fill:#AFAA96";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("fill", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(175, 170, 150, 1)", property.Value);
        }

        [Test]
        public void CssFillNoneKeywordLegal()
        {
            var snippet = "fill:none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("fill", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }
    }
}
