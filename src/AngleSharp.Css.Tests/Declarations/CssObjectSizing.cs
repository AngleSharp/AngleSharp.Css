namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssObjectSizingTests
    {
        [Test]
        public void CssObjectFitNoneLegal()
        {
            var snippet = "object-fit : none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-fit"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void ObjectFitScaledownIllegal()
        {
            var snippet = "object-fit : scaledown";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-fit"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void ObjectFitScaleDownLegal()
        {
            var snippet = "object-fit : scale-DOWN";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-fit"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("scale-down"));
        }

        [Test]
        public void CssObjectFitCoverLegal()
        {
            var snippet = "object-fit : cover";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-fit"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("cover"));
        }

        [Test]
        public void CssObjectFitContainLegal()
        {
            var snippet = "object-fit : contain";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-fit"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("contain"));
        }

        [Test]
        public void CssObjectPositionCenterLegal()
        {
            var snippet = "object-position : center";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("center"));
        }

        [Test]
        public void ObjectPositionTopLeftIllegal()
        {
            var snippet = "object-position : top-left";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void ObjectPositionTopLeftLegal()
        {
            var snippet = "object-position : top left";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("left top"));
        }

        [Test]
        public void CssObjectPosition5050Legal()
        {
            var snippet = "object-position : 50%   50% ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("center"));
        }

        [Test]
        public void CssObjectPositionLeft30Legal()
        {
            var snippet = "object-position : left  30px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("object-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsAnimatable, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0 30px"));
        }
    }
}
