namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssListPropertyTests
    {
        [Test]
        public void CssListStylePositionOutsideLegal()
        {
            var snippet = "list-style-position: outside ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("outside", property.Value);
        }

        [Test]
        public void CssListStylePositionOutsideIllegal()
        {
            var snippet = "list-style-position: out-side ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssListStylePositionNoneIllegal()
        {
            var snippet = "list-style-position: none ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssListStylePositionInsideLegal()
        {
            var snippet = "list-style-position: insiDe ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("inside", property.Value);
        }

        [Test]
        public void CssListStyleImageNoneLegal()
        {
            var snippet = "list-style-image: none ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssListStyleImageUrlLegal()
        {
            var snippet = "list-style-image: url(http://www.example.com/images/list.png)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("url(\"http://www.example.com/images/list.png\")", property.Value);
        }

        [Test]
        public void CssListStyleTypeDiscLegal()
        {
            var snippet = "list-style-type: disc ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("disc", property.Value);
        }

        [Test]
        public void CssListStyleTypeLowerAlphaLegal()
        {
            var snippet = "list-style-type: lower-ALPHA ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("lower-alpha", property.Value);
        }

        [Test]
        public void CssListStyleTypeGeorgianLegal()
        {
            var snippet = "list-style-type: georgian ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("georgian", property.Value);
        }

        [Test]
        public void CssListStyleTypeDecimalLeadingZeroLegal()
        {
            var snippet = "list-style-type: decimal-leading-zerO ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("decimal-leading-zero", property.Value);
        }

        [Test]
        public void CssListStyleTypeNumberIllegal()
        {
            var snippet = "list-style-type: number ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssListStyleCircleLegal()
        {
            var snippet = "list-style: circle ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("circle", property.Value);
        }

        [Test]
        public void CssListStyleNone()
        {
            var snippet = "list-style: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssListStyleSquareInsideLegal()
        {
            var snippet = "list-style: square inside ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("square inside", property.Value);
        }

        [Test]
        public void CssListStyleSquareImageInsideLegal()
        {
            var snippet = "list-style: square url('image.png') inside ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("square inside url(\"image.png\")", property.Value);
        }

        [Test]
        public void CssCounterResetLegal()
        {
            var snippet = "counter-reset: chapter section 1 page;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("chapter 0 section 1 page 0", property.Value);
        }

        [Test]
        public void CssCounterResetSingleLegal()
        {
            var snippet = "counter-reset: counter-name";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("counter-name 0", property.Value);
        }

        [Test]
        public void CssCounterResetNoneLegal()
        {
            var snippet = "counter-reset: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssCounterResetNumberIllegal()
        {
            var snippet = "counter-reset: 3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void CssCounterResetNegativeLegal()
        {
            var snippet = "counter-reset  :  counter-name   -1";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("counter-name -1", property.Value);
        }

        [Test]
        public void CssCounterResetTwoCountersExplicitLegal()
        {
            var snippet = "counter-reset  :  counter1   1   counter2   4  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("counter1 1 counter2 4", property.Value);
        }

        [Test]
        public void CssCounterIncrementNoneLegal()
        {
            var snippet = "counter-increment: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-increment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssCounterIncrementLegal()
        {
            var snippet = "counter-increment: chapter section 2 page";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-increment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("chapter 1 section 2 page 1", property.Value);
        }
    }
}
