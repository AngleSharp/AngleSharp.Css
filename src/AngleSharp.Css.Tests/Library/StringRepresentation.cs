namespace AngleSharp.Css.Tests.Library
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.IO;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    [TestFixture]
    public class StringRepresentationTests
    {
        [Test]
        public void PrettyStyleFormatterStringifyShouldWork_Issue41()
        {
            var text = "@media (min-width: 800px) { .ad_column { width: 728px; height: 90px } }";
            var parser = new CssParser();
            var document = parser.ParseStyleSheet(text);

            using (var stringWriter = new StringWriter())
            {
                document.ToCss(stringWriter, new PrettyStyleFormatter());
                Assert.AreEqual("@media (min-width: 800px) { \n\t.ad_column {\n\t\twidth: 728px;\n\t\theight: 90px;\n\t}\n}", stringWriter.ToString());
            }
        }

        [Test]
        public void SimpleColorWorksWithHexOutput_Issue96()
        {
            var color = new Color(65, 12, 48);
            Color.UseHex = true;
            var text = color.CssText;
            Color.UseHex = false;
            Assert.AreEqual("#410C30", text);
        }

        [Test]
        public void TransparentColorWorksWithHexOutput_Issue132()
        {
            var color = new Color(65, 12, 48, 10);
            Color.UseHex = true;
            var text = color.CssText;
            Color.UseHex = false;
            Assert.AreEqual("#410C300A", text);
        }

        [Test]
        public void ShorthandPaddingInheritPropertiesShouldBeIncluded_Issue100()
        {
            var html = @"<style>#x div { padding: inherit }</style>";
            var dom = ParseDocument(html);
            var styleSheet = dom.StyleSheets[0] as ICssStyleSheet;
            var css = styleSheet.ToCss();
            Assert.AreEqual("#x div { padding: inherit }", css);
        }

        [Test]
        public void ShorthandMarginInheritPropertiesShouldBeIncluded_Issue100()
        {
            var html = @"<style>#x div { margin: inherit }</style>";
            var dom = ParseDocument(html);
            var styleSheet = dom.StyleSheets[0] as ICssStyleSheet;
            var css = styleSheet.ToCss();
            Assert.AreEqual("#x div { margin: inherit }", css);
        }

        [Test]
        public void ShorthandBorderInheritPropertiesShouldBeIncluded_Issue100()
        {
            var html = @"<style>#x div { border: inherit }</style>";
            var dom = ParseDocument(html);
            var styleSheet = dom.StyleSheets[0] as ICssStyleSheet;
            var css = styleSheet.ToCss();
            Assert.AreEqual("#x div { border: inherit }", css);
        }

        [Test]
        public void ConicGradientNotParsedCorrectly_Issue101()
        {
            var html = @"<style>div { background: conic-gradient(red, yellow, green); }</style>";
            var dom = ParseDocument(html);
            var styleSheet = dom.StyleSheets[0] as ICssStyleSheet;
            var css = styleSheet.ToCss();
            Assert.AreEqual("div { background: conic-gradient(rgba(255, 0, 0, 1), rgba(255, 255, 0, 1), rgba(0, 128, 0, 1)) }", css);
        }

        [Test]
        public void EscapePropertyNames_CustomProperty_Issue120()
        {
            var css = @".class { --\/\%\$\!: value }";
            var styleSheet = ParseStyleSheet(css);

            var generatedCss = styleSheet.ToCss();

            Assert.AreEqual(css, generatedCss);
        }

        [Test]
        public void EscapePropertyNames_UnknownDeclaration_Issue120()
        {
            var css = @".class { \/\%\$\!: value }";
            var styleSheet = ParseStyleSheet(css, new CssParserOptions{ IsIncludingUnknownDeclarations = true });

            var generatedCss = styleSheet.ToCss();

            Assert.AreEqual(css, generatedCss);
        }

        [Test]
        public void CssTextShouldNotAddReplacementCharacter_Issue123()
        {
            var html = @"<span style=""background-image: var(--urlSpellingErrorV2,url(&quot;https://www.example.com/))"">Ipsum</span>";
            var dom = html.ToHtmlDocument(Configuration.Default.WithCss(new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidSelectors = true,
            }));
            var div = dom.Body?.FirstElementChild;
            var style = div.GetStyle();
            var css = style.ToCss();

            Assert.AreEqual("background-image: var(--urlSpellingErrorV2,url(\"https://www.example.com/))", css);
        }

        [Test]
        public void CssTextShouldNotTrailingSemicolonCharacter_Issue123()
        {
            var html = @"<span style=""color: red;"">Ipsum</span>";
            var dom = html.ToHtmlDocument(Configuration.Default.WithCss(new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidSelectors = true,
            }));
            var div = dom.Body?.FirstElementChild;
            var style = div.GetStyle();
            var css = style.ToCss();

            Assert.AreEqual("color: rgba(255, 0, 0, 1)", css);
        }

        [Test]
        public void BorderWithEmptyPx_Issue129()
        {
            var html = "<div style=\"border-width:1px;border-right-width:px;\"></div>";
            var dom = html.ToHtmlDocument(Configuration.Default.WithCss());
            var div = dom.Body?.FirstElementChild;
            var style = div.GetStyle();
            var css = style.ToCss();

            Assert.AreEqual("border-width: 1px", css);
        }

        [Test]
        public async Task MediaListForLinkedStyleSheet_Issue133()
        {
            var html = "<link href=\"style.css\" rel=\"stylesheet\">";
            var mockRequester = new MockRequester();
            mockRequester.BuildResponse(request =>
            {
                if (request.Address.Path.EndsWith("style.css"))
                {
                    return "div#A   { color: blue;	}";
                }

                return null;
            });
            var config = Configuration.Default.WithCss().WithMockRequester(mockRequester);
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync((res) => res.Content(html));
            var link = document.QuerySelector<IHtmlLinkElement>("link");
            Assert.AreEqual("", link.Sheet.Media.MediaText);
            Assert.IsTrue(link.Sheet.Media.Validate(new DefaultRenderDevice()));
        }

        [Test]
        public async Task ExternalCssNotConsidered_Issue140()
        {
            var html = @"<html>
        <head><link href=""https://some/tested/url.css"" rel=""stylesheet""></head>
        <body><label>HI</label></body>
      </html>";
            var mockRequester = new MockRequester();
            mockRequester.BuildResponse(request =>
            {
                if (request.Address.Path.EndsWith("url.css"))
                {
                    return @"label, .test {
  min-width: 50px;
  border: 1px solid green;
}";
                }

                return null;
            });
            var config = Configuration.Default
                .WithRenderDevice(new DefaultRenderDevice
                {
                    DeviceWidth = 1920,
                    DeviceHeight = 1080,
                })
                .WithCss()
                .WithMockRequester(mockRequester);
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync((res) => res.Content(html));
            var window = document.DefaultView;
            var tree = window.Render();
            var label = tree.Find(document.QuerySelector("label"));
            var minWidth = window.GetComputedStyle(label.Ref as IHtmlElement).GetMinWidth();

            Assert.AreEqual("50px", minWidth);
        }
    }
}
