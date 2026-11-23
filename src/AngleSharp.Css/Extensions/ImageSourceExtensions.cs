namespace AngleSharp.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Media;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Picks the source of an image for a render device (see "select an image source" in
    /// HTML): the first source of the picture around it whose type is supported and whose
    /// media matches, then the candidate of its srcset whose density fits the one of the
    /// device best, with the size of the image given by the sizes attribute.
    /// </summary>
    public static class ImageSourceExtensions
    {
        /// <summary>
        /// Gets the source of the image to show for the image element, picked for the render
        /// device of its document (the default device, if there is none).
        /// </summary>
        /// <param name="img">The image element.</param>
        /// <returns>The source of the image, or null if it has none.</returns>
        public static ImageSourceSelection? GetImageSource(this IHtmlImageElement img)
        {
            var device = img.Owner?.Context?.GetService<IRenderDevice>() ?? new DefaultRenderDevice();
            return img.GetImageSource(device);
        }

        /// <summary>
        /// Gets the source of the image to show for the image element on the given device.
        /// </summary>
        /// <param name="img">The image element.</param>
        /// <param name="device">The device the image is shown on.</param>
        /// <returns>The source of the image, or null if it has none.</returns>
        public static ImageSourceSelection? GetImageSource(this IHtmlImageElement img, IRenderDevice device)
        {
            if (img is null)
            {
                throw new ArgumentNullException(nameof(img));
            }

            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            var context = img.Owner?.Context;
            var sources = img.GetSources();

            while (sources.Count > 0)
            {
                var source = sources.Pop();
                var type = source.Type;
                var media = source.Media;

                if ((String.IsNullOrEmpty(type) || context?.GetResourceService<IImageInfo>(type!) is not null) &&
                    (String.IsNullOrEmpty(media) || Matches(context, media!, device)) &&
                    SelectCandidate(source.SourceSet, source.Sizes, null, context, device) is { } selected)
                {
                    return Resolve(img, selected.Key, selected.Value);
                }
            }

            var candidate = SelectCandidate(img.SourceSet, img.Sizes, img.GetAttribute("src"), context, device);
            return candidate is { } found ? Resolve(img, found.Key, found.Value) : null;
        }

        private static ImageSourceSelection? Resolve(IHtmlImageElement img, String source, Double density)
        {
            var url = img.BaseUrl is { } baseUrl ? new Url(baseUrl, source) : new Url(source);
            return url.IsInvalid ? null : new ImageSourceSelection(url, density);
        }

        private static KeyValuePair<String, Double>? SelectCandidate(String? srcset, String? sizes, String? src, IBrowsingContext? context, IRenderDevice device)
        {
            var candidates = new List<KeyValuePair<String, Double>>();
            var sourceSize = default(Double?);
            var hasWidth = false;
            var hasDensityOne = false;

            foreach (var (url, descriptors) in ParseSourceSet(srcset))
            {
                var density = 1.0;

                foreach (var descriptor in descriptors)
                {
                    var number = descriptor.Substring(0, descriptor.Length - 1);

                    switch (descriptor[descriptor.Length - 1])
                    {
                        case 'w':
                            sourceSize ??= GetSourceSize(sizes, context, device);
                            density = sourceSize > 0 ? number.ToDouble(0.0) / sourceSize.Value : 1.0;
                            hasWidth = true;
                            break;
                        case 'x':
                            density = number.ToDouble(1.0);
                            break;
                    }
                }

                hasDensityOne |= density == 1.0;
                candidates.Add(new KeyValuePair<String, Double>(url, density));
            }

            // the src is the candidate of density 1, unless the srcset has one (or sizes)
            if (src is { Length: > 0 } && !hasWidth && !hasDensityOne)
            {
                candidates.Add(new KeyValuePair<String, Double>(src, 1.0));
            }

            if (candidates.Count == 0)
            {
                return null;
            }

            // the least dense candidate as dense as the device's pixels, else the densest one
            var ratio = device.Resolution > 0 ? device.Resolution / 96.0 : 1.0;
            var best = default(KeyValuePair<String, Double>?);
            var densest = candidates[0];

            foreach (var candidate in candidates)
            {
                if (candidate.Value >= ratio && (best is null || candidate.Value < best.Value.Value))
                {
                    best = candidate;
                }

                if (candidate.Value > densest.Value)
                {
                    densest = candidate;
                }
            }

            return best ?? densest;
        }

        // the width of the image in CSS pixels: the length of the first size whose media
        // matches, else the width of the viewport (100vw)
        private static Double GetSourceSize(String? sizes, IBrowsingContext? context, IRenderDevice device)
        {
            if (sizes is { Length: > 0 })
            {
                foreach (var size in SplitTopLevel(sizes))
                {
                    var (media, length) = SplitSize(size.Trim());

                    if (length.Length > 0 && (media.Length == 0 || Matches(context, media, device)) &&
                        ValueConverters.LengthConverter.Convert(length) is { } value &&
                        value.AsPx(device, RenderMode.Horizontal) is var pixels && pixels > 0)
                    {
                        return pixels;
                    }
                }
            }

            return device.ViewPortWidth;
        }

        private static Boolean Matches(IBrowsingContext? context, String media, IRenderDevice device)
        {
            // without a context (e.g. a document of its own) media are not looked at
            if (context is null)
            {
                return true;
            }

            var list = new MediaList(context);
            list.SetMediaText(media, throwOnError: false);
            return list.Validate(device);
        }

        // the length of a size is its last component (a dimension or a function, e.g. calc),
        // what is in front of it the media condition
        private static (String Media, String Length) SplitSize(String size)
        {
            if (size.EndsWith(")", StringComparison.Ordinal))
            {
                var depth = 0;

                for (var index = size.Length - 1; index >= 0; index--)
                {
                    if (size[index] == ')')
                    {
                        depth++;
                    }
                    else if (size[index] == '(' && --depth == 0)
                    {
                        var start = index;

                        while (start > 0 && (Char.IsLetterOrDigit(size[start - 1]) || size[start - 1] == '-'))
                        {
                            start--;
                        }

                        // a parenthesized media condition alone has no length
                        return start == index ? (size, String.Empty) : (size.Substring(0, start).Trim(), size.Substring(start));
                    }
                }

                return (size, String.Empty);
            }

            var space = size.LastIndexOfAny(new[] { ' ', '\t', '\n', '\r', '\f' });
            return space < 0 ? (String.Empty, size) : (size.Substring(0, space).Trim(), size.Substring(space + 1));
        }

        private static List<String> SplitTopLevel(String sizes)
        {
            var parts = new List<String>();
            var depth = 0;
            var start = 0;

            for (var index = 0; index < sizes.Length; index++)
            {
                switch (sizes[index])
                {
                    case '(':
                        depth++;
                        break;
                    case ')':
                        depth--;
                        break;
                    case ',' when depth == 0:
                        parts.Add(sizes.Substring(start, index - start));
                        start = index + 1;
                        break;
                }
            }

            parts.Add(sizes.Substring(start));
            return parts;
        }

        // the candidates of a srcset (HTML 4.8.4.3.10): a URL (commas at its end separate it
        // from the next one), then descriptors up to a comma outside of parentheses
        private static IEnumerable<(String Url, List<String> Descriptors)> ParseSourceSet(String? srcset)
        {
            if (srcset is null)
            {
                yield break;
            }

            var position = 0;

            while (position < srcset.Length)
            {
                while (position < srcset.Length && (srcset[position].IsSpaceCharacter() || srcset[position] == ','))
                {
                    position++;
                }

                var start = position;

                while (position < srcset.Length && !srcset[position].IsSpaceCharacter())
                {
                    position++;
                }

                if (position == start)
                {
                    yield break;
                }

                var url = srcset.Substring(start, position - start);
                var descriptors = new List<String>();

                if (url.EndsWith(",", StringComparison.Ordinal))
                {
                    url = url.TrimEnd(',');
                }
                else
                {
                    var depth = 0;
                    var descriptor = new System.Text.StringBuilder();

                    for (; position < srcset.Length; position++)
                    {
                        var c = srcset[position];

                        if (c == '(')
                        {
                            depth++;
                        }
                        else if (c == ')')
                        {
                            depth--;
                        }
                        else if (depth == 0 && (c == ',' || c.IsSpaceCharacter()))
                        {
                            if (descriptor.Length > 0)
                            {
                                descriptors.Add(descriptor.ToString());
                                descriptor.Clear();
                            }

                            if (c == ',')
                            {
                                position++;
                                break;
                            }

                            continue;
                        }

                        descriptor.Append(c);
                    }

                    if (descriptor.Length > 0)
                    {
                        descriptors.Add(descriptor.ToString());
                    }
                }

                if (url.Length > 0)
                {
                    descriptors.RemoveAll(static descriptor => descriptor.Length < 2);
                    yield return (url, descriptors);
                }
            }
        }
    }
}
