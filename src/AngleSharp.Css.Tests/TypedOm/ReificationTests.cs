#nullable disable
namespace AngleSharp.Css.Tests.TypedOm
{
    using AngleSharp.Css.TypedOm;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    [TestFixture]
    public class ReificationTests
    {
        [Test]
        public void LengthIsAUnitValue()
        {
            var value = CssStyleValue.Parse("width", "10px");
            Assert.IsInstanceOf<CssUnitValue>(value);
            Assert.AreEqual(10.0, ((CssUnitValue)value).Value);
            Assert.AreEqual("px", ((CssUnitValue)value).Unit);
        }

        [Test]
        public void PercentageIsAUnitValue()
        {
            var value = (CssUnitValue)CssStyleValue.Parse("width", "50%");
            Assert.AreEqual("percent", value.Unit);
            Assert.AreEqual("50%", value.ToString());
        }

        [Test]
        public void UnitlessZeroIsAPixelValue()
        {
            var value = (CssUnitValue)CssStyleValue.Parse("width", "0");
            Assert.AreEqual(0.0, value.Value);
            Assert.AreEqual("px", value.Unit);
        }

        [Test]
        public void NumbersAndIntegersAreUnitValues()
        {
            Assert.AreEqual("number", ((CssUnitValue)CssStyleValue.Parse("opacity", "0.5")).Unit);
            Assert.AreEqual("number", ((CssUnitValue)CssStyleValue.Parse("z-index", "3")).Unit);
            Assert.AreEqual("s", ((CssUnitValue)CssStyleValue.Parse("transition-duration", "2s")).Unit);
        }

        [Test]
        public void KeywordsAreKeywordValues()
        {
            Assert.AreEqual("auto", ((CssKeywordValue)CssStyleValue.Parse("width", "auto")).Value);
            Assert.AreEqual("block", ((CssKeywordValue)CssStyleValue.Parse("display", "block")).Value);
            Assert.AreEqual("bold", ((CssKeywordValue)CssStyleValue.Parse("font-weight", "bold")).Value);
            Assert.AreEqual("none", ((CssKeywordValue)CssStyleValue.Parse("transform", "none")).Value);
        }

        [Test]
        public void CssWideKeywordsAreKeywordValues()
        {
            Assert.AreEqual("inherit", ((CssKeywordValue)CssStyleValue.Parse("width", "inherit")).Value);
            Assert.AreEqual("initial", ((CssKeywordValue)CssStyleValue.Parse("color", "initial")).Value);
            Assert.AreEqual("unset", ((CssKeywordValue)CssStyleValue.Parse("display", "unset")).Value);
        }

        [Test]
        public void CalcIsAMathValue()
        {
            var value = CssStyleValue.Parse("width", "calc(10px + 5%)");
            Assert.IsInstanceOf<CssMathSum>(value);
            Assert.AreEqual("calc(10px + 5%)", value.ToString());
        }

        [Test]
        public void ComparisonFunctionsAreMathValues()
        {
            Assert.IsInstanceOf<CssMathMin>(CssStyleValue.Parse("width", "min(10px, 2em)"));
            Assert.IsInstanceOf<CssMathClamp>(CssStyleValue.Parse("width", "clamp(1px, 2em, 3px)"));
        }

        [Test]
        public void ImagesAreImageValues()
        {
            var values = CssStyleValue.ParseAll("background-image", "url(a.png), url(b.png)");
            Assert.AreEqual(2, values.Count);
            Assert.IsInstanceOf<CssImageValue>(values[0]);
            Assert.AreEqual("url(\"a.png\")", values[0].ToString());
            Assert.AreEqual("url(\"b.png\")", values[1].ToString());
        }

        [Test]
        public void GradientsAreImageValues()
        {
            var value = CssStyleValue.Parse("background-image", "linear-gradient(red, blue)");
            Assert.IsInstanceOf<CssImageValue>(value);
        }

        [Test]
        public void ParseReturnsTheFirstOfAList()
        {
            var value = CssStyleValue.Parse("transition-duration", "1s, 2s");
            Assert.AreEqual("1s", value.ToString());
            Assert.AreEqual(2, CssStyleValue.ParseAll("transition-duration", "1s, 2s").Count);
        }

        [Test]
        public void PositionIsAPositionValue()
        {
            var value = CssStyleValue.Parse("object-position", "10px 20%");
            Assert.IsInstanceOf<CssPositionValue>(value);
            var position = (CssPositionValue)value;
            Assert.AreEqual("10px", position.X.ToString());
            Assert.AreEqual("20%", position.Y.ToString());
            Assert.AreEqual("10px 20%", position.ToString());
        }

        [Test]
        public void CustomPropertyIsAnUnparsedValue()
        {
            var value = CssStyleValue.Parse("--foo", "bar baz");
            Assert.IsInstanceOf<CssUnparsedValue>(value);
            Assert.AreEqual("bar baz", value.ToString());
        }

        [Test]
        public void ValueWithAReferenceIsAnUnparsedValue()
        {
            var value = CssStyleValue.Parse("width", "var(--x)");
            Assert.IsInstanceOf<CssUnparsedValue>(value);
            var unparsed = (CssUnparsedValue)value;
            Assert.AreEqual(1, unparsed.Length);
            Assert.AreEqual("--x", ((CssVariableReferenceValue)unparsed[0]).Variable);
        }

        [Test]
        public void TransformIsATransformValue()
        {
            var value = CssStyleValue.Parse("transform", "translate(10px, 20px) rotate(45deg)");
            Assert.IsInstanceOf<CssTransformValue>(value);
            var transform = (CssTransformValue)value;
            Assert.AreEqual(2, transform.Length);
            Assert.IsInstanceOf<CssTranslate>(transform[0]);
            Assert.IsInstanceOf<CssRotate>(transform[1]);
        }

        [Test]
        public void ValuesWithoutATypedRepresentationAreStyleValues()
        {
            var color = CssStyleValue.Parse("color", "red");
            Assert.AreEqual(typeof(CssStyleValue), color.GetType().BaseType);
            Assert.IsNotInstanceOf<CssKeywordValue>(color);
            Assert.AreEqual("rgba(255, 0, 0, 1)", color.ToString());

            var margin = CssStyleValue.Parse("margin", "1px 2px");
            Assert.AreEqual("1px 2px", margin.ToString());
        }

        [Test]
        public void InvalidValuesThrow()
        {
            Assert.Throws<ArgumentException>(() => CssStyleValue.Parse("width", "red"));
        }

        [Test]
        public void UnknownPropertiesThrow()
        {
            Assert.Throws<ArgumentException>(() => CssStyleValue.Parse("foo-bar", "10px"));
        }

        [Test]
        public void PropertyNamesAreCaseInsensitive()
        {
            Assert.AreEqual("10px", CssStyleValue.Parse("WIDTH", "10px").ToString());
        }

        [Test]
        public void ReifiesTheValueOfADeclaration()
        {
            var property = ParseDeclaration("width: calc(1px + 2em)");
            var value = property.ToStyleValue();
            Assert.IsInstanceOf<CssMathSum>(value);
            Assert.AreEqual("calc(1px + 2em)", value.ToString());
            Assert.AreEqual("calc(1px + 2em)", property.RawValue.ToStyleValue("width").ToString());
        }
    }
}
