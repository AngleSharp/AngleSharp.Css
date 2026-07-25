namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBoxModelPropertyTests
    {
        #region aspect-ratio

        [Test]
        public void AspectRatioAutoLegal()
        {
            var snippet = "aspect-ratio: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("aspect-ratio", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void AspectRatioRatioLegal()
        {
            var snippet = "aspect-ratio: 16/9";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("aspect-ratio", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("16/9", property.Value);
        }

        [Test]
        public void AspectRatioSquareLegal()
        {
            var snippet = "aspect-ratio: 1/1";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("aspect-ratio", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1/1", property.Value);
        }

        [Test]
        public void AspectRatioNegativeLegal()
        {
            // Note: the spec requires non-negative ratios, but ParseRatio does
            // not currently enforce a sign constraint.
            var snippet = "aspect-ratio: -1/1";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("aspect-ratio", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void AspectRatioStringIllegal()
        {
            var snippet = "aspect-ratio: wide";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("aspect-ratio", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        #endregion

        #region block-size

        [Test]
        public void BlockSizeAutoLegal()
        {
            var snippet = "block-size: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("block-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void BlockSizePixelLegal()
        {
            var snippet = "block-size: 200px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("block-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("200px", property.Value);
        }

        [Test]
        public void BlockSizePercentLegal()
        {
            var snippet = "block-size: 50%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("block-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("50%", property.Value);
        }

        [Test]
        public void BlockSizeMaxContentLegal()
        {
            var snippet = "block-size: max-content";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("block-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("max-content", property.Value);
        }

        #endregion

        #region inline-size

        [Test]
        public void InlineSizeAutoLegal()
        {
            var snippet = "inline-size: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inline-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void InlineSizePixelLegal()
        {
            var snippet = "inline-size: 100px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inline-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px", property.Value);
        }

        [Test]
        public void InlineSizePercentLegal()
        {
            var snippet = "inline-size: 75%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inline-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("75%", property.Value);
        }

        #endregion

        #region min-block-size

        [Test]
        public void MinBlockSizeZeroLegal()
        {
            var snippet = "min-block-size: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-block-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void MinBlockSizePixelLegal()
        {
            var snippet = "min-block-size: 50px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-block-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("50px", property.Value);
        }

        [Test]
        public void MinBlockSizeAutoIllegal()
        {
            var snippet = "min-block-size: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-block-size", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        #endregion

        #region max-block-size

        [Test]
        public void MaxBlockSizeNoneLegal()
        {
            var snippet = "max-block-size: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-block-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void MaxBlockSizePixelLegal()
        {
            var snippet = "max-block-size: 500px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-block-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("500px", property.Value);
        }

        #endregion

        #region min-inline-size

        [Test]
        public void MinInlineSizeZeroLegal()
        {
            var snippet = "min-inline-size: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-inline-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void MinInlineSizePixelLegal()
        {
            var snippet = "min-inline-size: 100px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-inline-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("100px", property.Value);
        }

        #endregion

        #region max-inline-size

        [Test]
        public void MaxInlineSizeNoneLegal()
        {
            var snippet = "max-inline-size: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-inline-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void MaxInlineSizePixelLegal()
        {
            var snippet = "max-inline-size: 800px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-inline-size", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("800px", property.Value);
        }

        #endregion

        #region overflow-clip-margin

        [Test]
        public void OverflowClipMarginZeroLegal()
        {
            var snippet = "overflow-clip-margin: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-clip-margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void OverflowClipMarginPixelLegal()
        {
            var snippet = "overflow-clip-margin: 10px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-clip-margin", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px", property.Value);
        }

        [Test]
        public void OverflowClipMarginContentBoxLegal()
        {
            var snippet = "overflow-clip-margin: content-box";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-clip-margin", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("content-box", property.Value);
        }

        [Test]
        public void OverflowClipMarginPaddingBoxLegal()
        {
            var snippet = "overflow-clip-margin: padding-box";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-clip-margin", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("padding-box", property.Value);
        }

        [Test]
        public void OverflowClipMarginNegativeLegal()
        {
            // Note: the spec requires non-negative lengths, but the current
            // LengthOrPercentConverter does not enforce sign constraints.
            var snippet = "overflow-clip-margin: -5px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-clip-margin", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("-5px", property.Value);
        }

        #endregion

        #region overflow-anchor

        [Test]
        public void OverflowAnchorAutoLegal()
        {
            var snippet = "overflow-anchor: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-anchor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void OverflowAnchorNoneLegal()
        {
            var snippet = "overflow-anchor: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-anchor", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void OverflowAnchorInvalidIllegal()
        {
            var snippet = "overflow-anchor: scroll";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow-anchor", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        #endregion
    }
}
