#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// AngleSharp has no notion of pointer/interaction state, so `:hover` is unconditionally
    /// non-matching unless explicitly forced via <see cref="ElementExtensions.SetPseudoClass"/>
    /// (see PseudoClassForcing.cs for the general mechanism this relies on). These tests pin down
    /// the specific `:hover` case that first surfaced the gap while building CSS `transition`
    /// support in a downstream renderer (AngleSharp.Renderer).
    /// </summary>
    [TestFixture]
    public class HoverPseudoClassTests
    {
        [Test]
        public void HoverDoesNotMatchByDefault()
        {
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            Assert.IsFalse(target.Matches(":hover"));
            Assert.IsTrue(target.Matches(":not(:hover)"));
        }

        [Test]
        public void ForcingHoverMakesItMatchInTheSelectorEngine()
        {
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            target.SetPseudoClass("hover");

            Assert.IsTrue(target.Matches(":hover"));
            Assert.IsFalse(target.Matches(":not(:hover)"));
        }

        [Test]
        public void ForcingHoverLetsTheMoreSpecificHoverRuleWinTheCascade()
        {
            // #target:hover is more specific than #target and declared after it, so once :hover
            // is forced to match, the cascade should resolve to red instead of blue.
            var document = ParseDocument(@"<html><head><style>
                #target { background-color: rgb(0, 0, 255); }
                #target:hover { background-color: rgb(255, 0, 0); }
            </style></head><body><div id=target></div></body></html>");
            var target = document.GetElementById("target");

            target.SetPseudoClass("hover");
            var backgroundColor = target.ComputeCurrentStyle().GetPropertyValue("background-color");

            Assert.AreEqual("rgba(255, 0, 0, 1)", backgroundColor);
        }
    }
}
