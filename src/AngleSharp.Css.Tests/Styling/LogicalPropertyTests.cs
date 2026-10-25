#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    /// <summary>
    /// Flow-relative properties like padding-inline were not mapped to the physical sides, so
    /// GitHub's repository tabs had no padding. They share one cascade with the physical ones
    /// (CSS Logical Properties 1 §4).
    /// </summary>
    [TestFixture]
    public class LogicalPropertyTests
    {
        private static async Task<ICssStyleDeclaration> StyleOfAsync(string css)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = await context.OpenAsync(req => req.Content($"<!doctype html><style>#t {{ {css} }}</style><div id=t></div>"));
            return document.DefaultView.GetComputedStyle(document.GetElementById("t"));
        }

        // top right bottom left
        [TestCase("padding-inline: 8px 2px", " 2px  8px")]
        [TestCase("padding-inline: var(--p, 8px)", " 8px  8px")]
        [TestCase("padding-block-start: 3px; padding-inline-end: 4px", "3px 4px  ")]
        [TestCase("padding: 1px; padding-inline-start: 8px", "1px 1px 1px 8px")]
        [TestCase("padding-inline-start: 8px; padding: 1px", "1px 1px 1px 1px")]
        [TestCase("padding-left: 3px !important; padding-inline-start: 8px", "   3px")]
        [TestCase("direction: rtl; padding-inline-start: 8px", " 8px  ")]
        [TestCase("writing-mode: vertical-rl; padding-block-start: 8px; padding-inline-start: 2px", "2px 8px  ")]
        [TestCase("writing-mode: vertical-lr; padding-block-start: 8px", "   8px")]
        public async Task LogicalPaddingMapsToThePhysicalSides(string css, string expected)
        {
            var style = await StyleOfAsync(css);

            Assert.AreEqual(expected, $"{style.GetPaddingTop()} {style.GetPaddingRight()} {style.GetPaddingBottom()} {style.GetPaddingLeft()}");
        }

        [Test]
        public async Task LogicalMarginsInsetsAndSizesMapToPhysicalOnes()
        {
            var style = await StyleOfAsync("margin-inline-end: var(--m, 4px); inset-inline-start: 5px; border-block-end-width: 2px; inline-size: 50px; max-block-size: 20px");

            Assert.AreEqual("4px", style.GetMarginRight());
            Assert.AreEqual("5px", style.GetLeft());
            Assert.AreEqual("2px", style.GetPropertyValue("border-bottom-width"));
            Assert.AreEqual("50px", style.GetWidth());
            Assert.AreEqual("20px", style.GetMaxHeight());
        }

        [Test]
        public async Task SingleGapSetsBothGaps()
        {
            var style = await StyleOfAsync("gap: var(--g, 8px)");

            Assert.AreEqual("8px", style.GetPropertyValue("row-gap"));
            Assert.AreEqual("8px", style.GetPropertyValue("column-gap"));
        }
    }
}
