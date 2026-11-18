#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Computed styles asked for one after another reuse the indexed rules of the document, as
    /// long as the rules stay the same; any change of them is seen by the next computed style.
    /// </summary>
    [TestFixture]
    public class ComputedStyleCollectionReuseTests
    {
        private static Task<IDocument> LoadAsync(string body) =>
            BrowsingContext.New(Configuration.Default.WithCss()).OpenAsync(m => m.Content("<!doctype html>" + body));

        private static string ColorOf(IDocument document) =>
            document.DefaultView.GetComputedStyle(document.GetElementById("t")).GetColor();

        [Test]
        public async Task AddedStyleSheetIsSeen()
        {
            var document = await LoadAsync("<style>p { color: red }</style><p id=t>A</p>");
            Assert.AreEqual("rgba(255, 0, 0, 1)", ColorOf(document));

            var style = document.CreateElement("style");
            style.TextContent = "p { color: green }";
            document.Head.AppendChild(style);

            Assert.AreEqual("rgba(0, 128, 0, 1)", ColorOf(document));
        }

        [Test]
        public async Task InsertedAndDeletedRulesAreSeen()
        {
            var document = await LoadAsync("<style>p { color: red }</style><p id=t>A</p>");
            var sheet = document.StyleSheets.OfType<ICssStyleSheet>().First();
            Assert.AreEqual("rgba(255, 0, 0, 1)", ColorOf(document));

            sheet.Insert("#t { color: green }", 1);
            Assert.AreEqual("rgba(0, 128, 0, 1)", ColorOf(document));

            // the same number of rules, but another one
            sheet.RemoveAt(1);
            sheet.Insert("#t { color: blue }", 1);
            Assert.AreEqual("rgba(0, 0, 255, 1)", ColorOf(document));
        }

        [Test]
        public async Task ChangedSelectorIsSeen()
        {
            var document = await LoadAsync("<style>p { color: red } div { color: green }</style><p id=t>A</p>");
            var rule = document.StyleSheets.OfType<ICssStyleSheet>().First().Rules.OfType<ICssStyleRule>().Last();
            Assert.AreEqual("rgba(255, 0, 0, 1)", ColorOf(document));

            rule.SelectorText = "p";

            Assert.AreEqual("rgba(0, 128, 0, 1)", ColorOf(document));
        }

        [Test]
        public async Task DisabledStyleSheetIsSeen()
        {
            var document = await LoadAsync("<style>p { color: red }</style><style>p { color: green }</style><p id=t>A</p>");
            Assert.AreEqual("rgba(0, 128, 0, 1)", ColorOf(document));

            document.StyleSheets.Last().IsDisabled = true;

            Assert.AreEqual("rgba(255, 0, 0, 1)", ColorOf(document));
        }
    }
}
