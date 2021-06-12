namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssWidthPropertyTests
    {
        [Test]
        public void CssWidthPropertyAutoLegal()
        {
            var snippet = "width: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }
        
        [Test]
        public void CssWidthPropertyFitContentLegal()
        {
            var snippet = "width: fit-content";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("fit-content", property.Value);
        }

        [Test]
        public void CssWidthPropertyValueInPxLegal()
        {
            var snippet = "width: 42px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("42px", property.Value);
        }
    }
}
