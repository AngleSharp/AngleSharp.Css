﻿namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssCoordinatePropertyTests
    {
        [Test]
        public void CssHeightLegalPercentage()
        {
            var snippet = "height:   28% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("28%", property.Value);
        }

        [Test]
        public void CssHeightLegalLengthInEm()
        {
            var snippet = "height:   0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("0.3em", property.Value);
        }

        [Test]
        public void CssHeightLegalLengthInPx()
        {
            var snippet = "height:   144px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("144px", property.Value);
        }

        [Test]
        public void CssHeightLegalAutoUppercase()
        {
            var snippet = "height: AUTO ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssWidthLegalLengthInCm()
        {
            var snippet = "width:0.5cm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("0.5cm", property.Value);
        }

        [Test]
        public void CssWidthLegalLengthInMm()
        {
            var snippet = "width:1.5mm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("1.5mm", property.Value);
        }

        [Test]
        public void CssWidthIllegalLength()
        {
            var snippet = "width:1.5 meter";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsNotNull(property);
        }

        [Test]
        public void CssLeftLegalPixel()
        {
            var snippet = "left: 25px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssTopLegalEm()
        {
            var snippet = "top:  0.7em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssRightLegalMm()
        {
            var snippet = "right:  1.5mm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssBottomFoundInStyleDeclaration()
        {
            var snippet = "bottom:  50%";
            var style = ParseDeclarations(snippet);
            Assert.AreEqual(1, style.Length);
            var bottom = style.Declarations.First();
            Assert.AreEqual("bottom", bottom.Name);
            Assert.AreEqual("50%", ((ICssStyleDeclaration)style).GetBottom());
        }

        [Test]
        public void CssBottomLegalPercent()
        {
            var snippet = "bottom:  50%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssHeightZeroLegal()
        {
            var snippet = "height:0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssWidthZeroLegal()
        {
            var snippet = "width  :  0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssWidthPercentLegal()
        {
            var snippet = "width  :  20.5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssWidthPercentInLegal()
        {
            var snippet = "width  :  3in";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
        }

        [Test]
        public void CssHeightAngleIllegal()
        {
            var snippet = "height  :  3deg";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
        }

        [Test]
        public void CssHeightResolutionIllegal()
        {
            var snippet = "height  :  3dpi";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
        }

        [Test]
        public void CssTopLegalRem()
        {
            var snippet = "top:  1.2rem ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssRightLegalCm()
        {
            var snippet = "right:  0.5cm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssBottomLegalPercentTwo()
        {
            var snippet = "bottom:  0.50%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssBottomLegalZero()
        {
            var snippet = "bottom:  0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssBottomIllegalNumber()
        {
            var snippet = "bottom:  20";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssMinHeightLegalZero()
        {
            var snippet = "min-height:  0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssMaxHeightIllegalAuto()
        {
            var snippet = "max-height:  auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssMaxWidthLegalNone()
        {
            var snippet = "max-width:  none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssMaxWidthLegalLength()
        {
            var snippet = "max-width:  15px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15px", property.Value);
        }

        [Test]
        public void CssMinWidthLegalPercent()
        {
            var snippet = "min-width:  15%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15%", property.Value);
        }
    }
}
