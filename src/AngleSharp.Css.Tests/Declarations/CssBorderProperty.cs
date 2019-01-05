namespace AngleSharp.Css.Tests.Declarations
{
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
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("20px", property.Value);
        }

        [Test]
        public void CssBorderSpacingZeroLegal()
        {
            var snippet = "border-spacing: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssBorderSpacingLengthLengthLegal()
        {
            var snippet = "border-spacing: 15px 3em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15px 3em", property.Value);
        }

        [Test]
        public void CssBorderSpacingLengthZeroLegal()
        {
            var snippet = "border-spacing: 15px 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15px 0", property.Value);
        }

        [Test]
        public void CssBorderSpacingPercentIllegal()
        {
            var snippet = "border-spacing: 15%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssBorderBottomColorRedLegal()
        {
            var snippet = "border-bottom-color: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderTopColorHexLegal()
        {
            var snippet = "border-top-color: #0F0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(0, 255, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderRightColorRgbaLegal()
        {
            var snippet = "border-right-color: rgba(1, 1, 1, 0)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(1, 1, 1, 0)", property.Value);
        }

        [Test]
        public void CssBorderLeftColorRgbLegal()
        {
            var snippet = "border-left-color: rgb(1, 255, 100)  !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left-color", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(1, 255, 100, 1)", property.Value);
        }

        [Test]
        public void CssBorderColorTransparentLegal()
        {
            var snippet = "border-color: transparent";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(0, 0, 0, 0)", property.Value);
        }

        [Test]
        public void CssBorderColorRedGreenLegal()
        {
            var snippet = "border-color: red   green";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1) rgba(0, 128, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderColorRedRgbLegal()
        {
            var snippet = "border-color: red   rgb(0,0,0)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1) rgba(0, 0, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderColorRedBlueGreenLegal()
        {
            var snippet = "border-color: red blue green";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1) rgba(0, 0, 255, 1) rgba(0, 128, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderColorRedBlueGreenBlackLegal()
        {
            var snippet = "border-color: red blue green   BLACK";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1) rgba(0, 0, 255, 1) rgba(0, 128, 0, 1) rgba(0, 0, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderColorRedBlueGreenBlackTransparentIllegal()
        {
            var snippet = "border-color: red blue green black transparent";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssBorderStyleDottedLegal()
        {
            var snippet = "border-style: dotted";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("dotted", property.Value);
        }

        [Test]
        public void CssBorderStyleInsetOutsetUpperLegal()
        {
            var snippet = "border-style: INSET   OUTset";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("inset outset", property.Value);
        }

        [Test]
        public void CssBorderStyleDoubleGrooveLegal()
        {
            var snippet = "border-style: double   groove";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("double groove", property.Value);
        }

        [Test]
        public void CssBorderStyleRidgeSolidDashedLegal()
        {
            var snippet = "border-style: ridge solid dashed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("ridge solid dashed", property.Value);
        }

        [Test]
        public void CssBorderStyleHiddenDottedNoneNoneLegal()
        {
            var snippet = "border-style   :   hidden  dotted  NONE   nONe";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("hidden dotted none none", property.Value);
        }

        [Test]
        public void CssBorderStyleWavyIllegal()
        {
            var snippet = "border-style: wavy";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssBorderBottomStyleGrooveLegal()
        {
            var snippet = "border-bottom-style: GROOVE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("groove", property.Value);
        }

        [Test]
        public void CssBorderTopStyleNoneLegal()
        {
            var snippet = "border-top-style:none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssBorderRightStyleDoubleLegal()
        {
            var snippet = "border-right-style:double";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("double", property.Value);
        }

        [Test]
        public void CssBorderLeftStyleHiddenLegal()
        {
            var snippet = "border-left-style: hidden  !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left-style", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("hidden", property.Value);
        }

        [Test]
        public void CssBorderBottomWidthThinLegal()
        {
            var snippet = "border-bottom-width: THIN";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1px", property.Value);
        }

        [Test]
        public void CssBorderTopWidthZeroLegal()
        {
            var snippet = "border-top-width: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssBorderRightWidthEmLegal()
        {
            var snippet = "border-right-width: 3em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3em", property.Value);
        }

        [Test]
        public void CssBorderLeftWidthThickLegal()
        {
            var snippet = "border-left-width: thick !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5px", property.Value);
        }

        [Test]
        public void CssBorderWidthMediumLegal()
        {
            var snippet = "border-width: medium";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3px", property.Value);
        }

        [Test]
        public void CssBorderWidthLengthZeroLegal()
        {
            var snippet = "border-width: 3px   0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3px 0", property.Value);
        }

        [Test]
        public void CssBorderWidthThinLengthLegal()
        {
            var snippet = "border-width: THIN   1px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1px", property.Value);
        }

        [Test]
        public void CssBorderWidthMediumThinThickLegal()
        {
            var snippet = "border-width: medium thin thick";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3px 1px 5px", property.Value);
        }

        [Test]
        public void CssBorderWidthLengthLengthLengthLengthLegal()
        {
            var snippet = "border-width:  1px  2px   3px  4px  !important ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1px 2px 3px 4px", property.Value);
        }

        [Test]
        public void CssBorderWidthLengthInEmZeroLegal()
        {
            var snippet = "border-width:  0.3em 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.3em 0", property.Value);
        }

        [Test]
        public void CssBorderWidthMediumZeroLengthThickLegal()
        {
            var snippet = "border-width:   medium 0 1px thick ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3px 0 1px 5px", property.Value);
        }

        [Test]
        public void CssBorderWidthZerosIllegal()
        {
            var snippet = "border-width: 0 0 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssBorderLeftZeroLegal()
        {
            var snippet = "border-left:   0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssBorderRightLineStyleLegal()
        {
            var snippet = "border-right :   dotted ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("dotted", property.Value);
        }

        [Test]
        public void CssBorderTopLengthRedLegal()
        {
            var snippet = "border-top :  2px red ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2px rgba(255, 0, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderBottomRgbLegal()
        {
            var snippet = "border-bottom :  rgb(255, 100, 0) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 100, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderGrooveRgbLegal()
        {
            var snippet = "border :  GROOVE rgb(255, 100, 0) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssBorderInsetGreenLengthLegal()
        {
            var snippet = "border :  inset  green 3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3em inset rgba(0, 128, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderRedSolidLengthLegal()
        {
            var snippet = "border :  red  SOLID 1px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssBorderLengthBlackDoubleLegal()
        {
            var snippet = "border :  0.5px black double ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.5px double rgba(0, 0, 0, 1)", property.Value);
        }

        [Test]
        public void CssBorderOutSetCurrentColor()
        {
            var snippet = "border: 1px outset currentColor";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1px outset currentColor", property.Value);
        }

        [Test]
        public void CssBorderOutSetWithNoColor()
        {
            var snippet = "border: 1px outset";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1px outset", property.Value);
        }
    }
}
