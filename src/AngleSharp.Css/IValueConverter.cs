namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;

    /// <summary>
    /// Represents a converter for value strings.
    /// </summary>
    public interface IValueConverter
    {
        /// <summary>
        /// Tries to convert the given source to a value.
        /// </summary>
        /// <param name="source">The source to convert.</param>
        /// <returns>The value if valid, otherwise null.</returns>
        ICssValue Convert(StringSource source);
    }
}
