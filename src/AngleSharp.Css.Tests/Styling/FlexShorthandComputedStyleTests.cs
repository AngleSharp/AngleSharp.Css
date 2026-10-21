#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The `flex` shorthand does not expand into the longhands CSS Flexbox 1 (7.1.1) prescribes:
    /// a unitless `flex: &lt;grow&gt; [&lt;shrink&gt;]` must set `flex-basis` to `0%` (it is left
    /// unset, so `flex: 1` sizes items from their content), a basis-only `flex: &lt;basis&gt;` must
    /// set `flex-grow` to 1 (left unset, so the item never grows), and omitted factors must take
    /// `1`. The computed shorthand, rebuilt from the longhands, is also lossy: a lone
    /// `flex-basis: 40px` reads back as `flex: 40px`, which would mean `1 1 40px`. Found in a
    /// downstream renderer (AngleSharp.Renderer), which therefore has to read the longhands only.
    /// </summary>
    [TestFixture]
    public class FlexShorthandComputedStyleTests
    {
        [TestCase("1", "1", "1", "0%")]
        [TestCase("2 3", "2", "3", "0%")]
        [TestCase("1 50px", "1", "1", "50px")]
        [TestCase("100px", "1", "1", "100px")]
        [TestCase("auto", "1", "1", "auto")]
        [TestCase("initial", "0", "1", "auto")]
        public void FlexShorthandExpandsIntoItsLonghands(string flex, string grow, string shrink, string basis)
        {
            var style = Compute($"flex: {flex}");

            Assert.AreEqual(grow, style.GetPropertyValue("flex-grow"));
            Assert.AreEqual(shrink, style.GetPropertyValue("flex-shrink"));
            Assert.AreEqual(basis, style.GetPropertyValue("flex-basis"));
        }

        [TestCase("flex-basis: 40px", "0 1 40px")]
        [TestCase("flex-grow: 2", "2 1 auto")]
        [TestCase("flex-shrink: 0", "0 0 auto")]
        public void ComputedFlexShorthandKeepsTheLonghandsMeaning(string declaration, string expected)
        {
            Assert.AreEqual(expected, Compute(declaration).GetPropertyValue("flex"));
        }

        private static ICssStyleDeclaration Compute(string declarations)
        {
            var document = ParseDocument($"<div style=\"display: flex\"><div id=target style=\"{declarations}\">x</div></div>");
            return document.GetElementById("target").ComputeCurrentStyle();
        }
    }
}
