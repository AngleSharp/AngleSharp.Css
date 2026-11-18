#nullable disable
namespace AngleSharp.Css.Tests.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class NumericValueTests
    {
        [Test]
        public void UnitValuesSerializeWithTheirUnit()
        {
            Assert.AreEqual("10px", CssHelpers.Px(10).ToString());
            Assert.AreEqual("2", CssHelpers.Number(2).ToString());
            Assert.AreEqual("50%", CssHelpers.Percent(50).ToString());
            Assert.AreEqual("1.5em", CssHelpers.Em(1.5).ToString());
            Assert.AreEqual("2khz", CssHelpers.KHz(2).ToString());
        }

        [Test]
        public void UnitIsNormalized()
        {
            var value = new CssUnitValue(1, "PX");
            Assert.AreEqual("px", value.Unit);
            Assert.AreEqual("percent", new CssUnitValue(1, "%").Unit);
        }

        [Test]
        public void UnknownUnitIsRejected()
        {
            Assert.Throws<ArgumentException>(() => new CssUnitValue(1, "foo"));
        }

        [Test]
        public void TypeOfUnitValues()
        {
            Assert.AreEqual(1, CssHelpers.Px(1).Type().Length);
            Assert.IsNull(CssHelpers.Px(1).Type().Angle);
            Assert.AreEqual(1, CssHelpers.Deg(1).Type().Angle);
            Assert.AreEqual(1, CssHelpers.Percent(1).Type().Percent);
            Assert.AreEqual(1, CssHelpers.Fr(1).Type().Flex);
            Assert.IsNull(CssHelpers.Number(1).Type().Length);
            Assert.IsNull(CssHelpers.Number(1).Type().PercentHint);
        }

        [Test]
        public void AddingTheSameUnitsGivesAUnitValue()
        {
            var result = CssHelpers.Px(1).Add(CssHelpers.Px(2), CssHelpers.Px(3));
            Assert.IsInstanceOf<CssUnitValue>(result);
            Assert.AreEqual("6px", result.ToString());
        }

        [Test]
        public void AddingDifferentUnitsGivesASum()
        {
            var result = CssHelpers.Px(1).Add(CssHelpers.Em(2));
            Assert.IsInstanceOf<CssMathSum>(result);
            Assert.AreEqual("calc(1px + 2em)", result.ToString());
            Assert.AreEqual(1, result.Type().Length);
        }

        [Test]
        public void AddingAPercentageSetsThePercentHint()
        {
            var result = CssHelpers.Px(1).Add(CssHelpers.Percent(2));
            Assert.AreEqual("calc(1px + 2%)", result.ToString());
            Assert.AreEqual(1, result.Type().Length);
            Assert.IsNull(result.Type().Percent);
            Assert.AreEqual(CssNumericBaseType.Length, result.Type().PercentHint);
        }

        [Test]
        public void AddingIncompatibleTypesThrows()
        {
            Assert.Throws<ArgumentException>(() => CssHelpers.Px(1).Add(CssHelpers.Deg(1)));
            Assert.Throws<ArgumentException>(() => CssHelpers.Px(1).Add(1));
        }

        [Test]
        public void AddingASumFlattensIt()
        {
            var sum = CssHelpers.Px(1).Add(CssHelpers.Em(2));
            var result = sum.Add(CssHelpers.Vw(3));
            Assert.AreEqual("calc(1px + 2em + 3vw)", result.ToString());
            Assert.AreEqual(3, ((CssMathSum)result).Values.Length);
        }

        [Test]
        public void SubtractingNegatesUnitValuesDirectly()
        {
            // negating a unit value negates its value (CSS Typed OM 4.3.2)
            var result = CssHelpers.Px(1).Sub(CssHelpers.Em(2));
            Assert.AreEqual("calc(1px + -2em)", result.ToString());
            Assert.IsInstanceOf<CssUnitValue>(((CssMathSum)result).Values[1]);
        }

        [Test]
        public void SubtractingAMathValueGivesASumWithANegation()
        {
            var result = CssHelpers.Px(1).Sub(CssHelpers.Em(2).Mul(CssHelpers.Px(1)).Div(CssHelpers.Px(1)));
            Assert.AreEqual("calc(1px - (2em * 1px / 1px))", result.ToString());
            Assert.IsInstanceOf<CssMathNegate>(((CssMathSum)result).Values[1]);
            Assert.IsInstanceOf<CssMathNegate>(((CssMathSum)CssNumericValue.Parse("calc(1px - 2em)")).Values[1]);
        }

        [Test]
        public void SubtractingTheSameUnits()
        {
            Assert.AreEqual("-1px", CssHelpers.Px(1).Sub(CssHelpers.Px(2)).ToString());
        }

        [Test]
        public void MultiplyingWithNumbersGivesAUnitValue()
        {
            Assert.AreEqual("6px", CssHelpers.Px(2).Mul(3).ToString());
            Assert.AreEqual("6", CssHelpers.Number(2).Mul(3).ToString());
        }

        [Test]
        public void MultiplyingTwoDimensionsGivesAProduct()
        {
            var result = CssHelpers.Px(2).Mul(CssHelpers.Em(3));
            Assert.IsInstanceOf<CssMathProduct>(result);
            Assert.AreEqual("calc(2px * 3em)", result.ToString());
            Assert.AreEqual(2, result.Type().Length);
        }

        [Test]
        public void DividingByANumber()
        {
            Assert.AreEqual("5px", CssHelpers.Px(10).Div(2).ToString());
        }

        [Test]
        public void DividingByADimensionGivesAProductWithAnInversion()
        {
            var result = CssHelpers.Px(1).Div(CssHelpers.Em(2));
            Assert.AreEqual("calc(1px / 2em)", result.ToString());
            Assert.IsNull(result.Type().Length);
        }

        [Test]
        public void DividingByZeroThrows()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CssHelpers.Px(1).Div(0));
        }

        [Test]
        public void MinAndMaxOfTheSameUnits()
        {
            Assert.AreEqual("1px", CssHelpers.Px(1).Min(CssHelpers.Px(3)).ToString());
            Assert.AreEqual("3px", CssHelpers.Px(1).Max(CssHelpers.Px(3)).ToString());
        }

        [Test]
        public void MinAndMaxOfDifferentUnits()
        {
            Assert.AreEqual("min(1px, 2em)", CssHelpers.Px(1).Min(CssHelpers.Em(2)).ToString());
            Assert.AreEqual("max(1px, 2em)", CssHelpers.Px(1).Max(CssHelpers.Em(2)).ToString());
            Assert.Throws<ArgumentException>(() => CssHelpers.Px(1).Min(CssHelpers.S(2)));
        }

        [Test]
        public void ClampSerializes()
        {
            var clamp = new CssMathClamp(CssHelpers.Px(1), CssHelpers.Em(2), CssHelpers.Px(3));
            Assert.AreEqual("clamp(1px, 2em, 3px)", clamp.ToString());
            Assert.AreEqual(CssMathOperator.Clamp, clamp.Operator);
            Assert.Throws<ArgumentException>(() => new CssMathClamp(CssHelpers.Px(1), CssHelpers.Deg(2), CssHelpers.Px(3)));
        }

        [Test]
        public void EmptySumIsASyntaxError()
        {
            var ex = Assert.Throws<DomException>(() => new CssMathSum());
            Assert.AreEqual((Int32)DomError.Syntax, ex.Code);
        }

        [Test]
        public void NestedExpressionsSerializeWithParentheses()
        {
            var sum = new CssMathSum(CssHelpers.Px(1), new CssMathProduct(CssHelpers.Em(2), 3));
            Assert.AreEqual("calc(1px + (2em * 3))", sum.ToString());
            Assert.AreEqual("calc(-1px)", new CssMathNegate(CssHelpers.Px(1)).ToString());
            Assert.AreEqual("calc(1 / 2px)", new CssMathInvert(CssHelpers.Px(2)).ToString());
            Assert.AreEqual("min(1px + 2em, 3px)", new CssMathMin(new CssMathSum(CssHelpers.Px(1), CssHelpers.Em(2)), CssHelpers.Px(3)).ToString());
        }

        [Test]
        public void OperatorsMapToTheMethods()
        {
            var a = CssHelpers.Px(1);
            var b = CssHelpers.Px(2);
            Assert.AreEqual("3px", (a + b).ToString());
            Assert.AreEqual("-1px", (a - b).ToString());
            Assert.AreEqual("4px", (b * 2).ToString());
            Assert.AreEqual("0.5px", (a / 2).ToString());
            Assert.AreEqual("-1px", (-a).ToString());
        }

        [Test]
        public void ConvertingAbsoluteUnits()
        {
            Assert.AreEqual(96.0, CssHelpers.In(1).To("px").Value, 1e-9);
            Assert.AreEqual(1.0, CssHelpers.Px(96).To("in").Value, 1e-9);
            Assert.AreEqual(10.0, CssHelpers.Cm(1).To("mm").Value, 1e-9);
            Assert.AreEqual(180.0, CssHelpers.Turn(0.5).To("deg").Value, 1e-9);
            Assert.AreEqual(Math.PI, CssHelpers.Deg(180).To("rad").Value, 1e-9);
            Assert.AreEqual(1500.0, CssHelpers.S(1.5).To("ms").Value, 1e-9);
            Assert.AreEqual(2000.0, CssHelpers.KHz(2).To("hz").Value, 1e-9);
            Assert.AreEqual(1.0, CssHelpers.Dpi(96).To("dppx").Value, 1e-9);
        }

        [Test]
        public void ConvertingRelativeUnitsThrows()
        {
            Assert.Throws<ArgumentException>(() => CssHelpers.Em(1).To("px"));
            Assert.Throws<ArgumentException>(() => CssHelpers.Px(1).To("deg"));
            Assert.Throws<ArgumentException>(() => CssHelpers.Px(1).Add(CssHelpers.Em(1)).To("px"));
        }

        [Test]
        public void ConvertingToAnUnknownUnitIsASyntaxError()
        {
            Assert.Throws<DomException>(() => CssHelpers.Px(1).To("foo"));
        }

        [Test]
        public void ConvertingMathExpressions()
        {
            Assert.AreEqual(97.0, CssNumericValue.Parse("calc(1px + 1in)").To("px").Value, 1e-9);
            Assert.AreEqual(1.5, CssNumericValue.Parse("calc(1s + 500ms)").To("s").Value, 1e-9);
            Assert.AreEqual(5.0, CssNumericValue.Parse("calc(10px / 2)").To("px").Value, 1e-9);
            Assert.AreEqual(1.0, CssNumericValue.Parse("min(1px, 2px, 1in)").To("px").Value, 1e-9);
            Assert.AreEqual(2.0, CssNumericValue.Parse("clamp(1px, 2px, 3px)").To("px").Value, 1e-9);
            Assert.AreEqual(3.0, CssNumericValue.Parse("clamp(1px, 5px, 3px)").To("px").Value, 1e-9);
        }

        [Test]
        public void ToSumWithoutUnitsGivesTheCanonicalUnits()
        {
            var sum = CssNumericValue.Parse("calc(1in + 1em + 1px)").ToSum();
            Assert.AreEqual("calc(1em + 97px)", sum.ToString());
        }

        [Test]
        public void ToSumWithUnits()
        {
            var sum = CssNumericValue.Parse("calc(1in + 1em + 1px)").ToSum("px", "em");
            Assert.AreEqual("calc(97px + 1em)", sum.ToString());
            Assert.Throws<ArgumentException>(() => CssNumericValue.Parse("calc(1in + 1em)").ToSum("px"));
        }

        [Test]
        public void ToSumOfAProductOfUnitsThrows()
        {
            Assert.Throws<ArgumentException>(() => CssHelpers.Px(1).Mul(CssHelpers.Px(1)).ToSum());
        }

        [Test]
        public void StructuralEquality()
        {
            Assert.IsTrue(CssHelpers.Px(1).Equals(CssHelpers.Px(1)));
            Assert.IsFalse(CssHelpers.Px(1).Equals(CssHelpers.Px(2)));
            Assert.IsFalse(CssHelpers.Px(1).Equals(CssHelpers.Em(1)));
            Assert.IsTrue(CssNumericValue.Parse("calc(1px + 2em)").Equals(CssHelpers.Px(1).Add(CssHelpers.Em(2))));
            Assert.IsTrue(CssHelpers.Px(1).Equals(CssHelpers.Px(1), CssHelpers.Px(1)));
            Assert.IsFalse(CssHelpers.Px(1).Equals(CssHelpers.Px(1), CssHelpers.Px(2)));
            Assert.AreEqual(CssHelpers.Px(1).GetHashCode(), CssHelpers.Px(1).GetHashCode());
        }

        [TestCase("10px")]
        [TestCase("-2.5em")]
        [TestCase("50%")]
        [TestCase("3")]
        [TestCase("45deg")]
        [TestCase("calc(1px + 2em)")]
        [TestCase("calc(1px - 2em)")]
        [TestCase("calc(2 * 1em)")]
        [TestCase("calc(1px / 2)")]
        [TestCase("calc(1px + (2em * 3))")]
        [TestCase("calc((1px + 2em) * 3)")]
        [TestCase("min(1px, 2em)")]
        [TestCase("max(1px, 2em, 3vw)")]
        [TestCase("clamp(1px, 2em, 3px)")]
        [TestCase("min(1px + 2em, 3px)")]
        public void ParseRoundTrips(String text)
        {
            Assert.AreEqual(text, CssNumericValue.Parse(text).ToString());
        }

        [Test]
        public void ParseKeepsTheStructure()
        {
            Assert.IsInstanceOf<CssUnitValue>(CssNumericValue.Parse("10px"));
            Assert.IsInstanceOf<CssMathSum>(CssNumericValue.Parse("calc(1px + 2px)"));
            Assert.IsInstanceOf<CssMathProduct>(CssNumericValue.Parse("calc(1px * 2)"));
            Assert.IsInstanceOf<CssMathMin>(CssNumericValue.Parse("min(1px, 2em)"));
            Assert.IsInstanceOf<CssMathMax>(CssNumericValue.Parse("max(1px, 2em)"));
            Assert.IsInstanceOf<CssMathClamp>(CssNumericValue.Parse("clamp(1px, 2em, 3px)"));
        }

        [Test]
        public void ParseOfCalcAroundASingleValueIsASum()
        {
            var value = CssNumericValue.Parse("calc(10px)");
            Assert.IsInstanceOf<CssMathSum>(value);
            Assert.AreEqual("calc(10px)", value.ToString());
        }

        [Test]
        public void ParseOfInvalidTextIsASyntaxError()
        {
            Assert.Throws<DomException>(() => CssNumericValue.Parse("foo"));
            Assert.Throws<DomException>(() => CssNumericValue.Parse("10px 20px"));
            Assert.Throws<DomException>(() => CssNumericValue.Parse("calc(1px +)"));
        }

        [Test]
        public void ParseOfIncompatibleTypesIsASyntaxError()
        {
            Assert.Throws<DomException>(() => CssNumericValue.Parse("calc(1px + 1s)"));
        }
    }
}
