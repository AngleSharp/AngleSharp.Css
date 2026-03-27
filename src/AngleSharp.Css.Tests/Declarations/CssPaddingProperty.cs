namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPaddingPropertyTests
    {
        [Test]
        public void CssPaddingLeftLengthLegal()
        {
            var snippet = "padding-left: 15px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding-left"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15px"));
        }

        [Test]
        public void CssPaddingRightLengthImportantLegal()
        {
            var snippet = "padding-right: 3em!important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding-right"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3em"));
        }

        [Test]
        public void CssPaddingTopPercentLegal()
        {
            var snippet = "padding-top: 4% ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding-top"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("4%"));
        }

        [Test]
        public void CssPaddingBottomZeroLegal()
        {
            var snippet = "padding-bottom: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding-bottom"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssPaddingAllZeroLegal()
        {
            var snippet = "padding: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssPaddingAllPercentLegal()
        {
            var snippet = "padding: 25% ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("25%"));
        }

        [Test]
        public void CssPaddingSidesLengthLegal()
        {
            var snippet = "padding: 10px 3em ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 3em"));
        }

        [Test]
        public void CssPaddingAutoIllegal()
        {
            var snippet = "padding: auto ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssPaddingThreeValuesLegal()
        {
            var snippet = "padding: 10px 3em 5px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 3em 5px"));
        }

        [Test]
        public void CssPaddingAllValuesWithPercentLegal()
        {
            var snippet = "padding: 10px 5% 8px 2% ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("padding"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 5% 8px 2%"));
        }

        [Test]
        public void CssPaddingTooManyValuesIllegal()
        {
            var snippet = "padding: 10px 5% 8px 2% 3px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssPaddingShouldBeRecombinedCorrectly()
        {
            var snippet = ".centered {padding-bottom: 2em; padding-top: 2.5em; padding-left: 0; padding-right: 0}";
            var expected = ".centered { padding: 2.5em 0 2em }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
