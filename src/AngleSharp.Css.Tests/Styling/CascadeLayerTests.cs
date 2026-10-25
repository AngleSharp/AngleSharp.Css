#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    /// <summary>
    /// Rules inside `@layer` blocks were left out of the cascade entirely. Found on GitHub, which
    /// puts its design systems into layers (e.g. a tooltip's `display: none` never applied).
    /// The cascade orders layers by their first appearance, with unlayered rules last, a layer's
    /// own rules after its sub-layers, and the order reversed for important declarations
    /// (CSS Cascade 5, 6.4).
    /// </summary>
    [TestFixture]
    public class CascadeLayerTests
    {
        private const string Green = "rgba(0, 128, 0, 1)";

        private static async Task<string> ColorOfAsync(string css)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = await context.OpenAsync(req => req.Content($"<!doctype html><style>{css}</style><p id=t>A</p>"));
            return document.DefaultView.GetComputedStyle(document.GetElementById("t")).GetColor();
        }

        [TestCase("@layer x { p { color: green } }")]
        [TestCase("@media screen { @layer x { p { color: green } } }")]
        [TestCase("p { color: green } @layer x { #t { color: red } }")]
        [TestCase("@layer x { #t { color: red } } p { color: green }")]
        [TestCase("@layer a { #t { color: red } } @layer b { p { color: green } }")]
        [TestCase("@layer b, a; @layer a { p { color: green } } @layer b { #t { color: red } }")]
        [TestCase("@layer a { #t { color: red } } @layer b { p { color: red } } @layer a { p.x, p { color: blue } } @layer b { p { color: green } }")]
        [TestCase("@layer a { p { color: green } @layer b { #t { color: red } } }")]
        [TestCase("@layer a.b { #t { color: red } } @layer a { p { color: green } }")]
        [TestCase("@layer { #t { color: red } } @layer { p { color: green } }")]
        [TestCase("@layer { #t { color: red } } p { color: green }")]
        [TestCase("@layer a { p { color: green !important } } @layer b { p { color: red !important } }")]
        [TestCase("p { color: red !important } @layer a { p { color: green !important } }")]
        [TestCase("@layer a { p { color: green !important } } p { color: red }")]
        [TestCase("@layer a { #t { color: red } p { color: green !important } } @layer b { #t { color: blue } }")]
        public async Task LayersTakePartInTheCascade(string css)
        {
            Assert.AreEqual(Green, await ColorOfAsync(css));
        }
    }
}
