#nullable disable
namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for the rendering nodes
    /// </summary>
    public static class RenderNodeExtensions
    {
        private static readonly String[] ResourceProperties =
        {
            PropertyNames.BackgroundImage,
            PropertyNames.BorderImageSource,
            PropertyNames.ListStyleImage,
            PropertyNames.Cursor,
        };

        /// <summary>
        /// Downloads the referenced resources from the node if visible.
        ///
        /// Included resources:
        /// 
        /// - Background images
        /// - Border images
        /// - List style images
        /// - Custom cursor images
        /// </summary>
        /// <param name="node">The node to use as a starting base.</param>
        /// <param name="cancellationToken">The cancellation token to use, if any.</param>
        public static Task DownloadResources(this IRenderNode node, CancellationToken cancellationToken = default)
        {
            var context = node.Ref.Owner?.Context ?? throw new InvalidOperationException("The node needs to be inside a browsing context.");
            var loader = context.GetService<IResourceLoader>() ?? throw new InvalidOperationException("A resource loader is required. Check your configuration.");
            var tasks = new List<Task>();
            var requestedUrls = new HashSet<String>(StringComparer.Ordinal);

            CollectResources(node);

            return Task.WhenAll(tasks);

            void CollectResources(IRenderNode renderNode)
            {
                if (!renderNode.IsVisible())
                {
                    return;
                }

                if (renderNode is ElementRenderNode element)
                {
                    var elementRef = element.Ref;
                    var style = element.ComputedStyle;

                    foreach (var propertyName in ResourceProperties)
                    {
                        var value = style.GetProperty(propertyName).RawValue;

                        foreach (var resourceUrl in GetResourceUrls(value))
                        {
                            if (requestedUrls.Add(resourceUrl))
                            {
                                var request = new ResourceRequest(elementRef, new Url(resourceUrl));
                                var download = loader.FetchAsync(request);
                                cancellationToken.Register(download.Cancel);
                                tasks.Add(download.Task);
                            }
                        }
                    }
                }

                foreach (var child in renderNode.Children)
                {
                    CollectResources(child);
                }
            }
        }

        private static IEnumerable<String> GetResourceUrls(ICssValue value)
        {
            if (value is null)
            {
                yield break;
            }

            if (value is CssUrlValue urlValue)
            {
                if (!String.IsNullOrEmpty(urlValue.Path))
                {
                    yield return urlValue.Path;
                }

                yield break;
            }

            if (value is CssCustomCursorValue customCursor)
            {
                foreach (var resourceUrl in GetResourceUrls(customCursor.Source))
                {
                    yield return resourceUrl;
                }

                yield break;
            }

            if (value is CssCursorValue cursor)
            {
                foreach (var definition in cursor.Definitions)
                {
                    foreach (var resourceUrl in GetResourceUrls(definition))
                    {
                        yield return resourceUrl;
                    }
                }

                yield break;
            }

            if (value is ICssMultipleValue multiple)
            {
                foreach (var item in multiple)
                {
                    foreach (var resourceUrl in GetResourceUrls(item))
                    {
                        yield return resourceUrl;
                    }
                }

                yield break;
            }

            if (value is ICssSpecialValue special && special.Value is not null)
            {
                foreach (var resourceUrl in GetResourceUrls(special.Value))
                {
                    yield return resourceUrl;
                }
            }
        }

        /// <summary>
        /// Checks if the provided render node is visible.
        /// </summary>
        /// <param name="node">The node to check for visibility.</param>
        /// <returns>True if its visible, otherwise false.</returns>
        public static Boolean IsVisible(this IRenderNode node)
        {
            var hasOwner = node.Ref.Owner != null;

            if (hasOwner)
            {
                if (node is ElementRenderNode element)
                {
                    var style = element.ComputedStyle;

                    if (element.Ref is IHtmlElement htmlElement && htmlElement.IsHidden)
                    {
                        return false;
                    }
                    else if (style.GetDisplay() == CssKeywords.None)
                    {
                        return false;
                    }
                    else if (style.GetVisibility() == CssKeywords.Hidden)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Finds a particular render node based on the given reference node.
        /// </summary>
        /// <param name="node">The render tree root.</param>
        /// <param name="reference">The reference node.</param>
        /// <returns>The related render tree node, if any.</returns>
        public static IRenderNode Find(this IRenderNode node, INode reference)
        {
            if (!Object.ReferenceEquals(node.Ref, reference))
            {
                return node.Children
                    .Select(child => child.Find(reference))
                    .Where(child => child != null)
                    .FirstOrDefault();
            }

            return node;
        }
    }
}
