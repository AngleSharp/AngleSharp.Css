namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class ElementsTests
    {
        [Test]
        public void SetAllStyles()
        {
            var document = "<div></div><div></div><div></div>".ToHtmlDocument(Configuration.Default.WithCss());
            var divs = document.QuerySelectorAll("div");
            divs.SetStyle(style => style.SetBackground("red"));

            Assert.AreEqual("rgba(255, 0, 0, 1)", divs.Skip(0).First().GetStyle().GetBackground());
            Assert.AreEqual("rgba(255, 0, 0, 1)", divs.Skip(1).First().GetStyle().GetBackground());
            Assert.AreEqual("rgba(255, 0, 0, 1)", divs.Skip(2).First().GetStyle().GetBackground());
        }

        [Test]
        public async Task DownloadResources()
        {
            var urls = new List<Url>();
            var loaderOptions = new LoaderOptions
            {
                IsResourceLoadingEnabled = true,
                Filter = (req) =>
                {
                    urls.Add(req.Address);
                    return true;
                },
            };
            var config = Configuration.Default
                .WithDefaultLoader(loaderOptions)
                .WithRenderDevice()
                .WithCss();
            var document = "<style>div { background: url('https://avatars1.githubusercontent.com/u/10828168?s=200&v=4'); }</style><div></div>".ToHtmlDocument(config);
            var tree = document.DefaultView!.Render();
            var node = tree.Find(document.QuerySelector("div"));
            await node.DownloadResources();
            Assert.AreEqual(1, urls.Count);
            Assert.AreEqual("https://avatars1.githubusercontent.com/u/10828168?s=200&v=4", urls[0].Href);
        }

        [Test]
        public async Task DownloadResourcesFromVisibleSubtree()
        {
            var urls = new List<Url>();
            var loaderOptions = new LoaderOptions
            {
                IsResourceLoadingEnabled = true,
                Filter = (req) =>
                {
                    urls.Add(req.Address);
                    return true;
                },
            };
            var config = Configuration.Default
                .WithDefaultLoader(loaderOptions)
                .WithRenderDevice()
                .WithCss();
            var document = @"<style>
div {
    background-image: url('https://example.com/background.png');
    border-image-source: url('https://example.com/border.png');
    cursor: url('https://example.com/cursor.cur'), auto;
}
ul {
    list-style-image: url('https://example.com/list.png');
}
</style>
<div><ul><li>Item</li></ul></div>".ToHtmlDocument(config);

            var tree = document.DefaultView!.Render();
            await tree.DownloadResources();

            CollectionAssert.AreEquivalent(new[]
            {
                "https://example.com/background.png",
                "https://example.com/border.png",
                "https://example.com/cursor.cur",
                "https://example.com/list.png",
            }, urls.Select(m => m.Href).ToArray());
        }

        [Test]
        public async Task DownloadResourcesSkipsHiddenSubtree()
        {
            var urls = new List<Url>();
            var loaderOptions = new LoaderOptions
            {
                IsResourceLoadingEnabled = true,
                Filter = (req) =>
                {
                    urls.Add(req.Address);
                    return true;
                },
            };
            var config = Configuration.Default
                .WithDefaultLoader(loaderOptions)
                .WithRenderDevice()
                .WithCss();
            var document = @"<style>
.hidden { display: none; }
.hidden div { background-image: url('https://example.com/hidden-background.png'); }
.hidden ul { list-style-image: url('https://example.com/hidden-list.png'); }
</style>
<section class='hidden'><div><ul><li>Item</li></ul></div></section>".ToHtmlDocument(config);

            var tree = document.DefaultView!.Render();
            await tree.DownloadResources();

            Assert.AreEqual(0, urls.Count);
        }

        [Test]
        public async Task DownloadResourcesDeduplicatesMatchingUrls()
        {
            var urls = new List<Url>();
            var loaderOptions = new LoaderOptions
            {
                IsResourceLoadingEnabled = true,
                Filter = (req) =>
                {
                    urls.Add(req.Address);
                    return true;
                },
            };
            var config = Configuration.Default
                .WithDefaultLoader(loaderOptions)
                .WithRenderDevice()
                .WithCss();
            var document = @"<style>
div {
    background-image: url('https://example.com/shared.png');
    border-image-source: url('https://example.com/shared.png');
}
span {
    list-style-image: url('https://example.com/shared.png');
}
</style>
<div><span>Item</span></div>".ToHtmlDocument(config);

            var tree = document.DefaultView!.Render();
            await tree.DownloadResources();

            CollectionAssert.AreEqual(new[]
            {
                "https://example.com/shared.png",
            }, urls.Select(m => m.Href).ToArray());
        }

        [Test]
        public void RenderBuildsElementAndTextNodeTree()
        {
            var document = "<style>body { color: green; } .box { font-size: 1.5rem; }</style><body><div class='box'>Hello <span>world</span></div></body>"
                .ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var tree = document.DefaultView!.Render();
            var boxElement = document.QuerySelector(".box")!;
            var spanElement = document.QuerySelector("span")!;

            var boxNode = tree.Find(boxElement) as ElementRenderNode;
            var spanNode = tree.Find(spanElement) as ElementRenderNode;

            Assert.IsNotNull(boxNode);
            Assert.IsNotNull(spanNode);
            Assert.AreEqual("24px", boxNode!.ComputedStyle.GetFontSize());
            Assert.AreEqual("rgba(0, 128, 0, 1)", spanNode!.ComputedStyle.GetColor());

            var textChildren = boxNode.Children.OfType<TextRenderNode>().ToArray();
            Assert.IsTrue(textChildren.Length > 0);
            Assert.AreSame(boxNode, textChildren[0].Parent);
        }

        [Test]
        public void RenderExposesCascadedStyleForElement()
        {
            var document = "<style>.parent { color: green; } .child { color: inherit; }</style><div class='parent'><span class='child'>Item</span></div>"
                .ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var tree = document.DefaultView!.Render();
            var childElement = document.QuerySelector(".child")!;
            var childNode = tree.Find(childElement) as ElementRenderNode;

            Assert.IsNotNull(childNode);
            Assert.AreEqual("rgba(0, 128, 0, 1)", childNode!.SpecifiedStyle.GetColor());
            Assert.AreEqual("rgba(0, 128, 0, 1)", childNode.ComputedStyle.GetColor());
        }
    }
}
