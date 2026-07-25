namespace AngleSharp.Css.Tests.Styling
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPhase13MaskPropertiesTests
    {
        [Test]
        public void CssMaskModeAlpha()
        {
            var snippet = "mask-mode: alpha;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-mode", property.Name);
            Assert.AreEqual("alpha", property.Value);
        }

        [Test]
        public void CssMaskModeLuminance()
        {
            var snippet = "mask-mode: luminance;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-mode", property.Name);
            Assert.AreEqual("luminance", property.Value);
        }

        [Test]
        public void CssMaskClipBorderBox()
        {
            var snippet = "mask-clip: border-box;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-clip", property.Name);
            Assert.AreEqual("border-box", property.Value);
        }

        [Test]
        public void CssMaskClipPaddingBox()
        {
            var snippet = "mask-clip: padding-box;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-clip", property.Name);
            Assert.AreEqual("padding-box", property.Value);
        }

        [Test]
        public void CssMaskClipContentBox()
        {
            var snippet = "mask-clip: content-box;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-clip", property.Name);
            Assert.AreEqual("content-box", property.Value);
        }

        [Test]
        public void CssMaskOriginBorderBox()
        {
            var snippet = "mask-origin: border-box;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-origin", property.Name);
            Assert.AreEqual("border-box", property.Value);
        }

        [Test]
        public void CssMaskOriginPaddingBox()
        {
            var snippet = "mask-origin: padding-box;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-origin", property.Name);
            Assert.AreEqual("padding-box", property.Value);
        }

        [Test]
        public void CssMaskCompositeAdd()
        {
            var snippet = "mask-composite: add;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-composite", property.Name);
            Assert.AreEqual("add", property.Value);
        }

        [Test]
        public void CssMaskCompositeSubtract()
        {
            var snippet = "mask-composite: subtract;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-composite", property.Name);
            Assert.AreEqual("subtract", property.Value);
        }

        [Test]
        public void CssMaskCompositeIntersect()
        {
            var snippet = "mask-composite: intersect;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-composite", property.Name);
            Assert.AreEqual("intersect", property.Value);
        }

        [Test]
        public void CssMaskTypeLuminance()
        {
            var snippet = "mask-type: luminance;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-type", property.Name);
            Assert.AreEqual("luminance", property.Value);
        }

        [Test]
        public void CssMaskTypeAlpha()
        {
            var snippet = "mask-type: alpha;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-type", property.Name);
            Assert.AreEqual("alpha", property.Value);
        }

        [Test]
        public void CssMaskBorderNone()
        {
            var snippet = "mask-border: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-border", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssMaskBorderSourceNone()
        {
            var snippet = "mask-border-source: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-border-source", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssMaskBorderSliceNumber()
        {
            var snippet = "mask-border-slice: 10;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-border-slice", property.Name);
            Assert.AreEqual("10", property.Value);
        }

        [Test]
        public void CssMaskBorderWidthAuto()
        {
            var snippet = "mask-border-width: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-border-width", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssMaskBorderModeAlpha()
        {
            var snippet = "mask-border-mode: alpha;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-border-mode", property.Name);
            Assert.AreEqual("alpha", property.Value);
        }

        [Test]
        public void CssMaskBorderModeLuminance()
        {
            var snippet = "mask-border-mode: luminance;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("mask-border-mode", property.Name);
            Assert.AreEqual("luminance", property.Value);
        }
    }
}
