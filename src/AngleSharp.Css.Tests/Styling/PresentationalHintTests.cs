#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The attributes of HTML elements mapping to CSS (HTML 15.3), and the quirks mode sheet.
    /// </summary>
    [TestFixture]
    public class PresentationalHintTests
    {
        private static ICssStyleDeclaration Style(string html, string doctype = "<!doctype html>") =>
            ParseDocument($"{doctype}<html><body>{html}</body></html>").GetElementById("t").ComputeCurrentStyle();

        [TestCase("<table id=t cellspacing=5></table>", "border-spacing", "5px")]
        [TestCase("<table cellpadding=7><tr><td id=t></td></tr></table>", "padding-left", "7px")]
        [TestCase("<table cellpadding=10%><tr><td id=t></td></tr></table>", "padding-left", "10px")]
        [TestCase("<table border=3 id=t></table>", "border-top-width", "3px")]
        [TestCase("<table border=3 id=t></table>", "border-top-style", "outset")]
        [TestCase("<table border=3><tr><td id=t></td></tr></table>", "border-top-width", "1px")]
        [TestCase("<table border=3><tr><td id=t></td></tr></table>", "border-top-style", "inset")]
        [TestCase("<table align=right id=t></table>", "float", "right")]
        [TestCase("<table align=center id=t></table>", "margin-left", "auto")]
        [TestCase("<table><tr><td id=t align=center></td></tr></table>", "text-align", "-webkit-center")]
        [TestCase("<table><tr><td id=t valign=top></td></tr></table>", "vertical-align", "top")]
        [TestCase("<table><tr valign=bottom><td id=t></td></tr></table>", "vertical-align", "bottom")]
        [TestCase("<table><tr><td id=t nowrap></td></tr></table>", "white-space", "nowrap")]
        [TestCase("<div id=t align=right></div>", "text-align", "-webkit-right")]
        [TestCase("<p id=t align=center></p>", "text-align", "center")]
        [TestCase("<center id=t></center>", "text-align", "-webkit-center")]
        [TestCase("<img id=t align=left>", "float", "left")]
        [TestCase("<img id=t align=top>", "vertical-align", "top")]
        [TestCase("<img id=t align=middle>", "vertical-align", "-webkit-baseline-middle")]
        [TestCase("<img id=t align=absmiddle>", "vertical-align", "middle")]
        [TestCase("<img id=t hspace=5>", "margin-right", "5px")]
        [TestCase("<img id=t vspace=7>", "margin-top", "7px")]
        [TestCase("<img id=t border=2>", "border-left-width", "2px")]
        [TestCase("<font id=t size=1></font>", "font-size", "10px")]
        [TestCase("<font id=t size=5></font>", "font-size", "24px")]
        [TestCase("<font id=t size=+1></font>", "font-size", "18px")]
        [TestCase("<font id=t face=monospace></font>", "font-family", "monospace")]
        [TestCase("<hr id=t align=right>", "margin-left", "auto")]
        [TestCase("<hr id=t size=5>", "height", "3px")]
        [TestCase("<ul id=t type=square></ul>", "list-style-type", "square")]
        [TestCase("<ol id=t type=a></ol>", "list-style-type", "lower-alpha")]
        public void AttributeMapsToProperty(string html, string property, string expected)
        {
            Assert.AreEqual(expected, Style(html).GetPropertyValue(property));
        }

        [TestCase("<iframe id=t></iframe>", "border-top-width", "2px")]
        [TestCase("<iframe id=t></iframe>", "border-top-style", "inset")]
        [TestCase("<iframe id=t frameborder=0></iframe>", "border-top-style", "none")]
        [TestCase("<iframe id=t width=50 height=20></iframe>", "width", "50px")]
        public void IframeHasItsDefaultBorderAndHints(string html, string property, string expected)
        {
            Assert.AreEqual(expected, Style(html).GetPropertyValue(property));
        }

        [TestCase("<div id=t style=\"box-shadow: 1px 2px\"></div>", "box-shadow")]
        [TestCase("<div id=t style=\"text-shadow: 1px 2px\"></div>", "text-shadow")]
        public void ShadowWithoutAColorCanBeComputed(string html, string property)
        {
            StringAssert.Contains("1px 2px", Style(html).GetPropertyValue(property));
        }

        [Test]
        public void AuthorRulesWinOverHints()
        {
            Assert.AreEqual("1px", Style("<style>td { padding: 1px }</style><table cellpadding=7><tr><td id=t></td></tr></table>").GetPropertyValue("padding-left"));
        }

        [TestCase("<div style=\"font-size: 30px\"><table id=t></table></div>", "font-size", "16px")]
        [TestCase("<div style=\"text-align: right\"><table id=t></table></div>", "text-align", "start")]
        [TestCase("<div style=\"white-space: pre\"><table id=t></table></div>", "white-space", "normal")]
        [TestCase("<form id=t></form>", "margin-bottom", "16px")]
        [TestCase("<input id=t>", "box-sizing", "border-box")]
        [TestCase("<img id=t align=left>", "margin-right", "3px")]
        public void QuirksModeHasItsOwnRules(string html, string property, string expected)
        {
            Assert.AreEqual(expected, Style(html, doctype: string.Empty).GetPropertyValue(property));
        }

        [Test]
        public void StandardsModeHasNoneOfThem()
        {
            Assert.AreEqual("right", Style("<div style=\"text-align: right\"><table id=t></table></div>").GetPropertyValue("text-align"));
            Assert.AreNotEqual("16px", Style("<form id=t></form>").GetPropertyValue("margin-bottom"));
        }

        // a limited-quirks document (compatMode "CSS1Compat") has no quirks
        [TestCase("<div style=\"text-align: right\"><table id=t></table></div>", "text-align", "right")]
        [TestCase("<div id=t style=\"width: 100\"></div>", "width", "")]
        public void LimitedQuirksModeHasNoneOfThem(string html, string property, string expected)
        {
            const string doctype = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = context.OpenAsync(request => request.Content($"{doctype}<html><body>{html}</body></html>")).Result;

            Assert.AreEqual("CSS1Compat", document.CompatMode);
            Assert.AreEqual(expected, document.GetElementById("t").ComputeCurrentStyle().GetPropertyValue(property));
        }

        [TestCase("<div id=t style=\"width: 100\"></div>", "width", "100px")]
        [TestCase("<div id=t style=\"margin-left: 20\"></div>", "margin-left", "20px")]
        [TestCase("<div id=t style=\"color: 0f0\"></div>", "color", "rgba(0, 255, 0, 1)")]
        public void QuirksModeAllowsLengthsWithoutUnitsAndColorsWithoutHash(string html, string property, string expected)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = context.OpenAsync(request => request.Content($"<html><body>{html}</body></html>")).Result;

            Assert.AreEqual(expected, document.GetElementById("t").ComputeCurrentStyle().GetPropertyValue(property));
        }

        [TestCase("<div id=t style=\"grid-template-columns: min(50%, 300px) 1fr\"></div>", "grid-template-columns", "min(50%, 300px) 1fr")]
        [TestCase("<div id=t style=\"grid-template-columns: minmax(max(1px, 2%), 1fr)\"></div>", "grid-template-columns", "minmax(max(1px, 2%), 1fr)")]
        [TestCase("<div id=t style=\"grid-template-columns: clamp(10px, 1px + 5%, 100px) 1fr\"></div>", "grid-template-columns", "clamp(10px, 1px + 5%, 100px) 1fr")]
        [TestCase("<div id=t style=\"background-size: max(18px, 10px)\"></div>", "background-size", "18px")]
        [TestCase("<div id=t style=\"mask-size: min(18px, 10px)\"></div>", "mask-size", "10px")]
        public void MathFunctionsAreSizes(string html, string property, string expected)
        {
            Assert.AreEqual(expected, Style(html).GetPropertyValue(property));
        }

        [Test]
        public void StandardsModeRejectsThem()
        {
            Assert.AreNotEqual("100px", Style("<div id=t style=\"width: 100\"></div>").GetPropertyValue("width"));
        }
    }
}
