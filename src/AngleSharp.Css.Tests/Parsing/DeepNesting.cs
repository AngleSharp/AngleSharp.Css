#nullable disable
namespace AngleSharp.Css.Tests.Parsing
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Dom.Events;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.ExceptionServices;
    using System.Text;
    using System.Threading;
    using static CssConstructionFunctions;

    /// <summary>
    /// Deeply nested input used to overflow the stack, which ends the process. The deep cases
    /// use depths that crashed before. The limits only mitigate this: parsing still recurses
    /// once per level, so these tests parse on a thread with a small, fixed stack to show the
    /// defaults hold there too.
    /// </summary>
    [TestFixture]
    public class DeepNesting
    {
        private const Int32 Deep = 10000;

        // as AngleSharp's own selector nesting tests do: less stack than the 1 MB main thread
        private const Int32 SmallStack = 256 * 1024;

        private static T OnSmallStack<T>(Func<T> parse)
        {
            var result = default(T);
            var error = default(Exception);
            var thread = new Thread(() =>
            {
                try
                {
                    result = parse();
                }
                catch (Exception ex)
                {
                    error = ex;
                }
            }, SmallStack);
            thread.Start();
            thread.Join();

            if (error is not null)
            {
                ExceptionDispatchInfo.Capture(error).Throw();
            }

            return result;
        }

        private static CssStyleSheet ParseOnSmallStack(String source) =>
            OnSmallStack(() => ParseStyleSheet(source));

        private static String Repeat(String value, Int32 count) =>
            new StringBuilder(value.Length * count).Insert(0, value, count).ToString();

        private static String NestedGroups(String atRule, Int32 depth, String inner) =>
            Repeat(atRule + "{", depth) + inner + Repeat("}", depth);

        /// <summary>
        /// The number of grouping rules nested in each other, following the first rule of each.
        /// </summary>
        private static Int32 GroupDepth(ICssRuleList rules, out ICssRuleList innermost)
        {
            var depth = 0;
            innermost = rules;

            while (innermost.Length > 0 && innermost[0] is ICssGroupingRule group)
            {
                depth++;
                innermost = group.Rules;
            }

            return depth;
        }

        private static List<Int32> CollectErrors(String source, CssParserOptions options)
        {
            var codes = new List<Int32>();
            var parser = new CssParser(options);
            parser.Error += (_, ev) => codes.Add(((CssErrorEvent)ev).Code);
            parser.ParseStyleSheet(source);
            return codes;
        }

        [TestCase("@media all")]
        [TestCase("@supports (display: block)")]
        [TestCase("@layer x")]
        [TestCase("@container (width > 1px)")]
        [TestCase("@scope (a)")]
        [TestCase("@starting-style")]
        public void DeeplyNestedGroupingRulesStopAtTheDefaultLimit(String atRule)
        {
            var sheet = ParseOnSmallStack(NestedGroups(atRule, Deep, "a{color:red}") + "b{color:blue}");
            var depth = GroupDepth(sheet.Rules, out _);
            Assert.AreEqual(CssParserOptions.DefaultMaxNestingDepth + 1, depth);
            Assert.AreEqual(2, sheet.Rules.Length);
            Assert.AreEqual("b", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
        }

        [Test]
        public void StyleRuleNestedInAsManyGroupsAsAllowedIsKept()
        {
            var limit = CssParserOptions.DefaultMaxNestingDepth;
            var sheet = ParseOnSmallStack(NestedGroups("@media all", limit, "a{color:red}"));
            Assert.AreEqual(limit, GroupDepth(sheet.Rules, out var innermost));
            Assert.AreEqual(1, innermost.Length);
            Assert.AreEqual("a", ((ICssStyleRule)innermost[0]).SelectorText);
        }

        [Test]
        public void StyleRuleNestedDeeperThanAllowedIsDroppedWithAnError()
        {
            var limit = CssParserOptions.DefaultMaxNestingDepth;
            var source = NestedGroups("@media all", limit + 1, "a{color:red}");
            var sheet = ParseOnSmallStack(source);
            Assert.AreEqual(limit + 1, GroupDepth(sheet.Rules, out var innermost));
            Assert.AreEqual(0, innermost.Length);
            CollectionAssert.Contains(CollectErrors(source, default), (Int32)CssParseError.NestingTooDeep);
        }

        [Test]
        public void NestingLimitIsConfigurable()
        {
            var options = new CssParserOptions { MaxNestingDepth = 3 };
            var source = NestedGroups("@media all", 5, "a{color:red}") + "b{color:blue}";
            var sheet = ParseStyleSheet(source, options);
            Assert.AreEqual(4, GroupDepth(sheet.Rules, out var innermost));
            Assert.AreEqual(0, innermost.Length);
            Assert.AreEqual(2, sheet.Rules.Length);
            Assert.AreEqual(1, CollectErrors(source, options).Count(code => code == (Int32)CssParseError.NestingTooDeep));
        }

        [Test]
        public void NestingLimitAboveTheDefaultIsHonored()
        {
            // on the 1 MB test thread: a raised limit is the caller's risk, and 200 nested @media
            // already overflow a 256 KB stack
            var options = new CssParserOptions { MaxNestingDepth = 200 };
            var sheet = ParseStyleSheet(NestedGroups("@media all", 200, "a{color:red}"), options);
            Assert.AreEqual(200, GroupDepth(sheet.Rules, out var innermost));
            Assert.AreEqual(1, innermost.Length);
        }

        [TestCase("& b")]
        [TestCase(".b")]
        public void DeeplyNestedStyleRulesDoNotOverflow(String selector)
        {
            var sheet = ParseOnSmallStack("a{" + Repeat(selector + "{", Deep) + "color:red" + Repeat("}", Deep + 1) + "c{color:blue}");
            Assert.AreEqual(2, sheet.Rules.Length);
            Assert.AreEqual("c", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
        }

        [Test]
        public void NestedStyleRuleBeyondTheLimitIsDroppedAndTheDeclarationsContinue()
        {
            var options = new CssParserOptions { MaxNestingDepth = 1 };
            var sheet = ParseStyleSheet("a { & b { & c { color: red } color: green } color: blue }", options);
            var a = (ICssStyleRule)sheet.Rules[0];
            Assert.AreEqual("blue", a.Style.GetPropertyValue("color").Replace("rgba(0, 0, 255, 1)", "blue"));
            Assert.AreEqual(1, a.Rules.Length);
            var b = (ICssStyleRule)a.Rules[0];
            Assert.AreEqual("rgba(0, 128, 0, 1)", b.Style.GetPropertyValue("color"));
            Assert.AreEqual(0, b.Rules.Length);
        }

        [TestCase("calc(", "(1px + ", ")", ")")]
        [TestCase("", "calc(", ")", "")]
        [TestCase("", "min(1px, ", ")", "")]
        public void DeeplyNestedMathIsInvalid(String prefix, String open, String close, String suffix)
        {
            var value = prefix + Repeat(open, Deep) + "1px" + Repeat(close, Deep) + suffix;
            var sheet = ParseOnSmallStack("a{width:" + value + "}");
            Assert.AreEqual("", ((ICssStyleRule)sheet.Rules[0]).Style.GetPropertyValue("width"));
        }

        [Test]
        public void MathNestedUpToTheLimitIsValid()
        {
            // calc() itself is the first level
            var limit = CssParserOptions.DefaultMaxValueNestingDepth;
            var style = OnSmallStack(() => ParseDeclarations("width: " + NestedCalc(limit - 1)));
            Assert.AreNotEqual("", style.GetPropertyValue("width"));
            style = OnSmallStack(() => ParseDeclarations("width: " + NestedCalc(limit)));
            Assert.AreEqual("", style.GetPropertyValue("width"));
        }

        [Test]
        public void DeeplyNestedMathSetThroughTheCssomIsInvalid()
        {
            var document = ParseDocument("<div></div>");
            var style = document.QuerySelector("div").GetStyle();
            OnSmallStack(() => { style.SetProperty("width", "calc(" + Repeat("(1px + ", Deep) + "1px" + Repeat(")", Deep) + ")"); return style; });
            Assert.AreEqual("", style.GetPropertyValue("width"));
        }

        [Test]
        public void DeeplyNestedSupportsConditionIsInvalid()
        {
            var sheet = ParseOnSmallStack("@supports " + Repeat("(", Deep) + "display: block" + Repeat(")", Deep) + " { a { color: red } } b { color: blue }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.AreEqual("b", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
        }

        [Test]
        public void SupportsConditionNestedUpToTheLimitIsValid()
        {
            var limit = CssParserOptions.DefaultMaxValueNestingDepth;
            var valid = ParseOnSmallStack("@supports " + Repeat("(", limit) + "display: block" + Repeat(")", limit) + " { a { color: red } }");
            var invalid = ParseOnSmallStack("@supports " + Repeat("(", limit + 1) + "display: block" + Repeat(")", limit + 1) + " { a { color: red } }");
            Assert.AreEqual(1, valid.Rules.Length);
            Assert.AreEqual(0, invalid.Rules.Length);
        }

        [Test]
        public void DeeplyNestedMediaConditionDoesNotOverflow()
        {
            var sheet = ParseOnSmallStack("@media " + Repeat("(", Deep) + "min-width: 1px" + Repeat(")", Deep) + " { a { color: red } } b { color: blue }");
            Assert.AreEqual("b", ((ICssStyleRule)sheet.Rules[sheet.Rules.Length - 1]).SelectorText);
        }

        [Test]
        public void ModeratelyNestedMediaConditionIsStillValid()
        {
            var sheet = ParseStyleSheet("@media " + Repeat("(", 10) + "min-width: 1px" + Repeat(")", 10) + " { a { color: red } }");
            Assert.AreEqual("(min-width: 1px)", ((ICssMediaRule)sheet.Rules[0]).ConditionText);
        }

        private static String NestedCalc(Int32 parentheses) =>
            "calc(" + Repeat("(", parentheses) + "1px" + Repeat(")", parentheses) + ")";

        private static String WidthOf(ICssStyleSheet sheet) =>
            ((ICssStyleRule)sheet.Rules[0]).Style.GetPropertyValue("width");

        [Test]
        public void ValueNestingLimitIsConfigurable()
        {
            // calc() itself is the first level
            var options = new CssParserOptions { MaxValueNestingDepth = 3 };
            Assert.AreNotEqual("", WidthOf(ParseStyleSheet("a{width:" + NestedCalc(2) + "}", options)));
            Assert.AreEqual("", WidthOf(ParseStyleSheet("a{width:" + NestedCalc(3) + "}", options)));
        }

        [Test]
        public void ValueNestingLimitAboveTheDefaultIsHonored()
        {
            var options = new CssParserOptions { MaxValueNestingDepth = 100 };
            Assert.AreEqual("", WidthOf(ParseStyleSheet("a{width:" + NestedCalc(99) + "}")));
            Assert.AreNotEqual("", WidthOf(ParseStyleSheet("a{width:" + NestedCalc(99) + "}", options)));
        }

        [Test]
        public void ValueNestingLimitAppliesToSupportsConditions()
        {
            var options = new CssParserOptions { MaxValueNestingDepth = 3 };
            var valid = ParseStyleSheet("@supports " + Repeat("(", 3) + "display: block" + Repeat(")", 3) + " { a { color: red } }", options);
            var invalid = ParseStyleSheet("@supports " + Repeat("(", 4) + "display: block" + Repeat(")", 4) + " { a { color: red } }", options);
            Assert.AreEqual(1, valid.Rules.Length);
            Assert.AreEqual(0, invalid.Rules.Length);
        }

        [Test]
        public void ValueNestingLimitOfTheBrowsingContextAppliesToItsStyleSheets()
        {
            var config = Configuration.Default.WithCss(new CssParserOptions { MaxValueNestingDepth = 3 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument("<style>a{width:" + NestedCalc(3) + "}</style>");
            Assert.AreEqual("", WidthOf((ICssStyleSheet)document.StyleSheets[0]));
        }

        [Test]
        public void ValueNestingLimitDoesNotOutliveItsParser()
        {
            ParseStyleSheet("a{width:" + NestedCalc(3) + "}", new CssParserOptions { MaxValueNestingDepth = 3 });
            // a value set without a parser right after, on the same thread, gets the default again
            var style = ParseDocument("<div></div>").QuerySelector("div").GetStyle();
            style.SetProperty("width", NestedCalc(10));
            Assert.AreNotEqual("", style.GetPropertyValue("width"));
        }

        [Test]
        public void ValuesSetThroughTheCssomUseTheDefaultLimit()
        {
            var config = Configuration.Default.WithCss(new CssParserOptions { MaxValueNestingDepth = 3 });
            var document = BrowsingContext.New(config).GetService<IHtmlParser>().ParseDocument("<div></div><div></div>");
            var divs = document.QuerySelectorAll("div");
            // an invalid value is ignored by SetProperty, so each case gets its own element
            var accepted = divs[0].GetStyle();
            accepted.SetProperty("width", NestedCalc(10));
            Assert.AreNotEqual("", accepted.GetPropertyValue("width"));
            var rejected = divs[1].GetStyle();
            rejected.SetProperty("width", NestedCalc(CssParserOptions.DefaultMaxValueNestingDepth));
            Assert.AreEqual("", rejected.GetPropertyValue("width"));
        }
    }
}
