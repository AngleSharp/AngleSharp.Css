namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Handles the presence of a default stylesheet, if any.
    /// </summary>
    public interface ICssDefaultStyleSheetProvider
    {
        /// <summary>
        /// Gets the default stylesheet for some basic styling.
        /// </summary>
        ICssStyleSheet Default { get; }

        /// <summary>
        /// Sets a new default stylesheet to use.
        /// </summary>
        /// <param name="sheet">The default stylesheet to use.</param>
        void SetDefault(ICssStyleSheet sheet);

        /// <summary>
        /// Sets a new default stylesheet to use.
        /// </summary>
        /// <param name="source">The source of the default stylesheet.</param>
        void SetDefault(String source);

        /// <summary>
        /// Sets the default stylesheet to the W3C stylesheet together with
        /// some custom extensions in form of the given source.
        /// </summary>
        /// <param name="source">The source of the custom stylesheet part.</param>
        void AppendDefault(String source);
    }
}
