namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssCounterStyleRuleTests
    {
        [Test]
        public void CssCounterStyleThumbsIntroExample()
        {
            var source = "@counter-style thumbs {\n                  system: cyclic;\n                  symbols: \"\"\\1F44D\"\";\n                  suffix: \"\" \"\";\n                }";
            var rule = ParseCounterStyleRule(source);
            Assert.AreEqual("thumbs", rule.StyleName);
        }
    }
}
