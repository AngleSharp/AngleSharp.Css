namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssContentVisibilityPropertyTests
    {
        [Test]
        public void CssContentVisibilityIsHidden()
        {
            var source = "a{content-visibility:hidden}";
            var parsed = ParseStyle(source);
            Assert.That(parsed.Style.CssText, Is.EqualTo("content-visibility: hidden"));
        }

        [Test]
        public void CssContentVisibilityIsUnkown()
        {
            var source = "a{content-visibility:aa}";
            var parsed = ParseStyle(source);
            Assert.That(parsed.Style.CssText, Is.EqualTo(""));
        }
    }
}
