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
            Assert.That(property.Name, Is.EqualTo("fill"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(175, 170, 150, 1)"));
        }

        [Test]
        public void CssFillNoneKeywordLegal()
        {
            var snippet = "fill:none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("fill"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }
    }
}
