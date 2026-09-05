namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    sealed class CssCustomPropertyResolver
    {
        private readonly Dictionary<String, ICssValue?> _values = new(StringComparer.Ordinal);

        public CssCustomPropertyResolver(IEnumerable<ICssProperty> properties, ICssProperties? parent = null)
        {
            if (parent is not null)
            {
                foreach (var property in parent)
                {
                    if (property.Name.StartsWith("--", StringComparison.Ordinal))
                    {
                        _values[property.Name] = property.RawValue is CssInvalidValue ? null : property.RawValue;
                    }
                }
            }

            var nodes = new Dictionary<String, Node>(StringComparer.Ordinal);

            foreach (var property in properties)
            {
                if (property.Name.StartsWith("--", StringComparison.Ordinal))
                {
                    var value = property.RawValue;

                    if (value is CssAnyValue { IsResolved: true })
                    {
                        _values[property.Name] = value;
                        continue;
                    }

                    var tokens = value is null || value is CssInvalidValue ? null : new CssVariableValue(value.CssText);
                    var keyword = tokens?.Keyword;

                    if (String.Equals(keyword, CssKeywords.Inherit, StringComparison.OrdinalIgnoreCase) ||
                        String.Equals(keyword, CssKeywords.Unset, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    _values[property.Name] = null;

                    if (tokens is not null && tokens.IsValid && !String.Equals(keyword, CssKeywords.Initial, StringComparison.OrdinalIgnoreCase))
                    {
                        nodes[property.Name] = new Node(property.Name, tokens);
                    }
                }
            }

            foreach (var node in nodes.Values)
            {
                foreach (var name in node.Value.Dependencies)
                {
                    if (nodes.TryGetValue(name, out var dependency))
                    {
                        node.Dependencies.Add(dependency);
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
                            var text = node.Value.Substitute(Resolve);
                            _values[node.Name] = text is null ? null : new CssAnyValue(text, isResolved: true);
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

        public ICssValue? Resolve(String name) => _values.TryGetValue(name, out var value) ? value : null;

        private sealed class Node
        {
            public Node(String name, CssVariableValue value)
            {
                Name = name;
                Value = value;
            }

            public String Name { get; }
            public CssVariableValue Value { get; }
            public List<Node> Dependencies { get; } = new();
            public Int32 Index { get; set; } = -1;
            public Int32 LowLink { get; set; }
            public Int32 NextDependency { get; set; }
            public Boolean Active { get; set; }
        }
    }
}
