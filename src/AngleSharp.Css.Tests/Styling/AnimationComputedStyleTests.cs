#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Per the CSS cascade, a computed style is supposed to fully resolve every longhand to a
    /// concrete value - the CSS-wide keyword `initial` is itself resolved away during computation,
    /// never present verbatim in the computed declaration. Confirmed empirically while building
    /// CSS `animation` support in a downstream renderer (AngleSharp.Renderer): any `animation`
    /// longhand the `animation` shorthand does not explicitly set reports the literal string
    /// `"initial"` instead of that property's own real initial value (`normal` for
    /// `animation-direction`, `none` for `animation-fill-mode`, `0s` for `animation-delay`,
    /// `running` for `animation-play-state`) - unlike `transition`'s equivalent longhands, which
    /// report an empty string instead in the same situation (see BasicStyling.cs's sibling
    /// investigation for `transition`, not affected by this).
    /// </summary>
    [TestFixture]
    public class AnimationComputedStyleTests
    {
        [Test]
        public void UnsetAnimationDirectionResolvesToNormal()
        {
            var document = ParseDocument("<div id=target style=\"animation: spin 2s linear;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("normal", target.ComputeCurrentStyle().GetPropertyValue("animation-direction"));
        }

        [Test]
        public void UnsetAnimationFillModeResolvesToNone()
        {
            var document = ParseDocument("<div id=target style=\"animation: spin 2s linear;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("none", target.ComputeCurrentStyle().GetPropertyValue("animation-fill-mode"));
        }

        [Test]
        public void UnsetAnimationDelayResolvesToZeroSeconds()
        {
            var document = ParseDocument("<div id=target style=\"animation: spin 2s linear;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("0s", target.ComputeCurrentStyle().GetPropertyValue("animation-delay"));
        }

        [Test]
        public void UnsetAnimationPlayStateResolvesToRunning()
        {
            var document = ParseDocument("<div id=target style=\"animation: spin 2s linear;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("running", target.ComputeCurrentStyle().GetPropertyValue("animation-play-state"));
        }
    }
}
