namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Tests.Mocks;
    using NUnit.Framework;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class AsyncParsingTests
    {
        [Test]
        public async Task TestAsyncCssParsingFromStream()
        {
            var text = "h1 { color: red; } h2 { color: blue; } p { font-family: Arial; } div { margin: 10 }";
            var source = new DelayedStream(Encoding.UTF8.GetBytes(text));
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var parser = context.GetService<ICssParser>();

            using (var task = parser.ParseStyleSheetAsync(source))
            {
                Assert.IsFalse(task.IsCompleted);
                var result = await task;

                Assert.IsTrue(task.IsCompleted);
                Assert.AreEqual(4, result.Rules.Length);
            }
        }

        [Test]
        public async Task TestAsyncCssParsingFromString()
        {
            var source = "h1 { color: red; } h2 { color: blue; } p { font-family: Arial; } div { margin: 10 }";
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            var parser = context.GetService<ICssParser>();

            using (var task = parser.ParseStyleSheetAsync(source))
            {
                Assert.IsTrue(task.IsCompleted);
                var result = await task;

                Assert.AreEqual(result, result);
                Assert.AreEqual(4, result.Rules.Length);
            }
        }
    }
}
