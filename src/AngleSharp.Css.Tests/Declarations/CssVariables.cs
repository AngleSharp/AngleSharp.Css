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
            var variable = property.RawValue as VarReferences;
            Assert.IsNotNull(variable);
            Assert.AreEqual(1, variable.References.Length);
            Assert.AreEqual("--foo", variable.References[0].VariableName);
            Assert.IsNull(variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitVariableReferenceWithFallback()
        {
            var source = @"padding-bottom: var(--my-bar, 24px)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReferences;
            Assert.IsNotNull(variable);
            Assert.AreEqual(1, variable.References.Length);
            Assert.AreEqual("--my-bar", variable.References[0].VariableName);
            Assert.AreEqual("24px", variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitVariableReferenceWithFallbackContainingComma()
        {
            var source = @"border-top-color: var(--color, red, blue)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReferences;
            Assert.IsNotNull(variable);
            Assert.AreEqual(1, variable.References.Length);
            Assert.AreEqual("--color", variable.References[0].VariableName);
            Assert.AreEqual("red, blue", variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitSingleVariableReferenceInBackgroundShorthand()
        {
            var source = @"background: var(--foo)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReferences;
            Assert.IsNotNull(variable);
            Assert.AreEqual(1, variable.References.Length);
            Assert.AreEqual("--foo", variable.References[0].VariableName);
            Assert.IsNull(variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitMixedVariableReferenceInBackgroundShorthand()
        {
            var source = @"background: url('http://bit.ly/2FiPrRA') 0 100%/340px no-repeat, var(--primary-color);";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReferences;
            Assert.IsNotNull(variable);
            Assert.AreEqual(1, variable.References.Length);
            Assert.AreEqual("--primary-color", variable.References[0].VariableName);
            Assert.IsNull(variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitMultipleVariableReferenceInBorderShorthand()
        {
            var source = @"border: var(--width) solid var(--color, black)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as VarReferences;
            Assert.IsNotNull(variable);
            Assert.AreEqual(2, variable.References.Length);
            Assert.AreEqual("--width", variable.References[0].VariableName);
            Assert.IsNull(variable.References[0].DefaultValue);
            Assert.AreEqual("--color", variable.References[1].VariableName);
            Assert.AreEqual("black", variable.References[1].DefaultValue);
        }
    }
}
