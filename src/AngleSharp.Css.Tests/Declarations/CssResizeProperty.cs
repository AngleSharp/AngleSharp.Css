namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssResizePropertyTests
    {
        [Test]
        public void CssResizeNoneLegal()
        {
            var snippet = "resize: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("resize", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssResizeScaledownIllegal()
        {
            var snippet = "resize: scaledown";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("resize", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssResizeBothLegal()
        {
            var snippet = "resize : both";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("resize", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("both", property.Value);
        }

        [Test]
        public void CssResizeHorizontalLegal()
        {
            var snippet = "resize : horizontal";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("resize", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("horizontal", property.Value);
        }

        [Test]
        public void CssResizeVerticalLegal()
        {
            var snippet = "resize : vertical";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("resize", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("vertical", property.Value);
        }
    }
}
