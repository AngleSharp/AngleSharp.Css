namespace AngleSharp.Css.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class ComputedStyleTests
    {
        [Test]
        [Ignore("Not implemented yet")]
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
    }
}
