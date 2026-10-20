#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Computed values of text properties a downstream renderer (AngleSharp.Renderer) reads while
    /// implementing small caps, font stretch and justification:
    /// - `font-variant-caps` is not a known property, so `all-small-caps` is dropped and the
    ///   `font-variant` shorthand does not set it.
    /// - `font-stretch` drops percentage values (`75%`).
    /// - `text-align-last` is not inherited, unlike what CSS Text 3 specifies.
    /// </summary>
    [TestFixture]
    public class TextPropertiesComputedStyleTests
    {
        private static ICssStyleDeclaration ComputedStyle(string html, string id = "target")
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 400, ViewPortHeight = 300, FontSize = 16 });
            var parser = BrowsingContext.New(config).GetService<IHtmlParser>();
            var document = parser.ParseDocument(html);
            return document.GetElementById(id).ComputeCurrentStyle();
        }

        [TestCase("small-caps")]
        [TestCase("all-small-caps")]
        [TestCase("petite-caps")]
        [TestCase("unicase")]
        public void FontVariantCapsIsComputed(string value)
        {
            Assert.AreEqual(value, ComputedStyle($"<p id=target style=\"font-variant-caps: {value}\">x</p>").GetPropertyValue("font-variant-caps"));
        }

        [Test]
        public void FontVariantShorthandSetsFontVariantCaps()
        {
            Assert.AreEqual("small-caps", ComputedStyle("<p id=target style=\"font-variant: small-caps\">x</p>").GetPropertyValue("font-variant-caps"));
        }

        [Test]
        public void FontVariantCapsIsInherited()
        {
            Assert.AreEqual("small-caps", ComputedStyle("<p style=\"font-variant-caps: small-caps\"><span id=target>x</span></p>").GetPropertyValue("font-variant-caps"));
        }

        [TestCase("75%", "75%")]
        [TestCase("condensed", "condensed")]
        public void FontStretchAcceptsPercentagesAndKeywords(string value, string expected)
        {
            Assert.AreEqual(expected, ComputedStyle($"<p id=target style=\"font-stretch: {value}\">x</p>").GetPropertyValue("font-stretch"));
        }

        [Test]
        public void TextAlignLastIsInherited()
        {
            Assert.AreEqual("center", ComputedStyle("<p style=\"text-align-last: center\"><span id=target>x</span></p>").GetPropertyValue("text-align-last"));
        }
    }
}
