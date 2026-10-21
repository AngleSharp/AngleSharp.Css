#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Relative values are resolved against the wrong reference while computing styles. Found in
    /// a downstream renderer (AngleSharp.Renderer), which paints `font-size: 2em` inside a 20px
    /// parent at 32px.
    /// - `em` always resolves against the device's default font size (16px) instead of the
    ///   element's own font size (the parent's for `font-size` itself), and `rem` ignores the
    ///   root element's font size.
    /// - Percentages are resolved against the viewport width: lengths whose percentages refer to
    ///   the containing block (or another box) must either stay percentages or resolve against
    ///   that box, and `font-size`/`line-height`/`vertical-align` percentages refer to the parent
    ///   font size, the element's font size and the element's line height.
    /// - Relative `font-size` keywords (`larger`, `smaller`) and absolute ones (`medium`,
    ///   `x-large`) are not turned into lengths.
    /// The viewport is 400px wide here and the parent 200px, so the two cases are distinguishable.
    /// </summary>
    [TestFixture]
    public class RelativeValueComputedStyleTests
    {
        private static string Computed(string html, string property)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 400, ViewPortHeight = 300, FontSize = 16 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument(html);
            return document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue(property);
        }

        [TestCase("font-size: 2em", "font-size", "40px")]
        [TestCase("width: 2em", "width", "40px")]
        [TestCase("margin-top: 1em", "margin-top", "20px")]
        [TestCase("padding-left: 1em", "padding-left", "20px")]
        [TestCase("line-height: 2em", "line-height", "40px")]
        [TestCase("font-size: 10px; width: 2em", "width", "20px")]
        [TestCase("font-size: 10px; letter-spacing: 1em", "letter-spacing", "10px")]
        [TestCase("font-size: 10px; line-height: 2em", "line-height", "20px")]
        public void EmResolvesAgainstTheElementFontSize(string declarations, string property, string expected)
        {
            Assert.AreEqual(expected, Computed($"<div style=\"font-size: 20px\"><p id=target style=\"{declarations}\">x</p></div>", property));
        }

        [Test]
        public void RemResolvesAgainstTheRootFontSize()
        {
            Assert.AreEqual("20px", Computed("<html style=\"font-size: 10px\"><body><p id=target style=\"width: 2rem\">x</p></body></html>", "width"));
        }

        [TestCase("width: 50%", "width", "50%", "100px")]
        [TestCase("margin-left: 10%", "margin-left", "10%", "20px")]
        [TestCase("padding-top: 5%", "padding-top", "5%", "10px")]
        [TestCase("text-indent: 10%", "text-indent", "10%", "20px")]
        [TestCase("width: calc(50% + 10px)", "width", "calc(50% + 10px)", "110px")]
        [TestCase("flex: 2 1 50%", "flex-basis", "50%", "100px")]
        public void PercentagesDoNotResolveAgainstTheViewport(string declarations, string property, string percentage, string resolved)
        {
            var computed = Computed($"<div style=\"width: 200px\"><p id=target style=\"{declarations}\">x</p></div>", property);

            Assert.That(computed, Is.AnyOf(percentage, resolved));
        }

        [TestCase("background-size: 50% auto", "background-size", "50% auto", "50%")]
        [TestCase("object-position: right top", "object-position", "right top", "100% 0%")]
        [TestCase("row-gap: 5%", "row-gap", "5%", "5%")]
        public void PercentagesOfOtherBoxesStayPercentages(string declarations, string property, string expected, string alternative)
        {
            var computed = Computed($"<div style=\"width: 200px\"><p id=target style=\"{declarations}\">x</p></div>", property);

            Assert.That(computed, Is.AnyOf(expected, alternative));
        }

        [Test]
        public void FontSizePercentageResolvesAgainstTheParentFontSize()
        {
            Assert.AreEqual("24px", Computed("<div style=\"font-size: 20px\"><p id=target style=\"font-size: 120%\">x</p></div>", "font-size"));
        }

        [Test]
        public void LineHeightPercentageResolvesAgainstTheElementFontSize()
        {
            Assert.AreEqual("30px", Computed("<div style=\"font-size: 20px\"><p id=target style=\"line-height: 150%\">x</p></div>", "line-height"));
        }

        [Test]
        public void InheritedLineHeightPercentageIsTheParentsAbsoluteLength()
        {
            Assert.AreEqual("30px", Computed("<div style=\"font-size: 20px; line-height: 150%\"><p id=target style=\"font-size: 10px\">x</p></div>", "line-height"));
        }

        [Test]
        public void VerticalAlignPercentageResolvesAgainstTheLineHeight()
        {
            var computed = Computed("<div style=\"width: 200px\"><span id=target style=\"line-height: 20px; vertical-align: -20%\">x</span></div>", "vertical-align");

            Assert.That(computed, Is.AnyOf("-20%", "-4px"));
        }

        [TestCase("medium", "16px")]
        [TestCase("x-large", "24px")]
        public void AbsoluteFontSizeKeywordsComputeToLengths(string keyword, string expected)
        {
            Assert.AreEqual(expected, Computed($"<div style=\"font-size: 20px\"><p id=target style=\"font-size: {keyword}\">x</p></div>", "font-size"));
        }

        [TestCase("larger")]
        [TestCase("smaller")]
        public void RelativeFontSizeKeywordsComputeToLengths(string keyword)
        {
            var computed = Computed($"<div style=\"font-size: 20px\"><p id=target style=\"font-size: {keyword}\">x</p></div>", "font-size");

            Assert.That(computed, Does.EndWith("px"));
        }
    }
}
