#nullable disable
namespace AngleSharp.Css.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser.Tokens;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for details.
    /// </summary>
    sealed class CssBuilder
    {
        #region Fields

        private static readonly Dictionary<String, Int32> AtRuleMap = new Dictionary<String, Int32>(StringComparer.OrdinalIgnoreCase)
        {
            { RuleNames.Media, 1 },
            { RuleNames.FontFace, 2 },
            { RuleNames.Keyframes, 3 },
            { RuleNames.Import, 4 },
            { RuleNames.Charset, 5 },
            { RuleNames.Namespace, 6 },
            { RuleNames.Page, 7 },
            { RuleNames.Supports, 8 },
            { RuleNames.ViewPort, 9 },
            { RuleNames.Document, 10 },
            { RuleNames.CounterStyle, 11 },
            { RuleNames.FontFeatureValues, 12 },
        };

        private readonly CssTokenizer _tokenizer;
        private readonly CssParserOptions _options;
        private readonly IBrowsingContext _context;

        #endregion

        #region ctor

        public CssBuilder(CssParserOptions options, CssTokenizer tokenizer, IBrowsingContext context)
        {
            _tokenizer = tokenizer;
            _options = options;
            _context = context;
        }

        #endregion

        #region Create Rules

        public ICssRule CreateRule(ICssStyleSheet sheet, CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                    return CreateAtRule(sheet, token);

                case CssTokenType.CurlyBracketOpen:
                    RaiseErrorOccurred(CssParseError.InvalidBlockStart, token.Position);
                    JumpToRuleEnd(ref token);
                    return null;

                case CssTokenType.String:
                case CssTokenType.Url:
                case CssTokenType.CurlyBracketClose:
                case CssTokenType.RoundBracketClose:
                case CssTokenType.SquareBracketClose:
                    RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
                    JumpToRuleEnd(ref token);
                    return null;

                default:
                    return CreateStyleRule(sheet, token);
            }
        }

        private ICssRule CreateStyleRule(ICssStyleSheet sheet, CssToken token)
        {
            var rule = new CssStyleRule(sheet);
            return CreateStyle(rule, token);
        }

        private ICssRule CreateAtRule(ICssStyleSheet sheet, CssToken token)
        {
            if (AtRuleMap.TryGetValue(token.Data, out var ruleId))
            {
                switch (ruleId)
                {
                    case 1: return CreateMedia(new CssMediaRule(sheet), token);
                    case 2: return CreateFontFace(new CssFontFaceRule(sheet), token);
                    case 3: return CreateKeyframes(new CssKeyframesRule(sheet), token);
                    case 4: return CreateImport(new CssImportRule(sheet), token);
                    case 5: return CreateCharset(new CssCharsetRule(sheet), token);
                    case 6: return CreateNamespace(new CssNamespaceRule(sheet), token);
                    case 7: return CreatePage(new CssPageRule(sheet), token);
                    case 8: return CreateSupports(new CssSupportsRule(sheet), token);
                    case 9: return CreateViewport(new CssViewportRule(sheet), token);
                    case 10: return CreateDocument(new CssDocumentRule(sheet), token);
                    case 11: return CreateCounterStyle(new CssCounterStyleRule(sheet), token);
                    case 12: return CreateFontFeatureValues(new CssFontFeatureValuesRule(sheet), token);
                }
            }

            if (_options.IsIncludingUnknownRules)
            {
                return CreateUnknownAtRule(sheet, token);
            }

            RaiseErrorOccurred(CssParseError.UnknownAtRule, token.Position);
            JumpToRuleEnd(ref token);
            return null;
        }

        private CssCharsetRule CreateCharset(CssCharsetRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.String)
            {
                rule.CharacterSet = token.Data;
            }

            JumpToEnd(ref token);
            return rule;
        }

        private CssDocumentRule CreateDocument(CssDocumentRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            var functions = GetArgument(ref token);
            var result = rule.SetConditionText(functions, throwOnError: false);
            CollectTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(token);
            }
            else if (FillRules(rule) && result)
            {
                return rule;
            }

            return null;
        }

        private CssViewportRule CreateViewport(CssViewportRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(token);
                return null;
            }

            FillDeclarations(rule);
            return rule;
        }

        private CssFontFaceRule CreateFontFace(CssFontFaceRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(token);
                return null;
            }

            FillDeclarations(rule);
            return rule;
        }

        private CssImportRule CreateImport(CssImportRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (!token.Is(CssTokenType.String, CssTokenType.Url))
            {
                JumpToEnd(ref token);
                return null;
            }

            rule.Href = token.Data;
            token = NextToken();
            CollectTrivia(ref token);
            var media = GetArgument(ref token);

            if (!String.IsNullOrEmpty(media))
            {
                rule.Media.SetMediaText(media, throwOnError: false);
            }

            CollectTrivia(ref token);
            JumpToEnd(ref token);
            return rule;
        }

        private CssKeyframesRule CreateKeyframes(CssKeyframesRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            rule.Name = GetRuleName(ref token);
            CollectTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(token);
                return null;
            }

            FillKeyframeRules(rule);
            return rule;
        }

        private CssMediaRule CreateMedia(CssMediaRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            var media = GetArgument(ref token);
            rule.Media.SetMediaText(media, throwOnError: false);
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketOpen))
            {
                if (token.Type == CssTokenType.Semicolon)
                {
                    return null;
                }

                token = NextToken();
            }

            FillRules(rule);
            return rule;
        }

        private CssNamespaceRule CreateNamespace(CssNamespaceRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            rule.Prefix = GetRuleName(ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.Url)
            {
                rule.NamespaceUri = token.Data;
            }

            JumpToEnd(ref token);
            return rule;
        }

        private CssPageRule CreatePage(CssPageRule rule, CssToken current)
        {
            current = NextToken();
            var selectorText = GetArgument(ref current);

            rule.SelectorText = selectorText;

            if (rule.Selector is null && _options.IsToleratingInvalidSelectors)
            {
                rule.SetInvalidSelector(selectorText);
            }

            CollectTrivia(ref current);

            if (current.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(current);
                return null;
            }

            FillDeclarations(rule.Style, NextToken());
            return rule;
        }

        private CssSupportsRule CreateSupports(CssSupportsRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            var conditions = GetArgument(ref token);
            var result = rule.SetConditionText(conditions, throwOnError: false);
            CollectTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(token);
            }
            else if (FillRules(rule) && result)
            {
                return rule;
            }

            return null;
        }

        private CssFontFeatureValuesRule CreateFontFeatureValues(CssFontFeatureValuesRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            rule.FamilyName = GetArgument(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                JumpToRuleEnd(ref token);
                return rule;
            }

            SkipDeclarations(token);
            return null;
        }

        private CssCounterStyleRule CreateCounterStyle(CssCounterStyleRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            rule.StyleName = GetArgument(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(token);
                return null;
            }

            FillDeclarations(rule);
            return rule;
        }

        public CssStyleRule CreateStyle(CssStyleRule rule, CssToken current)
        {
            CollectTrivia(ref current);
            var selectorText = GetArgument(ref current);

            rule.SelectorText = selectorText;

            if (rule.Selector is null && _options.IsToleratingInvalidSelectors)
            {
                rule.SetInvalidSelector(selectorText);
            }

            if (current.Type != CssTokenType.CurlyBracketOpen)
            {
                SkipDeclarations(current);
                return null;
            }

            FillDeclarations(rule.Style, NextToken());
            return rule;
        }

        public CssKeyframeRule CreateKeyframeRule(CssKeyframeRule rule, CssToken current)
        {
            CollectTrivia(ref current);
            rule.KeyText = GetArgument(ref current);
            FillDeclarations(rule.Style, NextToken());
            return rule;
        }

        private CssKeyframesRule FillKeyframeRules(CssKeyframesRule parentRule)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var rule = new CssKeyframeRule(parentRule.Owner);
                CreateKeyframeRule(rule, token);
                token = NextToken();
                CollectTrivia(ref token);
                parentRule.Add(rule);
            }

            return parentRule;
        }

        private CssDeclarationRule FillDeclarations(CssDeclarationRule rule)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                CreateDeclarationWith(rule, ref token);
                CollectTrivia(ref token);
            }

            return rule;
        }

        private CssUnknownRule CreateUnknownAtRule(ICssStyleSheet sheet, CssToken current)
        {
            var token = NextToken();

            while (token.IsNot(CssTokenType.CurlyBracketOpen, CssTokenType.Semicolon, CssTokenType.EndOfFile))
            {
                token = NextToken();
            }

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                var curly = 1;

                do
                {
                    token = NextToken();

                    switch (token.Type)
                    {
                        case CssTokenType.CurlyBracketOpen:
                            curly++;
                            break;
                        case CssTokenType.CurlyBracketClose:
                            curly--;
                            break;
                        case CssTokenType.EndOfFile:
                            curly = 0;
                            break;
                    }
                }
                while (curly != 0);
            }

            var range = new TextRange(current.Position, token.Position);
            return new CssUnknownRule(sheet, current.Data, new TextView(sheet.Source, range));
        }

        private Boolean FillRules(CssGroupingRule group)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateRule(group.Owner, token);
                token = NextToken();
                CollectTrivia(ref token);

                if (rule != null)
                {
                    group.Add(rule);
                }
            }

            return token.Type == CssTokenType.CurlyBracketClose;
        }

        #endregion

        #region API

        /// <summary>
        /// Creates as many rules as possible.
        /// </summary>
        /// <returns>The found rules.</returns>
        public TextPosition CreateRules(CssStyleSheet sheet)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.Type != CssTokenType.EndOfFile)
            {
                var rule = CreateRule(sheet, token);
                token = NextToken();
                CollectTrivia(ref token);

                if (rule is not null)
                {
                    sheet.Add(rule);
                }
            }

            return token.Position;
        }

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        public CssStyleDeclaration FillDeclarations(CssStyleDeclaration style, CssToken token)
        {
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                CreateDeclarationWith(style, ref token);
                CollectTrivia(ref token);
            }

            return style;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public void CreateDeclarationWith(ICssProperties properties, ref CssToken token)
        {
            CollectTrivia(ref token);
            var start = token.Position;

            if (!_options.IsExcludingNesting && token.IsPotentiallyNested() && properties is ICssStyleDeclaration decl && decl.Parent is CssStyleRule style)
            {
                var factory = _context.GetService<DefaultAttributeSelectorFactory>();
                var rule = new CssStyleRule(style.Owner);
                var previous = factory.Unregister("&");
                factory.Register("&", (_, _, _, _) =>
                {
                    rule.IsNested = true;
                    return new ReferencedNestedSelector(style.Selector);
                });
                var result = CreateStyle(rule, token);
                factory.Unregister("&");

                if (previous is not null)
                {
                    factory.Register("&", previous);
                }

                if (result is not null)
                {
                    style.Add(result);
                    token = NextToken();
                    return;
                }
            }

            if (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose, CssTokenType.Colon) &&
                token.IsNot(CssTokenType.Semicolon, CssTokenType.CurlyBracketOpen))
            {
                var name = token.Data;

                if (token.Type == CssTokenType.Delim)
                {
                    var sb = StringBuilderPool.Obtain();
                    sb.Append(name);

                    while (token.Type == CssTokenType.Delim)
                    {
                        token = NextToken();
                        sb.Append(token.Data);
                    }

                    name = sb.ToPool();
                }

                token = NextToken();
                CollectTrivia(ref token);

                if (token.Type == CssTokenType.Colon)
                {
                    token = NextToken();
                    CollectTrivia(ref token);
                    var value = CreateValue(ref token, out var important);

                    if (String.IsNullOrEmpty(value))
                    {
                        RaiseErrorOccurred(CssParseError.ValueMissing, token.Position);
                    }
                    else
                    {
                        properties.SetProperty(name, value, important ? CssKeywords.Important : null);
                    }
                }
                else
                {
                    RaiseErrorOccurred(CssParseError.ColonMissing, token.Position);
                }

                JumpToDeclEnd(ref token);
            }
            else if (token.Type != CssTokenType.EndOfFile)
            {
                RaiseErrorOccurred(CssParseError.IdentExpected, start);
                JumpToDeclEnd(ref token);
            }

            if (token.Type == CssTokenType.Semicolon)
            {
                token = NextToken();
            }
        }

        #endregion

        #region Helpers

        private void JumpToEnd(ref CssToken current)
        {
            while (current.IsNot(CssTokenType.EndOfFile, CssTokenType.Semicolon))
            {
                current = NextToken();
            }
        }

        private void JumpToRuleEnd(ref CssToken current)
        {
            var scopes = 0;

            while (current.Type != CssTokenType.EndOfFile)
            {
                if (current.Type == CssTokenType.CurlyBracketOpen)
                {
                    scopes++;
                }
                else if (current.Type == CssTokenType.CurlyBracketClose)
                {
                    scopes--;
                }

                if (scopes <= 0 && (current.Is(CssTokenType.CurlyBracketClose, CssTokenType.Semicolon)))
                {
                    break;
                }

                current = NextToken();
            }
        }

        private void JumpToArgEnd(ref CssToken current)
        {
            var arguments = 0;

            while (current.Type != CssTokenType.EndOfFile)
            {
                if (current.Type == CssTokenType.RoundBracketOpen)
                {
                    arguments++;
                }
                else if (arguments <= 0 && current.Type == CssTokenType.RoundBracketClose)
                {
                    break;
                }
                else if (current.Type == CssTokenType.RoundBracketClose)
                {
                    arguments--;
                }

                current = NextToken();
            }
        }

        private void JumpToDeclEnd(ref CssToken current)
        {
            var scopes = 0;

            while (current.Type != CssTokenType.EndOfFile)
            {
                if (current.Type == CssTokenType.CurlyBracketOpen)
                {
                    scopes++;
                }
                else if (scopes <= 0 && (current.Is(CssTokenType.CurlyBracketClose, CssTokenType.Semicolon)))
                {
                    break;
                }
                else if (current.Type == CssTokenType.CurlyBracketClose)
                {
                    scopes--;
                }

                current = NextToken();
            }
        }

        private CssToken NextToken() => _tokenizer.Get();

        private void CollectTrivia(ref CssToken token)
        {
            var storeComments = false;

            while (token.Type == CssTokenType.Whitespace || token.Type == CssTokenType.Comment || token.Type == CssTokenType.Cdc || token.Type == CssTokenType.Cdo)
            {
                if (storeComments && token.Type == CssTokenType.Comment)
                {
                    var comment = new CssComment(token.Data);
                    //TODO What should be done with the comment?
                }

                token = _tokenizer.Get();
            }
        }

        private void SkipDeclarations(CssToken token)
        {
            RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
            JumpToRuleEnd(ref token);
        }

        private void RaiseErrorOccurred(CssParseError code, TextPosition position) =>
            _tokenizer.RaiseErrorOccurred(code, position);

        #endregion

        #region Fill Inner

        private String CreateValue(ref CssToken token, out Boolean important)
        {
            var keyword = CssKeywords.BangImportant;
            var value = _tokenizer.ContentFrom(token.Position.Position);
            important = value.EndsWith(keyword, StringComparison.OrdinalIgnoreCase);
            token = NextToken();
            return important ? value.Substring(0, value.Length - keyword.Length).Trim() : value;
        }

        private String GetArgument(ref CssToken token)
        {
            var argument = _tokenizer.ContentFrom(token.Position.Position);
            token = NextToken();
            return argument;
        }

        private String GetRuleName(ref CssToken token)
        {
            var name = String.Empty;

            if (token.Type == CssTokenType.Ident)
            {
                name = token.Data;
                token = NextToken();
            }

            return name;
        }

        #endregion
    }
}
