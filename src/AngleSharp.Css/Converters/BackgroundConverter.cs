namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

    sealed class BackgroundConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var layers = new List<BackgroundLayer>();
            var color = default(Color?);
            var pos = 0;
            var c = source.SkipSpacesAndComments();

            while (!source.IsDone && !color.HasValue)
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
                        layer.Source = source.ToImageSource();
                        c = source.SkipSpacesAndComments();
                    }

                    if (!layer.Position.HasValue)
                    {
                        layer.Position = source.ToPoint();
                        c = source.SkipSpacesAndComments();

                        if (c == Symbols.Solidus && !layer.Size.HasValue)
                        {
                            c = source.SkipSpacesAndComments();
                            layer.Size = source.ToSize();
                            c = source.SkipSpacesAndComments();
                        }
                    }

                    if (layer.Repeat == null)
                    {
                        layer.Repeat = source.ToBackgroundRepeat();
                        c = source.SkipSpacesAndComments();
                    }

                    if (!layer.Attachment.HasValue)
                    {
                        layer.Attachment = source.ToConstant(Map.BackgroundAttachments);
                        c = source.SkipSpacesAndComments();
                    }

                    if (!layer.Origin.HasValue)
                    {
                        layer.Origin = source.ToConstant(Map.BoxModels);
                        c = source.SkipSpacesAndComments();
                    }

                    if (!layer.Clip.HasValue)
                    {
                        layer.Clip = source.ToConstant(Map.BoxModels);
                        c = source.SkipSpacesAndComments();
                    }

                    if (!color.HasValue)
                    {
                        color = source.ParseColor();
                        c = source.SkipSpacesAndComments();
                    }
                }
                while (pos != source.Index);

                layers.Add(layer);
            }

            return new BackgroundValue(layers.ToArray(), color);
        }

        struct BackgroundLayer
        {
            public IImageSource Source;
            public Point? Position;
            public Point? Size;
            public Tuple<BackgroundRepeat, BackgroundRepeat> Repeat;
            public BackgroundAttachment? Attachment;
            public BoxModel? Origin;
            public BoxModel? Clip;

            private static String Stringify(Tuple<BackgroundRepeat, BackgroundRepeat> r)
            {
                if (r.Item1 == BackgroundRepeat.Repeat && r.Item2 == BackgroundRepeat.NoRepeat)
                {
                    return CssKeywords.RepeatX;
                }
                else if (r.Item2 == BackgroundRepeat.Repeat && r.Item1 == BackgroundRepeat.NoRepeat)
                {
                    return CssKeywords.RepeatY;
                }
                else if (r.Item1 == r.Item2)
                {
                    return r.Item1.ToString(Map.BackgroundRepeats);
                }

                return String.Concat(r.Item1.ToString(Map.BackgroundRepeats), " ", r.Item2.ToString(Map.BackgroundRepeats));
            }

            public override String ToString()
            {
                var sb = StringBuilderPool.Obtain();

                if (Source != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Source.ToString());
                }

                if (Position.HasValue)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Position.Value.ToString());

                    if (Size.HasValue)
                    {
                        sb.Append(" / ");
                        sb.Append(Size.Value.ToString());
                    }
                }

                if (Repeat != null)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Stringify(Repeat));
                }

                if (Attachment.HasValue)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Attachment.Value.ToString(Map.BackgroundAttachments));
                }

                if (Origin.HasValue)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Origin.Value.ToString(Map.BoxModels));
                }

                if (Clip.HasValue)
                {
                    if (sb.Length > 0) sb.Append(' ');
                    sb.Append(Clip.Value.ToString(Map.BoxModels));
                }

                return sb.ToPool();
            }
        }

        sealed class BackgroundValue : ICssValue
        {
            private readonly BackgroundLayer[] _layers;
            private readonly Color? _color;

            public BackgroundValue(BackgroundLayer[] layers, Color? color)
            {
                _layers = layers;
                _color = color;
            }

            public String CssText
            {
                get
                {
                    var layer = _layers.Join(", ");

                    if (_color.HasValue)
                    {
                        var sep = layer.Length > 0 ? " " : "";
                        var color = _color.Value.ToString();
                        return String.Concat(layer, sep, color);
                    }

                    return layer;
                }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
