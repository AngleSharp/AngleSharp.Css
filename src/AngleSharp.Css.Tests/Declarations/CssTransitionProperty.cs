namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssTransitionPropertyTests
    {
        [Test]
        public void CssTransitionPropertyNoneLegal()
        {
            var snippet = "transition-property : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssTransitionPropertyAllLegal()
        {
            var snippet = "transition-property : ALL";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("all", property.Value);
        }

        [Test]
        public void CssTransitionPropertyWidthHeightLegal()
        {
            var snippet = "transition-property : width   , height";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("width, height", property.Value);
        }

        [Test]
        public void CssTransitionPropertyDashSpecificIllegal()
        {
            var snippet = "transition-property : -specific";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssTransitionPropertySlidingVerticallyIllegal()
        {
            var snippet = "transition-property : sliding-vertically";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssTransitionPropertyTest05Illegal()
        {
            var snippet = "transition-property : test_05";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssTransitionTimingFunctionEaseLegal()
        {
            var snippet = "transition-timing-function : ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("ease", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionEaseInLegal()
        {
            var snippet = "transition-timing-function : ease-IN";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("ease-in", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartLegal()
        {
            var snippet = "transition-timing-function : step-start";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("step-start", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartStepEndLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("step-start, step-end", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartStepEndLinearEaseInOutLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end,linear,ease-IN-OUT";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("step-start, step-end, linear, ease-in-out", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionCubicBezierLegal()
        {
            var snippet = "transition-timing-function : cubic-bezier(0, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("cubic-bezier(0, 1, 0.5, 1)", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepsStartLegal()
        {
            var snippet = "transition-timing-function : steps(10, start)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("steps(10, start)", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepsEndLegal()
        {
            var snippet = "transition-timing-function : steps(25, end)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("steps(25)", property.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepsLinearCubicBezierLegal()
        {
            var snippet = "transition-timing-function : steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)", property.Value);
        }

        [Test]
        public void CssTransitionDurationSecondsLegal()
        {
            var snippet = "transition-duration : 6s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("6s", property.Value);
        }

        [Test]
        public void CssTransitionDurationMillisecondsLegal()
        {
            var snippet = "transition-duration : 60ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("60ms", property.Value);
        }

        [Test]
        public void CssTransitionDurationMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition-duration : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("60ms, 1s, 2s", property.Value);
        }

        [Test]
        public void CssTransitionDelayMillisecondsLegal()
        {
            var snippet = "transition-delay : 60ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("60ms", property.Value);
        }

        [Test]
        public void CssTransitionDelayMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition-delay : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("60ms, 1s, 2s", property.Value);
        }

        [Test]
        public void CssTransitionMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("60ms, 1s, 2s", property.Value);
        }

        [Test]
        public void CssTransitionStepsLinearCubicBezierLegal()
        {
            var snippet = "transition : steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)", property.Value);
        }

        [Test]
        public void CssTransitionWidthHeightLegal()
        {
            var snippet = "transition : width   , height";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("width, height", property.Value);
        }

        [Test]
        public void CssTransitionEaseLegal()
        {
            var snippet = "transition : ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("ease", property.Value);
        }

        [Test]
        public void CssTransitionSecondsEaseAllLegal()
        {
            var snippet = "transition : all 1s ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("all 1s ease", property.Value);
        }

        [Test]
        public void CssTransitionSecondsEaseAllHeightMsStepsLegal()
        {
            var snippet = "transition : all 1s ease, height steps(5) 50ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("all 1s ease, height 50ms steps(5)", property.Value);
        }

        [Test]
        public void CssTransitionSecondsEaseAllHeightMsStepsWidthCubicBezierLegal()
        {
            var snippet = "transition : all 1s ease, height step-start 50ms,width,cubic-bezier(0.2,0.5 , 1  ,  1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("all 1s ease, height 50ms step-start, width, cubic-bezier(0.2, 0.5, 1, 1)", property.Value);
        }
    }
}
