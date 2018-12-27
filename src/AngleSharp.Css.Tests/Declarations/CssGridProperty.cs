namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssGridPropertyTests
    {
        [Test]
        public void CssGridAutoFlowOnlyDenseLegal()
        {
            var snippet = "grid-auto-flow : dense";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-flow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("dense", property.Value);
        }

        [Test]
        public void CssGridAutoFlowOnlyRowLegal()
        {
            var snippet = "grid-auto-flow : row";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-flow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("row", property.Value);
        }
        [Test]
        public void CssGridAutoFlowOnlyColumnLegal()
        {
            var snippet = "grid-auto-flow : column";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-flow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("column", property.Value);
        }
        [Test]
        public void CssGridAutoFlowColumnDenseLegal()
        {
            var snippet = "grid-auto-flow : column dense";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-flow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("column dense", property.Value);
        }
        [Test]
        public void CssGridAutoFlowRowDenseLegal()
        {
            var snippet = "grid-auto-flow : row dense";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-flow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("row dense", property.Value);
        }
        [Test]
        public void CssGridAutoFlowDoubleDenseIllegal()
        {
            var snippet = "grid-auto-flow : dense dense";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-flow", property.Name);
            Assert.IsFalse(property.HasValue);
        }
    }
}
