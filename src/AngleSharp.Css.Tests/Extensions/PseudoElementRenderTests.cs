#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using System.Linq;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// `::before`/`::after` were entirely absent from the render tree `WindowExtensions.Render`/
    /// `RenderTreeBuilder.RenderElement` produce - the tree only ever walked `element.ChildNodes`
    /// for real DOM elements/text, with no awareness of `IElement.Pseudo(...)` at all, even though
    /// `window.GetPseudoElements` (a separate, independent API) already resolved `::before`/`::after`
    /// selector matching and `content` correctly. Found while adding pseudo-element support to a
    /// downstream renderer.
    /// </summary>
    [TestFixture]
    public class PseudoElementRenderTests
    {
        private static ElementRenderNode Render(string html)
        {
            var document = ParseDocument(html);
            var window = document.DefaultView;
            return (ElementRenderNode)window.Render(new PlainRenderDevice());
        }

        private static ElementRenderNode FindElement(ElementRenderNode node, string id)
        {
            if (node.Ref.Id == id)
            {
                return node;
            }

            foreach (var child in node.Children.OfType<ElementRenderNode>())
            {
                var found = FindElement(child, id);

                if (found is not null)
                {
                    return found;
                }
            }

            return null;
        }

        [Test]
        public void BeforePseudoElementIsTheFirstChildWithItsGeneratedTextContent()
        {
            var root = Render("<html><head><style>#target::before { content: \"PREFIX-\"; }</style></head>" +
                "<body><div id=target>middle</div></body></html>");
            var target = FindElement(root, "target");
            var children = target.Children.ToArray();

            Assert.AreEqual(2, children.Length);
            var before = (ElementRenderNode)children[0];
            Assert.IsInstanceOf<IPseudoElement>(before.Ref);
            Assert.AreEqual("before", ((IPseudoElement)before.Ref).PseudoName);
            var beforeText = (TextRenderNode)before.Children.Single();
            Assert.AreEqual("PREFIX-", beforeText.Ref.Data);
            var realText = (TextRenderNode)children[1];
            Assert.AreEqual("middle", realText.Ref.Data);
        }

        [Test]
        public void AfterPseudoElementIsTheLastChildWithItsGeneratedTextContent()
        {
            var root = Render("<html><head><style>#target::after { content: \"-SUFFIX\"; }</style></head>" +
                "<body><div id=target>middle</div></body></html>");
            var target = FindElement(root, "target");
            var children = target.Children.ToArray();

            Assert.AreEqual(2, children.Length);
            var realText = (TextRenderNode)children[0];
            Assert.AreEqual("middle", realText.Ref.Data);
            var after = (ElementRenderNode)children[1];
            Assert.AreEqual("after", ((IPseudoElement)after.Ref).PseudoName);
            var afterText = (TextRenderNode)after.Children.Single();
            Assert.AreEqual("-SUFFIX", afterText.Ref.Data);
        }

        [Test]
        public void PseudoElementWithNoContentDeclarationGeneratesNoNode()
        {
            var root = Render("<html><body><div id=target>middle</div></body></html>");
            var target = FindElement(root, "target");

            Assert.AreEqual(1, target.Children.Count());
            Assert.IsInstanceOf<TextRenderNode>(target.Children.Single());
        }

        [Test]
        public void ExplicitContentNoneGeneratesNoNode()
        {
            var root = Render("<html><head><style>#target::before { content: none; }</style></head>" +
                "<body><div id=target>middle</div></body></html>");
            var target = FindElement(root, "target");

            Assert.AreEqual(1, target.Children.Count());
            Assert.IsInstanceOf<TextRenderNode>(target.Children.Single());
        }

        [Test]
        public void ContentAttrResolvesTheHostsOwnAttribute()
        {
            var root = Render("<html><head><style>#target::before { content: attr(data-label); }</style></head>" +
                "<body><div id=target data-label=\"Hello\">middle</div></body></html>");
            var target = FindElement(root, "target");
            var before = (ElementRenderNode)target.Children.First();
            var beforeText = (TextRenderNode)before.Children.Single();

            Assert.AreEqual("Hello", beforeText.Ref.Data);
        }

        [Test]
        public void MultiplePartsConcatenateInDeclaredOrder()
        {
            var root = Render("<html><head><style>#target::before { content: \"[\" attr(data-label) \"]\"; }</style></head>" +
                "<body><div id=target data-label=\"X\">middle</div></body></html>");
            var target = FindElement(root, "target");
            var before = (ElementRenderNode)target.Children.First();
            var beforeText = (TextRenderNode)before.Children.Single();

            Assert.AreEqual("[X]", beforeText.Ref.Data);
        }

        [Test]
        public void PseudoElementInheritsFromItsHostWhenNotItselfSet()
        {
            var root = Render("<html><head><style>" +
                "#target { color: rgb(10, 20, 30); }" +
                "#target::before { content: \"x\"; }" +
                "</style></head><body><div id=target>middle</div></body></html>");
            var target = FindElement(root, "target");
            var before = (ElementRenderNode)target.Children.First();

            Assert.AreEqual("rgba(10, 20, 30, 1)", before.ComputedStyle.GetPropertyValue("color"));
        }

        [Test]
        public void BothBeforeAndAfterCanCoexistBracketingRealContent()
        {
            var root = Render("<html><head><style>" +
                "#target::before { content: \"<\"; }" +
                "#target::after { content: \">\"; }" +
                "</style></head><body><div id=target>middle</div></body></html>");
            var target = FindElement(root, "target");
            var children = target.Children.ToArray();

            Assert.AreEqual(3, children.Length);
            Assert.AreEqual("before", ((IPseudoElement)((ElementRenderNode)children[0]).Ref).PseudoName);
            Assert.IsInstanceOf<TextRenderNode>(children[1]);
            Assert.AreEqual("after", ((IPseudoElement)((ElementRenderNode)children[2]).Ref).PseudoName);
        }

        /// <summary>
        /// A generated-content pseudo-element defaults to `display: inline` per spec (the same as a
        /// real browser's own UA stylesheet) unless the author overrides it - AngleSharp.Css's UA
        /// stylesheet had no rule for `::before`/`::after` at all, so an unstyled one computed
        /// `display: block` (this project's general "unset display defaults to block" fallback,
        /// correct for most real elements but wrong for generated content specifically), which would
        /// have made every `::before`/`::after` start its own new block line instead of flowing
        /// inline with the rest of its host's content - the overwhelmingly common real-world case
        /// (an icon, a required-field marker, a breadcrumb separator, ...). Fixed with a targeted
        /// `*:before, *:after { display: inline }` UA-stylesheet addition, the same "give the right
        /// initial value via a UA rule" precedent already used for `list-style-type`.
        /// </summary>
        [Test]
        public void PseudoElementDefaultsToInlineDisplay()
        {
            var root = Render("<html><head><style>#target::before { content: \"x\"; }</style></head>" +
                "<body><div id=target>middle</div></body></html>");
            var target = FindElement(root, "target");
            var before = (ElementRenderNode)target.Children.First();

            Assert.AreEqual("inline", before.ComputedStyle.GetDisplay());
        }
    }
}
