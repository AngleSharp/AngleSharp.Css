#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// A `box-shadow` or `text-shadow` layer without a blur radius (`2px 2px red`, the most common
    /// short form) makes `ComputeCurrentStyle` throw a `NullReferenceException`, so a downstream
    /// renderer (AngleSharp.Renderer) cannot render the document at all. Layers with a blur radius
    /// compute fine.
    /// </summary>
    [TestFixture]
    public class ShadowWithoutBlurComputedStyleTests
    {
        private static ICssStyleDeclaration ComputedStyle(string html)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 400, ViewPortHeight = 300, FontSize = 16 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument(html);
            return document.GetElementById("target").ComputeCurrentStyle();
        }

        [TestCase("box-shadow", "2px 2px red")]
        [TestCase("box-shadow", "2px 2px red, 3px 3px blue")]
        [TestCase("box-shadow", "inset 0 0 5px red, 2px 2px red")]
        [TestCase("text-shadow", "1px 1px red")]
        [TestCase("text-shadow", "1px 1px red, 0 0 2px blue")]
        public void ShadowWithoutBlurRadiusComputes(string property, string value)
        {
            string computed = null;

            Assert.DoesNotThrow(() => computed = ComputedStyle($"<div id=target style=\"{property}: {value}\">x</div>").GetPropertyValue(property));
            Assert.That(computed, Does.Contain("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void ShadowWithoutBlurRadiusDoesNotBreakOtherProperties()
        {
            string computed = null;

            Assert.DoesNotThrow(() => computed = ComputedStyle("<div id=target style=\"box-shadow: 2px 2px red; width: 10px\">x</div>").GetPropertyValue("width"));
            Assert.AreEqual("10px", computed);
        }
    }
}
