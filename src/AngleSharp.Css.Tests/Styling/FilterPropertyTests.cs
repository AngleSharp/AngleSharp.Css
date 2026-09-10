#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Verifies that filter function lists are parsed into structured values and preserved by
    /// computed style, while the original inline style remains available through the DOM.
    /// </summary>
    [TestFixture]
    public class FilterPropertyTests
    {
        [Test]
        public void FilterComputedValuePreservesAuthoredFunctions()
        {
            var document = ParseDocument("<div id=target style=\"filter: grayscale(0.9) blur(2px);\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("grayscale(0.9) blur(2px)", target.ComputeCurrentStyle().GetPropertyValue("filter"));
        }

        [Test]
        public void FilterFunctionsExposeNamesAndArguments()
        {
            var document = ParseDocument("<div id=target style=\"filter: grayscale(0.9) blur(2px);\"></div>");
            var target = document.GetElementById("target");
            var value = target.GetStyle().GetProperty("filter").RawValue as CssFilterValue;

            Assert.IsNotNull(value);
            Assert.AreEqual(2, value.Functions.Length);
            Assert.AreEqual("grayscale", value.Functions[0].Name);
            Assert.AreEqual("0.9", value.Functions[0].Arguments[0].CssText);
            Assert.AreEqual("blur", value.Functions[1].Name);
            Assert.AreEqual("2px", value.Functions[1].Arguments[0].CssText);
        }

        [Test]
        public void RawFilterTextIsStillReadableFromTheInlineStyleAttributeItself()
        {
            // Confirms the gap is specifically in AngleSharp.Css's own computed-style/cascade
            // pipeline, not in the HTML/attribute layer - the text is right there, just never
            // parsed into a structured value or even echoed back through computed style.
            var document = ParseDocument("<div id=target style=\"filter: grayscale(0.9) blur(2px);\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("filter: grayscale(0.9) blur(2px);", target.GetAttribute("style"));
        }
    }
}
