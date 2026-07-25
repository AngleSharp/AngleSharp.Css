namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssViewTransitionRuleTests
    {
        [Test]
        public void ViewTransitionRuleParsesDescriptors()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: auto; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssViewTransitionRule>(sheet.Rules[0]);

            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual("auto", rule.GetPropertyValue("navigation"));
        }

        [Test]
        public void ViewTransitionRuleParsesMultipleDescriptors()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: auto; types: fade, slide; }");

            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual("auto", rule.GetPropertyValue("navigation"));
            Assert.AreEqual("fade, slide", rule.GetPropertyValue("types"));
        }

        [Test]
        public void ViewTransitionRuleUsesLastDescriptorValue()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: none; navigation: auto; }");

            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual("auto", rule.GetPropertyValue("navigation"));
        }

        [Test]
        public void ViewTransitionRuleTracksImportantPriority()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: auto !important; }");

            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual(CssKeywords.Important, rule.GetPropertyPriority("navigation"));
        }

        [Test]
        public void ViewTransitionRuleSetPropertyAddsAndUpdatesDescriptor()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: auto; }");
            var rule = (ICssViewTransitionRule)sheet.Rules[0];

            rule.SetProperty("types", "wipe");
            Assert.AreEqual("wipe", rule.GetPropertyValue("types"));

            rule.SetProperty("types", "wipe, fade");
            Assert.AreEqual("wipe, fade", rule.GetPropertyValue("types"));
        }

        [Test]
        public void ViewTransitionRuleRemovePropertyRemovesDescriptor()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: auto; types: fade; }");
            var rule = (ICssViewTransitionRule)sheet.Rules[0];

            var removed = rule.RemoveProperty("types");

            Assert.AreEqual("fade", removed);
            Assert.AreEqual("", rule.GetPropertyValue("types"));
        }

        [Test]
        public void ViewTransitionWithoutBlockIsIgnored()
        {
            var sheet = ParseStyleSheet("@view-transition navigation: auto;");

            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void ViewTransitionRuleSerializes()
        {
            var rule = ParseRule("@view-transition { navigation: auto; types: fade; }");

            Assert.AreEqual(CssRuleType.ViewTransition, rule.Type);
            Assert.AreEqual("@view-transition { navigation: auto; types: fade }", rule.ToCss());
        }

        [Test]
        public void ViewTransitionRuleWithMalformedDescriptorKeepsFollowingValidDescriptor()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation auto; types: fade; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual("", rule.GetPropertyValue("navigation"));
            Assert.AreEqual("fade", rule.GetPropertyValue("types"));
        }

        [Test]
        public void ViewTransitionRuleWithLeadingSemicolonParsesFollowingDescriptors()
        {
            var sheet = ParseStyleSheet("@view-transition { ; navigation: auto; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual("auto", rule.GetPropertyValue("navigation"));
        }

        [Test]
        public void ViewTransitionRuleWithMissingValueKeepsFollowingDescriptor()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: ; types: fade; }");

            Assert.AreEqual(1, sheet.Rules.Length);
            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual("", rule.GetPropertyValue("navigation"));
            Assert.AreEqual("fade", rule.GetPropertyValue("types"));
        }

        [Test]
        public void ViewTransitionRuleDescriptorLookupUsesOriginalCasing()
        {
            var sheet = ParseStyleSheet("@view-transition { NAVIGATION: auto; Types: fade; }");

            var rule = (ICssViewTransitionRule)sheet.Rules[0];
            Assert.AreEqual("auto", rule.GetPropertyValue("NAVIGATION"));
            Assert.AreEqual("fade", rule.GetPropertyValue("Types"));
            Assert.AreEqual("", rule.GetPropertyValue("navigation"));
        }

        [Test]
        public void ViewTransitionRuleCanBeUpdatedViaCssText()
        {
            var rule = ParseRule("@view-transition { navigation: none; }");

            rule.CssText = "@view-transition { navigation: auto; types: fade; }";

            Assert.AreEqual("@view-transition { navigation: auto; types: fade }", rule.ToCss());
        }

        [Test]
        public void ViewTransitionRuleCssTextRejectsWrongRuleType()
        {
            var rule = ParseRule("@view-transition { navigation: none; }");

            Assert.Throws<AngleSharp.Dom.DomException>(() => rule.CssText = "@layer x;" );
        }

        [Test]
        public void ViewTransitionRulePreservesCommentsWhenRequested()
        {
            var sheet = ParseStyleSheet("@view-transition { /*vt*/ navigation: auto; }", new AngleSharp.Css.Parser.CssParserOptions { IsIncludingUnknownDeclarations = true });

            var result = sheet.ToCss(new CssSerializationOptions { PreserveComments = true });

            Assert.IsTrue(result.Contains("/*vt*/"));
            Assert.IsTrue(result.Contains("navigation: auto"));
        }

        [Test]
        public void ViewTransitionInsideLayerRoundtripsWithSiblings()
        {
            var sheet = ParseStyleSheet("@layer app { @view-transition { navigation: auto; } .x { top: 0; } }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.AreEqual(CssRuleType.Layer, sheet.Rules[0].Type);
            var layer = (ICssLayerRule)sheet.Rules[0];
            Assert.AreEqual(2, layer.Rules.Length);
            Assert.AreEqual(CssRuleType.ViewTransition, layer.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Style, layer.Rules[1].Type);

            var css = sheet.ToCss();
            Assert.IsTrue(css.Contains("@view-transition"));
            Assert.IsTrue(css.Contains(".x { top: 0 }"));
        }

        [Test]
        public void ViewTransitionInsideScopeRoundtripsWithSiblings()
        {
            var sheet = ParseStyleSheet("@scope (.host) { @view-transition { navigation: auto; } .x { left: 0; } }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.AreEqual(CssRuleType.Scope, sheet.Rules[0].Type);
            var scope = (ICssScopeRule)sheet.Rules[0];
            Assert.AreEqual(2, scope.Rules.Length);
            Assert.AreEqual(CssRuleType.ViewTransition, scope.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Style, scope.Rules[1].Type);
        }

        [Test]
        public void MinifyRemovesEmptyViewTransitionRule()
        {
            var sheet = ParseStyleSheet("@view-transition {}");

            var result = sheet.ToCss(new MinifyStyleFormatter());

            Assert.AreEqual("", result);
        }

        [Test]
        public void MinifyKeepsNonEmptyViewTransitionRule()
        {
            var sheet = ParseStyleSheet("@view-transition { navigation: auto; }");

            var result = sheet.ToCss(new MinifyStyleFormatter());

            Assert.AreEqual("@view-transition{navigation:auto}", result);
        }

        [Test]
        public void MinifyRemovesEmptyViewTransitionInsideLayer()
        {
            var sheet = ParseStyleSheet("@layer app { @view-transition {} .x { top: 0; } }");

            var result = sheet.ToCss(new MinifyStyleFormatter());

            Assert.AreEqual("@layer app{.x{top:0}}", result);
        }
    }
}
