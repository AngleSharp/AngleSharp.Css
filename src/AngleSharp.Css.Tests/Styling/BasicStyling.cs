namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    [TestFixture]
    public class BasicStylingTests
    {
        private static Task<IDocument> CreateDocumentWithOptions(String source)
        {
            var mockRequester = new MockRequester();
            mockRequester.BuildResponse(request =>
            {
                if (request.Address.Path.EndsWith("a.css"))
                {
                    return "div#A   { color: blue;	}";
                }
                else if (request.Address.Path.EndsWith("b.css"))
                {
                    return "div#B   { color: red;   }";
                }

                return null;
            });
            var config = Configuration.Default.WithCss().WithMockRequester(mockRequester);
            var context = BrowsingContext.New(config);
            return context.OpenAsync(m => m.Content(source));
        }

        [Test]
        public async Task ExternalStyleSheetIsPreferred()
        {
            var source = @"<!doctype html>
<html>
    <head>
    	<link rel=""stylesheet"" media=""screen"" type=""text/css"" title=""A"" href=""a.css"" />
    	<link rel=""stylesheet alternate"" media=""screen"" type=""text/css"" title=""B"" href=""b.css"" />
    </head>
</html>";
            var document = await CreateDocumentWithOptions(source);
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            Assert.AreEqual(2, document.StyleSheets.Length);
            Assert.AreEqual(2, document.StyleSheetSets.Length);
            Assert.IsTrue(link.IsPreferred());
            Assert.IsFalse(link.IsAlternate());
            Assert.IsFalse(link.IsPersistent());
        }

        [Test]
        public async Task ExternalStyleSheetIsPersistent()
        {
            var source = @"<!doctype html>
<html>
    <head>
    	<link rel=""stylesheet"" media=""screen"" type=""text/css"" href=""a.css"" />
    	<link rel=""stylesheet alternate"" media=""screen"" type=""text/css"" title=""B"" href=""b.css"" />
    </head>
</html>";
            var document = await CreateDocumentWithOptions(source);
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            Assert.AreEqual(2, document.StyleSheets.Length);
            Assert.AreEqual(1, document.StyleSheetSets.Length);
            Assert.IsFalse(link.IsPreferred());
            Assert.IsFalse(link.IsAlternate());
            Assert.IsTrue(link.IsPersistent());
        }

        [Test]
        public async Task ExternalStyleSheetIsAlternate()
        {
            var source = @"<!doctype html>
<html>
    <head>
    	<link rel=""stylesheet alternate"" media=""screen"" type=""text/css"" title=""A"" href=""a.css"" />
    	<link rel=""stylesheet"" media=""screen"" type=""text/css"" title=""B"" href=""b.css"" />
    </head>
</html>";
            var document = await CreateDocumentWithOptions(source);
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            Assert.AreEqual(2, document.StyleSheets.Length);
            Assert.AreEqual(2, document.StyleSheetSets.Length);
            Assert.IsFalse(link.IsPreferred());
            Assert.IsTrue(link.IsAlternate());
            Assert.IsFalse(link.IsPersistent());
        }

        [Test]
        public async Task GetComputedStyleFromHelperShouldBeOkay()
        {
            var source = "<!doctype html><head><style>p > span { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";
            var document = await CreateDocumentWithOptions(source);
            var element = document.QuerySelector("span.bold");
            Assert.AreEqual("span", element.LocalName);
            Assert.AreEqual("bold", element.ClassName);
            var style = element.ComputeCurrentStyle();
            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.Length);
        }

        [Test]
        public void CssStyleDeclarationEmpty()
        {
            var css = ParseDeclarations(String.Empty);
            Assert.AreEqual("", css.CssText);
            Assert.AreEqual(0, css.Length);
        }

        [Test]
        public void CssStyleDeclarationUnbound()
        {
            var css = ParseDeclarations(String.Empty);
            css.CssText = "background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)";
            Assert.AreEqual("background-color: rgba(255, 0, 0, 1); color: rgba(0, 0, 0, 1)", css.CssText);
            Assert.AreEqual(2, css.Length);
        }

        [Test]
        public void CssStyleDeclarationBoundOutboundDirectionIndirect()
        {
            var document = ParseDocument(String.Empty);
            var element = document.CreateElement<IHtmlSpanElement>();
            element.SetAttribute("style", "background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)");
            Assert.AreEqual("background-color: rgba(255, 0, 0, 1); color: rgba(0, 0, 0, 1)", element.GetStyle().CssText);
            Assert.AreEqual(2, element.GetStyle().Length);
        }

        [Test]
        public void CssStyleDeclarationBoundOutboundDirectionDirect()
        {
            var document = ParseDocument(String.Empty);
            var element = document.CreateElement<IHtmlSpanElement>();
            element.SetAttribute("style", String.Empty);
            Assert.AreEqual(String.Empty, element.GetStyle().CssText);
            element.SetAttribute("style", "background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)");
            Assert.AreEqual("background-color: rgba(255, 0, 0, 1); color: rgba(0, 0, 0, 1)", element.GetStyle().CssText);
            Assert.AreEqual(2, element.GetStyle().Length);
        }

        [Test]
        public void CssStyleDeclarationBoundInboundDirection()
        {
            var document = ParseDocument(String.Empty);
            var element = document.CreateElement<IHtmlSpanElement>();
            element.SetStyle("background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)");
            Assert.AreEqual("background-color: rgba(255, 0, 0, 1); color: rgba(0, 0, 0, 1)", element.GetAttribute("style"));
            Assert.AreEqual(2, element.GetStyle().Length);
        }
    }
}
