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
    /// Tests for CSS blend mode and filter properties.
    /// </summary>
    [TestFixture]
    public class CssBlendFilterPropertiesTests
    {
        private IHtmlDocument _document;

        [SetUp]
        public void Setup()
        {
            _document = ParseDocument("");
        }

        [Test]
        public void MixBlendModeNormalInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeMultiply()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "multiply");
            Assert.AreEqual("multiply", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeScreen()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "screen");
            Assert.AreEqual("screen", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeOverlay()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "overlay");
            Assert.AreEqual("overlay", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeDarken()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "darken");
            Assert.AreEqual("darken", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeLighten()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "lighten");
            Assert.AreEqual("lighten", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeColorDodge()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "color-dodge");
            Assert.AreEqual("color-dodge", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeColorBurn()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "color-burn");
            Assert.AreEqual("color-burn", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeHardLight()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "hard-light");
            Assert.AreEqual("hard-light", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeSoftLight()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "soft-light");
            Assert.AreEqual("soft-light", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeDifference()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "difference");
            Assert.AreEqual("difference", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeExclusion()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "exclusion");
            Assert.AreEqual("exclusion", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeHue()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "hue");
            Assert.AreEqual("hue", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeSaturation()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "saturation");
            Assert.AreEqual("saturation", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeColor()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "color");
            Assert.AreEqual("color", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeLuminosity()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "luminosity");
            Assert.AreEqual("luminosity", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeAdd()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "add");
            Assert.AreEqual("add", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void MixBlendModeInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("mix-blend-mode", "invalid");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("mix-blend-mode"));
        }

        [Test]
        public void BackgroundBlendModeNormalInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("background-blend-mode"));
        }

        [Test]
        public void BackgroundBlendModeMultiply()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("background-blend-mode", "multiply");
            Assert.AreEqual("multiply", element.GetStyle().GetPropertyValue("background-blend-mode"));
        }

        [Test]
        public void BackgroundBlendModeScreen()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("background-blend-mode", "screen");
            Assert.AreEqual("screen", element.GetStyle().GetPropertyValue("background-blend-mode"));
        }

        [Test]
        public void BackgroundBlendModeOverlay()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("background-blend-mode", "overlay");
            Assert.AreEqual("overlay", element.GetStyle().GetPropertyValue("background-blend-mode"));
        }

        [Test]
        public void BackgroundBlendModeMultipleValues()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("background-blend-mode", "multiply, screen");
            Assert.AreEqual("multiply, screen", element.GetStyle().GetPropertyValue("background-blend-mode"));
        }

        [Test]
        public void BackgroundBlendModeInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("background-blend-mode", "invalid");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("background-blend-mode"));
        }

        [Test]
        public void BackdropFilterNoneInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("backdrop-filter"));
        }

        [Test]
        public void BackdropFilterNone()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("backdrop-filter", "none");
            Assert.AreEqual("none", element.GetStyle().GetPropertyValue("backdrop-filter"));
        }

        [Test]
        public void BackdropFilterInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("backdrop-filter", "blur(5px)");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("backdrop-filter"));
        }

        [Test]
        public void IsolationAutoInitial()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("isolation"));
        }

        [Test]
        public void IsolationAuto()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("isolation", "auto");
            Assert.AreEqual("auto", element.GetStyle().GetPropertyValue("isolation"));
        }

        [Test]
        public void IsolationIsolate()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("isolation", "isolate");
            Assert.AreEqual("isolate", element.GetStyle().GetPropertyValue("isolation"));
        }

        [Test]
        public void IsolationInvalid()
        {
            var element = _document.CreateElement("div") as IHtmlElement;
            element.GetStyle().SetProperty("isolation", "invalid");
            Assert.AreEqual("", element.GetStyle().GetPropertyValue("isolation"));
        }
    }
}
