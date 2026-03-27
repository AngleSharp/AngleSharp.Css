namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Values;
    using NUnit.Framework;

    [TestFixture]
    public class CssColorValueTests
    {
        [Test]
        public void CssColorValueInvalidHexDigitString()
        {
            var color = "BCDEFG";
            var result = CssColorValue.TryFromHex(color, out var hc);
            Assert.That(result, Is.False);
        }

        [Test]
        public void CssColorValueValidFourLetterString()
        {
            var color = "abcd";
            var result = CssColorValue.TryFromHex(color, out var hc);
            Assert.That(hc, Is.EqualTo(new CssColorValue(170, 187, 204, 221)));
            Assert.That(result, Is.True);
        }

        [Test]
        public void CssColorValueInvalidLengthString()
        {
            var color = "abcde";
            var result = CssColorValue.TryFromHex(color, out var hc);
            Assert.That(result, Is.False);
        }

        [Test]
        public void CssColorValueValidLengthShortString()
        {
            var color = "fff";
            var result = CssColorValue.TryFromHex(color, out var hc);
            Assert.That(result, Is.True);
        }

        [Test]
        public void CssColorValueValidLengthLongString()
        {
            var color = "fffabc";
            var result = CssColorValue.TryFromHex(color, out var hc);
            Assert.That(result, Is.True);
        }

        [Test]
        public void CssColorValueWhiteShortString()
        {
            var color = "fff";
            var result = CssColorValue.FromHex(color);
            Assert.That(result, Is.EqualTo(CssColorValue.FromRgb(255, 255, 255)));
        }

        [Test]
        public void CssColorValueRedShortString()
        {
            var color = "f00";
            var result = CssColorValue.FromHex(color);
            Assert.That(result, Is.EqualTo(CssColorValue.FromRgb(255, 0, 0)));
        }

        [Test]
        public void CssColorValueFromRedName()
        {
            var color = "red";
            var result = CssColorValue.FromName(color);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result, Is.EqualTo(CssColorValue.Red));
        }

        [Test]
        public void CssColorValueFromWhiteName()
        {
            var color = "white";
            var result = CssColorValue.FromName(color);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result, Is.EqualTo(CssColorValue.White));
        }

        [Test]
        public void CssColorValueFromUnknownName()
        {
            var color = "bla";
            var result = CssColorValue.FromName(color);
            Assert.That(result.HasValue, Is.False);
        }

        [Test]
        public void CssColorValueMixedLongString()
        {
            var color = "facc36";
            var result = CssColorValue.FromHex(color);
            Assert.That(result, Is.EqualTo(CssColorValue.FromRgb(250, 204, 54)));
        }

        [Test]
        public void CssColorValueMixedEightDigitLongStringTransparent()
        {
            var color = "facc3600";
            var result = CssColorValue.FromHex(color);
            Assert.That(result, Is.EqualTo(CssColorValue.FromRgba(250, 204, 54, 0)));
        }

        [Test]
        public void CssColorValueMixedEightDigitLongStringOpaque()
        {
            var color = "facc36ff";
            var result = CssColorValue.FromHex(color);
            Assert.That(result, Is.EqualTo(CssColorValue.FromRgba(250, 204, 54, 1)));
        }

        [Test]
        public void CssColorValueMixBlackOnWhite50Percent()
        {
            var color1 = CssColorValue.Black;
            var color2 = CssColorValue.White;
            var mix = CssColorValue.Mix(0.5, color1, color2);
            Assert.That(mix, Is.EqualTo(CssColorValue.FromRgb(127, 127, 127)));
        }

        [Test]
        public void CssColorValueMixRedOnWhite75Percent()
        {
            var color1 = CssColorValue.Red;
            var color2 = CssColorValue.White;
            var mix = CssColorValue.Mix(0.75, color1, color2);
            Assert.That(mix, Is.EqualTo(CssColorValue.FromRgb(255, 63, 63)));
        }

        [Test]
        public void CssColorValueMixBlueOnWhite10Percent()
        {
            var color1 = CssColorValue.Blue;
            var color2 = CssColorValue.White;
            var mix = CssColorValue.Mix(0.1, color1, color2);
            Assert.That(mix, Is.EqualTo(CssColorValue.FromRgb(229, 229, 255)));
        }

        [Test]
        public void CssColorValueMixGreenOnRed30Percent()
        {
            var color1 = CssColorValue.PureGreen;
            var color2 = CssColorValue.Red;
            var mix = CssColorValue.Mix(0.3, color1, color2);
            Assert.That(mix, Is.EqualTo(CssColorValue.FromRgb(178, 76, 0)));
        }

        [Test]
        public void CssColorValueMixWhiteOnBlack20Percent()
        {
            var color1 = CssColorValue.White;
            var color2 = CssColorValue.Black;
            var mix = CssColorValue.Mix(0.2, color1, color2);
            Assert.That(mix, Is.EqualTo(CssColorValue.FromRgb(51, 51, 51)));
        }

        [Test]
        public void CssColorValueHslBlackMixed()
        {
            var color = CssColorValue.FromHsl(0, 1, 0);
            Assert.That(color, Is.EqualTo(CssColorValue.Black));
        }

        [Test]
        public void CssColorValueHslBlackMixed1()
        {
            var color = CssColorValue.FromHsl(0, 1, 0);
            Assert.That(color, Is.EqualTo(CssColorValue.Black));
        }

        [Test]
        public void CssColorValueHslBlackMixed2()
        {
            var color = CssColorValue.FromHsl(0.5f, 1, 0);
            Assert.That(color, Is.EqualTo(CssColorValue.Black));
        }

        [Test]
        public void CssColorValueHslRedPure()
        {
            var color = CssColorValue.FromHsl(0, 1, 0.5f);
            Assert.That(color, Is.EqualTo(CssColorValue.Red));
        }

        [Test]
        public void CssColorValueHslGreenPure()
        {
            var color = CssColorValue.FromHsl(1f / 3f, 1, 0.5f);
            Assert.That(color, Is.EqualTo(CssColorValue.PureGreen));
        }

        [Test]
        public void CssColorValueHslBluePure()
        {
            var color = CssColorValue.FromHsl(2f / 3f, 1, 0.5f);
            Assert.That(color, Is.EqualTo(CssColorValue.Blue));
        }

        [Test]
        public void CssColorValueHslBlackPure()
        {
            var color = CssColorValue.FromHsl(0, 0, 0);
            Assert.That(color, Is.EqualTo(CssColorValue.Black));
        }

        [Test]
        public void CssColorValueHslMagentaPure()
        {
            var color = CssColorValue.FromHsl(300f / 360f, 1, 0.5f);
            Assert.That(color, Is.EqualTo(CssColorValue.Magenta));
        }

        [Test]
        public void CssColorValueHslYellowGreenMixed()
        {
            var color = CssColorValue.FromHsl(1f / 4f, 0.75f, 0.63f);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(161, 232, 90)));
        }

        [Test]
        public void CssColorValueHslGrayBlueMixed()
        {
            var color = CssColorValue.FromHsl(210f / 360f, 0.25f, 0.25f);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(48, 64, 80)));
        }

        [Test]
        public void CssColorValueFlexHexOneLetter()
        {
            var color = CssColorValue.FromFlexHex("F");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0xf, 0x0, 0x0)));
        }

        [Test]
        public void CssColorValueFlexHexTwoLetters()
        {
            var color = CssColorValue.FromFlexHex("0F");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0x0, 0xf, 0x0)));
        }

        [Test]
        public void CssColorValueFlexHexFourLetters()
        {
            var color = CssColorValue.FromFlexHex("0F0F");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0xf, 0xf, 0x0)));
        }

        [Test]
        public void CssColorValueFlexHexSevenLetters()
        {
            var color = CssColorValue.FromFlexHex("0F0F0F0");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0xf, 0xf0, 0x0)));
        }

        [Test]
        public void CssColorValueFlexHexFifteenLetters()
        {
            var color = CssColorValue.FromFlexHex("1234567890ABCDE");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0x12, 0x67, 0xab)));
        }

        [Test]
        public void CssColorValueFlexHexExtremelyLong()
        {
            var color = CssColorValue.FromFlexHex("1234567890ABCDE1234567890ABCDE");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0x34, 0xcd, 0x89)));
        }

        [Test]
        public void CssColorValueFlexHexRandomString()
        {
            var color = CssColorValue.FromFlexHex("6db6ec49efd278cd0bc92d1e5e072d68");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0x6e, 0xcd, 0xe0)));
        }

        [Test]
        public void CssColorValueFlexHexSixLettersInvalid()
        {
            var color = CssColorValue.FromFlexHex("zqbttv");
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(0x0, 0xb0, 0x0)));
        }

        [Test]
        public void CssColorValueFromGraySimple()
        {
            var color = CssColorValue.FromGray(25);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(25, 25, 25)));
        }

        [Test]
        public void CssColorValueFromGrayWithAlpha()
        {
            var color = CssColorValue.FromGray(25, 0.5f);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgba(25, 25, 25, 0.5f)));
        }

        [Test]
        public void CssColorValueFromGrayPercent()
        {
            var color = CssColorValue.FromGray(0.5f, 0.5f);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgba(128, 128, 128, 0.5f)));
        }

        [Test]
        public void CssColorValueFromHwbRed()
        {
            var color = CssColorValue.FromHwb(0f, 0.2f, 0.2f);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(204, 51, 51)));
        }

        [Test]
        public void CssColorValueFromHwbGreen()
        {
            var color = CssColorValue.FromHwb(1f / 3f, 0.2f, 0.6f);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgb(51, 102, 51)));
        }

        [Test]
        public void CssColorValueFromHwbMagentaTransparent()
        {
            var color = CssColorValue.FromHwba(5f / 6f, 0.4f, 0.2f, 0.5f);
            Assert.That(color, Is.EqualTo(CssColorValue.FromRgba(204, 102, 204, 0.5f)));
        }
    }
}
