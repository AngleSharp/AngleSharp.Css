namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
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

        [Test]
        public void LegitVariableReferenceWithoutFallback()
        {
            var source = @"padding-bottom: var(--foo)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReference;
            Assert.IsNotNull(variable);
            Assert.AreEqual("--foo", variable.Name);
            Assert.IsNull(variable.DefaultValue);
        }

        [Test]
        public void LegitVariableReferenceWithFallback()
        {
            var source = @"padding-bottom: var(--my-bar, 24px)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReference;
            Assert.IsNotNull(variable);
            Assert.AreEqual("--my-bar", variable.Name);
            Assert.AreEqual("24px", variable.DefaultValue);
        }

        [Test]
        public void LegitVariableReferenceInBackgroundShorthand()
        {
            var source = @"background: var(--foo)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReference;
            Assert.IsNotNull(variable);
            Assert.AreEqual("--foo", variable.Name);
            Assert.IsNull(variable.DefaultValue);
        }
    }
}
