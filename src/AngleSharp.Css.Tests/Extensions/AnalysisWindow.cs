namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Text;
    using System.Threading.Tasks;
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

        [Test]
        public void GetCascadedValueOfTextTransformFromGlobalStyle()
        {
            var sourceCode = "<!doctype html><style>body { text-transform: uppercase }</style><div><p><span>Bold text";

            var document = ParseDocument(sourceCode);
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            var element = document.QuerySelector("span");
            Assert.IsNotNull(element);

            var styleNormal = window.GetComputedStyle(element);
            Assert.IsNotNull(styleNormal);
            Assert.AreEqual("uppercase", styleNormal.GetTextTransform());
        }

        [Test]
        public void GetCascadedValueOfTextTransformFromElementStyle()
        {
            var sourceCode = "<!doctype html><div style=\"text-transform: uppercase\"><p><span>Bold text";

            var document = ParseDocument(sourceCode);
            Assert.IsNotNull(document);
            var window = document.DefaultView;

            var element = document.QuerySelector("span");
            Assert.IsNotNull(element);

            var styleNormal = window.GetComputedStyle(element);
            Assert.IsNotNull(styleNormal);
            Assert.AreEqual("uppercase", styleNormal.GetTextTransform());
        }

        [Test]
        public void GetCascadedValueOfTextTransformFromElementStyleWithElementApi()
        {
            var sourceCode = "<!doctype html><div style=\"text-transform: uppercase\"><p><span>Bold text";

            var document = ParseDocument(sourceCode);
            var element = document.QuerySelector("span");
            var styleNormal = element.ComputeStyle();
            Assert.IsNotNull(styleNormal);
            Assert.AreEqual("uppercase", styleNormal.GetTextTransform());
        }

        [Test]
        public async Task NullSelectorStillWorks_Issue52()
        {
            var sheet = ParseStyleSheet("a {}");
            var document = await sheet.Context.OpenAsync(res => res.Content("<body></body>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var decl = sc.ComputeCascadedStyle(document.Body);
            Assert.IsNotNull(decl);
        }

        [Test]
        public async Task PriorityInMultiSelectorIsEvaluatedPerMatch()
        {
            var sheet = ParseStyleSheet(@"#target {color: blue} h3, #nottarget { color: purple; } ");
            var document = await sheet.Context.OpenAsync(res => res.Content(@"<h3 id='target'>Test</h3>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var style = sc.ComputeCascadedStyle(document.QuerySelector("h3"));
            Assert.AreEqual("rgba(0, 0, 255, 1)", style.GetColor());
        }

        [Test]
        public async Task ComputesAbsoluteValuesFromRelative_Issue136()
        {
            var sheet = ParseStyleSheet(@"p { font-size: 1.5em }");
            var document = await sheet.Context.OpenAsync(res => res.Content(@"<p>This is <span>only</span> a test.</p>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var style = sc.ComputeDeclarations(document.QuerySelector("span"));
            Assert.AreEqual("24px", style.GetFontSize());
        }

        [Test]
        public async Task ResolvesCssVariables_Issue62()
        {
            var sheet = ParseStyleSheet(@"
            :root {
                --color: #FFFFFF;
            }

            p {
                color: var(--color);
            }");
            var document = await sheet.Context.OpenAsync(res => res.Content(@"<p>This is a test</p>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var style = sc.ComputeDeclarations(document.QuerySelector("p"));
            Assert.AreEqual("rgba(255, 255, 255, 1)", style.GetColor());
        }

        [Test]
        public async Task ResolvesCssVariablesWithUnusedFallback_Issue62()
        {
            var sheet = ParseStyleSheet(@"
            :root {
                --color: #FFFFFF;
            }

            p {
                color: var(--color, green);
            }");
            var document = await sheet.Context.OpenAsync(res => res.Content(@"<p>This is a test</p>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var style = sc.ComputeDeclarations(document.QuerySelector("p"));
            Assert.AreEqual("rgba(255, 255, 255, 1)", style.GetColor());
        }

        [Test]
        public async Task ResolvesCssVariablesWithUsedFallback_Issue62()
        {
            var sheet = ParseStyleSheet(@"
            :root {}

            p {
                color: var(--color, green);
            }");
            var document = await sheet.Context.OpenAsync(res => res.Content(@"<p>This is a test</p>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var style = sc.ComputeDeclarations(document.QuerySelector("p"));
            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetColor());
        }

        [Test]
        public async Task ResolvesCssVariablesWithUsedFallbackVarReference_Issue62()
        {
            var sheet = ParseStyleSheet(@"
            :root {
                --defaultColor: green;
            }

            p {
                color: var(--color, var(--defaultColor));
            }");
            var document = await sheet.Context.OpenAsync(res => res.Content(@"<p>This is a test</p>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var style = sc.ComputeDeclarations(document.QuerySelector("p"));
            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetColor());
        }

        [Test]
        public async Task ResolvesCssVariablesWithCascade_Issue62()
        {
            var sheet = ParseStyleSheet(@"
            :root {
                --color: blue;
                --defaultColor: red;
            }

            body {
                --color: green;
            }

            p {
                color: var(--color, var(--defaultColor));
            }");
            var document = await sheet.Context.OpenAsync(res => res.Content(@"<p>This is a test</p>"));
            var sc = new StyleCollection(new[] { sheet }, new DefaultRenderDevice());
            var style = sc.ComputeDeclarations(document.QuerySelector("p"));
            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetColor());
        }
    }
}
