namespace AngleSharp.Css.Tests.Styling
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPhase14ContainmentPropertiesTests
    {

        [Test]
        public void CssContainNone()
        {
            var snippet = "contain: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssContainStrict()
        {
            var snippet = "contain: strict;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain", property.Name);
            Assert.AreEqual("strict", property.Value);
        }

        [Test]
        public void CssContainContent()
        {
            var snippet = "contain: content;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain", property.Name);
            Assert.AreEqual("content", property.Value);
        }

        [Test]
        public void CssContainLayout()
        {
            var snippet = "contain: layout;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain", property.Name);
            Assert.AreEqual("layout", property.Value);
        }

        [Test]
        public void CssContainStyle()
        {
            var snippet = "contain: style;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain", property.Name);
            Assert.AreEqual("style", property.Value);
        }

        [Test]
        public void CssContainPaint()
        {
            var snippet = "contain: paint;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain", property.Name);
            Assert.AreEqual("paint", property.Value);
        }

        [Test]
        public void CssContainSize()
        {
            var snippet = "contain: size;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain", property.Name);
            Assert.AreEqual("size", property.Value);
        }

        [Test]
        public void CssContainIntrinsicSizeNone()
        {
            var snippet = "contain-intrinsic-size: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-size", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssContainIntrinsicSizeAuto()
        {
            var snippet = "contain-intrinsic-size: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-size", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssContainIntrinsicSizeLength()
        {
            var snippet = "contain-intrinsic-size: 100px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-size", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void CssContainIntrinsicWidthNone()
        {
            var snippet = "contain-intrinsic-width: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-width", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssContainIntrinsicWidthAuto()
        {
            var snippet = "contain-intrinsic-width: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-width", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssContainIntrinsicWidthLength()
        {
            var snippet = "contain-intrinsic-width: 200px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-width", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void CssContainIntrinsicHeightNone()
        {
            var snippet = "contain-intrinsic-height: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-height", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssContainIntrinsicHeightAuto()
        {
            var snippet = "contain-intrinsic-height: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-height", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssContainIntrinsicHeightLength()
        {
            var snippet = "contain-intrinsic-height: 150px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-height", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void CssContainIntrinsicBlockSizeNone()
        {
            var snippet = "contain-intrinsic-block-size: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-block-size", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssContainIntrinsicBlockSizeAuto()
        {
            var snippet = "contain-intrinsic-block-size: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-block-size", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssContainIntrinsicBlockSizeLength()
        {
            var snippet = "contain-intrinsic-block-size: 100px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-block-size", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void CssContainIntrinsicInlineSizeNone()
        {
            var snippet = "contain-intrinsic-inline-size: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-inline-size", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssContainIntrinsicInlineSizeAuto()
        {
            var snippet = "contain-intrinsic-inline-size: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-inline-size", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssContainIntrinsicInlineSizeLength()
        {
            var snippet = "contain-intrinsic-inline-size: 80px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("contain-intrinsic-inline-size", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void CssWillChangeAuto()
        {
            var snippet = "will-change: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("will-change", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssWillChangeTransform()
        {
            var snippet = "will-change: transform;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("will-change", property.Name);
            Assert.AreEqual("transform", property.Value);
        }

        [Test]
        public void CssWillChangeOpacity()
        {
            var snippet = "will-change: opacity;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("will-change", property.Name);
            Assert.AreEqual("opacity", property.Value);
        }

        [Test]
        public void CssWillChangeContents()
        {
            var snippet = "will-change: contents;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("will-change", property.Name);
            Assert.AreEqual("contents", property.Value);
        }

        [Test]
        public void CssWillChangeScrollPosition()
        {
            var snippet = "will-change: scroll-position;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("will-change", property.Name);
            Assert.AreEqual("scroll-position", property.Value);
        }
    }
}
