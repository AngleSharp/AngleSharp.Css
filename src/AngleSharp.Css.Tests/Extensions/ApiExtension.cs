namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
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

        [Test]
        public void ExtensionCssWithAnonymousObjectSetsDeclarations()
        {
            var document = ParseDocument("<ul><li>a</li><li>b</li></ul>");
            var elements = document.QuerySelectorAll("li").Css(new { color = "red", display = "none" });
            Assert.AreEqual(2, elements.Count());

            foreach (var element in elements)
            {
                Assert.AreEqual("rgba(255, 0, 0, 1)", element.GetStyle().GetPropertyValue("color"));
                Assert.AreEqual("none", element.GetStyle().GetPropertyValue("display"));
            }
        }

        [Test]
        public void ExtensionCssWithDictionaryCopiesItsEntries()
        {
            // A dictionary binds to the generic overload ahead of the key value pair one, so it
            // must be copied there rather than have the properties of Dictionary read.
            var document = ParseDocument("<ul><li>a</li></ul>");
            var properties = new Dictionary<String, String> { ["color"] = "red", ["margin-left"] = "2px" };
            var element = document.QuerySelectorAll("li").Css(properties).Single();
            Assert.AreEqual("rgba(255, 0, 0, 1)", element.GetStyle().GetPropertyValue("color"));
            Assert.AreEqual("2px", element.GetStyle().GetPropertyValue("margin-left"));
        }

        [Test]
        public void ExtensionCssWithValueDeclaredAsObjectStillWorks()
        {
            var document = ParseDocument("<ul><li>a</li></ul>");
            Object properties = new { color = "red" };
#pragma warning disable CS0618 // The obsolete Object overload is the one under test.
            var element = document.QuerySelectorAll("li").Css(properties).Single();
#pragma warning restore CS0618
            Assert.AreEqual("rgba(255, 0, 0, 1)", element.GetStyle().GetPropertyValue("color"));
        }
    }
}
