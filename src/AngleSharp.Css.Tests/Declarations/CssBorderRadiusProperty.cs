namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBorderRadiusPropertyTests
    {
        [Test]
        public void CssBorderBottomLeftRadiusPxPxLegal()
        {
            var snippet = "border-bottom-left-radius: 40px  40px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("40px 40px", property.Value);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPxEmLegal()
        {
            var snippet = "border-bottom-left-radius  : 40px 20em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("40px 20em", property.Value);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPxPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10px 5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 5%", property.Value);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10%", property.Value);
        }

        [Test]
        public void CssBorderBottomRightRadiusZeroLegal()
        {
            var snippet = "border-bottom-right-radius: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssBorderBottomRightRadiusPxLegal()
        {
            var snippet = "border-bottom-right-radius: 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("20px", property.Value);
        }

        [Test]
        public void CssBorderTopLeftRadiusCmLegal()
        {
            var snippet = "border-top-left-radius: 3.5cm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3.5cm", property.Value);
        }

        [Test]
        public void CssBorderTopRightRadiusPercentPercentLegal()
        {
            var snippet = "border-top-right-radius: 15% 3.5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15% 3.5%", property.Value);
        }

        [Test]
        public void CssBorderRadiusPercentPercentLegal()
        {
            var snippet = "border-radius: 15% 3.5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15% 3.5%", property.Value);
        }

        [Test]
        public void CssBorderRadiusZeroLegal()
        {
            var snippet = "border-radius: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssBorderRadiusThreeLengthsLegal()
        {
            var snippet = "border-radius: 2px 4px 3px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2px 4px 3px", property.Value);
        }

        [Test]
        public void CssBorderRadiusFourLengthsLegal()
        {
            var snippet = "border-radius: 2px 4px 3px 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2px 4px 3px 0", property.Value);
        }

        [Test]
        public void CssBorderRadiusFiveLengthsIllegal()
        {
            var snippet = "border-radius: 2px 4px 3px 0 1px";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssBorderRadiusLengthFractionLegal()
        {
            var snippet = "border-radius: 1em/5em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("1em / 5em", property.Value);
        }

        [Test]
        public void CssBorderRadiusLengthFractionInbalancedLegal()
        {
            var snippet = "border-radius: 4px 3px 6px / 2px 4px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("4px 3px 6px / 2px 4px", property.Value);
        }

        [Test]
        public void CssBorderRadiusFullFractionLegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em / 2px 4px 0 20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("4px 3px 6px 1em / 2px 4px 0 20%", property.Value);
        }

        [Test]
        public void CssBorderRadiusFiveTailFractionIllegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em / 2px 4px 0 20% 0";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssBorderRadiusFiveHeadFractionIllegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em 0 / 2px 4px 0 20%";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssBorderRadiusCircleShouldBeExpandedAndRecombinedCorrectly()
        {
            var snippet = ".centered { border-radius: 5px; }";
            var expected = ".centered { border-radius: 5px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusEllipseShouldBeExpandedAndRecombinedCorrectly()
        {
            var snippet = ".centered { border-radius: 5px/3px; }";
            var expected = ".centered { border-radius: 5px / 3px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusSimplificationShouldWork()
        {
            var snippet = ".centered { border-top-left-radius: 0 1px; border-bottom-left-radius: 1px 2px; border-top-right-radius: 0 3px; border-bottom-right-radius: 1px 4px; }";
            var expected = ".centered { border-radius: 0 0 1px 1px / 1px 3px 4px 2px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusRecombinationAndReductionCheck()
        {
            var snippet = ".centered { border-top-left-radius: 0 1px; border-bottom-left-radius: 0 1px; border-top-right-radius: 1px 1px; border-bottom-right-radius: 0 1px; }";
            var expected = ".centered { border-radius: 0 1px 0 0 / 1px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusPureCircleRecombination()
        {
            var snippet = ".test { border-top-left-radius:15px;border-bottom-left-radius:15px;border-bottom-right-radius:0;border-top-right-radius:0;}";
            var expected = ".test { border-radius: 15px 0 0 15px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }
    }
}
