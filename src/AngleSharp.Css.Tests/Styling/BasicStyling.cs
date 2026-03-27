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

            Assert.That(document.StyleSheets.Length, Is.EqualTo(2));
            Assert.That(document.StyleSheetSets.Length, Is.EqualTo(2));
            Assert.That(link.IsPreferred(), Is.True);
            Assert.That(link.IsAlternate(), Is.False);
            Assert.That(link.IsPersistent(), Is.False);
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

            Assert.That(document.StyleSheets.Length, Is.EqualTo(2));
            Assert.That(document.StyleSheetSets.Length, Is.EqualTo(1));
            Assert.That(link.IsPreferred(), Is.False);
            Assert.That(link.IsAlternate(), Is.False);
            Assert.That(link.IsPersistent(), Is.True);
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

            Assert.That(document.StyleSheets.Length, Is.EqualTo(2));
            Assert.That(document.StyleSheetSets.Length, Is.EqualTo(2));
            Assert.That(link.IsPreferred(), Is.False);
            Assert.That(link.IsAlternate(), Is.True);
            Assert.That(link.IsPersistent(), Is.False);
        }

        [Test]
        public async Task GetComputedStyleFromHelperShouldBeOkay()
        {
            var source = "<!doctype html><head><style>p > span { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";
            var document = await CreateDocumentWithOptions(source);
            var element = document.QuerySelector("span.bold");
            Assert.That(element.LocalName, Is.EqualTo("span"));
            Assert.That(element.ClassName, Is.EqualTo("bold"));
            var style = element.ComputeCurrentStyle();
            Assert.IsNotNull(style);
            Assert.That(style.Length, Is.EqualTo(2));
        }

        [Test]
        public void CssStyleDeclarationEmpty()
        {
            var css = ParseDeclarations(String.Empty);
            Assert.That(css.CssText, Is.EqualTo(""));
            Assert.That(css.Length, Is.EqualTo(0));
        }

        [Test]
        public void CssStyleDeclarationUnbound()
        {
            var css = ParseDeclarations(String.Empty);
            css.CssText = "background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)";
            Assert.That(css.CssText, Is.EqualTo("background-color: rgba(255, 0, 0, 1); color: rgba(0, 0, 0, 1)"));
            Assert.That(css.Length, Is.EqualTo(2));
        }

        [Test]
        public void CssStyleDeclarationBoundOutboundDirectionIndirect()
        {
            var document = ParseDocument(String.Empty);
            var element = document.CreateElement<IHtmlSpanElement>();
            element.SetAttribute("style", "background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)");
            Assert.That(element.GetStyle().CssText, Is.EqualTo("background-color: rgba(255, 0, 0, 1); color: rgba(0, 0, 0, 1)"));
            Assert.That(element.GetStyle().Length, Is.EqualTo(2));
        }

        [Test]
        public void CssStyleDeclarationBoundOutboundDirectionDirect()
        {
            var document = ParseDocument(String.Empty);
            var element = document.CreateElement<IHtmlSpanElement>();
            element.SetAttribute("style", String.Empty);
            Assert.That(element.GetStyle().CssText, Is.EqualTo(String.Empty));
            element.SetAttribute("style", "background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)");
            Assert.That(element.GetStyle().CssText, Is.EqualTo("background-color: rgba(255, 0, 0, 1); color: rgba(0, 0, 0, 1)"));
            Assert.That(element.GetStyle().Length, Is.EqualTo(2));
        }

        [Test]
        public void CssStyleDeclarationBoundInboundDirection()
        {
            var document = ParseDocument(String.Empty);
            var element = document.CreateElement<IHtmlSpanElement>();
            element.SetStyle("background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)");
            Assert.That(element.GetAttribute("style"), Is.EqualTo("background-color: rgb(255, 0, 0); color: rgb(0, 0, 0)"));
            Assert.That(element.GetStyle().Length, Is.EqualTo(2));
        }

        [Test]
        public void MinifyRemovesComment()
        {
            var sheet = ParseStyleSheet("h1 /* this is a comment */ { color: red; /*another comment*/ }");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo("h1{color:rgba(255, 0, 0, 1)}"));
        }

        [Test]
        public void MinifyRemovesEmptyStyleRule()
        {
            var sheet = ParseStyleSheet("h1 {  }");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void MinifyRemovesEmptyStyleRuleKeepsOtherRule()
        {
            var sheet = ParseStyleSheet("h1 {  } h2 { font-size:0;  }");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo("h2{font-size:0}"));
        }

        [Test]
        public void MinifyRemovesEmptyMediaRule()
        {
            var sheet = ParseStyleSheet("@media screen { h1 {  } }");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void MinifyDoesNotRemovesMediaRuleIfOneStyleIsInThere()
        {
            var sheet = ParseStyleSheet("@media screen { h1 {  } h2 { top:0} }");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo("@media screen{h2{top:0}}"));
        }

        [Test]
        public void MinifyWorksWithNestedMediaRules()
        {
            var sheet = ParseStyleSheet("@media screen { @media screen{h1{}}div{border-top  :  none} }");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo("@media screen{div{border-top:none}}"));
        }

        [Test]
        public void MinifyWithMultipleDeclarations()
        {
            var sheet = ParseStyleSheet("h1 { top:0   ; left:   2px;  border: none;  } h2 { border: 1px solid red;} h3{}");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo("h1{top:0;left:2px;border:none}h2{border:1px solid rgba(255, 0, 0, 1)}"));
        }

        [Test]
        public void MinifyMinimizesProperties_Issue89()
        {
            var sheet = ParseStyleSheet("a { grid-area: aa / aa / aa / aa }");
            var result = sheet.ToCss(new MinifyStyleFormatter());
            Assert.That(result, Is.EqualTo("a{grid-area:aa}"));
        }
    }
}
