namespace AngleSharp.Css.Tests
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBoxAlignmentProperties
    {
        [Test]
        public void JustifyItemsKeywordFlexStart()
        {
            var property = ParseDeclaration("justify-items: flex-start");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("justify-items"));
        }

        [Test]
        public void JustifyItemsKeywordFlexEnd()
        {
            var property = ParseDeclaration("justify-items: flex-end");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifyItemsKeywordCenter()
        {
            var property = ParseDeclaration("justify-items: center");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifyItemsKeywordBaseline()
        {
            var property = ParseDeclaration("justify-items: baseline");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifyItemsKeywordStretch()
        {
            var property = ParseDeclaration("justify-items: stretch");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifyItemsInvalidValue()
        {
            var property = ParseDeclaration("justify-items: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void JustifySelfKeywordAuto()
        {
            var property = ParseDeclaration("justify-self: auto");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("justify-self"));
        }

        [Test]
        public void JustifySelfKeywordCenter()
        {
            var property = ParseDeclaration("justify-self: center");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifySelfKeywordFlexStart()
        {
            var property = ParseDeclaration("justify-self: flex-start");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifySelfKeywordBaseline()
        {
            var property = ParseDeclaration("justify-self: baseline");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifySelfKeywordStretch()
        {
            var property = ParseDeclaration("justify-self: stretch");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void JustifySelfInvalidValue()
        {
            var property = ParseDeclaration("justify-self: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void PlaceContentSingleValue()
        {
            var property = ParseDeclaration("place-content: center");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("place-content"));
        }

        [Test]
        public void PlaceContentTwoValues()
        {
            var property = ParseDeclaration("place-content: center flex-end");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceContentFlexStart()
        {
            var property = ParseDeclaration("place-content: flex-start");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceContentSpaceAround()
        {
            var property = ParseDeclaration("place-content: space-around");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceContentInvalidValue()
        {
            var property = ParseDeclaration("place-content: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void PlaceItemsSingleValue()
        {
            var property = ParseDeclaration("place-items: center");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("place-items"));
        }

        [Test]
        public void PlaceItemsTwoValues()
        {
            var property = ParseDeclaration("place-items: center flex-end");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceItemsFlexStart()
        {
            var property = ParseDeclaration("place-items: flex-start");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceItemsBaseline()
        {
            var property = ParseDeclaration("place-items: baseline");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceItemsInvalidValue()
        {
            var property = ParseDeclaration("place-items: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void PlaceSelfSingleValue()
        {
            var property = ParseDeclaration("place-self: center");
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Name, Is.EqualTo("place-self"));
        }

        [Test]
        public void PlaceSelfTwoValues()
        {
            var property = ParseDeclaration("place-self: center flex-end");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceSelfAuto()
        {
            var property = ParseDeclaration("place-self: auto");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceSelfAutoCenter()
        {
            var property = ParseDeclaration("place-self: auto center");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceSelfFlexStart()
        {
            var property = ParseDeclaration("place-self: flex-start");
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void PlaceSelfInvalidValue()
        {
            var property = ParseDeclaration("place-self: invalid");
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void JustifyItemsInitialValue()
        {
            var rule = ParseRule("div { justify-items: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["justify-items"], Is.Not.Null);
        }

        [Test]
        public void JustifySelfInitialValue()
        {
            var rule = ParseRule("div { justify-self: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["justify-self"], Is.Not.Null);
        }

        [Test]
        public void PlaceContentInitialValue()
        {
            var rule = ParseRule("div { place-content: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["place-content"], Is.Not.Null);
        }

        [Test]
        public void PlaceItemsInitialValue()
        {
            var rule = ParseRule("div { place-items: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["place-items"], Is.Not.Null);
        }

        [Test]
        public void PlaceSelfInitialValue()
        {
            var rule = ParseRule("div { place-self: initial; }") as CssStyleRule;
            Assert.That(rule, Is.Not.Null);
            var style = rule!.Style;
            Assert.That(style["place-self"], Is.Not.Null);
        }
    }
}
