#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The CSS part of the popover API (HTML, "The popover attribute"): the `:popover-open`
    /// pseudo-class, the `::backdrop` pseudo-element and the user-agent styles that hide a
    /// popover until it is shown and then center it in the viewport. The DOM part (the
    /// `popover` attribute, `ShowPopover` and friends) lives in AngleSharp itself.
    /// </summary>
    [TestFixture]
    public class PopoverTests
    {
        private static IHtmlDocument ParsePopoverDocument() =>
            ParseDocument("<button popovertarget=menu>Open</button><div id=menu popover>Hello</div>");

        [Test]
        public void PopoverOpenDoesNotMatchAClosedPopover()
        {
            var document = ParsePopoverDocument();
            var menu = document.GetElementById("menu");

            Assert.IsFalse(menu.Matches(":popover-open"));
            Assert.IsTrue(menu.Matches(":not(:popover-open)"));
        }

        [Test]
        public void PopoverOpenMatchesAShownPopover()
        {
            var document = ParsePopoverDocument();
            var menu = document.GetElementById("menu") as IHtmlElement;

            menu.ShowPopover();

            Assert.IsTrue(menu.Matches(":popover-open"));
            Assert.IsFalse(menu.Matches(":not(:popover-open)"));
        }

        [Test]
        public void PopoverOpenStopsMatchingAHiddenPopover()
        {
            var document = ParsePopoverDocument();
            var menu = document.GetElementById("menu") as IHtmlElement;

            menu.ShowPopover();
            menu.HidePopover();

            Assert.IsFalse(menu.Matches(":popover-open"));
        }

        [Test]
        public void PopoverOpenDoesNotMatchANonPopoverElement()
        {
            var document = ParseDocument("<div id=target>x</div>");
            var target = document.GetElementById("target");

            Assert.IsFalse(target.Matches(":popover-open"));
        }

        [Test]
        public void PopoverOpenCanBeForcedLikeOtherPseudoClasses()
        {
            var document = ParsePopoverDocument();
            var menu = document.GetElementById("menu");

            menu.SetPseudoClass("popover-open");

            Assert.IsTrue(menu.Matches(":popover-open"));
        }

        [Test]
        public void ClosedPopoverIsHiddenByDefault()
        {
            var document = ParsePopoverDocument();
            var menu = document.GetElementById("menu");

            Assert.AreEqual("none", menu.ComputeCurrentStyle().GetPropertyValue("display"));
        }

        [Test]
        public void ShownPopoverIsDisplayedAndCenteredInTheViewport()
        {
            var document = ParsePopoverDocument();
            var menu = document.GetElementById("menu") as IHtmlElement;

            menu.ShowPopover();
            var style = menu.ComputeCurrentStyle();

            Assert.AreEqual("block", style.GetPropertyValue("display"));
            Assert.AreEqual("fixed", style.GetPropertyValue("position"));
            Assert.AreEqual("0px", style.GetPropertyValue("top"));
            Assert.AreEqual("0px", style.GetPropertyValue("right"));
            Assert.AreEqual("0px", style.GetPropertyValue("bottom"));
            Assert.AreEqual("0px", style.GetPropertyValue("left"));
            Assert.AreEqual("auto", style.GetPropertyValue("margin-top"));
            Assert.AreEqual("auto", style.GetPropertyValue("margin-left"));
            Assert.AreEqual("fit-content", style.GetPropertyValue("width"));
            Assert.AreEqual("fit-content", style.GetPropertyValue("height"));
            Assert.AreEqual("auto", style.GetPropertyValue("overflow-x"));
            Assert.AreEqual("solid", style.GetPropertyValue("border-top-style"));
        }

        [Test]
        public void HiddenAttributeStillHidesAShownPopover()
        {
            var document = ParseDocument("<div id=menu popover hidden>Hello</div>");
            var menu = document.GetElementById("menu");

            Assert.AreEqual("none", menu.ComputeCurrentStyle().GetPropertyValue("display"));
        }

        [Test]
        public void AuthorStylesCanOverrideThePopoverDefaults()
        {
            var document = ParseDocument(@"<html><head><style>
                #menu:popover-open { background-color: rgb(255, 0, 0) }
            </style></head><body><div id=menu popover>Hello</div></body></html>");
            var menu = document.GetElementById("menu") as IHtmlElement;

            menu.ShowPopover();

            Assert.AreEqual("rgba(255, 0, 0, 1)", menu.ComputeCurrentStyle().GetPropertyValue("background-color"));
        }

        [Test]
        public void ShownPopoverHasATransparentBackdrop()
        {
            var document = ParsePopoverDocument();
            var menu = document.GetElementById("menu") as IHtmlElement;

            menu.ShowPopover();
            var style = document.DefaultView.GetComputedStyle(menu, "::backdrop");

            Assert.AreEqual("fixed", style.GetPropertyValue("position"));
            Assert.AreEqual("0px", style.GetPropertyValue("top"));
            Assert.AreEqual("none", style.GetPropertyValue("pointer-events"));
            Assert.AreEqual("rgba(0, 0, 0, 0)", style.GetPropertyValue("background-color"));
        }

        [Test]
        public void BackdropStylesOfAuthorsApply()
        {
            var document = ParseDocument(@"<html><head><style>
                #menu::backdrop { background-color: rgb(0, 0, 255) }
            </style></head><body><div id=menu popover>Hello</div></body></html>");
            var menu = document.GetElementById("menu") as IHtmlElement;

            menu.ShowPopover();
            var style = document.DefaultView.GetComputedStyle(menu, "::backdrop");

            Assert.AreEqual("rgba(0, 0, 255, 1)", style.GetPropertyValue("background-color"));
        }
    }
}
