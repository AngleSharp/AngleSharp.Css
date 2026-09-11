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
            var document = ParseWithRenderDevice(
                "<div id=target style=\"background-image: radial-gradient(circle at 20% 80%, red, blue);\"></div>", 1000, 1000);
            var target = document.GetElementById("target");

            var computed = target.ComputeCurrentStyle().GetPropertyValue("background-image");

            StringAssert.Contains("200px 800px", computed);
        }

        // A separate, still-open, narrower issue surfaced alongside this fix: Y always resolves
        // its own percentage/length against the viewport's *width*, never its height (confirmed:
        // "20% 80%" resolves to "200px 800px" regardless of viewport height, i.e. 80% of the 1000px
        // *width* - matching a coincidence in this test's own numbers, not genuine height-tracking).
        // That is a materially different, deeper problem (percentage resolution mode not threaded
        // per-axis through a point's own Compute() at all) than the X-value-copied-into-Y typo this
        // test pins down, and is not fixed here - a downstream renderer relying on correct Y-axis
        // percentage resolution for a computed point still needs to read the pre-Compute() specified
        // value instead (as AngleSharp.Renderer's own ResolveExplicitBackgroundImage now does for
        // exactly this reason), not the computed one.
    }
}
