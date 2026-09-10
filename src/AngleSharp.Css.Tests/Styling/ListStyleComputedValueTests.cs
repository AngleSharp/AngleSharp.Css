#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// A computed style should always resolve to a property's own initial value when nothing in
    /// the cascade sets it explicitly - confirmed this does not happen for `list-style-type`/
    /// `list-style-position` on a `&lt;ul&gt;` while building `display: list-item` support in a
    /// downstream renderer (AngleSharp.Renderer): both come back as an empty string instead of
    /// their real initial values (`disc`, `outside`). `&lt;ol&gt;` is included as a control case -
    /// its UA-stylesheet rule explicitly sets `list-style-type: decimal`, and that value *does*
    /// show up correctly, confirming this is specifically a missing initial-value fallback (for a
    /// property nothing in the cascade ever set), not a blanket "these properties are never
    /// computed" issue.
    /// </summary>
    [TestFixture]
    public class ListStyleComputedValueTests
    {
        [Test]
        public void UnsetListStyleTypeOnUnorderedListResolvesToDisc()
        {
            var document = ParseDocument("<ul id=target><li>Item</li></ul>");
            var target = document.GetElementById("target");

            Assert.AreEqual("disc", target.ComputeCurrentStyle().GetPropertyValue("list-style-type"));
        }

        [Test]
        public void UnsetListStylePositionResolvesToOutside()
        {
            var document = ParseDocument("<ul id=target><li>Item</li></ul>");
            var target = document.GetElementById("target");

            Assert.AreEqual("outside", target.ComputeCurrentStyle().GetPropertyValue("list-style-position"));
        }

        [Test]
        public void OrderedListsUaRuleAlreadyResolvesListStyleTypeCorrectly()
        {
            var document = ParseDocument("<ol id=target><li>Item</li></ol>");
            var target = document.GetElementById("target");

            Assert.AreEqual("decimal", target.ComputeCurrentStyle().GetPropertyValue("list-style-type"));
        }
    }
}
