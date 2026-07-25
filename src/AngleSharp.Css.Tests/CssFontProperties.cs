namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssFontProperties
    {
        [Test]
        public void FontDisplayAuto()
        {
            var property = ParseDeclaration("font-display: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-display"));
        }

        [Test]
        public void FontDisplayBlock()
        {
            var property = ParseDeclaration("font-display: block");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontDisplaySwap()
        {
            var property = ParseDeclaration("font-display: swap");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontDisplayFallback()
        {
            var property = ParseDeclaration("font-display: fallback");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontDisplayOptional()
        {
            var property = ParseDeclaration("font-display: optional");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontDisplayInvalid()
        {
            var property = ParseDeclaration("font-display: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void FontKerningAuto()
        {
            var property = ParseDeclaration("font-kerning: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-kerning"));
        }

        [Test]
        public void FontKerningNormal()
        {
            var property = ParseDeclaration("font-kerning: normal");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontKerningNone()
        {
            var property = ParseDeclaration("font-kerning: none");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontLanguageOverrideNormal()
        {
            var property = ParseDeclaration("font-language-override: normal");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-language-override"));
        }

        [Test]
        public void FontOpticalSizingAuto()
        {
            var property = ParseDeclaration("font-optical-sizing: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-optical-sizing"));
        }

        [Test]
        public void FontOpticalSizingNone()
        {
            var property = ParseDeclaration("font-optical-sizing: none");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontPaletteNormal()
        {
            var property = ParseDeclaration("font-palette: normal");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-palette"));
        }

        [Test]
        public void FontPaletteIdent()
        {
            var property = ParseDeclaration("font-palette: custom-palette");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontSynthesisNone()
        {
            var property = ParseDeclaration("font-synthesis: none");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-synthesis"));
        }

        [Test]
        public void FontSynthesisWeight()
        {
            var property = ParseDeclaration("font-synthesis: weight");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontSynthesisStyle()
        {
            var property = ParseDeclaration("font-synthesis: style");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontSynthesisSmallCaps()
        {
            var property = ParseDeclaration("font-synthesis: small-caps");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontSynthesisMultiple()
        {
            var property = ParseDeclaration("font-synthesis: weight style small-caps");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontSynthesisWeightAuto()
        {
            var property = ParseDeclaration("font-synthesis-weight: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-synthesis-weight"));
        }

        [Test]
        public void FontSynthesisWeightNone()
        {
            var property = ParseDeclaration("font-synthesis-weight: none");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontSynthesisStyleAuto()
        {
            var property = ParseDeclaration("font-synthesis-style: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-synthesis-style"));
        }

        [Test]
        public void FontSynthesisStyleNone()
        {
            var property = ParseDeclaration("font-synthesis-style: none");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontSynthesisSmallCapsAuto()
        {
            var property = ParseDeclaration("font-synthesis-small-caps: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-synthesis-small-caps"));
        }

        [Test]
        public void FontSynthesisSmallCapsNone()
        {
            var property = ParseDeclaration("font-synthesis-small-caps: none");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void FontVariationSettingsNormal()
        {
            var property = ParseDeclaration("font-variation-settings: normal");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("font-variation-settings"));
        }
    }
}
