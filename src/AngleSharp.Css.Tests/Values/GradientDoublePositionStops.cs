#nullable disable
namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;
    using static ValueConverters;

    /// <summary>
    /// CSS Images 4 allows a color stop to carry two positions (`red 0 50%`), shorthand for the
    /// same color at both positions - the usual way to write hard-edged stripes. The gradient
    /// parser does not accept the second position, so the whole gradient fails to parse and,
    /// as a `background-image` or `border-image-source`, silently paints nothing. Found in a
    /// downstream renderer (AngleSharp.Renderer) while implementing `border-image`.
    /// </summary>
    [TestFixture]
    public class GradientDoublePositionStopsTests
    {
        [TestCase("linear-gradient(red 0 50%, blue 50% 100%)")]
        [TestCase("repeating-linear-gradient(45deg, red 0 6px, yellow 6px 12px)")]
        [TestCase("radial-gradient(red 0 20%, blue 20%)")]
        [TestCase("conic-gradient(red 0deg 90deg, blue 90deg 180deg)")]
        public void DoublePositionColorStopsParse(string source)
        {
            Assert.IsNotNull(GradientConverter.Convert(source), source);
        }

        [Test]
        public void DoublePositionColorStopExpandsIntoTwoStops()
        {
            var gradient = GradientConverter.Convert("linear-gradient(red 0 50%, blue 50% 100%)") as CssLinearGradientValue;

            Assert.IsNotNull(gradient);
            var stops = gradient.Stops.OfType<CssGradientStopValue>().ToArray();
            Assert.AreEqual(4, stops.Length);
            Assert.AreEqual(stops[0].Color, stops[1].Color);
            Assert.AreEqual(stops[2].Color, stops[3].Color);
        }
    }
}
