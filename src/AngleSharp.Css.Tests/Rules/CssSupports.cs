namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssSupportsTests
    {
        [Test]
        public void SupportsEmptyRule()
        {
            var source = @"@supports () { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("()"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsBackgroundColorRedRule()
        {
            var source = @"@supports (background-color: red) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("(background-color: red)"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsBackgroundColorRedAndColorBlueRule()
        {
            var source = @"@supports ((background-color: red) and (color: blue)) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("((background-color: red) and (color: blue))"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsNotUnsupportedDeclarationRule()
        {
            var source = @"@supports (not (background-transparency: half)) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("(not (background-transparency: half))"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsUnsupportedDeclarationRule()
        {
            var source = @"@supports ((background-transparency: zero)) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("((background-transparency: zero))"));
            Assert.That(supports.Condition.Check(device), Is.False);
        }

        [Test]
        public void SupportsBackgroundRedWithImportantRule()
        {
            var source = @"@supports (background: red !important) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("(background: red !important)"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsPaddingTopOrPaddingLeftRule()
        {
            var source = @"@supports ((padding-TOP :  0) or (padding-left : 0)) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("((padding-TOP: 0) or (padding-left: 0))"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsPaddingTopOrPaddingLeftAndPaddingBottomOrPaddingRightRule()
        {
            var source = @"@supports (((padding-top: 0)  or  (padding-left: 0))  and  ((padding-bottom:  0)  or  (padding-right: 0))) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("(((padding-top: 0) or (padding-left: 0)) and ((padding-bottom: 0) or (padding-right: 0)))"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsDisplayFlexWithImportantRule()
        {
            var source = @"@supports (display: flex !important) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("(display: flex !important)"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsBareDisplayFlexRule()
        {
            var source = @"@supports display: flex { }";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(0));
        }

        [Test]
        public void SupportsDisplayFlexMultipleBracketsRule()
        {
            var source = @"@supports ((display: flex)) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("((display: flex))"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsTransitionOrAnimationNameAndTransformFrontBracketRule()
        {
            var source = @"@supports ((transition-property: color) or
           (animation-name: foo)) and
          (transform: rotate(10deg)) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("((transition-property: color) or (animation-name: foo)) and (transform: rotate(10deg))"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsTransitionOrAnimationNameAndTransformBackBracketRule()
        {
            var source = @"@supports (transition-property: color) or
           ((animation-name: foo) and
          (transform: rotate(10deg))) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("(transition-property: color) or ((animation-name: foo) and (transform: rotate(10deg)))"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsShadowVendorPrefixesRule()
        {
            var source = @"@supports ( box-shadow: 0 0 2px black ) or
          ( -moz-box-shadow: 0 0 2px black ) or
          ( -webkit-box-shadow: 0 0 2px black ) or
          ( -o-box-shadow: 0 0 2px black ) { }";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.ConditionText, Is.EqualTo("(box-shadow: 0 0 2px black) or (-moz-box-shadow: 0 0 2px black) or (-webkit-box-shadow: 0 0 2px black) or (-o-box-shadow: 0 0 2px black)"));
            Assert.That(supports.Condition.Check(device), Is.True);
        }

        [Test]
        public void SupportsNegatedDisplayFlexRuleWithDeclarations()
        {
            var source = @"@supports not ( display: flex ) {
  body { width: 100%; height: 100%; background: white; color: black; }
  #navigation { width: 25%; }
  #article { width: 75%; }
}";
            var sheet = ParseStyleSheet(source);
            var device = new DefaultRenderDevice();
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.That(supports.Rules.Length, Is.EqualTo(3));
            Assert.That(supports.ConditionText, Is.EqualTo("not (display: flex)"));
            Assert.That(supports.Condition.Check(device), Is.False);
        }
    }
}
