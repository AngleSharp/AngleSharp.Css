namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// An enumeration with all possible symbol types.
    /// </summary>
    public enum SymbolsType : byte
    {
        /// <summary>
        /// The system cycles through the given values in the order of their definition, and returns to the start when it reaches the end.
        /// </summary>
        Cyclic,
        /// <summary>
        /// The system interprets the given values as the successive units of a place-value numbering system.
        /// </summary>
        Numeric,
        /// <summary>
        ///  The system interprets the given values as the digits of an alphabetic numbering system, like a place-value numbering system but without 0.
        /// </summary>
        Alphabetic,
        /// <summary>
        /// The system cycles through the values, printing them an additional time at each cycle (one time for the first cycle, two times for the second, etc.).
        /// </summary>
        Symbolic,
        /// <summary>
        /// The system cycles through the given values once, then falls back to Arabic numerals.
        /// </summary>
        Fixed,
    }
}
