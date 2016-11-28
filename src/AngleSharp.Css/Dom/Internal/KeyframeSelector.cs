namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Represents the keyframe selector.
    /// </summary>
    sealed class KeyframeSelector : IKeyframeSelector
    {
        #region Fields

        private readonly List<Single> _stops;

        #endregion

        #region ctor

        public KeyframeSelector(List<Single> stops)
        {
            _stops = stops;
        }

        #endregion

        #region Properties

        public IEnumerable<Single> Stops
        {
            get { return _stops; }
        }

        public String Text
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_stops.Count > 0)
            {
                Write(writer, _stops[0]);

                for (var i = 1; i < _stops.Count; i++)
                {
                    writer.Write(", ");
                    Write(writer, _stops[i]);
                }
            }
        }

        #endregion

        #region Helpers

        private static void Write(TextWriter writer, Single value)
        {
            var pc = Math.Truncate(100f * value);
            var str = pc.ToString(CultureInfo.InvariantCulture);
            writer.Write(str);
            writer.Write(Symbols.Percent);
        }

        #endregion
    }
}
