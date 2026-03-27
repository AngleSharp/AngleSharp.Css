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
            Assert.That(property.Name, Is.EqualTo("transition-property"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssTransitionPropertyAllLegal()
        {
            var snippet = "transition-property : ALL";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-property"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("all"));
        }

        [Test]
        public void CssTransitionPropertyWidthHeightLegal()
        {
            var snippet = "transition-property : width   , height";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-property"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("width, height"));
        }

        [Test]
        public void CssTransitionPropertyDashSpecificIllegal()
        {
            var snippet = "transition-property : -specific";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-property"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssTransitionPropertySlidingVerticallyIllegal()
        {
            var snippet = "transition-property : sliding-vertically";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-property"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssTransitionPropertyTest05Illegal()
        {
            var snippet = "transition-property : test_05";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-property"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssTransitionTimingFunctionEaseLegal()
        {
            var snippet = "transition-timing-function : ease";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("ease"));
        }

        [Test]
        public void CssTransitionTimingFunctionEaseInLegal()
        {
            var snippet = "transition-timing-function : ease-IN";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("ease-in"));
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartLegal()
        {
            var snippet = "transition-timing-function : step-start";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("step-start"));
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartStepEndLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("step-start, step-end"));
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartStepEndLinearEaseInOutLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end,linear,ease-IN-OUT";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("step-start, step-end, linear, ease-in-out"));
        }

        [Test]
        public void CssTransitionTimingFunctionCubicBezierLegal()
        {
            var snippet = "transition-timing-function : cubic-bezier(0, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("cubic-bezier(0, 1, 0.5, 1)"));
        }

        [Test]
        public void CssTransitionTimingFunctionStepsStartLegal()
        {
            var snippet = "transition-timing-function : steps(10, start)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("steps(10, start)"));
        }

        [Test]
        public void CssTransitionTimingFunctionStepsEndLegal()
        {
            var snippet = "transition-timing-function : steps(25, end)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("steps(25)"));
        }

        [Test]
        public void CssTransitionTimingFunctionStepsLinearCubicBezierLegal()
        {
            var snippet = "transition-timing-function : steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-timing-function"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)"));
        }

        [Test]
        public void CssTransitionDurationSecondsLegal()
        {
            var snippet = "transition-duration : 6s";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-duration"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("6s"));
        }

        [Test]
        public void CssTransitionDurationMillisecondsLegal()
        {
            var snippet = "transition-duration : 60ms";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-duration"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("60ms"));
        }

        [Test]
        public void CssTransitionDurationMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition-duration : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-duration"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("60ms, 1s, 2s"));
        }

        [Test]
        public void CssTransitionDelayMillisecondsLegal()
        {
            var snippet = "transition-delay : 60ms";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-delay"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("60ms"));
        }

        [Test]
        public void CssTransitionDelayMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition-delay : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition-delay"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("60ms, 1s, 2s"));
        }

        [Test]
        public void CssTransitionMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("60ms, 1s, 2s"));
        }

        [Test]
        public void CssTransitionStepsLinearCubicBezierLegal()
        {
            var snippet = "transition : steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)"));
        }

        [Test]
        public void CssTransitionWidthHeightLegal()
        {
            var snippet = "transition : width   , height";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("width, height"));
        }

        [Test]
        public void CssTransitionEaseLegal()
        {
            var snippet = "transition : ease";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("ease"));
        }

        [Test]
        public void CssTransitionSecondsEaseAllLegal()
        {
            var snippet = "transition : all 1s ease";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("all 1s ease"));
        }

        [Test]
        public void CssTransitionSecondsEaseAllHeightMsStepsLegal()
        {
            var snippet = "transition : all 1s ease, height steps(5) 50ms";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("all 1s ease, height 50ms steps(5)"));
        }

        [Test]
        public void CssTransitionSecondsEaseAllHeightMsStepsWidthCubicBezierLegal()
        {
            var snippet = "transition : all 1s ease, height step-start 50ms,width,cubic-bezier(0.2,0.5 , 1  ,  1)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transition"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("all 1s ease, height 50ms step-start, width, cubic-bezier(0.2, 0.5, 1, 1)"));
        }
    }
}
