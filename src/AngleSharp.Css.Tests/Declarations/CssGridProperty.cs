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

        [Test]
        public void CssGridAutoColumnsMinContentLegal()
        {
            var snippet = @"grid-auto-columns: min-content";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("min-content", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsMaxContentLegal()
        {
            var snippet = @"grid-auto-columns: max-content";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("max-content", property.Value);
        }

        [Test]
        public void CssGridAutoRowsAutoUppercaseLegal()
        {
            var snippet = @"grid-auto-rows: AUTO";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsLengthInPxLegal()
        {
            var snippet = @"grid-auto-columns: 100px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsLengthInCmLegal()
        {
            var snippet = @"  grid-auto-columns  : 20cm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("20cm", property.Value);
        }

        [Test]
        public void CssGridAutoRowsLengthInVmaxLegal()
        {
            var snippet = @"grid-auto-rows: 50vmax";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("50vmax", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsInPercentLegal()
        {
            var snippet = @"grid-auto-columns: 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10%", property.Value);
        }

        [Test]
        public void CssGridAutoRowsInPercentLegal()
        {
            var snippet = @"grid-auto-rows: 33.3%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("33.3%", property.Value);
        }

        [Test]
        public void CssGridAutoRowsFractionLegal()
        {
            var snippet = @"grid-auto-rows: 0.5fr";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.5fr", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsFractionLegal()
        {
            var snippet = @"grid-auto-columns: 3fr;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3fr", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsMinmaxLegal()
        {
            var snippet = @"grid-auto-columns: minmax(100px, auto)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("minmax(100px, auto)", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsMinmaxWithMaxContentLegal()
        {
            var snippet = @"grid-auto-columns: minmax(max-content, 2fr)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("minmax(max-content, 2fr)", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsMinmaxPercentLegal()
        {
            var snippet = @"grid-auto-columns: minmax(20%, 80vmax)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("minmax(20%, 80vmax)", property.Value);
        }

        [Test]
        public void CssGridAutoRowsFitContentLegal()
        {
            var snippet = @"grid-auto-rows: fit-content(400px)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("fit-content(400px)", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsFitContentLegal()
        {
            var snippet = @"grid-auto-columns: fit-content(5cm)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("fit-content(5cm)", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsFitContentPercentLegal()
        {
            var snippet = @"grid-auto-columns: fit-content(20%)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("fit-content(20%)", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsMinContentMaxContentAndAutoLegal()
        {
            var snippet = @"grid-auto-columns: min-content max-content auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("min-content max-content auto", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsLengthLengthAndLengthLegal()
        {
            var snippet = @"grid-auto-columns: 100px 150px 390px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px 150px 390px", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsLengthMinmaxPercentFractionAndFitContentLegal()
        {
            var snippet = @"grid-auto-columns: 100px minmax(100px, auto) 10% 0.5fr fit-content(400px)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px minmax(100px, auto) 10% 0.5fr fit-content(400px)", property.Value);
        }

        [Test]
        public void CssGridAutoRowsPercentAndPercentLegal()
        {
            var snippet = @"grid-auto-rows: 10% 33.3%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10% 33.3%", property.Value);
        }

        [Test]
        public void CssGridAutoColumnsFractionFractionAndFractionLegal()
        {
            var snippet = @"grid-auto-columns: 0.5fr 3fr 1fr";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-columns", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.5fr 3fr 1fr", property.Value);
        }

        [Test]
        public void CssGridAutoRowsMinmaxMinmaxAndMinmaxLegal()
        {
            var snippet = @"grid-auto-rows: minmax(100px, auto) minmax(max-content, 2fr) minmax(20%, 80vmax)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-auto-rows", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("minmax(100px, auto) minmax(max-content, 2fr) minmax(20%, 80vmax)", property.Value);
        }

        [Test]
        public void CssGridRowEndNumberNameAndSpanLegal()
        {
            var snippet = @"grid-row-end: 5 somegridarea span";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-row-end", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("span 5 somegridarea", property.Value);
        }

        [Test]
        public void CssGridColumnStartSpanNameLegal()
        {
            var snippet = @"grid-column-start: span somegridarea";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-column-start", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("span somegridarea", property.Value);
        }

        [Test]
        public void CssGridRowStartSpanNumberLegal()
        {
            var snippet = @"grid-row-start: span 3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-row-start", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("span 3", property.Value);
        }

        [Test]
        public void CssGridColumnEndNameAndNumberLegal()
        {
            var snippet = @"grid-column-end: somegridarea 4";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-column-end", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("4 somegridarea", property.Value);
        }

        [Test]
        public void CssGridColumnStartNumberLegal()
        {
            var snippet = @"grid-column-start: 2";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-column-start", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2", property.Value);
        }

        [Test]
        public void CssGridRowEndNameLegal()
        {
            var snippet = @"grid-row-end: somegridarea";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-row-end", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("somegridarea", property.Value);
        }

        [Test]
        public void CssGridRowStartAutoLegal()
        {
            var snippet = @"grid-row-start: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-row-start", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssGridAreaFourValuesLegal()
        {
            var snippet = @"grid-area: 2 / 2 / auto / span 3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-area", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2 / 2 / auto / span 3", property.Value);
        }

        [Test]
        public void CssGridAreaThreeValuesLegal()
        {
            var snippet = @"grid-area: 2 / foobar / auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-area", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2 / foobar / auto / foobar", property.Value);
        }

        [Test]
        public void CssGridAreaSingleValueLegal()
        {
            var snippet = @"grid-area: 2";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-area", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2 / auto / auto / auto", property.Value);
        }

        [Test]
        public void CssGridAutoFlowAndRepeatLegal()
        {
            var snippet = @"grid: auto-flow 300px / repeat(3, [line1 line2 line3] 200px)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto-flow 300px / repeat(3, [line1 line2 line3] 200px)", property.Value);
        }

        [Test]
        public void CssGridAutoFlowDenseLegal()
        {
            var snippet = @"grid: auto-flow dense / 30%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto-flow dense / 30%", property.Value);
        }

        [Test]
        public void CssGridAutoFlowLineNameLegal()
        {
            var snippet = @"grid: auto-flow dense 40% / [line1] minmax(20em, max-content)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto-flow dense 40% / [line1] minmax(20em, max-content)", property.Value);
        }

        [Test]
        public void CssGridAutoFlowLengthLegal()
        {
            var snippet = @"grid: auto-flow / 200px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto-flow / 200px", property.Value);
        }

        [Test]
        public void CssGridRepeatAutoFlowLegal()
        {
            var snippet = @"grid: repeat(3, [line1 line2 line3] 200px) / auto-flow 300px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("repeat(3, [line1 line2 line3] 200px) / auto-flow 300px", property.Value);
        }

        [Test]
        public void CssGridPercentDenseAutoFlowLegal()
        {
            var snippet = @"grid: 30% / auto-flow dense";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("30% / auto-flow dense", property.Value);
        }

        [Test]
        public void CssGridMinmaxAndRepeatLegal()
        {
            var snippet = @"grid: minmax(400px, min-content) / repeat(auto-fill, 50px)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("minmax(400px, min-content) / repeat(auto-fill, 50px)", property.Value);
        }

        [Test]
        public void CssGridMinmaxAndAutoFlowDenseLegal()
        {
            var snippet = @"grid: [line1] minmax(20em, max-content) / auto-flow dense 40%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[line1] minmax(20em, max-content) / auto-flow dense 40%", property.Value);
        }

        [Test]
        public void CssGridLengthAndAutoFlowLegal()
        {
            var snippet = @"grid: 200px / auto-flow";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("200px / auto-flow", property.Value);
        }

        [Test]
        public void CssGridLengthAndLengthLegal()
        {
            var snippet = @"grid: 100px / 200px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px / 200px", property.Value);
        }

        [Test]
        public void CssGridStringMinmaxAndStringLegal()
        {
            var snippet = @"grid: ""a"" minmax(100px, max-content) ""b"" 20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"a\" minmax(100px, max-content) \"b\" 20%", property.Value);
        }

        [Test]
        public void CssGridStringLengthAndStringLegal()
        {
            var snippet = @"grid: ""a"" 200px ""b"" min-content";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"a\" 200px \"b\" min-content", property.Value);
        }

        [Test]
        public void CssGridLineNameAndStringLengthLegal()
        {
            var snippet = @"grid: [linename1] ""a"" 100px [linename2]";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[linename1] \"a\" 100px [linename2]", property.Value);
        }

        [Test]
        public void CssGridStringAndLengthAndStringLegal()
        {
            var snippet = @"grid: ""a"" 100px ""b"" 1fr";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"a\" 100px \"b\" 1fr", property.Value);
        }

        [Test]
        public void CssGridNoneLegal()
        {
            var snippet = @"grid: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssGridTemplateNoneLegal()
        {
            var snippet = @"grid-template: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssGridTemplateLineNamesAndStringWithFractionsLegal()
        {
            var snippet = @"grid-template: [header-top] ""a a a""     [header-bottom]
                 [main-top] ""b b b"" 1fr [main-bottom] / auto 1fr auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[header-top] \"a a a\" [header-bottom] [main-top] \"b b b\" 1fr [main-bottom] / auto 1fr auto", property.Value);
        }

        [Test]
        public void CssGridTemplateStirngsAndWidthsLegal()
        {
            var snippet = @"grid-template: ""a a a"" 20%
               ""b b b"" auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"a a a\" 20% \"b b b\" auto", property.Value);
        }

        [Test]
        public void CssGridTemplateStringsLegal()
        {
            var snippet = @"grid-template: ""a a a""
               ""b b b""";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"a a a\" \"b b b\"", property.Value);
        }

        [Test]
        public void CssGridTemplateFitContentColumnsAndRowsLegal()
        {
            var snippet = @"grid-template: fit-content(100px) / fit-content(40%)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("fit-content(100px) / fit-content(40%)", property.Value);
        }

        [Test]
        public void CssGridTemplateLineNamesAndPercentagesLegal()
        {
            var snippet = @"grid-template: [linename] 100px / [columnname1] 30% [columnname2] 70%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("[linename] 100px / [columnname1] 30% [columnname2] 70%", property.Value);
        }

        [Test]
        public void CssGridTemplateRowsAndColumnsWithAutoLegal()
        {
            var snippet = @"grid-template: auto 1fr / auto 1fr auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto 1fr / auto 1fr auto", property.Value);
        }

        [Test]
        public void CssGridTemplateRowsAndColumnsWithFractionsLegal()
        {
            var snippet = @"grid-template: 100px 1fr / 50px 1fr";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("grid-template", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px 1fr / 50px 1fr", property.Value);
        }
    }
}
