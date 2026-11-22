#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS @scope rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssScopeRule ({ScopeText})")]
    sealed class CssScopeRule : CssGroupingRule, ICssScopeRule
    {
        internal CssScopeRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Scope)
        {
        }

        private String _parsedText;
        private ISelector _start;
        private ISelector _end;

        public String ScopeText { get; set; }

        /// <summary>
        /// Finds the scoping root of the element (CSS Cascade 6, 2.5): the closest inclusive
        /// ancestor matching the scope's start (the parent of the style sheet's owner if there
        /// is none), such that the element is not in a scoping limit below it.
        /// </summary>
        internal Boolean TryGetScopeRoot(IElement element, out IElement root)
        {
            root = null;
            EnsureSelectors();

            if (_start is null)
            {
                // @scope { ... } is scoped to the parent of the <style> element
                var owner = (Owner?.OwnerNode as IElement)?.ParentElement;

                if (owner is null)
                {
                    return false;
                }

                root = owner;
                return IsInScope(element, owner);
            }

            for (var candidate = element; candidate is not null; candidate = candidate.ParentElement)
            {
                if (_start.Match(candidate, candidate) && IsInScope(element, candidate))
                {
                    root = candidate;
                    return true;
                }
            }

            return false;
        }

        private Boolean IsInScope(IElement element, IElement root)
        {
            for (var current = element; current is not null; current = current.ParentElement)
            {
                if (current == root)
                {
                    return true;
                }

                if (_end is not null && _end.Match(current, root))
                {
                    return false;
                }
            }

            return false;
        }

        private void EnsureSelectors()
        {
            var text = ScopeText ?? String.Empty;

            if (!ReferenceEquals(text, _parsedText))
            {
                _start = ParseScopeSelector(text, 0, out var next);
                _end = null;
                var rest = text.Substring(next).TrimStart();

                if (rest.StartsWith("to", StringComparison.OrdinalIgnoreCase))
                {
                    _end = ParseScopeSelector(rest, 2, out _);
                }

                _parsedText = text;
            }
        }

        private ISelector ParseScopeSelector(String text, Int32 start, out Int32 next)
        {
            var open = text.IndexOf('(', start);
            next = start;

            if (open < 0 || text.Substring(start, open - start).Trim().Length > 0)
            {
                return null;
            }

            var depth = 0;

            for (var i = open; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    depth++;
                }
                else if (text[i] == ')' && --depth == 0)
                {
                    next = i + 1;

                    try
                    {
                        return ParseSelector(text.Substring(open + 1, i - open - 1));
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            ScopeText = ((ICssScopeRule)rule).ScopeText;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(GetFormattableRules());
            writer.Write(formatter.Rule(RuleNames.Scope, ScopeText, rules));
        }
    }
}
