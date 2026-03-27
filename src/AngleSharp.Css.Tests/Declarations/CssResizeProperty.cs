namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssResizePropertyTests
    {
        [Test]
        public void CssResizeNoneLegal()
        {
            var snippet = "resize: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("resize"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssResizeScaledownIllegal()
        {
            var snippet = "resize: scaledown";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("resize"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssResizeBothLegal()
        {
            var snippet = "resize : both";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("resize"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("both"));
        }

        [Test]
        public void CssResizeHorizontalLegal()
        {
            var snippet = "resize : horizontal";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("resize"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("horizontal"));
        }

        [Test]
        public void CssResizeVerticalLegal()
        {
            var snippet = "resize : vertical";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("resize"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("vertical"));
        }
    }
}
