namespace AngleSharp.Css.Painting
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Draws the images of paint() functions with the paint definitions registered in the
    /// paint worklet of the document. AngleSharp.Css does no layout, so the size of the
    /// image is given by the caller, e.g., a renderer that knows the box of the element.
    /// https://www.w3.org/TR/css-paint-api-1/#drawing-an-image
    /// </summary>
    public static class PaintImageExtensions
    {
        // the name the arguments are matched and reified with, as for a custom property
        private const String ArgumentName = "--paint-argument";

        /// <summary>
        /// Draws the image of the paint() function for the element.
        /// </summary>
        /// <param name="element">The element the image is drawn for.</param>
        /// <param name="paint">The paint() function.</param>
        /// <param name="size">The size of the image in CSS pixels.</param>
        /// <returns>
        /// The image or null if it is invalid, i.e., if no paint definition has been
        /// registered under its name, its arguments do not match the input arguments,
        /// or the paint definition failed. An invalid image is drawn as transparent black.
        /// </returns>
        public static PaintImage? DrawPaintImage(this IElement element, CssPaintValue paint, PaintSize size) =>
            element.DrawPaintImage(paint, size, null);

        /// <summary>
        /// Draws the image of the paint() function for the element, whose input properties
        /// are taken from the given computed style, e.g., the one of a render tree.
        /// </summary>
        /// <param name="element">The element the image is drawn for.</param>
        /// <param name="paint">The paint() function.</param>
        /// <param name="size">The size of the image in CSS pixels.</param>
        /// <param name="computedStyle">The computed style of the element, if known.</param>
        /// <returns>The image or null if it is invalid.</returns>
        public static PaintImage? DrawPaintImage(this IElement element, CssPaintValue paint, PaintSize size, ICssStyleDeclaration? computedStyle)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            if (paint is null)
                throw new ArgumentNullException(nameof(paint));

            if (size is null)
                throw new ArgumentNullException(nameof(size));

            var document = element.Owner;
            var worklet = document is not null ? PaintWorklet.Find(document) : null;
            var registration = worklet?.Find(paint.PainterName);

            if (registration is null)
            {
                return null;
            }

            var arguments = GetArguments(registration, paint);

            if (arguments is null)
            {
                return null;
            }

            var style = computedStyle is not null ? ForScript(computedStyle) : GetComputedStyle(element);
            var properties = new PaintInputPropertyMap(new StylePropertyMapReadOnly(style, document!.Context, true, document), registration.InputProperties);
            var ratio = worklet!.DevicePixelRatio;
            var factory = document.Context?.GetService<IPaintRenderingContextFactory>();
            var options = new PaintRenderingContext2DSettings { Alpha = registration.ContextOptions.Alpha };

            try
            {
                var context = factory?.Create(size, ratio, options) ?? new RecordingPaintRenderingContext2D(size, ratio, options);
                registration.Definition.Paint(context, size, properties, arguments);
                return new PaintImage(paint, size, ratio, context);
            }
            catch (Exception)
            {
                // a paint definition that throws gives an invalid image (as with script)
                return null;
            }
        }

        /// <summary>
        /// Draws the images of the paint() functions in the computed value of the property
        /// of the element, e.g., of background-image, all with the same size.
        /// </summary>
        /// <param name="element">The element the images are drawn for.</param>
        /// <param name="propertyName">The name of the property, e.g., background-image.</param>
        /// <param name="size">The size of the images in CSS pixels.</param>
        /// <returns>
        /// One entry per paint() function in the order of the value, which is null if
        /// the image is invalid.
        /// </returns>
        public static IReadOnlyList<PaintImage?> DrawPaintImages(this IElement element, String propertyName, PaintSize size)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            return DrawPaintImages(element, GetComputedStyle(element), propertyName, size);
        }

        /// <summary>
        /// Draws the images of the paint() functions in the computed value of the property
        /// of the rendered element, e.g., of background-image, all with the same size.
        /// </summary>
        /// <param name="node">The rendered element the images are drawn for.</param>
        /// <param name="propertyName">The name of the property, e.g., background-image.</param>
        /// <param name="size">The size of the images in CSS pixels.</param>
        /// <returns>
        /// One entry per paint() function in the order of the value, which is null if
        /// the image is invalid.
        /// </returns>
        public static IReadOnlyList<PaintImage?> DrawPaintImages(this ElementRenderNode node, String propertyName, PaintSize size)
        {
            if (node is null)
                throw new ArgumentNullException(nameof(node));

            return DrawPaintImages(node.Ref, ForScript(node.ComputedStyle), propertyName, size);
        }

        /// <summary>
        /// Gets the paint() functions of the value, e.g., of the layers of a background-image
        /// or the options of an image-set.
        /// </summary>
        /// <param name="value">The value to search.</param>
        /// <returns>The paint() functions in the order of the value.</returns>
        public static IEnumerable<CssPaintValue> GetPaintValues(this ICssValue? value)
        {
            switch (value)
            {
                case CssPaintValue paint:
                    yield return paint;
                    break;
                case ICssMultipleValue multiple:
                    foreach (var item in multiple)
                    {
                        foreach (var paint in item.GetPaintValues())
                        {
                            yield return paint;
                        }
                    }
                    break;
                case ICssFunctionValue function when function.Arguments is not null:
                    foreach (var argument in function.Arguments)
                    {
                        foreach (var paint in argument.GetPaintValues())
                        {
                            yield return paint;
                        }
                    }
                    break;
            }
        }

        private static IReadOnlyList<PaintImage?> DrawPaintImages(IElement element, ICssStyleDeclaration style, String propertyName, PaintSize size)
        {
            if (propertyName is null)
                throw new ArgumentNullException(nameof(propertyName));

            var value = style.GetProperty(propertyName)?.RawValue;
            return value.GetPaintValues().Select(paint => element.DrawPaintImage(paint, size, style)).ToArray();
        }

        private static ICssStyleDeclaration GetComputedStyle(IElement element)
        {
            var window = element.Owner?.DefaultView;
            return window is not null ? window.GetComputedStyleOfScript(element) : ForScript(element.ComputeCurrentStyle());
        }

        // a property that was not declared has its initial value, as script sees it
        private static ICssStyleDeclaration ForScript(ICssStyleDeclaration style) =>
            style is CssStyleDeclaration declaration ? declaration.WithInitialValues(null!) : style;

        private static IReadOnlyList<CssStyleValue>? GetArguments(PaintRegistration registration, CssPaintValue paint)
        {
            var arguments = paint.Arguments;
            var syntaxes = registration.ArgumentSyntaxes;

            // without input arguments, the arguments are given as written
            if (syntaxes.Length == 0)
            {
                return arguments.Select(m => (CssStyleValue)CssUnparsedValue.FromText(m.CssText)).ToArray();
            }

            if (syntaxes.Length != arguments.Length)
            {
                return null;
            }

            var result = new CssStyleValue[arguments.Length];

            for (var i = 0; i < arguments.Length; i++)
            {
                var text = arguments[i].CssText;
                var syntax = syntaxes[i];

                if (syntax.IsUniversal)
                {
                    result[i] = CssUnparsedValue.FromText(text);
                    continue;
                }

                var argument = new CssPropertyRegistration(ArgumentName, syntax, false, null);
                var value = argument.MatchDeclared(new CssAnyValue(text));

                if (value is null)
                {
                    return null;
                }

                var values = CssStyleValueReifier.ReifyAll(ArgumentName, value, argument);
                result[i] = values.Count == 1 ? values[0] : CssUnparsedValue.FromText(text);
            }

            return result;
        }
    }
}
