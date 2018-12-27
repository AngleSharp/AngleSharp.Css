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
            Assert.AreEqual("flex-shrink", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5", property.Value);
        }

        [Test]
        public void CssFlexShrinkNegativeNumberLegal()
        {
            var snippet = "flex-shrink : -1";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-shrink", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("-1", property.Value);
        }

        [Test]
        public void CssFlexShrinkNoneIllegal()
        {
            var snippet = "flex-shrink : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-shrink", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFlexGrowPositiveNumberLegal()
        {
            var snippet = "flex-grow : 7";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-grow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("7", property.Value);
        }

        [Test]
        public void CssFlexBasisLengthLegal()
        {
            var snippet = "flex-basis:100px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-basis", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px", property.Value);
        }

        [Test]
        public void CssFlexShorthandLegal()
        {
            var snippet = "flex: 1 0 100%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1 0 100%", property.Value);
        }

        [Test]
        public void CssFlexShorthandOnlyBasisLegal()
        {
            var snippet = "flex: 10em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10em", property.Value);
        }

        [Test]
        public void CssFlexShorthandGrowAndBasisLegal()
        {
            var snippet = "flex: 1 10em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1 10em", property.Value);
        }

        [Test]
        public void CssFlexShorthandGrowAndShrinkLegal()
        {
            var snippet = "flex: 1 2";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1 2", property.Value);
        }

        [Test]
        public void CssFlexWrapLegal()
        {
            var snippet = "flex-wrap: wrap-reverse";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-wrap", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("wrap-reverse", property.Value);
        }

        [Test]
        public void CssFlexWrapIllegal()
        {
            var snippet = "flex-wrap: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-wrap", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFleDirectionLegal()
        {
            var snippet = "flex-direction: column-REVERSE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-direction", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("column-reverse", property.Value);
        }

        [Test]
        public void CssFlexDirectionIllegal()
        {
            var snippet = "flex-direction: inverse-row";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("flex-direction", property.Name);
            Assert.IsFalse(property.HasValue);
        }
    }
}
