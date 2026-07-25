namespace AngleSharp.Css.Tests.Styling
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPhase12TextPropertiesTests
    {
        [Test]
        public void CssTextUnderlineOffsetInitial()
        {
            var snippet = "text-underline-offset: 0;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-underline-offset", property.Name);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssTextUnderlineOffsetLength()
        {
            var snippet = "text-underline-offset: 2px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-underline-offset", property.Name);
            Assert.AreEqual("2px", property.Value);
        }

        [Test]
        public void CssTextUnderlineOffsetPercentage()
        {
            var snippet = "text-underline-offset: 25%;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-underline-offset", property.Name);
            Assert.AreEqual("25%", property.Value);
        }

        [Test]
        public void CssTextUnderlineOffsetInvalid()
        {
            var snippet = "text-underline-offset: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-underline-offset", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssTextDecorationThicknessAuto()
        {
            var snippet = "text-decoration-thickness: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-thickness", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssTextDecorationThicknessFromFont()
        {
            var snippet = "text-decoration-thickness: from-font;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-thickness", property.Name);
            Assert.AreEqual("from-font", property.Value);
        }

        [Test]
        public void CssTextDecorationThicknessLength()
        {
            var snippet = "text-decoration-thickness: 1.5px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-thickness", property.Name);
            Assert.AreEqual("1.5px", property.Value);
        }

        [Test]
        public void CssTextDecorationThicknessPercentage()
        {
            var snippet = "text-decoration-thickness: 10%;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-thickness", property.Name);
            Assert.AreEqual("10%", property.Value);
        }

        [Test]
        public void CssTextDecorationThicknessInvalid()
        {
            var snippet = "text-decoration-thickness: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-thickness", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssTextDecorationSkipInkAuto()
        {
            var snippet = "text-decoration-skip-ink: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-skip-ink", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssTextDecorationSkipInkAll()
        {
            var snippet = "text-decoration-skip-ink: all;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-skip-ink", property.Name);
            Assert.AreEqual("all", property.Value);
        }

        [Test]
        public void CssTextDecorationSkipInkNone()
        {
            var snippet = "text-decoration-skip-ink: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-skip-ink", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssTextDecorationSkipInkInvalid()
        {
            var snippet = "text-decoration-skip-ink: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-skip-ink", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssTextWrapWrap()
        {
            var snippet = "text-wrap: wrap;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap", property.Name);
            Assert.AreEqual("wrap", property.Value);
        }

        [Test]
        public void CssTextWrapNowrap()
        {
            var snippet = "text-wrap: nowrap;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap", property.Name);
            Assert.AreEqual("nowrap", property.Value);
        }

        [Test]
        public void CssTextWrapBalance()
        {
            var snippet = "text-wrap: balance;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap", property.Name);
            Assert.AreEqual("balance", property.Value);
        }

        [Test]
        public void CssTextWrapStable()
        {
            var snippet = "text-wrap: stable;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap", property.Name);
            Assert.AreEqual("stable", property.Value);
        }

        [Test]
        public void CssTextWrapPretty()
        {
            var snippet = "text-wrap: pretty;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap", property.Name);
            Assert.AreEqual("pretty", property.Value);
        }

        [Test]
        public void CssTextWrapInvalid()
        {
            var snippet = "text-wrap: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssTextWrapModeWrap()
        {
            var snippet = "text-wrap-mode: wrap;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-mode", property.Name);
            Assert.AreEqual("wrap", property.Value);
        }

        [Test]
        public void CssTextWrapModeNowrap()
        {
            var snippet = "text-wrap-mode: nowrap;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-mode", property.Name);
            Assert.AreEqual("nowrap", property.Value);
        }

        [Test]
        public void CssTextWrapModeInvalid()
        {
            var snippet = "text-wrap-mode: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-mode", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssTextWrapStyleAuto()
        {
            var snippet = "text-wrap-style: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-style", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssTextWrapStyleStable()
        {
            var snippet = "text-wrap-style: stable;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-style", property.Name);
            Assert.AreEqual("stable", property.Value);
        }

        [Test]
        public void CssTextWrapStyleBalance()
        {
            var snippet = "text-wrap-style: balance;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-style", property.Name);
            Assert.AreEqual("balance", property.Value);
        }

        [Test]
        public void CssTextWrapStylePretty()
        {
            var snippet = "text-wrap-style: pretty;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-style", property.Name);
            Assert.AreEqual("pretty", property.Value);
        }

        [Test]
        public void CssTextWrapStyleInvalid()
        {
            var snippet = "text-wrap-style: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-wrap-style", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssWhiteSpaceCollapseCollapse()
        {
            var snippet = "white-space-collapse: collapse;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("white-space-collapse", property.Name);
            Assert.AreEqual("collapse", property.Value);
        }

        [Test]
        public void CssWhiteSpaceCollapsePreserve()
        {
            var snippet = "white-space-collapse: preserve;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("white-space-collapse", property.Name);
            Assert.AreEqual("preserve", property.Value);
        }

        [Test]
        public void CssWhiteSpaceCollapsePreserveBreaks()
        {
            var snippet = "white-space-collapse: preserve-breaks;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("white-space-collapse", property.Name);
            Assert.AreEqual("preserve-breaks", property.Value);
        }

        [Test]
        public void CssWhiteSpaceCollapsePreserveSpaces()
        {
            var snippet = "white-space-collapse: preserve-spaces;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("white-space-collapse", property.Name);
            Assert.AreEqual("preserve-spaces", property.Value);
        }

        [Test]
        public void CssWhiteSpaceCollapseInvalid()
        {
            var snippet = "white-space-collapse: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("white-space-collapse", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssTabSizeNumber()
        {
            var snippet = "tab-size: 4;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("tab-size", property.Name);
            Assert.AreEqual("4", property.Value);
        }

        [Test]
        public void CssTabSizeLength()
        {
            var snippet = "tab-size: 2em;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("tab-size", property.Name);
            Assert.AreEqual("2em", property.Value);
        }

        [Test]
        public void CssTabSizeInvalid()
        {
            var snippet = "tab-size: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("tab-size", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssHyphensNone()
        {
            var snippet = "hyphens: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphens", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssHyphensManual()
        {
            var snippet = "hyphens: manual;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphens", property.Name);
            Assert.AreEqual("manual", property.Value);
        }

        [Test]
        public void CssHyphensAuto()
        {
            var snippet = "hyphens: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphens", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssHyphensInvalid()
        {
            var snippet = "hyphens: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphens", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssHyphenateCharacterAuto()
        {
            var snippet = "hyphenate-character: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphenate-character", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssHyphenateCharacterCustom()
        {
            var snippet = "hyphenate-character: \"-\";";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphenate-character", property.Name);
            Assert.AreEqual("\"-\"", property.Value);
        }

        [Test]
        public void CssHyphenateCharacterInvalid()
        {
            var snippet = "hyphenate-character: 123;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphenate-character", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssHypenatateLimitCharsAuto()
        {
            var snippet = "hyphenate-limit-chars: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphenate-limit-chars", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssHypenatateLimitCharsInvalid()
        {
            var snippet = "hyphenate-limit-chars: 5 2 2;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hyphenate-limit-chars", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssLineBreakAuto()
        {
            var snippet = "line-break: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("line-break", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssLineBreakLoose()
        {
            var snippet = "line-break: loose;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("line-break", property.Name);
            Assert.AreEqual("loose", property.Value);
        }

        [Test]
        public void CssLineBreakStrict()
        {
            var snippet = "line-break: strict;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("line-break", property.Name);
            Assert.AreEqual("strict", property.Value);
        }

        [Test]
        public void CssLineBreakAnywhere()
        {
            var snippet = "line-break: anywhere;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("line-break", property.Name);
            Assert.AreEqual("anywhere", property.Value);
        }

        [Test]
        public void CssLineBreakInvalid()
        {
            var snippet = "line-break: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("line-break", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssInitialLetterNormal()
        {
            var snippet = "initial-letter: normal;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter", property.Name);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssInitialLetterNumber()
        {
            var snippet = "initial-letter: 2;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter", property.Name);
            Assert.AreEqual("2", property.Value);
        }

        [Test]
        public void CssInitialLetterFloat()
        {
            var snippet = "initial-letter: 1.5;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter", property.Name);
            Assert.AreEqual("1.5", property.Value);
        }

        [Test]
        public void CssInitialLetterInvalid()
        {
            var snippet = "initial-letter: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssInitialLetterAlignAuto()
        {
            var snippet = "initial-letter-align: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter-align", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssInitialLetterAlignAlphabetic()
        {
            var snippet = "initial-letter-align: alphabetic;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter-align", property.Name);
            Assert.AreEqual("alphabetic", property.Value);
        }

        [Test]
        public void CssInitialLetterAlignHanging()
        {
            var snippet = "initial-letter-align: hanging;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter-align", property.Name);
            Assert.AreEqual("hanging", property.Value);
        }

        [Test]
        public void CssInitialLetterAlignLeading()
        {
            var snippet = "initial-letter-align: leading;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter-align", property.Name);
            Assert.AreEqual("leading", property.Value);
        }

        [Test]
        public void CssInitialLetterAlignInvalid()
        {
            var snippet = "initial-letter-align: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("initial-letter-align", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssHangingPunctuationNone()
        {
            var snippet = "hanging-punctuation: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hanging-punctuation", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssHangingPunctuationInvalid()
        {
            var snippet = "hanging-punctuation: first;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("hanging-punctuation", property.Name);
            Assert.AreEqual("", property.Value);
        }
    }
}
