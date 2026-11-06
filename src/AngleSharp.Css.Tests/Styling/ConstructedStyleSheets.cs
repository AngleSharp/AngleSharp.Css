#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    [TestFixture]
    public class ConstructedStyleSheetsTests
    {
        [Test]
        public async Task ConstructedSheetSupportsOptionsAndReplacement()
        {
            var document = ParseDocument("<p>Text</p>");
            var sheet = new CssStyleSheet(document.DefaultView, "https://example.com/styles/", "screen", true);

            Assert.IsNull(sheet.OwnerNode);
            Assert.IsNull(sheet.Href);
            Assert.AreEqual("screen", sheet.Media.MediaText);
            Assert.IsTrue(sheet.IsDisabled);

            sheet.ReplaceSync("p { background-image: url(image.png) } @import 'ignored.css';");

            Assert.AreEqual(1, sheet.Rules.Length);
            StringAssert.Contains("https://example.com/styles/image.png", sheet.ToCss());

            var replaced = await sheet.Replace("p { color: blue }");

            Assert.AreSame(sheet, replaced);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.AreEqual("rgba(0, 0, 255, 1)", ((ICssStyleRule)sheet.Rules[0]).Style.GetPropertyValue("color"));
        }

        [Test]
        public async Task ReplaceMethodsRequireAConstructedSheet()
        {
            var sheet = ParseStyleSheet("p { color: red }");
            var syncError = Assert.Throws<DomException>(() => sheet.ReplaceSync(String.Empty));
            Assert.AreEqual("NotAllowedError", syncError.Name);

            var asyncError = Assert.ThrowsAsync<DomException>(async () => await sheet.Replace(String.Empty));
            Assert.AreEqual("NotAllowedError", asyncError.Name);
        }

        [Test]
        public void InsertRuleRejectsImportOnConstructedSheet()
        {
            var document = ParseDocument(String.Empty);
            var sheet = new CssStyleSheet(document.DefaultView);

            var error = Assert.Throws<DomException>(() => sheet.Insert("@import url(styles.css);", 0));

            Assert.AreEqual("SyntaxError", error.Name);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public async Task ConstructedSheetUsesDocumentAddressAsItsDefaultBaseUrl()
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = await context.OpenAsync(m => m.Content("<p>Text</p>").Address("https://example.com/page/"));
            var sheet = new CssStyleSheet(document.DefaultView);

            sheet.ReplaceSync("p { background-image: url(image.png) }");

            StringAssert.Contains("https://example.com/page/image.png", sheet.ToCss());
        }

        [Test]
        public void AdoptedSheetsApplyAfterDocumentSheetsAndCanBeMutated()
        {
            var document = ParseDocument("<style>p { color: red }</style><p>Text</p>");
            var sheet = new CssStyleSheet(document.DefaultView);
            sheet.ReplaceSync("p { color: blue }");
            document.SetAdoptedStyleSheets(new[] { sheet });
            Assert.AreEqual(1, document.StyleSheets.Length);

            var paragraph = document.QuerySelector("p");
            Assert.AreEqual("rgba(0, 0, 255, 1)", document.DefaultView.GetComputedStyle(paragraph).GetColor());

            sheet.ReplaceSync("p { color: green }");
            Assert.AreEqual("rgba(0, 128, 0, 1)", document.DefaultView.GetComputedStyle(paragraph).GetColor());

            sheet.IsDisabled = true;
            Assert.AreEqual("rgba(255, 0, 0, 1)", document.DefaultView.GetComputedStyle(paragraph).GetColor());

            sheet.IsDisabled = false;
            Assert.AreEqual("rgba(0, 128, 0, 1)", document.DefaultView.GetComputedStyle(paragraph).GetColor());

            document.GetAdoptedStyleSheets().RemoveAt(0);

            Assert.AreEqual("rgba(255, 0, 0, 1)", document.DefaultView.GetComputedStyle(paragraph).GetColor());
        }

        [Test]
        public void AdoptedSheetsMustBeConstructedForTheSameDocument()
        {
            var firstDocument = ParseDocument(String.Empty);
            var secondDocument = ParseDocument(String.Empty);
            var sheet = new CssStyleSheet(firstDocument.DefaultView);

            var error = Assert.Throws<DomException>(() => secondDocument.SetAdoptedStyleSheets(new[] { sheet }));

            Assert.AreEqual("NotAllowedError", error.Name);
            Assert.AreEqual(0, secondDocument.GetAdoptedStyleSheets().Count);
        }
    }
}
