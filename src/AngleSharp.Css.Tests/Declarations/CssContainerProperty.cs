namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssContainerPropertyTests
    {
        [Test]
        public void CssContainerTypeInlineSizeLegal()
        {
            var property = ParseDeclaration("container-type: inline-size");
            Assert.AreEqual("container-type", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("inline-size", property.Value);
        }

        [Test]
        public void CssContainerTypeInvalidKeywordIllegal()
        {
            var property = ParseDeclaration("container-type: block-size");
            Assert.AreEqual("container-type", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssContainerNameSingleLegal()
        {
            var property = ParseDeclaration("container-name: sidebar");
            Assert.AreEqual("container-name", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("sidebar", property.Value);
        }

        [Test]
        public void CssContainerNameMultipleLegal()
        {
            var property = ParseDeclaration("container-name: sidebar card");
            Assert.AreEqual("container-name", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("sidebar card", property.Value);
        }

        [Test]
        public void CssContainerShorthandNameOnlyLegal()
        {
            var property = ParseDeclaration("container: sidebar");
            Assert.AreEqual("container", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("sidebar", property.Value);
        }

        [Test]
        public void CssContainerShorthandNameAndTypeLegal()
        {
            var property = ParseDeclaration("container: sidebar / inline-size");
            Assert.AreEqual("container", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("sidebar / inline-size", property.Value);
        }

        [Test]
        public void CssContainerShorthandNoneAndTypeLegal()
        {
            var property = ParseDeclaration("container: none / size");
            Assert.AreEqual("container", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none / size", property.Value);
        }
    }
}
