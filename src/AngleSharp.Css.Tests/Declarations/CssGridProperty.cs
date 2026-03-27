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
            Assert.That(property.Name, Is.EqualTo("grid-auto-flow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("dense"));
        }

        [Test]
        public void CssGridAutoFlowOnlyRowLegal()
        {
            var snippet = "grid-auto-flow : row";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-flow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("row"));
        }

        [Test]
        public void CssGridAutoFlowOnlyColumnLegal()
        {
            var snippet = "grid-auto-flow : column";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-flow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("column"));
        }

        [Test]
        public void CssGridAutoFlowColumnDenseLegal()
        {
            var snippet = "grid-auto-flow : column dense";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-flow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("column dense"));
        }

        [Test]
        public void CssGridAutoFlowRowDenseLegal()
        {
            var snippet = "grid-auto-flow : row dense";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-flow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("row dense"));
        }

        [Test]
        public void CssGridAutoFlowDoubleDenseIllegal()
        {
            var snippet = "grid-auto-flow : dense dense";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-flow"));
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssGridTemplateRowsLengthFlexLegal()
        {
            var snippet = "grid-template-rows: 100px 1fr";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("100px 1fr"));
        }

        [Test]
        public void CssGridTemplateRowsLinenameLengthLegal()
        {
            var snippet = "grid-template-rows: [linename] 100px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[linename] 100px"));
        }

        [Test]
        public void CssGridTemplateRowsLengthFlexMoreLineNamesLegal()
        {
            var snippet = "grid-template-rows: [linename1] 100px [linename2 linename3]";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[linename1] 100px [linename2 linename3]"));
        }

        [Test]
        public void CssGridTemplateRowsFitContentPercentLegal()
        {
            var snippet = "grid-template-rows: fit-content(40%)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fit-content(40%)"));
        }

        [Test]
        public void CssGridTemplateRowsRepeatLegal()
        {
            var snippet = "grid-template-rows: repeat(3, 200px)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat(3, 200px)"));
        }

        [Test]
        public void CssGridTemplateRowsMinmaxRepeatPercentInAutoTrackListLegal()
        {
            var snippet = "grid-template-rows: minmax(100px, max-content) repeat(auto-fill, 200px) 20%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("minmax(100px, max-content) repeat(auto-fill, 200px) 20%"));
        }

        [Test]
        public void CssGridTemplateColumnMinmaxLegal()
        {
            var snippet = "grid-template-columns: minmax(100px, 1fr)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("minmax(100px, 1fr)"));
        }

        [Test]
        public void CssGridTemplateColumnsLengthAutoRepeatLengthLegal()
        {
            var snippet = "grid-template-columns: 200px repeat(auto-fill, 100px) 300px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("200px repeat(auto-fill, 100px) 300px"));
        }

        [Test]
        public void CssGridTemplateColumnsMultilineLinenamesAutoRepeatLegal()
        {
            var snippet = "grid-template-columns: [linename1] 100px [linename2]\n repeat(auto-fit, [linename3 linename4] 300px)\n 100px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[linename1] 100px [linename2] repeat(auto-fit, [linename3 linename4] 300px) 100px"));
        }

        [Test]
        public void CssGridTemplateColumnsMultilineLinenamesAutoFitLengthLegal()
        {
            var snippet = @"grid-template-columns: [linename1 linename2] 100px
                       repeat(auto-fit, [linename1] 300px) [linename3]";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[linename1 linename2] 100px repeat(auto-fit, [linename1] 300px) [linename3]"));
        }

        [Test]
        public void CssGridTemplateAreasNoneLegal()
        {
            var snippet = @"grid-template-areas: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-areas"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssGridTemplateAreasSingleStringLegal()
        {
            var snippet = @"grid-template-areas: ""a b""";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-areas"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"a b\""));
        }

        [Test]
        public void CssGridTemplateAreasMultilineMultipleLegal()
        {
            var snippet = @"grid-template-areas: ""a b b""
                     ""a c d""";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template-areas"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"a b b\" \"a c d\""));
        }

        [Test]
        public void CssGridAutoColumnsMinContentLegal()
        {
            var snippet = @"grid-auto-columns: min-content";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("min-content"));
        }

        [Test]
        public void CssGridAutoColumnsMaxContentLegal()
        {
            var snippet = @"grid-auto-columns: max-content";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("max-content"));
        }

        [Test]
        public void CssGridAutoRowsAutoUppercaseLegal()
        {
            var snippet = @"grid-auto-rows: AUTO";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssGridAutoColumnsLengthInPxLegal()
        {
            var snippet = @"grid-auto-columns: 100px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("100px"));
        }

        [Test]
        public void CssGridAutoColumnsLengthInCmLegal()
        {
            var snippet = @"  grid-auto-columns  : 20cm";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("20cm"));
        }

        [Test]
        public void CssGridAutoRowsLengthInVmaxLegal()
        {
            var snippet = @"grid-auto-rows: 50vmax";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("50vmax"));
        }

        [Test]
        public void CssGridAutoColumnsInPercentLegal()
        {
            var snippet = @"grid-auto-columns: 10%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10%"));
        }

        [Test]
        public void CssGridAutoRowsInPercentLegal()
        {
            var snippet = @"grid-auto-rows: 33.3%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("33.3%"));
        }

        [Test]
        public void CssGridAutoRowsFractionLegal()
        {
            var snippet = @"grid-auto-rows: 0.5fr";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.5fr"));
        }

        [Test]
        public void CssGridAutoColumnsFractionLegal()
        {
            var snippet = @"grid-auto-columns: 3fr;";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3fr"));
        }

        [Test]
        public void CssGridAutoColumnsMinmaxLegal()
        {
            var snippet = @"grid-auto-columns: minmax(100px, auto)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("minmax(100px, auto)"));
        }

        [Test]
        public void CssGridAutoColumnsMinmaxWithMaxContentLegal()
        {
            var snippet = @"grid-auto-columns: minmax(max-content, 2fr)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("minmax(max-content, 2fr)"));
        }

        [Test]
        public void CssGridAutoColumnsMinmaxPercentLegal()
        {
            var snippet = @"grid-auto-columns: minmax(20%, 80vmax)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("minmax(20%, 80vmax)"));
        }

        [Test]
        public void CssGridAutoRowsFitContentLegal()
        {
            var snippet = @"grid-auto-rows: fit-content(400px)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fit-content(400px)"));
        }

        [Test]
        public void CssGridAutoColumnsFitContentLegal()
        {
            var snippet = @"grid-auto-columns: fit-content(5cm)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fit-content(5cm)"));
        }

        [Test]
        public void CssGridAutoColumnsFitContentPercentLegal()
        {
            var snippet = @"grid-auto-columns: fit-content(20%)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fit-content(20%)"));
        }

        [Test]
        public void CssGridAutoColumnsMinContentMaxContentAndAutoLegal()
        {
            var snippet = @"grid-auto-columns: min-content max-content auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("min-content max-content auto"));
        }

        [Test]
        public void CssGridAutoColumnsLengthLengthAndLengthLegal()
        {
            var snippet = @"grid-auto-columns: 100px 150px 390px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("100px 150px 390px"));
        }

        [Test]
        public void CssGridAutoColumnsLengthMinmaxPercentFractionAndFitContentLegal()
        {
            var snippet = @"grid-auto-columns: 100px minmax(100px, auto) 10% 0.5fr fit-content(400px)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("100px minmax(100px, auto) 10% 0.5fr fit-content(400px)"));
        }

        [Test]
        public void CssGridAutoRowsPercentAndPercentLegal()
        {
            var snippet = @"grid-auto-rows: 10% 33.3%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10% 33.3%"));
        }

        [Test]
        public void CssGridAutoColumnsFractionFractionAndFractionLegal()
        {
            var snippet = @"grid-auto-columns: 0.5fr 3fr 1fr";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-columns"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.5fr 3fr 1fr"));
        }

        [Test]
        public void CssGridAutoRowsMinmaxMinmaxAndMinmaxLegal()
        {
            var snippet = @"grid-auto-rows: minmax(100px, auto) minmax(max-content, 2fr) minmax(20%, 80vmax)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-auto-rows"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("minmax(100px, auto) minmax(max-content, 2fr) minmax(20%, 80vmax)"));
        }

        [Test]
        public void CssGridRowEndNumberNameAndSpanLegal()
        {
            var snippet = @"grid-row-end: 5 somegridarea span";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-row-end"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("span 5 somegridarea"));
        }

        [Test]
        public void CssGridColumnStartSpanNameLegal()
        {
            var snippet = @"grid-column-start: span somegridarea";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-column-start"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("span somegridarea"));
        }

        [Test]
        public void CssGridRowStartSpanNumberLegal()
        {
            var snippet = @"grid-row-start: span 3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-row-start"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("span 3"));
        }

        [Test]
        public void CssGridColumnEndNameAndNumberLegal()
        {
            var snippet = @"grid-column-end: somegridarea 4";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-column-end"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("4 somegridarea"));
        }

        [Test]
        public void CssGridColumnStartNumberLegal()
        {
            var snippet = @"grid-column-start: 2";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-column-start"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2"));
        }

        [Test]
        public void CssGridRowEndNameLegal()
        {
            var snippet = @"grid-row-end: somegridarea";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-row-end"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("somegridarea"));
        }

        [Test]
        public void CssGridRowStartAutoLegal()
        {
            var snippet = @"grid-row-start: auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-row-start"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssGridAreaFourValuesLegal()
        {
            var snippet = @"grid-area: 2 / 2 / auto / span 3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-area"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2 / 2 / auto / span 3"));
        }

        [Test]
        public void CssGridAreaThreeValuesLegal()
        {
            var snippet = @"grid-area: 2 / foobar / auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-area"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2 / foobar / auto / foobar"));
        }

        [Test]
        public void CssGridAreaSingleValueLegal()
        {
            var snippet = @"grid-area: 2";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-area"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2 / auto / auto / auto"));
        }

        [Test]
        public void CssGridAreaTextValueLegal1()
        {
            var source = "#nav-header {grid-area: aaa; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: aaa }";
            Assert.That(text, Is.EqualTo(expected));
        }


        [Test]
        public void CssGridAreaTextValueLegal2()
        {
            var source = "#nav-header {grid-area: aaa / bbb; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: aaa / bbb / aaa / bbb }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueLegal3()
        {
            var source = "#nav-header {grid-area: 1 / 2; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: 1 / 2 / auto / auto }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueLegal4()
        {
            var source = "#nav-header {grid-area: aaa / 2; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: aaa / 2 / aaa / auto }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueLegal5()
        {
            var source = "#nav-header {grid-area: aaa / bbb / ccc; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: aaa / bbb / ccc / bbb }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueLegal6()
        {
            var source = "#nav-header {grid-area: aaa / bbb / ccc; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: aaa / bbb / ccc / bbb }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueLegal7()
        {
            var source = "#nav-header {grid-area: 1; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: 1 / auto / auto / auto }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueLegal8()
        {
            var source = "#nav-header {grid-area: 2 / aaa; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: 2 / aaa / auto / aaa }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueIllegal1()
        {
            var source = "#nav-header {grid-area: 2a / 3%; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueIllegal2()
        {
            var source = "#nav-header {grid-area: 2a; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueTrim1()
        {
            var source = "#nav-header {grid-area: 9999999; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: 10000 / auto / auto / auto }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAreaTextValueTrim2()
        {
            var source = "#nav-header {grid-area: 9999999 / 8888888 }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "#nav-header { grid-area: 10000 / 10000 / auto / auto }";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void CssGridAutoFlowAndRepeatLegal()
        {
            var snippet = @"grid: auto-flow 300px / repeat(3, [line1 line2 line3] 200px)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto-flow 300px / repeat(3, [line1 line2 line3] 200px)"));
        }

        [Test]
        public void CssGridAutoFlowDenseLegal()
        {
            var snippet = @"grid: auto-flow dense / 30%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto-flow dense / 30%"));
        }

        [Test]
        public void CssGridAutoFlowLineNameLegal()
        {
            var snippet = @"grid: auto-flow dense 40% / [line1] minmax(20em, max-content)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto-flow dense 40% / [line1] minmax(20em, max-content)"));
        }

        [Test]
        public void CssGridAutoFlowLengthLegal()
        {
            var snippet = @"grid: auto-flow / 200px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto-flow / 200px"));
        }

        [Test]
        public void CssGridRepeatAutoFlowLegal()
        {
            var snippet = @"grid: repeat(3, [line1 line2 line3] 200px) / auto-flow 300px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat(3, [line1 line2 line3] 200px) / auto-flow 300px"));
        }

        [Test]
        public void CssGridPercentDenseAutoFlowLegal()
        {
            var snippet = @"grid: 30% / auto-flow dense";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("30% / auto-flow dense"));
        }

        [Test]
        public void CssGridMinmaxAndRepeatLegal()
        {
            var snippet = @"grid: minmax(400px, min-content) / repeat(auto-fill, 50px)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("minmax(400px, min-content) / repeat(auto-fill, 50px)"));
        }

        [Test]
        public void CssGridMinmaxAndAutoFlowDenseLegal()
        {
            var snippet = @"grid: [line1] minmax(20em, max-content) / auto-flow dense 40%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[line1] minmax(20em, max-content) / auto-flow dense 40%"));
        }

        [Test]
        public void CssGridLengthAndAutoFlowLegal()
        {
            var snippet = @"grid: 200px / auto-flow";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("200px / auto-flow"));
        }

        [Test]
        public void CssGridLengthAndLengthLegal()
        {
            var snippet = @"grid: 100px / 200px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("100px / 200px"));
        }

        [Test]
        public void CssGridStringMinmaxAndStringLegal()
        {
            var snippet = @"grid: ""a"" minmax(100px, max-content) ""b"" 20%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"a\" minmax(100px, max-content) \"b\" 20%"));
        }

        [Test]
        public void CssGridStringLengthAndStringLegal()
        {
            var snippet = @"grid: ""a"" 200px ""b"" min-content";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"a\" 200px \"b\" min-content"));
        }

        [Test]
        public void CssGridLineNameAndStringLengthLegal()
        {
            var snippet = @"grid: [linename1] ""a"" 100px [linename2]";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[linename1] \"a\" 100px [linename2]"));
        }

        [Test]
        public void CssGridStringAndLengthAndStringLegal()
        {
            var snippet = @"grid: ""a"" 100px ""b"" 1fr";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"a\" 100px \"b\" 1fr"));
        }

        [Test]
        public void CssGridNoneLegal()
        {
            var snippet = @"grid: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssGridTemplateNoneLegal()
        {
            var snippet = @"grid-template: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssGridTemplateLineNamesAndStringWithFractionsLegal()
        {
            var snippet = @"grid-template: [header-top] ""a a a""     [header-bottom]
                 [main-top] ""b b b"" 1fr [main-bottom] / auto 1fr auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[header-top] \"a a a\" [header-bottom] [main-top] \"b b b\" 1fr [main-bottom] / auto 1fr auto"));
        }

        [Test]
        public void CssGridTemplateStirngsAndWidthsLegal()
        {
            var snippet = @"grid-template: ""a a a"" 20%
               ""b b b"" auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"a a a\" 20% \"b b b\" auto"));
        }

        [Test]
        public void CssGridTemplateStringsLegal()
        {
            var snippet = @"grid-template: ""a a a""
               ""b b b""";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"a a a\" \"b b b\""));
        }

        [Test]
        public void CssGridTemplateFitContentColumnsAndRowsLegal()
        {
            var snippet = @"grid-template: fit-content(100px) / fit-content(40%)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fit-content(100px) / fit-content(40%)"));
        }

        [Test]
        public void CssGridTemplateLineNamesAndPercentagesLegal()
        {
            var snippet = @"grid-template: [linename] 100px / [columnname1] 30% [columnname2] 70%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("[linename] 100px / [columnname1] 30% [columnname2] 70%"));
        }

        [Test]
        public void CssGridTemplateRowsAndColumnsWithAutoLegal()
        {
            var snippet = @"grid-template: auto 1fr / auto 1fr auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto 1fr / auto 1fr auto"));
        }

        [Test]
        public void CssGridTemplateRowsAndColumnsWithFractionsLegal()
        {
            var snippet = @"grid-template: 100px 1fr / 50px 1fr";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("grid-template"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("100px 1fr / 50px 1fr"));
        }

        [Test]
        public void CssRuleWithOnlyGridTemplateAreasLegal_Issue27()
        {
            var snippet = @"div#A { grid-template-areas: ""a b b"" ""a c d"" }";
            var rule = ParseRule(snippet);
            var text = rule.CssText;
            Assert.That(text, Is.EqualTo(snippet));
        }

        [Test]
        public void CssGridTemplateLonghands_Issue68()
        {
            var snippet = "grid-template-areas: none; grid-template-columns: none; grid-template-rows: none";
            var style = ParseDeclarations(snippet);
            Assert.That(style.CssText, Is.EqualTo("grid-template: none"));
        }

        [Test]
        public void CssGridPreservesParts_Issue137()
        {
            var snippet = "grid: 10px / 80px";
            var style = ParseDeclarations(snippet);
            Assert.That(style.CssText, Is.EqualTo("grid: 10px / 80px"));
        }

        [Test]
        public void CssGridGapPreservesParts_Issue137()
        {
            var snippet = "grid-gap: 10px 80px";
            var style = ParseDeclarations(snippet);
            Assert.That(style.CssText, Is.EqualTo("grid-gap: 10px 80px"));
        }
    }
}
