#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Computed values of paint-related properties that a downstream renderer
    /// (AngleSharp.Renderer) needs while implementing `border-image`, CSS masks, `backdrop-filter`
    /// and multiple background layers:
    /// - `border-image-slice` serializes a single number as `30 NaNpx NaNpx NaNpx`.
    /// - A unitless `border-image-width`/`border-image-outset` (a multiple of the border width)
    ///   throws while computing, which aborts the whole render tree.
    /// - `backdrop-filter`, `mask-size` and `mask-repeat` are dropped at parse time.
    /// - `mask-position` is resolved to pixels against the viewport; like `background-position`,
    ///   its percentages refer to the element's own box and must stay percentages.
    /// - `background-repeat` and `background-position` with more than one layer are parsed but
    ///   computed as empty, unlike `background-size`, `background-clip` or `background-origin`.
    /// </summary>
    [TestFixture]
    public class PaintPropertiesComputedStyleTests
    {
        private static ICssStyleDeclaration ComputedStyle(string style)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 400, ViewPortHeight = 300, FontSize = 16 });
            var parser = BrowsingContext.New(config).GetService<IHtmlParser>();
            var document = parser.ParseDocument($"<div id=target style=\"{style}\">x</div>");
            return document.GetElementById("target").ComputeCurrentStyle();
        }

        [TestCase("30", "30")]
        [TestCase("30%", "30%")]
        [TestCase("10 20 30 40", "10 20 30 40")]
        [TestCase("25% fill", "25% fill")]
        public void BorderImageSliceKeepsItsValues(string slice, string expected)
        {
            Assert.AreEqual(expected, ComputedStyle($"border-image-slice: {slice}").GetPropertyValue("border-image-slice"));
        }

        [TestCase("border-image-width: 2", "border-image-width", "2")]
        [TestCase("border-image-width: 1 8px", "border-image-width", "1 8px")]
        [TestCase("border-image-outset: 1.5", "border-image-outset", "1.5")]
        [TestCase("border-image: linear-gradient(red, blue) 25% / 1 / 4px round", "border-image-width", "1")]
        public void BorderImageNumbersAreMultiplesOfTheBorderWidth(string style, string property, string expected)
        {
            // A unitless number is not a length: computing it currently throws "Unsupported unit
            // cannot be converted", which aborts the whole render tree.
            Assert.AreEqual(expected, ComputedStyle(style).GetPropertyValue(property));
        }

        [Test]
        public void BackdropFilterIsComputed()
        {
            Assert.AreEqual("blur(4px) grayscale(50%)", ComputedStyle("backdrop-filter: blur(4px) grayscale(50%)").GetPropertyValue("backdrop-filter"));
        }

        [Test]
        public void MaskSizeIsComputed()
        {
            Assert.AreEqual("20px 30px, cover", ComputedStyle("mask-image: none, none; mask-size: 20px 30px, cover").GetPropertyValue("mask-size"));
        }

        [Test]
        public void MaskRepeatIsComputed()
        {
            Assert.AreEqual("no-repeat, space", ComputedStyle("mask-image: none, none; mask-repeat: no-repeat, space").GetPropertyValue("mask-repeat"));
        }

        [Test]
        public void MaskPositionPercentagesAreNotResolvedAgainstTheViewport()
        {
            var position = ComputedStyle("mask-image: none; mask-position: center").GetPropertyValue("mask-position");

            Assert.That(position, Does.Not.Contain("px"));
        }

        [Test]
        public void BackgroundRepeatKeepsEveryLayer()
        {
            Assert.AreEqual("space round, no-repeat", ComputedStyle("background-image: none, none; background-repeat: space round, no-repeat").GetPropertyValue("background-repeat"));
        }

        [Test]
        public void BackgroundPositionKeepsEveryLayer()
        {
            var position = ComputedStyle("background-image: none, none; background-position: left top, 30% 70%").GetPropertyValue("background-position");

            Assert.AreEqual(2, position.Split(',').Length, position);
            Assert.That(position, Does.Contain("30% 70%"));
        }
    }
}
