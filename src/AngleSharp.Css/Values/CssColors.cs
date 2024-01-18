namespace AngleSharp.Css.Values
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class contains information about CssColorValues like their given names or
    /// assignments of names to CssColorValues. Most names are derived from:
    /// http://en.wikipedia.org/wiki/X11_color_names
    /// </summary>
    static class CssColors
    {
        #region Fields

        private static readonly Dictionary<String, CssColorValue> TheColors = new(StringComparer.OrdinalIgnoreCase)
        {
            // Extended CssColorValue keywords
            { "aliceblue", new CssColorValue(240, 248, 255) },
            { "antiquewhite", new CssColorValue(250, 235, 215) },
            { "aqua", new CssColorValue(0, 255, 255) },
            { "aquamarine", new CssColorValue(127, 255, 212) },
            { "azure", new CssColorValue(240, 255, 255) },
            { "beige", new CssColorValue(245, 245, 220) },
            { "bisque", new CssColorValue(255, 228, 196) },
            { "black", new CssColorValue(0, 0, 0) },
            { "blanchedalmond", new CssColorValue(255, 235, 205) },
            { "blue", new CssColorValue(0, 0, 255) },
            { "blueviolet", new CssColorValue(138, 43, 226) },
            { "brown", new CssColorValue(165, 42, 42) },
            { "burlywood", new CssColorValue(222, 184, 135) },
            { "cadetblue", new CssColorValue(95, 158, 160) },
            { "chartreuse", new CssColorValue(127, 255, 0) },
            { "chocolate", new CssColorValue(210, 105, 30) },
            { "coral", new CssColorValue(255, 127, 80) },
            { "cornflowerblue", new CssColorValue(100, 149, 237) },
            { "cornsilk", new CssColorValue(255, 248, 220) },
            { "crimson", new CssColorValue(220, 20, 60) },
            { "cyan", new CssColorValue(0, 255, 255) },
            { "darkblue", new CssColorValue(0, 0, 139) },
            { "darkcyan", new CssColorValue(0, 139, 139) },
            { "darkgoldenrod", new CssColorValue(184, 134, 11) },
            { "darkgray", new CssColorValue(169, 169, 169) },
            { "darkgreen", new CssColorValue(0, 100, 0) },
            { "darkgrey", new CssColorValue(169, 169, 169) },
            { "darkkhaki", new CssColorValue(189, 183, 107) },
            { "darkmagenta", new CssColorValue(139, 0, 139) },
            { "darkolivegreen", new CssColorValue(85, 107, 47) },
            { "darkorange", new CssColorValue(255, 140, 0) },
            { "darkorchid", new CssColorValue(153, 50, 204) },
            { "darkred", new CssColorValue(139, 0, 0) },
            { "darksalmon", new CssColorValue(233, 150, 122) },
            { "darkseagreen", new CssColorValue(143, 188, 143) },
            { "darkslateblue", new CssColorValue(72, 61, 139) },
            { "darkslategray", new CssColorValue(47, 79, 79) },
            { "darkslategrey", new CssColorValue(47, 79, 79) },
            { "darkturquoise", new CssColorValue(0, 206, 209) },
            { "darkviolet", new CssColorValue(148, 0, 211) },
            { "deeppink", new CssColorValue(255, 20, 147) },
            { "deepskyblue", new CssColorValue(0, 191, 255) },
            { "dimgray", new CssColorValue(105, 105, 105) },
            { "dimgrey", new CssColorValue(105, 105, 105) },
            { "dodgerblue", new CssColorValue(30, 144, 255) },
            { "firebrick", new CssColorValue(178, 34, 34) },
            { "floralwhite", new CssColorValue(255, 250, 240) },
            { "forestgreen", new CssColorValue(34, 139, 34) },
            { "fuchsia", new CssColorValue(255, 0, 255) },
            { "gainsboro", new CssColorValue(220, 220, 220) },
            { "ghostwhite", new CssColorValue(248, 248, 255) },
            { "gold", new CssColorValue(255, 215, 0) },
            { "goldenrod", new CssColorValue(218, 165, 32) },
            { "gray", new CssColorValue(128, 128, 128) },
            { "green", new CssColorValue(0, 128, 0) },
            { "greenyellow", new CssColorValue(173, 255, 47) },
            { "grey", new CssColorValue(128, 128, 128) },
            { "honeydew", new CssColorValue(240, 255, 240) },
            { "hotpink", new CssColorValue(255, 105, 180) },
            { "indianred", new CssColorValue(205, 92, 92) },
            { "indigo", new CssColorValue(75, 0, 130) },
            { "ivory", new CssColorValue(255, 255, 240) },
            { "khaki", new CssColorValue(240, 230, 140) },
            { "lavender", new CssColorValue(230, 230, 250) },
            { "lavenderblush", new CssColorValue(255, 240, 245) },
            { "lawngreen", new CssColorValue(124, 252, 0) },
            { "lemonchiffon", new CssColorValue(255, 250, 205) },
            { "lightblue", new CssColorValue(173, 216, 230) },
            { "lightcoral", new CssColorValue(240, 128, 128) },
            { "lightcyan", new CssColorValue(224, 255, 255) },
            { "lightgoldenrodyellow", new CssColorValue(250, 250, 210) },
            { "lightgray", new CssColorValue(211, 211, 211) },
            { "lightgreen", new CssColorValue(144, 238, 144) },
            { "lightgrey", new CssColorValue(211, 211, 211) },
            { "lightpink", new CssColorValue(255, 182, 193) },
            { "lightsalmon", new CssColorValue(255, 160, 122) },
            { "lightseagreen", new CssColorValue(32, 178, 170) },
            { "lightskyblue", new CssColorValue(135, 206, 250) },
            { "lightslategray", new CssColorValue(119, 136, 153) },
            { "lightslategrey", new CssColorValue(119, 136, 153) },
            { "lightsteelblue", new CssColorValue(176, 196, 222) },
            { "lightyellow", new CssColorValue(255, 255, 224) },
            { "lime", new CssColorValue(0, 255, 0) },
            { "limegreen", new CssColorValue(50, 205, 50) },
            { "linen", new CssColorValue(250, 240, 230) },
            { "magenta", new CssColorValue(255, 0, 255) },
            { "maroon", new CssColorValue(128, 0, 0) },
            { "mediumaquamarine", new CssColorValue(102, 205, 170) },
            { "mediumblue", new CssColorValue(0, 0, 205) },
            { "mediumorchid", new CssColorValue(186, 85, 211) },
            { "mediumpurple", new CssColorValue(147, 112, 219) },
            { "mediumseagreen", new CssColorValue(60, 179, 113) },
            { "mediumslateblue", new CssColorValue(123, 104, 238) },
            { "mediumspringgreen", new CssColorValue(0, 250, 154) },
            { "mediumturquoise", new CssColorValue(72, 209, 204) },
            { "mediumvioletred", new CssColorValue(199, 21, 133) },
            { "midnightblue", new CssColorValue(25, 25, 112) },
            { "mintcream", new CssColorValue(245, 255, 250) },
            { "mistyrose", new CssColorValue(255, 228, 225) },
            { "moccasin", new CssColorValue(255, 228, 181) },
            { "navajowhite", new CssColorValue(255, 222, 173) },
            { "navy", new CssColorValue(0, 0, 128) },
            { "oldlace", new CssColorValue(253, 245, 230) },
            { "olive", new CssColorValue(128, 128, 0) },
            { "olivedrab", new CssColorValue(107, 142, 35) },
            { "orange", new CssColorValue(255, 165, 0) },
            { "orangered", new CssColorValue(255, 69, 0) },
            { "orchid", new CssColorValue(218, 112, 214) },
            { "palegoldenrod", new CssColorValue(238, 232, 170) },
            { "palegreen", new CssColorValue(152, 251, 152) },
            { "paleturquoise", new CssColorValue(175, 238, 238) },
            { "palevioletred", new CssColorValue(219, 112, 147) },
            { "papayawhip", new CssColorValue(255, 239, 213) },
            { "peachpuff", new CssColorValue(255, 218, 185) },
            { "peru", new CssColorValue(205, 133, 63) },
            { "pink", new CssColorValue(255, 192, 203) },
            { "plum", new CssColorValue(221, 160, 221) },
            { "powderblue", new CssColorValue(176, 224, 230) },
            { "purple", new CssColorValue(128, 0, 128) },
            { "rebeccapurple", new CssColorValue(102, 51, 153) },
            { "red", new CssColorValue(255, 0, 0) },
            { "rosybrown", new CssColorValue(188, 143, 143) },
            { "royalblue", new CssColorValue(65, 105, 225) },
            { "saddlebrown", new CssColorValue(139, 69, 19) },
            { "salmon", new CssColorValue(250, 128, 114) },
            { "sandybrown", new CssColorValue(244, 164, 96) },
            { "seagreen", new CssColorValue(46, 139, 87) },
            { "seashell", new CssColorValue(255, 245, 238) },
            { "sienna", new CssColorValue(160, 82, 45) },
            { "silver", new CssColorValue(192, 192, 192) },
            { "skyblue", new CssColorValue(135, 206, 235) },
            { "slateblue", new CssColorValue(106, 90, 205) },
            { "slategray", new CssColorValue(112, 128, 144) },
            { "slategrey", new CssColorValue(112, 128, 144) },
            { "snow", new CssColorValue(255, 250, 250) },
            { "springgreen", new CssColorValue(0, 255, 127) },
            { "steelblue", new CssColorValue(70, 130, 180) },
            { "tan", new CssColorValue(210, 180, 140) },
            { "teal", new CssColorValue(0, 128, 128) },
            { "thistle", new CssColorValue(216, 191, 216) },
            { "tomato", new CssColorValue(255, 99, 71) },
            { "turquoise", new CssColorValue(64, 224, 208) },
            { "violet", new CssColorValue(238, 130, 238) },
            { "wheat", new CssColorValue(245, 222, 179) },
            { "white", new CssColorValue(255, 255, 255) },
            { "whitesmoke", new CssColorValue(245, 245, 245) },
            { "yellow", new CssColorValue(255, 255, 0) },
            { "yellowgreen", new CssColorValue(154, 205, 50) },
            { "transparent", new CssColorValue(0, 0, 0, 0) },
            // CSS2 system CssColorValues
            { "activeborder", new CssColorValue(255, 255, 255) },
            { "activecaption", new CssColorValue(204, 204, 204) },
            { "appworkspace", new CssColorValue(255, 255, 255) },
            { "background", new CssColorValue(99, 99, 206) },
            { "buttonface", new CssColorValue(221, 221, 221) },
            { "buttonhighlight", new CssColorValue(221, 221, 221) },
            { "buttonshadow", new CssColorValue(136, 136, 136) },
            { "buttontext", new CssColorValue(0, 0, 0) },
            { "captiontext", new CssColorValue(0, 0, 0) },
            { "graytext", new CssColorValue(128, 128, 128) },
            { "highlight", new CssColorValue(181, 213, 255) },
            { "highlighttext", new CssColorValue(0, 0, 0) },
            { "inactiveborder", new CssColorValue(255, 255, 255) },
            { "inactivecaption", new CssColorValue(255, 255, 255) },
            { "inactivecaptiontext", new CssColorValue(127, 127, 127) },
            { "infobackground", new CssColorValue(251, 252, 197) },
            { "infotext", new CssColorValue(0, 0, 0) },
            { "menu", new CssColorValue(247, 247, 247) },
            { "menutext", new CssColorValue(0, 0, 0) },
            { "scrollbar", new CssColorValue(255, 255, 255) },
            { "threeddarkshadow", new CssColorValue(102, 102, 102) },
            { "threedface", new CssColorValue(255, 255, 255) },
            { "threedhighlight", new CssColorValue(221, 221, 221) },
            { "threedlightshadow", new CssColorValue(192, 192, 192) },
            { "threedshadow", new CssColorValue(136, 136, 136) },
            { "window", new CssColorValue(255, 255, 255) },
            { "windowframe", new CssColorValue(204, 204, 204) },
            { "windowtext", new CssColorValue(0, 0, 0) },
        };

        #endregion

        #region Properties

        /// <summary>
        /// Gets the available CssColorValue names.
        /// </summary>
        public static IEnumerable<String> Names => TheColors.Keys;

        #endregion

        #region Methods

        /// <summary>
        /// Gets a CssColorValue from the specified name.
        /// </summary>
        /// <param name="name">The name of the CssColorValue</param>
        /// <returns>The CssColorValue with the given name or null.</returns>
        public static CssColorValue? GetColor(String name)
        {
            if (TheColors.TryGetValue(name, out CssColorValue CssColorValue))
            {
                return CssColorValue;
            }

            return null;
        }

        /// <summary>
        /// Gets the name of the given CssColorValue.
        /// </summary>
        /// <param name="color">The CssColorValue associated with a name.</param>
        /// <returns>The name of the given CssColorValue or null.</returns>
        public static String GetName(CssColorValue color)
        {
            foreach (var pair in TheColors)
            {
                if (pair.Value.Equals(color))
                {
                    return pair.Key;
                }
            }

            return null;
        }

        #endregion
    }
}
