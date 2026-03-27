namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Parser;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPropertyTests
    {
        [Test]
        public void CssBreakAfterLegalAvoid()
        {
            var snippet = "break-after:avoid";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("break-after"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("avoid"));
        }

        [Test]
        public void CssPageBreakAfterLegalAvoid()
        {
            var snippet = "page-break-after:avoid";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("page-break-after"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("avoid"));
        }

        [Test]
        public void CssBreakAfterLegalPageCapital()
        {
            var snippet = "break-after:Page";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("break-after"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("page"));
        }

        [Test]
        public void CssPageBreakAfterIllegalAvoidColumn()
        {
            var snippet = "page-break-after:avoid-column";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("page-break-after"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
        }

        [Test]
        public void CssBreakAfterLegalAvoidColumn()
        {
            var snippet = "break-after:avoid-column";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("break-after"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("avoid-column"));
        }

        [Test]
        public void CssBreakBeforeLegalAvoidColumn()
        {
            var snippet = "break-before:AUTO";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("break-before"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssPageBreakBeforeLegalAvoid()
        {
            var snippet = "page-break-before:AUTO";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("page-break-before"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssPageBreakBeforeLegalLeft()
        {
            var snippet = "page-break-before:left";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("page-break-before"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("left"));
        }

        [Test]
        public void CssBreakBeforeIllegalValue()
        {
            var snippet = "break-before:whatever";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("break-before"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsImportant, Is.False);
        }

        [Test]
        public void CssBreakInsideIllegalPage()
        {
            var snippet = "break-inside:page";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("break-inside"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.IsNotNull(property);
        }

        [Test]
        public void CssBreakInsideLegalAvoidRegionUppercase()
        {
            var snippet = "break-inside:avoid-REGION";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("break-inside"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("avoid-region"));
        }

        [Test]
        public void CssPageBreakInsideLegalAvoid()
        {
            var snippet = "page-break-inside:avoid";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("page-break-inside"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("avoid"));
        }

        [Test]
        public void CssPageBreakInsideLegalAutoUppercase()
        {
            var snippet = "page-break-inside:AUTO";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("page-break-inside"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssClearLegalLeft()
        {
            var snippet = "clear:left";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clear"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("left"));
        }

        [Test]
        public void CssClearLegalBoth()
        {
            var snippet = "clear:both";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clear"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("both"));
        }

        [Test]
        public void CssClearInherited()
        {
            var snippet = "clear:inherit";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clear"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.IsNotNull(property);
        }

        [Test]
        public void CssClearIllegal()
        {
            var snippet = "clear:yes";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clear"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.IsNotNull(property);
        }

        [Test]
        public void CssPositionLegalAbsolute()
        {
            var snippet = "position:absolute";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("position"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("absolute"));
        }

        [Test]
        public void CssDisplayLegalBlock()
        {
            var snippet = "display:   block ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("display"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("block"));
        }

        [Test]
        public void CssVisibilityLegalCollapse()
        {
            var snippet = "visibility:collapse";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("visibility"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("collapse"));
        }

        [Test]
        public void CssVisibilityLegalHiddenCompleteUppercase()
        {
            var snippet = "VISIBILITY:HIDDEN";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("visibility"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("hidden"));
        }

        [Test]
        public void CssOverflowLegalAuto()
        {
            var snippet = "overflow:auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("overflow"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssTableLayoutLegalFixedCapitalX()
        {
            var snippet = "table-layout: fiXed";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("table-layout"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("fixed"));
        }

        [Test]
        public void CssBoxShadowOffsetLegal()
        {
            var snippet = "box-shadow:  5px 4px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5px 4px"));
        }

        [Test]
        public void CssBoxShadowInsetOffsetLegal()
        {
            var snippet = "box-shadow: inset 5px 4px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inset 5px 4px"));
        }

        [Test]
        public void CssBoxShadowNoneUppercaseLegal()
        {
            var snippet = "box-shadow: NONE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssBoxShadowNormalTealLegal()
        {
            var snippet = "box-shadow: 60px -16px teal";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("60px -16px rgba(0, 128, 128, 1)"));
        }

        [Test]
        public void CssBoxShadowNormalSpreadBlackLegal()
        {
            var snippet = "box-shadow: 10px 5px 5px black";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 5px 5px rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowOliveAndRedLegal()
        {
            var snippet = "box-shadow: 3px 3px red, -1em 0 0.4em olive";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3px 3px rgba(255, 0, 0, 1), -1em 0 0.4em rgba(128, 128, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowInsetGoldLegal()
        {
            var snippet = "box-shadow: inset 5em 1em gold";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inset 5em 1em rgba(255, 215, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowZeroGoldLegal()
        {
            var snippet = "box-shadow: 0 0 1em gold";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0 0 1em rgba(255, 215, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowInsetZeroGoldLegal()
        {
            var snippet = "box-shadow: inset  0 0 1em gold";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inset 0 0 1em rgba(255, 215, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowInsetZeroGoldAndNormalRedLegal()
        {
            var snippet = "box-shadow: inset  0 0 1em  gold   ,  0 0   1em   red !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inset 0 0 1em rgba(255, 215, 0, 1), 0 0 1em rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowOffsetColorLegal()
        {
            var snippet = "box-shadow:  5px 4px #000";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5px 4px rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowOffsetBlurColorLegal()
        {
            var snippet = "box-shadow:  5px 4px 2px #000";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5px 4px 2px rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssBoxShadowInitialUppercaseLegal()
        {
            var snippet = "box-shadow:  INITIAL";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("initial"));
        }

        [Test]
        public void CssBoxShadowOffsetIllegal()
        {
            var snippet = "box-shadow:  5px 4px 2px 1px 3px #f00";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-shadow"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssClipShapeLegal()
        {
            var snippet = "clip: rect( 2px, 3em, 1in, 0cm )";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rect(2px, 3em, 1in, 0)"));
        }

        [Test]
        public void CssClipShapeBackwards()
        {
            var snippet = "clip: rect( 2px 3em 1in 0cm )";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rect(2px, 3em, 1in, 0)"));
        }

        [Test]
        public void CssClipShapeZerosLegal()
        {
            var snippet = "clip: rect(0, 0, 0, 0)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rect(0, 0, 0, 0)"));
        }

        [Test]
        public void CssClipShapeZerosIllegal()
        {
            var snippet = "clip: rect(0, 0, 0 0)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssClipShapeNonZerosIllegal()
        {
            var snippet = "clip: rect(2px, 1cm, 5mm)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssClipShapeSingleValueIllegal()
        {
            var snippet = "clip: rect(1em)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssCursorDefaultUppercaseLegal()
        {
            var snippet = "cursor: DEFAULT";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("default"));
        }

        [Test]
        public void CssCursorAutoLegal()
        {
            var snippet = "cursor: auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssCursorZoomOutLegal()
        {
            var snippet = "cursor  : zoom-out";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("zoom-out"));
        }

        [Test]
        public void CssCursorUrlNoFallbackIllegal()
        {
            var snippet = "cursor  : url(foo.png)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssCursorUrlLegal()
        {
            var snippet = "cursor  : url(foo.png), default";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"foo.png\"), default"));
        }

        [Test]
        public void CssCursorUrlShiftedLegal()
        {
            var snippet = "cursor  : url(foo.png) 0 5, auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"foo.png\") 0 5, auto"));
        }

        [Test]
        public void CssCursorUrlShiftedNoFallbackIllegal()
        {
            var snippet = "cursor  : url(foo.png) 0 5";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssCursorUrlsLegal()
        {
            var snippet = "cursor  : url(foo.png), url(master.png), url(more.png), wait";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("cursor"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"foo.png\"), url(\"master.png\"), url(\"more.png\"), wait"));
        }

        [Test]
        public void CssColorHexLegal()
        {
            var snippet = "color : #123456";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(18, 52, 86, 1)"));
        }

        [Test]
        public void CssColorRgbLegal()
        {
            var snippet = "color : rgb(121, 181, 201)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(121, 181, 201, 1)"));
        }

        [Test]
        public void CssColorRgbaLegal()
        {
            var snippet = "color : rgba(255, 255, 201, 0.7)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 255, 201, 0.7)"));
        }

        [Test]
        public void CssColorNameLegal()
        {
            var snippet = "color : red";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssColorNameUppercaseLegal()
        {
            var snippet = "color : BLUE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 0, 255, 1)"));
        }

        [Test]
        public void CssColorNameIllegal()
        {
            var snippet = "color : horse";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssOrphansZeroLegal()
        {
            var snippet = "orphans : 0 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("orphans"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssOrphansTwoLegal()
        {
            var snippet = "orphans : 2 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("orphans"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2"));
        }

        [Test]
        public void CssOrphansNegativeIllegal()
        {
            var snippet = "orphans : -2 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("orphans"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssOrphansFloatingIllegal()
        {
            var snippet = "orphans : 1.5 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("orphans"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBoxDecorationBreakNumberIllegal()
        {
            var snippet = "box-decoration-break : 1.5 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-decoration-break"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBoxDecorationBreakSliceLegal()
        {
            var snippet = "box-decoration-break : slice ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-decoration-break"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("slice"));
        }

        [Test]
        public void CssBoxDecorationBreakClonePascalLegal()
        {
            var snippet = "box-decoration-break : Clone ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-decoration-break"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("clone"));
        }

        [Test]
        public void CssBoxDecorationBreakInheritLegal()
        {
            var snippet = "box-decoration-break : inherit!important ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("box-decoration-break"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inherit"));
        }

        [Test]
        public void CssContentNormalLegal()
        {
            var snippet = "content : normal ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("content"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssContentNoneLegalUppercaseN()
        {
            var snippet = "content : noNe ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("content"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssContentStringLegal()
        {
            var snippet = "content : 'hi' ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("content"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"hi\""));
        }

        [Test]
        public void CssContentNoOpenQuoteNoCloseQuoteLegal()
        {
            var snippet = "content : no-open-quote no-close-quote ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("content"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("no-open-quote no-close-quote"));
        }

        [Test]
        public void CssContentUrlLegal()
        {
            var snippet = "content : url(test.html) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("content"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"test.html\")"));
        }

        [Test]
        public void CssContentStringsLegal()
        {
            var snippet = "content : 'how' 'are' 'you' ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("content"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"how\" \"are\" \"you\""));
        }

        [Test]
        public void CssQuoteStringIllegal()
        {
            var snippet = "quotes : '\"' ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssQuoteStringsLegal()
        {
            var snippet = "quotes : '\"' '\"' ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"\\\"\" \"\\\"\""));
        }

        [Test]
        public void CssQuoteStringsIllegal()
        {
            var snippet = "quotes : \"'\"";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssQuoteStringsMultipleLegal()
        {
            var snippet = "quotes : '\"' '\"' '`' '´' ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"\\\"\" \"\\\"\" \"`\" \"´\""));
        }

        [Test]
        public void CssQuoteStringsMultipleIllegal()
        {
            var snippet = "quotes : '\"' '\"' '`' ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssQuoteNoneLegal()
        {
            var snippet = "quotes : none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssQuoteNoneStringIllegal()
        {
            var snippet = "quotes : 'none'";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssQuoteNormalIllegal()
        {
            var snippet = "quotes : normal ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("quotes"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssWidowsZeroLegal()
        {
            var snippet = "widows: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("widows"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssWidowsThreeLegal()
        {
            var snippet = "widows: 3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("widows"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3"));
        }

        [Test]
        public void CssWidowsLengthIllegal()
        {
            var snippet = "widows: 5px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("widows"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssUnicodeBidiEmbedLegal()
        {
            var snippet = "unicode-BIDI: Embed";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("unicode-bidi"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("embed"));
        }

        [Test]
        public void CssUnicodeBidiIsolateLegal()
        {
            var snippet = "unicode-Bidi: isolate";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("unicode-bidi"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("isolate"));
        }

        [Test]
        public void CssUnicodeBidiBidiOverrideLegal()
        {
            var snippet = "unicode-Bidi: Bidi-Override";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("unicode-bidi"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("bidi-override"));
        }

        [Test]
        public void CssUnicodeBidiPlaintextLegal()
        {
            var snippet = "unicode-Bidi: PLAINTEXT";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("unicode-bidi"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("plaintext"));
        }

        [Test]
        public void CssUnicodeBidiIllegal()
        {
            var snippet = "unicode-bidi: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("unicode-bidi"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssPropertyFactoryCalls()
        {
            var factory = new DefaultDeclarationFactory();
            var invalid = factory.Create("invalid");
            var border = factory.Create("border");
            var color = factory.Create("color");

            Assert.That(invalid.Converter, Is.EqualTo(ValueConverters.Any));
            Assert.AreNotEqual(ValueConverters.Any, border.Converter);
            Assert.AreNotEqual(ValueConverters.Any, color.Converter);
        }

        [Test]
        public void CssUnknownPropertyAreAlsoRenderedInNormalWay()
        {
            var snippet = "my-Property: something";
            var property = ParseDeclaration(snippet, new CssParserOptions { IsIncludingUnknownDeclarations = true });
            Assert.That(property.Name, Is.EqualTo("my-property"));
        }
    }
}
