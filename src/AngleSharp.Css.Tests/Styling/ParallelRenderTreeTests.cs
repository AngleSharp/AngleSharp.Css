#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The explicit styles of a large document are computed on several threads before its render
    /// tree is built, which must give the same tree as computing them one after the other.
    /// </summary>
    [TestFixture]
    public class ParallelRenderTreeTests
    {
        [Test]
        public void ComputingStylesInParallelGivesTheSameRenderTree()
        {
            var html = new StringBuilder("<html><head><style>" +
                ":root { --gap: 4px; --fg: rgb(1, 2, 3) } " +
                ".list > * { margin: var(--gap) } .list > :nth-child(2n) { color: var(--fg) } " +
                ".card a { text-decoration: none } .card [data-x] { padding: 2px } " +
                "section:nth-child(3n) .card { border: 1px solid red } #s7 * { font-weight: 700 } " +
                ".item::before { content: '•' } li:first-child { font-style: italic }" +
                "</style></head><body>");

            for (var i = 0; i < 40; i++)
            {
                html.Append($"<section id=s{i}><div class='card list' data-i={i}><a href=#>Link {i}</a><span data-x>x</span>");
                html.Append($"<ul class=list><li class=item>a</li><li style='color: blue'>b</li><li>c</li></ul>");
                html.Append(i % 10 == 0 ? "<div><template shadowrootmode=open><style>p { color: green }</style><p>s</p><slot></slot></template><b>light</b></div>" : "<p>text</p>");
                html.Append("</div></section>");
            }

            html.Append("</body></html>");
            var document = CssConstructionFunctions.ParseDocument(html.ToString());
            Assert.Greater(document.All.Length, 300);

            var parallel = Describe(document);

            AppContext.SetSwitch(RenderTreeBuilder.DisableParallelismSwitch, true);

            try
            {
                Assert.AreEqual(Describe(document), parallel);
            }
            finally
            {
                AppContext.SetSwitch(RenderTreeBuilder.DisableParallelismSwitch, false);
            }
        }

        private static String Describe(IDocument document)
        {
            var root = (ElementRenderNode)document.DefaultView.Render(new PlainRenderDevice());
            var result = new StringBuilder();
            Describe(root, result);
            return result.ToString();
        }

        private static void Describe(IRenderNode node, StringBuilder result)
        {
            if (node is ElementRenderNode element)
            {
                result.Append(element.Ref.LocalName).Append(" { ").Append(element.SpecifiedStyle.CssText).Append(" } { ")
                    .Append(element.ComputedStyle.CssText).AppendLine(" }");
            }

            foreach (var child in node.Children)
            {
                Describe(child, result);
            }
        }
    }
}
