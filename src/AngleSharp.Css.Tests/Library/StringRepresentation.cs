namespace AngleSharp.Css.Tests.Library
{
    using AngleSharp.Css.Parser;
    using NUnit.Framework;
    using System.IO;

    [TestFixture]
    public class StringRepresentationTests
    {
        [Test]
        public void PrettyStyleFormatterStringifyShouldWork_Issue41()
        {
            var text = "@media (min-width: 800px) { .ad_column { width: 728px; height: 90px } }";
            var parser = new CssParser();
            var document = parser.ParseStyleSheet(text);

            using (var stringWriter = new StringWriter())
            {
                document.ToCss(stringWriter, new PrettyStyleFormatter());
                Assert.AreEqual("@media (min-width: 800px) { \n\t.ad_column {\n\t\twidth: 728px;\n\t\theight: 90px;\n\t}\n}", stringWriter.ToString());
            }
        }
    }
}
