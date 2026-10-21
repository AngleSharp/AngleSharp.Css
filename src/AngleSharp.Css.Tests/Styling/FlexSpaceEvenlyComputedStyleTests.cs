#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// `space-evenly` (CSS Box Alignment 3, supported by every browser for flexbox and grid) is
    /// missing from the `justify-content`/`align-content` keyword maps (and from
    /// `FlexContentMode`), so the whole declaration is dropped and computes as empty, while
    /// `space-between`/`space-around` work. Found in a downstream renderer (AngleSharp.Renderer),
    /// where a flex container with `justify-content: space-evenly` packs its items at the start.
    /// </summary>
    [TestFixture]
    public class FlexSpaceEvenlyComputedStyleTests
    {
        [TestCase("justify-content")]
        [TestCase("align-content")]
        public void SpaceEvenlyIsComputed(string property)
        {
            var document = ParseDocument($"<div id=target style=\"display: flex; {property}: space-evenly\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("space-evenly", target.ComputeCurrentStyle().GetPropertyValue(property));
        }

        [Test]
        public void SpaceEvenlyFromAStyleSheetIsComputed()
        {
            var document = ParseDocument("<style>.nested { display: flex; justify-content: space-evenly; }</style><div id=target class=nested></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("space-evenly", target.ComputeCurrentStyle().GetPropertyValue("justify-content"));
        }
    }
}
