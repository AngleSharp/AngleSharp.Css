namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;

    [TestFixture]
    public class ApiExtensionTests
    {
        [Test]
        public void ExtensionCssWithEmptyListAndEmptyDeclaration()
        {
            var document = ParseDocument("");
            var elements = document.QuerySelectorAll("li").Css(new { });
            Assert.That(elements.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ExtensionCssWithEmptyListOnly()
        {
            var document = ParseDocument("");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.That(elements.Count(), Is.EqualTo(0));
        }
    }
}
