namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssWidthPropertyTests
    {
        [Test]
        public void CssWidthPropertyAutoLegal()
        {
            var snippet = "width: auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }
        
        [Test]
        public void CssWidthPropertyFitContentLegal()
        {
            var snippet = "width: fit-content";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fit-content"));
        }

        [Test]
        public void CssWidthPropertyValueInPxLegal()
        {
            var snippet = "width: 42px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("42px"));
        }
    }
}
