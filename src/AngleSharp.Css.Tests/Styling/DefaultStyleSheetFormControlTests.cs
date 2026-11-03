#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// The default (UA) stylesheet gives form controls `display: inline-block` but none of the
    /// default look every browser's UA stylesheet gives them (border, padding, background, the
    /// checkbox/radio margin, the round radio). A downstream renderer (AngleSharp.Renderer) that
    /// takes every style from the computed style therefore draws an unstyled `&lt;input&gt;` as
    /// an invisible box, unless it injects that look itself based on tag names - which also makes
    /// it impossible for an author's own `input { ... }` rules to reliably override it through
    /// the cascade. The expected values match the look AngleSharp.Renderer used to inject.
    /// </summary>
    [TestFixture]
    public class DefaultStyleSheetFormControlTests
    {
        private static ICssStyleDeclaration ComputedStyle(string html)
        {
            var document = ParseDocument($"<body>{html}</body>");
            return document.GetElementById("target").ComputeCurrentStyle();
        }

        private static void AssertBorder(ICssStyleDeclaration style, string width, string lineStyle, string color)
        {
            foreach (var side in new[] { "top", "right", "bottom", "left" })
            {
                Assert.AreEqual(width, style.GetPropertyValue($"border-{side}-width"), side);
                Assert.AreEqual(lineStyle, style.GetPropertyValue($"border-{side}-style"), side);
                Assert.AreEqual(color, style.GetPropertyValue($"border-{side}-color"), side);
            }
        }

        private static void AssertPadding(ICssStyleDeclaration style, string vertical, string horizontal)
        {
            Assert.AreEqual(vertical, style.GetPropertyValue("padding-top"));
            Assert.AreEqual(horizontal, style.GetPropertyValue("padding-right"));
            Assert.AreEqual(vertical, style.GetPropertyValue("padding-bottom"));
            Assert.AreEqual(horizontal, style.GetPropertyValue("padding-left"));
        }

        [TestCase("<input id=target>")]
        [TestCase("<input id=target type=text>")]
        [TestCase("<input id=target type=email>")]
        [TestCase("<input id=target type=number>")]
        [TestCase("<input id=target type=password>")]
        public void TextFieldsHaveABorderPaddingAndAWhiteBackground(string html)
        {
            var style = ComputedStyle(html);

            AssertBorder(style, "1px", "solid", "rgba(118, 118, 118, 1)");
            AssertPadding(style, "1px", "2px");
            Assert.AreEqual("rgba(255, 255, 255, 1)", style.GetPropertyValue("background-color"));
        }

        [Test]
        public void TextAreaHasABorderPaddingAndAWhiteBackground()
        {
            var style = ComputedStyle("<textarea id=target></textarea>");

            AssertBorder(style, "1px", "solid", "rgba(118, 118, 118, 1)");
            AssertPadding(style, "2px", "2px");
            Assert.AreEqual("rgba(255, 255, 255, 1)", style.GetPropertyValue("background-color"));
        }

        [TestCase("<input id=target>")]
        [TestCase("<button id=target>Go</button>")]
        [TestCase("<select id=target></select>")]
        [TestCase("<textarea id=target></textarea>")]
        public void ControlsDoNotInheritTheFontOfThePage(string html)
        {
            var style = ComputedStyle(html);

            Assert.AreEqual("13.3333px", style.GetPropertyValue("font-size"));
            StringAssert.StartsWith("Arial", style.GetPropertyValue("font-family"));
        }

        [TestCase("<input id=target>")]
        [TestCase("<button id=target>Go</button>")]
        [TestCase("<select id=target></select>")]
        [TestCase("<textarea id=target></textarea>")]
        public void ControlsDoNotInheritTheLineHeightOfThePage(string html)
        {
            var style = ComputedStyle(html);

            Assert.AreEqual("normal", style.GetPropertyValue("line-height"));
        }

        [Test]
        public void SelectLooksLikeAButtonWithTextPadding()
        {
            var style = ComputedStyle("<select id=target><option>One</option></select>");

            AssertBorder(style, "1px", "solid", "rgba(118, 118, 118, 1)");
            AssertPadding(style, "0px", "0px");
            Assert.AreEqual("border-box", style.GetPropertyValue("box-sizing"));
            Assert.AreEqual("rgba(232, 232, 232, 1)", style.GetPropertyValue("background-color"));
        }

        [TestCase("<button id=target>Go</button>")]
        [TestCase("<input id=target type=button value=Go>")]
        [TestCase("<input id=target type=submit>")]
        [TestCase("<input id=target type=reset>")]
        public void ButtonsHaveABorderWidePaddingAndAGrayBackground(string html)
        {
            var style = ComputedStyle(html);

            AssertBorder(style, "1px", "solid", "rgba(118, 118, 118, 1)");
            AssertPadding(style, "1px", "6px");
            Assert.AreEqual("rgba(232, 232, 232, 1)", style.GetPropertyValue("background-color"));
        }

        [Test]
        public void FileInputIsATransparentBoxWithoutPadding()
        {
            var style = ComputedStyle("<input id=target type=file>");

            Assert.AreEqual("none", style.GetPropertyValue("border-top-style"));
            AssertPadding(style, "0px", "0px");
        }

        [TestCase("<select id=target multiple></select>")]
        [TestCase("<select id=target size=3></select>")]
        public void ListBoxesAreWhite(string html)
        {
            Assert.AreEqual("rgba(255, 255, 255, 1)", ComputedStyle(html).GetPropertyValue("background-color"));
        }

        [TestCase("<progress id=target></progress>")]
        [TestCase("<meter id=target></meter>")]
        public void ProgressAndMeterAreInlineBlocksInheritingTheFont(string html)
        {
            var style = ComputedStyle("<style>body { font-size: 20px }</style>" + html);

            Assert.AreEqual("inline-block", style.GetPropertyValue("display"));
            Assert.AreEqual("border-box", style.GetPropertyValue("box-sizing"));
            Assert.AreEqual("20px", style.GetPropertyValue("font-size"));
        }

        [TestCase("<input id=target>")]
        [TestCase("<input id=target type=submit>")]
        [TestCase("<input id=target type=checkbox>")]
        [TestCase("<button id=target>Go</button>")]
        [TestCase("<select id=target></select>")]
        [TestCase("<textarea id=target></textarea>")]
        public void ControlsDoNotInheritTheTextColorOfThePage(string html)
        {
            var style = ComputedStyle("<style>body { color: rgb(200, 0, 0) }</style>" + html);

            Assert.AreEqual("rgba(0, 0, 0, 1)", style.GetPropertyValue("color"));
        }

        [TestCase("<input id=target type=file>")]
        [TestCase("<progress id=target></progress>")]
        [TestCase("<meter id=target></meter>")]
        public void SomeControlsInheritTheTextColorOfThePage(string html)
        {
            var style = ComputedStyle("<style>body { color: rgb(200, 0, 0) }</style>" + html);

            Assert.AreEqual("rgba(200, 0, 0, 1)", style.GetPropertyValue("color"));
        }

        [Test]
        public void ColorInputHasANarrowPadding()
        {
            var style = ComputedStyle("<input id=target type=color>");

            AssertBorder(style, "1px", "solid", "rgba(118, 118, 118, 1)");
            AssertPadding(style, "1px", "2px");
            Assert.AreEqual("rgba(232, 232, 232, 1)", style.GetPropertyValue("background-color"));
        }

        [TestCase("checkbox")]
        [TestCase("radio")]
        public void CheckboxesAndRadiosHaveABorderAndTheirOwnMargin(string type)
        {
            var style = ComputedStyle($"<input id=target type={type}>");

            AssertBorder(style, "1px", "solid", "rgba(118, 118, 118, 1)");
            AssertPadding(style, "0px", "0px");
            Assert.AreEqual("rgba(255, 255, 255, 1)", style.GetPropertyValue("background-color"));
            Assert.AreEqual("3px", style.GetPropertyValue("margin-top"));
            Assert.AreEqual("3px", style.GetPropertyValue("margin-right"));
            Assert.AreEqual(type == "radio" ? "0px" : "3px", style.GetPropertyValue("margin-bottom"));
            Assert.AreEqual(type == "radio" ? "5px" : "4px", style.GetPropertyValue("margin-left"));
        }

        [Test]
        public void RadioIsRound()
        {
            var style = ComputedStyle("<input id=target type=radio>");
            var radius = style.GetPropertyValue("border-top-left-radius");

            Assert.IsNotEmpty(radius);
            Assert.AreNotEqual("0px", radius);
        }

        [Test]
        public void RangeHasNoBoxOfItsOwn()
        {
            var style = ComputedStyle("<input id=target type=range>");

            Assert.AreEqual("none", style.GetPropertyValue("border-top-style"));
            Assert.AreEqual("rgba(0, 0, 0, 0)", style.GetPropertyValue("background-color"));
        }

        [Test]
        public void AuthorRulesOverrideTheFormControlLook()
        {
            var document = ParseDocument("<head><style>input { border: 3px dashed red; padding: 1px; background-color: black }</style></head><body><input id=target></body>");
            var style = document.GetElementById("target").ComputeCurrentStyle();

            AssertBorder(style, "3px", "dashed", "rgba(255, 0, 0, 1)");
            AssertPadding(style, "1px", "1px");
            Assert.AreEqual("rgba(0, 0, 0, 1)", style.GetPropertyValue("background-color"));
        }
    }
}
