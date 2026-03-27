namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssFontPropertyTests
    {
        [Test]
        public void CssFontFamilyMultipleWithIdentifiersLegal()
        {
            var snippet = "font-family: Gill Sans Extrabold, sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("Gill Sans Extrabold, sans-serif"));
        }

        [Test]
        public void CssFontFamilyInitialLegal()
        {
            var snippet = "font-family: initial ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("initial"));
        }

        [Test]
        public void CssFontFamilyMultipleDiverseLegal()
        {
            var snippet = "font-family: Courier, \"Lucida Console\", monospace ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("Courier, \"Lucida Console\", monospace"));
        }

        [Test]
        public void CssFontFamilyMultipleStringLegal()
        {
            var snippet = "font-family: \"Goudy Bookletter 1911\", sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"Goudy Bookletter 1911\", sans-serif"));
        }

        [Test]
        public void CssFontFamilyMultipleNumberIllegal()
        {
            var snippet = "font-family: Goudy Bookletter 1911, sans-serif  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontFamilyMultipleFractionIllegal()
        {
            var snippet = "font-family: Red/Black, sans-serif  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontFamilyMultipleStringMixedWithIdentifierIllegal()
        {
            var snippet = "font-family: \"Lucida\" Grande, sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontFamilyMultipleExclamationMarkIllegal()
        {
            var snippet = "font-family: Ahem!, sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontFamilyMultipleAtIllegal()
        {
            var snippet = "font-family: test@foo, sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontFamilyHashIllegal()
        {
            var snippet = "font-family: #POUND ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontFamilyDashIllegal()
        {
            var snippet = "font-family: Hawaii 5-0 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-family"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontVariantNormalUppercaseLegal()
        {
            var snippet = "font-variant : NORMAL";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-variant"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssFontVariantSmallCapsLegal()
        {
            var snippet = "font-variant : small-caps ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-variant"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("small-caps"));
        }

        [Test]
        public void CssFontVariantSmallCapsIllegal()
        {
            var snippet = "font-variant : smallCaps ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-variant"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontStyleItalicLegal()
        {
            var snippet = "font-style : italic";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("italic"));
        }

        [Test]
        public void CssFontStyleObliqueLegal()
        {
            var snippet = "font-style : oblique ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("oblique"));
        }

        [Test]
        public void CssFontStyleNormalImportantLegal()
        {
            var snippet = "font-style : normal !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-style"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssFontSizeAbsoluteImportantXxSmallLegal()
        {
            var snippet = "font-size : xx-small !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("xx-small"));
        }

        [Test]
        public void CssFontSizeAbsoluteImportantXxxLargeLegal()
        {
            var snippet = "font-size : xxx-large !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("xxx-large"));
        }

        [Test]
        public void CssFontSizeAbsoluteMediumUppercaseLegal()
        {
            var snippet = "font-size : medium";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("medium"));
        }

        [Test]
        public void CssFontSizeAbsoluteLargeImportantLegal()
        {
            var snippet = "font-size : large !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("large"));
        }

        [Test]
        public void CssFontSizeRelativeLargerLegal()
        {
            var snippet = "font-size : larger ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("larger"));
        }

        [Test]
        public void CssFontSizeRelativeLargestIllegal()
        {
            var snippet = "font-size : largest ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontSizePercentLegal()
        {
            var snippet = "font-size : 120% ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("120%"));
        }

        [Test]
        public void CssFontSizeZeroLegal()
        {
            var snippet = "font-size : 0 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssFontSizeLengthLegal()
        {
            var snippet = "font-size : 3.5em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3.5em"));
        }

        [Test]
        public void CssFontSizeNumberIllegal()
        {
            var snippet = "font-size : 120.3 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontWeightPercentllegal()
        {
            var snippet = "font-weight : 100% ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-weight"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontWeightBolderLegalImportant()
        {
            var snippet = "font-weight : bolder !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-weight"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("bolder"));
        }

        [Test]
        public void CssFontWeightBoldLegal()
        {
            var snippet = "font-weight : bold";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-weight"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("bold"));
        }

        [Test]
        public void CssFontWeight400Legal()
        {
            var snippet = "font-weight : 400 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-weight"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("400"));
        }

        [Test]
        public void CssFontStretchNormalUppercaseImportantLegal()
        {
            var snippet = "font-stretch : NORMAL !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-stretch"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssFontStretchExtraCondensedLegal()
        {
            var snippet = "font-stretch : extra-condensed ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-stretch"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("extra-condensed"));
        }

        [Test]
        public void CssFontStretchSemiExpandedSpaceBetweenIllegal()
        {
            var snippet = "font-stretch : semi expanded ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-stretch"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontShorthandWithFractionLegal()
        {
            var snippet = "font : 12px/14px sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("12px / 14px sans-serif"));
        }

        [Test]
        public void CssFontShorthandPercentLegal()
        {
            var snippet = "font : 80% sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("80% sans-serif"));
        }

        [Test]
        public void CssFontShorthandBoldItalicLargeLegal()
        {
            var snippet = "font : bold italic large serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("italic bold 1.2em serif"));
        }

        [Test]
        public void CssFontShorthandPredefinedLegal()
        {
            var snippet = "font : status-bar ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("status-bar"));
        }

        [Test]
        public void CssFontShorthandSizeAndFontListLegal()
        {
            var snippet = "font : 15px arial,sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15px arial, sans-serif"));
        }

        [Test]
        public void CssFontShorthandStyleWeightSizeLineHeightAndFontListLegal()
        {
            var snippet = "font : italic bold 12px/30px Georgia, serif";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("italic bold 12px / 30px Georgia, serif"));
        }

        [Test]
        public void CssLetterSpacingLengthPxLegal()
        {
            var snippet = "letter-spacing: 3px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("letter-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3px"));
        }

        [Test]
        public void CssLetterSpacingLengthFloatPxLegal()
        {
            var snippet = "letter-spacing: .3px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("letter-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.3px"));
        }

        [Test]
        public void CssLetterSpacingLengthFloatEmLegal()
        {
            var snippet = "letter-spacing: 0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("letter-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.3em"));
        }

        [Test]
        public void CssLetterSpacingNormalLegal()
        {
            var snippet = "letter-spacing: normal ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("letter-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssFontSizeAdjustNoneLegal()
        {
            var snippet = "font-size-adjust : NONE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size-adjust"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssFontSizeAdjustNumberLegal()
        {
            var snippet = "font-size-adjust : 0.5";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size-adjust"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.5"));
        }

        [Test]
        public void CssFontSizeAdjustLengthIllegal()
        {
            var snippet = "font-size-adjust : 1.1em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font-size-adjust"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssFontSizeHeightFamilyLegal()
        {
            var snippet = "font: 12pt/14pt sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("12pt / 14pt sans-serif"));
        }

        [Test]
        public void CssFontSizeFamilyLegal()
        {
            var snippet = "font: 80% sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("80% sans-serif"));
        }

        [Test]
        public void CssFontSizeHeightMultipleFamiliesLegal()
        {
            var snippet = "font: x-large/110% 'New Century Schoolbook', serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1.5em / 110% \"New Century Schoolbook\", serif"));
        }

        [Test]
        public void CssFontWeightVariantSizeFamiliesLegal()
        {
            var snippet = "font: bold italic large Palatino, serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("italic bold 1.2em Palatino, serif"));
        }

        [Test]
        public void CssFontStyleVariantSizeHeightFamilyLegal()
        {
            var snippet = "font: normal small-caps 120%/120% Fantasy ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal small-caps 120% / 120% Fantasy"));
        }

        [Test]
        public void CssFontStyleVariantSizeFamiliesLegal()
        {
            var snippet = "font: condensed oblique 12pt \"Helvetica Neue\", serif ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("oblique condensed 12pt \"Helvetica Neue\", serif"));
        }

        [Test]
        public void CssFontSystemFamilyLegal()
        {
            var snippet = "font: status-bar ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("status-bar"));
        }

        [Test]
        public void CssFontFaceWithThreeRulesShouldSerializeCorrectly()
        {
            var snippet = @"@font-face {
        font-family: FrutigerLTStd;
            src: url(""https://example.com/FrutigerLTStd-Light.otf"") format(""opentype"");
           font-weight: bold;
    }";
            var rule = ParseRule(snippet);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.FontFace));
            Assert.That(rule.ToCss(), Is.EqualTo("@font-face { font-family: FrutigerLTStd; src: url(\"https://example.com/FrutigerLTStd-Light.otf\") format(\"opentype\"); font-weight: bold }"));
        }

        [Test]
        public void CssFontFaceWithTwoRulesShouldSerializeCorrectly()
        {
            var snippet = @"@font-face {
        font-family: FrutigerLTStd;
            src: url(""https://example.com/FrutigerLTStd-Light.otf"") format(""opentype"");
    }";
            var rule = ParseRule(snippet);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.FontFace));
            Assert.That(rule.ToCss(), Is.EqualTo("@font-face { font-family: FrutigerLTStd; src: url(\"https://example.com/FrutigerLTStd-Light.otf\") format(\"opentype\") }"));
        }

        [Test]
        public void CssFontFaceWithOneRuleShouldSerializeCorrectly()
        {
            var snippet = @"@font-face {
        font-family: FrutigerLTStd;
    }";
            var rule = ParseRule(snippet);
            Assert.That(rule.Type, Is.EqualTo(CssRuleType.FontFace));
            Assert.That(rule.ToCss(), Is.EqualTo("@font-face { font-family: FrutigerLTStd }"));
        }

        [Test]
        public void CssFontStyleWeightSizeHeightFamiliesLegal()
        {
            var snippet = "font: italic bold 12px/30px Georgia, serif";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("italic bold 12px / 30px Georgia, serif"));
        }

        [Test]
        public void CssFontStyleNumericWeightSizeFamiliesLegal()
        {
            var snippet = "font: 400 12px Georgia, serif";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("font"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("400 12px Georgia, serif"));
        }

        [Test]
        public void LongDataUrisShouldNotBeDisappearing_Issue76()
        {
            var url = "data-uri.txt".LoadFromResources();
            var html = $@"<style>@font-face {{
font-family: ""MyFont"";
src: url(""{url}"") format('woff');
font-weight: normal;
font-style: normal;
font-display: swap;
}}</style>";

            var parser = new HtmlParser(new HtmlParserOptions(), BrowsingContext.New(Configuration.Default.WithCss(new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidSelectors = true,
            })));

            var dom = parser.ParseDocument(html);
            var fontFace = ((ICssStyleSheet)dom.StyleSheets[0]).Rules[0] as ICssFontFaceRule;
            Assert.IsNotEmpty(fontFace.Source);
        }
    }
}
