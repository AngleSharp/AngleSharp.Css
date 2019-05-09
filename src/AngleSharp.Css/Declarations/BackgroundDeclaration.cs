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
            PropertyNames.BackgroundColor,
            PropertyNames.BackgroundImage,
            PropertyNames.BackgroundAttachment,
            PropertyNames.BackgroundClip,
            PropertyNames.BackgroundPositionX,
            PropertyNames.BackgroundPositionY,
            PropertyNames.BackgroundOrigin,
            PropertyNames.BackgroundRepeatX,
            PropertyNames.BackgroundRepeatY,
            PropertyNames.BackgroundSize,
        };

        sealed class BackgroundValueConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var layers = new List<CssBackgroundLayerValue>();
                var color = default(ICssValue);
                var pos = 0;
                var c = source.SkipSpacesAndComments();

                while (!source.IsDone && color == null)
                {
                    if (layers.Count > 0)
                    {
                        if (c != Symbols.Comma)
                        {
                            return null;
                        }

                        c = source.SkipCurrentAndSpaces();
                    }

                    var image = default(ICssImageValue);
                    var position = default(Point?);
                    var size = default(CssBackgroundSizeValue);
                    var repeat = default(CssImageRepeatsValue);
                    var attachment = default(ICssValue);
                    var origin = default(ICssValue);
                    var clip = default(ICssValue);

                    do
                    {
                        pos = source.Index;

                        if (image == null)
                        {
                            image = source.ParseImageSource();
                            c = source.SkipSpacesAndComments();
                        }

                        if (position == null)
                        {
                            position = source.ParsePoint();
                            c = source.SkipSpacesAndComments();

                            if (c == Symbols.Solidus && size == null)
                            {
                                c = source.SkipSpacesAndComments();
                                size = source.ParseSize();
                                c = source.SkipSpacesAndComments();
                            }
                        }

                        if (repeat == null)
                        {
                            repeat = source.ParseBackgroundRepeat();
                            c = source.SkipSpacesAndComments();
                        }

                        if (attachment == null)
                        {
                            attachment = source.ParseConstant(Map.BackgroundAttachments);
                            c = source.SkipSpacesAndComments();
                        }

                        if (origin == null)
                        {
                            origin = source.ParseConstant(Map.BoxModels);
                            c = source.SkipSpacesAndComments();
                        }

                        if (clip == null)
                        {
                            clip = source.ParseConstant(Map.BoxModels);
                            c = source.SkipSpacesAndComments();
                        }

                        if (color == null)
                        {
                            color = ColorParser.ParseColor(source);
                            c = source.SkipSpacesAndComments();
                        }
                    }
                    while (pos != source.Index);
                    
                    layers.Add(new CssBackgroundLayerValue(
                        image,
                        position,
                        size,
                        repeat,
                        attachment,
                        origin,
                        clip));
                }

                return new CssBackgroundValue(new CssListValue(layers.OfType<ICssValue>().ToArray()), color);
            }
        }

        sealed class BackgroundAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new BackgroundValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var color = values[0];
                var image = values[1];
                var attachment = values[2];
                var clip = values[3];
                var positionX = values[4];
                var positionY = values[5];
                var origin = values[6];
                var repeatX = values[7];
                var repeatY = values[8];
                var size = values[9];

                var layers = CreateLayers(image as CssListValue, attachment as CssListValue, clip as CssListValue, positionX as CssListValue, positionY as CssListValue, origin as CssListValue, repeatX as CssListValue, repeatY as CssListValue, size as CssListValue);

                if (color != null || layers != null)
                {
                    return new CssBackgroundValue(layers, color);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssBackgroundValue background)
                {
                    return new[]
                    {
                        background.Color,
                        CreateMultiple(background, m => m.Image),
                        CreateMultiple(background, m => m.Attachment),
                        CreateMultiple(background, m => m.Clip),
                        CreateMultiple(background, m => m.Position.HasValue ? m.Position.Value.X : new Nullable<Length>()),
                        CreateMultiple(background, m => m.Position.HasValue ? m.Position.Value.Y : new Nullable<Length>()),
                        CreateMultiple(background, m => m.Origin),
                        CreateMultiple(background, m => m.Repeat?.Horizontal),
                        CreateMultiple(background, m => m.Repeat?.Vertical),
                        CreateMultiple(background, m => m.Size),
                    };
                }

                return null;
            }
            
            private static ICssValue CreateLayers(CssListValue image, CssListValue attachment, CssListValue clip, CssListValue positionX, CssListValue positionY, CssListValue origin, CssListValue repeatX, CssListValue repeatY, CssListValue size)
            {
                if (image != null)
                {
                    var layers = new ICssValue[image.Items.Length];

                    for (var i = 0; i < image.Items.Length; i++)
                    {
                        var px = GetValue(positionX, i);
                        var py = GetValue(positionY, i);
                        var rx = GetValue(repeatX, i);
                        var ry = GetValue(repeatY, i);
                        layers[i] = new CssBackgroundLayerValue(
                            image.Items[i] as ICssImageValue,
                            px == null && py == null ? new Nullable<Point>() : new Point(px as Length? ?? Length.Zero, py as Length? ?? Length.Zero),
                            GetValue(size, i) as CssBackgroundSizeValue,
                            rx == null && ry == null ? null : new CssImageRepeatsValue(rx, ry),
                            GetValue(attachment, i),
                            GetValue(origin, i),
                            GetValue(clip, i));
                    }

                    return new CssListValue(layers);
                }

                return null;
            }

            private static ICssValue GetValue(CssListValue container, Int32 index)
            {
                if (container != null && index < container.Items.Length)
                {
                    return container.Items[index];
                }

                return null;
            }

            private static ICssValue CreateMultiple(CssBackgroundValue background, Func<CssBackgroundLayerValue, ICssValue> getValue)
            {
                if (background.Layers is CssListValue layers)
                {
                    var values = layers.Items.OfType<CssBackgroundLayerValue>().Select(getValue);

                    if (values.Any(m => m != null))
                    {
                        return new CssListValue(values.ToArray());
                    }
                }

                return new CssInitialValue<Object>(null);
            }
        }
    }
}
