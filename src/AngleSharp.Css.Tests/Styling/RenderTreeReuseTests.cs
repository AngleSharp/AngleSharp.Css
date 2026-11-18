#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Building a render tree remembers what is the same for many elements: the rules the
    /// ancestors of an element bring, and the computed value of a declaration with the same
    /// font sizes. Neither may outlive a change of the DOM or differ between elements.
    /// </summary>
    [TestFixture]
    public class RenderTreeReuseTests
    {
        private static IDocument Parse(string html)
        {
            var config = Configuration.Default.WithCss().WithRenderDevice(new DefaultRenderDevice { ViewPortWidth = 1000, ViewPortHeight = 800 });
            return BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument(html);
        }

        private static ElementRenderNode Find(IRenderNode node, string id)
        {
            if (node is ElementRenderNode element && node.Ref is IElement { Id: var elementId } && elementId == id)
            {
                return element;
            }

            return node.Children.Select(child => Find(child, id)).FirstOrDefault(found => found is not null);
        }

        [Test]
        public void RulesOfAncestorsFollowChangesOfTheDomBetweenRenders()
        {
            var document = Parse("<style>.outer .inner { color: red } .other span { color: blue }</style>" +
                "<div id=a class=outer><section><p class=inner id=b>x</p><span id=c>y</span></section></div>");
            var tree = document.DefaultView.Render();

            Assert.AreEqual("rgba(255, 0, 0, 1)", Find(tree, "b").ComputedStyle.GetPropertyValue("color"));
            Assert.AreNotEqual("rgba(0, 0, 255, 1)", Find(tree, "c").ComputedStyle.GetPropertyValue("color"));

            document.GetElementById("a").ClassName = "other";
            tree = document.DefaultView.Render();

            Assert.AreNotEqual("rgba(255, 0, 0, 1)", Find(tree, "b").ComputedStyle.GetPropertyValue("color"));
            Assert.AreEqual("rgba(0, 0, 255, 1)", Find(tree, "c").ComputedStyle.GetPropertyValue("color"));
        }

        [Test]
        public void RulesOfAncestorsApplyInLargeDocuments()
        {
            // enough elements to compute the styles on several threads
            var html = new StringBuilder("<style>.list li { margin-left: 3px } .list .list li { margin-left: 5px }</style>");

            for (var i = 0; i < 100; i++)
            {
                html.Append("<ul class=list><li id=a").Append(i).Append(">x<ul class=list><li id=b").Append(i).Append(">y</li></ul></li></ul>");
            }

            var tree = Parse(html.ToString()).DefaultView.Render();

            for (var i = 0; i < 100; i++)
            {
                Assert.AreEqual("3px", Find(tree, "a" + i).ComputedStyle.GetPropertyValue("margin-left"));
                Assert.AreEqual("5px", Find(tree, "b" + i).ComputedStyle.GetPropertyValue("margin-left"));
            }
        }

        [Test]
        public void SameDeclarationComputesPerFontSize()
        {
            var document = Parse("<style>p { margin-left: 2em; line-height: 150% }</style>" +
                "<div style=\"font-size: 10px\"><p id=a>x</p></div><div style=\"font-size: 20px\"><p id=b>y</p></div><p id=c style=\"font-size: 10px\">z</p>");
            var tree = document.DefaultView.Render();

            Assert.AreEqual("20px", Find(tree, "a").ComputedStyle.GetPropertyValue("margin-left"));
            Assert.AreEqual("40px", Find(tree, "b").ComputedStyle.GetPropertyValue("margin-left"));
            Assert.AreEqual("20px", Find(tree, "c").ComputedStyle.GetPropertyValue("margin-left"));
            Assert.AreEqual("15px", Find(tree, "a").ComputedStyle.GetPropertyValue("line-height"));
            Assert.AreEqual("30px", Find(tree, "b").ComputedStyle.GetPropertyValue("line-height"));

            document.GetElementById("c").SetAttribute("style", "font-size: 30px");
            tree = document.DefaultView.Render();

            Assert.AreEqual("60px", Find(tree, "c").ComputedStyle.GetPropertyValue("margin-left"));
            Assert.AreEqual("20px", Find(tree, "a").ComputedStyle.GetPropertyValue("margin-left"));
        }

        [Test]
        public void InlineStyleSetByScriptApplies()
        {
            var document = Parse("<p id=a>x</p><p id=b style=\"color: red\">y</p>");
            document.GetElementById("a").GetStyle().SetProperty("color", "blue");
            var tree = document.DefaultView.Render();

            Assert.AreEqual("rgba(0, 0, 255, 1)", Find(tree, "a").ComputedStyle.GetPropertyValue("color"));
            Assert.AreEqual("rgba(255, 0, 0, 1)", Find(tree, "b").ComputedStyle.GetPropertyValue("color"));

            document.GetElementById("b").RemoveAttribute("style");
            tree = document.DefaultView.Render();

            Assert.AreNotEqual("rgba(255, 0, 0, 1)", Find(tree, "b").ComputedStyle.GetPropertyValue("color"));
        }
    }
}
