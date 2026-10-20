#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The default (UA) stylesheet is the CSS 2.2 sample stylesheet, which predates HTML5: it has
    /// no rules for the sectioning and grouping elements, the elements that are never rendered,
    /// or form-control text handling that the HTML Standard's rendering section
    /// (https://html.spec.whatwg.org/multipage/rendering.html) requires. A downstream renderer
    /// (AngleSharp.Renderer) that takes every style from the computed style therefore lays out a
    /// `&lt;section&gt;` or `&lt;footer&gt;` as `inline` (the initial value of `display`) and
    /// would render the text of a `&lt;script&gt;` in the body, unless it guesses from tag names.
    /// </summary>
    [TestFixture]
    public class DefaultStyleSheetHtmlElementsTests
    {
        private static string ComputedValue(string html, string property)
        {
            var document = ParseDocument(html);
            return document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue(property);
        }

        [TestCase("article")]
        [TestCase("aside")]
        [TestCase("details")]
        [TestCase("figcaption")]
        [TestCase("figure")]
        [TestCase("footer")]
        [TestCase("header")]
        [TestCase("hgroup")]
        [TestCase("main")]
        [TestCase("nav")]
        [TestCase("search")]
        [TestCase("section")]
        [TestCase("legend")]
        public void Html5BlockElementsAreDisplayBlock(string tagName)
        {
            Assert.AreEqual("block", ComputedValue($"<body><{tagName} id=target>x</{tagName}></body>", "display"));
        }

        [TestCase("<body><script id=target>var x = 1;</script></body>")]
        [TestCase("<body><style id=target>p { color: red }</style></body>")]
        [TestCase("<body><template id=target><p>x</p></template></body>")]
        [TestCase("<body><div id=target hidden>x</div></body>")]
        [TestCase("<body><input id=target type=hidden value=x></body>")]
        public void NonRenderedElementsAreDisplayNone(string html)
        {
            Assert.AreEqual("none", ComputedValue(html, "display"));
        }

        [Test]
        public void TextareaPreservesWhiteSpace()
        {
            Assert.AreEqual("pre-wrap", ComputedValue("<body><textarea id=target>a   b</textarea></body>", "white-space"));
        }
    }
}
