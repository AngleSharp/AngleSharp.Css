namespace AngleSharp.Css.Tests.Parsing
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using NUnit.Framework;

    [TestFixture]
    public class CssTokenizationTests
    {
        [Test]
        public void CssParserIdentifier()
        {
            var teststring = "h1 { background: blue; }";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Type, Is.EqualTo(CssTokenType.Ident));
        }

        [Test]
        public void CssParserAtRule()
        {
            var teststring = "@media { background: blue; }";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Type, Is.EqualTo(CssTokenType.AtKeyword));
        }

        [Test]
        public void CssParserUrlUnquoted()
        {
            var url = "http://someurl";
            var teststring = "url(" + url + ")";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Data, Is.EqualTo(url));
        }

        [Test]
        public void CssParserUrlDoubleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url(\"" + url + "\")";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Data, Is.EqualTo(url));
        }

        [Test]
        public void CssParserUrlSingleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url('" + url + "')";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Data, Is.EqualTo(url));
        }

        [Test]
        public void CssTokenizerOnlyCarriageReturn()
        {
            var teststring = "\r";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Data, Is.EqualTo("\n"));
        }

        [Test]
        public void CssTokenizerCarriageReturnLineFeed()
        {
            var teststring = "\r\n";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Data, Is.EqualTo("\n"));
        }

        [Test]
        public void CssTokenizerOnlyLineFeed()
        {
            var teststring = "\n";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.That(token.Data, Is.EqualTo("\n"));
        }
    }
}
