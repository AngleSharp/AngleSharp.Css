namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssVariablesTests
    {
        [Test]
        public void RootVariableCorrectlyIdentified()
        {
            var source = @":root { --my-variable: black; }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.AreEqual(":root", style.SelectorText);
            Assert.AreEqual(1, style.Style.Length);
            var propertyName = style.Style[0];
            var propertyValue = style.Style[propertyName];
            Assert.AreEqual("--my-variable", propertyName);
            Assert.AreEqual("black", propertyValue);
        }

        [Test]
        public void RootVariableWithInvalidIdentifier()
        {
            var source = @":root { --my-vari@able: black; }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.AreEqual(":root", style.SelectorText);
            Assert.AreEqual(0, style.Style.Length);
        }
    }
}
