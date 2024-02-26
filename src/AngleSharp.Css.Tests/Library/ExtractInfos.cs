namespace AngleSharp.Css.Tests.Library
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class ExtractInfosTests
    {
        [Test]
        public void GetFontUrl_Issue126()
        {
            var css = @"@font-face {
  font-family: 'Inter';
  font-style: normal;
  font-weight: 400;
  src: url(https://example.org/some-font.ttf) format('truetype');
}";
            var html = $"<style>{css}</style>";
            var document = html.ToHtmlDocument(Configuration.Default.WithCss());
            var style = document.QuerySelector<IHtmlStyleElement>("style");
            var sheet = style.Sheet as ICssStyleSheet;
            var fontFace = sheet.Rules[0] as ICssFontFaceRule;
            var src = fontFace.GetProperty("src").RawValue;
            var url = ((src as ICssMultipleValue)[0] as ICssMultipleValue)[0] as CssUrlValue;
            Assert.AreEqual("https://example.org/some-font.ttf", url.Path);
        }
    }
}
