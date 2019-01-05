namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssTextPropertyTests
    {
        [Test]
        public void CssWordSpacingZeroLengthLegal()
        {
            var snippet = "word-spacing: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssWordSpacingLengthFloatRemLegal()
        {
            var snippet = "word-spacing: .3rem ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.3rem", property.Value);
        }

        [Test]
        public void CssWordSpacingLengthFloatEmLegal()
        {
            var snippet = "word-spacing: 0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0.3em", property.Value);
        }

        [Test]
        public void CssWordSpacingNormalLegal()
        {
            var snippet = "word-spacing: normal ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssTextShadowLegalInsetAtLast()
        {
            var snippet = "text-shadow: 0 0 2px black inset";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("inset 0 0 2px", property.Value);
        }

        [Test]
        public void CssTextShadowLegalColorInFront()
        {
            var snippet = "text-shadow: rgba(255,255,255,0.5) 0px 3px 3px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("0 3px 3px rgba(255, 255, 255, 0.5)", property.Value);
        }

        [Test]
        public void CssTextShadowLegalMultipleMultilines()
        {
            var snippet = @"text-shadow: 0px 3px 0px #b2a98f,
             0px 14px 10px rgba(0,0,0,0.15),
             0px 24px 2px rgba(0,0,0,0.1),
             0px 34px 30px rgba(0,0,0,0.1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("0 3px rgba(178, 169, 143, 1), 0 14px 10px rgba(0, 0, 0, 0.15), 0 24px 2px rgba(0, 0, 0, 0.1), 0 34px 30px rgba(0, 0, 0, 0.1)", property.Value);
        }

        [Test]
        public void CssTextShadowLegalMultipleInline()
        {
            var snippet = "text-shadow: 4px 3px 0px #fff, 9px 8px 0px rgba(0,0,0,0.15)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("4px 3px rgba(255, 255, 255, 1), 9px 8px rgba(0, 0, 0, 0.15)", property.Value);
        }

        [Test]
        public void CssTextShadowLegalColorRgbaLast()
        {
            var snippet = "text-shadow: 2px 4px 3px rgba(0,0,0,0.3)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("2px 4px 3px rgba(0, 0, 0, 0.3)", property.Value);
        }

        [Test]
        public void CssTextAlignLegalJustify()
        {
            var snippet = "text-align:justify";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("justify", property.Value);
        }

        [Test]
        public void CssTextIndentLegalLength()
        {
            var snippet = "text-indent:3em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("3em", property.Value);
        }

        [Test]
        public void CssTextIndentLegalZero()
        {
            var snippet = "text-indent:0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssTextIndentLegalPercent()
        {
            var snippet = "text-indent:10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.AreEqual("10%", property.Value);
        }

        [Test]
        public void CssTextIndentIllegalNone()
        {
            var snippet = "text-indent:none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
        }

        [Test]
        public void CssTextDecorationIllegal()
        {
            var snippet = "text-decoration: line-pass";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssTextDecorationLegalLineThrough()
        {
            var snippet = "text-decoration: line-Through";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.AreEqual("line-through", property.Value);
        }

        [Test]
        public void CssTextDecorationLegalUnderlineOverline()
        {
            var snippet = "text-decoration:  underline  overline";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.AreEqual("underline overline", property.Value);
        }

        [Test]
        public void CssTextDecorationColorLegalHex()
        {
            var snippet = "text-decoration-color: #F00";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.AreEqual("rgba(255, 0, 0, 1)", property.Value);
        }

        [Test]
        public void CssTextDecorationColorLegalRed()
        {
            var snippet = "text-decoration-color: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.AreEqual("rgba(255, 0, 0, 1)", property.Value);
        }

        [Test]
        public void CssTextDecorationLineIllegalInteger()
        {
            var snippet = "text-decoration-line: 5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
        }

        [Test]
        public void CssTextDecorationLineLegalNone()
        {
            var snippet = "text-decoration-line: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssTextDecorationLineLegalOverlineUnderlineLineThrough()
        {
            var snippet = "text-decoration-line: overline    underline line-through  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.AreEqual("overline underline line-through", property.Value);
        }

        [Test]
        public void CssTextDecorationStyleLegalWavyUppercase()
        {
            var snippet = "text-decoration-style: WAVY ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.AreEqual("wavy", property.Value);
        }

        [Test]
        public void CssTextDecorationStyleIllegalMultiple()
        {
            var snippet = "text-decoration-style: wavy dotted";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
        }

        [Test]
        public void CssTextDecorationExpansionAndRecombination()
        {
            var snippet = ".centered {text-decoration:underline;}";
            var expected = ".centered { text-decoration: underline }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
		}

		[Test]
		public void CssWordBreakNormalLegal()
		{
			var snippet = "word-break : normal";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("word-break", property.Name);
			Assert.IsTrue(property.HasValue);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.AreEqual("normal", property.Value);
		}

		[Test]
		public void CssWordBreakBreakAllLegal()
		{
			var snippet = "word-break : break-all";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("word-break", property.Name);
			Assert.IsTrue(property.HasValue);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.AreEqual("break-all", property.Value);
		}

		[Test]
		public void CssWordBreakKeepAllLegal()
		{
			var snippet = "word-break : keep-all";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("word-break", property.Name);
			Assert.IsTrue(property.HasValue);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.AreEqual("keep-all", property.Value);
		}

		[Test]
		public void CssWordBreakNoneIllegal()
		{
			var snippet = "word-break : none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("word-break", property.Name);
			Assert.IsFalse(property.HasValue);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
		}

		public void CssTextAlignLastAutoLegal()
		{
			var snippet = "text-align-last: auto";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("auto", property.Value);
		}

		[Test]
		public void CssTextAlignLastStartLegal()
		{
			var snippet = "text-align-last: start";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("start", property.Value);
		}

		[Test]
		public void CssTextAlignLastEndLegal()
		{
			var snippet = "text-align-last: end";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("end", property.Value);
		}

		[Test]
		public void CssTextAlignLastRightLegal()
		{
			var snippet = "text-align-last: right";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("right", property.Value);
		}

		[Test]
		public void CssTextAlignLastLeftLegal()
		{
			var snippet = "text-align-last: left";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("left", property.Value);
		}

		[Test]
		public void CssTextAlignLastCenterLegal()
		{
			var snippet = "text-align-last: center";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("center", property.Value);
		}

		[Test]
		public void CssTextAlignLastJustifyLegal()
		{
			var snippet = "text-align-last: justify";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("justify", property.Value);
		}

		[Test]
		public void CssTextAlignLastNoneIllegal()
		{
			var snippet = "text-align-last: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-align-last", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssTextAnchorStartLegal()
		{
			var snippet = "text-anchor: start";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-anchor", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("start", property.Value);
		}

		[Test]
		public void CssTextAnchorMiddleLegal()
		{
			var snippet = "text-anchor: middle";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-anchor", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("middle", property.Value);
		}

		[Test]
		public void CssTextAnchorEndLegal()
		{
			var snippet = "text-anchor: end";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-anchor", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("end", property.Value);
		}

		[Test]
		public void CssTextAnchorNoneIllegal()
		{
			var snippet = "text-anchor: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-anchor", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssTextJustifyAutoLegal()
		{
			var snippet = "text-justify: auto";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("auto", property.Value);
		}

		[Test]
		public void CssTextJustifyDistributeLegal()
		{
			var snippet = "text-justify: distribute";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("distribute", property.Value);
		}

		[Test]
		public void CssTextJustifyDistributeAllLinesLegal()
		{
			var snippet = "text-justify: distribute-all-lines";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("distribute-all-lines", property.Value);
		}

		[Test]
		public void CssTextJustifyDistributeCenterLastLegal()
		{
			var snippet = "text-justify: distribute-center-last";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("distribute-center-last", property.Value);
		}

		[Test]
		public void CssTextJustifyInterClusterLegal()
		{
			var snippet = "text-justify: inter-cluster";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("inter-cluster", property.Value);
		}

		[Test]
		public void CssTextJustifyInterIdeographLegal()
		{
			var snippet = "text-justify: inter-ideograph";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("inter-ideograph", property.Value);
		}

		[Test]
		public void CssTextJustifyInterWordLegal()
		{
			var snippet = "text-justify: inter-word";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("inter-word", property.Value);
		}

		[Test]
		public void CssTextJustifyKashidaLegal()
		{
			var snippet = "text-justify: kashida";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("kashida", property.Value);
		}

		[Test]
		public void CssTextJustifyNewspaperLegal()
		{
			var snippet = "text-justify: newspaper";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("newspaper", property.Value);
		}

		[Test]
		public void CssTextJustifyNoneIllegal()
		{
			var snippet = "text-justify: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("text-justify", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssOverflowWrapNormalLegal()
		{
			var snippet = "overflow-wrap: normal";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("overflow-wrap", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("normal", property.Value);
		}

		[Test]
		public void CssOverflowWrapAlternateNameNormalLegal()
		{
			var snippet = "word-wrap: normal";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("word-wrap", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("normal", property.Value);
		}

		[Test]
		public void CssOverflowWrapBreakWordLegal()
		{
			var snippet = "overflow-wrap: break-word";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("overflow-wrap", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("break-word", property.Value);
		}

		[Test]
		public void CssOverflowWrapAlternateNameBreakWordLegal()
		{
			var snippet = "word-wrap: break-word";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("word-wrap", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("break-word", property.Value);
		}

		[Test]
		public void CssOverflowWrapNoneIllegal()
		{
			var snippet = "overflow-wrap: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("overflow-wrap", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssOverflowWrapAlternateNameNoneIllegal()
		{
			var snippet = "word-wrap: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("word-wrap", property.Name);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.HasValue);
		}
	}
}
