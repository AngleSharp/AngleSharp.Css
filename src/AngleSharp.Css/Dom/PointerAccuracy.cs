﻿namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// Values for the accuracy of a pointer device.
    /// </summary>
    public enum PointerAccuracy : byte
    {
        /// <summary>
        /// Not actually a pointing device.
        /// </summary>
        None,
        /// <summary>
        /// Defines a pointing device of limited accuracy.
        /// </summary>
        Coarse,
        /// <summary>
        /// Defines an accurate pointing device.
        /// </summary>
        Fine
    }
}
