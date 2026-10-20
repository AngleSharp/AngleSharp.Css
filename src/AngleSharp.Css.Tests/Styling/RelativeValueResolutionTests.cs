#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Complements <see cref="RelativeValueComputedStyleTests"/> with the edge cases of resolving
    /// relative values during the style computation.
    /// </summary>
    [TestFixture]
    public class RelativeValueResolutionTests
    {
        private static string Computed(string html, string property)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 400, ViewPortHeight = 300, FontSize = 16 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument(html);
            return document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue(property);
        }

        private static string ComputedInParent(string declarations, string property) =>
            Computed($"<div style=\"font-size: 20px; width: 200px\"><p id=target style=\"{declarations}\">x</p></div>", property);

        [TestCase("width: calc(50% + 10px)", "width", "calc(50% + 10px)")]
        [TestCase("width: calc((10% + 5px) * 2)", "width", "calc((10% + 5px) * 2)")]
        [TestCase("width: calc(1em + 10px)", "width", "30px")]
        [TestCase("width: min(50%, 100px)", "width", "min(50%, 100px)")]
        public void UnresolvableCalcKeepsItsExpression(string declarations, string property, string expected)
        {
            Assert.AreEqual(expected, ComputedInParent(declarations, property));
        }

        [TestCase("width: 2ch", "width", "20px")]
        [TestCase("width: 2ex", "width", "20px")]
        [TestCase("width: 10vw", "width", "40px")]
        [TestCase("height: 10svh", "height", "30px")]
        public void FontAndViewportRelativeUnitsCompute(string declarations, string property, string expected)
        {
            Assert.AreEqual(expected, ComputedInParent(declarations, property));
        }

        [TestCase("font-size: 2em; margin-top: 1em", "margin-top", "40px")]
        [TestCase("font-size: 2rem", "font-size", "32px")]
        [TestCase("font-size: inherit; width: 1em", "width", "20px")]
        [TestCase("font-size: var(--size); --size: 15px; width: 2em", "width", "30px")]
        public void EmUsesTheComputedFontSizeOfTheElement(string declarations, string property, string expected)
        {
            Assert.AreEqual(expected, ComputedInParent(declarations, property));
        }

        [TestCase("font: large serif", "font-size", "19.2px")]
        [TestCase("font: larger serif", "font-size", "24px")]
        [TestCase("font-size: xx-large", "font-size", "32px")]
        public void FontSizeKeywordsComputeToLengths(string declarations, string property, string expected)
        {
            Assert.AreEqual(expected, ComputedInParent(declarations, property));
        }

        [TestCase("line-height: 1.5", "1.5")]
        [TestCase("line-height: normal", "normal")]
        public void UnitlessLineHeightIsKept(string declarations, string expected)
        {
            Assert.AreEqual(expected, ComputedInParent(declarations, "line-height"));
        }

        [Test]
        public void RemInsideNestedElementsUsesTheRootFontSize()
        {
            var html = "<html style=\"font-size: 10px\"><body style=\"font-size: 30px\"><div style=\"font-size: 2em\"><p id=target style=\"width: 3rem\">x</p></div></body></html>";

            Assert.AreEqual("30px", Computed(html, "width"));
        }

        [Test]
        public void CurrentColorOnTheRootIsTheInitialColor()
        {
            Assert.AreEqual("rgba(0, 0, 0, 1)", Computed("<html id=target style=\"color: currentcolor\"></html>", "color"));
        }

        [TestCase("border-radius: 50%", "border-radius", "50%")]
        [TestCase("grid: 1fr / auto-flow dense", "grid", "1fr / auto-flow dense")]
        [TestCase("opacity: 25%", "opacity", "0.25")]
        [TestCase("font-style: oblique 0.5turn", "font-style", "oblique 180deg")]
        public void ComputedValuesSerialize(string declarations, string property, string expected)
        {
            Assert.AreEqual(expected, ComputedInParent(declarations, property));
        }
    }
}
