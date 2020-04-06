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
        /// <summary>
        /// Downloads the referenced resources from the node if visible.
        ///
        /// Included resources:
        /// 
        /// - Background images
        /// </summary>
        /// <param name="node">The node to use as a starting base.</param>
        /// <param name="cancellationToken">The cancellation token to use, if any.</param>
        public static Task DownloadResources(this IRenderNode node, CancellationToken cancellationToken = default)
        {
            var context = node.Ref.Owner?.Context ?? throw new InvalidOperationException("The node needs to be inside a browsing context.");
            var loader = context.GetService<IResourceLoader>() ?? throw new InvalidOperationException("A resource loader is required. Check your configuration.");
            var tasks = new List<Task>();

            if (node.IsVisible() && node is ElementRenderNode element)
            {
                var elementRef = element.Ref as IElement;
                var style = element.ComputedStyle;
                var value = style.GetProperty(PropertyNames.BackgroundImage).RawValue;

                if (value is CssListValue list)
                {
                    var url = new Url(list.AsUrl());
                    var request = new ResourceRequest(elementRef, url);
                    var download = loader.FetchAsync(request);
                    cancellationToken.Register(download.Cancel);
                    tasks.Add(download.Task);
                }
            }

            return Task.WhenAll(tasks);
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
