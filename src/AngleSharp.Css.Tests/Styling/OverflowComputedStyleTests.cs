#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Two `overflow` computed-style gaps confirmed while building overflow clipping support in a
    /// downstream renderer (AngleSharp.Renderer):
    /// (1) the `overflow` shorthand does not decompose into `overflow-x`/`overflow-y` in the
    /// computed style the way other shorthand/longhand pairs do, so a value authored only via the
    /// shorthand is never visible through either longhand accessor;
    /// (2) `overflow: clip` (a real, shipped CSS Overflow Module value) is not recognized by the
    /// parser at all - the whole declaration is silently dropped rather than being computed or
    /// even reported as an unsupported/unresolved value.
    /// </summary>
    [TestFixture]
    public class OverflowComputedStyleTests
    {
        [Test]
        public void OverflowShorthandDecomposesIntoOverflowXLonghand()
        {
            var document = ParseDocument("<div id=target style=\"overflow: hidden;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("hidden", target.ComputeCurrentStyle().GetPropertyValue("overflow-x"));
        }

        [Test]
        public void OverflowShorthandDecomposesIntoOverflowYLonghand()
        {
            var document = ParseDocument("<div id=target style=\"overflow: hidden;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("hidden", target.ComputeCurrentStyle().GetPropertyValue("overflow-y"));
        }

        [Test]
        public void OverflowClipIsRecognizedAndComputed()
        {
            var document = ParseDocument("<div id=target style=\"overflow: clip;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("clip", target.ComputeCurrentStyle().GetPropertyValue("overflow"));
        }

        [Test]
        public void ExplicitLonghandAuthoredAfterTheShorthandOverridesItsComponent()
        {
            // A newly confirmed gap, found once the two gaps above were fixed: `overflow-y`
            // written *after* the `overflow` shorthand in the same declaration block should win
            // for that axis (the ordinary "later declaration of the same effective property wins"
            // cascade rule), but the shorthand's own component always wins instead, regardless of
            // declaration order (confirmed with both orderings below) - the shorthand's expansion
            // into longhands appears to be applied unconditionally rather than only when that
            // longhand was not otherwise explicitly set.
            var document = ParseDocument("<div id=target style=\"overflow: hidden; overflow-y: visible;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("visible", target.ComputeCurrentStyle().GetPropertyValue("overflow-y"));
        }

        [Test]
        public void ExplicitLonghandAuthoredBeforeTheShorthandStillWinsForThatAxis()
        {
            // The shorthand comes textually *after* the longhand here - if the shorthand's
            // expansion is unconditionally overwriting rather than declaration-order-aware, this
            // ordering fails identically to the reverse ordering above (confirmed: it does).
            var document = ParseDocument("<div id=target style=\"overflow-y: visible; overflow: hidden;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("hidden", target.ComputeCurrentStyle().GetPropertyValue("overflow-x"), "the horizontal axis is untouched by the explicit overflow-y override and should still pick up hidden from the shorthand.");
            Assert.AreEqual("visible", target.ComputeCurrentStyle().GetPropertyValue("overflow-y"));
        }
    }
}
