namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Values;
    using NUnit.Framework;

    [TestFixture]
    public class UnitConversionTests
    {
        [Test]
        public void LengthParseCorrectPxValue()
        {
            var s = "12px";
            var v = default(Length);
            var r = Length.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(12f, v.Value);
            Assert.AreEqual(Length.Unit.Px, v.Type);
        }

        [Test]
        public void LengthParseIncorrectValue()
        {
            var s = "123.5";
            var v = default(Length);
            var r = Length.TryParse(s, out v);
            Assert.IsFalse(r);
        }

        [Test]
        public void LengthParseCorrectVwValue()
        {
            var s = "12.2vw";
            var v = default(Length);
            var r = Length.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(12.2, v.Value);
            Assert.AreEqual(Length.Unit.Vw, v.Type);
        }

        [Test]
        public void LengthToPixlesCorrectPercentWidth()
        {
            var l = new Length(50, Length.Unit.Percent);
            var renderDevice = new DefaultRenderDevice{ ViewPortWidth = 500};
            Assert.AreEqual(250, l.ToPixel(renderDevice, true));
        }

        [Test]
        public void LengthToPixlesCorrectPercentHeight()
        {
            var l = new Length(25, Length.Unit.Percent);
            var renderDevice = new DefaultRenderDevice{ ViewPortHeight = 600};
            Assert.AreEqual(150, l.ToPixel(renderDevice, false));
        }

        [Test]
        public void LengthToPixlesCorrectRem()
        {
            var l = new Length(25, Length.Unit.Rem);
            var renderDevice = new DefaultRenderDevice{ FontSize = 10};
            Assert.AreEqual(250, l.ToPixel(renderDevice));
        }

        [Test]
        public void LengthToPixlesCorrectEm()
        {
            var l = new Length(10, Length.Unit.Em);
            var renderDevice = new DefaultRenderDevice{ FontSize = 10};
            Assert.AreEqual(100, l.ToPixel(renderDevice));
        }

        [Test]
        public void LengthToPixlesCorrectVh()
        {
            var l = new Length(10, Length.Unit.Vh);
            var renderDevice = new DefaultRenderDevice{ ViewPortHeight = 1000};
            Assert.AreEqual(100, l.ToPixel(renderDevice));
        }

        [Test]
        public void LengthToPixlesCorrectVw()
        {
            var l = new Length(20, Length.Unit.Vw);
            var renderDevice = new DefaultRenderDevice{ ViewPortHeight = 1000};
            Assert.AreEqual(200, l.ToPixel(renderDevice));
        }

        [Test]
        public void LengthToPixlesCorrectVmax()
        {
            var l = new Length(20, Length.Unit.Vmax);
            var renderDevice = new DefaultRenderDevice
            {
                ViewPortHeight = 1000,
                ViewPortWidth = 500
            };
            Assert.AreEqual(200, l.ToPixel(renderDevice));
        }

        [Test]
        public void LengthToPixlesCorrectVmin()
        {
            var l = new Length(20, Length.Unit.Vmin);
            var renderDevice = new DefaultRenderDevice
            {
                ViewPortHeight = 1000,
                ViewPortWidth = 500
            };
            Assert.AreEqual(100, l.ToPixel(renderDevice));
        }

        [Test]
        public void AngleParseCorrectDegValue()
        {
            var s = "1.35e2deg";
            var v = default(Angle);
            var r = Angle.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(135f, v.Value);
            Assert.AreEqual(Angle.Unit.Deg, v.Type);
        }

        [Test]
        public void ResolutionParseCorrectDpiValue()
        {
            var s = "-24.0dpi";
            var v = default(Resolution);
            var r = Resolution.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(-24f, v.Value);
            Assert.AreEqual(Resolution.Unit.Dpi, v.Type);
        }

        [Test]
        public void FrequencyParseCorrectKhzValue()
        {
            var s = "17.123khz";
            var v = default(Frequency);
            var r = Frequency.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(17.123, v.Value);
            Assert.AreEqual(Frequency.Unit.Khz, v.Type);
        }

        [Test]
        public void TimeParseCorrectSecondsValue()
        {
            var s = "0s";
            var v = default(Time);
            var r = Time.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(0f, v.Value);
            Assert.AreEqual(Time.Unit.S, v.Type);
        }

        [Test]
        public void AngleParseIncorrectValue()
        {
            var s = "123.deg";
            var v = default(Angle);
            var r = Angle.TryParse(s, out v);
            Assert.IsFalse(r);
        }
    }
}
