#nullable disable
namespace AngleSharp.Css.Tests.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    /// <summary>
    /// The typed values of registered custom properties (CSS Typed OM 1, 5 and CSS
    /// Properties and Values API 1, 6) and their relation to the CSSOM.
    /// </summary>
    [TestFixture]
    public class RegisteredPropertyTypedOmTests
    {
        private const String Rules =
            "@property --len { syntax: \"<length>\"; inherits: false; initial-value: 0px; } " +
            "@property --list { syntax: \"<length>#\"; inherits: false; initial-value: 0px; } " +
            "@property --spaced { syntax: \"<length>+\"; inherits: false; initial-value: 0px; } " +
            "@property --any { syntax: \"*\"; inherits: false; }";

        private static IElement CreateElement(String style)
        {
            var document = ParseDocument($"<style>{Rules}</style><div id=target style=\"{style}\"></div>");
            return document.GetElementById("target");
        }

        [Test]
        public void DeclaredValueIsTypedBySyntax()
        {
            var map = CreateElement("--len: 2em; --other: 2em").GetAttributeStyleMap();
            var value = map.Get("--len") as CssUnitValue;

            Assert.IsNotNull(value);
            Assert.AreEqual(2.0, value.Value);
            Assert.AreEqual("em", value.Unit);
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--other"));
        }

        [Test]
        public void DeclaredValueWithReferenceIsUnparsed()
        {
            var map = CreateElement("--len: var(--x)").GetAttributeStyleMap();
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--len"));
        }

        [Test]
        public void DeclaredValueNotMatchingTheSyntaxIsUnparsed()
        {
            // invalid at computed-value time, but still declared
            var map = CreateElement("--len: red").GetAttributeStyleMap();
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--len"));
        }

        [Test]
        public void UniversalSyntaxIsUnparsed()
        {
            var map = CreateElement("--any: 10px").GetAttributeStyleMap();
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--any"));
        }

        [Test]
        public void ListSyntaxesGiveOneValuePerItem()
        {
            var map = CreateElement("--list: 1px, 2px; --spaced: 3px 4px 5px").GetAttributeStyleMap();

            Assert.AreEqual(new[] { "1px", "2px" }, map.GetAll("--list").Select(m => m.ToString()).ToArray());
            Assert.AreEqual(new[] { "3px", "4px", "5px" }, map.GetAll("--spaced").Select(m => m.ToString()).ToArray());
            Assert.IsTrue(map.GetAll("--spaced").All(m => m is CssUnitValue));
        }

        [Test]
        public void SetValidatesTheSyntax()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();

            map.Set("--len", CssHelpers.Px(10));
            Assert.AreEqual("10px", element.GetStyle().GetPropertyValue("--len"));
            Assert.Throws<ArgumentException>(() => map.Set("--len", "red"));
            Assert.Throws<ArgumentException>(() => map.Set("--len", CssHelpers.Number(3)));
            Assert.AreEqual("10px", element.GetStyle().GetPropertyValue("--len"));
        }

        [Test]
        public void SetAcceptsReferencesAndKeywords()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();

            map.Set("--len", "var(--x)");
            Assert.AreEqual("var(--x)", element.GetStyle().GetPropertyValue("--len"));
            map.Set("--len", "initial");
            Assert.AreEqual("initial", element.GetStyle().GetPropertyValue("--len"));
        }

        [Test]
        public void SetOfUnregisteredOrUniversalPropertyAcceptsAnything()
        {
            var map = CreateElement("").GetAttributeStyleMap();

            Assert.DoesNotThrow(() => map.Set("--other", "red"));
            Assert.DoesNotThrow(() => map.Set("--any", "red"));
        }

        [Test]
        public void ListSyntaxIsListValued()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();

            map.Set("--list", CssHelpers.Px(1), CssHelpers.Px(2));
            map.Append("--list", CssHelpers.Px(3));
            map.Set("--spaced", CssHelpers.Px(4), CssHelpers.Px(5));

            Assert.AreEqual("1px, 2px, 3px", element.GetStyle().GetPropertyValue("--list"));
            Assert.AreEqual("4px 5px", element.GetStyle().GetPropertyValue("--spaced"));
            Assert.AreEqual(3, map.GetAll("--list").Count);
            Assert.Throws<ArgumentException>(() => map.Append("--list", "red"));
        }

        [Test]
        public void NonListSyntaxIsNotListValued()
        {
            var map = CreateElement("").GetAttributeStyleMap();

            Assert.Throws<ArgumentException>(() => map.Set("--len", CssHelpers.Px(1), CssHelpers.Px(2)));
            Assert.Throws<ArgumentException>(() => map.Append("--len", CssHelpers.Px(1)));
            Assert.Throws<ArgumentException>(() => map.Append("--any", "a"));
        }

        [Test]
        public void ScriptRegistrationApplies()
        {
            var element = CreateElement("--c: red");
            element.Owner.DefaultView.RegisterProperty(new PropertyDefinition
            {
                Name = "--c",
                Syntax = "<number>",
                InitialValue = "0",
            });
            var map = element.GetAttributeStyleMap();

            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--c"));
            map.Set("--c", CssHelpers.Number(5));
            Assert.IsInstanceOf<CssUnitValue>(map.Get("--c"));
            Assert.Throws<ArgumentException>(() => map.Set("--c", "red"));
        }

        [Test]
        public void StyleRuleMapUsesTheRegistrationsOfTheDocument()
        {
            var document = ParseDocument($"<style>{Rules} #target {{ --len: 3px }}</style>");
            var sheet = (ICssStyleSheet)document.StyleSheets[0];
            var rule = sheet.Rules.OfType<ICssStyleRule>().Single();
            var map = rule.GetStyleMap();

            Assert.IsInstanceOf<CssUnitValue>(map.Get("--len"));
            Assert.Throws<ArgumentException>(() => map.Set("--len", "red"));
        }

        [Test]
        public void TypedAndCssomViewsStayInSync()
        {
            var element = CreateElement("--len: 1px");
            var style = element.GetStyle();
            var map = element.GetAttributeStyleMap();

            style.SetProperty("--len", "4em");
            Assert.AreEqual("4em", map.Get("--len").ToString());
            map.Set("--len", CssHelpers.Px(7));
            Assert.AreEqual("7px", style.GetPropertyValue("--len"));
            // the CSSOM keeps the declared value of a custom property, i.e., any value
            style.SetProperty("--len", "red");
            Assert.AreEqual("red", style.GetPropertyValue("--len"));
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--len"));
        }

        [Test]
        public void ComputedMapTypesRegisteredValues()
        {
            var element = CreateElement("font-size: 10px; --len: 2em; --list: 1px, 1em");
            var map = element.ComputedStyleMap();

            Assert.AreEqual("20px", map.Get("--len").ToString());
            Assert.AreEqual(new[] { "1px", "10px" }, map.GetAll("--list").Select(m => m.ToString()).ToArray());
        }

        [Test]
        public async Task ParseUsesTheRegistrationOfTheActiveDocument()
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            await context.OpenAsync(res => res.Content($"<style>{Rules}</style>"));

            Assert.IsInstanceOf<CssUnitValue>(CssStyleValue.Parse("--len", "1px", context));
            Assert.Throws<ArgumentException>(() => CssStyleValue.Parse("--len", "red", context));
            Assert.AreEqual(2, CssStyleValue.ParseAll("--list", "1px, 2px", context).Count);
            Assert.IsInstanceOf<CssUnparsedValue>(CssStyleValue.Parse("--len", "1px"));
        }
    }
}
