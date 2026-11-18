#nullable disable
namespace AngleSharp.Css.Tests.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Css.Values;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    [TestFixture]
    public class TransformValueTests
    {
        private static CssTransformValue Parse(String text) => (CssTransformValue)CssStyleValue.Parse("transform", text);

        [TestCase("translate(10px, 20px) rotate(45deg)")]
        [TestCase("translate3d(1px, 2px, 3px)")]
        [TestCase("rotate3d(1, 0, 0, 45deg)")]
        [TestCase("scale(2, 3)")]
        [TestCase("scale3d(1, 2, 3)")]
        [TestCase("skew(10deg, 20deg)")]
        [TestCase("skewX(10deg)")]
        [TestCase("skewY(10deg)")]
        [TestCase("perspective(100px)")]
        [TestCase("matrix(1, 2, 3, 4, 5, 6)")]
        [TestCase("translate(calc(1px + 2em), 50%)")]
        public void RoundTrips(String text)
        {
            Assert.AreEqual(text, Parse(text).ToString());
        }

        [Test]
        public void ShortFormsAreExpanded()
        {
            Assert.AreEqual("translate(10px, 0px)", Parse("translateX(10px)").ToString());
            Assert.AreEqual("translate(0px, 10px)", Parse("translateY(10px)").ToString());
            Assert.AreEqual("translate3d(0px, 0px, 10px)", Parse("translateZ(10px)").ToString());
            Assert.AreEqual("scale(2, 2)", Parse("scale(2)").ToString());
            Assert.AreEqual("scale(2, 1)", Parse("scaleX(2)").ToString());
            Assert.AreEqual("rotate3d(1, 0, 0, 10deg)", Parse("rotateX(10deg)").ToString());
            Assert.AreEqual("skew(10deg)", Parse("skew(10deg)").ToString());
        }

        [Test]
        public void Is2D()
        {
            Assert.IsTrue(Parse("translate(10px, 20px) rotate(45deg)").Is2D);
            Assert.IsFalse(Parse("translate(10px, 20px) translateZ(5px)").Is2D);
        }

        [Test]
        public void ComponentsExposeTheirValues()
        {
            var transform = Parse("translate(10px, 20px) rotate(45deg) scale(2, 3)");
            var translate = (CssTranslate)transform[0];
            var rotate = (CssRotate)transform[1];
            var scale = (CssScale)transform[2];
            Assert.AreEqual("10px", translate.X.ToString());
            Assert.AreEqual("20px", translate.Y.ToString());
            Assert.AreEqual("45deg", rotate.Angle.ToString());
            Assert.AreEqual("3", scale.Y.ToString());
        }

        [TestCase("translate(10px, 20px)")]
        [TestCase("translate3d(10px, 20px, 30px)")]
        [TestCase("rotate(30deg)")]
        [TestCase("rotate3d(1, 2, 3, 30deg)")]
        [TestCase("scale(2, 3)")]
        [TestCase("skew(10deg, 20deg)")]
        [TestCase("perspective(100px)")]
        [TestCase("matrix(1, 2, 3, 4, 5, 6)")]
        public void ToMatrixAgreesWithTheCssom(String text)
        {
            var raw = (CssTupleValue)ParseDeclaration("transform: " + text).RawValue;
            var expected = ((ICssTransformFunctionValue)raw.Items[0]).ComputeMatrix(null);
            var actual = Parse(text).ToMatrix();
            AssertMatrix(expected, actual);
        }

        [Test]
        public void ToMatrixComposesTheTransforms()
        {
            var matrix = Parse("translate(10px, 0px) scale(2)").ToMatrix();
            Assert.AreEqual(2.0, matrix[0, 0], 1e-9);
            Assert.AreEqual(2.0, matrix[1, 1], 1e-9);
            Assert.AreEqual(10.0, matrix[0, 3], 1e-9);
        }

        [Test]
        public void ToMatrixConvertsAbsoluteUnits()
        {
            var matrix = Parse("translate(1in, 0px)").ToMatrix();
            Assert.AreEqual(96.0, matrix[0, 3], 1e-9);
        }

        [Test]
        public void ToMatrixOfRelativeUnitsThrows()
        {
            Assert.Throws<ArgumentException>(() => Parse("translate(1em, 0px)").ToMatrix());
            Assert.Throws<ArgumentException>(() => Parse("translate(50%, 0px)").ToMatrix());
        }

        [Test]
        public void ComponentsValidateTheirTypes()
        {
            Assert.Throws<ArgumentException>(() => new CssTranslate(CssHelpers.Deg(1), CssHelpers.Px(1)));
            Assert.Throws<ArgumentException>(() => new CssRotate(CssHelpers.Px(1)));
            Assert.Throws<ArgumentException>(() => new CssScale(CssHelpers.Px(1), 1));
            Assert.Throws<ArgumentException>(() => new CssPerspective(CssHelpers.Deg(1)));
            Assert.Throws<ArgumentException>(() => new CssTransformValue(Array.Empty<CssTransformComponent>()));
        }

        [Test]
        public void TransformCanBeBuilt()
        {
            var transform = new CssTransformValue(new CssTransformComponent[]
            {
                new CssTranslate(CssHelpers.Px(10), CssHelpers.Percent(50)),
                new CssRotate(CssHelpers.Turn(0.5)),
                new CssPerspective(new CssKeywordValue("none")),
            });
            Assert.AreEqual("translate(10px, 50%) rotate(0.5turn) perspective(none)", transform.ToString());
            Assert.IsFalse(transform.Is2D);
        }

        [Test]
        public void TranslateCanBeMade2D()
        {
            var translate = new CssTranslate(CssHelpers.Px(1), CssHelpers.Px(2), CssHelpers.Px(3));
            Assert.AreEqual("translate3d(1px, 2px, 3px)", translate.ToString());
            translate.Is2D = true;
            Assert.AreEqual("translate(1px, 2px)", translate.ToString());
            Assert.AreEqual(0.0, translate.ToMatrix()[2, 3]);
        }

        private static void AssertMatrix(TransformMatrix expected, TransformMatrix actual)
        {
            for (var row = 0; row < 4; row++)
            {
                for (var col = 0; col < 4; col++)
                {
                    Assert.AreEqual(expected[row, col], actual[row, col], 1e-9, $"[{row}, {col}]");
                }
            }
        }
    }
}
