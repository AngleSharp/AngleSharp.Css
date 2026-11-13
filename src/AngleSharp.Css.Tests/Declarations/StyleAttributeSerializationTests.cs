#nullable disable
namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// A style changed by script is serialized into the style attribute with every change, which
    /// is parsed again by whoever reads the attribute.
    /// </summary>
    [TestFixture]
    public class StyleAttributeSerializationTests
    {
        [TestCase("-0", "-0")]
        [TestCase("+0", "0")]
        [TestCase("0.0", "0")]
        [TestCase("-0.0", "-0")]
        public void UnitlessZeroIsALength(string value, string expected)
        {
            var declarations = ParseDeclarations($"left: {value}; margin-top: {value}");

            Assert.AreEqual(expected, declarations.GetPropertyValue("left"));
            Assert.AreEqual(expected, declarations.GetPropertyValue("margin-top"));
        }

        [Test]
        public void UnitlessNonZeroIsNoLength()
        {
            var declarations = ParseDeclarations("left: 1; margin-top: -0.5");

            Assert.AreEqual(string.Empty, declarations.GetPropertyValue("left"));
            Assert.AreEqual(string.Empty, declarations.GetPropertyValue("margin-top"));
        }

        [Test]
        public void ABackgroundPositionWithANegativeZeroSurvivesTheStyleAttribute()
        {
            var document = ParseDocument("<div id=sprite></div>");
            var sprite = document.GetElementById("sprite");

            // what a game does to show another frame of a sprite
            sprite.GetStyle().SetProperty("background-position", "-80px -0px");
            var copy = document.CreateElement("div");
            copy.SetAttribute("style", sprite.GetAttribute("style"));

            Assert.AreEqual("-80px", copy.GetStyle().GetPropertyValue("background-position-x"));
            Assert.AreEqual("-0", copy.GetStyle().GetPropertyValue("background-position-y"));
        }

        [TestCase("12.5px", "12.5px")]
        [TestCase("160px", "160px")]
        [TestCase("-374.900314795383px", "-374.900314795383px")]
        [TestCase("0.30000000000000004px", "0.3px")]
        [TestCase("0.0001px", "0.0001px")]
        [TestCase("0.00001px", "0.00001px")]
        [TestCase("1234.5678901234567px", "1234.56789012346px")]
        [TestCase("100000000000000000px", "100000000000000000px")]
        [TestCase("-2.5e3px", "-2500px")]
        public void LengthsAreSerializedAsBefore(string value, string expected)
        {
            var declarations = ParseDeclarations($"left: {value}");

            Assert.AreEqual(expected, declarations.GetPropertyValue("left"));
        }

        [Test]
        public void TheStyleAttributeFollowsEveryChange()
        {
            var document = ParseDocument("<div id=sprite style='left: 160px; background-image: url(sprite.png); background-position: 0 0; width: 80px'></div>");
            var sprite = document.GetElementById("sprite");
            var style = sprite.GetStyle();

            for (var i = 1; i < 4; i++)
            {
                style.SetProperty("left", $"{160 + i}px");
                style.SetProperty("background-position", $"-{i * 80}px -0px");

                Assert.AreEqual($"left: {160 + i}px; background-image: url(\"sprite.png\"); background-position: -{i * 80}px -0; width: 80px", sprite.GetAttribute("style"));
            }
        }
    }
}
