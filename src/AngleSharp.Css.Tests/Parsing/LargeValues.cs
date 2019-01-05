namespace AngleSharp.Css.Tests.Parsing
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class LargeValues
    {
        [Test]
        public void LargePositiveZIndexShouldNotErrorAndBeCapped()
        {
            var source = @"div#preview-toolbar {
    z-index: 9999999999999
}";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("2147483647", style.Style.GetZIndex());
        }

        [Test]
        public void LargeNegativeZIndexShouldNotErrorAndBeCapped()
        {
            var source = @"div#preview-toolbar {
    z-index: -9999999999999
}";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("-2147483648", style.Style.GetZIndex());
        }
    }
}
