namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssMarginPropertyTests
    {
        [Test]
        public void CssMarginLeftLengthLegal()
        {
            var snippet = "margin-left: 15px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15px", property.Value);
        }

        [Test]
        public void CssMarginLeftInitialLegal()
        {
            var snippet = "margin-left: initial ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("initial", property.Value);
        }

        [Test]
        public void CssMarginRightLengthImportantLegal()
        {
            var snippet = "margin-right: 3em!important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-right", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3em", property.Value);
        }

        [Test]
        public void CssMarginRightPercentLegal()
        {
            var snippet = "margin-right: 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10%", property.Value);
        }

        [Test]
        public void CssMarginTopPercentLegal()
        {
            var snippet = "margin-top: 4% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("4%", property.Value);
        }

        [Test]
        public void CssMarginBottomZeroLegal()
        {
            var snippet = "margin-bottom: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssMarginBottomNegativeLegal()
        {
            var snippet = "margin-bottom: -3px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("-3px", property.Value);
        }

        [Test]
        public void CssMarginBottomAutoLegal()
        {
            var snippet = "margin-bottom: auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssMarginAllZeroLegal()
        {
            var snippet = "margin: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void CssMarginAllPercentLegal()
        {
            var snippet = "margin: 25% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("25%", property.Value);
        }

        [Test]
        public void CssMarginSidesLengthLegal()
        {
            var snippet = "margin: 10px 3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 3em", property.Value);
        }

        [Test]
        public void CssMarginSidesLengthAndAutoLegal()
        {
            var snippet = "margin: 10px auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px auto", property.Value);
        }

        [Test]
        public void CssMarginAutoLegal()
        {
            var snippet = "margin: auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssMarginThreeValuesLegal()
        {
            var snippet = "margin: 10px 3em 5px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 3em 5px", property.Value);
        }

        [Test]
        public void CssMarginAllValuesWithPercentAndAutoLegal()
        {
            var snippet = "margin: 10px 5% auto 2% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 5% auto 2%", property.Value);
        }

        [Test]
        public void CssMarginTooManyValuesIllegal()
        {
            var snippet = "margin: 10px 5% 8px 2% 3px auto";
            var property = ParseDeclaration(snippet);
            Assert.IsNull(property);
        }

        [Test]
        public void CssMarginShouldBeRecombinedCorrectly()
        {
            var snippet = ".centered {margin-bottom: 1px; margin-top: 2px; margin-left: 3px; margin-right: 4px}";
            var expected = ".centered { margin: 2px 4px 1px 3px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssMarginShouldBeSimplifiedCorrectly()
        {
            var snippet = ".centered {margin:0;margin-left:auto;margin-right:auto;text-align:left;}";
            var expected = ".centered { margin: 0 auto; text-align: left }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssMarginShouldBeReducedCompletely()
        {
            var snippet = ".centered {margin-bottom: 0px; margin-top: 0; margin-left: 0px; margin-right: 0}";
            var expected = ".centered { margin: 0 }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssMarginReductionForPeriodicExpansion()
        {
            var snippet = "p { margin: 0 auto; }";
            var expected = "p { margin: 0 auto }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }
    }
}
