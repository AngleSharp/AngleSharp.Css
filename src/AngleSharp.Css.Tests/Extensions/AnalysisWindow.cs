﻿namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Text;
    using static CssConstructionFunctions;

    [TestFixture]
    public class AnalysisWindowTests
    {
        [Test]
        public void GetComputedStyleTrivialInitialScenario()
        {
            var source = "<!doctype html><head><style>p > span { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";
            var document = ParseDocument(source);
            var window = document.DefaultView;
            Assert.IsNotNull(document);

            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);

            Assert.AreEqual("span", element.LocalName);
            Assert.AreEqual("bold", element.ClassName);

            var style = window.GetComputedStyle(element);
            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.Length);
        }

        [Test]
        public void GetComputedStyleImportantHigherNoInheritance()
        {
            var source = new StringBuilder("<!doctype html> ");

            var styles = new StringBuilder("<head><style>");
            styles.Append("p {text-align: center;}");
            styles.Append("p > span { color: blue; }");
            styles.Append("p > span { color: red; }");
            styles.Append("span.bold { font-weight: bold !important; }");
            styles.Append("span.bold { font-weight: lighter; }");

            styles.Append("#prioOne { color: black; }");
            styles.Append("div {color: green; }");
            styles.Append("</style></head>");

            var body = new StringBuilder("<body>");
            body.Append("<div><p><span class='bold'>Bold text</span></p></div>");
            body.Append("<div id='prioOne'>prioOne</div>");
            body.Append("</body>");

            source.Append(styles);
            source.Append(body);

            var document = ParseDocument(source.ToString());
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            // checks for element with text bold text
            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);
            Assert.AreEqual("span", element.LocalName);
            Assert.AreEqual("bold", element.ClassName);

            var computedStyle = window.GetComputedStyle(element);
            Assert.AreEqual("rgba(255, 0, 0, 1)", computedStyle.GetColor());
            Assert.AreEqual("bold", computedStyle.GetFontWeight());
            Assert.AreEqual(3, computedStyle.Length);
        }

        [Test]
        public void GetComputedStyleHigherMatchingPrio()
        {
            var source = new StringBuilder("<!doctype html> ");

            var styles = new StringBuilder("<head><style>");
            styles.Append("p {text-align: center;}");
            styles.Append("p > span { color: blue; }");
            styles.Append("p > span { color: red; }");
            styles.Append("span.bold { font-weight: bold !important; }");
            styles.Append("span.bold { font-weight: lighter; }");

            styles.Append("#prioOne { color: black; }");
            styles.Append("div {color: green; }");
            styles.Append("</style></head>");

            var body = new StringBuilder("<body>");
            body.Append("<div><p><span class='bold'>Bold text</span></p></div>");
            body.Append("<div id='prioOne'>prioOne</div>");
            body.Append("</body>");

            source.Append(styles);
            source.Append(body);

            var document = ParseDocument(source.ToString());
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            // checks for element with text prioOne
            var prioOne = document.QuerySelector("#prioOne");
            Assert.IsNotNull(prioOne);
            Assert.AreEqual("div", prioOne.LocalName);
            Assert.AreEqual("prioOne", prioOne.Id);

            var computePrioOneStyle = window.GetComputedStyle(prioOne);
            Assert.AreEqual("rgba(0, 0, 0, 1)", computePrioOneStyle.GetColor());
        }

        [Test]
        public void GetComputedStyleUseAndPreferInlineStyle()
        {
            var source = new StringBuilder("<!doctype html> ");

            var styles = new StringBuilder("<head><style>");
            styles.Append("p > span { color: blue; }");
            styles.Append("</style></head>");

            var body = new StringBuilder("<body>");
            body.Append("<div><p><span style='color: red'>Bold text</span></p></div>");
            body.Append("</body>");

            source.Append(styles);
            source.Append(body);

            var document = ParseDocument(source.ToString());
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            // checks for element with text bold text
            var element = document.QuerySelector("p > span");
            Assert.IsNotNull(element);
            Assert.AreEqual("span", element.LocalName);

            var computedStyle = window.GetComputedStyle(element);
            Assert.AreEqual("rgba(255, 0, 0, 1)", computedStyle.GetColor());
            Assert.AreEqual(1, computedStyle.Length);
        }

        [Test]
        public void GetComputedStyleComplexScenario()
        {
            var sourceCode = @"<!doctype html>
<head>
<style>
p > span { color: blue; } 
span.bold { font-weight: bold; }
</style>
<style>
p { font-size: 20px; }
em { font-style: italic !important; }
.red { margin: 5px; }
</style>
<style>
#text { font-style: normal; margin: 0; }
</style>
</head>
<body>
<div><p><span class=bold>Bold <em style='color: red' class=red id=text>text</em>";

            var document = ParseDocument(sourceCode);
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            var element = document.QuerySelector("#text");
            Assert.IsNotNull(element);

            Assert.AreEqual("em", element.LocalName);
            Assert.AreEqual("red", element.ClassName);
            Assert.IsNotNull(element.GetAttribute("style"));
            Assert.AreEqual("text", element.TextContent);

            var style = window.GetComputedStyle(element);
            Assert.IsNotNull(style);
            Assert.AreEqual(8, style.Length);

            Assert.AreEqual("0", style.GetMargin());
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetColor());
            Assert.AreEqual("bold", style.GetFontWeight());
            Assert.AreEqual("italic", style.GetFontStyle());
            Assert.AreEqual("20px", style.GetFontSize());
        }

        [Test]
        public void GetComputedStylePseudoInitialScenarioSingleColon()
        {
            var sourceCode = "<!doctype html><head><style>p > span::after { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";

            var document = ParseDocument(sourceCode);
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);

            Assert.AreEqual("span", element.LocalName);
            Assert.AreEqual("bold", element.ClassName);

            var style = window.GetComputedStyle(element, ":after");
            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.Length);
        }

        [Test]
        public void GetComputedStylePseudoInitialScenarioDoubleColon()
        {
            var sourceCode = "<!doctype html><head><style>p > span::after { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";

            var document = ParseDocument(sourceCode);
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);

            Assert.AreEqual("span", element.LocalName);
            Assert.AreEqual("bold", element.ClassName);

            var style = window.GetComputedStyle(element, "::after");
            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.Length);
        }

        [Test]
        public void GetComputedStyleMixedTrivialAndPseudoScenario()
        {
            var sourceCode = "<!doctype html><head><style>p > span { color: blue; } span.bold { font-weight: bold; } span.bold::before { color: red; content: 'Important!'; }</style></head><body><div><p><span class='bold'>Bold text";

            var document = ParseDocument(sourceCode);
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);

            Assert.AreEqual("span", element.LocalName);
            Assert.AreEqual("bold", element.ClassName);

            var styleNormal = window.GetComputedStyle(element);
            Assert.IsNotNull(styleNormal);
            Assert.AreEqual(2, styleNormal.Length);

            var stylePseudo = window.GetComputedStyle(element, ":before");
            Assert.IsNotNull(stylePseudo);
            Assert.AreEqual(3, stylePseudo.Length);
        }
    }
}
