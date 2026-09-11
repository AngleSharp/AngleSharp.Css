namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void HasStyleEngine()
        {
            var config = new Configuration().WithCss();
            var context = BrowsingContext.New(config);
            var service = context.GetService<IStylingService>();
            Assert.IsNotNull(service);
            Assert.IsInstanceOf<CssStylingService>(service);
        }

        [Test]
        public void ConfigurationWithExtensionLeavesOriginallyUnmodified()
        {
            var original = Configuration.Default;
            var modified = original.WithCss();
            Assert.AreNotSame(original, modified);
            Assert.AreNotEqual(original.Services.Count(), modified.Services.Count());
        }

        [Test]
        public void ObtainDefaultSheet()
        {
            var service = new CssDefaultStyleSheetProvider();
            Assert.IsNotNull(service.Default);
            var sheet = service.Default;
            Assert.IsNotNull(sheet);
            // 50, not 49: the UA stylesheet now also carries a `*:before, *:after { display: inline }`
            // rule, giving generated-content pseudo-elements their spec-correct default display
            // (previously they had none at all, so this renderer's general "unset display defaults
            // to block" fallback made every ::before/::after start its own new block line).
            Assert.AreEqual(50, sheet.Rules.Length);
        }
    }
}
