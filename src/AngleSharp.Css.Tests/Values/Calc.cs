namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CalcTests
    {
        [Test]
        public void CalcSimpleNumber()
        {
            var source = new StringSource(@"calc(5)");
            var calc = CalcParser.ParseCalc(source);
            Assert.AreEqual("calc(5)", calc.CssText);
        }

        [Test]
        public void CalcSimpleLength()
        {
            var source = new StringSource(@"calc(5px)");
            var calc = CalcParser.ParseCalc(source);
            Assert.AreEqual("calc(5px)", calc.CssText);
        }

        [Test]
        public void CalcSimpleAddition()
        {
            var source = new StringSource(@"calc(5 + 3)");
            var calc = CalcParser.ParseCalc(source);
            Assert.AreEqual("calc(5 + 3)", calc.CssText);
        }

        [Test]
        public void CalcAdditionWithBrackets()
        {
            var source = new StringSource(@"calc(5 + (3 + 7))");
            var calc = CalcParser.ParseCalc(source);
            Assert.AreEqual("calc(5 + 3 + 7)", calc.CssText);
        }

        [Test]
        public void CalcAdditionWithCalcBracket()
        {
            var source = new StringSource(@"calc(5 + calc(3 + 7))");
            var calc = CalcParser.ParseCalc(source);
            Assert.AreEqual("calc(5 + 3 + 7)", calc.CssText);
        }

        [Test]
        public void CalcComplexAddition()
        {
            var source = new StringSource(@"calc(50% + 3px / 2em + -1cm)");
            var calc = CalcParser.ParseCalc(source);
            Assert.AreEqual("calc(50% + 3px / 2em + -1cm)", calc.CssText);
        }

        [Test]
        public void AngleCanBeUsedWithCalc()
        {
            var source = @"transform: rotate(calc(45deg *2))";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rotate(calc(45deg * 2))", property.Value);
        }

        [Test]
        public void TimeCanBeUsedWithCalc()
        {
            var source = @"transition-duration: calc(30ms + 2s)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("calc(30ms + 2s)", property.Value);
        }

        [Test]
        public void LengthCanBeUsedWithCalc()
        {
            var source = @"width: calc(50% - 200px)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("calc(50% - 200px)", property.Value);
        }

        [Test]
        public void NumberCanBeUsedWithCalc()
        {
            var source = @"flex-shrink: calc(20/6)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("calc(20 / 6)", property.Value);
        }

        [Test]
        public void IntegerCanBeUsedWithCalc()
        {
            var source = @"column-count: calc(21  +  5 -  4  *   2)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("calc(21 + 5 - 4 * 2)", property.Value);
        }
    }
}
