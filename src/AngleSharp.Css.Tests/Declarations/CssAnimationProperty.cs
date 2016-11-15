﻿namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
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
            Assert.IsInstanceOf<CssAnimationDurationProperty>(property);
            var concrete = (CssAnimationDurationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms", concrete.Value);
        }

        [Test]
        public void CssAnimationDurationMultipleSecondsLegal()
        {
            var snippet = "animation-duration : 1s  , 2s  , 3s  , 4s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDurationProperty>(property);
            var concrete = (CssAnimationDurationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1s, 2s, 3s, 4s", concrete.Value);
        }

        [Test]
        public void CssAnimationDelayMillisecondsLegal()
        {
            var snippet = "animation-delay : 0ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDelayProperty>(property);
            var concrete = (CssAnimationDelayProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0ms", concrete.Value);
        }

        [Test]
        public void CssAnimationDelayZeroIllegal()
        {
            var snippet = "animation-delay : 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDelayProperty>(property);
            var concrete = (CssAnimationDelayProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssAnimationDelayZeroZeroSecondMillisecondsLegal()
        {
            var snippet = "animation-delay : 0s  , 0s  , 1s  , 20ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDelayProperty>(property);
            var concrete = (CssAnimationDelayProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0s, 0s, 1s, 20ms", concrete.Value);
        }

        [Test]
        public void CssAnimationNameDashSpecificLegal()
        {
            var snippet = "animation-name : -specific";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationNameProperty>(property);
            var concrete = (CssAnimationNameProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("-specific", concrete.Value);
        }

        [Test]
        public void CssAnimationNameSlidingVerticallyLegal()
        {
            var snippet = "animation-name : sliding-vertically";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationNameProperty>(property);
            var concrete = (CssAnimationNameProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("sliding-vertically", concrete.Value);
        }

        [Test]
        public void CssAnimationNameTest05Legal()
        {
            var snippet = "animation-name : test_05";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationNameProperty>(property);
            var concrete = (CssAnimationNameProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("test_05", concrete.Value);
        }

        [Test]
        public void CssAnimationNameNumberIllegal()
        {
            var snippet = "animation-name : 42";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationNameProperty>(property);
            var concrete = (CssAnimationNameProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssAnimationNameMyAnimationOtherAnimationLegal()
        {
            var snippet = "animation-name : my-animation, other-animation";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationNameProperty>(property);
            var concrete = (CssAnimationNameProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation, other-animation", concrete.Value);
        }

        [Test]
        public void CssAnimationIterationCountZeroLegal()
        {
            var snippet = "animation-iteration-count : 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationIterationCountProperty>(property);
            var concrete = (CssAnimationIterationCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssAnimationIterationCountInfiniteLegal()
        {
            var snippet = "animation-iteration-count : infinite";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationIterationCountProperty>(property);
            var concrete = (CssAnimationIterationCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("infinite", concrete.Value);
        }

        [Test]
        public void CssAnimationIterationCountInfiniteUppercaseLegal()
        {
            var snippet = "animation-iteration-count : INFINITE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationIterationCountProperty>(property);
            var concrete = (CssAnimationIterationCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("infinite", concrete.Value);
        }

        [Test]
        public void CssAnimationIterationCountFloatLegal()
        {
            var snippet = "animation-iteration-count : 2.3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationIterationCountProperty>(property);
            var concrete = (CssAnimationIterationCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2.3", concrete.Value);
        }

        [Test]
        public void CssAnimationIterationCountTwoZeroInfiniteLegal()
        {
            var snippet = "animation-iteration-count : 2, 0, infinite";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationIterationCountProperty>(property);
            var concrete = (CssAnimationIterationCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2, 0, infinite", concrete.Value);
        }

        [Test]
        public void CssAnimationIterationCountNegativeIllegal()
        {
            var snippet = "animation-iteration-count : -1";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationIterationCountProperty>(property);
            var concrete = (CssAnimationIterationCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssAnimationTimingFunctionEaseUppercaseLegal()
        {
            var snippet = "animation-timing-function : EASE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationTimingFunctionProperty>(property);
            var concrete = (CssAnimationTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease", concrete.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionNoneIllegal()
        {
            var snippet = "animation-timing-function : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationTimingFunctionProperty>(property);
            var concrete = (CssAnimationTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssAnimationTimingFunctionEaseInOutLegal()
        {
            var snippet = "animation-timing-function : ease-IN-out";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationTimingFunctionProperty>(property);
            var concrete = (CssAnimationTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease-in-out", concrete.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionStepEndLegal()
        {
            var snippet = "animation-timing-function : step-END";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationTimingFunctionProperty>(property);
            var concrete = (CssAnimationTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-end", concrete.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionStepStartLinearLegal()
        {
            var snippet = "animation-timing-function : step-start  , LINeAr";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationTimingFunctionProperty>(property);
            var concrete = (CssAnimationTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, linear", concrete.Value);
        }

        [Test]
        public void CssAnimationTimingFunctionStepStartCubicBezierLegal()
        {
            var snippet = "animation-timing-function : step-start  , cubic-bezier(0,1,1,1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationTimingFunctionProperty>(property);
            var concrete = (CssAnimationTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, cubic-bezier(0, 1, 1, 1)", concrete.Value);
        }

        [Test]
        public void CssAnimationPlayStateRunningLegal()
        {
            var snippet = "animation-play-state: running";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationPlayStateProperty>(property);
            var concrete = (CssAnimationPlayStateProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("running", concrete.Value);
        }

        [Test]
        public void CssAnimationPlayStatePausedUppercaseLegal()
        {
            var snippet = "animation-play-state: PAUSED";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationPlayStateProperty>(property);
            var concrete = (CssAnimationPlayStateProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("paused", concrete.Value);
        }

        [Test]
        public void CssAnimationPlayStatePausedRunningPausedLegal()
        {
            var snippet = "animation-play-state: paused, Running, paused";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationPlayStateProperty>(property);
            var concrete = (CssAnimationPlayStateProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("paused, running, paused", concrete.Value);
        }

        [Test]
        public void CssAnimationFillModeNoneLegal()
        {
            var snippet = "animation-fill-mode: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationFillModeProperty>(property);
            var concrete = (CssAnimationFillModeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssAnimationFillModeZeroIllegal()
        {
            var snippet = "animation-fill-mode: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationFillModeProperty>(property);
            var concrete = (CssAnimationFillModeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssAnimationFillModeBackwardsLegal()
        {
            var snippet = "animation-fill-mode: backwards !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationFillModeProperty>(property);
            var concrete = (CssAnimationFillModeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("backwards", concrete.Value);
        }

        [Test]
        public void CssAnimationFillModeForwardsUppercaseLegal()
        {
            var snippet = "animation-fill-mode: FORWARDS";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationFillModeProperty>(property);
            var concrete = (CssAnimationFillModeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("forwards", concrete.Value);
        }

        [Test]
        public void CssAnimationFillModeBothBackwardsForwardsNoneLegal()
        {
            var snippet = "animation-fill-mode: both , backwards ,  forwards  ,NONE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationFillModeProperty>(property);
            var concrete = (CssAnimationFillModeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("both, backwards, forwards, none", concrete.Value);
        }

        [Test]
        public void CssAnimationDirectionNormalLegal()
        {
            var snippet = "animation-direction: normal";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDirectionProperty>(property);
            var concrete = (CssAnimationDirectionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value);
        }

        [Test]
        public void CssAnimationDirectionReverseLegal()
        {
            var snippet = "animation-direction  : reverse";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDirectionProperty>(property);
            var concrete = (CssAnimationDirectionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("reverse", concrete.Value);
        }

        [Test]
        public void CssAnimationDirectionNoneIllegal()
        {
            var snippet = "animation-direction  : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDirectionProperty>(property);
            var concrete = (CssAnimationDirectionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssAnimationDirectionAlternateReverseUppercaseLegal()
        {
            var snippet = "animation-direction : alternate-REVERSE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDirectionProperty>(property);
            var concrete = (CssAnimationDirectionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("alternate-reverse", concrete.Value);
        }

        [Test]
        public void CssAnimationDirectionNormalAlternateReverseAlternateReverseLegal()
        {
            var snippet = "animation-direction: normal,alternate  , reverse   ,ALTERNATE-reverse !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationDirectionProperty>(property);
            var concrete = (CssAnimationDirectionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal, alternate, reverse, alternate-reverse", concrete.Value);
        }

        [Test]
        public void CssAnimationIterationCountLegal()
        {
            var snippet = "animation : 5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationProperty>(property);
            var concrete = (CssAnimationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5", concrete.Value);
        }

        [Test]
        public void CssAnimationNameLegal()
        {
            var snippet = "animation : my-animation";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationProperty>(property);
            var concrete = (CssAnimationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation", concrete.Value);
        }

        [Test]
        public void CssAnimationNameDurationDelayLegal()
        {
            var snippet = "animation : my-animation 2s 0.5s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationProperty>(property);
            var concrete = (CssAnimationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2s 0.5s my-animation", concrete.Value);
        }

        [Test]
        public void CssAnimationNameDurationDelayEaseLegal()
        {
            var snippet = "animation : my-animation  200ms 0.5s    ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationProperty>(property);
            var concrete = (CssAnimationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("200ms ease 0.5s my-animation", concrete.Value);
        }

        [Test]
        public void CssAnimationCountDoubleIllegal()
        {
            var snippet = "animation : 10 20";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationProperty>(property);
            var concrete = (CssAnimationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssAnimationNameDurationCountEaseInOutLegal()
        {
            var snippet = "animation : my-animation  200ms 2.5   ease-in-out";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationProperty>(property);
            var concrete = (CssAnimationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("200ms ease-in-out 2.5 my-animation", concrete.Value);
        }

        [Test]
        public void CssAnimationMultipleLegal()
        {
            var snippet = "animation : my-animation 0s 10 ease,   other-animation  5 linear,yet-another 0s 1s  10 step-start !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssAnimationProperty>(property);
            var concrete = (CssAnimationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0s ease 10 my-animation, linear 5 other-animation, 0s step-start 1s 10 yet-another", concrete.Value);
        }
    }
}
