namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;

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
            return config.With(mockRequester).WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true);
        }

        public static IConfiguration WithPageRequester(this IConfiguration config, Boolean enableNavigation = true, Boolean enableResourceLoading = false)
        {
            return config.With(new PageRequester()).WithDefaultLoader(setup =>
            {
                setup.IsNavigationEnabled = enableNavigation;
                setup.IsResourceLoadingEnabled = enableResourceLoading;
            });
        }
    }
}
