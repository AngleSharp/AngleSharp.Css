#nullable disable
namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Regression test for a confirmed bug in <c>CssPoint2D.ICssValue.Compute(ICssComputeContext)</c>
    /// (Values/Composites/CssPoint2D.cs): its <c>y</c> local is assigned from
    /// <c>_x.Compute(context)</c> instead of <c>_y.Compute(context)</c> - a copy-paste typo that
    /// makes the computed Y coordinate track the X component's own value instead of its own,
    /// whenever the two axes' raw values actually differ (a symmetric point like the default
    /// "center center" happens to hide this, since X and Y start out equal). Found while
    /// integrating a downstream renderer's CSS gradient support (a radial/conic gradient's `at
    /// <position>` is a `CssPoint2D`), but this affects any consumer computing a point with
    /// unequal axes - `background-position` reproduces it just as directly.
    /// </summary>
    [TestFixture]
    public class Point2DComputationTests
    {
        private static IDocument ParseWithRenderDevice(string html, int viewPortWidth, int viewPortHeight)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = viewPortWidth, ViewPortHeight = viewPortHeight });
            var browsingContext = BrowsingContext.New(config);
            var htmlParser = browsingContext.GetService<IHtmlParser>();
            return htmlParser.ParseDocument(html);
        }

        [Test]
        public void ComputedYCoordinateTracksItsOwnValueNotX()
        {
            // A radial-gradient's `at <position>` is a CssPoint2D. Before the fix, Y always came
            // out equal to X's own computed value regardless of what Y itself was authored as -
            // 20% 80% computed to "200px 200px" (both from X's 20%), not "200px 800px".
            // Percentages of a gradient position refer to the gradient box, which is only known
            // during layout, so they remain percentages in the computed value (they are no longer
            // resolved against the viewport's width, which also made Y ignore the height).
            var document = ParseWithRenderDevice(
                "<div id=target style=\"background-image: radial-gradient(circle at 20% 80%, red, blue);\"></div>", 1000, 1000);
            var target = document.GetElementById("target");

            var computed = target.ComputeCurrentStyle().GetPropertyValue("background-image");

            StringAssert.Contains("at 20% 80%", computed);
        }
    }
}
