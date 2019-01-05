namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to document function creation mappings.
    /// </summary>
    public class DefaultDocumentFunctionFactory : IDocumentFunctionFactory
    {
        /// <summary>
        /// Represents a creator delegate for creating document functions.
        /// </summary>
        /// <param name="url">The url of the document function.</param>
        /// <returns>The created document function for the given url.</returns>
        public delegate IDocumentFunction Creator(String url);

        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { FunctionNames.Url, str => new UrlFunction(str) },
            { FunctionNames.Domain, str => new DomainFunction(str) },
            { FunctionNames.UrlPrefix, str => new UrlPrefixFunction(str) },
            { FunctionNames.Regexp, str => new RegexpFunction(str) }
        };

        /// <summary>
        /// Registers a new document function for the given name.
        /// Throws an exception if another creator for the given
        /// function name is already added.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String name, Creator creator)
        {
            _creators.Add(name, creator);
        }

        /// <summary>
        /// Unregisters an existing document function creator.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String name)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(name, out creator))
            {
                _creators.Remove(name);
            }

            return creator;
        }

        /// <summary>
        /// Creates the default document function for the given
        /// function name. Returns null by default.
        /// </summary>
        /// <param name="name">The name of the document function.</param>
        /// <param name="url">The url to handle.</param>
        /// <returns>The default document function.</returns>
        protected virtual IDocumentFunction CreateDefault(String name, String url)
        {
            return default(IDocumentFunction);
        }

        /// <summary>
        /// Creates a new document function for the given name and argument.
        /// </summary>
        /// <param name="name">The name of the document function.</param>
        /// <param name="url">The url to handle.</param>
        /// <returns>The created document function, if any.</returns>
        public IDocumentFunction Create(String name, String url)
        {
            var creator = default(Creator);

            if (!String.IsNullOrEmpty(name) && _creators.TryGetValue(name, out creator))
            {
                return creator.Invoke(url);
            }

            return CreateDefault(name, url);
        }
    }
}
