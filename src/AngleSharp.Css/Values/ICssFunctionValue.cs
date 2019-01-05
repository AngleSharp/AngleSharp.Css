namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS value using a function call.
    /// </summary>
    interface ICssFunctionValue : ICssValue
    {
        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the arguments used for calling the function.
        /// </summary>
        ICssValue[] Arguments { get; }
    }
}
