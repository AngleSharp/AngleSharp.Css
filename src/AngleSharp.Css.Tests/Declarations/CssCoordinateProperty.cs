namespace AngleSharp.Css.Tests.Declarations
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
            Assert.That(property.Name, Is.EqualTo("height"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("28%"));
        }

        [Test]
        public void CssHeightLegalLengthInEm()
        {
            var snippet = "height:   0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("height"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("0.3em"));
        }

        [Test]
        public void CssHeightLegalLengthInPx()
        {
            var snippet = "height:   144px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("height"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("144px"));
        }

        [Test]
        public void CssHeightLegalAutoUppercase()
        {
            var snippet = "height: AUTO ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("height"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssWidthLegalLengthInCm()
        {
            var snippet = "width:0.5cm";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("0.5cm"));
        }

        [Test]
        public void CssWidthLegalLengthInMm()
        {
            var snippet = "width:1.5mm";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("1.5mm"));
        }

        [Test]
        public void CssWidthIllegalLength()
        {
            var snippet = "width:1.5 meter";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.IsNotNull(property);
        }

        [Test]
        public void CssLeftLegalPixel()
        {
            var snippet = "left: 25px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("left"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssTopLegalEm()
        {
            var snippet = "top:  0.7em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("top"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssRightLegalMm()
        {
            var snippet = "right:  1.5mm";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("right"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssBottomFoundInStyleDeclaration()
        {
            var snippet = "bottom:  50%";
            var style = ParseDeclarations(snippet);
            Assert.That(style.Length, Is.EqualTo(1));
            var bottom = style.Declarations.First();
            Assert.That(bottom.Name, Is.EqualTo("bottom"));
            Assert.That(((ICssStyleDeclaration)style).GetBottom(), Is.EqualTo("50%"));
        }

        [Test]
        public void CssBottomLegalPercent()
        {
            var snippet = "bottom:  50%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("bottom"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssHeightZeroLegal()
        {
            var snippet = "height:0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("height"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssWidthZeroLegal()
        {
            var snippet = "width  :  0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssWidthPercentLegal()
        {
            var snippet = "width  :  20.5%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssWidthPercentInLegal()
        {
            var snippet = "width  :  3in";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
        }

        [Test]
        public void CssHeightAngleIllegal()
        {
            var snippet = "height  :  3deg";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("height"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsInherited, Is.False);
        }

        [Test]
        public void CssHeightResolutionIllegal()
        {
            var snippet = "height  :  3dpi";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("height"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsInherited, Is.False);
        }

        [Test]
        public void CssTopLegalRem()
        {
            var snippet = "top:  1.2rem ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("top"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssRightLegalCm()
        {
            var snippet = "right:  0.5cm";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("right"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssBottomLegalPercentTwo()
        {
            var snippet = "bottom:  0.50%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("bottom"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssBottomLegalZero()
        {
            var snippet = "bottom:  0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("bottom"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssBottomIllegalNumber()
        {
            var snippet = "bottom:  20";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("bottom"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssMinHeightLegalZero()
        {
            var snippet = "min-height:  0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("min-height"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssMaxHeightIllegalAuto()
        {
            var snippet = "max-height:  auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("max-height"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssMaxWidthLegalNone()
        {
            var snippet = "max-width:  none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("max-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssMaxWidthLegalLength()
        {
            var snippet = "max-width:  15px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("max-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15px"));
        }

        [Test]
        public void CssMinWidthLegalPercent()
        {
            var snippet = "min-width:  15%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("min-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15%"));
        }
    }
}
