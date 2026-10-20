#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    /// <summary>
    /// Computed values that are parsed correctly but come back wrong when serialized. Found in a
    /// downstream renderer (AngleSharp.Renderer):
    /// - the `border-radius` shorthand computes as empty (even for a single value), although the
    ///   per-corner longhands are right;
    /// - `grid: auto-flow / 1fr 1fr` loses `auto-flow`;
    /// - `counters(item, ".")` loses the comma and the quotes of its separator string.
    /// </summary>
    [TestFixture]
    public class ComputedSerializationTests
    {
        private static string Computed(string declarations, string property)
        {
            var config = Configuration.Default
                .WithCss()
                .WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 400, ViewPortHeight = 300, FontSize = 16 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument($"<div id=target style='{declarations}'>x</div>");
            return document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue(property);
        }

        [TestCase("10px", "10px")]
        [TestCase("10px 20px", "10px 20px")]
        [TestCase("10px / 20px", "10px / 20px")]
        [TestCase("10px 20px / 5px", "10px 20px / 5px")]
        public void BorderRadiusShorthandIsComputed(string value, string expected)
        {
            Assert.AreEqual(expected, Computed($"border-radius: {value}", "border-radius"));
        }

        [Test]
        public void GridShorthandKeepsAutoFlow()
        {
            Assert.AreEqual("auto-flow / 1fr 1fr", Computed("grid: auto-flow / 1fr 1fr", "grid"));
        }

        [Test]
        public void CountersKeepsItsSeparatorString()
        {
            Assert.AreEqual("counters(item, \".\")", Computed("content: counters(item, \".\")", "content"));
        }
    }
}
