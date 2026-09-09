#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Documents a confirmed, missing capability rather than a wrong-value bug (unlike the other
    /// gaps documented alongside this file - AnimationComputedStyleTests, ListStyleComputedValueTests,
    /// OverflowComputedStyleTests, BorderRadiusPercentageResolutionTests - which are each a small,
    /// pinpointable fix): CSS `filter` has no structured parsing support at all. Unlike `transform`
    /// (`AngleSharp.Css.Parser.TransformParser`/`ICssTransformFunctionValue`) and unlike
    /// `background-image`'s gradient functions (`AngleSharp.Css.Parser.GradientParser`/
    /// `ICssGradientFunctionValue` - both fully public and already relied on directly by a
    /// downstream renderer, AngleSharp.Renderer, instead of reimplementing that parsing locally),
    /// reflecting over this assembly finds no `FilterParser`/`ICssFilterFunctionValue` equivalent
    /// for `filter` - only the internal, unrelated `BackdropFilterDeclaration` for the different
    /// `backdrop-filter` property. A downstream renderer that wants to support `filter` (`blur()`,
    /// `grayscale()`, `drop-shadow()`, ...) currently has nothing to delegate to and must hand-parse
    /// the raw inline `style=""` text itself - the same situation `transform` and CSS gradients
    /// used to be in before `TransformParser`/`GradientParser` existed.
    /// </summary>
    [TestFixture]
    public class FilterPropertyTests
    {
        [Test]
        public void FilterComputedValueIsAlwaysEmptyRegardlessOfWhatWasAuthored()
        {
            var document = ParseDocument("<div id=target style=\"filter: grayscale(0.9) blur(2px);\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual(string.Empty, target.ComputeCurrentStyle().GetPropertyValue("filter"));
        }

        [Test]
        public void RawFilterTextIsStillReadableFromTheInlineStyleAttributeItself()
        {
            // Confirms the gap is specifically in AngleSharp.Css's own computed-style/cascade
            // pipeline, not in the HTML/attribute layer - the text is right there, just never
            // parsed into a structured value or even echoed back through computed style.
            var document = ParseDocument("<div id=target style=\"filter: grayscale(0.9) blur(2px);\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("filter: grayscale(0.9) blur(2px);", target.GetAttribute("style"));
        }
    }
}
