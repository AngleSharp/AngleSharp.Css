namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
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
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("Gill Sans Extrabold, sans-serif", property.Value);
        }

        [Test]
        public void CssFontFamilyInitialLegal()
        {
            var snippet = "font-family: initial ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("initial", property.Value);
        }

        [Test]
        public void CssFontFamilyMultipleDiverseLegal()
        {
            var snippet = "font-family: Courier, \"Lucida Console\", monospace ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("Courier, \"Lucida Console\", monospace", property.Value);
        }

        [Test]
        public void CssFontFamilyMultipleStringLegal()
        {
            var snippet = "font-family: \"Goudy Bookletter 1911\", sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("\"Goudy Bookletter 1911\", sans-serif", property.Value);
        }

        [Test]
        public void CssFontFamilyMultipleNumberIllegal()
        {
            var snippet = "font-family: Goudy Bookletter 1911, sans-serif  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontFamilyMultipleFractionIllegal()
        {
            var snippet = "font-family: Red/Black, sans-serif  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontFamilyMultipleStringMixedWithIdentifierIllegal()
        {
            var snippet = "font-family: \"Lucida\" Grande, sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontFamilyMultipleExclamationMarkIllegal()
        {
            var snippet = "font-family: Ahem!, sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontFamilyMultipleAtIllegal()
        {
            var snippet = "font-family: test@foo, sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontFamilyHashIllegal()
        {
            var snippet = "font-family: #POUND ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontFamilyDashIllegal()
        {
            var snippet = "font-family: Hawaii 5-0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-family", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontVariantNormalUppercaseLegal()
        {
            var snippet = "font-variant : NORMAL";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-variant", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssFontVariantSmallCapsLegal()
        {
            var snippet = "font-variant : small-caps ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-variant", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("small-caps", property.Value);
        }

        [Test]
        public void CssFontVariantSmallCapsIllegal()
        {
            var snippet = "font-variant : smallCaps ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-variant", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontStyleItalicLegal()
        {
            var snippet = "font-style : italic";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("italic", property.Value);
        }

        [Test]
        public void CssFontStyleObliqueLegal()
        {
            var snippet = "font-style : oblique ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("oblique", property.Value);
        }

        [Test]
        public void CssFontStyleNormalImportantLegal()
        {
            var snippet = "font-style : normal !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-style", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssFontSizeAbsoluteImportantXxSmallLegal()
        {
            var snippet = "font-size : xx-small !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("xx-small", property.Value);
        }

        [Test]
        public void CssFontSizeAbsoluteMediumUppercaseLegal()
        {
            var snippet = "font-size : medium";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("medium", property.Value);
        }

        [Test]
        public void CssFontSizeAbsoluteLargeImportantLegal()
        {
            var snippet = "font-size : large !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("large", property.Value);
        }

        [Test]
        public void CssFontSizeRelativeLargerLegal()
        {
            var snippet = "font-size : larger ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("larger", property.Value);
        }

        [Test]
        public void CssFontSizeRelativeLargestIllegal()
        {
            var snippet = "font-size : largest ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontSizePercentLegal()
        {
            var snippet = "font-size : 120% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("120%", property.Value);
        }

        [Test]
        public void CssFontSizeZeroLegal()
        {
            var snippet = "font-size : 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssFontSizeLengthLegal()
        {
            var snippet = "font-size : 3.5em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3.5em", property.Value);
        }

        [Test]
        public void CssFontSizeNumberIllegal()
        {
            var snippet = "font-size : 120.3 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontWeightPercentllegal()
        {
            var snippet = "font-weight : 100% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-weight", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontWeightBolderLegalImportant()
        {
            var snippet = "font-weight : bolder !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-weight", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("bolder", property.Value);
        }

        [Test]
        public void CssFontWeightBoldLegal()
        {
            var snippet = "font-weight : bold";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-weight", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("bold", property.Value);
        }

        [Test]
        public void CssFontWeight400Legal()
        {
            var snippet = "font-weight : 400 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-weight", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("400", property.Value);
        }

        [Test]
        public void CssFontStretchNormalUppercaseImportantLegal()
        {
            var snippet = "font-stretch : NORMAL !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-stretch", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssFontStretchExtraCondensedLegal()
        {
            var snippet = "font-stretch : extra-condensed ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-stretch", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("extra-condensed", property.Value);
        }

        [Test]
        public void CssFontStretchSemiExpandedSpaceBetweenIllegal()
        {
            var snippet = "font-stretch : semi expanded ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-stretch", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontShorthandWithFractionLegal()
        {
            var snippet = "font : 12px/14px sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("12px / 14px sans-serif", property.Value);
        }

        [Test]
        public void CssFontShorthandPercentLegal()
        {
            var snippet = "font : 80% sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("smaller sans-serif", property.Value);
        }

        [Test]
        public void CssFontShorthandBoldItalicLargeLegal()
        {
            var snippet = "font : bold italic large serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("italic bold large serif", property.Value);
        }

        [Test]
        public void CssFontShorthandPredefinedLegal()
        {
            var snippet = "font : status-bar ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("status-bar", property.Value);
        }

        [Test]
        public void CssFontShorthandSizeAndFontListLegal()
        {
            var snippet = "font : 15px arial,sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15px arial, sans-serif", property.Value);
        }

        [Test]
        public void CssFontShorthandStyleWeightSizeLineHeightAndFontListLegal()
        {
            var snippet = "font : italic bold 12px/30px Georgia, serif";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("italic bold 12px / 30px Georgia, serif", property.Value);
        }

        [Test]
        public void CssLetterSpacingLengthPxLegal()
        {
            var snippet = "letter-spacing: 3px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("letter-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3px", property.Value);
        }

        [Test]
        public void CssLetterSpacingLengthFloatPxLegal()
        {
            var snippet = "letter-spacing: .3px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("letter-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.3px", property.Value);
        }

        [Test]
        public void CssLetterSpacingLengthFloatEmLegal()
        {
            var snippet = "letter-spacing: 0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("letter-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.3em", property.Value);
        }

        [Test]
        public void CssLetterSpacingNormalLegal()
        {
            var snippet = "letter-spacing: normal ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("letter-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssFontSizeAdjustNoneLegal()
        {
            var snippet = "font-size-adjust : NONE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size-adjust", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssFontSizeAdjustNumberLegal()
        {
            var snippet = "font-size-adjust : 0.5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size-adjust", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.5", property.Value);
        }

        [Test]
        public void CssFontSizeAdjustLengthIllegal()
        {
            var snippet = "font-size-adjust : 1.1em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font-size-adjust", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssFontSizeHeightFamilyLegal()
        {
            var snippet = "font: 12pt/14pt sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("12pt / 14pt sans-serif", property.Value);
        }

        [Test]
        public void CssFontSizeFamilyLegal()
        {
            var snippet = "font: 80% sans-serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("smaller sans-serif", property.Value);
        }

        [Test]
        public void CssFontSizeHeightMultipleFamiliesLegal()
        {
            var snippet = "font: x-large/110% 'New Century Schoolbook', serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("x-large / 110% \"New Century Schoolbook\", serif", property.Value);
        }

        [Test]
        public void CssFontWeightVariantSizeFamiliesLegal()
        {
            var snippet = "font: bold italic large Palatino, serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("italic bold large Palatino, serif", property.Value);
        }

        [Test]
        public void CssFontStyleVariantSizeHeightFamilyLegal()
        {
            var snippet = "font: normal small-caps 120%/120% Fantasy ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal small-caps larger / 120% Fantasy", property.Value);
        }

        [Test]
        public void CssFontStyleVariantSizeFamiliesLegal()
        {
            var snippet = "font: condensed oblique 12pt \"Helvetica Neue\", serif ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("oblique condensed 12pt \"Helvetica Neue\", serif", property.Value);
        }

        [Test]
        public void CssFontSystemFamilyLegal()
        {
            var snippet = "font: status-bar ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("status-bar", property.Value);
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
            Assert.AreEqual(CssRuleType.FontFace, rule.Type);
            Assert.AreEqual("@font-face { font-family: FrutigerLTStd; src: url(\"https://example.com/FrutigerLTStd-Light.otf\") format(\"opentype\"); font-weight: bold }", rule.ToCss());
        }

        [Test]
        public void CssFontFaceWithTwoRulesShouldSerializeCorrectly()
        {
            var snippet = @"@font-face {
        font-family: FrutigerLTStd;
            src: url(""https://example.com/FrutigerLTStd-Light.otf"") format(""opentype"");
    }";
            var rule = ParseRule(snippet);
            Assert.AreEqual(CssRuleType.FontFace, rule.Type);
            Assert.AreEqual("@font-face { font-family: FrutigerLTStd; src: url(\"https://example.com/FrutigerLTStd-Light.otf\") format(\"opentype\") }", rule.ToCss());
        }

        [Test]
        public void CssFontFaceWithOneRuleShouldSerializeCorrectly()
        {
            var snippet = @"@font-face {
        font-family: FrutigerLTStd;
    }";
            var rule = ParseRule(snippet);
            Assert.AreEqual(CssRuleType.FontFace, rule.Type);
            Assert.AreEqual("@font-face { font-family: FrutigerLTStd }", rule.ToCss());
        }

        [Test]
        public void CssFontStyleWeightSizeHeightFamiliesLegal()
        {
            var snippet = "font: italic bold 12px/30px Georgia, serif";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("font", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("italic bold 12px / 30px Georgia, serif", property.Value);
        }
    }
}
