#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The computed value of `font-weight` is a number: the relative keywords `bolder` and
    /// `lighter` resolve against the parent's computed weight (CSS Fonts 4, section 2.2.1), and
    /// descendants inherit that number, not the keyword. Found while rendering `&lt;b&gt;` and
    /// `&lt;strong&gt;` (whose UA rule is `font-weight: bolder`) in a downstream renderer
    /// (AngleSharp.Renderer): the computed style reports the literal keyword `bolder` for the
    /// element itself and for every descendant, so a consumer cannot know the actual weight
    /// without re-implementing the cascade, and a nested element applies `bolder` once more.
    /// </summary>
    [TestFixture]
    public class FontWeightComputedStyleTests
    {
        private static string ComputedFontWeight(string html, string id)
        {
            var document = ParseDocument(html);
            return document.GetElementById(id).ComputeCurrentStyle().GetPropertyValue("font-weight");
        }

        [Test]
        public void UaBolderOnBoldElementResolvesAgainstNormalParent()
        {
            Assert.AreEqual("700", ComputedFontWeight("<p><b id=target>Bold</b></p>", "target"));
        }

        [Test]
        public void UaBolderOnStrongElementResolvesAgainstNormalParent()
        {
            Assert.AreEqual("700", ComputedFontWeight("<p><strong id=target>Bold</strong></p>", "target"));
        }

        [Test]
        public void ChildOfBoldElementInheritsTheResolvedWeight()
        {
            Assert.AreEqual("700", ComputedFontWeight("<p><b>Bold <i id=target>bold italic</i></b></p>", "target"));
        }

        [Test]
        public void BolderResolvesAgainstABoldParent()
        {
            Assert.AreEqual("900", ComputedFontWeight("<p style=\"font-weight: 700\"><span id=target style=\"font-weight: bolder\">x</span></p>", "target"));
        }

        [Test]
        public void BolderResolvesAgainstALightParent()
        {
            Assert.AreEqual("400", ComputedFontWeight("<p style=\"font-weight: 100\"><span id=target style=\"font-weight: bolder\">x</span></p>", "target"));
        }

        [Test]
        public void LighterResolvesAgainstABoldParent()
        {
            Assert.AreEqual("400", ComputedFontWeight("<p style=\"font-weight: 700\"><span id=target style=\"font-weight: lighter\">x</span></p>", "target"));
        }

        [Test]
        public void LighterResolvesAgainstANormalParent()
        {
            Assert.AreEqual("100", ComputedFontWeight("<p><span id=target style=\"font-weight: lighter\">x</span></p>", "target"));
        }

        [Test]
        public void NestedBoldElementsResolveAgainstEachOtherOnlyOnce()
        {
            // <b> in <b>: the inner one resolves `bolder` against its parent's 700, giving 900.
            Assert.AreEqual("900", ComputedFontWeight("<p><b>outer <b id=target>inner</b></b></p>", "target"));
        }

        [Test]
        public void HeadingUaBolderResolvesToBold()
        {
            Assert.AreEqual("700", ComputedFontWeight("<h1 id=target>Title</h1>", "target"));
        }
    }
}
