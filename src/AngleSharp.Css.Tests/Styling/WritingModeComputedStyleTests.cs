#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// CSS Writing Modes 3/4: `text-orientation` (mixed | upright | sideways, inherited) is not
    /// known at all, so it computes as empty and vertical text cannot be set upright; the legacy
    /// SVG `writing-mode` values, which CSS Writing Modes 3 (section 10.1) requires user agents to
    /// accept as aliases (lr, lr-tb, rl, rl-tb compute to horizontal-tb; tb, tb-rl to
    /// vertical-rl), are dropped. Found in a downstream renderer (AngleSharp.Renderer).
    /// </summary>
    [TestFixture]
    public class WritingModeComputedStyleTests
    {
        [TestCase("mixed")]
        [TestCase("upright")]
        [TestCase("sideways")]
        public void TextOrientationIsComputed(string value)
        {
            Assert.AreEqual(value, Compute($"text-orientation: {value}").GetPropertyValue("text-orientation"));
        }

        [Test]
        public void TextOrientationIsInherited()
        {
            var document = ParseDocument("<div style=\"writing-mode: vertical-rl; text-orientation: upright\"><p id=target>x</p></div>");

            Assert.AreEqual("upright", document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("text-orientation"));
        }

        [TestCase("lr", "horizontal-tb")]
        [TestCase("lr-tb", "horizontal-tb")]
        [TestCase("rl", "horizontal-tb")]
        [TestCase("rl-tb", "horizontal-tb")]
        [TestCase("tb", "vertical-rl")]
        [TestCase("tb-rl", "vertical-rl")]
        public void LegacyWritingModeValuesComputeToTheirModernEquivalent(string value, string expected)
        {
            Assert.AreEqual(expected, Compute($"writing-mode: {value}").GetPropertyValue("writing-mode"));
        }

        private static ICssStyleDeclaration Compute(string declarations)
        {
            var document = ParseDocument($"<div id=target style=\"{declarations}\">x</div>");
            return document.GetElementById("target").ComputeCurrentStyle();
        }
    }
}
