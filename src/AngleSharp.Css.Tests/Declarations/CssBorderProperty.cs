using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;

namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBorderPropertyTests
    {
        [Test]
        public void CssBorderSpacingLengthLegal()
        {
            var snippet = "border-spacing: 20px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("20px"));
        }

        [Test]
        public void CssBorderSpacingZeroLegal()
        {
            var snippet = "border-spacing: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssBorderSpacingLengthLengthLegal()
        {
            var snippet = "border-spacing: 15px 3em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15px 3em"));
        }

        [Test]
        public void CssBorderSpacingLengthZeroLegal()
        {
            var snippet = "border-spacing: 15px 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15px 0"));
        }

        [Test]
        public void CssBorderSpacingPercentIllegal()
        {
            var snippet = "border-spacing: 15%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderBottomColorRedLegal()
        {
            var snippet = "border-bottom-color: red";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-bottom-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssBorderTopColorHexLegal()
        {
            var snippet = "border-top-color: #0F0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-top-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 255, 0, 1)"));
        }

        [Test]
        public void CssBorderRightColorRgbaLegal()
        {
            var snippet = "border-right-color: rgba(1, 1, 1, 0)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-right-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(1, 1, 1, 0)"));
        }

        [Test]
        public void CssBorderLeftColorRgbLegal()
        {
            var snippet = "border-left-color: rgb(1, 255, 100)  !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-left-color"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(1, 255, 100, 1)"));
        }

        [Test]
        public void CssBorderColorTransparentLegal()
        {
            var snippet = "border-color: transparent";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 0, 0, 0)"));
        }

        [Test]
        public void CssBorderColorRedGreenLegal()
        {
            var snippet = "border-color: red   green";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1) rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssBorderColorRedRgbLegal()
        {
            var snippet = "border-color: red   rgb(0,0,0)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1) rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssBorderColorRedBlueGreenLegal()
        {
            var snippet = "border-color: red blue green";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1) rgba(0, 0, 255, 1) rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssBorderColorRedBlueGreenBlackLegal()
        {
            var snippet = "border-color: red blue green   BLACK";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1) rgba(0, 0, 255, 1) rgba(0, 128, 0, 1) rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssBorderColorRedBlueGreenBlackTransparentIllegal()
        {
            var snippet = "border-color: red blue green black transparent";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderStyleDottedLegal()
        {
            var snippet = "border-style: dotted";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("dotted"));
        }

        [Test]
        public void CssBorderStyleInsetOutsetUpperLegal()
        {
            var snippet = "border-style: INSET   OUTset";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inset outset"));
        }

        [Test]
        public void CssBorderStyleDoubleGrooveLegal()
        {
            var snippet = "border-style: double   groove";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("double groove"));
        }

        [Test]
        public void CssBorderStyleRidgeSolidDashedLegal()
        {
            var snippet = "border-style: ridge solid dashed";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("ridge solid dashed"));
        }

        [Test]
        public void CssBorderStyleHiddenDottedNoneNoneLegal()
        {
            var snippet = "border-style   :   hidden  dotted  NONE   nONe";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("hidden dotted none none"));
        }

        [Test]
        public void CssBorderStyleMultipleExpandCorrectly_Issue34()
        {
            var source = @"<!DOCTYPE html>
<html>
<head><title></title></head>
<body style=""border-style: hidden double dashed;""></body>
</html>";
            var document = source.ToHtmlDocument(Configuration.Default.WithCss());
            var styleDeclaration = document.Body.ComputeCurrentStyle();
            Assert.That(styleDeclaration.GetBorderTopStyle(), Is.EqualTo("hidden"));
            Assert.That(styleDeclaration.GetBorderLeftStyle(), Is.EqualTo("double"));
            Assert.That(styleDeclaration.GetBorderRightStyle(), Is.EqualTo("double"));
            Assert.That(styleDeclaration.GetBorderBottomStyle(), Is.EqualTo("dashed"));
        }

        [Test]
        public void CssBorderStyleWavyIllegal()
        {
            var snippet = "border-style: wavy";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderBottomStyleGrooveLegal()
        {
            var snippet = "border-bottom-style: GROOVE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-bottom-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("groove"));
        }

        [Test]
        public void CssBorderTopStyleNoneLegal()
        {
            var snippet = "border-top-style:none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-top-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssBorderRightStyleDoubleLegal()
        {
            var snippet = "border-right-style:double";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-right-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("double"));
        }

        [Test]
        public void CssBorderLeftStyleHiddenLegal()
        {
            var snippet = "border-left-style: hidden  !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-left-style"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("hidden"));
        }

        [Test]
        public void CssBorderBottomWidthThinLegal()
        {
            var snippet = "border-bottom-width: THIN";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-bottom-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px"));
        }

        [Test]
        public void CssBorderTopWidthZeroLegal()
        {
            var snippet = "border-top-width: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-top-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssBorderRightWidthEmLegal()
        {
            var snippet = "border-right-width: 3em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-right-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3em"));
        }

        [Test]
        public void CssBorderLeftWidthThickLegal()
        {
            var snippet = "border-left-width: thick !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-left-width"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5px"));
        }

        [Test]
        public void CssBorderWidthMediumLegal()
        {
            var snippet = "border-width: medium";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3px"));
        }

        [Test]
        public void CssBorderWidthLengthZeroLegal()
        {
            var snippet = "border-width: 3px   0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3px 0"));
        }

        [Test]
        public void CssBorderWidthThinLengthLegal()
        {
            var snippet = "border-width: THIN   1px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px"));
        }

        [Test]
        public void CssBorderWidthMediumThinThickLegal()
        {
            var snippet = "border-width: medium thin thick";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3px 1px 5px"));
        }

        [Test]
        public void CssBorderWidthLengthLengthLengthLengthLegal()
        {
            var snippet = "border-width:  1px  2px   3px  4px  !important ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-width"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px 2px 3px 4px"));
        }

        [Test]
        public void CssBorderWidthLengthInEmZeroLegal()
        {
            var snippet = "border-width:  0.3em 0 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.3em 0"));
        }

        [Test]
        public void CssBorderWidthMediumZeroLengthThickLegal()
        {
            var snippet = "border-width:   medium 0 1px thick ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3px 0 1px 5px"));
        }

        [Test]
        public void CssBorderWidthZerosIllegal()
        {
            var snippet = "border-width: 0 0 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderLeftZeroLegal()
        {
            var snippet = "border-left:   0 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-left"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssBorderRightLineStyleLegal()
        {
            var snippet = "border-right :   dotted ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-right"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("dotted"));
        }

        [Test]
        public void CssBorderTopLengthRedLegal()
        {
            var snippet = "border-top :  2px red ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-top"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2px rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssBorderBottomRgbLegal()
        {
            var snippet = "border-bottom :  rgb(255, 100, 0) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-bottom"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 100, 0, 1)"));
        }

        [Test]
        public void CssBorderGrooveRgbLegal()
        {
            var snippet = "border :  GROOVE rgb(255, 100, 0) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssBorderInsetGreenLengthLegal()
        {
            var snippet = "border :  inset  green 3em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3em inset rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssBorderRedSolidLengthLegal()
        {
            var snippet = "border :  red  SOLID 1px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssBorderLengthBlackDoubleLegal()
        {
            var snippet = "border :  0.5px black double ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.5px double rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssBorderOutSetCurrentColor()
        {
            var snippet = "border: 1px outset currentColor";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px outset currentColor"));
        }

        [Test]
        public void CssBorderOutSetWithNoColor()
        {
            var snippet = "border: 1px outset";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px outset"));
        }

        [Test]
        public void CssBorderAggregation()
        {
            var expectedCss = "border: 1px solid rgba(0, 0, 0, 1)";
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var style = new CssStyleDeclaration(context);
            style.SetBorderWidth("1px");
            style.SetBorderStyle("solid");
            style.SetBorderColor("black");
            Assert.That(style.CssText, Is.EqualTo(expectedCss));
        }
    }
}
