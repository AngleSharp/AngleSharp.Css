namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
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
            Assert.AreEqual(1, doc.StyleSheets.Length);
            var css = doc.StyleSheets[0] as CssStyleSheet;
            Assert.AreEqual(1, css.Rules.Length);
            var style = css.Rules[0] as CssStyleRule;
            Assert.AreEqual("body", style.SelectorText);
            Assert.AreEqual(1, style.Style.Length);
            var decl = style.Style;
            Assert.IsInstanceOf<CssStyleDeclaration>(decl);
            var rule = decl.GetProperty("background-color");
            Assert.IsTrue(rule.IsImportant);
            Assert.AreEqual("background-color", rule.Name);
            Assert.AreEqual(rule.Name, decl[0]);
            Assert.AreEqual("rgba(0, 128, 0, 1)", rule.Value);
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
            Assert.AreEqual("initial", div.GetStyle()["background-color"]);
            Assert.AreEqual("background-color: initial", div.GetStyle().CssText);
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
            var config = Configuration.Default.With(requester).WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true).WithCss();
            var document = await BrowsingContext.New(config).OpenAsync("http://localhost/index.html");
            var link = document.QuerySelector<IHtmlLinkElement>("link");
            var style = document.QuerySelector<IHtmlStyleElement>("style");

            await Task.Delay(100);

            Assert.IsNotNull(link);
            Assert.IsNotNull(style);

            var origin = link.Sheet as ICssStyleSheet;
            Assert.IsNotNull(origin);
            Assert.AreEqual("http://localhost/origin.css", origin.Href);
            Assert.AreEqual(1, origin.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, origin.Rules[0].Type);

            var linked1 = (origin.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked1);
            Assert.AreEqual("http://localhost/linked1.css", linked1.Href);
            Assert.AreEqual(0, linked1.Rules.Length);

            var styleSheet = style.Sheet as ICssStyleSheet;
            Assert.IsNotNull(styleSheet);
            Assert.AreEqual(null, styleSheet.Href);
            Assert.AreEqual(1, styleSheet.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, styleSheet.Rules[0].Type);

            var linked2 = (styleSheet.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked2);
            Assert.AreEqual("http://localhost/linked2.css", linked2.Href);
            Assert.AreEqual(2, linked2.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, linked2.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Import, linked2.Rules[1].Type);

            var linked3 = (linked2.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked3);
            Assert.AreEqual("http://localhost/linked3.css", linked3.Href);
            Assert.AreEqual(0, linked3.Rules.Length);

            var linked4 = (linked2.Rules[1] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked4);
            Assert.AreEqual("http://localhost/linked4.css", linked4.Href);
            Assert.AreEqual(0, linked4.Rules.Length);
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
            var config = Configuration.Default.With(requester).WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true).WithCss();
            var document = await BrowsingContext.New(config).OpenAsync("http://localhost/index.html");
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            await Task.Delay(100);

            Assert.IsNotNull(link);

            var origin = link.Sheet as ICssStyleSheet;
            Assert.IsNotNull(origin);
            Assert.AreEqual("http://localhost/origin.css", origin.Href);
            Assert.AreEqual(1, origin.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, origin.Rules[0].Type);

            var linked = (origin.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked);
            Assert.AreEqual("http://localhost/linked.css", linked.Href);
            Assert.AreEqual(1, linked.Rules.Length);

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
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as IElement;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as IElement;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as IElement;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as IElement;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as IElement;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var styleAttribute = dochtml0body1table0tbody0tr0.Attributes[0];
            Assert.AreEqual("style", styleAttribute.Name);
            Assert.AreEqual("display: none;", styleAttribute.Value);

            var style = ((IHtmlElement)dochtml0body1table0tbody0tr0).GetStyle();
            Assert.AreEqual("none", style.GetDisplay());
        }

        [Test]
        public void TableWithTableRowThatHasStyleAndChanged()
        {
            var doc = ParseDocument(@"<table><tr style=""display: none;"">");

            var html = doc.ChildNodes[0] as IElement;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(0, html.Attributes.Length);
            Assert.AreEqual("html", html.GetTagName());
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var body = html.ChildNodes[1] as IElement;
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(0, body.Attributes.Length);
            Assert.AreEqual("body", body.GetTagName());
            Assert.AreEqual(NodeType.Element, body.NodeType);

            var table = body.ChildNodes[0] as IElement;
            Assert.AreEqual(1, table.ChildNodes.Length);
            Assert.AreEqual(0, table.Attributes.Length);
            Assert.AreEqual("table", table.GetTagName());
            Assert.AreEqual(NodeType.Element, table.NodeType);

            var tableBody = table.ChildNodes[0] as IElement;
            Assert.AreEqual(1, tableBody.ChildNodes.Length);
            Assert.AreEqual(0, tableBody.Attributes.Length);
            Assert.AreEqual("tbody", tableBody.GetTagName());
            Assert.AreEqual(NodeType.Element, tableBody.NodeType);

            var tableRow = tableBody.ChildNodes[0] as IElement;
            Assert.AreEqual(0, tableRow.ChildNodes.Length);
            Assert.AreEqual(1, tableRow.Attributes.Length);
            Assert.AreEqual("tr", tableRow.GetTagName());
            Assert.AreEqual(NodeType.Element, tableRow.NodeType);

            var tr = (IHtmlElement)tableRow;
            var style = tr.GetStyle();
            Assert.AreEqual("none", style.GetDisplay());

            style.SetDisplay("block");
            Assert.AreEqual("block", style.GetDisplay());
        }

        [Test]
        public void TableWithTableRowThatHasNoStyleAndChanged()
        {
            var doc = ParseDocument(@"<table><tr>");

            var html = doc.ChildNodes[0] as IElement;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(0, html.Attributes.Length);
            Assert.AreEqual("html", html.GetTagName());
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var body = html.ChildNodes[1] as IElement;
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(0, body.Attributes.Length);
            Assert.AreEqual("body", body.GetTagName());
            Assert.AreEqual(NodeType.Element, body.NodeType);

            var table = body.ChildNodes[0] as IElement;
            Assert.AreEqual(1, table.ChildNodes.Length);
            Assert.AreEqual(0, table.Attributes.Length);
            Assert.AreEqual("table", table.GetTagName());
            Assert.AreEqual(NodeType.Element, table.NodeType);

            var tableBody = table.ChildNodes[0] as IElement;
            Assert.AreEqual(1, tableBody.ChildNodes.Length);
            Assert.AreEqual(0, tableBody.Attributes.Length);
            Assert.AreEqual("tbody", tableBody.GetTagName());
            Assert.AreEqual(NodeType.Element, tableBody.NodeType);

            var tableRow = tableBody.ChildNodes[0] as IElement;
            Assert.AreEqual(0, tableRow.ChildNodes.Length);
            Assert.AreEqual(0, tableRow.Attributes.Length);
            Assert.AreEqual("tr", tableRow.GetTagName());
            Assert.AreEqual(NodeType.Element, tableRow.NodeType);

            var tr = (IHtmlElement)tableRow;
            var style = tr.GetStyle();

            style.SetDisplay("none");
            Assert.AreEqual("none", style.GetDisplay());
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
            Assert.AreEqual("initial", div.GetStyle().GetBackgroundColor());
        }

        [Test]
        public void ExtensionCssWithOneElement()
        {
            var document = ParseDocument("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.AreEqual(1, elements.Length);

            var style = (elements[0] as IHtmlElement).GetStyle();
            Assert.AreEqual(1, style.Length);

            Assert.AreEqual("color", style[0]);
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetColor());
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
            Assert.AreEqual(1, elements.Length);

            var style = (elements[0] as IHtmlElement).GetStyle();

            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetColor());
            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetBackgroundColor());
            Assert.AreEqual("\"Tahoma\"", style.GetFontFamily());
            Assert.AreEqual("10px", style.GetFontSize());
            Assert.AreEqual("0.5", style.GetOpacity());
        }

        [Test]
        public void ExtensionCssWithMultipleElements()
        {
            var document = ParseDocument("<ul><li>First element<li>Second element<li>third<li style='background-color:blue'>Last");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.AreEqual(4, elements.Length);

            var style1 = (elements[0] as IHtmlElement).GetStyle();
            Assert.AreEqual(1, style1.Length);

            var test1 = style1[0];
            Assert.AreEqual("color", test1);
            Assert.AreEqual("rgba(255, 0, 0, 1)", style1.GetPropertyValue(test1));

            var style2 = (elements[1] as IHtmlElement).GetStyle();
            Assert.AreEqual(1, style2.Length);

            var test2 = style2[0];
            Assert.AreEqual("color", test2);
            Assert.AreEqual("rgba(255, 0, 0, 1)", style2.GetPropertyValue(test2));

            var style3 = (elements[2] as IHtmlElement).GetStyle();
            Assert.AreEqual(1, style3.Length);

            var test3 = style3[0];
            Assert.AreEqual("color", test3);
            Assert.AreEqual("rgba(255, 0, 0, 1)", style3.GetPropertyValue(test3));

            var style4 = (elements[3] as IHtmlElement).GetStyle();
            Assert.AreEqual(2, style4.Length);

            var background = style4[0];
            Assert.AreEqual("background-color", background);
            Assert.AreEqual("rgba(0, 0, 255, 1)", style4.GetPropertyValue(background));

            var color = style4[1];
            Assert.AreEqual("color", color);
            Assert.AreEqual("rgba(255, 0, 0, 1)", style4.GetPropertyValue(color));
        }
    }
}
