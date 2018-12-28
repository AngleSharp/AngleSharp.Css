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

        [Test]
        public void CssGridTemplateRowsLengthFlexLegal()
        {
            var snippet = "grid-template-rows: 100px 1fr";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px 1fr", property.Value);
        }

        [Test]
        public void CssGridTemplateRowsLinenameLengthLegal()
        {
            var snippet = "grid-template-rows: [linename] 100px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[linename] 100px", property.Value);
        }

        [Test]
        public void CssGridTemplateRowsLengthFlexMoreLineNamesLegal()
        {
            var snippet = "grid-template-rows: [linename1] 100px [linename2 linename3]";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[linename1] 100px [linename2 linename3]", property.Value);
        }

        [Test]
        public void CssGridTemplateRowsFitContentPercentLegal()
        {
            var snippet = "grid-template-rows: fit-content(40%)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("fit-content(40%)", property.Value);
        }

        [Test]
        public void CssGridTemplateRowsRepeatLegal()
        {
            var snippet = "grid-template-rows: repeat(3, 200px)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("repeat(3, 200px)", property.Value);
        }

        [Test]
        public void CssGridTemplateRowsMinmaxRepeatPercentInAutoTrackListLegal()
        {
            var snippet = "grid-template-rows: minmax(100px, max-content) repeat(auto-fill, 200px) 20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("minmax(100px, max-content) repeat(auto-fill, 200px) 20%", property.Value);
        }

        [Test]
        public void CssGridTemplateColumnMinmaxLegal()
        {
            var snippet = "grid-template-columns: minmax(100px, 1fr)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("minmax(100px, 1fr)", property.Value);
        }

        [Test]
        public void CssGridTemplateColumnsLengthAutoRepeatLengthLegal()
        {
            var snippet = "grid-template-columns: 200px repeat(auto-fill, 100px) 300px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("200px repeat(auto-fill, 100px) 300px", property.Value);
        }

        [Test]
        public void CssGridTemplateColumnsMultilineLinenamesAutoRepeatLegal()
        {
            var snippet = "grid-template-columns: [linename1] 100px [linename2]\n repeat(auto-fit, [linename3 linename4] 300px)\n 100px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[linename1] 100px [linename2] repeat(auto-fit, [linename3 linename4] 300px) 100px", property.Value);
        }

        [Test]
        public void CssGridTemplateColumnsMultilineLinenamesAutoFitLengthLegal()
        {
            var snippet = @"grid-template-columns: [linename1 linename2] 100px
                       repeat(auto-fit, [linename1] 300px) [linename3]";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[linename1 linename2] 100px repeat(auto-fit, [linename1] 300px) [linename3]", property.Value);
        }

        [Test]
        public void CssGridTemplateAreasNoneLegal()
        {
            var snippet = @"grid-template-areas: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-areas", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssGridTemplateAreasSingleStringLegal()
        {
            var snippet = @"grid-template-areas: ""a b""";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-areas", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"a b\"", property.Value);
        }

        [Test]
        public void CssGridTemplateAreasMultilineMultipleLegal()
        {
            var snippet = @"grid-template-areas: ""a b b""
                     ""a c d""";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template-areas", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"a b b\" \"a c d\"", property.Value);
        }
    }
}
