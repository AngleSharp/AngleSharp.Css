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
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssBorderImageSourceUrlLegal()
        {
            var snippet = "border-image-source: url(image.jpg)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"image.jpg\")", property.Value);
        }

        [Test]
        public void CssBorderImageSourceLinearGradientLegal()
        {
            var snippet = "border-image-source: linear-gradient(to top, red, yellow)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("linear-gradient(rgba(255, 0, 0, 1), rgba(255, 255, 0, 1))", property.Value);
        }

        [Test]
        public void CssBorderImageOutsetZeroLegal()
        {
            var snippet = "border-image-outset: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentLegal()
        {
            var snippet = "border-image-outset: 10px   25%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 25%", property.Value);
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentZeroLegal()
        {
            var snippet = "border-image-outset: 10px   25% 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 25% 0", property.Value);
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentZeroPercentLegal()
        {
            var snippet = "border-image-outset: 10px   25% 0 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 25% 0 10%", property.Value);
        }

        [Test]
        public void CssBorderImageOutsetZerosIllegal()
        {
            var snippet = "border-image-outset: 0 0 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssBorderImageWidthZeroLegal()
        {
            var snippet = "border-image-width: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssBorderImageWidthAutoLegal()
        {
            var snippet = "border-image-width: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssBorderImageWidthMultipleLegal()
        {
            var snippet = "border-image-width: 5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5", property.Value);
        }

        [Test]
        public void CssBorderImageWidthLengthPercentLegal()
        {
            var snippet = "border-image-width: 10px   25%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 25%", property.Value);
        }

        [Test]
        public void CssBorderImageWidthLengthPercentZeroLegal()
        {
            var snippet = "border-image-width: 10px   25% 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 25% 0", property.Value);
        }

        [Test]
        public void CssBorderImageWidthLengthPercentAutoPercentLegal()
        {
            var snippet = "border-image-width: 10px   25% auto 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 25% auto 10%", property.Value);
        }

        [Test]
        public void CssBorderImageWidthZerosIllegal()
        {
            var snippet = "border-image-width: 0 0 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssBorderImageRepeatStretchUppercaseLegal()
        {
            var snippet = "border-image-repeat:   StRETCH";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("stretch", property.Value);
        }

        [Test]
        public void CssBorderImageRepeatRepeatLegal()
        {
            var snippet = "border-image-repeat:   repeat";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("repeat", property.Value);
        }

        [Test]
        public void CssBorderImageRepeatRoundLegal()
        {
            var snippet = "border-image-repeat:   round";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("round", property.Value);
        }

        [Test]
        public void CssBorderImageRepeatStretchRoundLegal()
        {
            var snippet = "border-image-repeat: stretch round";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("stretch round", property.Value);
        }

        [Test]
        public void CssBorderImageRepeatNoRepeatIllegal()
        {
            var snippet = "border-image-repeat: no-repeat";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssBorderImageSlicePixelsLegal()
        {
            var snippet = "border-image-slice: 3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3", property.Value);
        }

        [Test]
        public void CssBorderImageSlicePercentLegal()
        {
            var snippet = "border-image-slice: 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10%", property.Value);
        }

        [Test]
        public void CssBorderImageSliceFillLegal()
        {
            var snippet = "border-image-slice: fill";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void CssBorderImageSlicePercentFillLegal()
        {
            var snippet = "border-image-slice: 10% fill";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10% fill", property.Value);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillLegal()
        {
            var snippet = "border-image-slice: 10% 30 fill";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10% 30 fill", property.Value);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillZerosLegal()
        {
            var snippet = "border-image-slice: 10% 30 fill 0 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10% 30 0 0 fill", property.Value);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillZerosIllegal()
        {
            var snippet = "border-image-slice: 10% 30 fill 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsZerosFillIllegal()
        {
            var snippet = "border-image-slice: 10% 30  0 0 0 fill";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssBorderImageNoneLegal()
        {
            var snippet = "border-image: none    ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssBorderImageUrlOffsetLegal()
        {
            var snippet = "border-image: url(image.png) 50 50";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"image.png\") 50 50", property.Value);
        }

        [Test]
        public void CssBorderImageUrlOffsetRepeatLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 repeat";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 repeat", property.Value);
        }

        [Test]
        public void CssBorderImageUrlStretchUppercaseLegal()
        {
            var snippet = "border-image: url(image.png) STRETCH";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"image.png\") stretch", property.Value);
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthTwoLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 / 15px 15px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 / 15px", property.Value);
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthFourLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 0 10 / 15px 0 15px 2em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 0 10 / 15px 0 15px 2em", property.Value);
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthOutsetLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 / 15px 15px / 5% 2% 0 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 / 15px / 5% 2% 0 10%", property.Value);
        }
    }
}
