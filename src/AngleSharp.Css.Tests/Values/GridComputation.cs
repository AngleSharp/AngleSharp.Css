#nullable disable
namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Regression tests for a confirmed bug in <c>CssTupleValue&lt;T&gt;.ICssValue.Compute</c>
    /// (Values/Multiples/CssTupleValue.cs), found while integrating this library's CSS Grid track
    /// sizing support into a downstream renderer - the same class of bug already pinned down for
    /// `transform`/gradients elsewhere in this project: it unconditionally called `.Compute(context)`
    /// on every tuple item with no null check, but `grid-column`/`grid-row` legitimately represent an
    /// omitted end line as a null item (`2 / span 2` has no explicit end line - only a start line and
    /// a span) - the single most common way a spanning grid item is actually placed. This crashed the
    /// *entire* render tree (RenderTreeBuilder computes an element's whole style declaration eagerly),
    /// not just whatever read the placement.
    /// </summary>
    [TestFixture]
    public class GridComputationTests
    {
        [Test]
        public void GridColumnWithOnlyAStartLineAndSpanDoesNotThrowWhenComputed()
        {
            var document = "<div style=\"display:grid;\"><div id=item style=\"grid-column: 2 / span 2;\"></div></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }

        [Test]
        public void GridRowWithOnlyASpanDoesNotThrowWhenComputed()
        {
            var document = "<div style=\"display:grid;\"><div id=item style=\"grid-row: span 3;\"></div></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }
    }
}
