#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using static CssConstructionFunctions;

    /// <summary>
    /// The representation of registrations (CSS Properties and Values API 1, 2.1), which
    /// @property rules and CSS.registerProperty() share.
    /// </summary>
    [TestFixture]
    public class PropertyRegistrationTests
    {
        private const String LengthRule = "@property --len { syntax: \"<length>\"; inherits: true; initial-value: 1in; }";

        private static IDocument Document(String css, String body = "") =>
            ParseDocument($"<style>{css}</style>{body}");

        [Test]
        public void PropertyRuleRegistrationIsExposed()
        {
            var registration = Document(LengthRule).GetRegisteredProperty("--len");

            Assert.IsNotNull(registration);
            Assert.AreEqual("--len", registration.Name);
            Assert.AreEqual("<length>", registration.Syntax);
            Assert.IsFalse(registration.IsUniversal);
            Assert.IsFalse(registration.IsList);
            Assert.IsTrue(registration.Inherits);
            Assert.AreEqual(CssPropertyRegistrationOrigin.Rule, registration.Origin);
        }

        [Test]
        public void InitialValueHasAllRepresentations()
        {
            var registration = Document(LengthRule).GetRegisteredProperty("--len");

            Assert.AreEqual("1in", registration.InitialValue);
            Assert.IsInstanceOf<CssLengthValue>(registration.RawInitialValue);
            var typed = (CssUnitValue)registration.RawInitialValue.ToStyleValue(registration.Name);
            Assert.AreEqual(1.0, typed.Value);
            Assert.AreEqual("in", typed.Unit);
        }

        [Test]
        public void UniversalSyntaxWithoutInitialValue()
        {
            var registration = Document("@property --any { syntax: \"*\"; inherits: false; }").GetRegisteredProperty("--any");

            Assert.IsTrue(registration.IsUniversal);
            Assert.IsNull(registration.InitialValue);
            Assert.IsNull(registration.RawInitialValue);
        }

        [Test]
        public void ScriptRegistrationIsExposedAndWins()
        {
            var document = Document(LengthRule);
            document.DefaultView.RegisterProperty(new PropertyDefinition
            {
                Name = "--len",
                Syntax = "<color>",
                InitialValue = "red",
            });
            var registration = document.GetRegisteredProperty("--len");

            Assert.AreEqual(CssPropertyRegistrationOrigin.Script, registration.Origin);
            Assert.AreEqual("<color>", registration.Syntax);
            Assert.IsFalse(registration.Inherits);
            Assert.AreEqual(1, document.GetRegisteredProperties().Count());
        }

        [Test]
        public void InvalidAndUnregisteredPropertiesHaveNoRegistration()
        {
            var document = Document("@property --bad { syntax: \"<length>\"; inherits: false; initial-value: 1em; }");

            Assert.IsNull(document.GetRegisteredProperty("--bad"));
            Assert.IsNull(document.GetRegisteredProperty("--unknown"));
            Assert.IsNull(document.GetRegisteredProperty("color"));
            Assert.AreEqual(0, document.GetRegisteredProperties().Count());
        }

        [Test]
        public void LastValidRuleWins()
        {
            var css = LengthRule + " @property --len { syntax: \"<color>\"; inherits: false; initial-value: blue; }";
            var registration = Document(css).GetRegisteredProperty("--len");

            Assert.AreEqual("<color>", registration.Syntax);
        }

        [Test]
        public void RegistrationFollowsConditionalRules()
        {
            var document = Document("@media print { " + LengthRule + " }");
            Assert.IsNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void RegistrationReflectsChangedStyleSheets()
        {
            var document = Document("");
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            var sheet = (ICssStyleSheet)document.StyleSheets[0];
            sheet.Insert(LengthRule, 0);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void RuleGivesItsRegistration()
        {
            var sheet = ParseStyleSheet(LengthRule + " @property --bad { syntax: \"<length>\"; }");
            var rules = sheet.Rules.OfType<ICssPropertyRule>().ToArray();

            var registration = rules[0].GetRegistration();
            Assert.AreEqual("--len", registration.Name);
            Assert.AreEqual(rules[0].Syntax, registration.Syntax);
            Assert.AreEqual(rules[0].Inherits, registration.Inherits);
            Assert.AreEqual(rules[0].InitialValue, registration.InitialValue);
            Assert.IsNull(rules[1].GetRegistration());
        }

        [Test]
        public void MatchUsesTheSyntax()
        {
            var registration = Document("@property --v { syntax: \"<length> | auto\"; inherits: false; initial-value: auto; }").GetRegisteredProperty("--v");

            Assert.IsInstanceOf<CssLengthValue>(registration.Match("10px"));
            Assert.AreEqual("auto", registration.Match(" auto ").CssText);
            Assert.IsNull(registration.Match("red"));
            Assert.IsNull(registration.Match("var(--x)"));
        }

        [Test]
        public void ListSyntaxes()
        {
            var css = "@property --a { syntax: \"<length>#\"; inherits: false; initial-value: 0px; } " +
                "@property --b { syntax: \"<length>+\"; inherits: false; initial-value: 0px; } " +
                "@property --c { syntax: \"<length># | <color>\"; inherits: false; initial-value: 0px; }";
            var document = Document(css);

            Assert.IsTrue(document.GetRegisteredProperty("--a").IsList);
            Assert.IsTrue(document.GetRegisteredProperty("--b").IsList);
            Assert.IsFalse(document.GetRegisteredProperty("--c").IsList);
        }
    }
}
