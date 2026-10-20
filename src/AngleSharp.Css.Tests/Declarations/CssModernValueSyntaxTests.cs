#nullable disable
namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Covers the parsing and serialization of value syntax from current CSS specifications,
    /// next to the cases in <see cref="Styling.DroppedValidValuesComputedStyleTests"/>.
    /// </summary>
    [TestFixture]
    public class CssModernValueSyntaxTests
    {
        private static void AssertValue(string declaration, string expected)
        {
            var property = ParseDeclaration(declaration);

            if (expected is null)
            {
                Assert.IsFalse(property.HasValue, declaration);
            }
            else
            {
                Assert.IsTrue(property.HasValue, declaration);
                Assert.AreEqual(expected, property.Value, declaration);
            }
        }

        [TestCase("display: inline flow-root", "inline-block")]
        [TestCase("display: flex inline", "inline-flex")]
        [TestCase("display: block table", "table")]
        [TestCase("display: inline grid", "inline-grid")]
        [TestCase("display: block flow", "block")]
        [TestCase("display: flow", "block")]
        [TestCase("display: block ruby", "block ruby")]
        [TestCase("display: list-item inline", "inline list-item")]
        [TestCase("display: block flow-root list-item", "flow-root list-item")]
        [TestCase("display: table list-item", null)]
        [TestCase("display: block inline", null)]
        public void DisplayMultiKeywordSyntax(string declaration, string expected) => AssertValue(declaration, expected);

        [TestCase("justify-items: right legacy", "legacy right")]
        [TestCase("justify-items: legacy center", "legacy center")]
        [TestCase("align-self: unsafe self-end", "unsafe self-end")]
        [TestCase("align-items: safe baseline", null)]
        [TestCase("justify-content: space-evenly", "space-evenly")]
        [TestCase("place-content: space-evenly start", "space-evenly start")]
        public void BoxAlignmentSyntax(string declaration, string expected) => AssertValue(declaration, expected);

        [TestCase("transition-timing-function: steps(3, jump-none)", "steps(3, jump-none)")]
        [TestCase("transition-timing-function: steps(2, jump-both)", "steps(2, jump-both)")]
        [TestCase("transition-timing-function: steps(2, jump-start)", "steps(2, jump-start)")]
        [TestCase("transition-timing-function: steps(1, jump-none)", null)]
        [TestCase("transition-timing-function: steps(2, jump-sideways)", null)]
        [TestCase("transition-timing-function: linear(0, 0.25 25% 75%, 1)", "linear(0, 0.25 25% 75%, 1)")]
        [TestCase("transition-timing-function: linear(0)", null)]
        [TestCase("transition-behavior: normal, allow-discrete", "normal, allow-discrete")]
        public void EasingSyntax(string declaration, string expected) => AssertValue(declaration, expected);

        [TestCase("color: hsl(none 100% 50%)", "rgba(255, 0, 0, 1)")]
        [TestCase("color: hsl(120 100 50)", "rgba(0, 255, 0, 1)")]
        [TestCase("color: hwb(0 0% 0% / 0.5)", "rgba(255, 0, 0, 0.5)")]
        [TestCase("color: color-mix(in srgb, red, blue)", "rgba(128, 0, 128, 1)")]
        [TestCase("color: color-mix(in srgb, red 25%, blue 25%)", "rgba(128, 0, 128, 0.5)")]
        [TestCase("color: light-dark(red, blue)", "rgba(255, 0, 0, 1)")]
        [TestCase("color: currentcolor", "currentColor")]
        [TestCase("color: color-mix(srgb, red, blue)", null)]
        public void ColorSyntax(string declaration, string expected) => AssertValue(declaration, expected);

        [TestCase("background-image: image-set(\"a.png\" 1x type(\"image/png\"), url(b.png) 2x)", "image-set(\"a.png\" 1x type(\"image/png\"), url(\"b.png\") 2x)")]
        [TestCase("background-position-x: right 10px, 20%", "right 10px, 20%")]
        [TestCase("object-position: left 10px top 5%", "left 10px top 5%")]
        [TestCase("transform-origin: right bottom 2cm", "right bottom 2cm")]
        [TestCase("background-clip: text, border-box", "text, border-box")]
        [TestCase("border-style: auto", null)]
        [TestCase("opacity: 50%", "50%")]
        public void PaintSyntax(string declaration, string expected) => AssertValue(declaration, expected);

        [TestCase("font-weight: 550", "550")]
        [TestCase("font-weight: 1001", null)]
        [TestCase("font-style: oblique 10deg", "oblique 10deg")]
        [TestCase("font-style: oblique", "oblique")]
        [TestCase("font-variant-numeric: slashed-zero oldstyle-nums", "oldstyle-nums slashed-zero")]
        [TestCase("font-variant-numeric: lining-nums oldstyle-nums", null)]
        [TestCase("font-variant-ligatures: no-common-ligatures contextual", "no-common-ligatures contextual")]
        [TestCase("text-indent: each-line 5% hanging", "5% hanging each-line")]
        [TestCase("text-indent: hanging", null)]
        [TestCase("text-overflow: clip ellipsis", "clip ellipsis")]
        public void TextSyntax(string declaration, string expected) => AssertValue(declaration, expected);

        [TestCase("grid-template-columns: subgrid [a] [b c]", "subgrid [a] [b c]")]
        [TestCase("aspect-ratio: 16/9 auto", "auto 16/9")]
        [TestCase("width: fit-content", "fit-content")]
        [TestCase("width: fit-content(50%)", "fit-content(50%)")]
        [TestCase("max-width: stretch", "stretch")]
        [TestCase("max-width: auto", null)]
        [TestCase("width: calc(min(10px, 20%) + 5px)", "calc(min(10px, 20%) + 5px)")]
        [TestCase("width: clamp(10px, 20px)", null)]
        [TestCase("height: 50dvmin", "50dvmin")]
        [TestCase("transform-box: fill-box", "fill-box")]
        public void LayoutSyntax(string declaration, string expected) => AssertValue(declaration, expected);
    }
}
