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
        public void SimpleSelectorNestingImplicit()
        {
            var source = @"<!doctype html><head><style>.foo {
  color: green;
  .bar {
    font-size: 1.4rem;
  }
}</style></head><body class='foo'><div class='bar'>Larger and green";
            var document = ParseDocument(source);
            var window = document.DefaultView;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element);

            Assert.AreEqual("1.4rem", style.GetFontSize());
        }

        [Test]
        public void SimpleSelectorNestingExplicit()
        {
            var source = @"<!doctype html><head><style>.foo {
  color: green;
  & .bar {
    font-size: 1.4rem;
  }
}</style></head><body class='foo'><div class='bar'>Larger and green";
            var document = ParseDocument(source);
            var window = document.DefaultView;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element);

            Assert.AreEqual("1.4rem", style.GetFontSize());
        }

        [Test]
        public void SimpleSelectorNestingOverwritten()
        {
            var source = @"<!doctype html><head><style>.foo .bar {
  font-size: 1rem;
}

.foo {
  color: green;
  .bar {
    font-size: 1.4rem;
  }
}</style></head><body class='foo'><div class='bar'>Larger and green";
            var document = ParseDocument(source);
            var window = document.DefaultView;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element);

            Assert.AreEqual("1.4rem", style.GetFontSize());
        }

        [Test]
        public void CombinedSelectorNesting()
        {
            var source = @"<!doctype html><head><style>.foo {
  color: red;
  &.bar {
    color: green;
  }
}</style></head><body><div class='foo bar'>green";
            var document = ParseDocument(source);
            var window = document.DefaultView;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element);

            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetColor());
        }

        [Test]
        public void ReversedSelectorNesting()
        {
            var source = @"<!doctype html><head><style>li {
  color: red;
  .list & {
    color: green;
  }
}</style></head><body><ul class='list'><li>green";
            var document = ParseDocument(source);
            var window = document.DefaultView;
            var element = document.QuerySelector("li");
            var style = window.GetComputedStyle(element);

            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetColor());
        }
    }
}
