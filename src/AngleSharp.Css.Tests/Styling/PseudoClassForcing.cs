#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Tests the generic pseudo-class forcing API (SetPseudoClass/GetPseudoClass/RemovePseudoClass/
    /// ClearPseudoClasses), which lets a caller simulate interaction states such as `:hover` and
    /// `:active` that AngleSharp otherwise never reports as matching - analogous to what browser
    /// devtools expose via the Chrome DevTools Protocol's `CSS.forcePseudoState`.
    /// </summary>
    [TestFixture]
    public class PseudoClassForcingTests
    {
        [Test]
        public void GetPseudoClassReturnsNullWhenNothingHasBeenForced()
        {
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            Assert.IsNull(target.GetPseudoClass("hover"));
        }

        [Test]
        public void SetPseudoClassForcesTheGivenStateAndGetPseudoClassReportsIt()
        {
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            target.SetPseudoClass("hover");

            Assert.AreEqual(true, target.GetPseudoClass("hover"));
            Assert.IsTrue(target.Matches(":hover"));
        }

        [Test]
        public void SetPseudoClassAcceptsALeadingColon()
        {
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            target.SetPseudoClass(":active");

            Assert.IsTrue(target.Matches(":active"));
        }

        [Test]
        public void RemovePseudoClassRevertsToTheNaturalState()
        {
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            target.SetPseudoClass("hover");
            target.RemovePseudoClass("hover");

            Assert.IsNull(target.GetPseudoClass("hover"));
            Assert.IsFalse(target.Matches(":hover"));
        }

        [Test]
        public void ClearPseudoClassesRemovesEveryForcedStateForTheElement()
        {
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            target.SetPseudoClass("hover");
            target.SetPseudoClass("active");
            target.ClearPseudoClasses();

            Assert.IsFalse(target.Matches(":hover"));
            Assert.IsFalse(target.Matches(":active"));
        }

        [Test]
        public void ForcingIsExplicitAndDoesNotPropagateToAncestors()
        {
            var document = ParseDocument("<div id=parent><span id=child></span></div>");
            var child = document.GetElementById("child");
            var parent = document.GetElementById("parent");

            child.SetPseudoClass("hover");

            Assert.IsTrue(child.Matches(":hover"));
            Assert.IsFalse(parent.Matches(":hover"), "forcing a pseudo-class on an element must not implicitly affect its ancestors.");
        }

        [Test]
        public void ForcingCanBeExplicitlySetToFalseToOverrideTheNaturalState()
        {
            var document = ParseDocument("<a id=target href=\"#foo\"></a>");
            var target = document.GetElementById("target");

            target.SetPseudoClass("active", false);

            Assert.AreEqual(false, target.GetPseudoClass("active"));
            Assert.IsFalse(target.Matches(":active"));
        }

        [Test]
        public void ForcingGeneralizesToOtherHardcodedPseudoClassesLikeVisited()
        {
            var document = ParseDocument("<a id=target href=\"http://example.com/\"></a>");
            var target = document.GetElementById("target");

            target.SetPseudoClass("visited");

            Assert.IsTrue(target.Matches(":visited"));
        }

        [Test]
        public void SettingFocusDelegatesToTheRealFocusStateInsteadOfAForcedOverride()
        {
            // Only elements whose DoFocus() implementation actually sets focus (e.g. anchors with
            // an href) can be focused today - this is a real, separate AngleSharp core limitation.
            var document = ParseDocument("<a id=target href=\"#foo\"></a>");
            var target = document.GetElementById("target") as IHtmlElement;

            target.SetPseudoClass("focus");

            Assert.IsTrue(target.Matches(":focus"));
            Assert.AreEqual(true, target.GetPseudoClass("focus"));
            Assert.AreSame(target, document.ActiveElement);
        }

        [Test]
        public void SettingFocusOnAnElementMakesFocusWithinMatchOnItsAncestorsNaturally()
        {
            var document = ParseDocument("<div id=parent><a id=target href=\"#foo\"></a></div>");
            var target = document.GetElementById("target") as IHtmlElement;
            var parent = document.GetElementById("parent");

            target.SetPseudoClass("focus");

            Assert.IsTrue(parent.Matches(":focus-within"), "focus-within is derived from real focus state, so it works without any forcing.");
        }

        [Test]
        public void RemovingFocusAttemptsToBlurTheElement()
        {
            // removing the forced focus calls DoBlur(), which clears the focus since AngleSharp
            // 1.8.6 (a no-op before), so :focus then follows the real focus state
            var document = ParseDocument("<a id=target href=\"#foo\"></a>");
            var target = document.GetElementById("target") as IHtmlElement;

            target.SetPseudoClass("focus");
            target.RemovePseudoClass("focus");

            Assert.AreEqual(target.IsFocused, target.Matches(":focus"));
        }
    }
}
