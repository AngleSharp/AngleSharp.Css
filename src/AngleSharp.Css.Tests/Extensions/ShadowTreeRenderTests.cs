#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The render tree only walked the light DOM, so the content of a shadow root (e.g. MDN's
    /// search button) never showed. A shadow host renders its shadow tree - styled by the shadow
    /// root's own style sheets - and a slot in there the light nodes assigned to it.
    /// </summary>
    [TestFixture]
    public class ShadowTreeRenderTests
    {
        private static IDocument CreateHost(string light, string shadow)
        {
            var document = ParseDocument($"<html><head><style>span {{ color: red }}</style></head><body><div id=host>{light}</div></body></html>");
            document.GetElementById("host").AttachShadow().InnerHtml = shadow;
            return document;
        }

        // :host needs AngleSharp 1.8 or later, while the minimum version is 1.5
        private static void AssumeHostIsSupported()
        {
            var parser = BrowsingContext.New(Configuration.Default.WithCss()).GetService<ICssSelectorParser>();
            Assume.That(parser.ParseSelector(":host"), Is.Not.Null, "The AngleSharp version does not support :host.");
        }

        private static ElementRenderNode Render(IDocument document) =>
            (ElementRenderNode)document.DefaultView.Render(new PlainRenderDevice());

        private static IEnumerable<ElementRenderNode> Flatten(ElementRenderNode node) =>
            new[] { node }.Concat(node.Children.OfType<ElementRenderNode>().SelectMany(Flatten));

        // the text that shows, i.e. without the one of hidden elements (like style)
        private static string TextOf(ElementRenderNode node) =>
            node.ComputedStyle.GetDisplay() == "none"
                ? string.Empty
                : string.Concat(node.Children.Select(m => m is TextRenderNode text ? text.Ref.TextContent : m is ElementRenderNode element ? TextOf(element) : string.Empty));

        private static ElementRenderNode Host(ElementRenderNode root) => Flatten(root).First(m => m.Ref.Id == "host");

        [Test]
        public void HostRendersItsShadowTreeInsteadOfItsChildren()
        {
            var document = CreateHost("<p>Light</p>", "<b>Shadow</b>");

            var host = Host(Render(document));

            Assert.AreEqual("Shadow", TextOf(host));
        }

        [Test]
        public void SlotsShowTheLightNodesAssignedToThem()
        {
            var document = CreateHost("<i slot=title>Title</i><p>Body</p>", "[<slot name=title></slot>|<slot></slot>]");

            var host = Host(Render(document));

            Assert.AreEqual("[Title|Body]", TextOf(host));
        }

        [Test]
        public void SlotsWithoutAssignedNodesShowTheirFallback()
        {
            var document = CreateHost("", "<slot name=title>Fallback</slot>");

            var host = Host(Render(document));

            Assert.AreEqual("Fallback", TextOf(host));
        }

        [Test]
        public void HiddenSlotsHideTheirAssignedNodes()
        {
            // e.g. a closed dropdown: :host(:not([open])) slot[name=dropdown] { display: none }
            var document = CreateHost("<b slot=dropdown>Panel</b><i>Button</i>", "<style>slot[name=dropdown] { display: none }</style><slot></slot><slot name=dropdown>Fallback</slot>");

            var host = Host(Render(document));

            Assert.AreEqual("Button", TextOf(host));
        }

        [Test]
        public void HostRulesStyleTheHost()
        {
            AssumeHostIsSupported();

            // e.g. MDN's custom elements: :host { display: contents }
            var document = CreateHost("", "<style>:host { display: inline-block; color: green }</style>Shadow");

            var host = Host(Render(document));

            Assert.AreEqual("inline-block", host.ComputedStyle.GetDisplay());
            Assert.AreEqual("rgba(0, 128, 0, 1)", host.ComputedStyle.GetColor());
        }

        [TestCase(":host { color: green }", "")]
        [TestCase(":host { color: red }", "#host { color: green }")]
        [TestCase(":host { color: red }", "div { color: green }")]
        [TestCase(":host { color: green !important }", "#host { color: red }")]
        [TestCase(":host { color: green !important }", "#host { color: red !important }")]
        [TestCase(":host { color: red } :host { color: green }", "")]
        [TestCase("div, :host { color: green }", "")]
        public void HostRulesLoseAgainstTheHostsTreeUnlessImportant(string shadowCss, string outerCss)
        {
            AssumeHostIsSupported();

            var document = ParseDocument($"<html><head><style>{outerCss}</style></head><body><div id=host></div></body></html>");
            document.GetElementById("host").AttachShadow().InnerHtml = $"<style>{shadowCss}</style>";

            var host = Host(Render(document));

            Assert.AreEqual("rgba(0, 128, 0, 1)", host.ComputedStyle.GetColor());
        }

        [TestCase("div { color: red }")]
        [TestCase("#host { color: red }")]
        [TestCase(":host div { color: red }")]
        [TestCase(":host > * { color: red }")]
        public void OtherShadowRulesDoNotStyleTheHost(string shadowCss)
        {
            AssumeHostIsSupported();

            var document = ParseDocument("<html><head><style>div { color: green }</style></head><body><div id=host></div></body></html>");
            document.GetElementById("host").AttachShadow().InnerHtml = $"<style>{shadowCss}</style>";

            var host = Host(Render(document));

            Assert.AreEqual("rgba(0, 128, 0, 1)", host.ComputedStyle.GetColor());
        }

        [Test]
        public void ShadowTreesAreStyledByTheirOwnStyleSheets()
        {
            var document = CreateHost("<span id=light>Light</span>", "<style>span { color: green }</style><span id=shadow>Shadow</span><slot></slot>");

            var nodes = Flatten(Render(document)).ToList();
            var shadow = nodes.First(m => m.Ref.Id == "shadow");
            var light = nodes.First(m => m.Ref.Id == "light");

            Assert.AreEqual("rgba(0, 128, 0, 1)", shadow.ComputedStyle.GetColor());
            Assert.AreEqual("rgba(255, 0, 0, 1)", light.ComputedStyle.GetColor());
        }
    }
}
