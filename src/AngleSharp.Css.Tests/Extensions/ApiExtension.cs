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
            Assert.AreEqual(0, elements.Count());
        }

        [Test]
        public void ExtensionCssWithEmptyListOnly()
        {
            var document = ParseDocument("");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.AreEqual(0, elements.Count());
        }
    }
}
