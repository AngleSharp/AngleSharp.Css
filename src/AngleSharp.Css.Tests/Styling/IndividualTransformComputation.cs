#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    [TestFixture]
    public class IndividualTransformComputationTests
    {
        [Test]
        public async Task OriginalReproductionDoesNotOverflow()
        {
            using var context = BrowsingContext.New(Configuration.Default.WithCss());
            using var document = await context.OpenAsync(request => request.Content(
                "<html><body><div style='translate: 1px'></div></body></html>"));

            Assert.AreEqual("1px", document.QuerySelector("div").ComputeCurrentStyle().GetPropertyValue("translate"));
        }

        [TestCase("translate", "1px")]
        [TestCase("translate", "1px 2px")]
        [TestCase("translate", "1px 2px 3px")]
        [TestCase("translate", "50% 25%")]
        [TestCase("translate", "0")]
        [TestCase("translate", "0 -50%")]
        [TestCase("translate", "none")]
        [TestCase("rotate", "1deg")]
        [TestCase("rotate", "45deg")]
        [TestCase("rotate", "x 45deg")]
        [TestCase("rotate", "1 0 0 45deg")]
        [TestCase("rotate", "none")]
        [TestCase("scale", "1")]
        [TestCase("scale", "1.5 2")]
        [TestCase("scale", "1 1.5 2")]
        [TestCase("scale", "none")]
        public void IndividualTransformsComputeWithoutChangingSpecifiedStyles(String name, String value)
        {
            using var document = ParseDocument("<style>div{" + name + ":" + value + "}</style><div></div>");
            var element = document.QuerySelector("div");
            var styles = document.DefaultView.GetStyleCollection(new DefaultRenderDevice());
            var specified = styles.GetDeclarations(element);
            var source = specified.CssText;
            var rendered = RenderTreeBuilder.GetInstance(document.DefaultView).RenderElement(element, styles.Device);

            Assert.AreEqual(value, element.ComputeCurrentStyle().GetPropertyValue(name));
            Assert.AreEqual(value, rendered.ComputedStyle.GetPropertyValue(name));
            Assert.AreEqual(value, rendered.SpecifiedStyle.GetPropertyValue(name));
            Assert.AreEqual(source, specified.CssText);
            Assert.AreEqual(value, element.ComputeCurrentStyle().GetPropertyValue(name));
        }

        [TestCase("translate", "1px", "2px", "1px 2px")]
        [TestCase("rotate", "x", "45deg", "x 45deg")]
        [TestCase("scale", "1", "2", "1 2")]
        public void SubstitutionPreservesAllComponentsAndInheritedAliases(String name, String first, String second, String expected)
        {
            using var document = ParseDocument("<div style='--a:" + first + ";--alias:var(--a)'>" +
                "<span style='--a:unused;--b:" + second + ";" + name + ":var(--alias) var(--b)'></span></div>");
            var element = document.QuerySelector("span");
            var computed = element.ComputeCurrentStyle();

            Assert.AreEqual(expected, computed.GetPropertyValue(name));
            Assert.AreEqual(first, computed.GetPropertyValue("--alias"));
            Assert.AreEqual("var(--alias) var(--b)", element.GetStyle().GetPropertyValue(name));
        }

        [TestCase("translate", "1px")]
        [TestCase("rotate", "45deg")]
        [TestCase("scale", "2")]
        public void MissingAndCyclicVariablesStillUseFallbacksOrInitialValues(String name, String fallback)
        {
            using var document = ParseDocument("<div style='--a:var(--b);--b:var(--a);" +
                name + ":var(--a," + fallback + ");color:var(--a,red)'></div>");
            var element = document.QuerySelector("div");
            var computed = element.ComputeCurrentStyle();

            Assert.AreEqual(fallback, computed.GetPropertyValue(name));
            Assert.AreEqual("rgba(255, 0, 0, 1)", computed.GetPropertyValue("color"));
            Assert.IsInstanceOf<CssInvalidValue>(computed.GetProperty("--a").RawValue);
            Assert.IsInstanceOf<CssInvalidValue>(computed.GetProperty("--b").RawValue);

            foreach (var value in new[] { "var(--a)", "var(--missing)", "var(--missing,none trailing)" })
            {
                element.GetStyle().SetProperty(name, value);
                Assert.AreEqual("none", element.ComputeCurrentStyle().GetPropertyValue(name), value);
            }
        }

        [TestCase("translate", "1px")]
        [TestCase("rotate", "45deg")]
        [TestCase("scale", "2")]
        public void InheritanceAndSubstitutedCssWideKeywordsRetainTheirBehavior(String name, String parentValue)
        {
            using var document = ParseDocument("<div style='" + name + ":" + parentValue + "'><span></span></div>");
            var element = document.QuerySelector("span");
            element.SetAttribute("style", name + ":inherit");
            Assert.AreEqual(parentValue, element.ComputeCurrentStyle().GetPropertyValue(name));

            foreach (var keyword in new[] { "inherit", "initial", "unset" })
            {
                element.SetAttribute("style", "--keyword:var(--missing," + keyword + ");" + name + ":var(--keyword)");
                Assert.AreEqual(keyword == "inherit" ? parentValue : "none",
                    element.ComputeCurrentStyle().GetPropertyValue(name), keyword);
            }
        }

        [Test]
        public void InvalidConcreteValuesStillDefaultInsteadOfExposingEarlierDeclarations()
        {
            using var document = ParseDocument("<div style='visibility:hidden'><span style='" +
                "--bad:2px trailing;width:12px;width:var(--bad);visibility:visible;visibility:var(--bad)'></span></div>");
            var computed = document.QuerySelector("span").ComputeCurrentStyle();

            Assert.AreEqual("auto", computed.GetPropertyValue("width"));
            Assert.AreEqual("hidden", computed.GetPropertyValue("visibility"));
        }
    }
}
