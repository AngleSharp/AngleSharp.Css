namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Text;

    sealed class CssCustomPropertyResolver
    {
        // created on demand: most elements declare no custom properties (but are computed with a resolver)
        private Dictionary<String, ICssValue?>? _values;
        // the custom properties of a parent that is not a CssStyleDeclaration, which are copied
        private Dictionary<String, ICssValue?>? _parentValues;
        private readonly CssStyleDeclaration? _parent;
        private Func<String, ICssValue?>? _resolve;

        // the registered custom properties (@property) and what is known about them on the element
        private readonly CssPropertyRegistry? _registry;
        private readonly ICssComputeContext? _context;
        private Dictionary<String, String>? _keywords;
        private Dictionary<String, ICssValue?>? _computed;
        private HashSet<String>? _computing;
        private Boolean _initializing;
        // the font size of the element refers to custom properties, i.e., cannot be computed
        // while they are substituted
        private Boolean _fontSizeHasReferences;

        public CssCustomPropertyResolver(IEnumerable<ICssProperty> properties, ICssProperties? parent = null)
            : this(properties, parent, null, null)
        {
        }

        /// <summary>
        /// Creates the resolver of the custom properties of an element. The values of registered
        /// custom properties are validated against their syntax and computed (with the given
        /// context) when they are resolved, which must not happen before the context is usable.
        /// </summary>
        public CssCustomPropertyResolver(IEnumerable<ICssProperty> properties, ICssProperties? parent, CssPropertyRegistry? registry, ICssComputeContext? context)
        {
            // the custom properties of the parent are looked up on demand instead of copied,
            // as there may be thousands of them (e.g., design tokens on :root)
            _parent = parent as CssStyleDeclaration;
            _registry = registry is { Count: > 0 } ? registry : null;
            _context = context;
            _initializing = true;

            if (parent is not null && _parent is null)
            {
                foreach (var property in parent)
                {
                    if (property.Name.StartsWith("--", StringComparison.Ordinal))
                    {
                        (_parentValues ??= new Dictionary<String, ICssValue?>(StringComparer.Ordinal))[property.Name] = property.RawValue is CssInvalidValue ? null : property.RawValue;
                    }
                }
            }

            try
            {
                Initialize(properties);
            }
            finally
            {
                _initializing = false;
            }
        }

        private void Initialize(IEnumerable<ICssProperty> properties)
        {

            Dictionary<String, Node>? nodes = null;

            foreach (var property in properties)
            {
                if (property.RawValue is CssReferenceValue && property.Name.Is(PropertyNames.FontSize))
                {
                    _fontSizeHasReferences = true;
                }

                if (property.Name.StartsWith("--", StringComparison.Ordinal))
                {
                    var value = property.RawValue;

                    if (value is CssAnyValue { IsResolved: true })
                    {
                        SetValue(property.Name, value);
                        continue;
                    }

                    var values = value is CssReferenceValue references ? references.GetVariableValues().ToArray() :
                        value is null || value is CssInvalidValue ? Array.Empty<CssVariableValue>() :
                        value is CssAnyValue any ? new[] { any.Tokens } :
                        new[] { new CssVariableValue(value.CssText) };
                    var keyword = values.Length == 1 ? values[0].Keyword : null;

                    if (keyword is not null && _registry?.Get(property.Name) is not null && IsCssWideKeyword(keyword))
                    {
                        (_keywords ??= new Dictionary<String, String>(StringComparer.Ordinal))[property.Name] = keyword.ToLowerInvariant();
                    }

                    if (String.Equals(keyword, CssKeywords.Inherit, StringComparison.OrdinalIgnoreCase) ||
                        String.Equals(keyword, CssKeywords.Unset, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    SetValue(property.Name, null);

                    if (values.Length > 0 && !String.Equals(keyword, CssKeywords.Initial, StringComparison.OrdinalIgnoreCase))
                    {
                        nodes ??= new Dictionary<String, Node>(StringComparer.Ordinal);
                        nodes[property.Name] = new Node(property.Name, values);
                    }
                }
            }

            if (nodes is null)
            {
                return;
            }

            foreach (var node in nodes.Values)
            {
                foreach (var value in node.Values)
                {
                    if (!value.IsValid)
                    {
                        continue;
                    }

                    foreach (var name in value.Dependencies)
                    {
                        if (nodes.TryGetValue(name, out var dependency))
                        {
                            node.Dependencies.Add(dependency);
                        }
                    }
                }
            }

            // Iterative Tarjan traversal: complete components in dependency order.
            // All fallback edges participate, even if substitution won't use them.
            var index = 0;
            var active = new Stack<Node>();
            var visits = new Stack<Node>();
            var component = new List<Node>();

            foreach (var root in nodes.Values)
            {
                if (root.Index >= 0)
                {
                    continue;
                }

                Enter(root);

                while (visits.Count > 0)
                {
                    var node = visits.Peek();

                    if (node.NextDependency < node.Dependencies.Count)
                    {
                        var dependency = node.Dependencies[node.NextDependency++];

                        if (dependency.Index < 0)
                        {
                            Enter(dependency);
                        }
                        else if (dependency.Active)
                        {
                            node.LowLink = Math.Min(node.LowLink, dependency.Index);
                        }

                        continue;
                    }

                    visits.Pop();

                    if (visits.Count > 0)
                    {
                        var previous = visits.Peek();
                        previous.LowLink = Math.Min(previous.LowLink, node.LowLink);
                    }

                    if (node.LowLink == node.Index)
                    {
                        component.Clear();
                        Node member;

                        do
                        {
                            member = active.Pop();
                            member.Active = false;
                            component.Add(member);
                        }
                        while (member != node);

                        if (component.Count == 1 && !node.Dependencies.Contains(node))
                        {
                            foreach (var value in node.Values)
                            {
                                var text = value.Substitute(_resolve ??= Resolve);

                                if (text is not null)
                                {
                                    SetValue(node.Name, new CssAnyValue(text, isResolved: true));
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            void Enter(Node node)
            {
                node.Index = node.LowLink = index++;
                node.Active = true;
                active.Push(node);
                visits.Push(node);
            }
        }

        private void SetValue(String name, ICssValue? value) => (_values ??= new Dictionary<String, ICssValue?>(StringComparer.Ordinal))[name] = value;

        public ICssValue? Resolve(String name)
        {
            var registration = _registry?.Get(name);

            if (registration is not null)
            {
                return ResolveRegistered(registration);
            }

            if (_values is not null && _values.TryGetValue(name, out var value))
            {
                return value;
            }

            return ResolveParent(name);
        }

        private ICssValue? ResolveParent(String name)
        {
            if (_parentValues is not null && _parentValues.TryGetValue(name, out var copied))
            {
                return copied;
            }

            if (_parent is not null && name.StartsWith("--", StringComparison.Ordinal))
            {
                var inherited = _parent.GetDeclaration(name)?.RawValue;
                return inherited is CssInvalidValue ? null : inherited;
            }

            return null;
        }

        #region Registered Custom Properties

        /// <summary>
        /// Resolves the computed value of a registered custom property (CSS Properties and
        /// Values API 2.4): a value not matching the syntax is invalid at computed-value time,
        /// i.e., behaves as unset, which (as when nothing is declared) is the value of the
        /// parent for an inherited property and otherwise the initial value.
        /// </summary>
        private ICssValue? ResolveRegistered(CssPropertyRegistration registration)
        {
            var name = registration.Name;

            if (_computed is not null && _computed.TryGetValue(name, out var cached))
            {
                return cached;
            }

            // the value refers to itself while it is computed, e.g., via the font size
            if (_computing is not null && _computing.Contains(name))
            {
                return null;
            }

            // While the custom properties are substituted (in dependency order), a value that
            // depends on the font size is computed only if the font size does not depend on the
            // custom properties in question, otherwise it is given as specified.
            if (_initializing && _fontSizeHasReferences)
            {
                var specified = Evaluate(registration, false);

                if (specified is not null && !CssPropertySyntax.IsComputationallyIndependent(specified.CssText))
                {
                    return specified;
                }
            }

            (_computing ??= new HashSet<String>(StringComparer.Ordinal)).Add(name);

            try
            {
                var value = Evaluate(registration, true);
                (_computed ??= new Dictionary<String, ICssValue?>(StringComparer.Ordinal))[name] = value;
                return value;
            }
            finally
            {
                _computing.Remove(name);
            }
        }

        /// <summary>
        /// Resolves the custom property for font-size: a registered property whose value
        /// depends on the font size, e.g., 2em, makes a cycle with it (CSS Properties and
        /// Values API 2.7), i.e., is the guaranteed-invalid value for font-size.
        /// </summary>
        public ICssValue? ResolveForFontSize(String name)
        {
            var registration = _registry?.Get(name);

            if (registration is not null)
            {
                var specified = Evaluate(registration, false);

                if (specified is not null && !CssPropertySyntax.IsComputationallyIndependent(specified.CssText))
                {
                    return null;
                }
            }

            return Resolve(name);
        }

        private ICssValue? Evaluate(CssPropertyRegistration registration, Boolean compute)
        {
            var name = registration.Name;

            if (_keywords is not null && _keywords.TryGetValue(name, out var keyword))
            {
                return keyword switch
                {
                    "initial" => Initial(registration, compute),
                    "inherit" => Parent(registration, compute),
                    _ => Unset(registration, compute),
                };
            }

            if (_values is not null && _values.TryGetValue(name, out var declared) && declared is not null)
            {
                var matched = registration.Syntax.Match(declared);

                if (matched is not null)
                {
                    return compute ? ComputeValue(registration, matched) : matched;
                }
            }

            // not declared, or invalid at computed-value time
            return Unset(registration, compute);
        }

        private ICssValue? Unset(CssPropertyRegistration registration, Boolean compute) =>
            registration.Inherits ? Parent(registration, compute) : Initial(registration, compute);

        // the parent's value is computed already, unless it was computed without the registration
        private ICssValue? Parent(CssPropertyRegistration registration, Boolean compute)
        {
            var inherited = ResolveParent(registration.Name);

            if (inherited is not null)
            {
                var matched = registration.Syntax.Match(inherited);

                if (matched is not null)
                {
                    // a typed value is the computed value of the parent already
                    return inherited is not CssAnyValue ? inherited : compute ? ComputeValue(registration, matched) : matched;
                }
            }

            return Initial(registration, compute);
        }

        private ICssValue? Initial(CssPropertyRegistration registration, Boolean compute)
        {
            var initial = registration.InitialValue;
            return initial is not null && compute ? ComputeValue(registration, initial) : initial;
        }

        private ICssValue? ComputeValue(CssPropertyRegistration registration, ICssValue value)
        {
            if (registration.Syntax.IsUniversal || _context is null)
            {
                return value;
            }

            return CssProperty.ComputeValue(value, ValueConverters.Any, registration.Name, _context) ?? value;
        }

        private static Boolean IsCssWideKeyword(String keyword) =>
            keyword.Isi(CssKeywords.Initial) || keyword.Isi(CssKeywords.Inherit) || keyword.Isi(CssKeywords.Unset);

        #endregion

        private sealed class Node
        {
            public Node(String name, CssVariableValue[] values)
            {
                Name = name;
                Values = values;
            }

            public String Name { get; }
            public CssVariableValue[] Values { get; }
            public List<Node> Dependencies { get; } = new();
            public Int32 Index { get; set; } = -1;
            public Int32 LowLink { get; set; }
            public Int32 NextDependency { get; set; }
            public Boolean Active { get; set; }
        }
    }
}
