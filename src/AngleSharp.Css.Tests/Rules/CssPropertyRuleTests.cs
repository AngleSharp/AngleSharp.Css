#nullable disable
namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPropertyRuleTests
    {
        private static ICssPropertyRule ParseRule(String source) =>
            (ICssPropertyRule)ParseStyleSheet(source).Rules[0];

        [Test]
        public void ValidRuleExposesItsDescriptors()
        {
            var rule = ParseRule("@property --my-color { syntax: \"<color>\"; inherits: false; initial-value: #c0ffee; }");

            Assert.AreEqual("--my-color", rule.Name);
            Assert.AreEqual("<color>", rule.Syntax);
            Assert.IsFalse(rule.Inherits);
            Assert.AreEqual("#c0ffee", rule.InitialValue);
            Assert.IsTrue(rule.IsValid);
        }

        [Test]
        public void InheritsIsCaseInsensitive()
        {
            var rule = ParseRule("@property --x { syntax: \"*\"; inherits: TRUE; }");
            Assert.IsTrue(rule.Inherits);
            Assert.IsTrue(rule.IsValid);
        }

        [Test]
        public void UniversalSyntaxNeedsNoInitialValue()
        {
            var rule = ParseRule("@property --x { syntax: \"*\"; inherits: false; }");
            Assert.IsTrue(rule.IsValid);
            Assert.IsNull(rule.InitialValue);
        }

        [Test]
        public void InvalidSyntaxDescriptorIsIgnored()
        {
            var rule = ParseRule("@property --x { syntax: \"<foo>\"; inherits: false; initial-value: 1px; }");
            Assert.AreEqual(String.Empty, rule.GetPropertyValue("syntax"));
            Assert.AreEqual(String.Empty, rule.Syntax);
            Assert.IsFalse(rule.IsValid);
        }

        [Test]
        public void SyntaxMustBeAString()
        {
            var rule = ParseRule("@property --x { syntax: <length>; inherits: false; initial-value: 1px; }");
            Assert.AreEqual(String.Empty, rule.GetPropertyValue("syntax"));
            Assert.IsFalse(rule.IsValid);
        }

        [Test]
        public void InvalidInheritsDescriptorIsIgnored()
        {
            var rule = ParseRule("@property --x { syntax: \"<length>\"; inherits: maybe; initial-value: 1px; }");
            Assert.AreEqual(String.Empty, rule.GetPropertyValue("inherits"));
            Assert.IsFalse(rule.IsValid);
        }

        [Test]
        public void InvalidDescriptorKeepsAnEarlierValidOne()
        {
            var rule = ParseRule("@property --x { syntax: \"<length>\"; syntax: \"<foo>\"; inherits: false; initial-value: 1px; }");
            Assert.AreEqual("<length>", rule.Syntax);
            Assert.IsTrue(rule.IsValid);
        }

        [Test]
        public void UnknownDescriptorsAreIgnoredWithoutInvalidatingTheRule()
        {
            var rule = ParseRule("@property --x { syntax: \"<length>\"; foo: bar; inherits: false; initial-value: 1px; }");
            Assert.AreEqual(String.Empty, rule.GetPropertyValue("foo"));
            Assert.IsTrue(rule.IsValid);
        }

        [TestCase("@property --x { inherits: false; initial-value: 1px; }")]
        [TestCase("@property --x { syntax: \"<length>\"; initial-value: 1px; }")]
        [TestCase("@property --x { syntax: \"<length>\"; inherits: false; }")]
        [TestCase("@property --x { syntax: \"<length>\"; inherits: false; initial-value: red; }")]
        [TestCase("@property --x { syntax: \"<length>\"; inherits: false; initial-value: 2em; }")]
        [TestCase("@property --x { syntax: \"<length>\"; inherits: false; initial-value: calc(1px + 1rem); }")]
        [TestCase("@property --x { syntax: \"<length>\"; inherits: false; initial-value: var(--y); }")]
        [TestCase("@property --x { syntax: \"*\"; inherits: false; initial-value: var(--y); }")]
        [TestCase("@property x { syntax: \"<length>\"; inherits: false; initial-value: 1px; }")]
        public void InvalidRegistrations(String source)
        {
            Assert.IsFalse(ParseRule(source).IsValid);
        }

        [TestCase("@property --x { syntax: \"<length>\"; inherits: false; initial-value: 1in; }")]
        [TestCase("@property --x { syntax: \"<length>\"; inherits: false; initial-value: 10vw; }")]
        [TestCase("@property --x { syntax: \"<length-percentage>\"; inherits: false; initial-value: 50%; }")]
        [TestCase("@property --x { syntax: \"<length> | auto\"; inherits: false; initial-value: auto; }")]
        [TestCase("@property --x { syntax: \"<color>#\"; inherits: false; initial-value: red, blue; }")]
        [TestCase("@property --x { syntax: \"<transform-list>\"; inherits: false; initial-value: rotate(45deg) scale(2); }")]
        [TestCase("@property --x { syntax: \"<string>\"; inherits: false; initial-value: 'em'; }")]
        public void ValidRegistrations(String source)
        {
            Assert.IsTrue(ParseRule(source).IsValid);
        }

        [Test]
        public void RuleSerializesAndParsesAgain()
        {
            var rule = ParseRule("@property --x { syntax: \"<length>\"; inherits: false; initial-value: 0px; }");
            var again = ParseRule(rule.CssText);

            Assert.AreEqual("--x", again.Name);
            Assert.AreEqual("<length>", again.Syntax);
            Assert.AreEqual("0px", again.InitialValue);
            Assert.IsTrue(again.IsValid);
        }

        [TestCase("*")]
        [TestCase("<length>")]
        [TestCase("  <number>  ")]
        [TestCase("<length>+")]
        [TestCase("<color>#")]
        [TestCase("<length> | auto")]
        [TestCase("big | bigger | BIGGER")]
        [TestCase("<transform-list>")]
        [TestCase("<transform-function>+")]
        [TestCase("<string>")]
        [TestCase("<custom-ident>#")]
        public void ValidSyntaxStrings(String text)
        {
            Assert.IsNotNull(CssPropertySyntax.Parse(text));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase("<foo>")]
        [TestCase("<Length>")]
        [TestCase("<transform-list>+")]
        [TestCase("<length> +")]
        [TestCase("* | <length>")]
        [TestCase("**")]
        [TestCase("inherit")]
        [TestCase("<length> <color>")]
        [TestCase("|")]
        [TestCase("<length> |")]
        [TestCase("<length>++")]
        public void InvalidSyntaxStrings(String text)
        {
            Assert.IsNull(CssPropertySyntax.Parse(text));
        }

        [TestCase("<length> | auto", "10px", true)]
        [TestCase("<length> | auto", "auto", true)]
        [TestCase("<length> | auto", "red", false)]
        [TestCase("<length>+", "1px 2em 3vw", true)]
        [TestCase("<length>+", "1px, 2px", false)]
        [TestCase("<color>#", "red, #00f", true)]
        [TestCase("<color>#", "red blue", false)]
        [TestCase("big | BIGGER", "BIGGER", true)]
        [TestCase("big | BIGGER", "bigger", false)]
        [TestCase("<custom-ident>", "foo", true)]
        [TestCase("<custom-ident>", "inherit", false)]
        [TestCase("<integer>", "3", true)]
        [TestCase("<integer>", "3.5", false)]
        [TestCase("<angle>", "45deg", true)]
        [TestCase("<url>", "url(a.png)", true)]
        [TestCase("<image>", "linear-gradient(red, blue)", true)]
        [TestCase("*", "anything goes", true)]
        public void Matching(String syntax, String value, Boolean matches)
        {
            var result = CssPropertySyntax.Parse(syntax).Match(new CssAnyValue(value, isResolved: true));
            Assert.AreEqual(matches, result is not null);
        }
    }
}
