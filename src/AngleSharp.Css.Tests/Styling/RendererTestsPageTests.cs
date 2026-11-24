#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using static AngleSharp.Css.ValueConverters;

    /// <summary>
    /// Found on the renderer test pages (anglesharp-tests.anglevisions.com/renderer/): gradients of
    /// CSS Images 4, @scope, the pseudo-elements a renderer styles, dialogs and shadow trees.
    /// </summary>
    [TestFixture]
    public class RendererTestsPageTests
    {
        private const string Green = "rgba(0, 128, 0, 1)";

        private static async Task<IDocument> OpenAsync(string html)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            return await context.OpenAsync(req => req.Content("<!doctype html>" + html));
        }

        [TestCase("linear-gradient(black, 20%, white)")]
        [TestCase("linear-gradient(red 10%, 30%, blue 80%)")]
        [TestCase("linear-gradient(in oklch, blue, red)")]
        [TestCase("linear-gradient(90deg in hsl longer hue, red, red)")]
        [TestCase("radial-gradient(circle in oklab, red, blue)")]
        [TestCase("conic-gradient(in oklch from 10deg, red, blue)")]
        public void GradientOfImages4IsValid(string source)
        {
            Assert.IsNotNull(GradientConverter.Convert(source));
        }

        [TestCase("linear-gradient(red, 10%)")]
        [TestCase("linear-gradient(10%, red, blue)")]
        [TestCase("linear-gradient(red, 10%, 20%, blue)")]
        [TestCase("linear-gradient(in bogus, red, blue)")]
        [TestCase("radial-gradient(closest-side in oklab at 10% 10%, red, blue)")]
        public void GradientWithMisplacedHintOrInterpolationIsInvalid(string source)
        {
            Assert.IsNull(GradientConverter.Convert(source));
        }

        [Test]
        public void ColorHintIsSerializedAsItsPosition()
        {
            var gradient = (CssLinearGradientValue)GradientConverter.Convert("linear-gradient(90deg in oklch, black, 20%, white)");
            Assert.AreEqual("linear-gradient(90deg in oklch, rgba(0, 0, 0, 1), 20%, rgba(255, 255, 255, 1))", gradient.CssText);
            Assert.AreEqual("in oklch", gradient.Interpolation);
            Assert.IsTrue(((CssGradientStopValue)gradient.Stops[1]).IsHint);
        }

        [Test]
        public void ConicGradientWithAngleAndCenterIsOneArgument()
        {
            var gradient = GradientConverter.Convert("conic-gradient(from 90deg at 30% 70%, black, white)");
            StringAssert.StartsWith("conic-gradient(from 90deg at 30% 70%, ", gradient.CssText);
        }

        [Test]
        public async Task ScopedRulesApplyBetweenRootAndLimit()
        {
            var document = await OpenAsync("<style>.t { color: red } @scope (.card) to (.content) { .t { color: green } }</style>" +
                "<div class=card><span class=t id=in>in</span><div class=content><span class=t id=out>out</span></div></div><span class=t id=outside>x</span>");
            var window = document.DefaultView;

            Assert.AreEqual(Green, window.GetComputedStyle(document.GetElementById("in")).GetColor());
            Assert.AreEqual("rgba(255, 0, 0, 1)", window.GetComputedStyle(document.GetElementById("out")).GetColor());
            Assert.AreEqual("rgba(255, 0, 0, 1)", window.GetComputedStyle(document.GetElementById("outside")).GetColor());
        }

        [TestCase("::marker", "<style>li::marker { color: green }</style><ul><li id=t>A</ul>")]
        [TestCase("::first-line", "<style>p::first-line { color: green }</style><p id=t>A</p>")]
        [TestCase("::first-letter", "<style>p::first-letter { color: green }</style><p id=t>A</p>")]
        public async Task PseudoElementOfARendererIsStyled(string pseudo, string html)
        {
            var document = await OpenAsync(html);
            Assert.AreEqual(Green, document.DefaultView.GetComputedStyle(document.GetElementById("t"), pseudo).GetColor());
        }

        [Test]
        public async Task ModalDialogMatchesModalAndClosedOneIsHidden()
        {
            var document = await OpenAsync("<dialog id=d>A</dialog>");
            var dialog = (IHtmlDialogElement)document.GetElementById("d");

            Assert.AreEqual("none", document.DefaultView.GetComputedStyle(dialog).GetDisplay());
            dialog.ShowModal();
            Assert.IsTrue(dialog.Matches(":modal"));
            Assert.AreEqual("fixed", document.DefaultView.GetComputedStyle(dialog).GetPosition());
            dialog.Close();
            Assert.IsFalse(dialog.Matches(":modal"));
        }

        [Test]
        public async Task ShadowTreeIsStyledByItsOwnSheetsAndInheritsFromTheHost()
        {
            var document = await OpenAsync("<style>.label { color: red } #host { color: green }</style><div id=host></div>");
            var host = document.GetElementById("host");
            var root = host.AttachShadow();
            root.InnerHtml = "<style>b { font-weight: 300 }</style><span class=label><b>A</b></span>";
            var window = document.DefaultView;

            Assert.AreEqual(Green, window.GetComputedStyle(root.QuerySelector(".label")).GetColor());
            Assert.AreEqual("300", window.GetComputedStyle(root.QuerySelector("b")).GetFontWeight());
        }
    }
}
