#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Per https://www.w3.org/TR/css-backgrounds-3/#corner-overlap, a percentage `border-radius`
    /// resolves its horizontal component against the border box's own *width* and its vertical
    /// component against the border box's own *height* - independently. Confirmed empirically
    /// while building `border-radius` support in a downstream renderer (AngleSharp.Renderer) that
    /// this does not happen: both components resolve against the containing block's/element's
    /// *width* alone, so on a box whose width and height differ, the vertical radius comes out
    /// wrong (tracking the wrong axis's dimension entirely, not merely imprecise).
    /// </summary>
    [TestFixture]
    public class BorderRadiusPercentageResolutionTests
    {
        private static IDocument ParseWithRenderDevice(string html, int viewPortWidth = 1000)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = viewPortWidth });
            var browsingContext = BrowsingContext.New(config);
            var htmlParser = browsingContext.GetService<IHtmlParser>();
            return htmlParser.ParseDocument(html);
        }

        [Test]
        public void VerticalPercentageComponentResolvesAgainstTheElementsOwnHeight()
        {
            // 200x100 box, `border-radius: 10% / 30%` - the horizontal component (10%) should
            // resolve against the 200px width (20px); the vertical component (30%) should resolve
            // against the 100px height (30px), not against the 200px width (which would give the
            // wrong value, 60px).
            var document = ParseWithRenderDevice("<div id=target style=\"width:200px; height:100px; border-radius: 10% / 30%;\"></div>");
            var target = document.GetElementById("target");
            var style = target.ComputeCurrentStyle();

            var horizontal = style.GetPropertyValue("border-top-left-radius");
            Assert.IsTrue(horizontal.Contains("30px"), $"expected the vertical 30% component to resolve to 30px (30% of the 100px height); got '{horizontal}'");
        }

        [Test]
        public void VerticalPercentageComponentTracksHeightAcrossDifferentElementHeights()
        {
            // The same vertical percentage against two different heights (but the same width)
            // must resolve to two different pixel values if it is genuinely tracking height - if
            // it were (incorrectly) tracking width instead, both would resolve identically despite
            // the different heights.
            var shortDocument = ParseWithRenderDevice("<div id=target style=\"width:200px; height:100px; border-radius: 0 / 30%;\"></div>");
            var shortTarget = shortDocument.GetElementById("target");
            var shortRadius = shortTarget.ComputeCurrentStyle().GetPropertyValue("border-top-left-radius");

            var tallDocument = ParseWithRenderDevice("<div id=target style=\"width:200px; height:200px; border-radius: 0 / 30%;\"></div>");
            var tallTarget = tallDocument.GetElementById("target");
            var tallRadius = tallTarget.ComputeCurrentStyle().GetPropertyValue("border-top-left-radius");

            Assert.AreNotEqual(shortRadius, tallRadius, "a 30% vertical radius against a 100px-tall box and a 200px-tall box must resolve to different pixel values.");
        }
    }
}
