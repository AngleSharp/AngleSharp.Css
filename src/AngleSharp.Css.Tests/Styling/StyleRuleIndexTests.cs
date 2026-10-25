#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The cascade only matches an element against the rules grouped under its
    /// id, classes, and tag name (plus the rules that cannot be grouped), and a
    /// pseudo-element only against the rules targeting pseudo-elements. Matching
    /// every rule against every element made pages with large style sheets (e.g.,
    /// GitHub) take minutes to render in a downstream browser.
    /// </summary>
    [TestFixture]
    public class StyleRuleIndexTests
    {
        private const string Green = "rgba(0, 128, 0, 1)";

        private static async Task<string> ColorOfAsync(string css, string body, string id = "t")
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = await context.OpenAsync(req => req.Content($"<!doctype html><style>{css}</style>{body}"));
            return document.DefaultView.GetComputedStyle(document.GetElementById(id)).GetColor();
        }

        [TestCase("[data-x] { color: red } .a { color: green }")]
        [TestCase(".a { color: red } [data-x] { color: green }")]
        [TestCase("[data-x][data-x] { color: red } .a.b { color: red } div[data-x] { color: blue } .b[data-x] { color: green }")]
        [TestCase("#t { color: red } * { color: blue } div#t { color: green }")]
        public async Task GroupedAndUngroupedRulesKeepTheCascadeOrder(string css)
        {
            var color = await ColorOfAsync(css, "<div id=t class='a b' data-x>A</div>");

            Assert.AreEqual(Green, color);
        }

        [TestCase(".a, .b { color: green }", "<p id=t class='a b'>A</p>")]
        [TestCase(".x, #t { color: green }", "<p id=t>A</p>")]
        [TestCase(".x, p { color: green }", "<p id=t>A</p>")]
        [TestCase(".x, [data-y] { color: green }", "<p id=t data-y>A</p>")]
        [TestCase("#t.y { color: green }", "<p id=t class=y>A</p>")]
        [TestCase("p.y { color: green }", "<p id=t class=y>A</p>")]
        [TestCase(".a span { color: green }", "<div class=a><span id=t>A</span></div>")]
        [TestCase(".a > .b + .c { color: green }", "<div class=a><i class=b></i><i id=t class=c>A</i></div>")]
        [TestCase("P { color: green }", "<p id=t>A</p>")]
        [TestCase("div:not(.a) { color: green }", "<div id=t class=b>A</div>")]
        [TestCase(":is(.a, .b) { color: green }", "<div id=t class=b>A</div>")]
        [TestCase(":where(#x) .a, :has(.c) { color: green }", "<div id=t><i class=c></i></div>")]
        [TestCase("foreignObject { color: green }", "<svg><foreignObject id=t>A</foreignObject></svg>")]
        [TestCase("@namespace svg url(http://www.w3.org/2000/svg); svg|text { color: green }", "<svg><text id=t>A</text></svg>")]
        [TestCase(".parent { .child { color: green } }", "<div class=parent><p id=t class=child>A</p></div>")]
        [TestCase(".parent { & .child { color: green } }", "<div class=parent><p id=t class=child>A</p></div>")]
        [TestCase("@media screen { .a { color: green } }", "<p id=t class=a>A</p>")]
        public async Task RulesAreFoundViaTheirSubject(string css, string body)
        {
            var color = await ColorOfAsync(css, body);

            Assert.AreEqual(Green, color);
        }

        [TestCase("#x.y { color: red }", "<p id=t class=y>A</p>")]
        [TestCase(".a span { color: red }", "<div class=a><b id=t>A</b></div>")]
        [TestCase(".a .b { color: red }", "<div class=b><p id=t class=a>A</p></div>")]
        public async Task RulesForOtherSubjectsDoNotApply(string css, string body)
        {
            var color = await ColorOfAsync(css, body);

            Assert.AreNotEqual("rgba(255, 0, 0, 1)", color);
        }

        [Test]
        public void PseudoElementsAreOnlyGeneratedWithContent()
        {
            var document = CssConstructionFunctions.ParseDocument("<html><head><style>" +
                "*, ::before, ::after { box-sizing: border-box } .a::before { content: 'A' } .b::after { content: 'B'; color: green }" +
                "</style></head><body><p id=a class=a>1</p><p id=b class=b>2</p><p id=c>3</p></body></html>");
            var root = (ElementRenderNode)document.DefaultView.Render(new PlainRenderDevice());
            var nodes = Flatten(root).ToArray();

            var a = nodes.First(m => m.Ref.Id == "a").Children.OfType<ElementRenderNode>().ToArray();
            var b = nodes.First(m => m.Ref.Id == "b").Children.OfType<ElementRenderNode>().ToArray();
            var c = nodes.First(m => m.Ref.Id == "c").Children.OfType<ElementRenderNode>().ToArray();

            Assert.AreEqual(1, a.Length);
            Assert.IsInstanceOf<IPseudoElement>(a[0].Ref);
            Assert.AreEqual(1, b.Length);
            Assert.AreEqual(Green, b[0].ComputedStyle.GetPropertyValue("color"));
            Assert.AreEqual(0, c.Length);
        }

        private static System.Collections.Generic.IEnumerable<ElementRenderNode> Flatten(ElementRenderNode node) =>
            new[] { node }.Concat(node.Children.OfType<ElementRenderNode>().SelectMany(Flatten));
    }
}
