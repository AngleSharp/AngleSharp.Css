namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBorderImagePropertyTests
    {
        [Test]
        public void CssBorderImageSourceNoneLegal()
        {
            var snippet = "border-image-source: none    ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-source"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssBorderImageSourceUrlLegal()
        {
            var snippet = "border-image-source: url(image.jpg)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-source"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.jpg\")"));
        }

        [Test]
        public void CssBorderImageSourceLinearGradientLegal()
        {
            var snippet = "border-image-source: linear-gradient(to top, red, yellow)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-source"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("linear-gradient(0deg, rgba(255, 0, 0, 1), rgba(255, 255, 0, 1))"));
        }

        [Test]
        public void CssBorderImageOutsetZeroLegal()
        {
            var snippet = "border-image-outset: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-outset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentLegal()
        {
            var snippet = "border-image-outset: 10px   25%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-outset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 25%"));
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentZeroLegal()
        {
            var snippet = "border-image-outset: 10px   25% 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-outset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 25% 0"));
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentZeroPercentLegal()
        {
            var snippet = "border-image-outset: 10px   25% 0 10%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-outset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 25% 0 10%"));
        }

        [Test]
        public void CssBorderImageOutsetZerosIllegal()
        {
            var snippet = "border-image-outset: 0 0 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-outset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderImageWidthZeroLegal()
        {
            var snippet = "border-image-width: 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssBorderImageWidthAutoLegal()
        {
            var snippet = "border-image-width: auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssBorderImageWidthMultipleLegal()
        {
            var snippet = "border-image-width: 5";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("5"));
        }

        [Test]
        public void CssBorderImageWidthLengthPercentLegal()
        {
            var snippet = "border-image-width: 10px   25%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 25%"));
        }

        [Test]
        public void CssBorderImageWidthLengthPercentZeroLegal()
        {
            var snippet = "border-image-width: 10px   25% 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 25% 0"));
        }

        [Test]
        public void CssBorderImageWidthLengthPercentAutoPercentLegal()
        {
            var snippet = "border-image-width: 10px   25% auto 10%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 25% auto 10%"));
        }

        [Test]
        public void CssBorderImageWidthZerosIllegal()
        {
            var snippet = "border-image-width: 0 0 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderImageRepeatStretchUppercaseLegal()
        {
            var snippet = "border-image-repeat:   StRETCH";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("stretch"));
        }

        [Test]
        public void CssBorderImageRepeatRepeatLegal()
        {
            var snippet = "border-image-repeat:   repeat";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat"));
        }

        [Test]
        public void CssBorderImageRepeatRoundLegal()
        {
            var snippet = "border-image-repeat:   round";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("round"));
        }

        [Test]
        public void CssBorderImageRepeatStretchRoundLegal()
        {
            var snippet = "border-image-repeat: stretch round";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("stretch round"));
        }

        [Test]
        public void CssBorderImageRepeatNoRepeatIllegal()
        {
            var snippet = "border-image-repeat: no-repeat";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderImageSlicePixelsLegal()
        {
            var snippet = "border-image-slice: 3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3"));
        }

        [Test]
        public void CssBorderImageSlicePercentLegal()
        {
            var snippet = "border-image-slice: 10%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10%"));
        }

        [Test]
        public void CssBorderImageSliceFillLegal()
        {
            var snippet = "border-image-slice: fill";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssBorderImageSlicePercentFillLegal()
        {
            var snippet = "border-image-slice: 10% fill";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10% fill"));
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillLegal()
        {
            var snippet = "border-image-slice: 10% 30 fill";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10% 30 fill"));
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillZerosLegal()
        {
            var snippet = "border-image-slice: 10% 30 fill 0 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10% 30 0 0 fill"));
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillZerosIllegal()
        {
            var snippet = "border-image-slice: 10% 30 fill 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsZerosFillIllegal()
        {
            var snippet = "border-image-slice: 10% 30  0 0 0 fill";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image-slice"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBorderImageNoneLegal()
        {
            var snippet = "border-image: none    ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssBorderImageUrlOffsetLegal()
        {
            var snippet = "border-image: url(image.png) 50 50";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\") 50 50"));
        }

        [Test]
        public void CssBorderImageUrlOffsetRepeatLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 repeat";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\") 30 30 repeat"));
        }

        [Test]
        public void CssBorderImageUrlStretchUppercaseLegal()
        {
            var snippet = "border-image: url(image.png) STRETCH";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\") stretch"));
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthTwoLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 / 15px 15px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\") 30 30 / 15px"));
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthFourLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 0 10 / 15px 0 15px 2em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\") 30 30 0 10 / 15px 0 15px 2em"));
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthOutsetLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 / 15px 15px / 5% 2% 0 10%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("border-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\") 30 30 / 15px / 5% 2% 0 10%"));
        }
    }
}
