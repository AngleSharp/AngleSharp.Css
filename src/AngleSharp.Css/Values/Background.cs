namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public sealed class Background : ICssValue
    {
        private readonly ICssValue _layers;
        private readonly ICssValue _color;

        public Background(ICssValue layers, ICssValue color)
        {
            _layers = layers;
            _color = color;
        }

        public ICssValue Color
        {
            get { return _color; }
        }

        public ICssValue Layers
        {
            get { return _layers; }
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
        {
            var layer = _layers.CssText;

            if (_color != null)
            {
                var sep = layer.Length > 0 ? " " : "";
                var color = _color.CssText;
                return String.Concat(layer, sep, color);
            }

            return layer;
        }
    }
}
