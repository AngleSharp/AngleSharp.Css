namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssContentPropertyTests
    {
        [Test]
        public void CssContentParseStringWithDoubleQuoteEscape()
        {
            var source = "a{content:\"\\\"\"}";
            var parsed = ParseStyle(source);
            Assert.AreEqual("\"\\\"\"", parsed.Style.GetContent());
        }

        [Test]
        public void CssContentParseStringWithSingleQuoteEscape()
        {
            var source = "a{content:'\\''}";
            var parsed = ParseStyle(source);
            Assert.AreEqual("\"'\"", parsed.Style.GetContent());
        }

        [Test]
        public void CssContentParseStringWithDoubleQuoteMultipleEscapes()
        {
            var source = "a{content:\"abc\\\"\\\"d\\\"ef\"}";
            var parsed = ParseStyle(source);
            Assert.AreEqual("\"abc\\\"\\\"d\\\"ef\"", parsed.Style.GetContent());
        }

        [Test]
        public void CssContentParseStringWithSingleQuoteMultipleEscapes()
        {
            var source = "a{content:'abc\\'\\'d\\'ef'}";
            var parsed = ParseStyle(source);
            Assert.AreEqual("\"abc''d'ef\"", parsed.Style.GetContent());
        }
    }
}
