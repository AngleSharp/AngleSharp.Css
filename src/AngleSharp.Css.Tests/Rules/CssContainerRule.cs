namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssContainerRuleTests
    {
        [Test]
        public void ContainerRuleWithQueryOnlyParses()
        {
            var source = "@container (width <= 30em) { h1 { color: green } }";
            var sheet = ParseStyleSheet(source);

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssContainerRule>(sheet.Rules[0]);

            var rule = (ICssContainerRule)sheet.Rules[0];
            Assert.AreEqual("", rule.ContainerName);
            Assert.AreEqual("(width <= 30em)", rule.ContainerQuery);
            Assert.AreEqual("(width <= 30em)", rule.ConditionText);
            Assert.AreEqual(1, rule.Rules.Length);
        }

        [Test]
        public void ContainerRuleWithNameParses()
        {
            var source = "@container sidebar (inline-size > 700px) { h1 { color: green } }";
            var sheet = ParseStyleSheet(source);

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssContainerRule>(sheet.Rules[0]);

            var rule = (ICssContainerRule)sheet.Rules[0];
            Assert.AreEqual("sidebar", rule.ContainerName);
            Assert.AreEqual("(inline-size > 700px)", rule.ContainerQuery);
            Assert.AreEqual("sidebar (inline-size > 700px)", rule.ConditionText);
            Assert.AreEqual(1, rule.Rules.Length);
        }

        [Test]
        public void ContainerRuleWithoutConditionIsIgnored()
        {
            var source = "@container { h1 { color: green } }";
            var sheet = ParseStyleSheet(source);

            Assert.AreEqual(0, sheet.Rules.Length);
        }
    }
}
