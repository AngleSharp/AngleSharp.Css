namespace AngleSharp.Css.Tests.Styling
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Tests for Phase 16 Individual Transform CSS properties:
    /// - rotate
    /// - scale
    /// - translate
    /// </summary>
    [TestFixture]
    public class CssPhase16IndividualTransformsTests
    {
        #region rotate property tests

        [Test]
        public void RotateWithNoneKeyword()
        {
            var snippet = "rotate: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("rotate", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void RotateWithAngleValue()
        {
            var snippet = "rotate: 45deg;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("rotate", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void RotateWithAxisAndAngle()
        {
            var snippet = "rotate: 1 0 0 45deg;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("rotate", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        #endregion

        #region scale property tests

        [Test]
        public void ScaleWithNoneKeyword()
        {
            var snippet = "scale: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scale", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void ScaleWithSingleNumber()
        {
            var snippet = "scale: 1.5;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scale", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void ScaleWithTwoNumbers()
        {
            var snippet = "scale: 1.5 2;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scale", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void ScaleWithThreeNumbers()
        {
            var snippet = "scale: 1 1.5 2;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scale", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        #endregion

        #region translate property tests

        [Test]
        public void TranslateWithNoneKeyword()
        {
            var snippet = "translate: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("translate", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void TranslateWithSingleLength()
        {
            var snippet = "translate: 10px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("translate", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void TranslateWithTwoLengths()
        {
            var snippet = "translate: 10px 20px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("translate", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void TranslateWithThreeLengths()
        {
            var snippet = "translate: 10px 20px 30px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("translate", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void TranslateWithPercentages()
        {
            var snippet = "translate: 50% 25%;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("translate", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        #endregion
    }
}
