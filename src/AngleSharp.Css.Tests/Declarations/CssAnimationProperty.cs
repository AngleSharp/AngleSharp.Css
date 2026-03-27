namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssAnimationPropertyTests 
    {
        [Test]
        public void CssAnimationDurationMillisecondsLegal()
        {
            var snippet = "animation-duration : 60ms";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-duration"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("60ms"));
        }

        [Test]
        public void CssAnimationDurationMultipleSecondsLegal()
        {
            var snippet = "animation-duration : 1s  , 2s  , 3s  , 4s";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-duration"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1s, 2s, 3s, 4s"));
        }

        [Test]
        public void CssAnimationDelayMillisecondsLegal()
        {
            var snippet = "animation-delay : 0ms";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-delay"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0ms"));
        }

        [Test]
        public void CssAnimationDelayZeroIllegal()
        {
            var snippet = "animation-delay : 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-delay"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssAnimationDelayZeroZeroSecondMillisecondsLegal()
        {
            var snippet = "animation-delay : 0s  , 0s  , 1s  , 20ms";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-delay"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0s, 0s, 1s, 20ms"));
        }

        [Test]
        public void CssAnimationNameDashSpecificLegal()
        {
            var snippet = "animation-name : -specific";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-name"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("-specific"));
        }

        [Test]
        public void CssAnimationNameSlidingVerticallyLegal()
        {
            var snippet = "animation-name : sliding-vertically";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-name"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("sliding-vertically"));
        }

        [Test]
        public void CssAnimationNameTest05Legal()
        {
            var snippet = "animation-name : test_05";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-name"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("test_05"));
        }

        [Test]
        public void CssAnimationNameNumberIllegal()
        {
            var snippet = "animation-name : 42";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-name"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssAnimationNameMyAnimationOtherAnimationLegal()
        {
            var snippet = "animation-name : my-animation, other-animation";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-name"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("my-animation, other-animation"));
        }

        [Test]
        public void CssAnimationIterationCountZeroLegal()
        {
            var snippet = "animation-iteration-count : 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-iteration-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssAnimationIterationCountInfiniteLegal()
        {
            var snippet = "animation-iteration-count : infinite";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-iteration-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("infinite"));
        }

        [Test]
        public void CssAnimationIterationCountInfiniteUppercaseLegal()
        {
            var snippet = "animation-iteration-count : INFINITE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-iteration-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("infinite"));
        }

        [Test]
        public void CssAnimationIterationCountFloatLegal()
        {
            var snippet = "animation-iteration-count : 2.3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-iteration-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2.3"));
        }

        [Test]
        public void CssAnimationIterationCountTwoZeroInfiniteLegal()
        {
            var snippet = "animation-iteration-count : 2, 0, infinite";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-iteration-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2, 0, infinite"));
        }

        [Test]
        public void CssAnimationIterationCountNegativeIllegal()
        {
            var snippet = "animation-iteration-count : -1";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-iteration-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssAnimationTimingFunctionEaseUppercaseLegal()
        {
            var snippet = "animation-timing-function : EASE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("ease"));
        }

        [Test]
        public void CssAnimationTimingFunctionNoneIllegal()
        {
            var snippet = "animation-timing-function : none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssAnimationTimingFunctionEaseInOutLegal()
        {
            var snippet = "animation-timing-function : ease-IN-out";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("ease-in-out"));
        }

        [Test]
        public void CssAnimationTimingFunctionStepEndLegal()
        {
            var snippet = "animation-timing-function : step-END";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("step-end"));
        }

        [Test]
        public void CssAnimationTimingFunctionStepStartLinearLegal()
        {
            var snippet = "animation-timing-function : step-start  , LINeAr";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("step-start, linear"));
        }

        [Test]
        public void CssAnimationTimingFunctionStepStartCubicBezierLegal()
        {
            var snippet = "animation-timing-function : step-start  , cubic-bezier(0,1,1,1)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("step-start, cubic-bezier(0, 1, 1, 1)"));
        }

        [Test]
        public void CssAnimationPlayStateRunningLegal()
        {
            var snippet = "animation-play-state: running";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-play-state"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("running"));
        }

        [Test]
        public void CssAnimationPlayStatePausedUppercaseLegal()
        {
            var snippet = "animation-play-state: PAUSED";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-play-state"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("paused"));
        }

        [Test]
        public void CssAnimationPlayStatePausedRunningPausedLegal()
        {
            var snippet = "animation-play-state: paused, Running, paused";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-play-state"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("paused, running, paused"));
        }

        [Test]
        public void CssAnimationFillModeNoneLegal()
        {
            var snippet = "animation-fill-mode: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-fill-mode"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssAnimationFillModeZeroIllegal()
        {
            var snippet = "animation-fill-mode: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-fill-mode"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssAnimationFillModeBackwardsLegal()
        {
            var snippet = "animation-fill-mode: backwards !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-fill-mode"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("backwards"));
        }

        [Test]
        public void CssAnimationFillModeForwardsUppercaseLegal()
        {
            var snippet = "animation-fill-mode: FORWARDS";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-fill-mode"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("forwards"));
        }

        [Test]
        public void CssAnimationFillModeBothBackwardsForwardsNoneLegal()
        {
            var snippet = "animation-fill-mode: both , backwards ,  forwards  ,NONE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-fill-mode"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("both, backwards, forwards, none"));
        }

        [Test]
        public void CssAnimationDirectionNormalLegal()
        {
            var snippet = "animation-direction: normal";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-direction"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssAnimationDirectionReverseLegal()
        {
            var snippet = "animation-direction  : reverse";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-direction"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("reverse"));
        }

        [Test]
        public void CssAnimationDirectionNoneIllegal()
        {
            var snippet = "animation-direction  : none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-direction"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssAnimationDirectionAlternateReverseUppercaseLegal()
        {
            var snippet = "animation-direction : alternate-REVERSE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-direction"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("alternate-reverse"));
        }

        [Test]
        public void CssAnimationDirectionNormalAlternateReverseAlternateReverseLegal()
        {
            var snippet = "animation-direction: normal,alternate  , reverse   ,ALTERNATE-reverse !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation-direction"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal, alternate, reverse, alternate-reverse"));
        }

        [Test]
        public void CssAnimationIterationCountLegal()
        {
            var snippet = "animation : 5";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5"));
        }

        [Test]
        public void CssAnimationNameLegal()
        {
            var snippet = "animation : my-animation";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("my-animation"));
        }

        [Test]
        public void CssAnimationNameDurationDelayLegal()
        {
            var snippet = "animation : my-animation 2s 0.5s";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2s 0.5s my-animation"));
        }

        [Test]
        public void CssAnimationNameDurationDelayEaseLegal()
        {
            var snippet = "animation : my-animation  200ms 0.5s    ease";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("200ms ease 0.5s my-animation"));
        }

        [Test]
        public void CssAnimationCountDoubleIllegal()
        {
            var snippet = "animation : 10 20";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssAnimationNameDurationCountEaseInOutLegal()
        {
            var snippet = "animation : my-animation  200ms 2.5   ease-in-out";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("200ms ease-in-out 2.5 my-animation"));
        }

        [Test]
        public void CssAnimationMultipleLegal()
        {
            var snippet = "animation : my-animation 0s 10 ease,   other-animation  5 linear,yet-another 0s 1s  10 step-start !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("animation"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0s ease 10 my-animation, linear 5 other-animation, 0s step-start 1s 10 yet-another"));
        }
    }
}
