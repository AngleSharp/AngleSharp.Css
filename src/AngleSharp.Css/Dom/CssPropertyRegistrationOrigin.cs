namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// What registered a custom property.
    /// </summary>
    public enum CssPropertyRegistrationOrigin : byte
    {
        /// <summary>
        /// A valid @property rule of a style sheet.
        /// </summary>
        Rule,
        /// <summary>
        /// Script, i.e., CSS.registerProperty(), which wins over the @property rules.
        /// </summary>
        Script,
    }
}
