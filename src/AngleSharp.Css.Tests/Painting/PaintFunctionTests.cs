#nullable disable
namespace AngleSharp.Css.Tests.Painting
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Painting;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using static CssConstructionFunctions;

    /// <summary>
    /// Parsing and serializing the paint() function (CSS Painting API 1, 2), which works
    /// without any paint definition or scripting engine.
    /// </summary>
    [TestFixture]
    public class PaintFunctionTests
    {
        [TestCase("background-image: paint(checkerboard)", "paint(checkerboard)")]
        [TestCase("background-image: PAINT( checkerboard )", "paint(checkerboard)")]
        [TestCase("background-image: paint(checkerboard, 10px)", "paint(checkerboard, 10px)")]
        [TestCase("background-image: paint(checkerboard,10px,red)", "paint(checkerboard, 10px, red)")]
        [TestCase("background-image: paint(x, 1px solid  red, \"a, b\")", "paint(x, 1px solid  red, \"a, b\")")]
        [TestCase("background-image: paint(x, calc(1px + 2px), rgb(0, 0, 0))", "paint(x, calc(1px + 2px), rgb(0, 0, 0))")]
        [TestCase("background-image: paint(x, [a, b], f(c, d))", "paint(x, [a, b], f(c, d))")]
        [TestCase("background-image: paint(x, a /* , b */)", "paint(x, a /* , b */)")]
        [TestCase("background-image: paint(a), url(b.png), paint(c, 1)", "paint(a), url(\"b.png\"), paint(c, 1)")]
        [TestCase("background-image: image-set(paint(a) 1x, \"b.png\" 2x)", "image-set(paint(a) 1x, \"b.png\" 2x)")]
        [TestCase("border-image-source: paint(border)", "paint(border)")]
        [TestCase("mask-image: paint(mask, 2)", "paint(mask, 2)")]
        [TestCase("list-style-image: paint(bullet)", "paint(bullet)")]
        public void PaintFunctionIsParsedAndSerialized(String declaration, String expected)
        {
            var property = ParseDeclaration(declaration);
            Assert.IsTrue(property.HasValue, declaration);
            Assert.AreEqual(expected, property.Value);
        }

        [TestCase("background-image: paint()")]
        [TestCase("background-image: paint(10px)")]
        [TestCase("background-image: paint(\"name\")")]
        [TestCase("background-image: paint(x,)")]
        [TestCase("background-image: paint(x, )")]
        [TestCase("background-image: paint(x, a,, b)")]
        [TestCase("background-image: paint(x, a, )")]
        [TestCase("background-image: paint(x y)")]
        [TestCase("background-image: paint(x, a])")]
        [TestCase("background-image: paint(x, (a)")]
        [TestCase("background-image: paint(x")]
        [TestCase("background-image: paint(x) red")]
        public void InvalidPaintFunctionIsDropped(String declaration)
        {
            var property = ParseDeclaration(declaration);
            Assert.IsFalse(property.HasValue, declaration);
        }

        [Test]
        public void PaintFunctionKeepsTheNameAndTheArguments()
        {
            var property = ParseDeclaration("background-image: paint(ring, 2px, \"a, b\", f(1, 2))");
            var paint = property.RawValue.GetPaintValues().Single();

            Assert.AreEqual("paint", paint.Name);
            Assert.AreEqual("ring", paint.PainterName);
            CollectionAssert.AreEqual(new[] { "2px", "\"a, b\"", "f(1, 2)" }, paint.Arguments.Select(m => m.CssText).ToArray());
        }

        [Test]
        public void PaintFunctionsAreEqualIfTheyAreWrittenAlike()
        {
            var a = new CssPaintValue("x", new ICssValue[] { new CssAnyValue("1px") });
            var b = ParseDeclaration("background-image: paint(x, 1px)").RawValue.GetPaintValues().Single();
            var c = new CssPaintValue("x", new ICssValue[] { new CssAnyValue("2px") });

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
        }

        [Test]
        public void BackgroundShorthandWithPaintFunction()
        {
            var style = ParseDeclarations("background: paint(stripes, 4px) no-repeat center / cover, red");

            Assert.AreEqual("paint(stripes, 4px), none", style.GetPropertyValue("background-image"));
            Assert.AreEqual("paint(stripes, 4px) center / cover no-repeat, rgba(255, 0, 0, 1)", style.GetPropertyValue("background"));
        }

        [Test]
        public void BorderImageShorthandWithPaintFunction()
        {
            var style = ParseDeclarations("border-image: paint(frame) 30 round");
            Assert.AreEqual("paint(frame)", style.GetPropertyValue("border-image-source"));
        }

        [Test]
        public void PaintFunctionWithVariableInArguments()
        {
            var document = ParseDocument("<div id=target style=\"--size: 8px; background-image: paint(dots, var(--size))\"></div>");
            var style = document.GetElementById("target").ComputeCurrentStyle();

            Assert.AreEqual("paint(dots, 8px)", style.GetPropertyValue("background-image"));
        }

        [Test]
        public void PaintFunctionIsComputedAsSpecified()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(dots, 2em)\"></div>");
            var style = document.GetElementById("target").ComputeCurrentStyle();

            Assert.AreEqual("paint(dots, 2em)", style.GetPropertyValue("background-image"));
        }

        [Test]
        public void PaintFunctionMatchesImageSyntaxOfRegisteredProperty()
        {
            var document = ParseDocument("<div id=target style=\"--img: paint(x, 1)\"></div>");
            document.DefaultView.RegisterProperty(new PropertyDefinition { Name = "--img", Syntax = "<image>", Inherits = false, InitialValue = "url(a.png)" });
            var style = document.GetElementById("target").ComputeCurrentStyle();

            Assert.AreEqual("paint(x, 1)", style.GetPropertyValue("--img"));
        }

        [Test]
        public void PaintFunctionIsReifiedAsImageValue()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(x, 1px)\"></div>");
            var map = document.GetElementById("target").GetAttributeStyleMap();
            var value = map.Get("background-image");

            Assert.IsInstanceOf<CssImageValue>(value);
            Assert.AreEqual("paint(x, 1px)", value.ToString());
        }

        [Test]
        public void ElementWithUnregisteredPainterIsStillStyled()
        {
            var document = ParseDocument("<style>div { background-image: paint(missing); color: red }</style><div id=target></div>");
            var style = document.GetElementById("target").ComputeCurrentStyle();

            Assert.AreEqual("paint(missing)", style.GetPropertyValue("background-image"));
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetPropertyValue("color"));
        }
    }
}
