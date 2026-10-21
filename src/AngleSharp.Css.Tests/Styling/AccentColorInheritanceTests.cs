#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// `accent-color` is an inherited property (CSS Basic User Interface 4), but a child reports it
    /// as empty. Found in a downstream renderer (AngleSharp.Renderer), where `accent-color` on a form
    /// does not reach the checkboxes inside it.
    /// </summary>
    [TestFixture]
    public class AccentColorInheritanceTests
    {
        [Test]
        public void AccentColorIsInherited()
        {
            var document = ParseDocument("<form style=\"accent-color: rgb(200, 0, 100)\"><input id=target type=checkbox></form>");

            Assert.AreEqual("rgba(200, 0, 100, 1)", document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("accent-color"));
        }
    }
}
