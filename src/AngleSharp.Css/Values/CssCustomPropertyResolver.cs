namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class CssCustomPropertyResolver
    {
        // created on demand: most elements declare no custom properties (but are computed with a resolver)
        private Dictionary<String, ICssValue?>? _values;
        private readonly CssStyleDeclaration? _parent;
        private Func<String, ICssValue?>? _resolve;

        public CssCustomPropertyResolver(IEnumerable<ICssProperty> properties, ICssProperties? parent = null)
        {
            // the custom properties of the parent are looked up on demand instead of copied,
            // as there may be thousands of them (e.g., design tokens on :root)
            _parent = parent as CssStyleDeclaration;

            if (parent is not null && _parent is null)
            {
                foreach (var property in parent)
                {
                    if (property.Name.StartsWith("--", StringComparison.Ordinal))
                    {
                        SetValue(property.Name, property.RawValue is CssInvalidValue ? null : property.RawValue);
                    }
                }
            }

            Dictionary<String, Node>? nodes = null;

            foreach (var property in properties)
            {
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
            if (_values is not null && _values.TryGetValue(name, out var value))
            {
                return value;
            }

            if (_parent is not null && name.StartsWith("--", StringComparison.Ordinal))
            {
                var inherited = _parent.GetDeclaration(name)?.RawValue;
                return inherited is CssInvalidValue ? null : inherited;
            }

            return null;
        }

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
