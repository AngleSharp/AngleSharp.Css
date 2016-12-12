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
            var service = new CssStylingService();
            Assert.IsNotNull(service.Default);
            Assert.IsTrue((service as IStylingService).SupportsType(MimeTypeNames.Css));
            var sheet = service.Default as ICssStyleSheet;
            Assert.IsNotNull(sheet);
            Assert.AreEqual(49, sheet.Rules.Length);
        }
    }
}
