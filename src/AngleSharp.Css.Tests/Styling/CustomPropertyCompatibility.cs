#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CustomPropertyCompatibilityTests
    {
        [TestCase("visible", "visible")]
        [TestCase("var(--b)", "hidden")]
        public void OnlyComputedStylesResolveCustomProperties(String value, String expected)
        {
            using var document = ParseDocument("<style>div{--a:" + value +
                ";--b:var(--a);visibility:var(--b,hidden)}</style><div><span></span></div>");
            var element = document.QuerySelector("div");
            var child = document.QuerySelector("span");
            var styles = document.DefaultView.GetStyleCollection(new DefaultRenderDevice());
            var explicitStyle = styles.ComputeExplicitStyle(element);
            var declarations = styles.GetDeclarations(element);
            var cascade = styles.ComputeCascadedStyle(child, declarations);
            var builder = RenderTreeBuilder.GetInstance(document.DefaultView);
            var rendered = builder.RenderElement(element, styles.Device);
            var renderedChild = rendered.Children.OfType<ElementRenderNode>().Single();

            foreach (var raw in new[] { explicitStyle, declarations, cascade, styles.GetDeclarations(child),
                rendered.SpecifiedStyle, renderedChild.SpecifiedStyle, builder.GetElementStyle(child) })
            {
                Assert.AreEqual("var(--a)", raw.GetPropertyValue("--b"));
                Assert.AreEqual("var(--b,hidden)", raw.GetPropertyValue("visibility"));
            }

            Assert.AreEqual(expected, styles.ComputeDeclarations(child).GetPropertyValue("visibility"));
            Assert.AreEqual(expected, renderedChild.ComputedStyle.GetPropertyValue("visibility"));
            Assert.AreEqual("var(--a)", declarations.GetPropertyValue("--b"));
        }

        [Test]
        public void PublicFallbackParserPreservesNestedVariableObjects()
        {
            var source = new StringSource("var(--a,var(--b,red)))");
            var outer = source.ParseVarFallback() as CssVarValue;
            Assert.IsNotNull(outer);
            Assert.AreEqual("--a", outer.VariableName);
            var inner = outer.DefaultValue as CssVarValue;
            Assert.IsNotNull(inner);
            Assert.AreEqual("--b", inner.VariableName);
            Assert.AreEqual("red", inner.DefaultValue.CssText);
            Assert.AreEqual("var(--a, var(--b, red))", outer.CssText);
            Assert.AreEqual(')', source.Current);
        }

        [Test]
        public void ParsedReferencesPreserveNestedFallbackObjects()
        {
            var property = ParseDeclaration("visibility:var(--a,var(--b,var(--c,hidden)))");
            var reference = (CssReferenceValue)property.RawValue;
            var second = reference.References[0].DefaultValue as CssVarValue;
            Assert.IsNotNull(second);
            Assert.AreEqual("--b", second.VariableName);
            var third = second.DefaultValue as CssVarValue;
            Assert.IsNotNull(third);
            Assert.AreEqual("--c", third.VariableName);
            Assert.AreEqual("hidden", third.DefaultValue.CssText);
        }

        [Test]
        public void PublicReferenceParserPreservesTheSourcePosition()
        {
            var source = new StringSource("var(--before) var(--after)");
            source.NextTo("var(--before) ".Length);
            var index = source.Index;
            var reference = source.ParseVars();
            Assert.AreEqual(index, source.Index);
            Assert.AreEqual(1, reference.References.Length);
            Assert.AreEqual("--after", reference.References[0].VariableName);
            Assert.AreEqual("var(--before) var(--after)", reference.CssText);
            Assert.AreEqual("after", ((ICssValue)reference).Compute(new TestComputeContext()).CssText);
        }

        [TestCase("var(--a)", false)]
        [TestCase("var(--a,)", true)]
        [TestCase("var(--a, )", true)]
        public void EmptyFallbacksAreDistinctFromAbsentFallbacks(String text, Boolean hasFallback)
        {
            var source = new StringSource(text);
            var reference = (CssVarValue)source.ParseVarFallback();
            Assert.AreEqual("--a", reference.VariableName);
            Assert.AreEqual(hasFallback, reference.DefaultValue is not null);
            Assert.AreEqual(String.Empty, reference.DefaultValue?.CssText ?? String.Empty);
            Assert.IsTrue(source.IsDone);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DeepPublicFallbackTreesRemainIterative(Boolean parseDirectly)
        {
            const Int32 count = 8192;
            var text = String.Concat(Enumerable.Repeat("var(--missing,", count)) + "visible" + new String(')', count);
            var reference = parseDirectly ? (CssVarValue)new StringSource(text).ParseVarFallback() :
                ((CssReferenceValue)ParseDeclaration("visibility:" + text).RawValue).References[0];
            var current = reference;
            var depth = 1;

            while (current.DefaultValue is CssVarValue nested)
            {
                current = nested;
                depth++;
            }

            Assert.AreEqual(count, depth);
            Assert.AreEqual("visible", current.DefaultValue.CssText);
            Assert.AreEqual(text.Replace(",", ", "), reference.CssText);
            var context = new TestComputeContext { Converter = ParseDeclaration("visibility:visible").Converter };
            Assert.AreEqual("visible", reference.Compute(context).CssText);
        }

        [Test]
        public void DirectReferenceComputationUsesSuppliedAndMutableReferences()
        {
            var reference = new CssReferenceValue("var(--literal)", new[]
            {
                Tuple.Create(new TextRange(default, default), new CssVarValue("--supplied")),
            });
            var context = new TestComputeContext();
            Assert.AreEqual("supplied", ((ICssValue)reference).Compute(context).CssText);
            reference.References[0] = new CssVarValue("--modified");
            Assert.AreEqual("modified", ((ICssValue)reference).Compute(context).CssText);
            Assert.AreEqual("var(--literal)", reference.CssText);
        }

        [Test]
        public void DirectReferenceComputationRetainsFirstSuccessfulReference()
        {
            var reference = new CssReferenceValue("var(--literal)", new[]
            {
                Tuple.Create(new TextRange(default, default), new CssVarValue("--missing")),
                Tuple.Create(new TextRange(default, default), new CssVarValue("--supplied")),
                Tuple.Create(new TextRange(default, default), new CssVarValue("--unused")),
            });
            var context = new TestComputeContext();
            Assert.AreEqual("supplied", ((ICssValue)reference).Compute(context).CssText);
            CollectionAssert.AreEqual(new[] { "--missing", "--supplied" }, context.Names);
        }

        [Test]
        public void ModifiedParsedReferencesAffectComputedStyles()
        {
            using var document = ParseDocument("<div style='--a:visible;--b:hidden;visibility:var(--a)'></div>");
            var element = document.QuerySelector("div");
            var reference = (CssReferenceValue)element.GetStyle().GetProperty("visibility").RawValue;
            reference.References[0] = new CssVarValue("--b");
            Assert.AreEqual("hidden", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual("var(--a)", reference.CssText);
        }

        [TestCase("--b", "hidden")]
        [TestCase("--alias", "collapse")]
        public void ModifiedCustomPropertyReferencesParticipateInResolution(String name, String expected)
        {
            using var document = ParseDocument("<div style='--a:visible;--b:hidden;--alias:var(--a);visibility:var(--alias,collapse)'></div>");
            var element = document.QuerySelector("div");
            var reference = (CssReferenceValue)element.GetStyle().GetProperty("--alias").RawValue;
            reference.References[0] = new CssVarValue(name);
            Assert.AreEqual(expected, element.ComputeCurrentStyle().GetPropertyValue("visibility"));
            Assert.AreEqual("var(--a)", element.GetStyle().GetPropertyValue("--alias"));
        }

        [Test]
        public void ConstructedCustomPropertyReferencesAreNotReparsedFromLiteralText()
        {
            using var document = ParseDocument("<div style='--a:visible;--b:hidden;--alias:var(--a);visibility:var(--alias)'></div>");
            var element = document.QuerySelector("div");
            ((CssProperty)element.GetStyle().GetProperty("--alias")).RawValue = new CssReferenceValue("var(--a)", new[]
            {
                Tuple.Create(new TextRange(default, default), new CssVarValue("--missing")),
                Tuple.Create(new TextRange(default, default), new CssVarValue("--b")),
            });
            Assert.AreEqual("hidden", element.ComputeCurrentStyle().GetPropertyValue("visibility"));
        }

        [Test]
        public void ModifiedShorthandReferencesParticipateInResolution()
        {
            using var document = ParseDocument("<div style='--a:1px 2px;--b:3px 4px;margin:var(--a)'></div>");
            var element = document.QuerySelector("div");
            var child = (CssChildValue)element.GetStyle().GetProperty("margin-top").RawValue;
            var reference = (CssReferenceValue)child.Parent;
            reference.References[0] = new CssVarValue("--b");
            var computed = element.ComputeCurrentStyle();
            Assert.AreEqual("3px", computed.GetPropertyValue("margin-top"));
            Assert.AreEqual("4px", computed.GetPropertyValue("margin-right"));
        }

        [Test]
        public void DirectVariableComputationRetainsFallbackOnFailedComputation()
        {
            var reference = new CssVarValue("--invalid", new CssIdentifierValue("fallback"));
            var context = new TestComputeContext();
            Assert.AreEqual("fallback", reference.Compute(context).CssText);
        }

        [Test]
        public void ComputationDoesNotSuppressValueExceptions()
        {
            var reference = new CssVarValue("--throw", new CssIdentifierValue("fallback"));
            Assert.Throws<InvalidOperationException>(() => reference.Compute(new TestComputeContext()));
        }

        private sealed class TestComputeContext : ICssComputeContext
        {
            public IRenderDevice Device { get; } = new DefaultRenderDevice();
            public IBrowsingContext Context => null;
            public IValueConverter Converter { get; set; }
            public List<String> Names { get; } = new();

            public ICssValue Resolve(String name)
            {
                Names.Add(name);

                if (name == "--invalid")
                {
                    return new CssAnyValue("not a value");
                }

                if (name == "--throw")
                {
                    throw new InvalidOperationException("Test exception");
                }

                return name == "--missing" ? null : new CssIdentifierValue(name.Substring(2));
            }
        }
    }
}
