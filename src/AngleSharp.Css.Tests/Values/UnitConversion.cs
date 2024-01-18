namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Values;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class UnitConversionTests
    {
        [Test]
        public void LengthParseCorrectPxValue()
        {
            var s = "12px";
            var v = default(CssLengthValue);
            var r = CssLengthValue.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(12f, v.Value);
            Assert.AreEqual(CssLengthValue.Unit.Px, v.Type);
        }

        [Test]
        public void LengthParseIncorrectValue()
        {
            var s = "123.5";
            var v = default(CssLengthValue);
            var r = CssLengthValue.TryParse(s, out v);
            Assert.IsFalse(r);
        }

        [Test]
        public void LengthParseCorrectVwValue()
        {
            var s = "12.2vw";
            var v = default(CssLengthValue);
            var r = CssLengthValue.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(12.2, v.Value);
            Assert.AreEqual(CssLengthValue.Unit.Vw, v.Type);
        }

        [Test]
        public void LengthToPixelsPercentThrowsOnInvalidRenderDimensions()
        {
            var l = new CssLengthValue(50, CssLengthValue.Unit.Percent);
            var renderDevice = new DefaultRenderDevice();
            Assert.Throws<ArgumentException>(() => l.ToPixel(renderDevice, RenderMode.Undefined));
            Assert.Throws<ArgumentException>(() => l.ToPixel(null, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPixelsEmThrowsOnInvalidRenderDimensions()
        {
            var l = new CssLengthValue(50, CssLengthValue.Unit.Em);
            var renderDevice = new DefaultRenderDevice { FontSize = 0 };
            Assert.Throws<ArgumentException>(() => l.ToPixel(renderDevice, RenderMode.Undefined));
            Assert.Throws<ArgumentException>(() => l.ToPixel(null, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPixelsCorrectPercentWidth()
        {
            var l = new CssLengthValue(50, CssLengthValue.Unit.Percent);
            var renderDevice = new DefaultRenderDevice {ViewPortWidth = 500};
            Assert.AreEqual(250, l.ToPixel(renderDevice, RenderMode.Horizontal));
        }

        [Test]
        public void LengthToPixelsCorrectPercentHeight()
        {
            var l = new CssLengthValue(25, CssLengthValue.Unit.Percent);
            var renderDevice = new DefaultRenderDevice {ViewPortHeight = 600};
            Assert.AreEqual(150, l.ToPixel(renderDevice, RenderMode.Vertical));
        }

        [Test]
        public void LengthToPixelsCorrectRem()
        {
            var l = new CssLengthValue(25, CssLengthValue.Unit.Rem);
            var renderDevice = new DefaultRenderDevice {FontSize = 10};
            Assert.AreEqual(250, l.ToPixel(renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPixelsCorrectEm()
        {
            var l = new CssLengthValue(10, CssLengthValue.Unit.Em);
            var renderDevice = new DefaultRenderDevice {FontSize = 10};
            Assert.AreEqual(100, l.ToPixel(renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPixelsCorrectVh()
        {
            var l = new CssLengthValue(10, CssLengthValue.Unit.Vh);
            var renderDevice = new DefaultRenderDevice {ViewPortHeight = 1000};
            Assert.AreEqual(100, l.ToPixel(renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPixelsCorrectVw()
        {
            var l = new CssLengthValue(20, CssLengthValue.Unit.Vw);
            var renderDevice = new DefaultRenderDevice {ViewPortWidth = 1000};
            Assert.AreEqual(200, l.ToPixel(renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPixelsCorrectVmax()
        {
            var l = new CssLengthValue(20, CssLengthValue.Unit.Vmax);
            var renderDevice = new DefaultRenderDevice
            {
                ViewPortHeight = 1000,
                ViewPortWidth = 500
            };
            Assert.AreEqual(200, l.ToPixel(renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPixelsCorrectVmin()
        {
            var l = new CssLengthValue(20, CssLengthValue.Unit.Vmin);
            var renderDevice = new DefaultRenderDevice
            {
                ViewPortHeight = 1000,
                ViewPortWidth = 500
            };
            Assert.AreEqual(100, l.ToPixel(renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToPercentCorrectWidth()
        {
            var l = new CssLengthValue(100, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice {ViewPortWidth = 500};
            Assert.AreEqual(20, l.To(CssLengthValue.Unit.Percent, renderDevice, RenderMode.Horizontal));
        }

        [Test]
        public void LengthToPercentCorrectHeight()
        {
            var l = new CssLengthValue(100, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice {ViewPortHeight = 1000};
            Assert.AreEqual(10, l.To(CssLengthValue.Unit.Percent, renderDevice, RenderMode.Vertical));
        }

        [Test]
        public void LengthToRemCorrectValue()
        {
            var l = new CssLengthValue(100, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice {FontSize = 16};
            Assert.AreEqual(6.25d, l.To(CssLengthValue.Unit.Rem, renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToEmCorrectValue()
        {
            var l = new CssLengthValue(1600, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice {FontSize = 16};
            Assert.AreEqual(100, l.To(CssLengthValue.Unit.Em, renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToVhCorrectValue()
        {
            var l = new CssLengthValue(100, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice {ViewPortHeight = 1000};
            Assert.AreEqual(10, l.To(CssLengthValue.Unit.Vh, renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToVwCorrectValue()
        {
            var l = new CssLengthValue(50, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice {ViewPortWidth = 1000};
            Assert.AreEqual(5, l.To(CssLengthValue.Unit.Vw, renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToVmaxCorrectValue()
        {
            var l = new CssLengthValue(50, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice
            {
                ViewPortWidth = 1000,
                ViewPortHeight = 500
            };
            Assert.AreEqual(5, l.To(CssLengthValue.Unit.Vmax, renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void LengthToVminCorrectValue()
        {
            var l = new CssLengthValue(50, CssLengthValue.Unit.Px);
            var renderDevice = new DefaultRenderDevice
            {
                ViewPortWidth = 1000,
                ViewPortHeight = 500
            };
            Assert.AreEqual(10, l.To(CssLengthValue.Unit.Vmin, renderDevice, RenderMode.Undefined));
        }

        [Test]
        public void AngleParseCorrectDegValue()
        {
            var s = "1.35e2deg";
            var v = default(CssAngleValue);
            var r = CssAngleValue.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(135f, v.Value);
            Assert.AreEqual(CssAngleValue.Unit.Deg, v.Type);
        }

        [Test]
        public void ResolutionParseCorrectDpiValue()
        {
            var s = "-24.0dpi";
            var v = default(CssResolutionValue);
            var r = CssResolutionValue.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(-24f, v.Value);
            Assert.AreEqual(CssResolutionValue.Unit.Dpi, v.Type);
        }

        [Test]
        public void FrequencyParseCorrectKhzValue()
        {
            var s = "17.123khz";
            var v = default(CssFrequencyValue);
            var r = CssFrequencyValue.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(17.123, v.Value);
            Assert.AreEqual(CssFrequencyValue.Unit.Khz, v.Type);
        }

        [Test]
        public void TimeParseCorrectSecondsValue()
        {
            var s = "0s";
            var v = default(CssTimeValue);
            var r = CssTimeValue.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(0f, v.Value);
            Assert.AreEqual(CssTimeValue.Unit.S, v.Type);
        }

        [Test]
        public void AngleParseIncorrectValue()
        {
            var s = "123.deg";
            var v = default(CssAngleValue);
            var r = CssAngleValue.TryParse(s, out v);
            Assert.IsFalse(r);
        }
    }
}