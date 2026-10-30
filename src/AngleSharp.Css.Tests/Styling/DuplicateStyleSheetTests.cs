#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Pages like dev.to link their style sheets twice. The cascade leaves out the earlier copy of
    /// a sheet linked again, which the later copy overrides anyway, instead of matching every
    /// element against both.
    /// </summary>
    [TestFixture]
    public class DuplicateStyleSheetTests
    {
        private static Task<IDocument> LoadAsync(string body)
        {
            var requester = new MockRequester();
            requester.BuildResponse(request =>
            {
                if (request.Address.Path.EndsWith("a.css"))
                {
                    return "p { color: red } .x { color: blue }";
                }
                else if (request.Address.Path.EndsWith("b.css"))
                {
                    return "p { color: green } p.x { color: green }";
                }
                else if (request.Address.Path.EndsWith("layers.css"))
                {
                    return "@layer one { p { color: red } }";
                }

                return null;
            });
            var config = Configuration.Default.WithCss().WithMockRequester(requester);
            return BrowsingContext.New(config).OpenAsync(m => m.Content("<!doctype html>" + body).Address("http://localhost/"));
        }

        private static string ColorOf(IDocument document, string id) =>
            document.DefaultView.GetComputedStyle(document.GetElementById(id)).GetColor();

        [Test]
        public async Task SheetLinkedAgainOverridesTheSheetsInBetween()
        {
            var document = await LoadAsync("<link rel=stylesheet href=a.css><link rel=stylesheet href=b.css><link rel=stylesheet href=a.css><p id=t>A</p><p id=u class=x>B</p>");

            Assert.AreEqual("rgba(255, 0, 0, 1)", ColorOf(document, "t"));
            // a higher specificity in between still wins
            Assert.AreEqual("rgba(0, 128, 0, 1)", ColorOf(document, "u"));
        }

        [Test]
        public async Task SheetLinkedTwiceIsMatchedOnce()
        {
            var document = await LoadAsync("<link rel=stylesheet href=a.css><link rel=stylesheet href=b.css><link rel=stylesheet href=a.css><link rel=stylesheet href=b.css><p id=t>A</p>");
            var rules = document.DefaultView.GetStyleCollection(new DefaultRenderDevice()).OfType<ICssStyleRule>().Count(m => m.Owner?.Href is not null);

            Assert.AreEqual(4, rules);
            Assert.AreEqual("rgba(0, 128, 0, 1)", ColorOf(document, "t"));
        }

        [Test]
        public async Task SheetWithLayersLinkedTwiceIsKept()
        {
            var document = await LoadAsync("<link rel=stylesheet href=layers.css><link rel=stylesheet href=layers.css><p id=t>A</p>");
            var rules = document.DefaultView.GetStyleCollection(new DefaultRenderDevice()).OfType<ICssStyleRule>().Count(m => m.Owner?.Href is not null);

            Assert.AreEqual(2, rules);
            Assert.AreEqual("rgba(255, 0, 0, 1)", ColorOf(document, "t"));
        }
    }
}
