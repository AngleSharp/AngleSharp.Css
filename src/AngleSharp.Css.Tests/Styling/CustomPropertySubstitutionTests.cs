#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    /// <summary>
    /// Substituting `var()` separated the substituted tokens with empty comments whenever no
    /// whitespace followed, e.g. `minmax(/**/15rem/**/,1fr)`, which some value parsers (like the
    /// one for grid track lists) do not accept. Found on MDN, whose page grid fell back to `none`.
    /// A separator is only needed where two tokens would otherwise merge.
    /// </summary>
    [TestFixture]
    public class CustomPropertySubstitutionTests
    {
        private static async Task<ICssStyleDeclaration> ComputeAsync(string declarations)
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var document = await context.OpenAsync(req => req.Content($"<!doctype html><style>#t {{ {declarations} }}</style><div id=t>A</div>"));
            return document.DefaultView.GetComputedStyle(document.GetElementById("t"));
        }

        [Test]
        public async Task NestedReferencesInATrackListAreValid()
        {
            var style = await ComputeAsync("display: grid; --x: 15rem; --cols: minmax(var(--x),1fr) 2rem; grid-template-columns: var(--cols)");

            var columns = style.GetPropertyValue("grid-template-columns");

            Assert.AreNotEqual("none", columns);
            StringAssert.DoesNotContain("/**/", columns);
        }

        [Test]
        public async Task SubstitutedTokensDoNotMergeWithTheirNeighbors()
        {
            // var(--n)px must stay the number 5 followed by px, not become the length 5px
            var style = await ComputeAsync("--n: 5; margin-left: var(--n)px");

            Assert.AreEqual("0px", style.GetPropertyValue("margin-left"));
        }
    }
}
