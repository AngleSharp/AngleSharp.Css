#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// `display: flow-root` (CSS Display 3) is a block box that establishes a new block formatting
    /// context - the standard way to make a container enclose its floats. The value is not in the
    /// list of recognized `display` keywords, so the declaration is dropped and the element falls
    /// back to `inline`. Found in a downstream renderer (AngleSharp.Renderer) while implementing
    /// floats: a `flow-root` container did not grow to contain its floated children.
    /// </summary>
    [TestFixture]
    public class DisplayFlowRootTests
    {
        [Test]
        public void FlowRootIsAValidDisplayValue()
        {
            var document = ParseDocument("<div id=target style=\"display: flow-root\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("flow-root", target.ComputeCurrentStyle().GetPropertyValue("display"));
        }

        [Test]
        public void FlowRootIsKeptOnTheDeclaration()
        {
            var document = ParseDocument("<div id=target style=\"display: flow-root\"></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("flow-root", target.GetStyle().GetPropertyValue("display"));
        }
    }
}
