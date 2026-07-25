namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssModernAtRulesTests
    {
        [Test]
        public void PropertyRuleParsesDescriptors()
        {
            var sheet = ParseStyleSheet("@property --brand-accent { syntax: \"<color>\"; inherits: false; initial-value: #0af; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssPropertyRule>(sheet.Rules[0]);

            var rule = (ICssPropertyRule)sheet.Rules[0];
            Assert.AreEqual("--brand-accent", rule.Name);
            Assert.AreEqual("\"<color>\"", rule.GetPropertyValue("syntax"));
            Assert.AreEqual("false", rule.GetPropertyValue("inherits"));
            Assert.AreEqual("#0af", rule.GetPropertyValue("initial-value"));
        }

        [Test]
        public void StartingStyleRuleParses()
        {
            var sheet = ParseStyleSheet("@starting-style { .panel { opacity: 0; } }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssStartingStyleRule>(sheet.Rules[0]);

            var rule = (ICssStartingStyleRule)sheet.Rules[0];
            Assert.AreEqual(1, rule.Rules.Length);
        }

        [Test]
        public void PositionTryRuleParses()
        {
            var sheet = ParseStyleSheet("@position-try --popover { top: 10px; left: 20px; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssPositionTryRule>(sheet.Rules[0]);

            var rule = (ICssPositionTryRule)sheet.Rules[0];
            Assert.AreEqual("--popover", rule.Name);
            Assert.AreEqual("10px", rule.Style.GetPropertyValue("top"));
            Assert.AreEqual("20px", rule.Style.GetPropertyValue("left"));
        }

        [Test]
        public void FontPaletteValuesRuleParsesDescriptors()
        {
            var sheet = ParseStyleSheet("@font-palette-values --brand { font-family: Bixa; base-palette: 2; override-colors: 1 #f00; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssFontPaletteValuesRule>(sheet.Rules[0]);

            var rule = (ICssFontPaletteValuesRule)sheet.Rules[0];
            Assert.AreEqual("--brand", rule.Name);
            Assert.AreEqual("Bixa", rule.GetPropertyValue("font-family"));
            Assert.AreEqual("2", rule.GetPropertyValue("base-palette"));
            Assert.AreEqual("1 #f00", rule.GetPropertyValue("override-colors"));
        }

        [Test]
        public void ColorProfileRuleParsesDescriptors()
        {
            var sheet = ParseStyleSheet("@color-profile --display-p3 { src: url(\"display-p3.icc\"); rendering-intent: auto; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssColorProfileRule>(sheet.Rules[0]);

            var rule = (ICssColorProfileRule)sheet.Rules[0];
            Assert.AreEqual("--display-p3", rule.Name);
            Assert.AreEqual("url(\"display-p3.icc\")", rule.GetPropertyValue("src"));
            Assert.AreEqual("auto", rule.GetPropertyValue("rendering-intent"));
        }
    }
}
