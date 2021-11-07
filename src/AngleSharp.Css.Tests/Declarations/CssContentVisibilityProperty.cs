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
            Assert.AreEqual("content-visibility: hidden", parsed.Style.CssText);
        }

        [Test]
        public void CssContentVisibilityIsUnkown()
        {
            var source = "a{content-visibility:aa}";
            var parsed = ParseStyle(source);
            Assert.AreEqual("", parsed.Style.CssText);
        }
    }
}
