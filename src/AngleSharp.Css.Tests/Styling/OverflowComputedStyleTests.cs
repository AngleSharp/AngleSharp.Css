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
    }
}
