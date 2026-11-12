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
            var window = document.DefaultView!;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element!);

            Assert.AreEqual("22.4px", style.GetFontSize());
        }

        [Test]
        public void SimpleSelectorNestingImplicitDeclarations()
        {
            var source = @"<!doctype html><head><style>.foo {
  color: green;
  .bar {
    font-size: 1.4rem;
  }
}</style></head><body class='foo'><div class='bar'>Larger and green";
            var document = ParseDocument(source);
            var window = document.DefaultView!;
            var device = document.Context.GetService<IRenderDevice>()!;
            var element = document.QuerySelector(".bar");
            var styleCollection = window.GetStyleCollection(device);
            var style = styleCollection.GetDeclarations(element!);

            Assert.AreEqual("1.4rem", style.GetFontSize());
        }


        [Test]
        public void SimpleSelectorNestingImplicitDeclarationsWordBreak()
        {
            var source = @"<!doctype html><head><style>.foo {
  color: green;
  .bar {
    word-break: break-word;
  }
}</style></head><body class='foo'><div class='bar'>Larger and green";
            var document = ParseDocument(source);
            var window = document.DefaultView!;
            var device = document.Context.GetService<IRenderDevice>()!;
            var element = document.QuerySelector(".bar")!;
            var styleCollection = window.GetStyleCollection(device);
            var style = styleCollection.GetDeclarations(element);

            Assert.AreEqual("break-word", style.GetWordBreak());
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
            var window = document.DefaultView!;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element!);

            Assert.AreEqual("22.4px", style.GetFontSize());
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
            var window = document.DefaultView!;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element!);

            Assert.AreEqual("22.4px", style.GetFontSize());
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
            var window = document.DefaultView!;
            var element = document.QuerySelector(".bar");
            var style = window.GetComputedStyle(element!);

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
            var window = document.DefaultView!;
            var element = document.QuerySelector("li");
            var style = window.GetComputedStyle(element!);

            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetColor());
        }

        // the IE hacks of old style sheets, found on www.florian-rappl.de: a declaration starting
        // with `*` looks like a nested rule, whose prelude ends at `;` or `}` and is dropped,
        // instead of taking the rest of the style sheet with it
        [TestCase("a{color:red;*zoom:1}b{color:blue}", "a { color: rgba(255, 0, 0, 1) }", "b { color: rgba(0, 0, 255, 1) }")]
        [TestCase("a{*zoom:1;color:red}b{color:blue}", "a { color: rgba(255, 0, 0, 1) }", "b { color: rgba(0, 0, 255, 1) }")]
        [TestCase("a{*display:inline;*zoom:1;color:red;}b{color:blue}", "a { color: rgba(255, 0, 0, 1) }", "b { color: rgba(0, 0, 255, 1) }")]
        public void InvalidNestedRuleIsDroppedUpToItsSemicolon(string css, string first, string second)
        {
            var sheet = ParseStyleSheet(css);

            Assert.AreEqual(2, sheet.Rules.Length);
            Assert.AreEqual(first, sheet.Rules[0].CssText);
            Assert.AreEqual(second, sheet.Rules[1].CssText);
        }
    
        // declarations directly in a nested conditional group rule apply to the elements of the
        // style rule (the tractor store: .e_HomePage { @media (min-width: 500px) { display: grid } })
        [TestCase(".foo { color: red; @media (min-width: 500px) { color: green } }", "rgba(0, 128, 0, 1)")]
        [TestCase(".foo { color: red; @media (max-width: 499px) { color: blue } }", "rgba(255, 0, 0, 1)")]
        [TestCase(".foo{color:red;@media(max-width:499px){color:blue}@media(min-width:500px){color:green}}", "rgba(0, 128, 0, 1)")]
        [TestCase(".foo { @supports (display: grid) { color: green } }", "rgba(0, 128, 0, 1)")]
        [TestCase(".bar { color: red } .foo { @media (min-width: 500px) { color: green } }", "rgba(0, 128, 0, 1)")]
        public void NestedConditionalGroupRuleWithDeclarations(string css, string color)
        {
            var source = $"<!doctype html><head><style>{css}</style></head><body><div class='foo bar'>text";
            var document = ParseDocumentOnScreen(source);
            var style = document.DefaultView!.GetComputedStyle(document.QuerySelector("div")!);

            Assert.AreEqual(color, style.GetColor());
        }

        [Test]
        public void NestedConditionalGroupRuleWithNestedStyleRule()
        {
            var source = @"<!doctype html><head><style>.foo {
  @media (min-width: 500px) {
    .bar { color: green }
  }
}</style></head><body><div class='foo'><span class='bar'>green</span></div><span class='bar'>not";
            var document = ParseDocumentOnScreen(source);
            var window = document.DefaultView!;
            var elements = document.QuerySelectorAll(".bar");

            Assert.AreEqual("rgba(0, 128, 0, 1)", window.GetComputedStyle(elements[0]).GetColor());
            Assert.AreNotEqual("rgba(0, 128, 0, 1)", window.GetComputedStyle(elements[1]).GetColor());
        }

        [Test]
        public void NestedConditionalGroupRuleInNestedStyleRule()
        {
            var source = @"<!doctype html><head><style>.foo {
  .bar {
    @media (min-width: 500px) { color: green }
  }
}</style></head><body><div class='foo'><span class='bar'>green</span></div><span class='bar'>not";
            var document = ParseDocumentOnScreen(source);
            var window = document.DefaultView!;
            var elements = document.QuerySelectorAll(".bar");

            Assert.AreEqual("rgba(0, 128, 0, 1)", window.GetComputedStyle(elements[0]).GetColor());
            Assert.AreNotEqual("rgba(0, 128, 0, 1)", window.GetComputedStyle(elements[1]).GetColor());
        }
    }
}
