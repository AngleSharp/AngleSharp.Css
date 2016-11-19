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
                    return CreateNormalRule(sheet, token);
            }
        }

        public ICssRule CreateNormalRule(ICssStyleSheet sheet, CssToken token)
        {
            var rule = new CssStyleRule(sheet);
            return CreateStyle(rule, token) ? rule : null;
        }

        public ICssRule CreateAtRule(ICssStyleSheet sheet, CssToken token)
        {
            if (token.Data.Is(RuleNames.Media))
            {
                var rule = new CssMediaRule(sheet);
                return CreateMedia(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.FontFace))
            {
                var rule = new CssFontFaceRule(sheet);
                return CreateFontFace(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.Keyframes))
            {
                var rule = new CssKeyframesRule(sheet);
                return CreateKeyframes(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.Import))
            {
                var rule = new CssImportRule(sheet);
                return CreateImport(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.Charset))
            {
                var rule = new CssCharsetRule(sheet);
                return CreateCharset(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.Namespace))
            {
                var rule = new CssNamespaceRule(sheet);
                return CreateNamespace(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.Page))
            {
                var rule = new CssPageRule(sheet);
                return CreatePage(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.Supports))
            {
                var rule = new CssSupportsRule(sheet);
                return CreateSupports(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.ViewPort))
            {
                var rule = new CssViewportRule(sheet);
                return CreateViewport(rule, token) ? rule : null;
            }
            else if (token.Data.Is(RuleNames.Document))
            {
                var rule = new CssDocumentRule(sheet);
                return CreateDocument(rule, token) ? rule : null;
            }
            else if (_options.IsIncludingUnknownRules)
            {
                return CreateUnknownAtRule(sheet, token);
            }

            RaiseErrorOccurred(CssParseError.UnknownAtRule, token.Position);
            JumpToRuleEnd(ref token);
            return null;
        }

        public Boolean CreateCharset(CssCharsetRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.String)
            {
                rule.CharacterSet = token.Data;
            }

            JumpToEnd(ref token);
            return true;
        }

        public Boolean CreateDocument(CssDocumentRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            FillFunctions(function => rule.Add(function), ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                FillRules(rule);
                return true;
            }

            SkipDeclarations(token);
            return false;
        }

        public Boolean CreateViewport(CssViewportRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                FillDeclarations(rule);
                return true;
            }

            SkipDeclarations(token);
            return false;
        }

        public Boolean CreateFontFace(CssFontFaceRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                FillDeclarations(rule);
                return true;
            }

            SkipDeclarations(token);
            return false;
        }

        public Boolean CreateImport(CssImportRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = NextToken();
                CollectTrivia(ref token);
                FillMediaList(rule.Media, CssTokenType.Semicolon, ref token);
            }

            CollectTrivia(ref token);
            JumpToEnd(ref token);
            return true;
        }

        public Boolean CreateKeyframes(CssKeyframesRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            rule.Name = GetRuleName(ref token);
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.CurlyBracketOpen)
            {
                FillKeyframeRules(rule);
                return true;
            }

            SkipDeclarations(token);
            return false;
        }

        public Boolean CreateMedia(CssMediaRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            FillMediaList(rule.Media, CssTokenType.CurlyBracketOpen, ref token);
            CollectTrivia(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketOpen))
                {
                    if (token.Type == CssTokenType.Semicolon)
                    {
                        return false;
                    }

                    token = NextToken();
                }
            }

            FillRules(rule);
            return true;
        }

        public Boolean CreateNamespace(CssNamespaceRule rule, CssToken current)
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
            return true;
        }

        public Boolean CreatePage(CssPageRule rule, CssToken current)
        {
            current = NextToken();
            rule.Selector = CreateSelector(ref current);
            CollectTrivia(ref current);

            if (current.Type == CssTokenType.CurlyBracketOpen)
            {
                FillDeclarations(rule.Style, NextToken());
                return true;
            }

            SkipDeclarations(current);
            return false;
        }

        public Boolean CreateSupports(CssSupportsRule rule, CssToken current)
        {
            var token = NextToken();
            CollectTrivia(ref token);
            var condition = AggregateCondition(ref token);

            if (condition != null)
            {
                rule.Condition = condition;
                CollectTrivia(ref token);

                if (token.Type == CssTokenType.CurlyBracketOpen)
                {
                    FillRules(rule);
                    return true;
                }
            }

            SkipDeclarations(token);
            return false;
        }

        public Boolean CreateStyle(CssStyleRule rule, CssToken current)
        {
            rule.Selector = CreateSelector(ref current);
            FillDeclarations(rule.Style, NextToken());
            return rule.Selector != null;
        }

        public Boolean CreateKeyframeRule(CssKeyframeRule rule, CssToken current)
        {
            CollectTrivia(ref current);
            rule.Key = CreateKeyframeSelector(ref current);
            var end = FillDeclarations(rule.Style, NextToken());
            return rule.Key != null;
        }

        private TextPosition FillKeyframeRules(CssKeyframesRule parentRule)
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

            return token.Position;
        }

        private TextPosition FillDeclarations(CssStyleDeclaration style, Func<String, ICssProperty> createProperty)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(createProperty, ref token);

                if (property != null)
                {
                    style.SetProperty(property);
                }

                CollectTrivia(ref token);
            }

            return token.Position;
        }

        private TextPosition FillDeclarations(CssDeclarationRule rule)
        {
            var token = NextToken();
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(rule.CreateProperty, ref token);

                if (property != null)
                {
                    rule.SetProperty(property);
                }

                CollectTrivia(ref token);
            }

            return token.Position;
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

        private TextPosition FillRules(CssGroupingRule group)
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

            return token.Position;
        }

        private void FillMediaList(MediaList list, CssTokenType end, ref CssToken token)
        {
            if (token.Type != end)
            {
                while (token.Type != CssTokenType.EndOfFile)
                {
                    var medium = new CssMedium();
                    var result = CreateMedium(medium, ref token);

                    if (result)
                    {
                        list.Add(medium);
                    }

                    if (token.Type != CssTokenType.Comma)
                        break;

                    token = NextToken();
                    CollectTrivia(ref token);
                }

                if (token.Type != end || list.Length == 0)
                {
                    list.Clear();
                    list.Add(new CssMedium
                    {
                        IsInverse = true,
                        Type = CssKeywords.All
                    });
                }
            }
        }

        #endregion

        #region API

        /// <summary>
        /// Creates a list of CssMedium objects.
        /// </summary>
        public List<CssMedium> CreateMedia(ref CssToken token)
        {
            var list = new List<CssMedium>();
            CollectTrivia(ref token);

            while (token.Type != CssTokenType.EndOfFile)
            {
                var medium = new CssMedium();
                var result = CreateMedium(medium, ref token);

                if (!result || token.IsNot(CssTokenType.Comma, CssTokenType.EndOfFile))
                    throw new DomException(DomError.Syntax);

                token = NextToken();
                CollectTrivia(ref token);
                list.Add(medium);
            }

            return list;
        }

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
        /// Called before any token in the value regime had been seen.
        /// </summary>
        public IConditionFunction CreateCondition(ref CssToken token)
        {
            CollectTrivia(ref token);
            return AggregateCondition(ref token);
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        public KeyframeSelector CreateKeyframeSelector(ref CssToken token)
        {
            var keys = new List<String>();
            var valid = true;
            var start = token.Position;
            CollectTrivia(ref token);

            while (token.Type != CssTokenType.EndOfFile)
            {
                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;

                    if (token.Type != CssTokenType.Comma)
                    {
                        valid = false;
                    }
                    else
                    {
                        token = NextToken();
                    }

                    CollectTrivia(ref token);
                }

                if (token.Type == CssTokenType.Percentage || 
                   (token.Type == CssTokenType.Ident && token.Data.IsOneOf(CssKeywords.From, CssKeywords.To)))
                {
                    keys.Add(token.Data);
                }
                else
                {
                    valid = false;
                }

                token = NextToken();
                CollectTrivia(ref token);
            }

            if (!valid)
            {
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);
            }

            return new KeyframeSelector(keys);
        }

        /// <summary>
        /// Called when the document functions have to been found.
        /// </summary>
        public DocumentFunctions CreateFunctions(ref CssToken token)
        {
            var functions = new List<IDocumentFunction>();
            CollectTrivia(ref token);
            FillFunctions(function => functions.Add(function), ref token);
            return new DocumentFunctions(functions);
        }

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        public TextPosition FillDeclarations(CssStyleDeclaration style, CssToken token)
        {
            CollectTrivia(ref token);

            while (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose))
            {
                var factory = _context.GetService<ICssPropertyFactory>() ?? Factory.Property;
                var property = CreateDeclarationWith(factory.Create, ref token);

                if (property != null)
                {
                    style.SetProperty(property);
                }

                CollectTrivia(ref token);
            }

            return token.Position;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public ICssProperty CreateDeclarationWith(Func<String, ICssProperty> createProperty, ref CssToken token)
        {
            var property = default(ICssProperty);
            var start = token.Position;

            if (token.IsNot(CssTokenType.EndOfFile, CssTokenType.CurlyBracketClose, CssTokenType.Colon) &&
                token.IsNot(CssTokenType.Delim, CssTokenType.Semicolon, CssTokenType.CurlyBracketOpen))
            {
                var propertyName = token.Data;
                token = NextToken();
                CollectTrivia(ref token);

                if (token.Type == CssTokenType.Colon)
                {
                    property = _options.IsIncludingUnknownDeclarations ? new CssUnknownProperty(propertyName) : createProperty(propertyName);

                    if (property != null)
                    {
                        var important = false;
                        token = NextToken();
                        CollectTrivia(ref token);
                        var value = CreateValue(ref token, Symbols.CurlyBracketClose, out important);

                        if (String.IsNullOrEmpty(value))
                        {
                            RaiseErrorOccurred(CssParseError.ValueMissing, token.Position);
                            property = null;
                        }
                        else if (property != null)
                        {
                            property.Value = value;
                            property.IsImportant = important;
                        }
                    }
                    else
                    {
                        RaiseErrorOccurred(CssParseError.UnknownDeclarationName, start);
                    }

                    CollectTrivia(ref token);
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

            return property;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        public ICssProperty CreateDeclaration(ref CssToken token)
        {
            CollectTrivia(ref token);
            var factory = _context.GetService<ICssPropertyFactory>();
            return CreateDeclarationWith(factory.Create, ref token);
        }

        /// <summary>
        /// Scans the current medium for the @media or @import rule.
        /// </summary>
        public Boolean CreateMedium(CssMedium medium, ref CssToken token)
        {
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.Ident)
            {
                var identifier = token.Data;

                if (identifier.Isi(CssKeywords.Not))
                {
                    medium.IsInverse = true;
                    token = NextToken();
                    CollectTrivia(ref token);
                }
                else if (identifier.Isi(CssKeywords.Only))
                {
                    medium.IsExclusive = true;
                    token = NextToken();
                    CollectTrivia(ref token);
                }
            }

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = token.Data;
                token = NextToken();
                CollectTrivia(ref token);

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(CssKeywords.And))
                {
                    return true;
                }

                token = NextToken();
                CollectTrivia(ref token);
            }

            do
            {
                if (token.Type != CssTokenType.RoundBracketOpen)
                {
                    return false;
                }

                token = NextToken();
                CollectTrivia(ref token);
                var feature = CreateFeature(ref token);

                if (feature != null)
                {
                    medium.Add(feature);
                }

                if (token.Type != CssTokenType.RoundBracketClose)
                {
                    return false;
                }

                token = NextToken();
                CollectTrivia(ref token);

                if (feature == null)
                {
                    return false;
                }

                if (token.Type != CssTokenType.Ident || !token.Data.Isi(CssKeywords.And))
                    break;

                token = NextToken();
                CollectTrivia(ref token);
            }
            while (token.Type != CssTokenType.EndOfFile);

            return true;
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

        #region Conditions

        private IConditionFunction AggregateCondition(ref CssToken token)
        {
            var condition = ExtractCondition(ref token);

            if (condition != null)
            {
                CollectTrivia(ref token);
                var conjunction = token.Data;
                var isAnd = conjunction.Isi(CssKeywords.And);
                var isOr = conjunction.Isi(CssKeywords.Or);

                if (isAnd || isOr)
                {
                    token = NextToken();
                    CollectTrivia(ref token);
                    var conditions = MultipleConditions(condition, conjunction, ref token);

                    if (isAnd)
                    {
                        condition = new AndCondition(conditions);
                    }
                    else if (isOr)
                    {
                        condition = new OrCondition(conditions);
                    }
                }
            }

            return condition;
        }

        private IConditionFunction ExtractCondition(ref CssToken token)
        {
            if (token.Type == CssTokenType.RoundBracketOpen)
            {
                token = NextToken();
                CollectTrivia(ref token);
                var condition = token.Type == CssTokenType.RoundBracketClose ?
                    new EmptyCondition() :
                    AggregateCondition(ref token);

                if (condition != null)
                {
                    condition = new GroupCondition(condition);
                }
                else if (token.Type == CssTokenType.Ident)
                {
                    condition = DeclarationCondition(ref token);
                }

                if (token.Type == CssTokenType.RoundBracketClose)
                {
                    token = NextToken();
                    CollectTrivia(ref token);
                }

                return condition;
            }
            else if (token.Data.Isi(CssKeywords.Not))
            {
                token = NextToken();
                CollectTrivia(ref token);
                var condition = ExtractCondition(ref token);
                return new NotCondition(condition);
            }

            return null;
        }

        private IConditionFunction DeclarationCondition(ref CssToken token)
        {
            var factory = _context.GetService<ICssPropertyFactory>();
            var propertyName = token.Data;
            var property = factory.Create(propertyName) ?? new CssUnknownProperty(propertyName);
            var declaration = default(DeclarationCondition);
            token = NextToken();
            CollectTrivia(ref token);

            if (token.Type == CssTokenType.Colon)
            {
                token = NextToken();
                var important = false;
                var result = CreateValue(ref token, Symbols.RoundBracketClose, out important);
                property.IsImportant = important;

                if (result != null)
                {
                    declaration = new DeclarationCondition(property, result);
                }
            }

            return declaration;
        }

        private List<IConditionFunction> MultipleConditions(IConditionFunction condition, String connector, ref CssToken token)
        {
            var list = new List<IConditionFunction>();
            CollectTrivia(ref token);
            list.Add(condition);

            while (token.Type != CssTokenType.EndOfFile)
            {
                condition = ExtractCondition(ref token);

                if (condition == null)
                    break;

                list.Add(condition);

                if (!token.Data.Isi(connector))
                    break;

                token = NextToken();
                CollectTrivia(ref token);
            }

            return list;
        }

        #endregion

        #region Fill Inner

        private void FillFunctions(Action<IDocumentFunction> add, ref CssToken token)
        {
            var factory = _context.GetService<IDocumentFunctionFactory>();

            do
            {
                if (token.Type != CssTokenType.Url && token.Type != CssTokenType.Function)
                    break;

                var functionName = token.Data;
                var data = token.Data;

                if (token.Type == CssTokenType.Url)
                {
                    functionName = FunctionNames.Url;
                }
                else
                {
                    token = NextToken();
                    data = token.Type == CssTokenType.String ? token.Data : String.Empty;

                    while (token.Type != CssTokenType.RoundBracketClose)
                    {
                        token = NextToken();
                    }
                }

                var function = factory?.Create(functionName, data);
                token = NextToken();
                CollectTrivia(ref token);

                if (function != null)
                {
                    add.Invoke(function);
                }

                if (token.Type != CssTokenType.Comma)
                    break;

                token = NextToken();
                CollectTrivia(ref token);
            }
            while (token.Type != CssTokenType.EndOfFile);
        }

        private String CreateValue(ref CssToken token, Char closing, out Boolean important)
        {
            var keyword = "!important";
            var value = _tokenizer.ContentTo(ref token, ';', closing).Trim();
            important = value.EndsWith(keyword, StringComparison.OrdinalIgnoreCase);
            return important ? value.Substring(0, value.Length - keyword.Length).Trim() : value;
        }

        private ISelector CreateSelector(ref CssToken token)
        {
            var value = _tokenizer.ContentTo(ref token, '{');
            var parser = _context.GetService<ICssSelectorParser>();
            return parser.ParseSelector(value);
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

        private IMediaFeature CreateFeature(ref CssToken token)
        {
            if (token.Type == CssTokenType.Ident)
            {
                var start = token.Position;
                var name = token.Data;
                var value = default(String);
                token = NextToken();

                if (token.Type == CssTokenType.Colon)
                {
                    token = NextToken();
                    var important = false;
                    value = CreateValue(ref token, Symbols.RoundBracketClose, out important);
                }

                return new MediaFeature(name, value);
            }
            else
            {
                JumpToArgEnd(ref token);
            }

            return null;
        }

        #endregion
    }
}
