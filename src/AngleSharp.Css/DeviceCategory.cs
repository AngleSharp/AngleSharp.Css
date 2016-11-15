namespace AngleSharp.Css
{
    /// <summary>
    /// All possible device kinds.
    /// </summary>
    public enum DeviceCategory : byte
    {
        /// <summary>
        /// A screen device. Default.
        /// </summary>
        Screen,
        /// <summary>
        /// A printing device.
        /// </summary>
        Printer,
        /// <summary>
        /// A device for speech output.
        /// </summary>
        Speech,
        /// <summary>
        /// Some other device.
        /// </summary>
        Other
    }
}
