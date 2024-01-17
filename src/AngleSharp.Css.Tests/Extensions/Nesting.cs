namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class NestingTests
    {
        [Test]
        public void SimpleSelectorNesting()
        {
            var source = @"<!doctype html><head><style>.foo {
  color: green;
 .bar {
    font-size: 1.4rem;
  }
}</style></head><body class='foo'><div class='bar'>Larger and green";
            var document = ParseDocument(source);
            var window = document.DefaultView;
            Assert.IsNotNull(document);

            var element = document.QuerySelector(".bar");
            Assert.IsNotNull(element);

            var style = window.GetComputedStyle(element);
            Assert.IsNotNull(style);

            Assert.AreEqual("1.4rem", style.GetFontSize());
        }
    }
}
