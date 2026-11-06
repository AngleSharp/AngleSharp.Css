#nullable enable
namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// The members of a MediaQueryList kept by the standard for pages from before it was an
    /// event target.
    /// </summary>
    [DomExposed("MediaQueryList")]
    public static class MediaQueryListExtensions
    {
        private const String ChangeEvent = "change";

        /// <summary>
        /// Adds a listener for changes of the query, like listening to its change event.
        /// </summary>
        /// <param name="list">The list to listen to.</param>
        /// <param name="callback">The listener.</param>
        [DomName("addListener")]
        public static void AddListener(this IMediaQueryList list, DomEventHandler? callback) =>
            list.AddEventListener(ChangeEvent, callback);

        /// <summary>
        /// Removes a listener for changes of the query.
        /// </summary>
        /// <param name="list">The list to stop listening to.</param>
        /// <param name="callback">The listener.</param>
        [DomName("removeListener")]
        public static void RemoveListener(this IMediaQueryList list, DomEventHandler? callback) =>
            list.RemoveEventListener(ChangeEvent, callback);
    }
}
