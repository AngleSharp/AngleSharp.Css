﻿namespace AngleSharp.Css.Tests.Rules
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
            Assert.AreEqual("0%", rule.KeyText);
            Assert.AreEqual(1, rule.Key.Stops.Count());
            Assert.AreEqual(1, rule.Style.Length);
            Assert.AreEqual("margin-left", rule.Style.First().Name);
        }

        [Test]
        public void KeyframeRuleWith50PercentAndMarginLeftOpacity()
        {
            var rule = ParseKeyframeRule(@"  50% {
    margin-left: 110px;
    opacity: 1;
  }");
            Assert.IsNotNull(rule);
            Assert.AreEqual("50%", rule.KeyText);
            Assert.AreEqual(1, rule.Key.Stops.Count());
            Assert.AreEqual(2, rule.Style.Length);
            Assert.AreEqual("margin-left", rule.Style.Skip(0).First().Name);
            Assert.AreEqual("opacity", rule.Style.Skip(1).First().Name);
        }

        [Test]
        public void KeyframeRuleWithToAndMarginLeft()
        {
            var rule = ParseKeyframeRule(@"  to {
    margin-left: 200px;
  }");
            Assert.IsNotNull(rule);
            Assert.AreEqual("100%", rule.KeyText);
            Assert.AreEqual(1, rule.Key.Stops.Count());
            Assert.AreEqual(1, rule.Style.Length);
            Assert.AreEqual("margin-left", rule.Style.First().Name);
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
            Assert.AreEqual("0%, 100%, 25%, 50%, 75%", rule.KeyText);
            Assert.AreEqual(5, rule.Key.Stops.Count());
            Assert.AreEqual(3, rule.Style.Length);
            Assert.AreEqual("padding-top", rule.Style.Skip(0).First().Name);
            Assert.AreEqual("padding-left", rule.Style.Skip(1).First().Name);
            Assert.AreEqual("color", rule.Style.Skip(2).First().Name);
        }

        [Test]
        public void KeyframeRuleWith0AndNoDeclarations()
        {
            var rule = ParseKeyframeRule(@"  0% { }");
            Assert.IsNotNull(rule);
            Assert.AreEqual("0%", rule.KeyText);
            Assert.AreEqual(1, rule.Key.Stops.Count());
            Assert.AreEqual(0, rule.Style.Length);
        }
    }
}
