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
            Assert.That(parsed.Style.GetContent(), Is.EqualTo("\"\\\"\""));
        }

        [Test]
        public void CssContentParseStringWithSingleQuoteEscape()
        {
            var source = "a{content:'\\''}";
            var parsed = ParseStyle(source);
            Assert.That(parsed.Style.GetContent(), Is.EqualTo("\"'\""));
        }

        [Test]
        public void CssContentParseStringWithDoubleQuoteMultipleEscapes()
        {
            var source = "a{content:\"abc\\\"\\\"d\\\"ef\"}";
            var parsed = ParseStyle(source);
            Assert.That(parsed.Style.GetContent(), Is.EqualTo("\"abc\\\"\\\"d\\\"ef\""));
        }

        [Test]
        public void CssContentParseStringWithSingleQuoteMultipleEscapes()
        {
            var source = "a{content:'abc\\'\\'d\\'ef'}";
            var parsed = ParseStyle(source);
            Assert.That(parsed.Style.GetContent(), Is.EqualTo("\"abc''d'ef\""));
        }

        [Test]
        public void CssContentWithCounter_Issue58()
        {
            var source = "a{content: counter(h1) \".\\00A0\"}";
            var parsed = ParseStyle(source);
            Assert.That(parsed.Style.GetContent(), Is.EqualTo("counter(h1) \". \""));
        }
    }
}
