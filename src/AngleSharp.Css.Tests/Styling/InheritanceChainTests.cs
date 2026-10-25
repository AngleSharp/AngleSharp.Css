#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class InheritanceChainTests
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
        public void InheritedDeclarationsReachDeeplyNestedElements()
        {
            var document = Parse("<style>:root { --a: 3px; color: red; }</style>" +
                "<div><div><div><div><span id=target style=\"margin-left: var(--a)\">x</span></div></div></div></div>");
            var target = Find(document.DefaultView.Render(), "target");
            var computed = target.ComputedStyle;

            Assert.AreEqual("rgba(255, 0, 0, 1)", computed.GetPropertyValue("color"));
            Assert.AreEqual("3px", computed.GetPropertyValue("--a"));
            Assert.AreEqual("3px", computed.GetPropertyValue("margin-left"));
            Assert.AreEqual("rgba(255, 0, 0, 1)", target.SpecifiedStyle.GetPropertyValue("color"));

            var names = new HashSet<string>(computed.Select(m => m.Name));
            Assert.IsTrue(names.Contains("color"));
            Assert.IsTrue(names.Contains("--a"));
            Assert.AreEqual(computed.Length, names.Count);
        }

        [Test]
        public void NonInheritedDeclarationsDoNotReachChildrenUnlessRequested()
        {
            var document = Parse("<div id=a style=\"width: 10px; border-top-style: solid\">" +
                "<p id=b style=\"border-top-style: inherit\"><span id=c style=\"border-top-style: inherit\">x</span></p></div>");
            var tree = document.DefaultView.Render();

            Assert.AreNotEqual("10px", Find(tree, "b").ComputedStyle.GetPropertyValue("width"));
            Assert.AreNotEqual("10px", Find(tree, "c").ComputedStyle.GetPropertyValue("width"));
            Assert.AreEqual("solid", Find(tree, "b").ComputedStyle.GetPropertyValue("border-top-style"));
            Assert.AreEqual("solid", Find(tree, "c").ComputedStyle.GetPropertyValue("border-top-style"));
        }

        [Test]
        public void ChildOverridesWinOverInheritedDeclarations()
        {
            var document = Parse("<div style=\"color: red; --a: 1px\"><p id=target style=\"color: blue; --a: 2px\">x</p></div>");
            var computed = Find(document.DefaultView.Render(), "target").ComputedStyle;

            Assert.AreEqual("rgba(0, 0, 255, 1)", computed.GetPropertyValue("color"));
            Assert.AreEqual("2px", computed.GetPropertyValue("--a"));
            Assert.AreEqual(1, computed.Count(m => m.Name == "color"));
        }

        [Test]
        public void MutatingAParentStyleDoesNotChangeItsChildren()
        {
            var document = Parse("<div id=parent style=\"color: red; --a: 1px\"><p id=child>x</p></div>");
            var tree = document.DefaultView.Render();
            var parent = Find(tree, "parent");
            var child = Find(tree, "child");

            parent.ComputedStyle.SetProperty("color", "blue");
            parent.ComputedStyle.SetProperty("--a", "5px");
            parent.SpecifiedStyle.SetProperty("color", "blue");

            Assert.AreEqual("rgba(0, 0, 255, 1)", parent.ComputedStyle.GetPropertyValue("color"));
            Assert.AreEqual("rgba(255, 0, 0, 1)", child.ComputedStyle.GetPropertyValue("color"));
            Assert.AreEqual("1px", child.ComputedStyle.GetPropertyValue("--a"));
            Assert.AreEqual("rgba(255, 0, 0, 1)", child.SpecifiedStyle.GetPropertyValue("color"));
        }

        [Test]
        public void MutatingAChildStyleDoesNotChangeItsParent()
        {
            var document = Parse("<div id=parent style=\"color: red\"><p id=child>x</p></div>");
            var tree = document.DefaultView.Render();
            var parent = Find(tree, "parent");
            var child = Find(tree, "child");

            child.ComputedStyle.SetProperty("color", "blue");
            child.ComputedStyle.RemoveProperty("--missing");

            Assert.AreEqual("rgba(0, 0, 255, 1)", child.ComputedStyle.GetPropertyValue("color"));
            Assert.AreEqual("rgba(255, 0, 0, 1)", parent.ComputedStyle.GetPropertyValue("color"));
        }
    }
}
