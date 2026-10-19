#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// A `::before`/`::after` pseudo-element only gets the declarations of rules whose selector
    /// targets that pseudo-element, plus whatever it inherits from its host. A rule that matches
    /// the host itself (`div#e { border: ... }`, or the default stylesheet's `input { ... }`) must
    /// not apply to the host's pseudo-elements. Found in a downstream renderer
    /// (AngleSharp.Renderer): a `::before` on an `&lt;input&gt;` got the input's own border,
    /// padding and background, so the control's look was painted twice.
    /// </summary>
    [TestFixture]
    public class PseudoElementComputedStyleTests
    {
        private static ICssStyleDeclaration PseudoStyle(string css, string pseudo)
        {
            var document = ParseDocument($"<head><style>{css}</style></head><body><div id=host>text</div></body>");
            var window = document.DefaultView;
            return window.GetComputedStyle(document.GetElementById("host"), pseudo);
        }

        [Test]
        public void HostRuleDoesNotApplyNonInheritedPropertiesToBefore()
        {
            var style = PseudoStyle("div#host { border: 2px solid red; padding: 5px; background-color: blue } #host::before { content: 'x' }", "::before");

            Assert.AreNotEqual("2px", style.GetPropertyValue("border-top-width"));
            Assert.AreNotEqual("5px", style.GetPropertyValue("padding-left"));
            Assert.AreNotEqual("rgba(0, 0, 255, 1)", style.GetPropertyValue("background-color"));
        }

        [Test]
        public void HostRuleDoesNotApplyNonInheritedPropertiesToAfter()
        {
            var style = PseudoStyle("div { margin-left: 30px } div::after { content: 'x' }", "::after");

            Assert.AreNotEqual("30px", style.GetPropertyValue("margin-left"));
        }

        [Test]
        public void InheritedPropertiesOfTheHostStillReachThePseudoElement()
        {
            var style = PseudoStyle("div#host { color: red } #host::before { content: 'x' }", "::before");

            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("color"));
        }

        [Test]
        public void RulesTargetingThePseudoElementStillApply()
        {
            var style = PseudoStyle("#host::before { content: 'x'; padding-left: 7px }", "::before");

            Assert.AreEqual("7px", style.GetPropertyValue("padding-left"));
        }
    }
}
