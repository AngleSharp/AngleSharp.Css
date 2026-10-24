#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The computed `display` of a floated box, an absolutely positioned box and the root element
    /// is blockified (CSS Display 3 §2.7), and `::placeholder` (CSS Pseudo-Elements 4 §4.3) is
    /// styled from CSS. Found in a downstream renderer (AngleSharp.Renderer): a floated inline
    /// computed as `inline` and caused unbounded recursion, and placeholder text could not be
    /// styled from CSS.
    /// </summary>
    [TestFixture]
    public class BlockificationAndPlaceholderTests
    {
        private static async Task<IDocument> LoadAsync(string html)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            return await context.OpenAsync(req => req.Content(html));
        }

        [TestCase("float: left", "inline", "block")]
        [TestCase("float: right", "inline-block", "block")]
        [TestCase("float: left", "inline-flex", "flex")]
        [TestCase("float: left", "inline-grid", "grid")]
        [TestCase("float: left", "inline-table", "table")]
        [TestCase("float: left", "inline list-item", "list-item")]
        [TestCase("float: left", "table-cell", "block")]
        [TestCase("float: left", "ruby", "block ruby")]
        [TestCase("position: absolute", "inline", "block")]
        [TestCase("position: fixed", "inline-flex", "flex")]
        [TestCase("float: left", "none", "none")]
        [TestCase("float: left", "contents", "contents")]
        [TestCase("float: left", "flow-root", "flow-root")]
        [TestCase("float: none", "inline", "inline")]
        [TestCase("position: relative", "inline", "inline")]
        public async Task FloatsAndAbsolutelyPositionedBoxesAreBlockified(string declaration, string display, string expected)
        {
            var document = await LoadAsync($"<span id=s style='{declaration}; display: {display}'>A</span>");
            var element = document.GetElementById("s");

            var computed = document.DefaultView.GetComputedStyle(element);

            Assert.AreEqual(expected, computed.GetDisplay());
        }

        [Test]
        public async Task UnsetDisplayOfAFloatIsBlockified()
        {
            var document = await LoadAsync("<span id=s style='float: left'>A</span>");

            var computed = document.DefaultView.GetComputedStyle(document.GetElementById("s"));

            Assert.AreEqual("block", computed.GetDisplay());
        }

        [Test]
        public async Task FloatedPseudoElementIsBlockified()
        {
            var document = await LoadAsync("<style>#s::before { content: 'x'; float: left }</style><span id=s>A</span>");

            var computed = document.DefaultView.GetComputedStyle(document.GetElementById("s"), "::before");

            Assert.AreEqual("block", computed.GetDisplay());
        }

        [Test]
        public async Task RootElementIsBlockified()
        {
            var document = await LoadAsync("<style>html { display: inline }</style>");

            var computed = document.DefaultView.GetComputedStyle(document.DocumentElement);

            Assert.AreEqual("block", computed.GetDisplay());
        }

        [Test]
        public async Task RenderTreeCarriesTheBlockifiedDisplay()
        {
            var document = await LoadAsync("<span id=s style='float: left'>A</span>");

            var tree = document.DefaultView.Render();
            var node = Find(tree, document.GetElementById("s"));

            Assert.IsNotNull(node);
            Assert.AreEqual("block", node.ComputedStyle.GetDisplay());
        }

        [Test]
        public async Task PlaceholderPseudoElementTakesAuthorRules()
        {
            var document = await LoadAsync("<style>#b::placeholder { color: rgb(1, 2, 3) }</style><input id=b placeholder=x>");

            var computed = document.DefaultView.GetComputedStyle(document.GetElementById("b"), "::placeholder");

            Assert.AreEqual("rgba(1, 2, 3, 1)", computed.GetColor());
        }

        [Test]
        public async Task PlaceholderPseudoElementInheritsFromItsElement()
        {
            var document = await LoadAsync("<style>#b { font-size: 30px }</style><input id=b placeholder=x>");

            var computed = document.DefaultView.GetComputedStyle(document.GetElementById("b"), "::placeholder");

            Assert.AreEqual("30px", computed.GetFontSize());
        }

        [Test]
        public async Task PlaceholderPseudoElementHasADefaultLook()
        {
            var document = await LoadAsync("<input id=b placeholder=x>");

            var placeholder = document.DefaultView.GetComputedStyle(document.GetElementById("b"), "::placeholder");
            var element = document.DefaultView.GetComputedStyle(document.GetElementById("b"));

            Assert.AreNotEqual(element.GetColor(), placeholder.GetColor());
        }

        [Test]
        public async Task PlaceholderPseudoElementOfATextareaTakesAuthorRules()
        {
            var document = await LoadAsync("<style>textarea::placeholder { color: rgb(1, 2, 3) }</style><textarea id=b placeholder=x></textarea>");

            var computed = document.DefaultView.GetComputedStyle(document.GetElementById("b"), "::placeholder");

            Assert.AreEqual("rgba(1, 2, 3, 1)", computed.GetColor());
        }

        [Test]
        public async Task PlaceholderRulesDoNotApplyToTheElementItself()
        {
            var document = await LoadAsync("<style>#b::placeholder { color: rgb(1, 2, 3) }</style><input id=b placeholder=x>");

            var computed = document.DefaultView.GetComputedStyle(document.GetElementById("b"));

            Assert.AreNotEqual("rgba(1, 2, 3, 1)", computed.GetColor());
        }

        [Test]
        public async Task PlaceholderPseudoElementDoesNotExistWithoutAPlaceholder()
        {
            var document = await LoadAsync("<input id=b><div id=d placeholder=x></div>");

            Assert.IsNull(document.GetElementById("b").Pseudo("placeholder"));
            Assert.IsNull(document.GetElementById("d").Pseudo("placeholder"));
        }

        [Test]
        public void PlaceholderSelectorParses()
        {
            var parser = new CssParser();

            var sheet = parser.ParseStyleSheet("input::placeholder { color: red }");

            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.AreEqual("input::placeholder", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
        }

        private static ElementRenderNode Find(IRenderNode node, INode target)
        {
            if (node.Ref == target)
            {
                return node as ElementRenderNode;
            }

            return node.Children.Select(child => Find(child, target)).FirstOrDefault(found => found is not null);
        }
    }
}
