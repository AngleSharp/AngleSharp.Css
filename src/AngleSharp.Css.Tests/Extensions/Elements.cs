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

            Assert.That(divs.Skip(0).First().GetStyle().GetBackground(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(divs.Skip(1).First().GetStyle().GetBackground(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(divs.Skip(2).First().GetStyle().GetBackground(), Is.EqualTo("rgba(255, 0, 0, 1)"));
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
            var tree = document.DefaultView.Render();
            var node = tree.Find(document.QuerySelector("div"));
            await node.DownloadResources();
            Assert.That(urls.Count, Is.EqualTo(1));
            Assert.That(urls[0].Href, Is.EqualTo("https://avatars1.githubusercontent.com/u/10828168?s=200&v=4"));
        }
    }
}
