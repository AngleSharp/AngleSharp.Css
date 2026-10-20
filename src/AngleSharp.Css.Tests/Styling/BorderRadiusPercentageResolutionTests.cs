#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Per https://www.w3.org/TR/css-backgrounds-3/#corner-overlap, a percentage `border-radius`
    /// resolves its horizontal component against the border box's own *width* and its vertical
    /// component against the border box's own *height* - independently. Since the border box is
    /// only known during layout, the computed value keeps both components as percentages (see
    /// `BorderRadiusPercentageComputedValueTests`) instead of resolving them prematurely.
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
        public void PercentageComponentsOfAnExplicitlySizedBoxStayPercentages()
        {
            // 200x100 box, `border-radius: 10% / 30%` - the computed value is a pair of
            // <length-percentage> values (https://www.w3.org/TR/css-backgrounds-3/#border-radius),
            // so both components stay percentages. Resolving them (the horizontal one against the
            // 200px width, the vertical one against the 100px height) is up to layout.
            var document = ParseWithRenderDevice("<div id=target style=\"width:200px; height:100px; border-radius: 10% / 30%;\"></div>");
            var target = document.GetElementById("target");
            var style = target.ComputeCurrentStyle();

            Assert.AreEqual("10% 30%", style.GetPropertyValue("border-top-left-radius"));
        }

        [Test]
        public void VerticalPercentageComponentIsNotResolvedAgainstTheWidth()
        {
            // The vertical component must not be turned into a pixel value derived from the
            // box's width; it is kept as authored, independent of the box's dimensions.
            var shortDocument = ParseWithRenderDevice("<div id=target style=\"width:200px; height:100px; border-radius: 0 / 30%;\"></div>");
            var shortRadius = shortDocument.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("border-top-left-radius");

            var tallDocument = ParseWithRenderDevice("<div id=target style=\"width:200px; height:200px; border-radius: 0 / 30%;\"></div>");
            var tallRadius = tallDocument.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("border-top-left-radius");

            Assert.AreEqual("0px 30%", shortRadius);
            Assert.AreEqual("0px 30%", tallRadius);
        }
    }
}
