#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    /// <summary>
    /// Tests for CSS shape properties.
    /// </summary>
    [TestFixture]
    public class CssShapePropertiesTests
    {
        private IHtmlDocument _document;

        [SetUp]
        public void Setup()
        {
            _document = ParseDocument("");
        }

        [Test]
        public void ShapeOutsideNoneInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-outside"));
        }

        [Test]
        public void ShapeOutsideNone()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-outside", "none");
            Assert.AreEqual("none", element.GetStyle().GetPropertyValue("shape-outside"));
        }

        [Test]
        public void ShapeOutsideInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-outside", "circle(50%)");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-outside"));
        }

        [Test]
        public void ShapeMarginInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-margin"));
        }

        [Test]
        public void ShapeMarginZero()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-margin", "0");
            Assert.AreEqual("0", element.GetStyle().GetPropertyValue("shape-margin"));
        }

        [Test]
        public void ShapeMarginLength()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-margin", "10px");
            Assert.AreEqual("10px", element.GetStyle().GetPropertyValue("shape-margin"));
        }

        [Test]
        public void ShapeMarginPercentage()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-margin", "5%");
            Assert.AreEqual("5%", element.GetStyle().GetPropertyValue("shape-margin"));
        }

        [Test]
        public void ShapeMarginInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-margin", "invalid");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-margin"));
        }

        [Test]
        public void ShapeImageThresholdInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-image-threshold"));
        }

        [Test]
        public void ShapeImageThresholdAuto()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-image-threshold", "auto");
            Assert.AreEqual("auto", element.GetStyle().GetPropertyValue("shape-image-threshold"));
        }

        [Test]
        public void ShapeImageThresholdZero()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-image-threshold", "0");
            Assert.AreEqual("0", element.GetStyle().GetPropertyValue("shape-image-threshold"));
        }

        [Test]
        public void ShapeImageThresholdHalf()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-image-threshold", "0.5");
            Assert.AreEqual("0.5", element.GetStyle().GetPropertyValue("shape-image-threshold"));
        }

        [Test]
        public void ShapeImageThresholdOne()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-image-threshold", "1");
            Assert.AreEqual("1", element.GetStyle().GetPropertyValue("shape-image-threshold"));
        }

        [Test]
        public void ShapeImageThresholdInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-image-threshold", "invalid");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-image-threshold"));
        }

        [Test]
        public void ShapeRenderingAutoInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-rendering"));
        }

        [Test]
        public void ShapeRenderingAuto()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-rendering", "auto");
            Assert.AreEqual("auto", element.GetStyle().GetPropertyValue("shape-rendering"));
        }

        [Test]
        public void ShapeRenderingOptimizeSpeed()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-rendering", "optimize-speed");
            Assert.AreEqual("optimize-speed", element.GetStyle().GetPropertyValue("shape-rendering"));
        }

        [Test]
        public void ShapeRenderingCrispEdges()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-rendering", "crisp-edges");
            Assert.AreEqual("crisp-edges", element.GetStyle().GetPropertyValue("shape-rendering"));
        }

        [Test]
        public void ShapeRenderingGeometricPrecision()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-rendering", "geometric-precision");
            Assert.AreEqual("geometric-precision", element.GetStyle().GetPropertyValue("shape-rendering"));
        }

        [Test]
        public void ShapeRenderingInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("shape-rendering", "invalid");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("shape-rendering"));
        }
    }
}
