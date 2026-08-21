namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssFontDescriptorPropertyTests
    {
        [TestCase("size-adjust: 100%", "100%")]
        [TestCase("size-adjust: 90.5%", "90.5%")]
        [TestCase("size-adjust: 0%", "0%")]
        public void SizeAdjustLegalValues(string snippet, string expected)
        {
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("size-adjust", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual(expected, property.Value);
        }

        [TestCase("size-adjust: 10px")]
        [TestCase("size-adjust: 100")]
        [TestCase("size-adjust: auto")]
        public void SizeAdjustIllegalValues(string snippet)
        {
            var property = ParseDeclaration(snippet);
            Assert.IsFalse(property.HasValue);
        }

        [TestCase("ascent-override: 90%", "90%")]
        [TestCase("ascent-override: normal", "normal")]
        [TestCase("descent-override: 20%", "20%")]
        [TestCase("line-gap-override: 0%", "0%")]
        public void MetricOverrideLegalValues(string snippet, string expected)
        {
            var property = ParseDeclaration(snippet);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual(expected, property.Value);
        }

        [TestCase("ascent-override: 10px")]
        [TestCase("descent-override: auto")]
        [TestCase("line-gap-override: none")]
        public void MetricOverrideIllegalValues(string snippet)
        {
            var property = ParseDeclaration(snippet);
            Assert.IsFalse(property.HasValue);
        }

        [TestCase("font-feature-settings: normal", "normal")]
        [TestCase("font-feature-settings: \"liga\"", "\"liga\"")]
        [TestCase("font-feature-settings: \"liga\" 1", "\"liga\" 1")]
        [TestCase("font-feature-settings: \"kern\" on", "\"kern\" on")]
        [TestCase("font-feature-settings: \"kern\" off", "\"kern\" off")]
        [TestCase("font-feature-settings: \"liga\" 1, \"kern\" off", "\"liga\" 1, \"kern\" off")]
        public void FontFeatureSettingsLegalValues(string snippet, string expected)
        {
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-feature-settings", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual(expected, property.Value);
        }

        [TestCase("font-feature-settings: 12")]
        [TestCase("font-feature-settings: liga")]
        [TestCase("font-feature-settings: \"liga\" bogus")]
        public void FontFeatureSettingsIllegalValues(string snippet)
        {
            var property = ParseDeclaration(snippet);
            Assert.IsFalse(property.HasValue);
        }

        [TestCase("font-variation-settings: normal", "normal")]
        [TestCase("font-variation-settings: \"wght\" 400", "\"wght\" 400")]
        [TestCase("font-variation-settings: \"wght\" 400, \"slnt\" -10", "\"wght\" 400, \"slnt\" -10")]
        public void FontVariationSettingsLegalValues(string snippet, string expected)
        {
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-variation-settings", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual(expected, property.Value);
        }

        [TestCase("font-variation-settings: wght 400")]
        [TestCase("font-variation-settings: 400")]
        public void FontVariationSettingsIllegalValues(string snippet)
        {
            var property = ParseDeclaration(snippet);
            Assert.IsFalse(property.HasValue);
        }
    }
}
