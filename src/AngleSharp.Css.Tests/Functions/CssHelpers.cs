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
            Assert.That(escaped, Is.EqualTo(""));
        }

        [Test]
        public void EscapeSimpleIdentifier()
        {
            var str = "abc";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo("abc"));
        }

        [Test]
        public void EscapeSingleMinus()
        {
            var str = "-";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo("\\-"));
        }

        [Test]
        public void EscapeMinusIdentifier()
        {
            var str = "-bc";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo("-bc"));
        }

        [Test]
        public void EscapeIntegerNumber()
        {
            var str = "123";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo("\\31 23"));
        }

        [Test]
        public void EscapeFloatingNumber()
        {
            var str = "1.23";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo("\\31 \\.23"));
        }

        [Test]
        public void EscapeEscapedZero()
        {
            var str = "\0";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo("\ufffd"));
        }

        [Test]
        public void EscapeZeroNumber()
        {
            var str = "0";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo(@"\30 "));
        }

        [Test]
        public void EscapeDecrementOperator()
        {
            var str = "--a";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo("--a"));
        }

        [Test]
        public void EscapeDifferentBrackets()
        {
            var str = "()[]{}";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo(@"\(\)\[\]\{\}"));
        }

        [Test]
        public void EscapeDotAndHashSymbol()
        {
            var str = ".foo#bar";
            var escaped = CssHelpers.Escape(str);
            Assert.That(escaped, Is.EqualTo(@"\.foo\#bar"));
        }
    }
}
