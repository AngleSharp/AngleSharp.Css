namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssScopeRuleTests
    {
        [Test]
        public void ScopeRuleParses()
        {
            var sheet = ParseStyleSheet("@scope (.card) to (.card-footer) { :scope { color: red; } }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssScopeRule>(sheet.Rules[0]);

            var rule = (ICssScopeRule)sheet.Rules[0];
            Assert.AreEqual("(.card) to (.card-footer)", rule.ScopeText);
            Assert.AreEqual(1, rule.Rules.Length);
        }

        [Test]
        public void ScopeRuleWithEmptyPreludeParses()
        {
            var sheet = ParseStyleSheet("@scope { .card { color: red; } }");

            var rule = (ICssScopeRule)sheet.Rules[0];
            Assert.AreEqual("", rule.ScopeText);
            Assert.AreEqual(1, rule.Rules.Length);
        }

        [Test]
        public void ScopeRuleCanContainNestedAtRule()
        {
            var sheet = ParseStyleSheet("@scope (.host) { @media screen { .item { display: block; } } }");

            var rule = (ICssScopeRule)sheet.Rules[0];
            Assert.AreEqual(1, rule.Rules.Length);
            Assert.AreEqual(CssRuleType.Media, rule.Rules[0].Type);
        }

        [Test]
        public void ScopeRuleCanContainMultipleInnerRules()
        {
            var sheet = ParseStyleSheet("@scope (.x) { .a { top: 0; } .b { left: 0; } }");

            var rule = (ICssScopeRule)sheet.Rules[0];
            Assert.AreEqual(2, rule.Rules.Length);
            Assert.AreEqual(CssRuleType.Style, rule.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Style, rule.Rules[1].Type);
        }

        [Test]
        public void ScopeWithoutBlockIsIgnored()
        {
            var sheet = ParseStyleSheet("@scope (.card) ;");

            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void ScopeRuleSerializes()
        {
            var rule = ParseRule("@scope (.panel) { .title { color: red; } }");

            Assert.AreEqual(CssRuleType.Scope, rule.Type);
            Assert.AreEqual("@scope (.panel) { .title { color: rgba(255, 0, 0, 1) } }", rule.ToCss());
        }

        [Test]
        public void ScopeRuleCanBeUpdatedViaCssText()
        {
            var rule = ParseRule("@scope (.panel) { .title { color: red; } }");

            rule.CssText = "@scope (.card) { .body { left: 0; } }";

            Assert.AreEqual("@scope (.card) { .body { left: 0 } }", rule.ToCss());
        }

        [Test]
        public void ScopeRuleCanBeUpdatedViaCssTextToEmptyPrelude()
        {
            var rule = ParseRule("@scope (.panel) { .title { color: red; } }");

            rule.CssText = "@scope { .body { left: 0; } }";

            Assert.AreEqual("@scope { .body { left: 0 } }", rule.ToCss());
        }

        [Test]
        public void ScopeRuleCssTextRejectsWrongRuleType()
        {
            var rule = ParseRule("@scope (.panel) { .title { color: red; } }");

            Assert.Throws<AngleSharp.Dom.DomException>(() => rule.CssText = "@layer utilities;");
        }

        [Test]
        public void ScopeRuleWithMalformedNestedRuleStillParsesFollowingValidRule()
        {
            var sheet = ParseStyleSheet("@scope (.x) { .broken { left 0; } .ok { top: 0; } }");

            var scope = (ICssScopeRule)sheet.Rules[0];
            Assert.AreEqual(2, scope.Rules.Length);
            Assert.AreEqual(".broken", ((ICssStyleRule)scope.Rules[0]).SelectorText);
            Assert.AreEqual(".ok", ((ICssStyleRule)scope.Rules[1]).SelectorText);
        }

        [Test]
        public void ScopeRulePreservesCommentsWhenRequested()
        {
            var sheet = ParseStyleSheet("@scope (.x) { /*inside*/ .y { left: 0; } }");

            var result = sheet.ToCss(new CssSerializationOptions { PreserveComments = true });

            Assert.IsTrue(result.Contains("/*inside*/"));
            Assert.IsTrue(result.Contains("@scope (.x)"));
        }

        [Test]
        public void MinifyRemovesEmptyScopeRule()
        {
            var sheet = ParseStyleSheet("@scope (.x) { .y {} }");

            var result = sheet.ToCss(new MinifyStyleFormatter());

            Assert.AreEqual("", result);
        }

        [Test]
        public void MinifyKeepsNonEmptyScopeRule()
        {
            var sheet = ParseStyleSheet("@scope (.x) { .y { left: 0; } }");

            var result = sheet.ToCss(new MinifyStyleFormatter());

            Assert.AreEqual("@scope (.x){.y{left:0}}", result);
        }
    }
}
