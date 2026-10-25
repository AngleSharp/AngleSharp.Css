#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    /// <summary>
    /// Attributes like the width of an img were ignored, so images showed in their natural size.
    /// Found on MDN. They map to CSS (HTML 15.3 - 15.4) like author rules of no specificity,
    /// which precede all other author rules.
    /// </summary>
    [TestFixture]
    public class PresentationalHintTests
    {
        private static async Task<ICssStyleDeclaration> StyleOfAsync(string css, string html)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = await context.OpenAsync(req => req.Content($"<!doctype html><style>{css}</style>{html}"));
            return document.DefaultView.GetComputedStyle(document.GetElementById("t"));
        }

        // an empty value means none was set
        [TestCase("<img id=t width=40 height=30>", "40px", "30px")]
        [TestCase("<img id=t width='40.5' height=' 30px'>", "40.5px", "30px")]
        [TestCase("<img id=t width=50%>", "50%", "")]
        [TestCase("<img id=t width=abc height=-3>", "", "")]
        [TestCase("<video id=t width=40 height=30></video>", "40px", "30px")]
        [TestCase("<iframe id=t width=40 height=30></iframe>", "40px", "30px")]
        [TestCase("<input id=t type=image width=40 height=30>", "40px", "30px")]
        [TestCase("<input id=t type=text width=40 height=30>", "", "")]
        [TestCase("<div id=t width=40 height=30></div>", "", "")]
        [TestCase("<table><tr><td id=t width=0 height=30></td></tr></table>", "", "30px")]
        public async Task DimensionAttributesMapToTheirProperties(string html, string width, string height)
        {
            var style = await StyleOfAsync("", html);

            Assert.AreEqual(width, style.GetWidth());
            Assert.AreEqual(height, style.GetHeight());
        }

        [TestCase("img { width: 20px }", "20px")]
        [TestCase("* { width: 20px }", "20px")]
        [TestCase("", "40px")]
        public async Task AuthorRulesOverrideTheHints(string css, string width)
        {
            var style = await StyleOfAsync(css, "<img id=t width=40>");

            Assert.AreEqual(width, style.GetWidth());
        }

        [Test]
        public async Task InlineStylesOverrideTheHints()
        {
            var style = await StyleOfAsync("", "<img id=t width=40 style='width: 20px'>");

            Assert.AreEqual("20px", style.GetWidth());
        }

        [TestCase("<table><tr><td id=t bgcolor=red></td></tr></table>")]
        [TestCase("<table><tr id=t bgcolor='#ff0000'><td></td></tr></table>")]
        [TestCase("<table id=t bgcolor=red></table>")]
        public async Task BackgroundColorAttributesMapToTheBackgroundColor(string html)
        {
            var style = await StyleOfAsync("", html);

            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetBackgroundColor());
        }
    }
}
