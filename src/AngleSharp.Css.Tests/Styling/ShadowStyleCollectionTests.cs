#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// The elements of a shadow tree are styled by the style sheets of their shadow root, which
    /// the style collection of the document does not have.
    /// </summary>
    [TestFixture]
    public class ShadowStyleCollectionTests
    {
        [Test]
        public void TheStyleCollectionOfAShadowRootStylesItsElementsAndPseudoElements()
        {
            var document = CssConstructionFunctions.ParseDocument(
                "<html><head><style>p { color: red } input::placeholder { color: red }</style></head><body>" +
                "<div id=host><template shadowrootmode=open><style>p { color: rgb(0, 128, 0) } .input::placeholder { color: rgb(1, 2, 3) }</style>" +
                "<p>shadow</p><input class=input placeholder=x></template></div></body></html>");
            var shadowRoot = document.GetElementById("host").ShadowRoot;
            var styles = shadowRoot.GetStyleCollection(new DefaultRenderDevice());

            var paragraph = shadowRoot.QuerySelector("p");
            var input = shadowRoot.QuerySelector("input");

            Assert.AreEqual("rgba(0, 128, 0, 1)", styles.ComputeDeclarations(paragraph).GetPropertyValue("color"));
            Assert.AreEqual("rgba(1, 2, 3, 1)", styles.ComputeDeclarations(input, "::placeholder").GetPropertyValue("color"));
        }
    }
}
