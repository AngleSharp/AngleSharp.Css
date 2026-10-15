namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Values;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBoxShadowProperty
    {
        [TestCase("5px")]
        [TestCase("-5px")]
        public void ZeroBlurRetainsSpreadOnRoundTrip(String spread)
        {
            var property = ParseDeclaration("box-shadow: 0 0 0 " + spread + " red");
            var reparsed = ParseDeclaration(property.CssText);

            Assert.IsTrue(reparsed.HasValue);

            var shadows = (CssListValue)reparsed.RawValue;
            var shadow = (CssShadowValue)shadows.Items[0];

            Assert.AreEqual(CssLengthValue.Zero, shadow.BlurRadius);
            Assert.AreEqual(spread, shadow.SpreadRadius.CssText);
        }
    }
}
