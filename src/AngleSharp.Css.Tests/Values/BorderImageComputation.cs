#nullable disable
namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Regression test for a confirmed bug in <c>CssBorderImageSliceValue</c>, found while
    /// auditing AngleSharp.Css for a downstream renderer: its own
    /// <c>ICssValue.Compute(ICssComputeContext)</c> hard-casts every one of its four corners with
    /// <c>(CssLengthValue)(...).Compute(context)</c>, but a border-image-slice component's own
    /// grammar is <c>&lt;number&gt; | &lt;percentage&gt;</c>, not <c>&lt;length&gt;</c> - so an
    /// ordinary, spec-valid slice value like the bare number <c>30</c> in
    /// <c>border-image: url(x.png) 30;</c> computes to a <c>CssPercentageValue</c>/
    /// <c>CssNumberValue</c>, not a <c>CssLengthValue</c>, and the cast throws
    /// <c>InvalidCastException</c> instead of computing normally - crashing the entire render
    /// (<c>RenderTreeBuilder</c> computes an element's whole style declaration eagerly), not just
    /// whatever reads <c>border-image</c>.
    ///
    /// Two related, narrower hypotheses were also checked here and ruled out, rather than assumed:
    ///
    /// - <c>CssBorderImageValue</c> (the shorthand's own composite type) has the identical
    /// "unconditional <c>.Compute()</c> on a field that can legitimately be null" shape already
    /// fixed for the three gradient value types (see <see cref="GradientComputationTests"/>) and
    /// <c>CssTupleValue&lt;T&gt;</c> (see <c>GridComputation.cs</c>) - but it is never actually
    /// reached this way: <c>border-image</c>'s <c>PropertyFlags.Shorthand</c> flag makes it
    /// decompose into its five longhands at parse time, so <c>CssBorderImageValue.Compute()</c>
    /// itself is not invoked by ordinary eager rendering (confirmed empirically - a
    /// <c>border-image: url(x.png);</c> with no slice/width/outset/repeat clause at all, which
    /// would leave those fields genuinely null, does not throw).
    ///
    /// - <c>CssShapeValue</c> (the other candidate originally flagged alongside this one, for the
    /// same reason) was also ruled out: <c>ShapeParser.ParseShape</c> - its only production call
    /// site - requires all four of <c>top</c>/<c>right</c>/<c>bottom</c>/<c>left</c> to have
    /// parsed successfully before ever constructing one, so its own unconditional
    /// <c>.Compute()</c> calls, while equally unguarded in the source, are never actually
    /// reachable with a null field today.
    /// </summary>
    [TestFixture]
    public class BorderImageComputationTests
    {
        [Test]
        public void BorderImageSliceAcceptsAPlainNumberWithoutThrowingWhenComputed()
        {
            var document = "<div style=\"border-image: url(x.png) 30;\"></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }
    }
}
