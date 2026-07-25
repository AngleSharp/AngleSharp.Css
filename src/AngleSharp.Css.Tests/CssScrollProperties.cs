namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssScrollProperties
    {
        // ── scroll-behavior ──────────────────────────────────────────────────

        [Test]
        public void ScrollBehaviorAuto()
        {
            var property = ParseDeclaration("scroll-behavior: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-behavior"));
        }

        [Test]
        public void ScrollBehaviorSmooth()
        {
            var property = ParseDeclaration("scroll-behavior: smooth");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ScrollBehaviorInvalid()
        {
            var property = ParseDeclaration("scroll-behavior: instant");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void ScrollBehaviorInitial()
        {
            var rule = ParseRule("div { scroll-behavior: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule!.Style["scroll-behavior"], Is.Not.Null);
        }

        // ── scroll-snap-stop ──────────────────────────────────────────────────

        [Test]
        public void ScrollSnapStopNormal()
        {
            var property = ParseDeclaration("scroll-snap-stop: normal");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-snap-stop"));
        }

        [Test]
        public void ScrollSnapStopAlways()
        {
            var property = ParseDeclaration("scroll-snap-stop: always");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ScrollSnapStopInvalid()
        {
            var property = ParseDeclaration("scroll-snap-stop: auto");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void ScrollSnapStopInitial()
        {
            var rule = ParseRule("div { scroll-snap-stop: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule!.Style["scroll-snap-stop"], Is.Not.Null);
        }

        // ── scroll-margin sides ───────────────────────────────────────────────

        [Test]
        public void ScrollMarginTopLength()
        {
            var property = ParseDeclaration("scroll-margin-top: 10px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-top"));
        }

        [Test]
        public void ScrollMarginRightLength()
        {
            var property = ParseDeclaration("scroll-margin-right: 20px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-right"));
        }

        [Test]
        public void ScrollMarginBottomLength()
        {
            var property = ParseDeclaration("scroll-margin-bottom: 0");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-bottom"));
        }

        [Test]
        public void ScrollMarginLeftLength()
        {
            var property = ParseDeclaration("scroll-margin-left: 1em");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-left"));
        }

        [Test]
        public void ScrollMarginShorthandSingleValue()
        {
            var property = ParseDeclaration("scroll-margin: 10px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin"));
        }

        [Test]
        public void ScrollMarginShorthandFourValues()
        {
            var property = ParseDeclaration("scroll-margin: 10px 20px 30px 40px");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ScrollMarginBlockStartLength()
        {
            var property = ParseDeclaration("scroll-margin-block-start: 5px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-block-start"));
        }

        [Test]
        public void ScrollMarginBlockEndLength()
        {
            var property = ParseDeclaration("scroll-margin-block-end: 5px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-block-end"));
        }

        [Test]
        public void ScrollMarginBlockShorthand()
        {
            var property = ParseDeclaration("scroll-margin-block: 10px 20px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-block"));
        }

        [Test]
        public void ScrollMarginInlineStartLength()
        {
            var property = ParseDeclaration("scroll-margin-inline-start: 5px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-inline-start"));
        }

        [Test]
        public void ScrollMarginInlineEndLength()
        {
            var property = ParseDeclaration("scroll-margin-inline-end: 5px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-inline-end"));
        }

        [Test]
        public void ScrollMarginInlineShorthand()
        {
            var property = ParseDeclaration("scroll-margin-inline: 10px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-margin-inline"));
        }

        // ── scroll-padding sides ──────────────────────────────────────────────

        [Test]
        public void ScrollPaddingTopAuto()
        {
            var property = ParseDeclaration("scroll-padding-top: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-top"));
        }

        [Test]
        public void ScrollPaddingTopLength()
        {
            var property = ParseDeclaration("scroll-padding-top: 10px");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ScrollPaddingTopPercent()
        {
            var property = ParseDeclaration("scroll-padding-top: 5%");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ScrollPaddingRightLength()
        {
            var property = ParseDeclaration("scroll-padding-right: 20px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-right"));
        }

        [Test]
        public void ScrollPaddingBottomLength()
        {
            var property = ParseDeclaration("scroll-padding-bottom: 30px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-bottom"));
        }

        [Test]
        public void ScrollPaddingLeftAuto()
        {
            var property = ParseDeclaration("scroll-padding-left: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-left"));
        }

        [Test]
        public void ScrollPaddingShorthandSingleValue()
        {
            var property = ParseDeclaration("scroll-padding: 10px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding"));
        }

        [Test]
        public void ScrollPaddingShorthandAuto()
        {
            var property = ParseDeclaration("scroll-padding: auto");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ScrollPaddingShorthandFourValues()
        {
            var property = ParseDeclaration("scroll-padding: 10px 20px 30px 40px");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ScrollPaddingBlockStartAuto()
        {
            var property = ParseDeclaration("scroll-padding-block-start: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-block-start"));
        }

        [Test]
        public void ScrollPaddingBlockEndLength()
        {
            var property = ParseDeclaration("scroll-padding-block-end: 20px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-block-end"));
        }

        [Test]
        public void ScrollPaddingBlockShorthand()
        {
            var property = ParseDeclaration("scroll-padding-block: 10px 20px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-block"));
        }

        [Test]
        public void ScrollPaddingInlineStartLength()
        {
            var property = ParseDeclaration("scroll-padding-inline-start: 15px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-inline-start"));
        }

        [Test]
        public void ScrollPaddingInlineEndAuto()
        {
            var property = ParseDeclaration("scroll-padding-inline-end: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-inline-end"));
        }

        [Test]
        public void ScrollPaddingInlineShorthand()
        {
            var property = ParseDeclaration("scroll-padding-inline: auto 10px");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("scroll-padding-inline"));
        }

        // ── overscroll-behavior ───────────────────────────────────────────────

        [Test]
        public void OverscrollBehaviorXAuto()
        {
            var property = ParseDeclaration("overscroll-behavior-x: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("overscroll-behavior-x"));
        }

        [Test]
        public void OverscrollBehaviorXContain()
        {
            var property = ParseDeclaration("overscroll-behavior-x: contain");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void OverscrollBehaviorXNone()
        {
            var property = ParseDeclaration("overscroll-behavior-x: none");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void OverscrollBehaviorXInvalid()
        {
            var property = ParseDeclaration("overscroll-behavior-x: smooth");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void OverscrollBehaviorYAuto()
        {
            var property = ParseDeclaration("overscroll-behavior-y: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("overscroll-behavior-y"));
        }

        [Test]
        public void OverscrollBehaviorBlockContain()
        {
            var property = ParseDeclaration("overscroll-behavior-block: contain");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("overscroll-behavior-block"));
        }

        [Test]
        public void OverscrollBehaviorInlineNone()
        {
            var property = ParseDeclaration("overscroll-behavior-inline: none");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("overscroll-behavior-inline"));
        }

        [Test]
        public void OverscrollBehaviorShorthandSingleValue()
        {
            var property = ParseDeclaration("overscroll-behavior: contain");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("overscroll-behavior"));
        }

        [Test]
        public void OverscrollBehaviorShorthandTwoValues()
        {
            var property = ParseDeclaration("overscroll-behavior: auto none");
            Assert.That(property.HasValue, Is.True);
        }

        // ── initial keyword checks ────────────────────────────────────────────

        [Test]
        public void ScrollMarginTopInitial()
        {
            var rule = ParseRule("div { scroll-margin-top: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule!.Style["scroll-margin-top"], Is.Not.Null);
        }

        [Test]
        public void ScrollPaddingTopInitial()
        {
            var rule = ParseRule("div { scroll-padding-top: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule!.Style["scroll-padding-top"], Is.Not.Null);
        }

        [Test]
        public void ScrollMarginInitial()
        {
            var rule = ParseRule("div { scroll-margin: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule!.Style["scroll-margin"], Is.Not.Null);
        }

        [Test]
        public void ScrollPaddingInitial()
        {
            var rule = ParseRule("div { scroll-padding: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule!.Style["scroll-padding"], Is.Not.Null);
        }

        [Test]
        public void OverscrollBehaviorInitial()
        {
            var rule = ParseRule("div { overscroll-behavior: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule!.Style["overscroll-behavior"], Is.Not.Null);
        }
    }
}
