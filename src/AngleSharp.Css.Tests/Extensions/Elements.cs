namespace AngleSharp.Css.Tests.Extensions
{
    using NUnit.Framework;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System.Linq;

    [TestFixture]
    public class ElementsTests
    {
        [Test]
        public void SetAllStyles()
        {
            var document = "<div></div><div></div><div></div>".ToHtmlDocument(Configuration.Default.WithCss());
            var divs = document.QuerySelectorAll("div");
            divs.SetStyle(style => style.SetBackground("red"));

            Assert.AreEqual("rgba(255, 0, 0, 1)", divs.Skip(0).First().GetStyle().GetBackground());
            Assert.AreEqual("rgba(255, 0, 0, 1)", divs.Skip(1).First().GetStyle().GetBackground());
            Assert.AreEqual("rgba(255, 0, 0, 1)", divs.Skip(2).First().GetStyle().GetBackground());
        }
    }
}
