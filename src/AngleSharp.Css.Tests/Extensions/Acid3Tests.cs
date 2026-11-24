#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Behavior the Acid3 test (acid3.acidtests.org) relies on.
    /// </summary>
    [TestFixture]
    public class Acid3Tests
    {
        [TestCase("color: red ! important", "rgba(255, 0, 0, 1)")]
        [TestCase("color: red !important", "rgba(255, 0, 0, 1)")]
        [TestCase("color: red!IMPORTANT ", "rgba(255, 0, 0, 1)")]
        [TestCase("border: 1px solid ! important", "1px solid")]
        public void ImportantMayHaveWhitespaceAfterTheBang(System.String declaration, System.String expected)
        {
            var parser = new CssParser();
            var sheet = parser.ParseStyleSheet($"p {{ {declaration} }}");
            var style = ((ICssStyleRule)sheet.Rules[0]).Style;
            var name = declaration.Substring(0, declaration.IndexOf(':'));

            Assert.AreEqual("important", style.GetPropertyPriority(name));
            Assert.AreEqual(expected, style.GetPropertyValue(name));
        }

        [Test]
        public void ImportantNeedsTheBang()
        {
            var parser = new CssParser();
            var sheet = parser.ParseStyleSheet("p { color: red important }");
            var style = ((ICssStyleRule)sheet.Rules[0]).Style;

            Assert.AreEqual(System.String.Empty, style.GetPropertyPriority("color"));
        }

        [TestCase("all and (min-color: 0)", true)]
        [TestCase("(color)", true)]
        [TestCase("(min-color: 1)", true)]
        [TestCase("(max-color: 0)", false)]
        [TestCase("(monochrome)", false)]
        [TestCase("(max-monochrome: 0)", true)]
        [TestCase("(min-monochrome: 1)", false)]
        [TestCase("(max-color: 0) and (max-monochrome: 0)", false)]
        [TestCase("(min-color: 1), (min-monochrome: 1)", true)]
        [TestCase("not all and (min-color: 1), not all and (min-monochrome: 1)", true)]
        [TestCase("not all and (bogus)", false)]
        [TestCase("(bogus), all", true)]
        public void MediaQueriesOfAColorScreen(System.String query, System.Boolean expected)
        {
            var window = CreateWindow(new DefaultRenderDevice { ColorBits = 32, MonochromeBits = 0, ViewPortWidth = 1000, ViewPortHeight = 800 });
            Assert.AreEqual(expected, window.MatchMedia(query).IsMatched, query);
        }

        private static IWindow CreateWindow(IRenderDevice device)
        {
            var config = Configuration.Default.WithCss().WithRenderDevice(device);
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument("<!doctype html><title>Example</title>");
            return document.DefaultView;
        }
    }
}
