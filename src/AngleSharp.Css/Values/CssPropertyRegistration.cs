namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// The registration of a custom property, e.g., by a valid @property rule.
    /// https://www.w3.org/TR/css-properties-values-api-1/#custom-property-registration
    /// </summary>
    sealed class CssPropertyRegistration
    {
        public CssPropertyRegistration(String name, CssPropertySyntax syntax, Boolean inherits, ICssValue? initialValue)
        {
            Name = name;
            Syntax = syntax;
            Inherits = inherits;
            InitialValue = initialValue;
        }

        /// <summary>
        /// Creates the registration with the given (non-universal or optional) initial value,
        /// which has to match the syntax and be computationally independent.
        /// </summary>
        /// <returns>The registration or null if the initial value is not valid.</returns>
        public static CssPropertyRegistration? Create(String name, CssPropertySyntax syntax, Boolean inherits, String? initialText)
        {
            var initial = default(ICssValue);

            if (initialText is not null)
            {
                if (!CssPropertySyntax.IsComputationallyIndependent(initialText))
                {
                    return null;
                }

                initial = syntax.Match(new CssAnyValue(initialText, isResolved: true));
            }

            if (initial is null && !syntax.IsUniversal)
            {
                return null;
            }

            return new CssPropertyRegistration(name, syntax, inherits, initial);
        }

        /// <summary>
        /// Checks if the name is a custom property name, i.e., starts with two dashes.
        /// </summary>
        public static Boolean IsCustomPropertyName(String? name) =>
            name is not null && name.Length > 2 && name.StartsWith("--", StringComparison.Ordinal);

        /// <summary>
        /// Gets the name of the custom property, e.g., --my-color.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the syntax values of the property have to match.
        /// </summary>
        public CssPropertySyntax Syntax { get; }

        /// <summary>
        /// Gets if the property is inherited.
        /// </summary>
        public Boolean Inherits { get; }

        /// <summary>
        /// Gets the initial value (matched against the syntax), which is null, i.e., the
        /// guaranteed-invalid value, for the universal syntax without an initial value.
        /// </summary>
        public ICssValue? InitialValue { get; }
    }

    /// <summary>
    /// The registered custom properties of a document, where the last registration
    /// of a name wins.
    /// </summary>
    sealed class CssPropertyRegistry
    {
        private readonly Dictionary<String, CssPropertyRegistration> _registrations;

        public CssPropertyRegistry()
        {
            _registrations = new Dictionary<String, CssPropertyRegistration>(StringComparer.Ordinal);
        }

        private CssPropertyRegistry(CssPropertyRegistry other)
        {
            _registrations = new Dictionary<String, CssPropertyRegistration>(other._registrations, StringComparer.Ordinal);
        }

        public Boolean Contains(String name) => _registrations.ContainsKey(name);

        /// <summary>
        /// Creates a copy of the registry with the given registration added.
        /// </summary>
        public CssPropertyRegistry With(CssPropertyRegistration registration)
        {
            var copy = new CssPropertyRegistry(this);
            copy.Register(registration);
            return copy;
        }

        /// <summary>
        /// Adds the registrations of the other registry, which replace the own ones.
        /// </summary>
        public void RegisterAll(CssPropertyRegistry other)
        {
            foreach (var registration in other.Registrations)
            {
                Register(registration);
            }
        }

        public Int32 Count => _registrations.Count;

        public IEnumerable<CssPropertyRegistration> Registrations => _registrations.Values;

        public void Register(CssPropertyRegistration registration) => _registrations[registration.Name] = registration;

        public CssPropertyRegistration? Get(String name) =>
            _registrations.Count > 0 && _registrations.TryGetValue(name, out var registration) ? registration : null;
    }

    /// <summary>
    /// The custom properties registered by script (CSS.registerProperty) per document, which
    /// win over the registrations of @property rules (CSS Properties and Values API 2.1).
    /// </summary>
    static class CssScriptPropertyRegistrations
    {
        private static readonly ConditionalWeakTable<IDocument, Holder> Registrations = new();

        /// <summary>
        /// Gets the (immutable) registrations of the document, if any.
        /// </summary>
        public static CssPropertyRegistry? Get(IDocument document) =>
            Registrations.TryGetValue(document, out var holder) ? holder.Registry : null;

        /// <summary>
        /// Adds the registration to the document.
        /// </summary>
        /// <returns>False if the name has been registered by script already.</returns>
        public static Boolean TryAdd(IDocument document, CssPropertyRegistration registration)
        {
            var holder = Registrations.GetValue(document, _ => new Holder());

            lock (holder)
            {
                if (holder.Registry.Contains(registration.Name))
                {
                    return false;
                }

                // replaced rather than changed, as styles may be computed meanwhile
                holder.Registry = holder.Registry.With(registration);
                return true;
            }
        }

        private sealed class Holder
        {
            private CssPropertyRegistry _registry = new();

            public CssPropertyRegistry Registry
            {
                get => Volatile.Read(ref _registry);
                set => Volatile.Write(ref _registry, value);
            }
        }
    }
}
