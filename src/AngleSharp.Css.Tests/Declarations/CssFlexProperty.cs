namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssFlexPropertyTests
    {
        [Test]
        public void CssFlexShrinkPositiveNumberLegal()
        {
            var snippet = "flex-shrink : 5";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-shrink"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5"));
        }

        [Test]
        public void CssFlexShrinkNegativeNumberLegal()
        {
            var snippet = "flex-shrink : -1";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-shrink"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("-1"));
        }

        [Test]
        public void CssFlexShrinkNoneIllegal()
        {
            var snippet = "flex-shrink : none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-shrink"));
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFlexGrowPositiveNumberLegal()
        {
            var snippet = "flex-grow : 7";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-grow"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("7"));
        }

        [Test]
        public void CssFlexBasisLengthLegal()
        {
            var snippet = "flex-basis:100px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-basis"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("100px"));
        }

        [Test]
        public void CssFlexShorthandLegal()
        {
            var snippet = "flex: 1 0 100%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1 0 100%"));
        }

        [Test]
        public void CssFlexShorthandOnlyBasisLegal()
        {
            var snippet = "flex: 10em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10em"));
        }

        [Test]
        public void CssFlexShorthandGrowAndBasisLegal()
        {
            var snippet = "flex: 1 10em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1 10em"));
        }

        [Test]
        public void CssFlexShorthandGrowAndShrinkLegal()
        {
            var snippet = "flex: 1 2";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1 2"));
        }

        [Test]
        public void CssFlexWrapLegal()
        {
            var snippet = "flex-wrap: wrap-reverse";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-wrap"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("wrap-reverse"));
        }

        [Test]
        public void CssFlexWrapIllegal()
        {
            var snippet = "flex-wrap: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-wrap"));
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFleDirectionLegal()
        {
            var snippet = "flex-direction: column-REVERSE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-direction"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("column-reverse"));
        }

        [Test]
        public void CssFlexDirectionIllegal()
        {
            var snippet = "flex-direction: inverse-row";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("flex-direction"));
            Assert.That(property.HasValue, Is.False);
        }
    }
}
