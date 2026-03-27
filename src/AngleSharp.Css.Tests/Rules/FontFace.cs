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
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("\"Open Sans\"", fontface.Family);
            Assert.AreEqual("", fontface.Features);
            Assert.AreEqual("", fontface.Range);
            Assert.AreNotEqual("", fontface.Source);
            Assert.AreEqual("", fontface.Stretch);
            Assert.AreEqual("normal", fontface.Style);
            Assert.AreEqual("", fontface.Variant);
            Assert.AreEqual("", fontface.Weight);
        }

        [Test]
        public void FontFaceOpenSansNoSource()
        {
            var src = "@font-face{font-family:'Open Sans';font-style:normal}";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("\"Open Sans\"", fontface.Family);
            Assert.AreEqual("", fontface.Features);
            Assert.AreEqual("", fontface.Range);
            Assert.AreEqual("", fontface.Source);
            Assert.AreEqual("", fontface.Stretch);
            Assert.AreEqual("normal", fontface.Style);
            Assert.AreEqual("", fontface.Variant);
            Assert.AreEqual("", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightRangeShouldBePreserved()
        {
            var src = "@font-face { font-weight: 100 700; }";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("100 700", fontface.Weight);
        }

        [Test]
        public void FontFaceSingleWeightIntegerShouldBePreserved()
        {
            var src = "@font-face { font-weight: 400; }";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("400", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightRangeShouldRoundtripViaToCss()
        {
            var src = "@font-face { font-weight: 100 700; }";
            var sheet = ParseStyleSheet(src);
            var css = sheet.ToCss();
            Assert.That(css, Does.Contain("font-weight: 100 700"));
        }

        [Test]
        public void FontFaceWeightBoldKeywordShouldBePreserved()
        {
            var src = "@font-face { font-weight: bold; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("bold", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightNormalKeywordShouldBePreserved()
        {
            var src = "@font-face { font-weight: normal; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("normal", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightMinBoundary100ShouldBePreserved()
        {
            var src = "@font-face { font-weight: 100; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("100", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightMaxBoundary900ShouldBePreserved()
        {
            var src = "@font-face { font-weight: 900; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("900", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightOutOfRange1ShouldBeInvalid()
        {
            var src = "@font-face { font-weight: 1; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightOutOfRange1000ShouldBeInvalid()
        {
            var src = "@font-face { font-weight: 1000; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("", fontface.Weight);
        }

        [Test]
        public void FontFaceWeightThreeValuesShouldBeInvalid()
        {
            var src = "@font-face { font-weight: 100 400 700; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("", fontface.Weight);
        }
    }
}
