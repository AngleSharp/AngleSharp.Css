namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Dom;
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
            Assert.That(rule.Href, Is.EqualTo("button.css"));
            Assert.That(rule.Media.MediaText, Is.EqualTo(""));
        }

        [Test]
        public void CssImportWithDoubleQuotedUrl()
        {
            var source = "@import url(\"button.css\");";
            var rule = ParseImportRule(source);
            Assert.That(rule.Href, Is.EqualTo("button.css"));
            Assert.That(rule.Media.MediaText, Is.EqualTo(""));
        }

        [Test]
        public void CssImportWithSingleQuotedUrl()
        {
            var source = "@import url('button.css');";
            var rule = ParseImportRule(source);
            Assert.That(rule.Href, Is.EqualTo("button.css"));
            Assert.That(rule.Media.MediaText, Is.EqualTo(""));
        }

        [Test]
        public void CssImportWithDoubleQuotedStringAsUrl()
        {
            var source = "@import \"button.css\";";
            var rule = ParseImportRule(source);
            Assert.That(rule.Href, Is.EqualTo("button.css"));
            Assert.That(rule.Media.MediaText, Is.EqualTo(""));
        }

        [Test]
        public void CssImportWithSingleQuotedStringAsUrl()
        {
            var source = "@import 'button.css';";
            var rule = ParseImportRule(source);
            Assert.That(rule.Href, Is.EqualTo("button.css"));
            Assert.That(rule.Media.MediaText, Is.EqualTo(""));
        }

        [Test]
        public void CssImportWithUrlAndAllMedia()
        {
            var media = "all";
            var source = "@import url(size/medium.css) " + media + ";";
            var rule = ParseImportRule(source);
            Assert.That(rule.Href, Is.EqualTo("size/medium.css"));
            Assert.That(rule.Media.MediaText, Is.EqualTo(media));
            Assert.That(rule.Media.Length, Is.EqualTo(1));
        }

        [Test]
        public void CssImportWithUrlAndComplicatedMedia()
        {
            var media = "screen and (color), projection and (min-color: 256)";
            var source = "@import url(old.css) " + media + ";";
            var rule = ParseImportRule(source);
            Assert.That(rule.Href, Is.EqualTo("old.css"));
            Assert.That(rule.Media.MediaText, Is.EqualTo(media));
            Assert.That(rule.Media.Length, Is.EqualTo(2));
        }
    }
}
