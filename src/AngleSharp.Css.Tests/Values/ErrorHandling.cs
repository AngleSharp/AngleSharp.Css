namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class ErrorHandlingTests
    {
        [Test]
        public void ParseInlineStyleWithToleratedInvalidValueShouldReturnThatValue()
        {
            var source = "<div style=\"background-image: url(javascript:alert(1))\"></div>";
            var document = ParseDocument(source, new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true
            });
            var div = document.QuerySelector<IHtmlElement>("div");
            Assert.That(div.GetStyle().Length, Is.EqualTo(1));
            Assert.That(div.GetStyle()[0], Is.EqualTo("background-image"));
            Assert.That(div.GetStyle().GetBackgroundImage(), Is.EqualTo("url(\"javascript:alert(1)\")"));
        }

        [Test]
        public void ParseInlineStyleWithUnknownDeclarationShouldBeAbleToRemoveThatDeclaration()
        {
            var source = @"<DIV STYLE='background: url(""javascript:alert(foo)"")'>";
            var document = ParseDocument(source, new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true
            });
            var div = document.QuerySelector<IHtmlElement>("div");
            Assert.That(div.GetStyle().Length, Is.EqualTo(10));
            div.GetStyle().RemoveProperty("background");
            Assert.That(div.GetStyle().Length, Is.EqualTo(0));
        }
    }
}
