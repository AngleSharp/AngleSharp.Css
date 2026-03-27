namespace AngleSharp.Css.Tests.Rules
{
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssKeyframeRuleTests
    {
        [Test]
        public void KeyframeRuleWithFromAndMarginLeft()
        {
            var rule = ParseKeyframeRule(@"  from {
    margin-left: 0px;
  }");
            Assert.IsNotNull(rule);
            Assert.That(rule.KeyText, Is.EqualTo("0%"));
            Assert.That(rule.Key.Stops.Count(), Is.EqualTo(1));
            Assert.That(rule.Style.Length, Is.EqualTo(1));
            Assert.That(rule.Style.First().Name, Is.EqualTo("margin-left"));
        }

        [Test]
        public void KeyframeRuleWith50PercentAndMarginLeftOpacity()
        {
            var rule = ParseKeyframeRule(@"  50% {
    margin-left: 110px;
    opacity: 1;
  }");
            Assert.IsNotNull(rule);
            Assert.That(rule.KeyText, Is.EqualTo("50%"));
            Assert.That(rule.Key.Stops.Count(), Is.EqualTo(1));
            Assert.That(rule.Style.Length, Is.EqualTo(2));
            Assert.That(rule.Style.Skip(0).First().Name, Is.EqualTo("margin-left"));
            Assert.That(rule.Style.Skip(1).First().Name, Is.EqualTo("opacity"));
        }

        [Test]
        public void KeyframeRuleWithToAndMarginLeft()
        {
            var rule = ParseKeyframeRule(@"  to {
    margin-left: 200px;
  }");
            Assert.IsNotNull(rule);
            Assert.That(rule.KeyText, Is.EqualTo("100%"));
            Assert.That(rule.Key.Stops.Count(), Is.EqualTo(1));
            Assert.That(rule.Style.Length, Is.EqualTo(1));
            Assert.That(rule.Style.First().Name, Is.EqualTo("margin-left"));
        }

        [Test]
        public void KeyframeRuleWithFromTo255075PercentAndPaddingTopPaddingLeftColor()
        {
            var rule = ParseKeyframeRule(@"  from,to, 25%, 50%,75%{
    padding-top: 200px;
    padding-left: 2em;
    color: red
  }");
            Assert.IsNotNull(rule);
            Assert.That(rule.KeyText, Is.EqualTo("0%, 100%, 25%, 50%, 75%"));
            Assert.That(rule.Key.Stops.Count(), Is.EqualTo(5));
            Assert.That(rule.Style.Length, Is.EqualTo(3));
            Assert.That(rule.Style.Skip(0).First().Name, Is.EqualTo("padding-top"));
            Assert.That(rule.Style.Skip(1).First().Name, Is.EqualTo("padding-left"));
            Assert.That(rule.Style.Skip(2).First().Name, Is.EqualTo("color"));
        }

        [Test]
        public void KeyframeRuleWith0AndNoDeclarations()
        {
            var rule = ParseKeyframeRule(@"  0% { }");
            Assert.IsNotNull(rule);
            Assert.That(rule.KeyText, Is.EqualTo("0%"));
            Assert.That(rule.Key.Stops.Count(), Is.EqualTo(1));
            Assert.That(rule.Style.Length, Is.EqualTo(0));
        }

        [Test]
        public void KeyframeRuleWithPercentage_Issue128()
        {
            var rule = ParseKeyframeRule(@"  0.52%,   50.0%,92.82% { }");
            Assert.IsNotNull(rule);
            Assert.That(rule.KeyText, Is.EqualTo("0.52%, 50%, 92.82%"));
            Assert.That(rule.Key.Stops.Count(), Is.EqualTo(3));
            Assert.That(rule.Style.Length, Is.EqualTo(0));
        }
    }
}
