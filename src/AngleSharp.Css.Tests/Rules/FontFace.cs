namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class FontFaceTests
    {
        [Test]
        public void FontFaceOpenSansWithSource()
        {
            var src = "@font-face{font-family:'Open Sans';src:url(fonts/OpenSans-Light.eot);src:local('Open Sans Light'),local('OpenSans-Light'),url(fonts/OpenSans-Light.ttf) format('truetype'),url(fonts/OpenSans-Light.woff) format('woff');font-style:normal}";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.That(fontface.Family, Is.EqualTo("\"Open Sans\""));
            Assert.That(fontface.Features, Is.EqualTo(""));
            Assert.That(fontface.Range, Is.EqualTo(""));
            Assert.AreNotEqual("", fontface.Source);
            Assert.That(fontface.Stretch, Is.EqualTo(""));
            Assert.That(fontface.Style, Is.EqualTo("normal"));
            Assert.That(fontface.Variant, Is.EqualTo(""));
            Assert.That(fontface.Weight, Is.EqualTo(""));
        }

        [Test]
        public void FontFaceOpenSansNoSource()
        {
            var src = "@font-face{font-family:'Open Sans';font-style:normal}";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.That(fontface.Family, Is.EqualTo("\"Open Sans\""));
            Assert.That(fontface.Features, Is.EqualTo(""));
            Assert.That(fontface.Range, Is.EqualTo(""));
            Assert.That(fontface.Source, Is.EqualTo(""));
            Assert.That(fontface.Stretch, Is.EqualTo(""));
            Assert.That(fontface.Style, Is.EqualTo("normal"));
            Assert.That(fontface.Variant, Is.EqualTo(""));
            Assert.That(fontface.Weight, Is.EqualTo(""));
        }
    }
}
