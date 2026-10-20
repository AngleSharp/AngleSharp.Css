#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The computed value of a `border-*-radius` is "a pair of computed &lt;length-percentage&gt;
    /// values" (https://www.w3.org/TR/css-backgrounds-3/#border-radius): a percentage stays a
    /// percentage, because it refers to the border box, which only layout knows. Resolving it to
    /// pixels at style-computation time only works for a box whose width and height are both
    /// authored; for an auto-sized box, or a form control whose size is intrinsic, the percentage
    /// collapses to `0`. Found in a downstream renderer (AngleSharp.Renderer): a radio button
    /// styled `border-radius: 50%` (as every UA stylesheet does) rendered square. Note that
    /// `BorderRadiusPercentageResolutionTests` currently expects pixel values for explicitly sized
    /// boxes; it needs to change along with this fix.
    /// </summary>
    [TestFixture]
    public class BorderRadiusPercentageComputedValueTests
    {
        private static string ComputedRadius(string html)
        {
            var document = ParseDocument($"<body>{html}</body>");
            return document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("border-top-left-radius");
        }

        [Test]
        public void PercentageRadiusOfAnAutoSizedBoxStaysAPercentage()
        {
            Assert.AreEqual("50%", ComputedRadius("<div id=target style=\"border-radius: 50%\">x</div>"));
        }

        [Test]
        public void PercentageRadiusOfAnIntrinsicallySizedControlStaysAPercentage()
        {
            Assert.AreEqual("50%", ComputedRadius("<input id=target type=checkbox style=\"border-radius: 50%\">"));
        }

        [Test]
        public void EllipticalPercentageRadiusKeepsBothComponents()
        {
            Assert.AreEqual("10% 30%", ComputedRadius("<div id=target style=\"border-radius: 10% / 30%\">x</div>"));
        }

        [Test]
        public void LengthRadiusIsStillComputedToPixels()
        {
            Assert.AreEqual("8px", ComputedRadius("<div id=target style=\"border-radius: 0.5em; font-size: 16px\">x</div>"));
        }
    }
}
