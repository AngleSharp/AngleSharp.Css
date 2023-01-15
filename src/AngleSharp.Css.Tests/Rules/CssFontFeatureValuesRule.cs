namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssFontFeatureValuesRuleTests
    {
        [Test]
        public void CssFontFeaturesValuesForSomeIntroExample()
        {
            var source = "@font-feature-values Font One {\n                    @styleset {\n                    nice-style: 12;\n                    }\n                }";
            var rule = ParseFontFeatureValuesRule(source);
            Assert.AreEqual("Font One", rule.FamilyName);
        }
    }
}
