namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class BackgroundDeclaration
    {
        public static String Name = PropertyNames.Background;

        public static IValueConverter Converter = new BackgroundAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BackgroundAttachment,
            PropertyNames.BackgroundClip,
            PropertyNames.BackgroundColor,
            PropertyNames.BackgroundImage,
            PropertyNames.BackgroundOrigin,
            PropertyNames.BackgroundPositionX,
            PropertyNames.BackgroundPositionY,
            PropertyNames.BackgroundSize,
            PropertyNames.BackgroundRepeatX,
            PropertyNames.BackgroundRepeatY,
        };

        sealed class BackgroundValueConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var layers = new List<BackgroundLayer>();
                var color = default(ICssValue);
                var pos = 0;
                var c = source.SkipSpacesAndComments();

                while (!source.IsDone && color == null)
                {
                    var layer = new BackgroundLayer();

                    if (layers.Count > 0)
                    {
                        if (c != Symbols.Comma)
                        {
                            return null;
                        }

                        c = source.SkipCurrentAndSpaces();
                    }

                    do
                    {
                        pos = source.Index;

                        if (layer.Source == null)
                        {
                            layer.Source = source.ParseImageSource();
                            c = source.SkipSpacesAndComments();
                        }

                        if (layer.Position == null)
                        {
                            layer.Position = source.ParsePoint();
                            c = source.SkipSpacesAndComments();

                            if (c == Symbols.Solidus && layer.Size == null)
                            {
                                c = source.SkipSpacesAndComments();
                                layer.Size = source.ParseSize();
                                c = source.SkipSpacesAndComments();
                            }
                        }

                        if (layer.Repeat == null)
                        {
                            layer.Repeat = source.ParseBackgroundRepeat();
                            c = source.SkipSpacesAndComments();
                        }

                        if (layer.Attachment == null)
                        {
                            layer.Attachment = source.ParseConstant(Map.BackgroundAttachments);
                            c = source.SkipSpacesAndComments();
                        }

                        if (layer.Origin == null)
                        {
                            layer.Origin = source.ParseConstant(Map.BoxModels);
                            c = source.SkipSpacesAndComments();
                        }

                        if (layer.Clip == null)
                        {
                            layer.Clip = source.ParseConstant(Map.BoxModels);
                            c = source.SkipSpacesAndComments();
                        }

                        if (color == null)
                        {
                            color = ColorParser.ParseColor(source);
                            c = source.SkipSpacesAndComments();
                        }
                    }
                    while (pos != source.Index);

                    layers.Add(layer);
                }

                return new Background(new Multiple(layers.OfType<ICssValue>().ToArray()), color);
            }
        }

        sealed class BackgroundAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new BackgroundValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

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
                    return CreateProperties(
                        background.Color,
                        CreateMultiple(background, m => m.Source),
                        CreateMultiple(background, m => m.Attachment),
                        CreateMultiple(background, m => m.Clip),
                        CreateMultiple(background, m => m.Position.HasValue ? m.Position.Value.X : new Nullable<Length>()),
                        CreateMultiple(background, m => m.Position.HasValue ? m.Position.Value.Y : new Nullable<Length>()),
                        CreateMultiple(background, m => m.Origin),
                        CreateMultiple(background, m => m.Repeat.HasValue ? m.Repeat.Value.Horizontal : null),
                        CreateMultiple(background, m => m.Repeat.HasValue ? m.Repeat.Value.Vertical : null),
                        CreateMultiple(background, m => m.Size));
                }

                return null;
            }

            private static IEnumerable<ICssProperty> CreateProperties(ICssValue color, ICssValue image, ICssValue attachment, ICssValue clip, ICssValue positionX, ICssValue positionY, ICssValue origin, ICssValue repeatX, ICssValue repeatY, ICssValue size)
            {
                return new[]
                {
                    new CssProperty(BackgroundColorDeclaration.Name, BackgroundColorDeclaration.Converter, BackgroundColorDeclaration.Flags, color),
                    new CssProperty(BackgroundImageDeclaration.Name, BackgroundImageDeclaration.Converter, BackgroundImageDeclaration.Flags, image),
                    new CssProperty(BackgroundAttachmentDeclaration.Name, BackgroundAttachmentDeclaration.Converter, BackgroundAttachmentDeclaration.Flags, attachment),
                    new CssProperty(BackgroundClipDeclaration.Name, BackgroundClipDeclaration.Converter, BackgroundClipDeclaration.Flags, clip),
                    new CssProperty(BackgroundPositionXDeclaration.Name, BackgroundPositionXDeclaration.Converter, BackgroundPositionXDeclaration.Flags, positionX),
                    new CssProperty(BackgroundPositionYDeclaration.Name, BackgroundPositionYDeclaration.Converter, BackgroundPositionYDeclaration.Flags, positionY),
                    new CssProperty(BackgroundOriginDeclaration.Name, BackgroundOriginDeclaration.Converter, BackgroundOriginDeclaration.Flags, origin),
                    new CssProperty(BackgroundRepeatXDeclaration.Name, BackgroundRepeatXDeclaration.Converter, BackgroundRepeatXDeclaration.Flags, repeatX),
                    new CssProperty(BackgroundRepeatYDeclaration.Name, BackgroundRepeatYDeclaration.Converter, BackgroundRepeatYDeclaration.Flags, repeatY),
                    new CssProperty(BackgroundSizeDeclaration.Name, BackgroundSizeDeclaration.Converter, BackgroundSizeDeclaration.Flags, size),
                };
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
}
