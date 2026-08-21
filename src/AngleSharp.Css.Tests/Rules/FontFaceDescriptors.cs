namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;

    /// <summary>
    /// Descriptors inside @font-face used to be limited to a hardcoded set of seven,
    /// with everything else dropped silently and without regard to the parser options.
    /// These cases pin down that the standard CSS Fonts Level 4 descriptors survive by
    /// default and that anything else survives when unknown declarations are included.
    /// </summary>
    [TestFixture]
    public class FontFaceDescriptorTests
    {
        private static readonly CssParserOptions IncludingUnknown = new() { IsIncludingUnknownDeclarations = true };

        [Test]
        public void FontFaceKeepsFontDisplay()
        {
            var sheet = ParseStyleSheet("@font-face { font-display: swap }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("swap", fontface.GetPropertyValue(PropertyNames.FontDisplay));
        }

        [Test]
        public void FontFaceKeepsSizeAdjust()
        {
            var sheet = ParseStyleSheet("@font-face { size-adjust: 90% }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("90%", fontface.GetPropertyValue(PropertyNames.SizeAdjust));
        }

        [TestCase("ascent-override", "90%")]
        [TestCase("descent-override", "20%")]
        [TestCase("line-gap-override", "0%")]
        [TestCase("ascent-override", "normal")]
        public void FontFaceKeepsMetricOverrides(string name, string value)
        {
            var sheet = ParseStyleSheet($"@font-face {{ {name}: {value} }}");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual(value, fontface.GetPropertyValue(name));
        }

        [Test]
        public void FontFaceKeepsFontFeatureSettings()
        {
            var sheet = ParseStyleSheet("@font-face { font-feature-settings: \"liga\" 1, \"kern\" on, \"smcp\" }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("\"liga\" 1, \"kern\" on, \"smcp\"", fontface.GetPropertyValue(PropertyNames.FontFeatureSettings));
        }

        [Test]
        public void FontFaceKeepsFontVariationSettings()
        {
            var sheet = ParseStyleSheet("@font-face { font-variation-settings: \"wght\" 400, \"slnt\" -10 }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("\"wght\" 400, \"slnt\" -10", fontface.GetPropertyValue(PropertyNames.FontVariationSettings));
        }

        [Test]
        public void FontFaceFeaturesMapToFontFeatureSettings()
        {
            var sheet = ParseStyleSheet("@font-face { font-feature-settings: \"liga\" 1 }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("\"liga\" 1", fontface.Features);

            fontface.Features = "\"kern\" off";
            Assert.AreEqual("\"kern\" off", fontface.GetPropertyValue(PropertyNames.FontFeatureSettings));
        }

        [Test]
        public void FontFaceStandardDescriptorsRoundtripViaToCss()
        {
            var src = "@font-face { font-family: \"FontName\"; src: url(\"https://example.com/font.woff\") format(\"woff\"); font-display: swap; size-adjust: 100% }";
            var sheet = ParseStyleSheet(src);
            var css = sheet.ToCss();
            Assert.That(css, Does.Contain("font-display: swap"));
            Assert.That(css, Does.Contain("size-adjust: 100%"));
        }

        [Test]
        public void FontFaceDropsVendorDescriptorByDefault()
        {
            var sheet = ParseStyleSheet("@font-face { mso-generic-font-family: auto }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual(0, fontface.Length);
        }

        [Test]
        public void FontFaceKeepsVendorDescriptorWhenIncludingUnknownDeclarations()
        {
            var sheet = ParseStyleSheet("@font-face { mso-generic-font-family: auto }", IncludingUnknown);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("auto", fontface.GetPropertyValue("mso-generic-font-family"));
            Assert.That(sheet.ToCss(), Does.Contain("mso-generic-font-family: auto"));
        }

        [Test]
        public void FontFaceKeepsCustomPropertyWhenIncludingUnknownDeclarations()
        {
            var sheet = ParseStyleSheet("@font-face { --custom-thing: 12px }", IncludingUnknown);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("12px", fontface.GetPropertyValue("--custom-thing"));
        }

        [Test]
        public void FontFaceIgnoresInvalidDescriptorValue()
        {
            var sheet = ParseStyleSheet("@font-face { size-adjust: 10px; ascent-override: bogus }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual(0, fontface.Length);
            Assert.That(sheet.ToCss(), Does.Not.Contain("size-adjust"));
        }

        [Test]
        public void FontFaceInvalidValueDoesNotOverwriteValidOne()
        {
            var sheet = ParseStyleSheet("@font-face { font-weight: 400; font-weight: bogus }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("400", fontface.Weight);
        }

        [Test]
        public void FontFaceValidValueOverwritesEarlierOne()
        {
            var sheet = ParseStyleSheet("@font-face { font-weight: 400; font-weight: 700 }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("700", fontface.Weight);
            Assert.AreEqual(1, fontface.Length);
        }

        [Test]
        public void CounterStyleKeepsDescriptorsWhenIncludingUnknownDeclarations()
        {
            var sheet = ParseStyleSheet("@counter-style thumbs { system: cyclic; symbols: \"X\" }", IncludingUnknown);
            var rule = (ICssProperties)sheet.Rules[0];
            Assert.AreEqual("cyclic", rule.GetPropertyValue("system"));
            Assert.AreEqual("\"X\"", rule.GetPropertyValue("symbols"));
        }

        [Test]
        public void ViewportKeepsUnknownDescriptorWhenIncludingUnknownDeclarations()
        {
            var sheet = ParseStyleSheet("@viewport { width: 100px; foo: bar }", IncludingUnknown);
            var rule = (ICssProperties)sheet.Rules[0];
            Assert.AreEqual("bar", rule.GetPropertyValue("foo"));
            Assert.AreEqual(2, rule.Length);
        }

        [Test]
        public void FontFaceDescriptorsAreEnumerable()
        {
            var sheet = ParseStyleSheet("@font-face { font-family: \"X\"; font-display: swap; size-adjust: 50% }");
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            var names = fontface.Select(m => m.Name).ToArray();
            CollectionAssert.AreEquivalent(new[] { "font-family", "font-display", "size-adjust" }, names);
        }
    }
}
