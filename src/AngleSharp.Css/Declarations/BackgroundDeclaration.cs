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

        sealed class BackgroundAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                // [ <bg-layer> , ]* <final-bg-layer>
                // where:
                //   <bg-layer> = <bg-image> || <bg-position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box>
                //   <final-bg-layer> = <'background-color'> || <bg-image> || <bg-position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box>
                // where:
                //   <bg-image> = none | <image>
                //   <bg-position> = [ [ left | center | right | top | bottom | <length-percentage> ] | [ left | center | right | <length-percentage> ] [ top | center | bottom | <length-percentage> ] | [ center | [ left | right ] <length-percentage>? ] && [ center | [ top | bottom ] <length-percentage>? ] ]
                //   <bg-size> = [ <length-percentage> | auto ]{1,2} | cover | contain
                //   <repeat-style> = repeat-x | repeat-y | [ repeat | space | round | no-repeat ]{1,2}
                //   <attachment> = scroll | fixed | local
                //   <box> = border-box | padding-box | content-box
                //   <image> = <url> | <image()> | <image-set()> | <element()> | <paint()> | <cross-fade()> | <gradient>
                //   <length-percentage> = <length> | <percentage>
                //   <image()> = image( <image-tags>? [ <image-src>? , <color>? ]! )
                //   <image-set()> = image-set( <image-set-option># )
                //   <element()> = element( <id-selector> )
                //   <paint()> = paint( <ident>, <declaration-value>? )
                //   <cross-fade()> = cross-fade( <cf-mixing-image> , <cf-final-image>? )
                //   <gradient> = <linear-gradient()> | <repeating-linear-gradient()> | <radial-gradient()> | <repeating-radial-gradient()> | <conic-gradient()>
                //   <image-tags> = ltr | rtl
                //   <image-src> = <url> | <string>
                //   <color> = <rgb()> | <rgba()> | <hsl()> | <hsla()> | <hex-color> | <named-color> | currentcolor | <deprecated-system-color>
                //   <image-set-option> = [ <image> | <string> ] <resolution>
                //   <id-selector> = <hash-token>
                //   <cf-mixing-image> = <percentage>? && <image>
                //   <cf-final-image> = <image> | <color>
                //   <linear-gradient()> = linear-gradient( [ <angle> | to <side-or-corner> ]? , <color-stop-list> )
                //   <repeating-linear-gradient()> = repeating-linear-gradient( [ <angle> | to <side-or-corner> ]? , <color-stop-list> )
                //   <radial-gradient()> = radial-gradient( [ <ending-shape> || <size> ]? [ at <position> ]? , <color-stop-list> )
                //   <repeating-radial-gradient()> = repeating-radial-gradient( [ <ending-shape> || <size> ]? [ at <position> ]? , <color-stop-list> )
                //   <conic-gradient()> = conic-gradient( [ from <angle> ]? [ at <position> ]?, <angular-color-stop-list> )
                //   <rgb()> = rgb( <percentage>{3} [ / <alpha-value> ]? ) | rgb( <number>{3} [ / <alpha-value> ]? ) | rgb( <percentage>#{3} , <alpha-value>? ) | rgb( <number>#{3} , <alpha-value>? )
                //   <rgba()> = rgba( <percentage>{3} [ / <alpha-value> ]? ) | rgba( <number>{3} [ / <alpha-value> ]? ) | rgba( <percentage>#{3} , <alpha-value>? ) | rgba( <number>#{3} , <alpha-value>? )
                //   <hsl()> = hsl( <hue> <percentage> <percentage> [ / <alpha-value> ]? ) | hsl( <hue>, <percentage>, <percentage>, <alpha-value>? )
                //   <hsla()> = hsla( <hue> <percentage> <percentage> [ / <alpha-value> ]? ) | hsla( <hue>, <percentage>, <percentage>, <alpha-value>? )
                //   <side-or-corner> = [ left | right ] || [ top | bottom ]
                //   <color-stop-list> = [ <linear-color-stop> [, <linear-color-hint>]? ]# , <linear-color-stop>
                //   <ending-shape> = circle | ellipse
                //   <size> = closest-side | farthest-side | closest-corner | farthest-corner | <length> | <length-percentage>{2}
                //   <position> = [ [ left | center | right ] || [ top | center | bottom ] | [ left | center | right | <length-percentage> ] [ top | center | bottom | <length-percentage> ]? | [ [ left | right ] <length-percentage> ] && [ [ top | bottom ] <length-percentage> ] ]
                //   <angular-color-stop-list> = [ <angular-color-stop> [, <angular-color-hint>]? ]# , <angular-color-stop>
                //   <alpha-value> = <number> | <percentage>
                //   <hue> = <number> | <angle>
                //   <linear-color-stop> = <color> <color-stop-length>?
                //   <linear-color-hint> = <length-percentage>
                //   <angular-color-stop> = <color> && <color-stop-angle>?
                //   <angular-color-hint> = <angle-percentage>
                //   <color-stop-length> = <length-percentage>{1,2}
                //   <color-stop-angle> = <angle-percentage>{1,2}
                //   <angle-percentage> = <angle> | <percentage>
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
                            image = source.ParseImageSource() ?? (source.IsIdentifier(CssKeywords.None) ? new CssNoneValue() : null);
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

                return new CssBackgroundValue(new CssListValue(layers.OfType<ICssValue>().ToArray()), color ?? new CssInitialValue(InitialValues.BackgroundColorDecl));
            }

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
