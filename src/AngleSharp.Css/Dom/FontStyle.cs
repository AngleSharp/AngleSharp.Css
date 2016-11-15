﻿namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// The enumeration with the various font styles.
    /// </summary>
    public enum FontStyle : byte
    {
        /// <summary>
        /// Selects a font that is classified as normal within a font-family.
        /// </summary>
        Normal,
        /// <summary>
        /// Selects a font that is labeled italic, if that is not available, one labeled oblique.
        /// </summary>
        Italic,
        /// <summary>
        /// Selects a font that is labeled oblique.
        /// </summary>
        Oblique
    }
}
