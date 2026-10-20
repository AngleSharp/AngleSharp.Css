#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// `word-break` and `overflow-wrap` (with its legacy alias `word-wrap`) are inherited
    /// properties (CSS Text 3), but a child reports them as empty. Found in a downstream renderer
    /// (AngleSharp.Renderer), where `word-break: break-all` on a paragraph does not reach the text
    /// inside its inline elements.
    /// </summary>
    [TestFixture]
    public class LineBreakingInheritanceTests
    {
        [TestCase("word-break", "break-all")]
        [TestCase("overflow-wrap", "break-word")]
        [TestCase("word-wrap", "break-word")]
        public void LineBreakingPropertiesAreInherited(string property, string value)
        {
            var document = ParseDocument($"<p style=\"{property}: {value}\"><span id=target>x</span></p>");

            Assert.AreEqual(value, document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue(property));
        }
    }
}
