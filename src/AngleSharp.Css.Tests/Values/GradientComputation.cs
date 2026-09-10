#nullable disable
namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Regression tests for a confirmed bug shared by all three gradient function values
    /// (`CssLinearGradientValue`/`CssRadialGradientValue`/`CssConicGradientValue`), found while
    /// integrating this library's gradient support into a downstream renderer - the same class of
    /// bug <see cref="TransformFunctionsTests"/> already pins down for `transform`: each gradient's
    /// own `ICssValue.Compute(ICssComputeContext)` unconditionally calls `.Compute(context)` on a
    /// field that is legitimately `null` whenever the corresponding clause was not authored (the
    /// single most common way each gradient function is actually written - no explicit angle/size/
    /// center at all), throwing a `NullReferenceException` instead of computing normally.
    /// RenderTreeBuilder computes an element's entire style declaration eagerly while constructing
    /// the render tree, so this crashes the whole render, not just whatever reads the gradient.
    /// </summary>
    [TestFixture]
    public class GradientComputationTests
    {
        [Test]
        public void LinearGradientWithNoAngleDoesNotThrowWhenComputed()
        {
            // CssLinearGradientValue.Compute() calls _angle.Compute(context) with no null check;
            // _angle is null for the default "to bottom" direction (no `to <side>`/angle authored).
            var document = "<div style=\"background-image: linear-gradient(red, blue)\"></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }

        [Test]
        public void RadialGradientWithNoSizeDoesNotThrowWhenComputed()
        {
            // CssRadialGradientValue.Compute() calls _width.Compute(context)/_height.Compute(context)
            // with no null check; both are null for the default ellipse/farthest-corner sizing (no
            // explicit shape/size clause authored at all).
            var document = "<div style=\"background-image: radial-gradient(red, blue)\"></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }

        [Test]
        public void ConicGradientWithNoAngleOrCenterDoesNotThrowWhenComputed()
        {
            // CssConicGradientValue.Compute() calls _angle.Compute(context)/_center.Compute(context)
            // with no null check; both are null for the default 0deg/center (no `from`/`at` clause
            // authored at all).
            var document = "<div style=\"background-image: conic-gradient(red, blue)\"></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }
    }
}
