namespace AngleSharp.Css.Tests.Styling
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Tests for Phase 15 UI/Interaction CSS properties:
    /// - appearance
    /// - user-select
    /// - touch-action
    /// - outline-offset
    /// - scrollbar-width
    /// - scrollbar-color
    /// - scrollbar-gutter
    /// </summary>
    [TestFixture]
    public class CssPhase15UIPropertiesTests
    {
        #region appearance property tests

        [Test]
        public void AppearanceWithAutoKeyword()
        {
            var snippet = "appearance: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("appearance", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void AppearanceWithNoneKeyword()
        {
            var snippet = "appearance: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("appearance", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        #endregion

        #region user-select property tests

        [Test]
        public void UserSelectWithAutoKeyword()
        {
            var snippet = "user-select: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("user-select", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void UserSelectWithTextKeyword()
        {
            var snippet = "user-select: text;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("user-select", property.Name);
            Assert.AreEqual("text", property.Value);
        }

        [Test]
        public void UserSelectWithNoneKeyword()
        {
            var snippet = "user-select: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("user-select", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void UserSelectWithContainKeyword()
        {
            var snippet = "user-select: contain;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("user-select", property.Name);
            Assert.AreEqual("contain", property.Value);
        }

        [Test]
        public void UserSelectWithAllKeyword()
        {
            var snippet = "user-select: all;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("user-select", property.Name);
            Assert.AreEqual("all", property.Value);
        }

        #endregion

        #region touch-action property tests

        [Test]
        public void TouchActionWithAutoKeyword()
        {
            var snippet = "touch-action: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("touch-action", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void TouchActionWithNoneKeyword()
        {
            var snippet = "touch-action: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("touch-action", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void TouchActionWithManipulationKeyword()
        {
            var snippet = "touch-action: manipulation;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("touch-action", property.Name);
            Assert.AreEqual("manipulation", property.Value);
        }

        #endregion

        #region outline-offset property tests

        [Test]
        public void OutlineOffsetWithAutoKeyword()
        {
            var snippet = "outline-offset: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-offset", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void OutlineOffsetWithLengthValue()
        {
            var snippet = "outline-offset: 10px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-offset", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void OutlineOffsetWithNegativeLengthValue()
        {
            var snippet = "outline-offset: -5px;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-offset", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        #endregion

        #region scrollbar-width property tests

        [Test]
        public void ScrollbarWidthWithAutoKeyword()
        {
            var snippet = "scrollbar-width: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-width", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void ScrollbarWidthWithThinKeyword()
        {
            var snippet = "scrollbar-width: thin;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-width", property.Name);
            Assert.AreEqual("thin", property.Value);
        }

        [Test]
        public void ScrollbarWidthWithNoneKeyword()
        {
            var snippet = "scrollbar-width: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-width", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        #endregion

        #region scrollbar-color property tests

        [Test]
        public void ScrollbarColorWithAutoKeyword()
        {
            var snippet = "scrollbar-color: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-color", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void ScrollbarColorWithColorValue()
        {
            var snippet = "scrollbar-color: #999999;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-color", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        [Test]
        public void ScrollbarColorWithNamedColor()
        {
            var snippet = "scrollbar-color: red;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-color", property.Name);
            Assert.IsNotEmpty(property.Value);
        }

        #endregion

        #region scrollbar-gutter property tests

        [Test]
        public void ScrollbarGutterWithAutoKeyword()
        {
            var snippet = "scrollbar-gutter: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-gutter", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void ScrollbarGutterWithStableKeyword()
        {
            var snippet = "scrollbar-gutter: stable;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("scrollbar-gutter", property.Name);
            Assert.AreEqual("stable", property.Value);
        }

        #endregion
    }
}

