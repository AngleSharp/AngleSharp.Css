#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    /// <summary>
    /// Custom properties registered with @property (CSS Properties and Values API 1).
    /// </summary>
    [TestFixture]
    public class RegisteredCustomPropertyTests
    {
        private const String Color = "@property --c { syntax: \"<color>\"; inherits: false; initial-value: #c0ffee; }";
        private const String InheritedColor = "@property --c { syntax: \"<color>\"; inherits: true; initial-value: #c0ffee; }";
        private const String Length = "@property --len { syntax: \"<length>\"; inherits: true; initial-value: 0px; }";

        private static ICssStyleDeclaration Compute(String css, String body, String id = "target")
        {
            var document = ParseDocument($"<style>{css}</style>{body}");
            return document.GetElementById(id).ComputeCurrentStyle();
        }

        [Test]
        public void UndeclaredPropertyHasTheInitialValue()
        {
            var style = Compute(Color + " #target { color: var(--c) }", "<div id=target></div>");
            Assert.AreEqual("rgba(192, 255, 238, 1)", style.GetPropertyValue("--c"));
            Assert.AreEqual("rgba(192, 255, 238, 1)", style.GetPropertyValue("color"));
        }

        [Test]
        public void DeclaredValueIsComputed()
        {
            var style = Compute(Color + " #target { --c: red; color: var(--c) }", "<div id=target></div>");
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("--c"));
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("color"));
        }

        [Test]
        public void ValueNotMatchingTheSyntaxIsTheInitialValue()
        {
            var style = Compute(Color + " #target { --c: 10px; color: var(--c) }", "<div id=target></div>");
            Assert.AreEqual("rgba(192, 255, 238, 1)", style.GetPropertyValue("--c"));
            Assert.AreEqual("rgba(192, 255, 238, 1)", style.GetPropertyValue("color"));
        }

        [Test]
        public void PropertyThatIsNotInheritedHasTheInitialValueOnChildren()
        {
            var style = Compute(Color + " #parent { --c: red }", "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("rgba(192, 255, 238, 1)", style.GetPropertyValue("--c"));
        }

        [Test]
        public void InheritedPropertyHasTheValueOfTheParent()
        {
            var style = Compute(InheritedColor + " #parent { --c: red }", "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("--c"));
        }

        [Test]
        public void InvalidValueOfAnInheritedPropertyIsTheValueOfTheParent()
        {
            var style = Compute(InheritedColor + " #parent { --c: red } #target { --c: 10px }", "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("--c"));
        }

        [Test]
        public void CssWideKeywords()
        {
            const String body = "<div id=parent><span id=a></span><span id=b></span><span id=c></span></div>";
            const String css = Color + " #parent { --c: red } #a { --c: inherit } #b { --c: unset } #c { --c: initial }";
            var document = ParseDocument($"<style>{css}</style>{body}");

            Assert.AreEqual("rgba(255, 0, 0, 1)", document.GetElementById("a").ComputeCurrentStyle().GetPropertyValue("--c"));
            Assert.AreEqual("rgba(192, 255, 238, 1)", document.GetElementById("b").ComputeCurrentStyle().GetPropertyValue("--c"));
            Assert.AreEqual("rgba(192, 255, 238, 1)", document.GetElementById("c").ComputeCurrentStyle().GetPropertyValue("--c"));
        }

        [Test]
        public void LengthIsComputedOnTheElementAndInheritedAsComputed()
        {
            var css = Length + " #parent { font-size: 10px; --len: 2em } #target { font-size: 20px; width: var(--len) }";
            var style = Compute(css, "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("20px", style.GetPropertyValue("--len"));
            Assert.AreEqual("20px", style.GetPropertyValue("width"));
        }

        [Test]
        public void UnregisteredLengthIsInheritedAsSpecified()
        {
            var css = "#parent { font-size: 10px; --len: 2em } #target { font-size: 20px; width: var(--len) }";
            var style = Compute(css, "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("2em", style.GetPropertyValue("--len"));
            Assert.AreEqual("40px", style.GetPropertyValue("width"));
        }

        [Test]
        public void AbsoluteUnitsAreConvertedToPixels()
        {
            var style = Compute(Length + " #target { --len: 1in }", "<div id=target></div>");
            Assert.AreEqual("96px", style.GetPropertyValue("--len"));
        }

        [Test]
        public void InitialValueIsComputed()
        {
            var style = Compute("@property --len { syntax: \"<length>\"; inherits: false; initial-value: 1in; }", "<div id=target></div>");
            Assert.AreEqual("96px", style.GetPropertyValue("--len"));
        }

        [Test]
        public void PercentagesAreKept()
        {
            var css = "@property --lp { syntax: \"<length-percentage>\"; inherits: false; initial-value: 0px; } #target { --lp: 50% }";
            Assert.AreEqual("50%", Compute(css, "<div id=target></div>").GetPropertyValue("--lp"));
        }

        [Test]
        public void ListsAreComputedPerItem()
        {
            var css = "@property --l { syntax: \"<length>#\"; inherits: false; initial-value: 0px; } #target { font-size: 10px; --l: 1px, 2em }";
            Assert.AreEqual("1px, 20px", Compute(css, "<div id=target></div>").GetPropertyValue("--l"));
        }

        [Test]
        public void SubstitutedValueIsValidated()
        {
            var css = Length + " #a { --x: 5px; --len: var(--x) } #b { --len: var(--missing) } #c { --x: red; --len: var(--x) }";
            var document = ParseDocument($"<style>{css}</style><div id=a></div><div id=b></div><div id=c></div>");

            Assert.AreEqual("5px", document.GetElementById("a").ComputeCurrentStyle().GetPropertyValue("--len"));
            Assert.AreEqual("0px", document.GetElementById("b").ComputeCurrentStyle().GetPropertyValue("--len"));
            Assert.AreEqual("0px", document.GetElementById("c").ComputeCurrentStyle().GetPropertyValue("--len"));
        }

        [Test]
        public void ReferenceToARegisteredPropertySubstitutesItsComputedValue()
        {
            var css = Length + " #target { font-size: 10px; --len: 3em; --text: var(--len) }";
            Assert.AreEqual("30px", Compute(css, "<div id=target></div>").GetPropertyValue("--text"));
        }

        [Test]
        public void FontRelativePropertyUsedByFontSizeIsACycle()
        {
            // font-size behaves as unset, i.e., is inherited (CSS Properties and Values API 2.7)
            var css = Length + " #parent { font-size: 10px } #target { --len: 2em; font-size: var(--len) }";
            var style = Compute(css, "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("10px", style.GetPropertyValue("font-size"));
            Assert.AreEqual("20px", style.GetPropertyValue("--len"));
        }

        [Test]
        public void AbsolutePropertyUsedByFontSize()
        {
            var css = Length + " #parent { font-size: 10px } #target { --len: 30px; font-size: var(--len); width: 2em }";
            var style = Compute(css, "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("30px", style.GetPropertyValue("font-size"));
            Assert.AreEqual("60px", style.GetPropertyValue("width"));
        }

        [Test]
        public void InheritedFontRelativePropertyUsedByFontSize()
        {
            // the inherited value is computed already, i.e., no cycle
            var css = Length + " #parent { font-size: 10px; --len: 2em } #target { font-size: var(--len) }";
            var style = Compute(css, "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("20px", style.GetPropertyValue("font-size"));
        }

        [Test]
        public void LastValidRegistrationWins()
        {
            var css = "@property --c { syntax: \"<length>\"; inherits: false; initial-value: 1px; } " +
                "@property --c { syntax: \"<color>\"; inherits: false; initial-value: red; } " +
                "@property --c { syntax: \"<foo>\"; inherits: false; initial-value: 2px; } #target { --c: blue }";
            Assert.AreEqual("rgba(0, 0, 255, 1)", Compute(css, "<div id=target></div>").GetPropertyValue("--c"));
        }

        [Test]
        public void InvalidRegistrationLeavesThePropertyUnregistered()
        {
            var css = "@property --c { syntax: \"<color>\"; inherits: false; } #parent { --c: 10px }";
            var style = Compute(css, "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("10px", style.GetPropertyValue("--c"));
        }

        [Test]
        public void UniversalSyntaxWithoutInitialValueThatIsNotInherited()
        {
            var css = "@property --u { syntax: \"*\"; inherits: false; } #parent { --u: foo bar }";
            var document = ParseDocument($"<style>{css}</style><div id=parent><span id=target></span></div>");

            Assert.AreEqual("foo bar", document.GetElementById("parent").ComputeCurrentStyle().GetPropertyValue("--u"));
            Assert.AreEqual(String.Empty, document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("--u"));
        }

        [Test]
        public void IdentifiersAreCaseSensitive()
        {
            var css = "@property --size { syntax: \"small | BIG\"; inherits: false; initial-value: small; } #a { --size: BIG } #b { --size: big }";
            var document = ParseDocument($"<style>{css}</style><div id=a></div><div id=b></div>");

            Assert.AreEqual("BIG", document.GetElementById("a").ComputeCurrentStyle().GetPropertyValue("--size"));
            Assert.AreEqual("small", document.GetElementById("b").ComputeCurrentStyle().GetPropertyValue("--size"));
        }

        [Test]
        public void UnregisteredPropertiesKeepTheirBehavior()
        {
            var style = Compute(Color + " #parent { --other: 2em  solid } ", "<div id=parent><span id=target></span></div>");
            Assert.AreEqual("2em  solid", style.GetPropertyValue("--other"));
        }

        [Test]
        public void TypedOmGivesTypedValuesOfRegisteredProperties()
        {
            var css = Length + " #target { font-size: 10px; --len: 2em; --other: 2em }";
            var document = ParseDocument($"<style>{css}</style><div id=target></div>");
            var map = document.GetElementById("target").ComputedStyleMap();
            var value = map.Get("--len") as CssUnitValue;

            Assert.IsNotNull(value);
            Assert.AreEqual(20.0, value.Value);
            Assert.AreEqual("px", value.Unit);
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--other"));
        }

        [Test]
        public void DeclaredStyleKeepsTheSpecifiedValue()
        {
            var document = ParseDocument("<style>" + Length + "</style><div id=target style=\"--len: 2em\"></div>");
            var element = document.GetElementById("target");

            Assert.AreEqual("2em", element.GetStyle().GetPropertyValue("--len"));
        }
    }
}
