namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The default factory for creating pseudo elements.
    /// </summary>
    public class DefaultPseudoElementFactory : IPseudoElementFactory
    {
        /// <summary>
        /// Creates a new pseudo element for the given host.
        /// </summary>
        /// <param name="host">The host to use.</param>
        /// <returns>The new pseudo element.</returns>
        public delegate IPseudoElement Creator(IElement host);

        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { PseudoElementNames.Before, element => new PseudoElement(element, PseudoElementNames.Before) },
            { PseudoElementNames.After, element => new PseudoElement(element, PseudoElementNames.After) },
            { PseudoElementNames.Slotted, element => new PseudoElement(element, PseudoElementNames.Slotted) }
        };

        /// <summary>
        /// Registers a new creator for the specified pseudo type.
        /// Throws an exception if another creator for the given
        /// pseudo type is already added.
        /// </summary>
        /// <param name="type">The pseudo element type.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String type, Creator creator)
        {
            _creators.Add(type, creator);
        }

        /// <summary>
        /// Unregisters an existing creator for the given pseudo type.
        /// </summary>
        /// <param name="type">The pseudo element type.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String type)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(type, out creator))
            {
                _creators.Remove(type);
            }

            return creator;
        }

        /// <summary>
        /// Creates the default pseudo element instance for the given pseudo
        /// type. By default this is null.
        /// </summary>
        /// <param name="host">The hosting element.</param>
        /// <param name="type">The type of pseudo element.</param>
        /// <returns>The default pseudo element instance.</returns>
        protected virtual IPseudoElement CreateDefault(IElement host, String type)
        {
            return default(IPseudoElement);
        }

        /// <summary>
        /// Creates the pseudo element instance for the given pseudo type.
        /// </summary>
        /// <param name="host">The hosting element.</param>
        /// <param name="type">The type of pseudo element.</param>
        /// <returns>The pseudo element instance.</returns>
        public IPseudoElement Create(IElement host, String type)
        {
            var creator = default(Creator);

            if (!String.IsNullOrEmpty(type) && _creators.TryGetValue(type, out creator))
            {
                return creator(host);
            }

            return CreateDefault(host, type);
        }
    }
}
