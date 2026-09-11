#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// `text-overflow` had no registered declaration at all (no `TextOverflowDeclaration`, never
    /// wired into `DefaultDeclarationFactory`) - `ComputeCurrentStyle().GetPropertyValue("text-overflow")`
    /// always reported an empty string, even for an explicitly authored `text-overflow: ellipsis`.
    /// Its own placeholder initial-value constant (`InitialValues.TextOverflowDecl`) also used the
    /// wrong enum entirely (`OverflowMode`, `visible`/`hidden`/`scroll`/`auto`/`clip`) paired with
    /// the wrong keyword (`auto`) for a property whose real initial value is the keyword `clip`.
    /// Found while adding `text-overflow: ellipsis` support to a downstream renderer.
    /// </summary>
    [TestFixture]
    public class TextOverflowComputedStyleTests
    {
        [Test]
        public void TextOverflowEllipsisIsRecognizedAndComputed()
        {
            var document = ParseDocument("<div id=target style=\"text-overflow: ellipsis;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("ellipsis", target.ComputeCurrentStyle().GetPropertyValue("text-overflow"));
        }

        [Test]
        public void TextOverflowClipIsRecognizedAndComputed()
        {
            var document = ParseDocument("<div id=target style=\"text-overflow: clip;\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("clip", target.ComputeCurrentStyle().GetPropertyValue("text-overflow"));
        }

        [Test]
        public void UnsetTextOverflowComputesToEmptyNotItsInitialValue()
        {
            // Matches the same "never serialized when nothing in the cascade set it explicitly"
            // behavior already established for white-space/list-style-type elsewhere in this
            // project - an unset property reports empty rather than resolving to its own CSS
            // initial value, so a consumer must supply that default itself (see
            // AngleSharp.Renderer's ParseTextOverflow).
            var document = ParseDocument("<div id=target></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("", target.ComputeCurrentStyle().GetPropertyValue("text-overflow"));
        }
    }
}
