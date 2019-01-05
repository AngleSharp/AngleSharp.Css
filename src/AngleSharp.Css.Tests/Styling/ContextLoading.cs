namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class ContextLoadingTests
    {
        [Test]
        public async Task ContextLoadPageWithCssAndNoLoaders()
        {
            var url = "http://localhost";
            var source = "<!doctype html><link rel=stylesheet href=http://localhost/beispiel.css type=text/css />";
            var memory = new MemoryStream(Encoding.UTF8.GetBytes(source));
            var config = Configuration.Default.WithCss();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(m => m.Content(memory).Address(url));
            var links = document.QuerySelectorAll("link");
            Assert.AreEqual(1, links.Length);
            var link = links[0] as IHtmlLinkElement;
            Assert.NotNull(link);
            Assert.AreEqual("http://localhost/beispiel.css", link.Href);
        }

        [Test]
        public async Task ContextLoadAmazonWithCss()
        {
            var address = "http://www.amazon.com";
            var config = Configuration.Default.WithPageRequester().WithCss();
            var document = await BrowsingContext.New(config).OpenAsync(address);
            Assert.IsNotNull(document);
            Assert.AreNotEqual(0, document.Body.ChildElementCount);
        }

        [Test]
        public async Task CheckIfAllStyleSheetsAreProcessed()
        {
            var html = @"<html>
  <head>
     <title>test title</title>
     <link href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css' rel='stylesheet'>
    </head>
    <body>
    </body>
</html>";

            var config = Configuration.Default.WithPageRequester(enableResourceLoading: true).WithCss();
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(html));
            Assert.AreEqual(1, document.StyleSheets.Length);
        }

        [Test]
        public async Task GetDownloadsOfExampleDocumentWithCssAndJsShouldYieldAllResources()
        {
            var scripting = new MockScriptEngine(_ => { }, MimeTypeNames.DefaultJavaScript);
            var config = Configuration.Default
                .WithCss()
                .With(scripting)
                .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var content = @"<link rel=stylesheet type=text/css href=bootstraph.css>
<link rel=stylesheet type=text/css href=fontawesome.css>
<link rel=stylesheet type=text/css href=style.css>
<body>
    <img src=foo.png>
    <iframe src=foo.html></iframe>
    <script>alert('Hello World!');</script>
    <script src=test.js></script>
</body>";
            var document = await BrowsingContext.New(config).OpenAsync(res => res.Content(content).Address("http://localhost"));
            var downloads = document.GetDownloads().ToArray();

            Assert.AreEqual(6, downloads.Length);

            foreach (var download in downloads)
            {
                Assert.IsTrue(download.IsCompleted);
                Assert.IsNotNull(download.Source);
            }

            Assert.AreEqual(document.QuerySelectorAll("link").Skip(0).First(), downloads[0].Source);
            Assert.AreEqual(document.QuerySelectorAll("link").Skip(1).First(), downloads[1].Source);
            Assert.AreEqual(document.QuerySelectorAll("link").Skip(2).First(), downloads[2].Source);
            Assert.AreEqual(document.QuerySelector("img"), downloads[3].Source);
            Assert.AreEqual(document.QuerySelector("iframe"), downloads[4].Source);
            Assert.AreEqual(document.QuerySelectorAll("script").Skip(1).First(), downloads[5].Source);
        }
    }
}
