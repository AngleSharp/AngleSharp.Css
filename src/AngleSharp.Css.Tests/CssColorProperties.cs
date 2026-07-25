namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssColorProperties
    {
        [Test]
        public void AccentColorAuto()
        {
            var property = ParseDeclaration("accent-color: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("accent-color"));
        }

        [Test]
        public void AccentColorRed()
        {
            var property = ParseDeclaration("accent-color: red");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AccentColorHex()
        {
            var property = ParseDeclaration("accent-color: #ff0000");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void AccentColorInvalid()
        {
            var property = ParseDeclaration("accent-color: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CaretColorAuto()
        {
            var property = ParseDeclaration("caret-color: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("caret-color"));
        }

        [Test]
        public void CaretColorBlue()
        {
            var property = ParseDeclaration("caret-color: blue");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CaretColorRgb()
        {
            var property = ParseDeclaration("caret-color: rgb(100, 150, 200)");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ColorSchemeNormal()
        {
            var property = ParseDeclaration("color-scheme: normal");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("color-scheme"));
        }

        [Test]
        public void ColorSchemeLight()
        {
            var property = ParseDeclaration("color-scheme: light");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ColorSchemeDark()
        {
            var property = ParseDeclaration("color-scheme: dark");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ColorSchemeLightDark()
        {
            var property = ParseDeclaration("color-scheme: light dark");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ColorSchemeDarkLight()
        {
            var property = ParseDeclaration("color-scheme: dark light");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ColorSchemeMultiple()
        {
            var property = ParseDeclaration("color-scheme: light dark light");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ForcedColorAdjustAuto()
        {
            var property = ParseDeclaration("forced-color-adjust: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("forced-color-adjust"));
        }

        [Test]
        public void ForcedColorAdjustNone()
        {
            var property = ParseDeclaration("forced-color-adjust: none");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void ForcedColorAdjustInvalid()
        {
            var property = ParseDeclaration("forced-color-adjust: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void PrintColorAdjustAuto()
        {
            var property = ParseDeclaration("print-color-adjust: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("print-color-adjust"));
        }

        [Test]
        public void PrintColorAdjustEconomy()
        {
            var property = ParseDeclaration("print-color-adjust: economy");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PrintColorAdjustExact()
        {
            var property = ParseDeclaration("print-color-adjust: exact");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PrintColorAdjustInvalid()
        {
            var property = ParseDeclaration("print-color-adjust: invalid");
            Assert.That(property.HasValue, Is.False);
        }
    }
}
