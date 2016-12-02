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
            Assert.AreEqual("animation-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("60ms", property.Value);
        }

        [Test]
        public void CssAnimationDurationMultipleSecondsLegal()
        {
            var snippet = "animation-duration : 1s  , 2s  , 3s  , 4s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1s, 2s, 3s, 4s", property.Value);
        }

        [Test]
        public void CssAnimationDelayMillisecondsLegal()
        {
            var snippet = "animation-delay : 0ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0ms", property.Value);
        }

        [Test]
        public void CssAnimationDelayZeroIllegal()
        {
            var snippet = "animation-delay : 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssAnimationDelayZeroZeroSecondMillisecondsLegal()
        {
            var snippet = "animation-delay : 0s  , 0s  , 1s  , 20ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0s, 0s, 1s, 20ms", property.Value);
        }

        [Test]
        public void CssAnimationNameDashSpecificLegal()
        {
            var snippet = "animation-name : -specific";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("-specific", property.Value);
        }

        [Test]
        public void CssAnimationNameSlidingVerticallyLegal()
        {
            var snippet = "animation-name : sliding-vertically";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("sliding-vertically", property.Value);
        }

        [Test]
        public void CssAnimationNameTest05Legal()
        {
            var snippet = "animation-name : test_05";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("test_05", property.Value);
        }

        [Test]
        public void CssAnimationNameNumberIllegal()
        {
            var snippet = "animation-name : 42";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssAnimationNameMyAnimationOtherAnimationLegal()
        {
            var snippet = "animation-name : my-animation, other-animation";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("my-animation, other-animation", property.Value);
        }

        [Test]
        public void CssAnimationIterationCountZeroLegal()
        {
            var snippet = "animation-iteration-count : 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssAnimationIterationCountInfiniteLegal()
        {
            var snippet = "animation-iteration-count : infinite";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("infinite", property.Value);
        }

        [Test]
        public void CssAnimationIterationCountInfiniteUppercaseLegal()
        {
            var snippet = "animation-iteration-count : INFINITE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("infinite", property.Value);
        }

        [Test]
        public void CssAnimationIterationCountFloatLegal()
        {
            var snippet = "animation-iteration-count : 2.3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2.3", property.Value);
        }

        [Test]
        public void CssAnimationIterationCountTwoZeroInfiniteLegal()
        {
            var snippet = "animation-iteration-count : 2, 0, infinite";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2, 0, infinite", property.Value);
        }

        [Test]
        public void CssAnimationIterationCountNegativeIllegal()
        {
            var snippet = "animation-iteration-count : -1";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssAnimationTimingFunctionEaseUppercaseLegal()
        {
            var snippet = "animation-timing-function : EASE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("ease", property.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionNoneIllegal()
        {
            var snippet = "animation-timing-function : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssAnimationTimingFunctionEaseInOutLegal()
        {
            var snippet = "animation-timing-function : ease-IN-out";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("ease-in-out", property.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionStepEndLegal()
        {
            var snippet = "animation-timing-function : step-END";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("step-end", property.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionStepStartLinearLegal()
        {
            var snippet = "animation-timing-function : step-start  , LINeAr";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("step-start, linear", property.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionStepStartCubicBezierLegal()
        {
            var snippet = "animation-timing-function : step-start  , cubic-bezier(0,1,1,1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("step-start, cubic-bezier(0, 1, 1, 1)", property.Value);
        }

        [Test]
        public void CssAnimationPlayStateRunningLegal()
        {
            var snippet = "animation-play-state: running";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("running", property.Value);
        }

        [Test]
        public void CssAnimationPlayStatePausedUppercaseLegal()
        {
            var snippet = "animation-play-state: PAUSED";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("paused", property.Value);
        }

        [Test]
        public void CssAnimationPlayStatePausedRunningPausedLegal()
        {
            var snippet = "animation-play-state: paused, Running, paused";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("paused, running, paused", property.Value);
        }

        [Test]
        public void CssAnimationFillModeNoneLegal()
        {
            var snippet = "animation-fill-mode: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssAnimationFillModeZeroIllegal()
        {
            var snippet = "animation-fill-mode: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssAnimationFillModeBackwardsLegal()
        {
            var snippet = "animation-fill-mode: backwards !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("backwards", property.Value);
        }

        [Test]
        public void CssAnimationFillModeForwardsUppercaseLegal()
        {
            var snippet = "animation-fill-mode: FORWARDS";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("forwards", property.Value);
        }

        [Test]
        public void CssAnimationFillModeBothBackwardsForwardsNoneLegal()
        {
            var snippet = "animation-fill-mode: both , backwards ,  forwards  ,NONE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("both, backwards, forwards, none", property.Value);
        }

        [Test]
        public void CssAnimationDirectionNormalLegal()
        {
            var snippet = "animation-direction: normal";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssAnimationDirectionReverseLegal()
        {
            var snippet = "animation-direction  : reverse";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("reverse", property.Value);
        }

        [Test]
        public void CssAnimationDirectionNoneIllegal()
        {
            var snippet = "animation-direction  : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssAnimationDirectionAlternateReverseUppercaseLegal()
        {
            var snippet = "animation-direction : alternate-REVERSE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("alternate-reverse", property.Value);
        }

        [Test]
        public void CssAnimationDirectionNormalAlternateReverseAlternateReverseLegal()
        {
            var snippet = "animation-direction: normal,alternate  , reverse   ,ALTERNATE-reverse !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal, alternate, reverse, alternate-reverse", property.Value);
        }

        [Test]
        public void CssAnimationIterationCountLegal()
        {
            var snippet = "animation : 5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5", property.Value);
        }

        [Test]
        public void CssAnimationNameLegal()
        {
            var snippet = "animation : my-animation";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("my-animation", property.Value);
        }

        [Test]
        public void CssAnimationNameDurationDelayLegal()
        {
            var snippet = "animation : my-animation 2s 0.5s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2s 0.5s my-animation", property.Value);
        }

        [Test]
        public void CssAnimationNameDurationDelayEaseLegal()
        {
            var snippet = "animation : my-animation  200ms 0.5s    ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("200ms ease 0.5s my-animation", property.Value);
        }

        [Test]
        public void CssAnimationCountDoubleIllegal()
        {
            var snippet = "animation : 10 20";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssAnimationNameDurationCountEaseInOutLegal()
        {
            var snippet = "animation : my-animation  200ms 2.5   ease-in-out";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("200ms ease-in-out 2.5 my-animation", property.Value);
        }

        [Test]
        public void CssAnimationMultipleLegal()
        {
            var snippet = "animation : my-animation 0s 10 ease,   other-animation  5 linear,yet-another 0s 1s  10 step-start !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0s ease 10 my-animation, linear 5 other-animation, 0s step-start 1s 10 yet-another", property.Value);
        }
    }
}
