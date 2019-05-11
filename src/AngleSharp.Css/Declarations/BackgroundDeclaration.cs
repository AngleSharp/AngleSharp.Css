namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class BackgroundDeclaration
    {
        public static String Name = PropertyNames.Background;

        public static IValueConverter Converter = new BackgroundAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BackgroundImage,
            PropertyNames.BackgroundPosition,
            PropertyNames.BackgroundSize,
            PropertyNames.BackgroundRepeat,
            PropertyNames.BackgroundAttachment,
            PropertyNames.BackgroundOrigin,
            PropertyNames.BackgroundClip,
            PropertyNames.BackgroundColor,
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
            private static readonly IValueConverter converter = new BackgroundValueConverter();

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var image = GetList(values[0]);
                var position = GetList(values[1]);
                var size = GetList(values[2]);
                var repeat = GetList(values[3]);
                var attachment = GetList(values[4]);
                var origin = GetList(values[5]);
                var clip = GetList(values[6]);
                var color = values[7];

                var layers = CreateLayers(image, attachment, clip, position, origin, repeat, size);

                if (color != null || layers != null)
                {
                    return new CssBackgroundValue(layers, color);
                }

                return null;
            }

            private static CssListValue GetList(ICssValue value)
            {
                if (value is CssListValue list)
                {
                    return list;
                }
                else if (value is CssInitialValue)
                {
                    return new CssListValue();
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssBackgroundValue background)
                {
                    return new[]
                    {
                        CreateMultiple(background, m => m.Image, InitialValues.BackgroundImageDecl),
                        CreateMultiple(background, m => m.Position, InitialValues.BackgroundPositionDecl),
                        CreateMultiple(background, m => m.Size, InitialValues.BackgroundSizeDecl),
                        CreateMultiple(background, m => m.Repeat, InitialValues.BackgroundRepeatDecl),
                        CreateMultiple(background, m => m.Attachment, InitialValues.BackgroundAttachmentDecl),
                        CreateMultiple(background, m => m.Origin, InitialValues.BackgroundOriginDecl),
                        CreateMultiple(background, m => m.Clip, InitialValues.BackgroundClipDecl),
                        background.Color,
                    };
                }

                return null;
            }
            
            private static ICssValue CreateLayers(CssListValue image, CssListValue attachment, CssListValue clip, CssListValue position, CssListValue origin, CssListValue repeat, CssListValue size)
            {
                var count = GetCount(image, attachment, clip, position, size, repeat, origin);

                if (count > 0)
                {
                    var layers = new ICssValue[count];

                    for (var i = 0; i < count; i++)
                    {
                        layers[i] = new CssBackgroundLayerValue(
                            GetValue(image, i),
                            GetValue(position, i),
                            GetValue(size, i),
                            GetValue(repeat, i),
                            GetValue(attachment, i),
                            GetValue(origin, i),
                            GetValue(clip, i));
                    }

                    return new CssListValue(layers);
                }

                return null;
            }

            private static Int32 GetCount(params CssListValue[] lists)
            {
                var count = 0;

                foreach (var list in lists)
                {
                    count = Math.Max(count, list?.Count ?? 0);
                }

                return count;
            }

            private static ICssValue GetValue(CssListValue container, Int32 index)
            {
                if (container != null && index < container.Items.Length)
                {
                    return container.Items[index];
                }

                return null;
            }

            private static ICssValue CreateMultiple(CssBackgroundValue background, Func<CssBackgroundLayerValue, ICssValue> getValue, ICssValue initialValue)
            {
                if (background.Layers is CssListValue layers)
                {
                    var values = layers.Items.OfType<CssBackgroundLayerValue>().Select(getValue);

                    if (values.Any(m => m != null))
                    {
                        return new CssListValue(values.ToArray());
                    }
                }

                return new CssInitialValue(initialValue);
            }
        }
    }
}
