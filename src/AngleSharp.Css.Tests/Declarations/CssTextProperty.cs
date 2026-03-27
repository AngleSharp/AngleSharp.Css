namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
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
            Assert.That(property.Name, Is.EqualTo("word-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssWordSpacingLengthFloatRemLegal()
        {
            var snippet = "word-spacing: .3rem ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("word-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.3rem"));
        }

        [Test]
        public void CssWordSpacingLengthFloatEmLegal()
        {
            var snippet = "word-spacing: 0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("word-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.3em"));
        }

        [Test]
        public void CssWordSpacingNormalLegal()
        {
            var snippet = "word-spacing: normal ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("word-spacing"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("normal"));
        }

        [Test]
        public void CssTextShadowLegalInsetAtLast()
        {
            var snippet = "text-shadow: 0 0 2px black inset";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-shadow"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("inset 0 0 2px rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssTextShadowLegalColorInFront()
        {
            var snippet = "text-shadow: rgba(255,255,255,0.5) 0px 3px 3px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-shadow"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("0 3px 3px rgba(255, 255, 255, 0.5)"));
        }

        [Test]
        public void CssTextShadowLegalMultipleMultilines()
        {
            var snippet = @"text-shadow: 0px 3px 0px #b2a98f,
             0px 14px 10px rgba(0,0,0,0.15),
             0px 24px 2px rgba(0,0,0,0.1),
             0px 34px 30px rgba(0,0,0,0.1)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-shadow"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("0 3px rgba(178, 169, 143, 1), 0 14px 10px rgba(0, 0, 0, 0.15), 0 24px 2px rgba(0, 0, 0, 0.1), 0 34px 30px rgba(0, 0, 0, 0.1)"));
        }

        [Test]
        public void CssTextShadowLegalMultipleInline()
        {
            var snippet = "text-shadow: 4px 3px 0px #fff, 9px 8px 0px rgba(0,0,0,0.15)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-shadow"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("4px 3px rgba(255, 255, 255, 1), 9px 8px rgba(0, 0, 0, 0.15)"));
        }

        [Test]
        public void CssTextShadowLegalColorRgbaLast()
        {
            var snippet = "text-shadow: 2px 4px 3px rgba(0,0,0,0.3)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-shadow"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("2px 4px 3px rgba(0, 0, 0, 0.3)"));
        }

        [Test]
        public void CssTextAlignLegalJustify()
        {
            var snippet = "text-align:justify";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-align"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("justify"));
        }

        [Test]
        public void CssTextIndentLegalLength()
        {
            var snippet = "text-indent:3em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-indent"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("3em"));
        }

        [Test]
        public void CssTextIndentLegalZero()
        {
            var snippet = "text-indent:0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-indent"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssTextIndentLegalPercent()
        {
            var snippet = "text-indent:10%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-indent"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("10%"));
        }

        [Test]
        public void CssTextIndentIllegalNone()
        {
            var snippet = "text-indent:none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-indent"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsImportant, Is.False);
        }

        [Test]
        public void CssTextDecorationIllegal()
        {
            var snippet = "text-decoration: line-pass";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssTextDecorationLegalLineThrough()
        {
            var snippet = "text-decoration: line-Through";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.Value, Is.EqualTo("line-through"));
        }

        [Test]
        public void CssTextDecorationExpandCorrectly_Issue35()
        {
            var source = @"<!DOCTYPE html>
<html>
<head><title></title></head>
<body style=""text-decoration: underline dotted;""></body>
</html>";
            var document = source.ToHtmlDocument(Configuration.Default.WithCss());
            var styleDeclaration = document.Body.ComputeCurrentStyle();
            Assert.That(styleDeclaration.GetTextDecorationStyle(), Is.EqualTo("dotted"));
            Assert.That(styleDeclaration.GetTextDecorationLine(), Is.EqualTo("underline"));
        }

        [Test]
        public void CssTextDecorationLegalUnderlineOverline()
        {
            var snippet = "text-decoration:  underline  overline";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.Value, Is.EqualTo("underline overline"));
        }

        [Test]
        public void CssTextDecorationColorLegalHex()
        {
            var snippet = "text-decoration-color: #F00";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration-color"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssTextDecorationColorLegalRed()
        {
            var snippet = "text-decoration-color: red";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration-color"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssTextDecorationLineIllegalInteger()
        {
            var snippet = "text-decoration-line: 5";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration-line"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
        }

        [Test]
        public void CssTextDecorationLineLegalNone()
        {
            var snippet = "text-decoration-line: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration-line"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssTextDecorationLineLegalOverlineUnderlineLineThrough()
        {
            var snippet = "text-decoration-line: overline    underline line-through  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration-line"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.Value, Is.EqualTo("overline underline line-through"));
        }

        [Test]
        public void CssTextDecorationStyleLegalWavyUppercase()
        {
            var snippet = "text-decoration-style: WAVY ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration-style"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.Value, Is.EqualTo("wavy"));
        }

        [Test]
        public void CssTextDecorationStyleIllegalMultiple()
        {
            var snippet = "text-decoration-style: wavy dotted";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-decoration-style"));
            Assert.That(property.HasValue, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.IsImportant, Is.False);
        }

        [Test]
        public void CssTextDecorationExpansionAndRecombination()
        {
            var snippet = ".centered {text-decoration:underline;}";
            var expected = ".centered { text-decoration: underline }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void CssWordBreakNormalLegal()
		{
			var snippet = "word-break : normal";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("word-break"));
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.Value, Is.EqualTo("normal"));
		}

		[Test]
		public void CssWordBreakBreakAllLegal()
		{
			var snippet = "word-break : break-all";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("word-break"));
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.Value, Is.EqualTo("break-all"));
		}

		[Test]
		public void CssWordBreakKeepAllLegal()
		{
			var snippet = "word-break : keep-all";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("word-break"));
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.Value, Is.EqualTo("keep-all"));
		}

		[Test]
		public void CssWordBreakNoneIllegal()
		{
			var snippet = "word-break : none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("word-break"));
			Assert.That(property.HasValue, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
		}

		public void CssTextAlignLastAutoLegal()
		{
			var snippet = "text-align-last: auto";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("auto"));
		}

		[Test]
		public void CssTextAlignLastStartLegal()
		{
			var snippet = "text-align-last: start";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("start"));
		}

		[Test]
		public void CssTextAlignLastEndLegal()
		{
			var snippet = "text-align-last: end";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("end"));
		}

		[Test]
		public void CssTextAlignLastRightLegal()
		{
			var snippet = "text-align-last: right";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("right"));
		}

		[Test]
		public void CssTextAlignLastLeftLegal()
		{
			var snippet = "text-align-last: left";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("left"));
		}

		[Test]
		public void CssTextAlignLastCenterLegal()
		{
			var snippet = "text-align-last: center";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("center"));
		}

		[Test]
		public void CssTextAlignLastJustifyLegal()
		{
			var snippet = "text-align-last: justify";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("justify"));
		}

		[Test]
		public void CssTextAlignLastNoneIllegal()
		{
			var snippet = "text-align-last: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-align-last"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssTextAnchorStartLegal()
		{
			var snippet = "text-anchor: start";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-anchor"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("start"));
		}

		[Test]
		public void CssTextAnchorMiddleLegal()
		{
			var snippet = "text-anchor: middle";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-anchor"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("middle"));
		}

		[Test]
		public void CssTextAnchorEndLegal()
		{
			var snippet = "text-anchor: end";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-anchor"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("end"));
		}

		[Test]
		public void CssTextAnchorNoneIllegal()
		{
			var snippet = "text-anchor: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-anchor"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssTextJustifyAutoLegal()
		{
			var snippet = "text-justify: auto";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("auto"));
		}

		[Test]
		public void CssTextJustifyDistributeLegal()
		{
			var snippet = "text-justify: distribute";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("distribute"));
		}

		[Test]
		public void CssTextJustifyDistributeAllLinesLegal()
		{
			var snippet = "text-justify: distribute-all-lines";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("distribute-all-lines"));
		}

		[Test]
		public void CssTextJustifyDistributeCenterLastLegal()
		{
			var snippet = "text-justify: distribute-center-last";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("distribute-center-last"));
		}

		[Test]
		public void CssTextJustifyInterClusterLegal()
		{
			var snippet = "text-justify: inter-cluster";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("inter-cluster"));
		}

		[Test]
		public void CssTextJustifyInterIdeographLegal()
		{
			var snippet = "text-justify: inter-ideograph";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("inter-ideograph"));
		}

		[Test]
		public void CssTextJustifyInterWordLegal()
		{
			var snippet = "text-justify: inter-word";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("inter-word"));
		}

		[Test]
		public void CssTextJustifyKashidaLegal()
		{
			var snippet = "text-justify: kashida";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("kashida"));
		}

		[Test]
		public void CssTextJustifyNewspaperLegal()
		{
			var snippet = "text-justify: newspaper";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("newspaper"));
		}

		[Test]
		public void CssTextJustifyNoneIllegal()
		{
			var snippet = "text-justify: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("text-justify"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssOverflowWrapNormalLegal()
		{
			var snippet = "overflow-wrap: normal";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("overflow-wrap"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("normal"));
		}

		[Test]
		public void CssOverflowWrapAlternateNameNormalLegal()
		{
			var snippet = "word-wrap: normal";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("word-wrap"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("normal"));
		}

		[Test]
		public void CssOverflowWrapBreakWordLegal()
		{
			var snippet = "overflow-wrap: break-word";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("overflow-wrap"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("break-word"));
		}

		[Test]
		public void CssOverflowWrapAlternateNameBreakWordLegal()
		{
			var snippet = "word-wrap: break-word";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("word-wrap"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("break-word"));
		}

		[Test]
		public void CssOverflowWrapNoneIllegal()
		{
			var snippet = "overflow-wrap: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("overflow-wrap"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssOverflowWrapAlternateNameNoneIllegal()
		{
			var snippet = "word-wrap: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("word-wrap"));
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssTextAlignLegalStart_Issue151()
        {
            var snippet = "text-align:start";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-align"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("start"));
        }

        [Test]
        public void CssTextAlignLegalJustifyAll_Issue151()
        {
            var snippet = "text-align:justify-all";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-align"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.Value, Is.EqualTo("justify-all"));
        }

        [Test]
        public void CssTextAlignIllegalJustifyNone_Issue151()
        {
            var snippet = "text-align:justify-none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("text-align"));
            Assert.That(property.HasValue, Is.False);
        }
    }
}
