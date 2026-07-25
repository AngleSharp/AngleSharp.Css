namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssAnimationProperties2
    {
        [Test]
        public void AnimationCompositionReplace()
        {
            var property = ParseDeclaration("animation-composition: replace");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("animation-composition"));
        }

        [Test]
        public void AnimationCompositionAdd()
        {
            var property = ParseDeclaration("animation-composition: add");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationCompositionAccumulate()
        {
            var property = ParseDeclaration("animation-composition: accumulate");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationCompositionMultiple()
        {
            var property = ParseDeclaration("animation-composition: replace, add, accumulate");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationCompositionInvalid()
        {
            var property = ParseDeclaration("animation-composition: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void AnimationTimelineAuto()
        {
            var property = ParseDeclaration("animation-timeline: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("animation-timeline"));
        }

        [Test]
        public void AnimationTimelineCustomIdent()
        {
            var property = ParseDeclaration("animation-timeline: custom-timeline");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationTimelineMultiple()
        {
            var property = ParseDeclaration("animation-timeline: auto, timeline1, timeline2");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationTimelineInvalid()
        {
            var property = ParseDeclaration("animation-timeline: 123");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void AnimationRangeStartPercent()
        {
            var property = ParseDeclaration("animation-range-start: 0%");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("animation-range-start"));
        }

        [Test]
        public void AnimationRangeStartNormal()
        {
            var property = ParseDeclaration("animation-range-start: normal");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationRangeStartLength()
        {
            var property = ParseDeclaration("animation-range-start: 100px");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationRangeEndPercent()
        {
            var property = ParseDeclaration("animation-range-end: 100%");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("animation-range-end"));
        }

        [Test]
        public void AnimationRangeEndNormal()
        {
            var property = ParseDeclaration("animation-range-end: normal");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationRangeEndCover()
        {
            var property = ParseDeclaration("animation-range-end: cover");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationRangeSingleValue()
        {
            var property = ParseDeclaration("animation-range: 0%");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("animation-range"));
        }

        [Test]
        public void AnimationRangeNormal()
        {
            var property = ParseDeclaration("animation-range: normal");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationRangeMultiple()
        {
            var property = ParseDeclaration("animation-range: 0%, 100%");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationRangeWithCover()
        {
            var property = ParseDeclaration("animation-range: cover 0%, 100%");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AnimationRangeInvalid()
        {
            var property = ParseDeclaration("animation-range: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void AnimationCompositionInitial()
        {
            var rule = ParseRule("div { animation-composition: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["animation-composition"], Is.Not.Null);
        }

        [Test]
        public void AnimationTimelineInitial()
        {
            var rule = ParseRule("div { animation-timeline: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["animation-timeline"], Is.Not.Null);
        }

        [Test]
        public void AnimationRangeStartInitial()
        {
            var rule = ParseRule("div { animation-range-start: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["animation-range-start"], Is.Not.Null);
        }

        [Test]
        public void AnimationRangeEndInitial()
        {
            var rule = ParseRule("div { animation-range-end: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["animation-range-end"], Is.Not.Null);
        }

        [Test]
        public void AnimationRangeInitial()
        {
            var rule = ParseRule("div { animation-range: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["animation-range"], Is.Not.Null);
        }
    }
}
