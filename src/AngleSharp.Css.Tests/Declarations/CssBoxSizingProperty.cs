namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBoxSizingPropertyTests
    {
        [Test]
        public void TestBoxSizing()
        {
            var source = "* { box-sizing: border-box; }";
            var css = ParseStyleSheet(source);
            var text = css.Rules[0].CssText;

            var expected = "* { box-sizing: border-box }";
            Assert.AreEqual(expected, text);
        }
    }
}

