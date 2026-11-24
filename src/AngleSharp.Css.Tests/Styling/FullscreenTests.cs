#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The CSS part of the Fullscreen API: the `:fullscreen` pseudo-class, which the host forces
    /// on the element it shows fullscreen, and the user-agent styles making it cover the viewport.
    /// </summary>
    [TestFixture]
    public class FullscreenTests
    {
        [Test]
        public void FullscreenDoesNotMatchByItself()
        {
            var document = ParseDocument("<div id=video>x</div>");
            var video = document.GetElementById("video");

            Assert.IsFalse(video.Matches(":fullscreen"));
            Assert.IsTrue(video.Matches(":not(:fullscreen)"));
        }

        [Test]
        public void FullscreenMatchesTheElementTheHostShowsFullscreen()
        {
            var document = ParseDocument("<div id=video>x</div>");
            var video = document.GetElementById("video");

            video.SetPseudoClass("fullscreen");
            Assert.IsTrue(video.Matches(":fullscreen"));

            video.RemovePseudoClass("fullscreen");
            Assert.IsFalse(video.Matches(":fullscreen"));
        }

        [Test]
        public void FullscreenElementCoversTheViewport()
        {
            var document = ParseDocument("<html><head><style>#video { position: absolute; width: 100px; margin: 20px }</style></head><body><div id=video>x</div></body></html>");
            var video = document.GetElementById("video");

            video.SetPseudoClass("fullscreen");
            var style = document.DefaultView.GetComputedStyle(video);

            Assert.AreEqual("fixed", style.GetPosition());
            Assert.AreEqual("100%", style.GetWidth());
            Assert.AreEqual("0px", style.GetPropertyValue("margin-top"));
        }

        [Test]
        public void AppendedDefaultRulesAreKept()
        {
            var provider = new CssDefaultStyleSheetProvider();
            provider.AppendDefault("x-first { color: red }");
            provider.AppendDefault("x-second { color: blue }");
            var css = provider.Default.ToCss();

            StringAssert.Contains("x-first", css);
            StringAssert.Contains("x-second", css);
        }
    }
}
