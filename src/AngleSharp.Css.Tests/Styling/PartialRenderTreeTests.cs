#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading;

    [TestFixture]
    public class PartialRenderTreeTests
    {
        private const string Html = "<style>p::before { content: 'B' } .hidden { display: none } q { color: red }</style>" +
            "<div id=host><span slot=s>slotted</span><b>unassigned</b></div>" +
            "<p>a<br>b <q>quote</q></p><div class=hidden><span>hidden</span></div>";

        private static IDocument Parse()
        {
            var config = Configuration.Default.WithCss().WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument(Html);
            document.GetElementById("host").AttachShadow().InnerHtml = "<style>i { color: blue }</style><i>shadow</i><slot name=s></slot>";
            return document;
        }

        private static ElementRenderNode RenderPartial(IDocument document)
        {
            var builder = RenderTreeBuilder.GetInstance(document.DefaultView);
            return builder.RenderPartial(document.DocumentElement, builder.DefaultDevice);
        }

        [Test]
        public void ACompletedPartialTreeIsTheRenderedTree()
        {
            var document = Parse();
            var expected = (ElementRenderNode)document.DefaultView.Render();
            var partial = RenderPartial(document);

            AssertSame(expected, partial);
        }

        [Test]
        public void ChildrenOfAPartialTreeReadBeforeTheRestAreTheSame()
        {
            var document = Parse();
            var expected = (ElementRenderNode)document.DefaultView.Render();
            var partial = RenderPartial(document);
            var body = partial.Content.OfType<ElementRenderNode>().Single(m => m.Ref.LocalName == "body");

            // the children without generated content first, then all of them
            Assert.IsTrue(body.Content.All(m => m.Ref is not IPseudoElement));
            AssertSame(expected, partial);
        }

        [Test]
        public void ConcurrentFirstReadsOfTheChildrenGetTheSameOnes()
        {
            var partial = RenderPartial(Parse());
            var results = new object[4];

            using (var barrier = new Barrier(results.Length))
            {
                var threads = Enumerable.Range(0, results.Length).Select(i => new Thread(() =>
                {
                    barrier.SignalAndWait();
                    results[i] = i % 2 == 0 ? partial.Children : partial.Content;
                })).ToList();
                threads.ForEach(m => m.Start());
                threads.ForEach(m => m.Join());
            }

            Assert.AreSame(results[0], results[2]);
            Assert.AreSame(results[1], results[3]);
            CollectionAssert.AreEqual((System.Collections.IEnumerable)results[1], partial.Children.Where(m => m.Ref is not IPseudoElement));
        }

        private static bool IsGenerated(IRenderNode node) => node.Ref is IPseudoElement || node.Ref.Parent is null;

        private static void AssertSame(IRenderNode expected, IRenderNode actual)
        {
            // pseudo-elements and their generated text are created for every rendering
            Assert.AreSame(IsGenerated(expected) ? null : expected.Ref, IsGenerated(actual) ? null : actual.Ref);
            Assert.AreEqual(expected.GetType(), actual.GetType());

            if (expected is ElementRenderNode element)
            {
                var other = (ElementRenderNode)actual;
                Assert.AreEqual(element.ComputedStyle.CssText, other.ComputedStyle.CssText, element.Ref.LocalName);
                Assert.AreEqual(element.SpecifiedStyle.CssText, other.SpecifiedStyle.CssText, element.Ref.LocalName);
            }
            else
            {
                Assert.AreEqual(((TextRenderNode)expected).Ref.Data, ((TextRenderNode)actual).Ref.Data);
            }

            var expectedChildren = expected.Children.ToList();
            var actualChildren = actual.Children.ToList();
            Assert.AreEqual(expectedChildren.Count, actualChildren.Count, expected.Ref.NodeName);

            for (var i = 0; i < expectedChildren.Count; i++)
            {
                AssertSame(expectedChildren[i], actualChildren[i]);
            }
        }
    }
}
