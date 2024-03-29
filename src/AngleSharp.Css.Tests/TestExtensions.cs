namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Reflection;

    static class TestExtensions
    {
        public static String GetTagName(this INode node)
        {
            var element = node as IElement;

            Assert.AreEqual(NodeType.Element, node.NodeType);
            Assert.IsNotNull(element);
            Assert.IsNull(element.Prefix);

            return element.LocalName;
        }

        public static IConfiguration WithMockRequester(this IConfiguration config, IRequester mockRequester)
        {
            return config
                .With(mockRequester)
                .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
        }

        public static IConfiguration WithPageRequester(this IConfiguration config, Boolean enableNavigation = true, Boolean enableResourceLoading = false)
        {
            return config
                .With(new PageRequester())
                .WithDefaultLoader(new LoaderOptions {
                    IsNavigationDisabled = !enableNavigation,
                    IsResourceLoadingEnabled = enableResourceLoading,
                });
        }

        public static IDocument ToHtmlDocument(this String sourceCode, IConfiguration configuration = null)
        {
            var context = BrowsingContext.New(configuration ?? Configuration.Default);
            var htmlParser = context.GetService<IHtmlParser>();
            return htmlParser.ParseDocument(sourceCode);
        }

        private static String GetResourceDirectory()
        {
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = new DirectoryInfo(baseDir);

            while (!path.Name.Is("AngleSharp.Css.Tests"))
            {
                path = path.Parent;
            }

            return Path.Combine(path.FullName, "Resources");
        }

        public static String LoadFromResources(this String fileName)
        {
            var directory = GetResourceDirectory();
            var path = Path.Combine(directory, fileName);
            return File.ReadAllText(path);
        }
    }
}
