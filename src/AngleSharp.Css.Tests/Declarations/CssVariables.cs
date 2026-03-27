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
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.That(style.SelectorText, Is.EqualTo(":root"));
            Assert.That(style.Style.Length, Is.EqualTo(1));
            var propertyName = style.Style[0];
            var propertyValue = style.Style[propertyName];
            Assert.That(propertyName, Is.EqualTo("--my-variable"));
            Assert.That(propertyValue, Is.EqualTo("black"));
        }

        [Test]
        public void RootVariableWithInvalidIdentifier()
        {
            var source = @":root { --my-vari@able: black; }";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.That(style.SelectorText, Is.EqualTo(":root"));
            Assert.That(style.Style.Length, Is.EqualTo(0));
        }

        [Test]
        public void LegitVariableReferenceWithoutFallback()
        {
            var source = @"padding-bottom: var(--foo)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.That(variable.References.Length, Is.EqualTo(1));
            Assert.That(variable.References[0].VariableName, Is.EqualTo("--foo"));
            Assert.IsNull(variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitVariableReferenceWithFallback()
        {
            var source = @"padding-bottom: var(--my-bar, 24px)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.That(variable.References.Length, Is.EqualTo(1));
            Assert.That(variable.References[0].VariableName, Is.EqualTo("--my-bar"));
            Assert.That(variable.References[0].DefaultValue.CssText, Is.EqualTo("24px"));
        }

        [Test]
        public void LegitVariableReferenceWithFallbackContainingComma()
        {
            var source = @"border-top-color: var(--color, red, blue)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.That(variable.References.Length, Is.EqualTo(1));
            Assert.That(variable.References[0].VariableName, Is.EqualTo("--color"));
            Assert.That(variable.References[0].DefaultValue.CssText, Is.EqualTo("red, blue"));
        }

        [Test]
        public void LegitSingleVariableReferenceInBackgroundShorthand()
        {
            var source = @"background: var(--foo)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.That(variable.References.Length, Is.EqualTo(1));
            Assert.That(variable.References[0].VariableName, Is.EqualTo("--foo"));
            Assert.IsNull(variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitMixedVariableReferenceInBackgroundShorthand()
        {
            var source = @"background: url('http://bit.ly/2FiPrRA') 0 100%/340px no-repeat, var(--primary-color);";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.That(variable.References.Length, Is.EqualTo(1));
            Assert.That(variable.References[0].VariableName, Is.EqualTo("--primary-color"));
            Assert.IsNull(variable.References[0].DefaultValue);
        }

        [Test]
        public void LegitMultipleVariableReferenceInBorderShorthand()
        {
            var source = @"border: var(--width) solid var(--color, black)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.That(variable.References.Length, Is.EqualTo(2));
            Assert.That(variable.References[0].VariableName, Is.EqualTo("--width"));
            Assert.IsNull(variable.References[0].DefaultValue);
            Assert.That(variable.References[1].VariableName, Is.EqualTo("--color"));
            Assert.That(variable.References[1].DefaultValue.CssText, Is.EqualTo("black"));
        }
    }
}
