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
            // 60, not 50: the UA stylesheet now also carries the rules for elements that are
            // never rendered (`[hidden]`, `input[type=hidden]`) and the default look of form
            // controls (text fields, buttons, checkboxes/radios, color and range inputs).
            // 70, not 60: the UA stylesheet now also follows the HTML Standard's rendering
            // section for bidi isolation, `summary`, `fieldset`, `mark`, `abbr[title]`, links,
            // and the block margins / list padding.
            // 72, not 70: the UA stylesheet now also generates quotes for `q::before` and
            // `q::after`.
            // 73, not 72: the UA stylesheet now also gives `::placeholder` its default look.
            Assert.AreEqual(73, sheet.Rules.Length);
        }
    }
}
