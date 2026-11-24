#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Media;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ImageSourcesTests
    {
        private static readonly String BaseUrl = "http://localhost/";

        private static IHtmlImageElement Construct(String html, IRenderDevice device = null, IConfiguration config = null)
        {
            config ??= Configuration.Default;
            var context = BrowsingContext.New(config.WithCss().WithOnly(device ?? new DefaultRenderDevice { ViewPortWidth = 1280, ViewPortHeight = 800 }));
            var document = context.OpenAsync(m => m.Content(html).Address(BaseUrl)).Result;
            return document.QuerySelector<IHtmlImageElement>("img");
        }

        private static String Source(String html, IRenderDevice device = null, IConfiguration config = null) =>
            Construct(html, device, config).GetImageSource()?.Url.Href;

        [Test]
        public void ImageWithSrcOnlyShowsItsSrc()
        {
            Assert.AreEqual(BaseUrl + "cat.jpg", Source("<img src=cat.jpg alt=cat>"));
        }

        [Test]
        public void ImageWithoutSourceShowsNothing()
        {
            Assert.IsNull(Source("<img alt=cat>"));
        }

        [Test]
        public void DensityCandidateOfOneIsTakenOnAStandardDevice()
        {
            Assert.AreEqual(BaseUrl + "cat.jpg", Source("<img src=cat.jpg srcset=\"cat.jpg, cat-2x.jpg 2x\">"));
        }

        [Test]
        public void DensityCandidateOfTheDevicePixelRatioIsTaken()
        {
            var device = new DefaultRenderDevice { ViewPortWidth = 1280, Resolution = 192 };
            Assert.AreEqual(BaseUrl + "cat-2x.jpg", Source("<img src=cat.jpg srcset=\"cat-2x.jpg 2x, cat-3x.jpg 3x\">", device));
        }

        [Test]
        public void SrcIsTheCandidateOfDensityOne()
        {
            Assert.AreEqual(BaseUrl + "cat.jpg", Source("<img src=cat.jpg srcset=\"cat-2x.jpg 2x\">"));
        }

        [Test]
        public void WidthCandidateFittingTheViewportIsTakenWithoutSizes()
        {
            var html = "<img src=cat.jpg srcset=\"cat-160.jpg 160w, cat-320.jpg 320w, cat-640.jpg 640w, cat-1280.jpg 1280w\">";
            Assert.AreEqual(BaseUrl + "cat-1280.jpg", Source(html));
            Assert.AreEqual(BaseUrl + "cat-320.jpg", Source(html, new DefaultRenderDevice { ViewPortWidth = 300 }));
        }

        [TestCase("300px", "cat-400.jpg")]
        [TestCase("50vw", "cat-800.jpg")]
        [TestCase("calc(20vw - 56px)", "cat-200.jpg")]
        [TestCase("(max-width: 600px) 480px, 150px", "cat-200.jpg")]
        [TestCase("(min-width: 600px) 380px, 150px", "cat-400.jpg")]
        [TestCase("auto, 300px", "cat-400.jpg")]
        public void WidthCandidateFittingTheSizesIsTaken(String sizes, String expected)
        {
            var html = $"<img src=cat.jpg srcset=\"cat-200.jpg 200w, cat-400.jpg 400w, cat-800.jpg 800w\" sizes=\"{sizes}\">";
            Assert.AreEqual(BaseUrl + expected, Source(html));
        }

        [Test]
        public void DensestCandidateIsTakenIfNoneIsDenseEnough()
        {
            var device = new DefaultRenderDevice { ViewPortWidth = 1280, Resolution = 192 };
            Assert.AreEqual(BaseUrl + "cat-400.jpg", Source("<img srcset=\"cat-200.jpg 200w, cat-400.jpg 400w\">", device));
        }

        [Test]
        public void UrlOfADataCandidateKeepsItsCommas()
        {
            Assert.AreEqual("data:image/png;base64,AAAA", Source("<img srcset=\"data:image/png;base64,AAAA 1x, cat-2x.jpg 2x\">"));
        }

        [Test]
        public void PictureSourceWhoseMediaMatchesIsTaken()
        {
            var html = @"<picture>
  <source media=""(min-width: 900px)"" srcset=""cat-vertical.jpg"">
  <source media=""(min-width: 750px)"" srcset=""cat-horizontal.jpg"">
  <img src=""cat.jpg"" alt=""cat"">
</picture>";
            Assert.AreEqual(BaseUrl + "cat-vertical.jpg", Source(html));
            Assert.AreEqual(BaseUrl + "cat-horizontal.jpg", Source(html, new DefaultRenderDevice { ViewPortWidth = 800 }));
            Assert.AreEqual(BaseUrl + "cat.jpg", Source(html, new DefaultRenderDevice { ViewPortWidth = 500 }));
        }

        [Test]
        public void PictureSourceOfACombinedSrcsetIsTaken()
        {
            var html = @"<picture>
  <source srcset=""person@desktop.png, person@desktop-2x.png 2x"" media=""(min-width: 990px)"">
  <source srcset=""person@tablet.png, person@tablet-2x.png 2x"" media=""(min-width: 750px)"">
  <img srcset=""person@mobile.png, person@mobile-2x.png 2x"" alt=""person"">
</picture>";
            Assert.AreEqual(BaseUrl + "person@desktop.png", Source(html));
            Assert.AreEqual(BaseUrl + "person@mobile-2x.png", Source(html, new DefaultRenderDevice { ViewPortWidth = 500, Resolution = 192 }));
        }

        [Test]
        public void PictureSourceOfAnUnsupportedTypeIsSkipped()
        {
            var html = @"<picture>
  <source type=""image/svg+xml"" srcset=""logo.xml"">
  <source type=""image/webp"" srcset=""logo.webp"">
  <img src=""logo.png"" alt=""ACME Corp"">
</picture>";
            Assert.AreEqual(BaseUrl + "logo.png", Source(html));

            Assert.AreEqual(BaseUrl + "logo.webp", Source(html, config: Configuration.Default.With(new WebpImages())));
        }

        [TestCase("<img src=cat.jpg>", 1.0)]
        [TestCase("<img src=cat.jpg srcset=\"cat-2x.jpg 2x\">", 1.0)]
        [TestCase("<img srcset=\"cat-2x.jpg 2x\">", 2.0)]
        [TestCase("<img srcset=\"cat-1.5x.jpg 1.5x, cat-3x.jpg 3x\">", 1.5)]
        [TestCase("<img srcset=\"cat-400.jpg 400w, cat-800.jpg 800w\" sizes=\"300px\">", 400.0 / 300.0)]
        [TestCase("<img srcset=\"cat-200.jpg 200w\" sizes=\"400px\">", 0.5)]
        public void SourceKnowsTheDensityOfItsPixels(String html, Double expected)
        {
            Assert.AreEqual(expected, Construct(html).GetImageSource().Density, 0.0001);
        }

        [Test]
        public void RenderTreePicksTheImageForTheViewport()
        {
            var device = new DefaultRenderDevice { ViewPortWidth = 1280, ViewPortHeight = 800 };
            var img = Construct("<img srcset=\"cat-200.jpg 200w, cat-400.jpg 400w, cat-800.jpg 800w\" sizes=\"25vw\">", device);
            var window = img.Owner.DefaultView;

            Assert.AreEqual(BaseUrl + "cat-400.jpg", FindNode(window.Render(), img).ImageSource?.Url.Href);

            // a tree built after the viewport was resized picks again
            device.ViewPortWidth = 3000;
            Assert.AreEqual(BaseUrl + "cat-800.jpg", FindNode(window.Render(), img).ImageSource?.Url.Href);
            Assert.IsNull(FindNode(window.Render(), img.Owner.Body).ImageSource);
        }

        private sealed class WebpImages : IResourceService<IImageInfo>
        {
            public Boolean SupportsType(String mimeType) => mimeType == "image/webp";

            public System.Threading.Tasks.Task<IImageInfo> CreateAsync(AngleSharp.Io.IResponse response, System.Threading.CancellationToken cancel) =>
                System.Threading.Tasks.Task.FromResult<IImageInfo>(null);
        }

        private static ElementRenderNode FindNode(IRenderNode node, IElement element) =>
            node is ElementRenderNode e && ReferenceEquals(e.Ref, element)
                ? e
                : node.Children.Select(child => FindNode(child, element)).FirstOrDefault(found => found is not null);
    }
}
