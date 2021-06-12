namespace AngleSharp.Css.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Css.Dom.Events;
    using AngleSharp.Css.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// The CSS tokenizer.
    /// See http://dev.w3.org/csswg/css-syntax/#tokenization for more details.
    /// </summary>
    sealed class CssTokenizer : BaseTokenizer
	{
		#region Fields

        private TextPosition _position;

        #endregion

        #region Events

        /// <summary>
        /// Fired in case of a parse error.
        /// </summary>
        public event EventHandler<CssErrorEvent> Error;

        #endregion

        #region ctor

        /// <summary>
        /// CSS Tokenization
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public CssTokenizer(TextSource source)
            : base(source)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public CssToken Get()
        {
            var current = GetNext();
            _position = GetCurrentPosition();
            return Data(current);
        }

        /// <summary>
        /// Gets the trimmed content until either '}' or ';' is hit.
        /// </summary>
        /// <returns>The trimmed string.</returns>
        public String ContentFrom(Int32 position)
        {
            var sb = StringBuilderPool.Obtain();
            Back(Position - position);
            var current = Current;
            var spaced = 0;

            while (!(current is Symbols.EndOfFile or Symbols.Semicolon or Symbols.CurlyBracketOpen or Symbols.CurlyBracketClose))
            {
                var token = Data(current);

                if (token.Type == CssTokenType.Whitespace)
                {
                    spaced++;
                    sb.Append(current);
                    current = GetNext();
                }
                else
                {
                    var length = Position - position;
                    Back(length++);
                    current = Current;
                    spaced = 0;

                    while (length > 0)
                    {
                        sb.Append(current);
                        --length;
                        current = GetNext();
                    }
                }
                
                position = Position;
            }

            if (spaced > 0)
            {
                sb.Remove(sb.Length - spaced, spaced);
            }

            Back();
            return sb.ToPool();
        }

        internal void RaiseErrorOccurred(CssParseError error, TextPosition position)
        {
            Error?.Invoke(this, new CssErrorEvent(error, position));
        }

        #endregion

        #region States

        /// <summary>
        /// 4.4.1. Data state
        /// </summary>
        private CssToken Data(Char current)
        {
            _position = GetCurrentPosition();

            switch (current)
            {
                case Symbols.FormFeed:
                case Symbols.LineFeed:
                case Symbols.CarriageReturn:
                case Symbols.Tab:
                case Symbols.Space:
                    return NewWhitespace(current);

                case Symbols.DoubleQuote:
                    return StringDQ();

                case Symbols.Num:
                    return HashStart();

                case Symbols.Dollar:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.Ends);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.SingleQuote:
                    return StringSQ();

                case Symbols.RoundBracketOpen:
                    return NewOpenRound();

                case Symbols.RoundBracketClose:
                    return NewCloseRound();

                case Symbols.Asterisk:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.InText);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Plus:
                {
                    var c1 = GetNext();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = GetNext();
                        Back(2);

                        if (c1.IsDigit() || (c1 == Symbols.Dot && c2.IsDigit()))
                        {
                            return NumberStart(current);
                        }
                    }
                    else
                    {
                        Back();
                    }

                    return NewDelimiter(current);
                }

                case Symbols.Comma:
                    return NewComma();

                case Symbols.Dot:
                {
                    var c = GetNext();

                    if (c.IsDigit())
                    {
                        return NumberStart(GetPrevious());
                    }

                    return NewDelimiter(GetPrevious());
                }

                case Symbols.Minus:
                {
                    var c1 = GetNext();

                    if (c1 != Symbols.EndOfFile)
                    {
                        var c2 = GetNext();
                        Back(2);

                        if (c1.IsDigit() || (c1 == Symbols.Dot && c2.IsDigit()))
                        {
                            return NumberStart(current);
                        }
                        else if (c1.IsNameStart())
                        {
                            return IdentStart(current);
                        }
                        else if (c1 == Symbols.ReverseSolidus && !c2.IsLineBreak() && c2 != Symbols.EndOfFile)
                        {
                            return IdentStart(current);
                        }
                        else if (c1 == Symbols.Minus && c2 == Symbols.GreaterThan)
                        {
                            Advance(2);
                            return NewCloseComment();
                        }
                    }
                    else
                    {
                        Back();
                    }

                    return NewDelimiter(current);
                }

                case Symbols.Solidus:
                    current = GetNext();

                    if (current == Symbols.Asterisk)
                    {
                        return Comment();
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.ReverseSolidus:
                    current = GetNext();

                    if (current.IsLineBreak())
                    {
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        return NewDelimiter(GetPrevious());
                    }
                    else if (current == Symbols.EndOfFile)
                    {
                        RaiseErrorOccurred(CssParseError.EOF);
                        return NewDelimiter(GetPrevious());
                    }

                    return IdentStart(GetPrevious());
                case Symbols.Colon:
                    return NewColon();

                case Symbols.Semicolon:
                    return NewSemicolon();

                case Symbols.LessThan:
                    current = GetNext();

                    if (current == Symbols.ExclamationMark)
                    {
                        current = GetNext();

                        if (current == Symbols.Minus)
                        {
                            current = GetNext();

                            if (current == Symbols.Minus)
                            {
                                return NewOpenComment();
                            }

                            current = GetPrevious();
                        }

                        current = GetPrevious();
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.At:
                    return AtKeywordStart();

                case Symbols.SquareBracketOpen:
                    return NewOpenSquare();

                case Symbols.SquareBracketClose:
                    return NewCloseSquare();

                case Symbols.Accent:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.Begins);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.CurlyBracketOpen:
                    return NewOpenCurly();

                case Symbols.CurlyBracketClose:
                    return NewCloseCurly();

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return NumberStart(current);

                case 'U':
                case 'u':
                    return FoundU(current);

                case Symbols.Pipe:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.InToken);
                    }
                    else if (current == Symbols.Pipe)
                    {
                        return NewColumn();
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Tilde:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.InList);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.EndOfFile:
                    return NewEof();

                case Symbols.ExclamationMark:
                    current = GetNext();

                    if (current == Symbols.Equality)
                    {
                        return NewMatch(CombinatorSymbols.Unlike);
                    }

                    return NewDelimiter(GetPrevious());

                case Symbols.Null:
                    do
                    {
                        current = GetNext();
                    }
                    while (current == Symbols.Null);

                    return Data(current);

                default:
                    if (current.IsNameStart())
                    {
                        return IdentStart(current);
                    }

                    return NewDelimiter(current);
            }
        }

        private CssToken FoundU(Char previous)
        {
            var current = GetNext();

            if (current == Symbols.Plus)
            {
                var next = GetNext();

                if (next.IsHex() || next == Symbols.QuestionMark)
                {
                    StringBuffer.Append(previous).Append(current);
                    return UnicodeRange(next);
                }

                GetPrevious();
            }

            return IdentStart(GetPrevious());
        }

        /// <summary>
        /// 4.4.2. Double quoted string state
        /// </summary>
        private CssToken StringDQ()
        {
            while (true)
            {
                var current = GetNext();

                switch (current)
                {
                    case Symbols.DoubleQuote:
                    case Symbols.EndOfFile:
                        return NewString(FlushBuffer());

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        Back();
                        return NewString(FlushBuffer(), bad: true);

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                        {
                            StringBuffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(ConsumeEscape(current));
                        }
                        else
                        {
                            RaiseErrorOccurred(CssParseError.EOF);
                            Back();
                            return NewString(FlushBuffer(), bad: true);
                        }

                        break;

                    default:
                        StringBuffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.3. Single quoted string state
        /// </summary>
        private CssToken StringSQ()
        {
            while (true)
            {
                var current = GetNext();

                switch (current)
                {
                    case Symbols.SingleQuote:
                        return NewString(FlushBuffer());

                    case Symbols.EndOfFile:
                        return NewString(FlushBuffer(), bad: true);

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                        Back();
                        return NewString(FlushBuffer(), bad: true);

                    case Symbols.ReverseSolidus:
                        current = GetNext();

                        if (current.IsLineBreak())
                        {
                            StringBuffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            StringBuffer.Append(ConsumeEscape(current));
                        }
                        else
                        {
                            RaiseErrorOccurred(CssParseError.EOF);
                            Back();
                            return NewString(FlushBuffer(), bad: true);
                        }

                        break;

                    default:
                        StringBuffer.Append(current);
                        break;
                }
            }
        }

        /// <summary>
        /// 4.4.4. Hash state
        /// </summary>
        private CssToken HashStart()
        {
            var current = GetNext();

            if (current.IsNameStart())
            {
                StringBuffer.Append(Symbols.Num).Append(current);
                return HashRest();
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                StringBuffer.Append(Symbols.Num).Append(ConsumeEscape(current));
                return HashRest();
            }
            else if (current == Symbols.ReverseSolidus)
            {
                RaiseErrorOccurred(CssParseError.InvalidCharacter);
                Back();
                return NewDelimiter(Symbols.Num);
            }
            else
            {
                Back();
                return NewDelimiter(Symbols.Num);
            }
        }

        /// <summary>
        /// 4.4.5. Hash-rest state
        /// </summary>
        private CssToken HashRest()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsName())
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Symbols.ReverseSolidus)
                {
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    Back();
                    return NewHash(FlushBuffer());
                }
                else
                {
                    Back();
                    return NewHash(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.6. Comment state
        /// </summary>
        private CssToken Comment()
        {
            var current = GetNext();
            StringBuffer.Append(Symbols.Solidus).Append(Symbols.Asterisk);

            while (current != Symbols.EndOfFile)
            {
                StringBuffer.Append(current);

                if (current == Symbols.Asterisk)
                {
                    current = GetNext();
                    StringBuffer.Append(current);

                    if (current == Symbols.Solidus)
                    {
                        return NewComment(FlushBuffer());
                    }
                }
                else
                {
                    current = GetNext();
                }
            }

            RaiseErrorOccurred(CssParseError.EOF);
            return NewComment(FlushBuffer(), bad: true);
        }

        /// <summary>
        /// 4.4.7. At-keyword state
        /// </summary>
        private CssToken AtKeywordStart()
        {
            var current = GetNext();

            if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    StringBuffer.Append(Symbols.At).Append(Symbols.Minus);
                    return AtKeywordRest(current);
                }

                Back(2);
                return NewDelimiter(Symbols.At);
            }
            else if (current.IsNameStart())
            {
                StringBuffer.Append(Symbols.At).Append(current);
                return AtKeywordRest(GetNext());
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                StringBuffer.Append(Symbols.At).Append(ConsumeEscape(current));
                return AtKeywordRest(GetNext());
            }
            else
            {
                Back();
                return NewDelimiter(Symbols.At);
            }
        }

        /// <summary>
        /// 4.4.8. At-keyword-rest state
        /// </summary>
        private CssToken AtKeywordRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    Back();
                    return NewAtKeyword(FlushBuffer());
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.9. Ident state
        /// </summary>
        private CssToken IdentStart(Char current)
        {
            if (current == Symbols.Minus)
            {
                current = GetNext();

                if (current.IsNameStart() || IsValidEscape(current))
                {
                    StringBuffer.Append(Symbols.Minus);
                    return IdentRest(current);
                }

                Back();
                return NewDelimiter(Symbols.Minus);
            }
            else if (current.IsNameStart())
            {
                StringBuffer.Append(current);
                return IdentRest(GetNext());
            }
            else if (current == Symbols.ReverseSolidus && IsValidEscape(current))
            {
                current = GetNext();
                StringBuffer.Append(ConsumeEscape(current));
                return IdentRest(GetNext());
            }

            return Data(current);
        }

        /// <summary>
        /// 4.4.10. Ident-rest state
        /// </summary>
        private CssToken IdentRest(Char current)
        {
            while (true)
            {
                if (current.IsName())
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else if (current == Symbols.RoundBracketOpen)
                {
                    var name = FlushBuffer();

                    if (name.Isi(FunctionNames.Url))
                    {
                        return UrlStart();
                    }
                    
                    return NewFunction(name);
                }
                else
                {
                    Back();
                    return NewIdent(FlushBuffer());
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.11. Transform-function-whitespace state
        /// </summary>
        private CssToken TransformFunctionWhitespace(Char current)
        {
            while (true)
            {
                current = GetNext();

                if (current == Symbols.RoundBracketOpen)
                {
                    Back();
                    return NewFunction(FlushBuffer());
                }
                else if (!current.IsSpaceCharacter())
                {
                    Back(2);
                    return NewIdent(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.12. Number state
        /// </summary>
        private CssToken NumberStart(Char current)
        {
            while (true)
            {
                if (current is Symbols.Plus or Symbols.Minus) 
                {
                    StringBuffer.Append(current);
                    current = GetNext();

                    if (current == Symbols.Dot)
                    {
                        StringBuffer.Append(current);
                        StringBuffer.Append(GetNext());
                        return NumberFraction();
                    }

                    StringBuffer.Append(current);
                    return NumberRest();
                }
                else if (current == Symbols.Dot)
                {
                    StringBuffer.Append(current);
                    StringBuffer.Append(GetNext());
                    return NumberFraction();
                }
                else if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                    return NumberRest();
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.13. Number-rest state
        /// </summary>
        private CssToken NumberRest()
        {
            var current = GetNext();

            while (true)
            {
                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else if (current.IsNameStart())
                {
                    StringBuffer.Append(current);
                    return Dimension();
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                    return Dimension();
                }
                else
                {
                    break;
                }

                current = GetNext();
            }

            switch (current)
            {
                case Symbols.Dot:
                    current = GetNext();

                    if (current.IsDigit())
                    {
                        StringBuffer.Append(Symbols.Dot).Append(current);
                        return NumberFraction();
                    }

                    Back();
                    return NewNumber(FlushBuffer());

                case Symbols.Percent:
                    StringBuffer.Append(Symbols.Percent);
                    return NewPercentage(FlushBuffer());

                case 'e':
                case 'E':
                    StringBuffer.Append(current);
                    return NumberExponential();

                case Symbols.Minus:
                    return NumberDash();

                default:
                    Back();
                    return NewNumber(FlushBuffer());
            }
        }

        /// <summary>
        /// 4.4.14. Number-fraction state
        /// </summary>
        private CssToken NumberFraction()
        {
            var current = GetNext();

            while (true)
            {
                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else if (current.IsNameStart())
                {
                    StringBuffer.Append(current);
                    return Dimension();
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                    return Dimension();
                }
                else
                {
                    break;
                }

                current = GetNext();
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    StringBuffer.Append(current);
                    return NumberExponential();

                case Symbols.Percent:
                    StringBuffer.Append(current);
                    return NewPercentage(FlushBuffer());

                case Symbols.Minus:
                    return NumberDash();

                default:
                    Back();
                    return NewNumber(FlushBuffer());
            }
        }

        /// <summary>
        /// 4.4.15. Dimension state
        /// </summary>
        private CssToken Dimension()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsLetter())
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    Back();
                    return NewDimension(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.16. SciNotation state
        /// </summary>
        private CssToken SciNotation()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else
                {
                    Back();
                    return NewNumber(FlushBuffer());
                }
            }
        }

        /// <summary>
        /// 4.4.17. URL state
        /// </summary>
        private CssToken UrlStart()
        {
            var current = SkipSpaces();

            switch (current)
            {
                case Symbols.EndOfFile:
                    RaiseErrorOccurred(CssParseError.EOF);
                    return NewUrl(String.Empty, bad: true);

                case Symbols.DoubleQuote:
                    return UrlDQ();

                case Symbols.SingleQuote:
                    return UrlSQ();

                case Symbols.RoundBracketClose:
                    return NewUrl(String.Empty, bad: false);

                default:
                    return UrlUQ(current);
            }
        }

        /// <summary>
        /// 4.4.18. URL-double-quoted state
        /// </summary>
        private CssToken UrlDQ()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsLineBreak())
                {
                    RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                    return UrlBad();
                }
                else if (Symbols.EndOfFile == current)
                {
                    return NewUrl(FlushBuffer());
                }
                else if (current == Symbols.DoubleQuote)
                {
                    return UrlEnd();
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    StringBuffer.Append(current);
                }
                else
                {
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        RaiseErrorOccurred(CssParseError.EOF);
                        return NewUrl(FlushBuffer(), bad: true);
                    }
                    else if (current.IsLineBreak())
                    {
                        StringBuffer.AppendLine();
                    }
                    else
                    {
                        StringBuffer.Append(ConsumeEscape(current));
                    }
                }
            }
        }

        /// <summary>
        /// 4.4.19. URL-single-quoted state
        /// </summary>
        private CssToken UrlSQ()
        {
            while (true)
            {
                var current = GetNext();

                if (current.IsLineBreak())
                {
                    RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
                    return UrlBad();
                }
                else if (current == Symbols.EndOfFile)
                {
                    return NewUrl(FlushBuffer());
                }
                else if (current == Symbols.SingleQuote)
                {
                    return UrlEnd();
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    StringBuffer.Append(current);
                }
                else
                {
                    current = GetNext();

                    if (current == Symbols.EndOfFile)
                    {
                        Back(2);
                        RaiseErrorOccurred(CssParseError.EOF);
                        return NewUrl(FlushBuffer(), bad: true);
                    }
                    else if (current.IsLineBreak())
                    {
                        StringBuffer.AppendLine();
                    }
                    else
                    {
                        StringBuffer.Append(ConsumeEscape(current));
                    }
                }
            }
        }

        /// <summary>
        /// 4.4.21. URL-unquoted state
        /// </summary>
        private CssToken UrlUQ(Char current)
        {
            while (true)
            {
                if (current.IsSpaceCharacter())
                {
                    StringBuffer.Append(current);
                    return UrlEnd();
                }
                else if (current == Symbols.RoundBracketClose)
                {
                    return NewUrl(FlushBuffer());
                }
                else if (current == Symbols.EndOfFile)
                {
                    return NewUrl(FlushBuffer(), bad: true);
                }
                else if (current is Symbols.DoubleQuote or Symbols.SingleQuote or Symbols.RoundBracketOpen || current.IsNonPrintable())
                {
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    return UrlBad();
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    StringBuffer.Append(current);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    StringBuffer.Append(current);
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    return UrlBad();
                }

                current = GetNext();
            }
        }

        /// <summary>
        /// 4.4.20. URL-end state
        /// </summary>
        private CssToken UrlEnd()
        {
            while (true)
            {
                var current = GetNext();

                if (current == Symbols.RoundBracketClose)
                {
                    return NewUrl(FlushBuffer());
                }
                else if (!current.IsSpaceCharacter())
                {
                    RaiseErrorOccurred(CssParseError.InvalidCharacter);
                    Back();
                    return UrlBad();
                }
            }
        }

        /// <summary>
        /// 4.4.22. Bad URL state
        /// </summary>
        private CssToken UrlBad()
        {
            var current = Current;
            var curly = 0;
            var round = 1;

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Semicolon)
                {
                    Back();
                    return NewUrl(FlushBuffer(), true);
                }
                else if (current == Symbols.CurlyBracketClose && --curly == -1)
                {
                    Back();
                    return NewUrl(FlushBuffer(), true);
                }
                else if (current == Symbols.RoundBracketClose && --round == 0)
                {
                    StringBuffer.Append(current);
                    return NewUrl(FlushBuffer(), true);
                }
                else if (IsValidEscape(current))
                {
                    current = GetNext();
                    StringBuffer.Append(ConsumeEscape(current));
                }
                else
                {
                    if (current == Symbols.RoundBracketOpen)
                    {
                        ++round;
                    }
                    else if (curly == Symbols.CurlyBracketOpen)
                    {
                        ++curly;
                    }

                    StringBuffer.Append(current);
                }

                current = GetNext();
            }

            RaiseErrorOccurred(CssParseError.EOF);
            return NewUrl(FlushBuffer(), bad: true);
        }

        /// <summary>
        /// 4.4.23. Unicode-range State
        /// </summary>
        private CssToken UnicodeRange(Char current)
        {
            for (var i = 0; i < 6 && current.IsHex(); i++)
            {
                StringBuffer.Append(current);
                current = GetNext();
            }

            if (StringBuffer.Length != 6)
            {
                for (var i = 0; i < 6 - StringBuffer.Length; i++)
                {
                    if (current != Symbols.QuestionMark)
                    {
                        current = GetPrevious();
                        break;
                    }

                    StringBuffer.Append(current);
                    current = GetNext();
                }
            }
            else if (current == Symbols.Minus)
            {
                var next = GetNext();

                if (next.IsHex())
                {
                    StringBuffer.Append(current);

                    for (var i = 0; i < 6; i++)
                    {
                        if (!next.IsHex())
                        {
                            next = GetPrevious();
                            break;
                        }

                        StringBuffer.Append(next);
                        next = GetNext();
                    }
                }
                else
                {
                    Back(2);
                }
            }
            else
            {
                Back();
            }

            return NewRange(FlushBuffer());
        }

        #endregion

        #region Tokens

        private CssToken NewMatch(String data)
        {
            return new CssToken(CssTokenType.Match, data) { Position = _position };
        }

        private CssToken NewColumn()
        {
            return new CssToken(CssTokenType.Column, CombinatorSymbols.Column) { Position = _position };
        }

        private CssToken NewCloseCurly()
        {
            return new CssToken(CssTokenType.CurlyBracketClose, "}") { Position = _position };
        }

        private CssToken NewOpenCurly()
        {
            return new CssToken(CssTokenType.CurlyBracketOpen, "{") { Position = _position };
        }

        private CssToken NewCloseSquare()
        {
            return new CssToken(CssTokenType.SquareBracketClose, "]") { Position = _position };
        }

        private CssToken NewOpenSquare()
        {
            return new CssToken(CssTokenType.SquareBracketOpen, "[") { Position = _position };
        }

        private CssToken NewOpenComment()
        {
            return new CssToken(CssTokenType.Cdo, "<!--") { Position = _position };
        }

        private CssToken NewSemicolon()
        {
            return new CssToken(CssTokenType.Semicolon, ";") { Position = _position };
        }

        private CssToken NewColon()
        {
            return new CssToken(CssTokenType.Colon, ":") { Position = _position };
        }

        private CssToken NewCloseComment()
        {
            return new CssToken(CssTokenType.Cdc, "-->") { Position = _position };
        }

        private CssToken NewComma()
        {
            return new CssToken(CssTokenType.Comma, ",") { Position = _position };
        }

        private CssToken NewCloseRound()
        {
            return new CssToken(CssTokenType.RoundBracketClose, ")") { Position = _position };
        }

        private CssToken NewOpenRound()
        {
            return new CssToken(CssTokenType.RoundBracketOpen, "(") { Position = _position };
        }

        private CssToken NewString(String value, Boolean bad = false)
        {
            return new CssStringToken(value, bad) { Position = _position };
        }

        private CssToken NewHash(String data)
        {
            return new CssToken(CssTokenType.Hash, data) { Position = _position };
        }

        private CssToken NewComment(String data, Boolean bad = false)
        {
            return new CssCommentToken(data, bad) { Position = _position };
        }

        private CssToken NewAtKeyword(String data)
        {
            return new CssToken(CssTokenType.AtKeyword, data) { Position = _position };
        }

        private CssToken NewIdent(String data)
        {
            return new CssToken(CssTokenType.Ident, data) { Position = _position };
        }

        private CssToken NewFunction(String data)
        {
            return new CssToken(CssTokenType.Function, data) { Position = _position };
        }

        private CssToken NewPercentage(String data)
        {
            return new CssToken(CssTokenType.Percentage, data) { Position = _position };
        }

        private CssToken NewDimension(String data)
        {
            return new CssToken(CssTokenType.Dimension, data) { Position = _position };
        }

        private CssToken NewUrl(String data, Boolean bad = false)
        {
            return new CssUrlToken(data, bad) { Position = _position };
        }

        private CssToken NewRange(String data)
        {
            return new CssToken(CssTokenType.Range, data) { Position = _position };
        }

        private CssToken NewWhitespace(Char c)
        {
            return new CssToken(CssTokenType.Whitespace, c.ToString()) { Position = _position };
        }

        private CssToken NewNumber(String data)
        {
            return new CssToken(CssTokenType.Number, data) { Position = _position };
        }

        private CssToken NewDelimiter(Char c)
        {
            return new CssToken(CssTokenType.Delim, c.ToString()) { Position = _position };
        }

        private CssToken NewEof()
        {
            return new CssToken(CssTokenType.EndOfFile, String.Empty) { Position = _position };
        }

        #endregion

        #region Helpers

        private CssToken NumberExponential()
        {
            var current = GetNext();

            if (current.IsDigit())
            {
                StringBuffer.Append(current);
                return SciNotation();
            }
            else if (current == Symbols.Plus || current == Symbols.Minus)
            {
                var op = current;
                current = GetNext();

                if (current.IsDigit())
                {
                    StringBuffer.Append(op).Append(current);
                    return SciNotation();
                }

                Back();
            }
            
            Back();
            return Dimension();
        }

        private CssToken NumberDash()
        {
            var current = GetNext();

            if (current.IsNameStart())
            {
                StringBuffer.Append(Symbols.Minus).Append(current);
                return Dimension();
            }
            else if (IsValidEscape(current))
            {
                current = GetNext();
                StringBuffer.Append(Symbols.Minus).Append(ConsumeEscape(current));
                return Dimension();
            }
            else
            {
                Back(2);
                return NewNumber(FlushBuffer());
            }
        }

        private String ConsumeEscape(Char current)
        {
            if (current.IsHex())
            {
                var isHex = true;
                var escape = new Char[6];
                var length = 0;

                while (isHex && length < escape.Length)
                {
                    escape[length++] = current;
                    current = GetNext();
                    isHex = current.IsHex();
                }

                if (!current.IsSpaceCharacter())
                {
                    Back();
                }

                var code = Int32.Parse(new String(escape, 0, length), NumberStyles.HexNumber);

                if (!code.IsInvalid())
                {
                    return Char.ConvertFromUtf32(code);
                }

                current = Symbols.Replacement;
            }

            return current.ToString();
        }

        private Boolean IsValidEscape(Char current)
        {
            if (current == Symbols.ReverseSolidus)
            {
                current = GetNext();
                Back();

                return current != Symbols.EndOfFile && !current.IsLineBreak();
            }
                
            return false;
        }

        private void RaiseErrorOccurred(CssParseError code)
        {
            RaiseErrorOccurred(code, GetCurrentPosition());
        }

        #endregion
    }
}
