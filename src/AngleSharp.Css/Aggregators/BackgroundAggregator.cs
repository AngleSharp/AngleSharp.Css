namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BackgroundAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var color = properties.Where(m => m.Name == BackgroundColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var image = properties.Where(m => m.Name == BackgroundImageDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var attachment = properties.Where(m => m.Name == BackgroundAttachmentDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var clip = properties.Where(m => m.Name == BackgroundClipDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var positionX = properties.Where(m => m.Name == BackgroundPositionXDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var positionY = properties.Where(m => m.Name == BackgroundPositionYDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var origin = properties.Where(m => m.Name == BackgroundOriginDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var repeatX = properties.Where(m => m.Name == BackgroundRepeatXDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var repeatY = properties.Where(m => m.Name == BackgroundRepeatYDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var size = properties.Where(m => m.Name == BackgroundSizeDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var layers = CreateLayers(image, attachment, clip, positionX, positionY, origin, repeatX, repeatY, size);

            if (color != null || layers != null)
            {
                return new Background(layers, color);
            }

            return null;
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            var background = value as Background;

            if (background != null)
            {
                return new[]
                {
                    new CssProperty(BackgroundColorDeclaration.Name, BackgroundColorDeclaration.Converter, BackgroundColorDeclaration.Flags, background.Color),
                    new CssProperty(BackgroundImageDeclaration.Name, BackgroundImageDeclaration.Converter, BackgroundImageDeclaration.Flags, CreateMultiple(background, m => m.Source)),
                    new CssProperty(BackgroundAttachmentDeclaration.Name, BackgroundAttachmentDeclaration.Converter, BackgroundAttachmentDeclaration.Flags, CreateMultiple(background, m => m.Attachment)),
                    new CssProperty(BackgroundClipDeclaration.Name, BackgroundClipDeclaration.Converter, BackgroundClipDeclaration.Flags, CreateMultiple(background, m => m.Clip)),
                    new CssProperty(BackgroundPositionXDeclaration.Name, BackgroundPositionXDeclaration.Converter, BackgroundPositionXDeclaration.Flags, CreateMultiple(background, m => m.Position.HasValue ? m.Position.Value.X : new Nullable<Length>())),
                    new CssProperty(BackgroundPositionYDeclaration.Name, BackgroundPositionYDeclaration.Converter, BackgroundPositionYDeclaration.Flags, CreateMultiple(background, m => m.Position.HasValue ? m.Position.Value.Y : new Nullable<Length>())),
                    new CssProperty(BackgroundOriginDeclaration.Name, BackgroundOriginDeclaration.Converter, BackgroundOriginDeclaration.Flags, CreateMultiple(background, m => m.Origin)),
                    new CssProperty(BackgroundRepeatXDeclaration.Name, BackgroundRepeatXDeclaration.Converter, BackgroundRepeatXDeclaration.Flags, CreateMultiple(background, m => m.Repeat.HasValue ? m.Repeat.Value.Horizontal : null)),
                    new CssProperty(BackgroundRepeatYDeclaration.Name, BackgroundRepeatYDeclaration.Converter, BackgroundRepeatYDeclaration.Flags, CreateMultiple(background, m => m.Repeat.HasValue ? m.Repeat.Value.Vertical : null)),
                    new CssProperty(BackgroundSizeDeclaration.Name, BackgroundSizeDeclaration.Converter, BackgroundSizeDeclaration.Flags, CreateMultiple(background, m => m.Size)),
                };
            }

            return null;
        }

        private static ICssValue CreateLayers(Multiple image, Multiple attachment, Multiple clip, Multiple positionX, Multiple positionY, Multiple origin, Multiple repeatX, Multiple repeatY, Multiple size)
        {
            if (image != null)
            {
                var layers = new ICssValue[image.Values.Length];

                for (var i = 0; i < image.Values.Length; i++)
                {
                    var px = GetValue(positionX, i);
                    var py = GetValue(positionY, i);
                    var rx = GetValue(repeatX, i);
                    var ry = GetValue(repeatY, i);
                    layers[i] = new BackgroundLayer
                    {
                        Attachment = GetValue(attachment, i),
                        Clip = GetValue(clip, i),
                        Origin = GetValue(origin, i),
                        Position = px == null && py == null ? new Nullable<Point>() : new Point(px as Length? ?? Length.Zero, py as Length? ?? Length.Zero),
                        Repeat = rx == null && ry == null ? new Nullable<ImageRepeats>() : new ImageRepeats(rx, ry),
                        Size = GetValue(size, i),
                        Source = image.Values[i],
                    };
                }

                return new Multiple(layers);
            }

            return null;
        }

        private static ICssValue GetValue(Multiple container, Int32 index)
        {
            if (container != null && index < container.Values.Length)
            {
                return container.Values[index];
            }

            return null;
        }

        private static ICssValue CreateMultiple(Background background, Func<BackgroundLayer, ICssValue> getValue)
        {
            var layers = background.Layers as Multiple;

            if (layers != null)
            {
                var values = layers.Values.OfType<BackgroundLayer>().Select(getValue);

                if (values.Any())
                {
                    return new Multiple(values.ToArray());
                }
            }

            return null;
        }
    }
}