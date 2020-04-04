namespace AngleSharp.Css.Tests.Parsing
{
    using AngleSharp.Css.Parser;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class StyleSheet
    {
        [Test]
        public async Task ParseEmptySheet_Issue42()
        {
            var sheetCode = "";
            var parser = new CssParser();
            var sheet = await parser.ParseStyleSheetAsync(sheetCode);
            Assert.IsNotNull(sheet);
        }
    }
}
