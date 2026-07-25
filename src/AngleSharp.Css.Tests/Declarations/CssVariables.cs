#nullable disable
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
            var variable = property.RawValue as CssReferenceValue;
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
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.AreEqual(1, variable.References.Length);
            Assert.AreEqual("--my-bar", variable.References[0].VariableName);
            Assert.AreEqual("24px", variable.References[0].DefaultValue.CssText);
        }

        [Test]
        public void LegitVariableReferenceWithFallbackContainingComma()
        {
            var source = @"border-top-color: var(--color, red, blue)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.AreEqual(1, variable.References.Length);
            Assert.AreEqual("--color", variable.References[0].VariableName);
            Assert.AreEqual("red, blue", variable.References[0].DefaultValue.CssText);
        }

        [Test]
        public void LegitSingleVariableReferenceInBackgroundShorthand()
        {
            var source = @"background: var(--foo)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;

            if (variable is not null)
            {
                Assert.AreEqual(1, variable.References.Length);
                Assert.AreEqual("--foo", variable.References[0].VariableName);
                Assert.IsNull(variable.References[0].DefaultValue);
            }
            else
            {
                Assert.AreEqual("var(--foo)", property.Value);
            }
        }

        [Test]
        public void LegitMixedVariableReferenceInBackgroundShorthand()
        {
            var source = @"background: url('http://bit.ly/2FiPrRA') 0 100%/340px no-repeat, var(--primary-color);";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;

            if (variable is not null)
            {
                Assert.AreEqual(1, variable.References.Length);
                Assert.AreEqual("--primary-color", variable.References[0].VariableName);
                Assert.IsNull(variable.References[0].DefaultValue);
            }
            else
            {
                Assert.IsTrue(property.Value.Contains("var(--primary-color)"));
            }
        }

        [Test]
        public void LegitMultipleVariableReferenceInBorderShorthand()
        {
            var source = @"border: var(--width) solid var(--color, black)";
            var property = ParseDeclaration(source);
            Assert.IsNotNull(property);
            var variable = property.RawValue as CssReferenceValue;
            Assert.IsNotNull(variable);
            Assert.AreEqual(2, variable.References.Length);
            Assert.AreEqual("--width", variable.References[0].VariableName);
            Assert.IsNull(variable.References[0].DefaultValue);
            Assert.AreEqual("--color", variable.References[1].VariableName);
            Assert.AreEqual("black", variable.References[1].DefaultValue.CssText);
        }

        [Test]
        public void BorderBottomShorthandWithVariableKeepsLonghands()
        {
            var style = ParseDeclarations(@"border-bottom: 1px solid var(--pale-grey)");

            Assert.AreEqual("1px", style.GetProperty("border-bottom-width").Value);
            Assert.AreEqual("solid", style.GetProperty("border-bottom-style").Value);
            Assert.AreEqual("var(--pale-grey)", style.GetProperty("border-bottom-color").Value);
        }

        [Test]
        public void MarginShorthandWithVariableKeepsLonghands()
        {
            var style = ParseDeclarations(@"margin: 1rem var(--space) 2rem 3rem");

            Assert.AreEqual("1rem", style.GetProperty("margin-top").Value);
            Assert.AreEqual("var(--space)", style.GetProperty("margin-right").Value);
            Assert.AreEqual("2rem", style.GetProperty("margin-bottom").Value);
            Assert.AreEqual("3rem", style.GetProperty("margin-left").Value);
        }

        [Test]
        public void PaddingShorthandWithVariableKeepsLonghands()
        {
            var style = ParseDeclarations(@"padding: 10px var(--pad-x)");

            Assert.AreEqual("10px", style.GetProperty("padding-top").Value);
            Assert.AreEqual("var(--pad-x)", style.GetProperty("padding-right").Value);
            Assert.AreEqual("10px", style.GetProperty("padding-bottom").Value);
            Assert.AreEqual("var(--pad-x)", style.GetProperty("padding-left").Value);
        }

        [Test]
        public void OutlineShorthandWithVariableKeepsLonghands()
        {
            var style = ParseDeclarations(@"outline: 2px solid var(--outline-color)");

            Assert.AreEqual("2px", style.GetProperty("outline-width").Value);
            Assert.AreEqual("solid", style.GetProperty("outline-style").Value);
            Assert.AreEqual("var(--outline-color)", style.GetProperty("outline-color").Value);
        }

        [Test]
        public void FontShorthandWithVariableKeepsLonghands()
        {
            var property = ParseDeclaration(@"font: italic 16px/1.5 var(--font-family)");

            Assert.IsNotNull(property);
            Assert.IsTrue(property.Value.Contains("var(--font-family)"));
        }

        [Test]
        public void BackgroundShorthandWithVariableKeepsLonghands()
        {
            var property = ParseDeclaration(@"background: url('a.png') no-repeat 10px 20px var(--bg-color)");

            Assert.IsNotNull(property);
            Assert.IsTrue(property.Value.Contains("var(--bg-color)"));
        }

        [Test]
        public void GridGapShorthandWithVariableKeepsLonghands()
        {
            var style = ParseDeclarations(@"gap: 12px var(--gap-x)");

            Assert.AreEqual("12px", style.GetProperty("column-gap").Value);
            Assert.AreEqual("var(--gap-x)", style.GetProperty("row-gap").Value);
        }

        [Test]
        public void GridAreaShorthandWithVariableKeepsLonghands()
        {
            var style = ParseDeclarations(@"grid-area: 2 / var(--start-col) / span 3 / 6");

            Assert.AreEqual("2", style.GetProperty("grid-row-start").Value);
            Assert.AreEqual("var(--start-col)", style.GetProperty("grid-column-start").Value);
            Assert.AreEqual("span 3", style.GetProperty("grid-row-end").Value);
            Assert.AreEqual("6", style.GetProperty("grid-column-end").Value);
        }

        [Test]
        public void FlexShorthandWithVariableKeepsLonghands()
        {
            var style = ParseDeclarations(@"flex: 1 0 var(--basis)");

            Assert.AreEqual("1", style.GetProperty("flex-grow").Value);
            Assert.AreEqual("0", style.GetProperty("flex-shrink").Value);
            Assert.AreEqual("var(--basis)", style.GetProperty("flex-basis").Value);
        }

        [Test]
        public void ColumnsShorthandWithVariableKeepsLonghands()
        {
            var property = ParseDeclaration(@"columns: var(--col-width) 3");

            Assert.IsNotNull(property);
            Assert.IsTrue(property.Value.Contains("var(--col-width)"));
        }

        [Test]
        public void TransformWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"transform: var(--transform-value)");

            Assert.IsNotNull(property);
            Assert.AreEqual("var(--transform-value)", property.Value);
        }

        [Test]
        public void WidthWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"width: var(--width-value)");

            Assert.IsNotNull(property);
            Assert.AreEqual("var(--width-value)", property.Value);
        }

        [Test]
        public void HeightWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"height: var(--height-value)");

            Assert.IsNotNull(property);
            Assert.AreEqual("var(--height-value)", property.Value);
        }

        [Test]
        public void BorderRadiusWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"border-radius: var(--radius)");

            Assert.IsNotNull(property);
            Assert.AreEqual("var(--radius)", property.Value);
        }

        [Test]
        public void FontSizeWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"font-size: var(--font-size)");

            Assert.IsNotNull(property);
            Assert.AreEqual("var(--font-size)", property.Value);
        }

        [Test]
        public void FontWeightWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"font-weight: var(--font-weight)");

            Assert.IsNotNull(property);
            Assert.AreEqual("var(--font-weight)", property.Value);
        }

        [Test]
        public void OpacityWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"opacity: var(--opacity)");

            Assert.IsNotNull(property);
            Assert.AreEqual("var(--opacity)", property.Value);
        }

        [Test]
        public void AnimationWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"animation: var(--anim-name) 2s linear");

            Assert.IsNotNull(property);
            Assert.IsTrue(property.Value.Contains("var(--anim-name)"));
        }

        [Test]
        public void TransitionWithVariableIsPreserved()
        {
            var property = ParseDeclaration(@"transition: opacity var(--duration) ease-in");

            Assert.IsNotNull(property);
            Assert.IsTrue(property.Value.Contains("var(--duration)"));
        }
    }
}
