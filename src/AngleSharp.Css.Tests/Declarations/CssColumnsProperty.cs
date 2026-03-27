namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssColumnsPropertyTests
    {
        [Test]
        public void CssColumnWidthLengthLegal()
        {
            var snippet = "column-width: 300px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("300px"));
        }

        [Test]
        public void CssColumnWidthPercentIllegal()
        {
            var snippet = "column-width: 30%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssColumnWidthVwLegal()
        {
            var snippet = "column-width: 0.3vw";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.3vw"));
        }

        [Test]
        public void CssColumnWidthAutoUppercaseLegal()
        {
            var snippet = "column-width: AUTO";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssColumnCountAutoLowercaseLegal()
        {
            var snippet = "column-count: auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssColumnCountNumberLegal()
        {
            var snippet = "column-count: 3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3"));
        }

        [Test]
        public void CssColumnCountZeroLegal()
        {
            var snippet = "column-count: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-count"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssColumsZeroLegal()
        {
            var snippet = "columns: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("columns"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssColumsLengthLegal()
        {
            var snippet = "columns: 10px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("columns"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px"));
        }

        [Test]
        public void CssColumsNumberLegal()
        {
            var snippet = "columns: 4";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("columns"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("4"));
        }

        [Test]
        public void CssColumsLengthNumberLegal()
        {
            var snippet = "columns: 25em 5";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("columns"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("25em 5"));
        }

        [Test]
        public void CssColumsNumberLengthLegal()
        {
            var snippet = "columns : 5   25em  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("columns"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("25em 5"));
        }

        [Test]
        public void CssColumsAutoAutoLegal()
        {
            var snippet = "columns : auto auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("columns"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto auto"));
        }

        [Test]
        public void CssColumsAutoLegal()
        {
            var snippet = "columns : auto  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("columns"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssColumsNumberPercenIllegal()
        {
            var snippet = "columns : 5   25%  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssColumSpanAllLegal()
        {
            var snippet = "column-span: all";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-span"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("all"));
        }

        [Test]
        public void CssColumSpanNoneUppercaseLegal()
        {
            var snippet = "column-span: None";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-span"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssColumSpanLengthIllegal()
        {
            var snippet = "column-span: 10px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-span"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssColumGapLengthLegal()
        {
            var snippet = "column-gap: 20px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-gap"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("20px"));
        }

        [Test]
        public void CssColumGapNormalLegal()
        {
            var snippet = "column-gap: normal";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-gap"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssColumGapZeroLegal()
        {
            var snippet = "column-gap: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-gap"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssColumGapPercentLegal()
        {
            var snippet = "column-gap: 20%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-gap"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("20%"));
        }

        [Test]
        public void CssColumFillBalanceLegal()
        {
            var snippet = "column-fill: balance;";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-fill"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("balance"));
        }

        [Test]
        public void CssColumFillAutoLegal()
        {
            var snippet = "column-fill: auto;";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-fill"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssColumRuleColorTransparentLegal()
        {
            var snippet = "column-rule-color: transparent";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 0, 0, 0)"));
        }

        [Test]
        public void CssColumRuleColorRgbLegal()
        {
            var snippet = "column-rule-color: rgb(192, 56, 78)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(192, 56, 78, 1)"));
        }

        [Test]
        public void CssColumRuleColorRedLegal()
        {
            var snippet = "column-rule-color: red";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssColumRuleColorNoneIllegal()
        {
            var snippet = "column-rule-color: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssColumRuleStyleInsetTailUpperLegal()
        {
            var snippet = "column-rule-style: inSET";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inset"));
        }

        [Test]
        public void CssColumRuleStyleNoneLegal()
        {
            var snippet = "column-rule-style: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssColumRuleStyleAutoIllegal()
        {
            var snippet = "column-rule-style: auto ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssColumRuleWidthLengthLegal()
        {
            var snippet = "column-rule-width: 2px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2px"));
        }

        [Test]
        public void CssColumRuleWidthThickLegal()
        {
            var snippet = "column-rule-width: thick";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5px"));
        }

        [Test]
        public void CssColumRuleWidthMediumLegal()
        {
            var snippet = "column-rule-width : medium !important ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-width"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3px"));
        }

        [Test]
        public void CssColumRuleWidthThinUppercaseLegal()
        {
            var snippet = "column-rule-width: THIN";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px"));
        }

        [Test]
        public void CssColumRuleDottedLegal()
        {
            var snippet = "column-rule: dotted";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("dotted"));
        }

        [Test]
        public void CssColumRuleSolidBlueLegal()
        {
            var snippet = "column-rule: solid  blue";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 0, 255, 1) solid"));
        }

        [Test]
        public void CssColumRuleSolidLengthLegal()
        {
            var snippet = "column-rule: solid 8px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("8px solid"));
        }

        [Test]
        public void CssColumRuleThickInsetBlueLegal()
        {
            var snippet = "column-rule: thick inset blue";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("column-rule"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 0, 255, 1) 5px inset"));
        }
    }
}
