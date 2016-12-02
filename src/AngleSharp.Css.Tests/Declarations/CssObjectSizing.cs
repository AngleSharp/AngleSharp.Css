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
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void ObjectFitScaledownIllegal()
        {
            var snippet = "object-fit : scaledown";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void ObjectFitScaleDownLegal()
        {
            var snippet = "object-fit : scale-DOWN";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("scale-down", property.Value);
        }

        [Test]
        public void CssObjectFitCoverLegal()
        {
            var snippet = "object-fit : cover";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("cover", property.Value);
        }

        [Test]
        public void CssObjectFitContainLegal()
        {
            var snippet = "object-fit : contain";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("contain", property.Value);
        }

        [Test]
        public void CssObjectPositionCenterLegal()
        {
            var snippet = "object-position : center";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("center", property.Value);
        }

        [Test]
        public void ObjectPositionTopLeftIllegal()
        {
            var snippet = "object-position : top-left";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void ObjectPositionTopLeftLegal()
        {
            var snippet = "object-position : top left";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("left top", property.Value);
        }

        [Test]
        public void CssObjectPosition5050Legal()
        {
            var snippet = "object-position : 50%   50% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("center", property.Value);
        }

        [Test]
        public void CssObjectPositionLeft30Legal()
        {
            var snippet = "object-position : left  30px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0 30px", property.Value);
        }
    }
}
