#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Rules of the HTML Standard's rendering section
    /// (https://html.spec.whatwg.org/multipage/rendering.html) that the default stylesheet still
    /// lacks or gets differently. Found in a downstream renderer (AngleSharp.Renderer), which
    /// takes every style from the computed style: links render black, `mark` has no highlight,
    /// list backgrounds stop short of the markers, and `bdi`/`[dir]` do not isolate their text
    /// for the bidi algorithm.
    /// </summary>
    [TestFixture]
    public class DefaultStyleSheetRenderingSectionTests
    {
        private static string Computed(string html, string property)
        {
            var document = ParseDocument(html);
            return document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue(property);
        }

        [Test]
        public void LinksAreBlue()
        {
            Assert.AreEqual("rgba(0, 0, 238, 1)", Computed("<p><a id=target href=#>x</a></p>", "color"));
        }

        [TestCase("background-color", "rgba(255, 255, 0, 1)")]
        [TestCase("color", "rgba(0, 0, 0, 1)")]
        public void MarkIsHighlighted(string property, string expected)
        {
            Assert.AreEqual(expected, Computed("<p><mark id=target>x</mark></p>", property));
        }

        [TestCase("margin-top")]
        [TestCase("margin-bottom")]
        public void FormHasNoMargins(string property)
        {
            // only a quirks mode form has a bottom margin; forms in grids (e.g. dev.to's sign-in
            // buttons) were spaced apart
            Assert.AreEqual(Computed("<!doctype html><div id=target>x</div>", property), Computed("<!doctype html><form id=target>x</form>", property));
        }

        [TestCase("margin-top")]
        [TestCase("margin-bottom")]
        public void PreHasBlockMargins(string property)
        {
            Assert.AreEqual("16px", Computed("<pre id=target>x</pre>", property));
        }

        [TestCase("margin-top", "8px")]
        [TestCase("margin-bottom", "8px")]
        [TestCase("margin-left", "auto")]
        [TestCase("margin-right", "auto")]
        public void HrHasMargins(string property, string expected)
        {
            Assert.AreEqual(expected, Computed("<hr id=target>", property));
        }

        [TestCase("::before", "open-quote")]
        [TestCase("::after", "close-quote")]
        public void QuotesAreGenerated(string pseudo, string expected)
        {
            var document = ParseDocument("<p><q id=target>x</q></p>");
            var element = document.GetElementById("target");
            Assert.AreEqual(expected, document.DefaultView.GetComputedStyle(element, pseudo).GetPropertyValue("content"));
        }

        [Test]
        public void TablesAreBorderBoxSized()
        {
            Assert.AreEqual("border-box", Computed("<table id=target><tr><td>x</td></tr></table>", "box-sizing"));
        }

        [Test]
        public void TablesDoNotInheritTextIndent()
        {
            Assert.AreEqual("0px", Computed("<div style=\"text-indent: 40px\"><table id=target><tr><td>x</td></tr></table></div>", "text-indent"));
        }

        [TestCase("td")]
        [TestCase("th")]
        public void TableCellsHavePadding(string tagName)
        {
            Assert.AreEqual("1px", Computed($"<table><tr><{tagName} id=target>x</{tagName}></tr></table>", "padding-top"));
        }

        [TestCase("border-top-style", "groove")]
        [TestCase("border-top-width", "2px")]
        [TestCase("padding-top", "5.6px")]
        [TestCase("padding-left", "12px")]
        [TestCase("margin-left", "2px")]
        public void FieldsetHasBorderAndPadding(string property, string expected)
        {
            Assert.AreEqual(expected, Computed("<fieldset id=target><legend>l</legend>x</fieldset>", property));
        }

        [TestCase("text-decoration-line", "underline")]
        [TestCase("text-decoration-style", "dotted")]
        public void AbbreviationsWithTitleAreUnderlinedDotted(string property, string expected)
        {
            Assert.AreEqual(expected, Computed("<p><abbr id=target title=t>x</abbr></p>", property));
        }

        [TestCase("<p><bdi id=target>x</bdi></p>")]
        [TestCase("<p dir=rtl id=target>x</p>")]
        [TestCase("<p><span dir=ltr id=target>x</span></p>")]
        public void BdiAndDirAttributeIsolate(string html)
        {
            Assert.AreEqual("isolate", Computed(html, "unicode-bidi"));
        }

        [Test]
        public void SummaryIsAListItemWithADisclosureMarker()
        {
            Assert.AreEqual("list-item", Computed("<details><summary id=target>s</summary>x</details>", "display"));
            Assert.AreEqual("disclosure-closed", Computed("<details><summary id=target>s</summary>x</details>", "list-style-type"));
            Assert.AreEqual("disclosure-open", Computed("<details open><summary id=target>s</summary>x</details>", "list-style-type"));
        }

        [TestCase("ul")]
        [TestCase("ol")]
        public void ListsIndentWithPadding(string tagName)
        {
            var html = $"<{tagName} id=target><li>x</li></{tagName}>";

            Assert.AreEqual("40px", Computed(html, "padding-left"));
            Assert.AreEqual("0px", Computed(html, "margin-left"));
        }
    }
}
