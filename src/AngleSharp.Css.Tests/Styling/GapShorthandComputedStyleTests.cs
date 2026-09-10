#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// `gap`/`grid-gap`'s two-value form is `&lt;row-gap&gt; &lt;column-gap&gt;` per
    /// https://drafts.csswg.org/css-align-3/#gap-shorthand - `GapDeclaration`/`GridGapDeclaration`'s
    /// own `Longhands` arrays listed `column-gap` before `row-gap`, the reverse of what their own
    /// `Merge()`/`Split()` methods already assumed (`values[0]`/`Items[0]` is always the row value),
    /// so the two longhands' computed values came out swapped. Found while adding CSS Grid track
    /// sizing support to a downstream renderer.
    /// </summary>
    [TestFixture]
    public class GapShorthandComputedStyleTests
    {
        [Test]
        public void GapShorthandDecomposesRowFirstColumnSecond()
        {
            var document = ParseDocument("<div id=target style=\"gap: 10px 20px;\"></div>");
            var target = document.GetElementById("target");
            var style = target.ComputeCurrentStyle();

            Assert.AreEqual("10px", style.GetPropertyValue("row-gap"));
            Assert.AreEqual("20px", style.GetPropertyValue("column-gap"));
        }

        [Test]
        public void GridGapShorthandDecomposesRowFirstColumnSecond()
        {
            var document = ParseDocument("<div id=target style=\"grid-gap: 10px 20px;\"></div>");
            var target = document.GetElementById("target");
            var style = target.ComputeCurrentStyle();

            Assert.AreEqual("10px", style.GetPropertyValue("grid-row-gap"));
            Assert.AreEqual("20px", style.GetPropertyValue("grid-column-gap"));
        }
    }
}
