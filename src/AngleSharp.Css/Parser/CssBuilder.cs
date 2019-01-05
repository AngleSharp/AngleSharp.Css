namespace AngleSharp.Css.Parser
{
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
            if (token.Data.Is(RuleNames.Media))
            {
                var rule = new CssMediaRule(sheet);
                return CreateMedia(rule, token);
            }
            else if (token.Data.Is(RuleNames.FontFace))
            {
                var rule = new CssFontFaceRule(sheet);
                return CreateFontFace(rule, token);
            }
            else if (token.Data.Is(RuleNames.Keyframes))
            {
                var rule = new CssKeyframesRule(sheet);
                return CreateKeyframes(rule, token);
            }
            else if (token.Data.Is(RuleNames.Import))
            {
                var rule = new CssImportRule(sheet);
                return CreateImport(rule, token);
            }
            else if (token.Data.Is(RuleNames.Charset))
            {
                var rule = new CssCharsetRule(sheet);
                return CreateCharset(rule, token);
            }
            else if (token.Data.Is(RuleNames.Namespace))
            {
                var rule = new CssNamespaceRule(sheet);
                return CreateNamespace(rule, token);
            }
            else if (token.Data.Is(RuleNames.Page))
            {
                var rule = new CssPageRule(sheet);
                return CreatePage(rule, token);
            }
            else if (token.Data.Is(RuleNames.Supports))
            {
                var rule = new CssSupportsRule(sheet);
                return CreateSupports(rule, token);
            }
            else if (token.Data.Is(RuleNames.ViewPort))
            {
                var rule = new CssViewportRule(sheet);
                return CreateViewport(rule, token);
            }
            else if (token.Data.Is(RuleNames.Document))
            {
                var rule = new CssDocumentRule(sheet);
                return CreateDocument(rule, token);
            }
            else if (_options.IsIncludingUnknownRules)
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

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = NextToken();
                CollectTrivia(ref token);
                var media = GetArgument(ref token);

                if (!String.IsNullOrEmpty(media))
                {
                    rule.Media.SetMediaText(media, throwOnError: false);
                }
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
            rule.SelectorText = GetArgument(ref current);
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

        public CssStyleRule CreateStyle(CssStyleRule rule, CssToken current)
        {
            CollectTrivia(ref current);
            rule.SelectorText = GetArgument(ref current);

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

                if (rule != null)
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

            if (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose, CssTokenType.Colon) &&
                token.IsNot(CssTokenType.Semicolon, CssTokenType.CurlyBracketOpen))
            {
                var name = token.Data;

                while (token.Type == CssTokenType.Delim)
                {
                    token = NextToken();
                    name += token.Data;
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

        private CssToken NextToken()
        {
            return _tokenizer.Get();
        }

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

        private void RaiseErrorOccurred(CssParseError code, TextPosition position)
        {
            _tokenizer.RaiseErrorOccurred(code, position);
        }

        #endregion

        #region Fill Inner

        private String CreateValue(ref CssToken token, out Boolean important)
        {
            const String keyword = "!important";
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
