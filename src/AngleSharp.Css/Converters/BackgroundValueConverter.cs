namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System.Collections.Generic;
    using System.Linq;

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
}
