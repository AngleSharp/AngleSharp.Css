namespace AngleSharp.Css.Tests.Rules
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssImportRuleTests
    {
        [Test]
        public void CssImportWithNonQuotedUrl()
        {
            var source = "@import url(button.css);";
            var rule = ParseImportRule(source);
            Assert.AreEqual("button.css", rule.Href);
            Assert.AreEqual("", rule.Media.MediaText);
        }

        [Test]
        public void CssImportWithDoubleQuotedUrl()
        {
            var source = "@import url(\"button.css\");";
            var rule = ParseImportRule(source);
            Assert.AreEqual("button.css", rule.Href);
            Assert.AreEqual("", rule.Media.MediaText);
        }

        [Test]
        public void CssImportWithSingleQuotedUrl()
        {
            var source = "@import url('button.css');";
            var rule = ParseImportRule(source);
            Assert.AreEqual("button.css", rule.Href);
            Assert.AreEqual("", rule.Media.MediaText);
        }

        [Test]
        public void CssImportWithDoubleQuotedStringAsUrl()
        {
            var source = "@import \"button.css\";";
            var rule = ParseImportRule(source);
            Assert.AreEqual("button.css", rule.Href);
            Assert.AreEqual("", rule.Media.MediaText);
        }

        [Test]
        public void CssImportWithSingleQuotedStringAsUrl()
        {
            var source = "@import 'button.css';";
            var rule = ParseImportRule(source);
            Assert.AreEqual("button.css", rule.Href);
            Assert.AreEqual("", rule.Media.MediaText);
        }

        [Test]
        public void CssImportWithUrlAndAllMedia()
        {
            var media = "all";
            var source = "@import url(size/medium.css) " + media + ";";
            var rule = ParseImportRule(source);
            Assert.AreEqual("size/medium.css", rule.Href);
            Assert.AreEqual(media, rule.Media.MediaText);
            Assert.AreEqual(1, rule.Media.Length);
        }

        [Test]
        public void CssImportWithUrlAndComplicatedMedia()
        {
            var media = "screen and (color), projection and (min-color: 256)";
            var source = "@import url(old.css) " + media + ";";
            var rule = ParseImportRule(source);
            Assert.AreEqual("old.css", rule.Href);
            Assert.AreEqual(media, rule.Media.MediaText);
            Assert.AreEqual(2, rule.Media.Length);
        }
    }
}
