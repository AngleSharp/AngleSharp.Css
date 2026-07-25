#nullable disable
namespace AngleSharp.Css.Tests.Library
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    [TestFixture]
    public class ComputedStyleTests
    {
        [Test]
        public async Task TransformEmToPx_Issue136()
        {
            // .With<IRenderDevice>()
            var config = Configuration.Default.WithCss();
            var context = BrowsingContext.New(config);
            var source = "<p>This is <span>only</span> a test.</p>";
            var cssSheet = "p { font-size: 1.5em }";
            var document = await context.OpenAsync(req => req.Content(source));
            var style = document.CreateElement<IHtmlStyleElement>();
            style.TextContent = cssSheet;
            document.Head.AppendChild(style);
            var span = document.QuerySelector("span");
            var fontSize = span.ComputeCurrentStyle().GetProperty("font-size");

            Assert.AreEqual("24px", fontSize.Value);
        }

        [Test]
        public void MarginInheritShouldResolveToParentValues()
        {
            var source = @"<!DOCTYPE html>
<html>
<head><style>
div.parent { margin: 10px 20px; }
div.child { margin: inherit; }
</style></head>
<body>
<div class='parent'><div class='child'>Content</div></div>
</body>
</html>";
            var document = ParseDocument(source);
            var child = document.QuerySelector("div.child");
            var style = child.ComputeCurrentStyle();
            Assert.AreEqual("10px", style.GetMarginTop());
            Assert.AreEqual("20px", style.GetMarginRight());
            Assert.AreEqual("10px", style.GetMarginBottom());
            Assert.AreEqual("20px", style.GetMarginLeft());
        }

        [Test]
        public void PaddingInheritShouldResolveToParentValues()
        {
            var source = @"<!DOCTYPE html>
<html>
<head><style>
div.parent { padding: 5px 15px 10px; }
div.child { padding: inherit; }
</style></head>
<body>
<div class='parent'><div class='child'>Content</div></div>
</body>
</html>";
            var document = ParseDocument(source);
            var child = document.QuerySelector("div.child");
            var style = child.ComputeCurrentStyle();
            Assert.AreEqual("5px", style.GetPaddingTop());
            Assert.AreEqual("15px", style.GetPaddingRight());
            Assert.AreEqual("10px", style.GetPaddingBottom());
            Assert.AreEqual("15px", style.GetPaddingLeft());
        }

        [Test]
        public void InheritOnNonInheritablePropertyShouldResolveFromParent()
        {
            var source = @"<!DOCTYPE html>
<html>
<head><style>
div.parent { border: 1px solid; padding: 8px; }
div.child { border: inherit; padding: inherit; }
</style></head>
<body>
<div class='parent'><div class='child'>Content</div></div>
</body>
</html>";
            var document = ParseDocument(source);
            var child = document.QuerySelector("div.child");
            var style = child.ComputeCurrentStyle();
            Assert.AreEqual("solid", style.GetBorderTopStyle());
            Assert.AreEqual("1px", style.GetBorderTopWidth());
            Assert.AreEqual("8px", style.GetPaddingTop());
        }

        [Test]
        public void BorderInheritShouldSerializeCorrectly()
        {
            var html = @"<style>div { border: inherit }</style>";
            var dom = ParseDocument(html);
            var styleSheet = dom.StyleSheets[0] as ICssStyleSheet;
            var rule = styleSheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("inherit", rule.Style.GetPropertyValue("border-top-style"));
            Assert.AreEqual("inherit", rule.Style.GetPropertyValue("border-top-width"));
            Assert.AreEqual("inherit", rule.Style.GetPropertyValue("border-top-color"));
        }
    }
}
