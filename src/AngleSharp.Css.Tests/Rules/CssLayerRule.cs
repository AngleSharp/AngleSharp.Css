namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssLayerRuleTests
    {
        [Test]
        public void LayerStatementRuleParses()
        {
            var sheet = ParseStyleSheet("@layer components.utilities;");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssLayerRule>(sheet.Rules[0]);

            var rule = (ICssLayerRule)sheet.Rules[0];
            Assert.IsTrue(rule.IsStatement);
            Assert.AreEqual("components.utilities", rule.Name);
            Assert.AreEqual(0, rule.Rules.Length);
        }

        [Test]
        public void LayerStatementWithMultipleNamesParses()
        {
            var sheet = ParseStyleSheet("@layer reset, theme, overrides;");

            var rule = (ICssLayerRule)sheet.Rules[0];
            Assert.IsTrue(rule.IsStatement);
            Assert.AreEqual("reset, theme, overrides", rule.Name);
        }

        [Test]
        public void LayerUnnamedStatementParses()
        {
            var sheet = ParseStyleSheet("@layer;");

            var rule = (ICssLayerRule)sheet.Rules[0];
            Assert.IsTrue(rule.IsStatement);
            Assert.AreEqual("", rule.Name);
        }

        [Test]
        public void LayerBlockRuleParses()
        {
            var sheet = ParseStyleSheet("@layer base { h1 { color: red; } }");

            var rule = (ICssLayerRule)sheet.Rules[0];
            Assert.IsFalse(rule.IsStatement);
            Assert.AreEqual("base", rule.Name);
            Assert.AreEqual(1, rule.Rules.Length);
            Assert.AreEqual(CssRuleType.Style, rule.Rules[0].Type);
        }

        [Test]
        public void LayerUnnamedBlockRuleParses()
        {
            var sheet = ParseStyleSheet("@layer { h1 { color: red; } }");

            var rule = (ICssLayerRule)sheet.Rules[0];
            Assert.IsFalse(rule.IsStatement);
            Assert.AreEqual("", rule.Name);
            Assert.AreEqual(1, rule.Rules.Length);
        }

        [Test]
        public void LayerCanContainNestedAtRule()
        {
            var sheet = ParseStyleSheet("@layer base { @media screen { h1 { color: red; } } }");

            var rule = (ICssLayerRule)sheet.Rules[0];
            Assert.AreEqual(1, rule.Rules.Length);
            Assert.AreEqual(CssRuleType.Media, rule.Rules[0].Type);
        }

        [Test]
        public void LayerWithoutTerminatorIsIgnored()
        {
            var sheet = ParseStyleSheet("@layer base");

            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void LayerStatementRuleSerializes()
        {
            var rule = ParseRule("@layer components;");

            Assert.AreEqual(CssRuleType.Layer, rule.Type);
            Assert.AreEqual("@layer components;", rule.ToCss());
        }

        [Test]
        public void LayerBlockRuleSerializes()
        {
            var rule = ParseRule("@layer utilities { .gap { gap: 8px; } }");

            Assert.AreEqual("@layer utilities { .gap { gap: 8px } }", rule.ToCss());
        }

        [Test]
        public void LayerRuleCanBeUpdatedViaCssTextFromStatementToBlock()
        {
            var rule = ParseRule("@layer components;");

            rule.CssText = "@layer components { .x { top: 0; } }";

            Assert.AreEqual("@layer components { .x { top: 0 } }", rule.ToCss());
        }

        [Test]
        public void LayerRuleCanBeUpdatedViaCssTextFromBlockToStatement()
        {
            var rule = ParseRule("@layer components { .x { top: 0; } }");

            rule.CssText = "@layer components;";

            Assert.AreEqual("@layer components;", rule.ToCss());
        }

        [Test]
        public void LayerRuleCssTextRejectsWrongRuleType()
        {
            var rule = ParseRule("@layer components;");

            Assert.Throws<AngleSharp.Dom.DomException>(() => rule.CssText = "@scope (.x) { .y { top: 0; } }");
        }

        [Test]
        public void LayerRuleWithMalformedNestedRuleStillParsesFollowingValidRule()
        {
            var sheet = ParseStyleSheet("@layer base { .broken { top 0; } .ok { left: 0; } }");

            var layer = (ICssLayerRule)sheet.Rules[0];
            Assert.AreEqual(2, layer.Rules.Length);
            Assert.AreEqual(".broken", ((ICssStyleRule)layer.Rules[0]).SelectorText);
            Assert.AreEqual(".ok", ((ICssStyleRule)layer.Rules[1]).SelectorText);
        }

        [Test]
        public void LayerRulePreservesCommentsWhenRequested()
        {
            var sheet = ParseStyleSheet("@layer base { /*inside*/ .x { top: 0; } }");

            var result = sheet.ToCss(new CssSerializationOptions { PreserveComments = true });

            Assert.IsTrue(result.Contains("/*inside*/"));
            Assert.IsTrue(result.Contains("@layer base"));
        }

        [Test]
        public void MinifyRemovesEmptyLayerRule()
        {
            var sheet = ParseStyleSheet("@layer base { h1 {} }");

            var result = sheet.ToCss(new MinifyStyleFormatter());

            Assert.AreEqual("", result);
        }

        [Test]
        public void MinifyKeepsNonEmptyLayerRule()
        {
            var sheet = ParseStyleSheet("@layer base { .a { top: 0; } }");

            var result = sheet.ToCss(new MinifyStyleFormatter());

            Assert.AreEqual("@layer base{.a{top:0}}", result);
        }
    }
}
