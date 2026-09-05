#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CustomPropertyCyclesTests
    {
        [Test]
        public async Task OriginalReproductionDoesNotOverflow()
        {
            using var context = BrowsingContext.New(Configuration.Default.WithCss());
            using var document = await context.OpenAsync(response => response.Content(
                "<!doctype html><style>:root { --a:var(--b); --b:var(--a) } button { color:var(--a) }</style><button>Save</button>"));
            var style = document.QuerySelector("button").ComputeCurrentStyle();
            Assert.AreEqual("rgba(0, 0, 0, 1)", style.GetPropertyValue("color"));
            Assert.AreEqual(String.Empty, style.GetPropertyValue("--a"));
            Assert.AreEqual(String.Empty, style.GetPropertyValue("--b"));
        }

        [TestCase("--a:var(--a)")]
        [TestCase("--a:var(--a,visible)")]
        [TestCase("--a:var(--b);--b:var(--a)")]
        [TestCase("--a:var(--b,visible);--b:var(--a,visible)")]
        [TestCase("--a:var(--b);--b:var(--c);--c:var(--a)")]
        [TestCase("--present:visible;--a:var(--present,var(--a))")]
        [TestCase("--present:visible;--a:var(--present,calc(var(--a)))")]
        [TestCase(@"--a:var(--\61,visible)")]
        [TestCase(@"--a:v\61 r(--a,visible)")]
        [TestCase("--a:var(--b,var(--c));--b:var(--a);--c:var(--b,visible)")]
        public void EveryCyclicMemberIsInvalid(String declarations)
        {
            using var document = ParseDocument("<div></div>");
            var element = document.QuerySelector("div");
            element.SetAttribute("style", declarations + ";visibility:var(--a,hidden);--outside:var(--a,visible);display:block");
            var style = element.ComputeCurrentStyle();
            Assert.AreEqual("hidden", style.GetPropertyValue("visibility"));
            Assert.AreEqual("visible", style.GetPropertyValue("--outside"));
            Assert.AreEqual("block", style.GetPropertyValue("display"));

            foreach (var name in new[] { "--a", "--b", "--c" })
            {
                Assert.AreEqual(String.Empty, style.GetPropertyValue(name), name);
            }
        }

        [TestCase("var(--missing,hidden)", "hidden")]
        [TestCase("var(--missing,var(--other,hidden))", "hidden")]
        [TestCase("var(--missing,var(--other,var(--third,hidden)))", "hidden")]
        [TestCase("var(--missing)", "visible")]
        [TestCase("var(--missing,)", "visible")]
        public void MissingVariablesAndNestedFallbacks(String value, String expected)
        {
            using var document = ParseDocument("<div></div>");
            var element = document.QuerySelector("div");
            element.SetAttribute("style", "visibility:" + value);
            Assert.AreEqual(expected, element.ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [TestCase("--a:var(--a)", "var(--a)")]
        [TestCase("--a:12px", "var(--a,visible)")]
        [TestCase("--a:var(--missing,)", "var(--a,visible)")]
        public void InvalidAtComputedValueTimeUsesInheritanceNotPreviousDeclaration(String custom, String value)
        {
            using var document = ParseDocument("<div style='visibility:hidden'><span></span></div>");
            var element = document.QuerySelector("span");
            element.SetAttribute("style", custom + ";visibility:visible;visibility:" + value + ";width:10px;width:var(--missing)");
            var style = element.ComputeCurrentStyle();
            Assert.AreEqual("hidden", style.GetPropertyValue("visibility"));
            Assert.AreEqual("auto", style.GetPropertyValue("width"));
        }

        [Test]
        public void ResolvedTokensAreNotConvertedUsingTheCustomPropertyConverter()
        {
            using var document = ParseDocument("<div style='--a:var(--b);--b:0;opacity:var(--a);width:var(--a)'></div>");
            var style = document.QuerySelector("div").ComputeCurrentStyle();
            Assert.AreEqual("0", style.GetPropertyValue("--a"));
            Assert.AreEqual("0", style.GetPropertyValue("opacity"));
            Assert.AreEqual("0", style.GetPropertyValue("width"));
        }

        [TestCase("--a:red;--b:var(--a);--c:var(--a);--d:var(--b) var(--c)", "red red")]
        [TestCase("--d:var(--missing,)", "")]
        public void AcyclicDiamondsAndEmptyFallbacksRemainValid(String declarations, String expected)
        {
            using var document = ParseDocument("<div></div>");
            var element = document.QuerySelector("div");
            element.SetAttribute("style", declarations);
            var style = element.ComputeCurrentStyle();
            Assert.AreEqual(expected, style.GetPropertyValue("--d"));
            Assert.IsNotInstanceOf<CssInvalidValue>(style.GetProperty("--d").RawValue);
        }

        [TestCase("--a:visible;--b:var(--a)", "--a:hidden", "visible")]
        [TestCase("--a:var(--b);--b:var(--a)", "--a:visible", "hidden")]
        [TestCase("--a:visible;--b:var(--a)", "--b:initial", "hidden")]
        [TestCase("--a:visible;--b:var(--a)", "--a:hidden;--b:inherit", "visible")]
        [TestCase("--a:visible;--b:var(--a)", "--a:hidden;--b:unset", "visible")]
        public void InheritanceUsesTheParentsResolvedCustomValues(String parent, String child, String expected)
        {
            using var document = ParseDocument("<div><span></span></div>");
            document.QuerySelector("div").SetAttribute("style", parent);
            var element = document.QuerySelector("span");
            element.SetAttribute("style", child + ";visibility:var(--b,hidden)");
            Assert.AreEqual(expected, element.ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [TestCase("var(--b)", "hidden")]
        [TestCase("hidden", "hidden")]
        [TestCase("visible", "visible")]
        public void SharedRulesAreStillLocalDeclarations(String childValue, String expected)
        {
            using var document = ParseDocument(
                "<style>div{--b:var(--a);visibility:var(--b,hidden)}#p{--a:visible}#c{--a:" + childValue +
                "}</style><div id=p><div id=c>Child</div><div id=s>Sibling</div></div>");
            var child = document.QuerySelector("#c");
            var sibling = document.QuerySelector("#s");
            var parent = document.QuerySelector("#p");
            var styles = document.DefaultView.GetStyleCollection(new DefaultRenderDevice());
            var parentStyle = styles.ComputeDeclarations(parent);

            Assert.AreEqual(expected, child.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual(expected, styles.ComputeDeclarationsWithParent(child, parentStyle).GetPropertyValue("visibility"));
            var rendered = RenderTreeBuilder.GetInstance(document.DefaultView).RenderElement(parent, styles.Device);
            var renderedChild = rendered.Children.OfType<ElementRenderNode>().Single(node => node.Ref == child);
            Assert.AreEqual(expected, renderedChild.ComputedStyle.GetPropertyValue("visibility"));
            var cascade = styles.ComputeCascadedStyle(child, parentStyle);
            Assert.AreEqual(expected, cascade.Compute(new CssComputeContext(styles.Device, document.Context, cascade, parentStyle)).GetPropertyValue("visibility"));
            Assert.AreEqual("visible", sibling.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual("visible", parent.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual(expected, child.ComputeCurrentStyle().GetPropertyValue("visibility"));

            using var inlineDocument = ParseDocument("<div style='--a:visible;--b:var(--a);visibility:var(--b,hidden)'>" +
                "<div style='--a:" + childValue + ";--b:var(--a);visibility:var(--b,hidden)'></div></div>");
            Assert.AreEqual(expected, inlineDocument.QuerySelector("div div").ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [Test]
        public void InheritedOrdinaryValuesAreNotRecomputedAgainstChildVariables()
        {
            using var document = ParseDocument("<div style='--a:hidden;visibility:var(--a)'><span style='--a:visible'></span></div>");
            Assert.AreEqual("hidden", document.QuerySelector("span").ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [TestCase("initial", "visible")]
        [TestCase("inherit", "hidden")]
        [TestCase("unset", "hidden")]
        public void SubstitutedCssWideKeywordsAreAppliedToConsumers(String keyword, String expected)
        {
            using var document = ParseDocument("<div style='visibility:hidden'><span style='--a:var(--missing," + keyword +
                ");visibility:var(--a)'></span></div>");
            var element = document.QuerySelector("span");
            var styles = document.DefaultView.GetStyleCollection(new DefaultRenderDevice());
            var computed = element.ComputeCurrentStyle();
            Assert.AreEqual(expected, computed.GetPropertyValue("visibility"));
            Assert.AreEqual(keyword, computed.GetPropertyValue("--a"));
            Assert.AreEqual(keyword, computed.Compute(new CssComputeContext(styles.Device, document.Context, computed)).GetPropertyValue("--a"));
        }

        [TestCase("--a:var(--a);--a:visible", "visible")]
        [TestCase("--a:var(--a)!important;--a:visible", "hidden")]
        [TestCase("--a:visible;--a:var(--a)", "hidden")]
        public void OnlyTheWinningDeclarationParticipatesInTheGraph(String text, String expected)
        {
            using var document = ParseDocument("<div style='" + text + ";visibility:var(--a,hidden)'></div>");
            Assert.AreEqual(expected, document.QuerySelector("div").ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [TestCase("--a:var(--a);margin:var(--a)", "0", "0")]
        [TestCase("--a:var(--a);margin:var(--a,1px 2px)", "1px", "2px")]
        [TestCase("--a:1px 2px;margin:var(--a)", "1px", "2px")]
        [TestCase("--a:var(--a);margin:3px var(--a,4px)", "3px", "4px")]
        public void ShorthandsUseTheCompleteSubstitutedValue(String text, String top, String right)
        {
            using var document = ParseDocument("<div style='" + text + "'></div>");
            var style = document.QuerySelector("div").ComputeCurrentStyle();
            Assert.AreEqual(top, style.GetPropertyValue("margin-top"));
            Assert.AreEqual(right, style.GetPropertyValue("margin-right"));
            Assert.AreEqual(top, style.GetPropertyValue("margin-bottom"));
            Assert.AreEqual(right, style.GetPropertyValue("margin-left"));
        }

        [TestCase("'var(--a)'")]
        [TestCase("\"var(--a)\"")]
        [TestCase("url('var(--a)')")]
        [TestCase("visible /*var(--a)*/")]
        [TestCase("myvar(--a)")]
        public void LiteralVariableTextDoesNotCreateDependencies(String text)
        {
            var value = new CssVariableValue(text);
            Assert.IsEmpty(value.Dependencies);
            Assert.AreEqual(text, value.Substitute(_ => null));
        }

        [TestCase(@"var(--\61)", "--a")]
        [TestCase(@"v\61 r(--a)", "--a")]
        [TestCase("VAR(--A)", "--A")]
        [TestCase("var(/*comment*/--a)", "--a")]
        public void DependenciesUseDecodedCaseSensitiveNames(String text, String name)
        {
            var value = new CssVariableValue(text);
            Assert.AreEqual(new[] { name }, value.Dependencies.ToArray());
            Assert.AreEqual("red", value.Substitute(n => n == name ? new CssAnyValue("red") : null));
        }

        [Test]
        public void CustomNamesAreCaseSensitiveThroughoutCssomAndComputation()
        {
            using var document = ParseDocument("<div style='--a:hidden;--A:visible;visibility:var(--A)'></div>");
            var element = document.QuerySelector("div");
            Assert.AreEqual("hidden", element.GetStyle().GetPropertyValue("--a"));
            Assert.AreEqual("visible", element.GetStyle().GetPropertyValue("--A"));
            Assert.AreEqual("visible", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
            element.GetStyle().RemoveProperty("--A");
            Assert.AreEqual("hidden", element.ComputeCurrentStyle().GetPropertyValue("--a"));
            Assert.AreEqual(String.Empty, element.ComputeCurrentStyle().GetPropertyValue("--A"));
        }

        [Test]
        public void MutationAndPriorityDoNotChangeSharedDeclarationObjects()
        {
            using var document = ParseDocument("<style>div{--a:var(--b)!important;--b:var(--a);visibility:var(--a,hidden)}</style>" +
                "<div id=a style='--a:visible'></div><div id=b></div>");
            var element = document.QuerySelector("#a");
            var other = document.QuerySelector("#b");
            var sheet = (ICssStyleSheet)document.GetStyleSheets().Single();
            var source = sheet.Rules[0].CssText;
            var inline = element.GetStyle().CssText;
            Assert.AreEqual("hidden", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual(source, sheet.Rules[0].CssText);
            Assert.AreEqual(inline, element.GetStyle().CssText);
            element.GetStyle().SetProperty("--b", "visible");
            Assert.AreEqual("visible", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual("hidden", other.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual(source, sheet.Rules[0].CssText);
            element.GetStyle().RemoveProperty("--b");
            Assert.AreEqual("hidden", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [Test]
        public void SubstitutionPreservesSurroundingTokensAndTokenBoundaries()
        {
            using var document = ParseDocument("<div style='--r:255;--g:0;--b:0;color:rgb(var(--r),var(--g),var(--b));width:var(--g)px'></div>");
            var style = document.QuerySelector("div").ComputeCurrentStyle();
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("color"));
            Assert.AreEqual("auto", style.GetPropertyValue("width"));
        }

        [Test]
        public void MatchingIsReusedAtEachInheritanceBoundary()
        {
            using var document = ParseDocument("<style>div{--a:var(--b);--b:var(--a)}</style><div><span></span></div>");
            var styles = new CountingStyleCollection(document.DefaultView.GetStyleCollection(new DefaultRenderDevice()));
            var element = document.QuerySelector("span");
            styles.ComputeDeclarations(element);
            Assert.AreEqual(element.GetAncestors().OfType<IElement>().Count() + 1, styles.Enumerations);
            var parent = styles.ComputeDeclarations(element.ParentElement);
            var before = styles.Enumerations;
            styles.ComputeDeclarationsWithParent(element, parent);
            Assert.AreEqual(before + 1, styles.Enumerations);
        }

        [Test]
        public void ComponentDetectionAgreesWithReachability()
        {
            const Int32 count = 16;
            var random = new Random(241);

            for (var sample = 0; sample < 100; sample++)
            {
                var reachable = new Boolean[count, count];
                var text = new StringBuilder();

                for (var i = 0; i < count; i++)
                {
                    text.Append("--v").Append(i).Append(':');

                    if (random.Next(3) == 0)
                    {
                        text.Append("red;");
                    }
                    else
                    {
                        var first = random.Next(count);
                        var second = random.Next(count);
                        reachable[i, first] = reachable[i, second] = true;
                        text.Append("var(--v").Append(first).Append(",var(--v").Append(second).Append(",red));");
                    }
                }

                for (var k = 0; k < count; k++)
                {
                    for (var i = 0; i < count; i++)
                    {
                        for (var j = 0; j < count; j++)
                        {
                            reachable[i, j] |= reachable[i, k] && reachable[k, j];
                        }
                    }
                }

                var resolver = new CssCustomPropertyResolver(ParseDeclarations(text.ToString()));

                for (var i = 0; i < count; i++)
                {
                    var value = resolver.Resolve("--v" + i);
                    Assert.AreEqual(reachable[i, i], value is null, "Sample {0}, variable {1}", sample, i);

                    if (value is not null)
                    {
                        Assert.AreEqual("red", value.CssText.Replace("/**/", String.Empty));
                    }
                }
            }
        }

        [Test]
        public void SubstitutionLimitIncludesTheBoundary()
        {
            var variable = new CssVariableValue("var(--a)");
            var maximum = new String('x', CssVariableValue.MaxSubstitutionLength);
            Assert.AreEqual(maximum, variable.Substitute(_ => new CssAnyValue(maximum)));
            Assert.IsNull(variable.Substitute(_ => new CssAnyValue(maximum + "x")));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void LongNamedChainsAndCyclesUseBoundedStackSpace(Boolean cycle)
        {
            const Int32 count = 4096;
            var text = new StringBuilder();

            for (var i = 0; i < count - 1; i++)
            {
                text.Append("--v").Append(i).Append(":var(--v").Append(i + 1).Append(");");
            }

            text.Append("--v").Append(count - 1).Append(cycle ? ":var(--v0);" : ":visible;");
            text.Append("visibility:var(--v0,hidden)");
            using var document = ParseDocument("<div></div>");
            var element = document.QuerySelector("div");
            element.SetAttribute("style", text.ToString());
            Assert.AreEqual(cycle ? "hidden" : "visible", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DeepFallbacksUseBoundedStackSpace(Boolean rawFallback)
        {
            const Int32 count = 8192;
            var prefix = rawFallback ? "var(--missing,calc(" : "var(--missing,";
            var suffix = rawFallback ? "))" : ")";
            var text = String.Concat(Enumerable.Repeat(prefix, count)) + "red" + String.Concat(Enumerable.Repeat(suffix, count));
            using var document = ParseDocument("<span></span>");
            var element = document.QuerySelector("span");
            element.GetStyle().SetProperty("--a", text);
            element.GetStyle().SetProperty("color", "var(--a,blue)");
            Assert.IsNotNull(element.GetStyle().GetProperty("--a").RawValue);
            var style = element.ComputeCurrentStyle();
            Assert.IsNotNull(style);

            if (!rawFallback)
            {
                Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("color"));
            }
        }

        [Test]
        public void ExponentialSubstitutionIsBounded()
        {
            var text = new StringBuilder("--v0:red;");

            for (var i = 1; i < 24; i++)
            {
                text.Append("--v").Append(i).Append(":var(--v").Append(i - 1).Append(") var(--v").Append(i - 1).Append(");");
            }

            using var document = ParseDocument("<div></div>");
            var element = document.QuerySelector("div");
            element.SetAttribute("style", text + "visibility:var(--v23,hidden)");
            Assert.AreEqual("hidden", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        private sealed class CountingStyleCollection : IStyleCollection
        {
            private readonly IStyleCollection _inner;

            public CountingStyleCollection(IStyleCollection inner) => _inner = inner;

            public IRenderDevice Device => _inner.Device;

            public Int32 Enumerations { get; private set; }

            public IEnumerator<ICssStyleRule> GetEnumerator()
            {
                Enumerations++;
                return _inner.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
