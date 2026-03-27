namespace AngleSharp.Css.Tests.Functions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssDocumentFunctionTests
    {
        [Test]
        public void CssDocumentRuleSingleUrlFunction()
        {
            var snippet = "@document url(http://www.w3.org/) { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.Document));
            Assert.That(rule.Conditions.Count(), Is.EqualTo(1));
            var condition = rule.Conditions.First();
            Assert.That(condition.Name, Is.EqualTo("url"));
            Assert.That(condition.Data, Is.EqualTo("http://www.w3.org/"));
            Assert.That(condition.Matches(Url.Create("https://www.w3.org/")), Is.False);
            Assert.That(condition.Matches(Url.Create("http://www.w3.org")), Is.True);
        }

        [Test]
        public void CssDocumentRuleSingleUrlPrefixFunction()
        {
            var snippet = "@document url-prefix('http://www.w3.org/Style/') { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.Document));
            Assert.That(rule.Conditions.Count(), Is.EqualTo(1));
            var condition = rule.Conditions.First();
            Assert.That(condition.Name, Is.EqualTo("url-prefix"));
            Assert.That(condition.Data, Is.EqualTo("http://www.w3.org/Style/"));
            Assert.That(condition.Matches(Url.Create("https://www.w3.org/Style/")), Is.False);
            Assert.That(condition.Matches(Url.Create("http://www.w3.org/Style/foo/bar")), Is.True);
        }

        [Test]
        public void CssDocumentRuleSingleDomainFunction()
        {
            var snippet = "@document domain('mozilla.org') { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.Document));
            Assert.That(rule.Conditions.Count(), Is.EqualTo(1));
            var condition = rule.Conditions.First();
            Assert.That(condition.Name, Is.EqualTo("domain"));
            Assert.That(condition.Data, Is.EqualTo("mozilla.org"));
            Assert.That(condition.Matches(Url.Create("https://www.w3.org/")), Is.False);
            Assert.That(condition.Matches(Url.Create("http://mozilla.org")), Is.True);
            Assert.That(condition.Matches(Url.Create("http://www.mozilla.org")), Is.True);
            Assert.That(condition.Matches(Url.Create("http://foo.mozilla.org")), Is.True);
        }

        [Test]
        public void CssDocumentRuleSingleRegexpFunction()
        {
            var snippet = "@document regexp(\"https:.*\") { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.Document));
            Assert.That(rule.Conditions.Count(), Is.EqualTo(1));
            var condition = rule.Conditions.First();
            Assert.That(condition.Name, Is.EqualTo("regexp"));
            Assert.That(condition.Data, Is.EqualTo("https:.*"));
            Assert.That(condition.Matches(Url.Create("http://www.w3.org")), Is.False);
            Assert.That(condition.Matches(Url.Create("https://www.w3.org/")), Is.True);
        }

        [Test]
        public void CssDocumentRuleMultipleFunctions()
        {
            var snippet = "@document url(http://www.w3.org/), url-prefix('http://www.w3.org/Style/'), domain('mozilla.org'), regexp(\"https:.*\") { }";
            var rule = ParseRule(snippet) as CssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.Document));
            Assert.That(rule.Conditions.Count(), Is.EqualTo(4));
            Assert.That(rule.IsValid(Url.Create("https://www.w3.org/")), Is.True);
            Assert.That(rule.IsValid(Url.Create("http://www.w3.org/")), Is.True);
            Assert.That(rule.IsValid(Url.Create("http://www.w3.org/Style/bar")), Is.True);
            Assert.That(rule.IsValid(Url.Create("https://test.mozilla.org/foo")), Is.True);
            Assert.That(rule.IsValid(Url.Create("http://localhost")), Is.False);
        }
    }
}
