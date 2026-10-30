#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Valid values (current CSS specifications, shipped in browsers) that the parser rejects, so
    /// the whole declaration is silently dropped and the property computes as empty. Found by
    /// probing every property a downstream renderer (AngleSharp.Renderer) reads. Where the
    /// serialization of the computed value is not obvious the test only requires the
    /// declaration to survive (`*`). `justify-content`/`align-content: space-evenly` are in
    /// <see cref="FlexSpaceEvenlyComputedStyleTests"/>.
    /// </summary>
    [TestFixture]
    public class DroppedValidValuesComputedStyleTests
    {
        private static string Computed(string html, string property)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 400, ViewPortHeight = 300, FontSize = 16 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument(html);
            return document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue(property);
        }

        private static void AssertComputed(string property, string value, string expected)
        {
            var computed = Computed($"<style>#target {{ {property}: {value} }}</style><div id=target>x</div>", property);

            if (expected == "*")
            {
                Assert.That(computed, Is.Not.Empty, $"{property}: {value}");
            }
            else
            {
                Assert.AreEqual(expected, computed, $"{property}: {value}");
            }
        }

        // CSS Box Alignment 3
        [TestCase("justify-content", "start", "start")]
        [TestCase("justify-content", "end", "end")]
        [TestCase("justify-content", "left", "left")]
        [TestCase("justify-content", "right", "right")]
        [TestCase("justify-content", "normal", "normal")]
        [TestCase("justify-content", "stretch", "stretch")]
        [TestCase("justify-content", "safe center", "safe center")]
        [TestCase("justify-content", "unsafe end", "unsafe end")]
        [TestCase("align-content", "start", "start")]
        [TestCase("align-content", "end", "end")]
        [TestCase("align-content", "normal", "normal")]
        [TestCase("align-content", "baseline", "baseline")]
        [TestCase("align-items", "start", "start")]
        [TestCase("align-items", "end", "end")]
        [TestCase("align-items", "self-start", "self-start")]
        [TestCase("align-items", "self-end", "self-end")]
        [TestCase("align-items", "normal", "normal")]
        [TestCase("align-items", "first baseline", "baseline")]
        [TestCase("align-items", "last baseline", "last baseline")]
        [TestCase("align-self", "start", "start")]
        [TestCase("align-self", "end", "end")]
        [TestCase("align-self", "self-end", "self-end")]
        [TestCase("align-self", "normal", "normal")]
        [TestCase("justify-self", "start", "start")]
        [TestCase("justify-self", "end", "end")]
        [TestCase("justify-items", "start", "start")]
        [TestCase("justify-items", "legacy", "legacy")]
        [TestCase("place-self", "end", "end")]
        public void BoxAlignmentKeywordsAreKept(string property, string value, string expected) =>
            AssertComputed(property, value, expected);

        // CSS Flexbox, Sizing, Values and Units, Display
        [TestCase("flex-basis", "max-content", "max-content")]
        [TestCase("min-width", "auto", "auto")]
        [TestCase("min-height", "auto", "auto")]
        [TestCase("display", "contents", "contents")]
        [TestCase("display", "inline flex", "inline-flex")]
        [TestCase("display", "block flow-root", "flow-root")]
        [TestCase("display", "inline list-item", "inline list-item")]
        [TestCase("display", "ruby", "ruby")]
        [TestCase("width", "min(100px, 50px)", "50px")]
        [TestCase("width", "max(100px, 50px)", "100px")]
        [TestCase("width", "clamp(100px, 50px, 300px)", "100px")]
        [TestCase("font-size", "clamp(12px, 20px, 18px)", "18px")]
        [TestCase("height", "10svh", "30px")]
        [TestCase("height", "10lvh", "30px")]
        [TestCase("height", "10dvh", "30px")]
        [TestCase("aspect-ratio", "auto 4 / 3", "*")]
        [TestCase("float", "inline-start", "inline-start")]
        [TestCase("clear", "inline-end", "inline-end")]
        [TestCase("width", "stretch", "stretch")]
        [TestCase("width", "fit-content(100px)", "fit-content(100px)")]
        [TestCase("grid-template-columns", "subgrid", "subgrid")]
        [TestCase("content-visibility", "auto", "auto")]
        public void LayoutValuesAreKept(string property, string value, string expected) =>
            AssertComputed(property, value, expected);

        [Test]
        public void FlexNoneIsKept()
        {
            const string Html = "<style>#target { flex: none }</style><div id=target>x</div>";

            Assert.AreEqual("0", Computed(Html, "flex-grow"));
            Assert.AreEqual("0", Computed(Html, "flex-shrink"));
            Assert.AreEqual("auto", Computed(Html, "flex-basis"));
        }

        // CSS Color 4/5
        [TestCase("color", "hsl(120 100% 50%)", "rgba(0, 255, 0, 1)")]
        [TestCase("color", "hsl(120deg 100% 50% / 50%)", "rgba(0, 255, 0, 0.5)")]
        [TestCase("color", "hwb(120 0% 0%)", "rgba(0, 255, 0, 1)")]
        [TestCase("color", "color-mix(in srgb, red 50%, blue)", "*")]
        [TestCase("color", "light-dark(red, blue)", "*")]
        [TestCase("opacity", "50%", "0.5")]
        [TestCase("box-shadow", "0 0 0 1px currentcolor", "*")]
        [TestCase("mix-blend-mode", "plus-lighter", "plus-lighter")]
        public void ColorValuesAreKept(string property, string value, string expected) =>
            AssertComputed(property, value, expected);

        [TestCase("currentcolor")]
        [TestCase("currentColor")]
        public void ColorCurrentColorIsTheInheritedColor(string value)
        {
            Assert.AreEqual("rgba(255, 0, 0, 1)", Computed($"<div style=\"color: red\"><p id=target style=\"color: blue; color: {value}\">x</p></div>", "color"));
        }

        // CSS Backgrounds 3/4, Images 4, Masking
        [TestCase("background-position", "right 10px bottom 20px", "right 10px bottom 20px")]
        [TestCase("background-position", "right 10px top", "*")]
        [TestCase("background-position-x", "right 10px", "right 10px")]
        [TestCase("background-position-y", "bottom 5px", "bottom 5px")]
        [TestCase("background-image", "image-set(\"a.png\" 1x)", "*")]
        [TestCase("background-clip", "text", "text")]
        [TestCase("border-image-repeat", "space", "space")]
        [TestCase("outline-style", "auto", "auto")]
        [TestCase("image-rendering", "pixelated", "pixelated")]
        [TestCase("transform-box", "fill-box", "fill-box")]
        public void PaintValuesAreKept(string property, string value, string expected) =>
            AssertComputed(property, value, expected);

        // CSS Fonts 4, Text 3/4, Overflow
        [TestCase("font-weight", "550", "550")]
        [TestCase("font-style", "oblique 10deg", "oblique 10deg")]
        [TestCase("font-variant-numeric", "tabular-nums", "tabular-nums")]
        [TestCase("font-variant-ligatures", "none", "none")]
        [TestCase("font-variant-east-asian", "proportional-width", "proportional-width")]
        [TestCase("overflow-wrap", "anywhere", "anywhere")]
        [TestCase("word-wrap", "anywhere", "anywhere")]
        [TestCase("white-space", "break-spaces", "break-spaces")]
        [TestCase("text-indent", "2em hanging", "*")]
        [TestCase("text-overflow", "clip ellipsis", "clip ellipsis")]
        public void TextValuesAreKept(string property, string value, string expected) =>
            AssertComputed(property, value, expected);

        // CSS Easing 2, Transitions 2
        [TestCase("transition-timing-function", "steps(4, jump-end)", "*")]
        [TestCase("transition-timing-function", "linear(0, 0.5 50%, 1)", "*")]
        [TestCase("transition-behavior", "allow-discrete", "allow-discrete")]
        public void AnimationValuesAreKept(string property, string value, string expected) =>
            AssertComputed(property, value, expected);
    }
}
