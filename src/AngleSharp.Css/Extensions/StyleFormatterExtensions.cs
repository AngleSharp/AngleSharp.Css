namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Extensions for IStyleFormattable instances.
    /// </summary>
    public static class StyleFormatterExtensions
    {
        /// <summary>
        /// Returns a minified serialization of the node guided by the
        /// MinifyStyleFormatter with the default options.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <returns>The source code snippet.</returns>
        public static String Minify(this IStyleFormattable style) =>
            style.ToCss(new MinifyStyleFormatter());

        /// <summary>
        /// Returns a prettified serialization of the node guided by the
        /// PrettyStyleFormatter with the default options.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <returns>The source code snippet.</returns>
        public static String Prettify(this IStyleFormattable style) =>
            style.ToCss(new PrettyStyleFormatter());
    }
}
