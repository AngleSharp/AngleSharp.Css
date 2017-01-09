namespace AngleSharp.Css.Tests.Functions
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class CssHelpersTests
    {
        [Test]
        public void EscapeEmptyString()
        {
            var str = "";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("", escaped);
        }

        [Test]
        public void EscapeSimpleIdentifier()
        {
            var str = "abc";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("abc", escaped);
        }

        [Test]
        public void EscapeSingleMinus()
        {
            var str = "-";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("\\-", escaped);
        }

        [Test]
        public void EscapeMinusIdentifier()
        {
            var str = "-bc";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("-bc", escaped);
        }

        [Test]
        public void EscapeIntegerNumber()
        {
            var str = "123";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("\\31 23", escaped);
        }

        [Test]
        public void EscapeFloatingNumber()
        {
            var str = "1.23";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("\\31 \\.23", escaped);
        }

        [Test]
        public void EscapeEscapedZero()
        {
            var str = "\0";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("\ufffd", escaped);
        }

        [Test]
        public void EscapeZeroNumber()
        {
            var str = "0";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual(@"\30 ", escaped);
        }

        [Test]
        public void EscapeDecrementOperator()
        {
            var str = "--a";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual("--a", escaped);
        }

        [Test]
        public void EscapeDifferentBrackets()
        {
            var str = "()[]{}";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual(@"\(\)\[\]\{\}", escaped);
        }

        [Test]
        public void EscapeDotAndHashSymbol()
        {
            var str = ".foo#bar";
            var escaped = CssHelpers.Escape(str);
            Assert.AreEqual(@"\.foo\#bar", escaped);
        }
    }
}
