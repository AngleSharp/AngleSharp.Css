namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    [TestFixture]
    public class HtmlCssIntegrationTests
    {
        [Test]
        public void DetectStylesheet()
        {
            var source = @"<!DOCTYPE html>

<html>
<head>
    <meta charset=""utf-8"" />
    <title></title>
    <style>
        body
        {
            background-color: green !important;
        }
    </style>
</head>
<body>
</body>
</html>";
            var doc = ParseDocument(source);
            Assert.That(doc.StyleSheets.Length, Is.EqualTo(1));
            var css = doc.StyleSheets[0] as CssStyleSheet;
            Assert.That(css.Rules.Length, Is.EqualTo(1));
            var style = css.Rules[0] as CssStyleRule;
            Assert.That(style.SelectorText, Is.EqualTo("body"));
            Assert.That(style.Style.Length, Is.EqualTo(1));
            var decl = style.Style;
            Assert.IsInstanceOf<CssStyleDeclaration>(decl);
            var rule = decl.GetProperty("background-color");
            Assert.That(rule.IsImportant, Is.True);
            Assert.That(rule.Name, Is.EqualTo("background-color"));
            Assert.That(decl[0], Is.EqualTo(rule.Name));
            Assert.That(rule.Value, Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void ParsedCssCanHaveExtraWhitespace()
        {
            var source = "<div style=\"background-color: http://www.codeplex.com?url=<!--[if gte IE 4]><SCRIPT>alert('XSS');</SCRIPT><![endif]-->\">";
            var doc = ParseDocument(source, new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true
            });
            var div = doc.QuerySelector<IHtmlElement>("div");
            Assert.That(div.GetStyle()["background-color"], Is.EqualTo(""));
            Assert.That(div.GetStyle().CssText, Is.EqualTo(""));
        }

        [Test]
        public async Task CssWithImportRuleShouldBeAbleToHandleNestedStylesheets()
        {
            var files = new Dictionary<String, String>
            {
                { "index.html", "<!doctype html><html><link rel=stylesheet href=origin.css type=text/css><style>@import url('linked2.css');</style>" },
                { "origin.css", "@import url(linked1.css);" },
                { "linked1.css", "" },
                { "linked2.css", "@import url(\"linked3.css\"); @import 'linked4.css';" },
                { "linked3.css", "" },
                { "linked4.css", "" },
            };
            var requester = new TestServerRequester(files);
            var config = Configuration.Default
                .With(requester)
                .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true })
                .WithCss();
            var document = await BrowsingContext.New(config).OpenAsync("http://localhost/index.html");
            var link = document.QuerySelector<IHtmlLinkElement>("link");
            var style = document.QuerySelector<IHtmlStyleElement>("style");

            await Task.Delay(100);

            Assert.IsNotNull(link);
            Assert.IsNotNull(style);

            var origin = link.Sheet as ICssStyleSheet;
            Assert.IsNotNull(origin);
            Assert.That(origin.Href, Is.EqualTo("http://localhost/origin.css"));
            Assert.That(origin.Rules.Length, Is.EqualTo(1));
            Assert.That(origin.Rules[0].Type, Is.EqualTo(CssRuleType.Import));

            var linked1 = (origin.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked1);
            Assert.That(linked1.Href, Is.EqualTo("http://localhost/linked1.css"));
            Assert.That(linked1.Rules.Length, Is.EqualTo(0));

            var styleSheet = style.Sheet as ICssStyleSheet;
            Assert.IsNotNull(styleSheet);
            Assert.That(styleSheet.Href, Is.EqualTo(null));
            Assert.That(styleSheet.Rules.Length, Is.EqualTo(1));
            Assert.That(styleSheet.Rules[0].Type, Is.EqualTo(CssRuleType.Import));

            var linked2 = (styleSheet.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked2);
            Assert.That(linked2.Href, Is.EqualTo("http://localhost/linked2.css"));
            Assert.That(linked2.Rules.Length, Is.EqualTo(2));
            Assert.That(linked2.Rules[0].Type, Is.EqualTo(CssRuleType.Import));
            Assert.That(linked2.Rules[1].Type, Is.EqualTo(CssRuleType.Import));

            var linked3 = (linked2.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked3);
            Assert.That(linked3.Href, Is.EqualTo("http://localhost/linked3.css"));
            Assert.That(linked3.Rules.Length, Is.EqualTo(0));

            var linked4 = (linked2.Rules[1] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked4);
            Assert.That(linked4.Href, Is.EqualTo("http://localhost/linked4.css"));
            Assert.That(linked4.Rules.Length, Is.EqualTo(0));
        }

        [Test]
        public async Task CssWithImportRuleShouldStopRecursion()
        {
            var files = new Dictionary<String, String>
            {
                { "index.html", "<!doctype html><html><link rel=stylesheet href=origin.css type=text/css>" },
                { "origin.css", "@import url(linked.css);" },
                { "linked.css", "@import url(origin.css);" },
            };
            var requester = new TestServerRequester(files);
            var config = Configuration.Default
                .With(requester)
                .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true })
                .WithCss();
            var document = await BrowsingContext.New(config).OpenAsync("http://localhost/index.html");
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            await Task.Delay(100);

            Assert.IsNotNull(link);

            var origin = link.Sheet as ICssStyleSheet;
            Assert.IsNotNull(origin);
            Assert.That(origin.Href, Is.EqualTo("http://localhost/origin.css"));
            Assert.That(origin.Rules.Length, Is.EqualTo(1));
            Assert.That(origin.Rules[0].Type, Is.EqualTo(CssRuleType.Import));

            var linked = (origin.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked);
            Assert.That(linked.Href, Is.EqualTo("http://localhost/linked.css"));
            Assert.That(linked.Rules.Length, Is.EqualTo(1));

            var originAborted = (linked.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNull(originAborted);
        }

        [Test]
        public async Task StylePropertyOfElementFromDocumentWithCssShouldNotBeNull()
        {
            var config = Configuration.Default.WithCss();
            var document = await BrowsingContext.New(config).OpenNewAsync();
            var div = document.CreateElement<IHtmlDivElement>();
            Assert.IsNotNull(div.GetStyle());
        }

        [Test]
        public async Task StylePropertyOfClonedElementShouldNotBeNull()
        {
            var config = Configuration.Default.WithCss();
            var document = await BrowsingContext.New(config).OpenNewAsync();
            var div = document.CreateElement<IHtmlDivElement>();
            var clone = div.Clone(true) as IHtmlDivElement;
            Assert.IsNotNull(clone);
            Assert.IsNotNull(clone.GetStyle());
        }

        [Test]
        public void TableWithTableRowThatHasStyle()
        {
            var doc = ParseDocument(@"<table><tr style=""display: none;"">");

            var dochtml0 = doc.ChildNodes[0] as IElement;
            Assert.That(dochtml0.ChildNodes.Length, Is.EqualTo(2));
            Assert.That(dochtml0.Attributes.Length, Is.EqualTo(0));
            Assert.That(dochtml0.GetTagName(), Is.EqualTo("html"));
            Assert.That(dochtml0.NodeType, Is.EqualTo(NodeType.Element));

            var dochtml0head0 = dochtml0.ChildNodes[0] as IElement;
            Assert.That(dochtml0head0.ChildNodes.Length, Is.EqualTo(0));
            Assert.That(dochtml0head0.Attributes.Length, Is.EqualTo(0));
            Assert.That(dochtml0head0.GetTagName(), Is.EqualTo("head"));
            Assert.That(dochtml0head0.NodeType, Is.EqualTo(NodeType.Element));

            var dochtml0body1 = dochtml0.ChildNodes[1] as IElement;
            Assert.That(dochtml0body1.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(dochtml0body1.Attributes.Length, Is.EqualTo(0));
            Assert.That(dochtml0body1.GetTagName(), Is.EqualTo("body"));
            Assert.That(dochtml0body1.NodeType, Is.EqualTo(NodeType.Element));

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as IElement;
            Assert.That(dochtml0body1table0.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(dochtml0body1table0.Attributes.Length, Is.EqualTo(0));
            Assert.That(dochtml0body1table0.GetTagName(), Is.EqualTo("table"));
            Assert.That(dochtml0body1table0.NodeType, Is.EqualTo(NodeType.Element));

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as IElement;
            Assert.That(dochtml0body1table0tbody0.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(dochtml0body1table0tbody0.Attributes.Length, Is.EqualTo(0));
            Assert.That(dochtml0body1table0tbody0.GetTagName(), Is.EqualTo("tbody"));
            Assert.That(dochtml0body1table0tbody0.NodeType, Is.EqualTo(NodeType.Element));

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as IElement;
            Assert.That(dochtml0body1table0tbody0tr0.ChildNodes.Length, Is.EqualTo(0));
            Assert.That(dochtml0body1table0tbody0tr0.Attributes.Length, Is.EqualTo(1));
            Assert.That(dochtml0body1table0tbody0tr0.GetTagName(), Is.EqualTo("tr"));
            Assert.That(dochtml0body1table0tbody0tr0.NodeType, Is.EqualTo(NodeType.Element));

            var styleAttribute = dochtml0body1table0tbody0tr0.Attributes[0];
            Assert.That(styleAttribute.Name, Is.EqualTo("style"));
            Assert.That(styleAttribute.Value, Is.EqualTo("display: none;"));

            var style = ((IHtmlElement)dochtml0body1table0tbody0tr0).GetStyle();
            Assert.That(style.GetDisplay(), Is.EqualTo("none"));
        }

        [Test]
        public void TableWithTableRowThatHasStyleAndChanged()
        {
            var doc = ParseDocument(@"<table><tr style=""display: none;"">");

            var html = doc.ChildNodes[0] as IElement;
            Assert.That(html.ChildNodes.Length, Is.EqualTo(2));
            Assert.That(html.Attributes.Length, Is.EqualTo(0));
            Assert.That(html.GetTagName(), Is.EqualTo("html"));
            Assert.That(html.NodeType, Is.EqualTo(NodeType.Element));

            var body = html.ChildNodes[1] as IElement;
            Assert.That(body.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(body.Attributes.Length, Is.EqualTo(0));
            Assert.That(body.GetTagName(), Is.EqualTo("body"));
            Assert.That(body.NodeType, Is.EqualTo(NodeType.Element));

            var table = body.ChildNodes[0] as IElement;
            Assert.That(table.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(table.Attributes.Length, Is.EqualTo(0));
            Assert.That(table.GetTagName(), Is.EqualTo("table"));
            Assert.That(table.NodeType, Is.EqualTo(NodeType.Element));

            var tableBody = table.ChildNodes[0] as IElement;
            Assert.That(tableBody.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(tableBody.Attributes.Length, Is.EqualTo(0));
            Assert.That(tableBody.GetTagName(), Is.EqualTo("tbody"));
            Assert.That(tableBody.NodeType, Is.EqualTo(NodeType.Element));

            var tableRow = tableBody.ChildNodes[0] as IElement;
            Assert.That(tableRow.ChildNodes.Length, Is.EqualTo(0));
            Assert.That(tableRow.Attributes.Length, Is.EqualTo(1));
            Assert.That(tableRow.GetTagName(), Is.EqualTo("tr"));
            Assert.That(tableRow.NodeType, Is.EqualTo(NodeType.Element));

            var tr = (IHtmlElement)tableRow;
            var style = tr.GetStyle();
            Assert.That(style.GetDisplay(), Is.EqualTo("none"));

            style.SetDisplay("block");
            Assert.That(style.GetDisplay(), Is.EqualTo("block"));
        }

        [Test]
        public void TableWithTableRowThatHasNoStyleAndChanged()
        {
            var doc = ParseDocument(@"<table><tr>");

            var html = doc.ChildNodes[0] as IElement;
            Assert.That(html.ChildNodes.Length, Is.EqualTo(2));
            Assert.That(html.Attributes.Length, Is.EqualTo(0));
            Assert.That(html.GetTagName(), Is.EqualTo("html"));
            Assert.That(html.NodeType, Is.EqualTo(NodeType.Element));

            var body = html.ChildNodes[1] as IElement;
            Assert.That(body.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(body.Attributes.Length, Is.EqualTo(0));
            Assert.That(body.GetTagName(), Is.EqualTo("body"));
            Assert.That(body.NodeType, Is.EqualTo(NodeType.Element));

            var table = body.ChildNodes[0] as IElement;
            Assert.That(table.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(table.Attributes.Length, Is.EqualTo(0));
            Assert.That(table.GetTagName(), Is.EqualTo("table"));
            Assert.That(table.NodeType, Is.EqualTo(NodeType.Element));

            var tableBody = table.ChildNodes[0] as IElement;
            Assert.That(tableBody.ChildNodes.Length, Is.EqualTo(1));
            Assert.That(tableBody.Attributes.Length, Is.EqualTo(0));
            Assert.That(tableBody.GetTagName(), Is.EqualTo("tbody"));
            Assert.That(tableBody.NodeType, Is.EqualTo(NodeType.Element));

            var tableRow = tableBody.ChildNodes[0] as IElement;
            Assert.That(tableRow.ChildNodes.Length, Is.EqualTo(0));
            Assert.That(tableRow.Attributes.Length, Is.EqualTo(0));
            Assert.That(tableRow.GetTagName(), Is.EqualTo("tr"));
            Assert.That(tableRow.NodeType, Is.EqualTo(NodeType.Element));

            var tr = (IHtmlElement)tableRow;
            var style = tr.GetStyle();

            style.SetDisplay("none");
            Assert.That(style.GetDisplay(), Is.EqualTo("none"));
        }

        [Test]
        public void SetStyleAttributeAfterPageLoadWithInvalidColor()
        {
            var source = "<Div style=\"background-color: http://www.codeplex.com?url=<SCRIPT>a=/XSS/alert(a.source)</SCRIPT>\">";
            var document = ParseDocument(source);
            var div = (IHtmlElement)document.QuerySelector("div");
            var n = div.GetStyle().Length;
            // hang occurs only if this line is executed prior to setting the attribute
            // hang occurs when executing next line
            div.SetAttribute("style", "background-color: http://www.codeplex.com?url=&lt;SCRIPT&gt;a=/XSS/alert(a.source)&lt;/SCRIPT&gt;");
            Assert.That(div.GetStyle().GetBackgroundColor(), Is.EqualTo(""));
        }

        [Test]
        public void ExtensionCssWithOneElement()
        {
            var document = ParseDocument("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.That(elements.Length, Is.EqualTo(1));

            var style = (elements[0] as IHtmlElement).GetStyle();
            Assert.That(style.Length, Is.EqualTo(1));

            Assert.That(style[0], Is.EqualTo("color"));
            Assert.That(style.GetColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void ExtensionCssWithOneElementButMultipleCssRules()
        {
            var document = ParseDocument("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Css(new
            {
                color = "red",
                background = "green",
                font = "10px 'Tahoma'",
                opacity = "0.5"
            });
            Assert.That(elements.Length, Is.EqualTo(1));

            var style = (elements[0] as IHtmlElement).GetStyle();

            Assert.That(style.GetColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(style.GetBackgroundColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
            Assert.That(style.GetFontFamily(), Is.EqualTo("\"Tahoma\""));
            Assert.That(style.GetFontSize(), Is.EqualTo("10px"));
            Assert.That(style.GetOpacity(), Is.EqualTo("0.5"));
        }

        [Test]
        public void ExtensionCssWithMultipleElements()
        {
            var document = ParseDocument("<ul><li>First element<li>Second element<li>third<li style='background-color:blue'>Last");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.That(elements.Length, Is.EqualTo(4));

            var style1 = (elements[0] as IHtmlElement).GetStyle();
            Assert.That(style1.Length, Is.EqualTo(1));

            var test1 = style1[0];
            Assert.That(test1, Is.EqualTo("color"));
            Assert.That(style1.GetPropertyValue(test1), Is.EqualTo("rgba(255, 0, 0, 1)"));

            var style2 = (elements[1] as IHtmlElement).GetStyle();
            Assert.That(style2.Length, Is.EqualTo(1));

            var test2 = style2[0];
            Assert.That(test2, Is.EqualTo("color"));
            Assert.That(style2.GetPropertyValue(test2), Is.EqualTo("rgba(255, 0, 0, 1)"));

            var style3 = (elements[2] as IHtmlElement).GetStyle();
            Assert.That(style3.Length, Is.EqualTo(1));

            var test3 = style3[0];
            Assert.That(test3, Is.EqualTo("color"));
            Assert.That(style3.GetPropertyValue(test3), Is.EqualTo("rgba(255, 0, 0, 1)"));

            var style4 = (elements[3] as IHtmlElement).GetStyle();
            Assert.That(style4.Length, Is.EqualTo(2));

            var background = style4[0];
            Assert.That(background, Is.EqualTo("background-color"));
            Assert.That(style4.GetPropertyValue(background), Is.EqualTo("rgba(0, 0, 255, 1)"));

            var color = style4[1];
            Assert.That(color, Is.EqualTo("color"));
            Assert.That(style4.GetPropertyValue(color), Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void Background0ShouldSerializeCorrectly_Issue14()
        {
            var dom = ParseDocument(@"<html><body><div style=""background: 0;"">Test</div></body></html>");
            var div = dom.QuerySelector("div");
            var style = div.GetStyle();

            Assert.That(style.CssText, Is.EqualTo("background: left"));
        }

        [Test]
        public void RemovingPropertiesShouldNotYieldEmptyStyle_Issue14()
        {
            var dom = ParseDocument(@"<html><body><div style=""background: 0;"">Test</div></body></html>");
            var div = dom.QuerySelector("div");
            var style = div.GetStyle();

            style.RemoveProperty("background-position-x");
            style.RemoveProperty("background-position-y");

            Assert.That(style.CssText, Is.EqualTo("background-image: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial; background-color: initial"));
        }

        [Test]
        public void RecombinationWorksWithBorder_Issue16()
        {
            var expected = "<button style=\"pointer-events: auto; border: 1px solid rgba(0, 0, 0, 1)\"></button>";
            var document = ParseDocument("");
            var element = document.CreateElement("button");
            element.GetStyle().SetPointerEvents("auto");
            element.GetStyle().SetBorderWidth("1px");
            element.GetStyle().SetBorderStyle("solid");
            element.GetStyle().SetBorderColor("black");
            Assert.That(element.ToHtml(), Is.EqualTo(expected));
        }

        [Test]
        public void DefaultStyleSheetTest_Issue21()
        {
            var browsingContext = BrowsingContext.New(Configuration.Default.WithCss());
            var htmlParser = browsingContext.GetService<IHtmlParser>();
            var document = htmlParser.ParseDocument("<html><body><b>Hello, World!</b></body></html>");
            var boldStyle = document.Body.FirstElementChild.ComputeCurrentStyle();
            Assert.That(boldStyle.GetFontWeight(), Is.EqualTo("bolder"));
        }

        [Test]
        public void MediaRuleCssCausesException_Issue20()
        {
            var browsingContext = BrowsingContext.New(Configuration.Default.WithCss());
            var htmlParser = browsingContext.GetService<IHtmlParser>();
            var document = htmlParser.ParseDocument("<html><head><style>@media screen { }</style></head><body></body></html>");
            var style = document.Body.ComputeCurrentStyle();
            Assert.IsNotNull(style);
        }

        [Test]
        public void MediaRuleIsCalculatedIfScreenIsOkay()
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice
                {
                    ViewPortWidth = 1000,
                });
            var browsingContext = BrowsingContext.New(config);
            var htmlParser = browsingContext.GetService<IHtmlParser>();
            var document = htmlParser.ParseDocument("<html><head><style>body { color: red } @media only screen and (min-width: 600px) { body { color: green } }</style></head><body></body></html>");
            var style = document.Body.ComputeCurrentStyle();
            Assert.That(style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void MediaRuleIsNotCalculatedIfScreenIsNotWideEnough()
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice
                {
                    ViewPortWidth = 599,
                });
            var browsingContext = BrowsingContext.New(config);
            var htmlParser = browsingContext.GetService<IHtmlParser>();
            var document = htmlParser.ParseDocument("<html><head><style>body { color: red } @media only screen and (min-width: 600px) { body { color: green } }</style></head><body></body></html>");
            var style = document.Body.ComputeCurrentStyle();
            Assert.That(style.GetColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
        }
    }
}
