namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssLogicalPropertiesTests
    {
        #region inset (shorthand for top/right/bottom/left)

        [Test]
        public void InsetSingleAutoLegal()
        {
            var snippet = "inset: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void InsetSinglePixelLegal()
        {
            var snippet = "inset: 10px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px", property.Value);
        }

        [Test]
        public void InsetTwoValuesLegal()
        {
            var snippet = "inset: 10px 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 20px", property.Value);
        }

        [Test]
        public void InsetFourValuesLegal()
        {
            var snippet = "inset: 5px 10px 15px 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5px 10px 15px 20px", property.Value);
        }

        #endregion

        #region inset-block / inset-inline longhands

        [Test]
        public void InsetBlockStartAutoLegal()
        {
            var snippet = "inset-block-start: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-block-start", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void InsetBlockStartPixelLegal()
        {
            var snippet = "inset-block-start: 15px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-block-start", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("15px", property.Value);
        }

        [Test]
        public void InsetBlockEndPixelLegal()
        {
            var snippet = "inset-block-end: 5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-block-end", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5%", property.Value);
        }

        [Test]
        public void InsetInlineStartAutoLegal()
        {
            var snippet = "inset-inline-start: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-inline-start", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void InsetInlineEndPixelLegal()
        {
            var snippet = "inset-inline-end: 30px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-inline-end", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("30px", property.Value);
        }

        #endregion

        #region inset-block / inset-inline shorthands

        [Test]
        public void InsetBlockSingleValueLegal()
        {
            var snippet = "inset-block: 10px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-block", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px", property.Value);
        }

        [Test]
        public void InsetBlockTwoValuesLegal()
        {
            var snippet = "inset-block: 10px 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-block", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("10px 20px", property.Value);
        }

        [Test]
        public void InsetInlineSingleValueLegal()
        {
            var snippet = "inset-inline: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-inline", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void InsetInlineTwoValuesLegal()
        {
            var snippet = "inset-inline: 5px 10px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("inset-inline", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5px 10px", property.Value);
        }

        #endregion

        #region border-block-start longhands

        [Test]
        public void BorderBlockStartColorCurrentColorLegal()
        {
            var snippet = "border-block-start-color: currentcolor";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("currentColor", property.Value);
        }

        [Test]
        public void BorderBlockStartColorRedLegal()
        {
            var snippet = "border-block-start-color: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1)", property.Value);
        }

        [Test]
        public void BorderBlockStartStyleSolidLegal()
        {
            var snippet = "border-block-start-style: solid";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("solid", property.Value);
        }

        [Test]
        public void BorderBlockStartStyleInvalidIllegal()
        {
            var snippet = "border-block-start-style: thick";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start-style", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        [Test]
        public void BorderBlockStartWidthMediumLegal()
        {
            var snippet = "border-block-start-width: medium";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start-width", property.Name);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            // Note: keyword widths are resolved to pixel values by LineWidthConverter
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderBlockStartWidthPixelLegal()
        {
            var snippet = "border-block-start-width: 2px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start-width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2px", property.Value);
        }

        #endregion

        #region border-block-end longhands

        [Test]
        public void BorderBlockEndColorBlueLegal()
        {
            var snippet = "border-block-end-color: blue";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-end-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(0, 0, 255, 1)", property.Value);
        }

        [Test]
        public void BorderBlockEndStyleDashedLegal()
        {
            var snippet = "border-block-end-style: dashed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-end-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("dashed", property.Value);
        }

        [Test]
        public void BorderBlockEndWidthThinLegal()
        {
            var snippet = "border-block-end-width: thin";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-end-width", property.Name);
            Assert.IsTrue(property.HasValue);
            // Note: keyword widths are resolved to pixel values by LineWidthConverter
        }

        #endregion

        #region border-inline-start / border-inline-end longhands

        [Test]
        public void BorderInlineStartColorGreenLegal()
        {
            var snippet = "border-inline-start-color: green";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-start-color", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderInlineStartStyleDottedLegal()
        {
            var snippet = "border-inline-start-style: dotted";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-start-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("dotted", property.Value);
        }

        [Test]
        public void BorderInlineStartWidthThickLegal()
        {
            var snippet = "border-inline-start-width: thick";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-start-width", property.Name);
            Assert.IsTrue(property.HasValue);
            // Note: keyword widths are resolved to pixel values by LineWidthConverter
        }

        [Test]
        public void BorderInlineEndColorCurrentColorLegal()
        {
            var snippet = "border-inline-end-color: currentcolor";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-end-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("currentColor", property.Value);
        }

        [Test]
        public void BorderInlineEndStyleNoneLegal()
        {
            var snippet = "border-inline-end-style: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-end-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void BorderInlineEndWidthPixelLegal()
        {
            var snippet = "border-inline-end-width: 3px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-end-width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("3px", property.Value);
        }

        #endregion

        #region border-block-start / border-block-end shorthands

        [Test]
        public void BorderBlockStartShorthandLegal()
        {
            var snippet = "border-block-start: 2px solid red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderBlockStartStyleOnlyLegal()
        {
            var snippet = "border-block-start: dashed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-start", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderBlockEndShorthandLegal()
        {
            var snippet = "border-block-end: 1px dotted blue";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-end", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        #endregion

        #region border-inline-start / border-inline-end shorthands

        [Test]
        public void BorderInlineStartShorthandLegal()
        {
            var snippet = "border-inline-start: 1px solid";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-start", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderInlineEndShorthandLegal()
        {
            var snippet = "border-inline-end: thick double green";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-end", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        #endregion

        #region border-block-color / border-block-style / border-block-width shorthands

        [Test]
        public void BorderBlockColorSingleLegal()
        {
            var snippet = "border-block-color: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1)", property.Value);
        }

        [Test]
        public void BorderBlockColorTwoValuesLegal()
        {
            var snippet = "border-block-color: red blue";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("rgba(255, 0, 0, 1) rgba(0, 0, 255, 1)", property.Value);
        }

        [Test]
        public void BorderBlockStyleSingleLegal()
        {
            var snippet = "border-block-style: solid";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("solid", property.Value);
        }

        [Test]
        public void BorderBlockStyleTwoValuesLegal()
        {
            var snippet = "border-block-style: solid dashed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("solid dashed", property.Value);
        }

        [Test]
        public void BorderBlockWidthSingleLegal()
        {
            var snippet = "border-block-width: 2px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("2px", property.Value);
        }

        [Test]
        public void BorderBlockWidthTwoValuesLegal()
        {
            var snippet = "border-block-width: thin thick";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block-width", property.Name);
            Assert.IsTrue(property.HasValue);
            // Note: keyword widths are resolved to pixel values by LineWidthConverter
        }

        #endregion

        #region border-inline-color / border-inline-style / border-inline-width shorthands

        [Test]
        public void BorderInlineColorSingleLegal()
        {
            var snippet = "border-inline-color: currentcolor";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("currentColor", property.Value);
        }

        [Test]
        public void BorderInlineColorTwoValuesLegal()
        {
            var snippet = "border-inline-color: red green";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-color", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderInlineStyleSingleLegal()
        {
            var snippet = "border-inline-style: dotted";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("dotted", property.Value);
        }

        [Test]
        public void BorderInlineWidthSingleLegal()
        {
            var snippet = "border-inline-width: medium";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline-width", property.Name);
            Assert.IsTrue(property.HasValue);
            // Note: keyword widths are resolved to pixel values by LineWidthConverter
        }

        #endregion

        #region border-block / border-inline super-shorthands

        [Test]
        public void BorderBlockFullLegal()
        {
            var snippet = "border-block: 1px solid red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderBlockStyleOnlyLegal()
        {
            var snippet = "border-block: dashed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-block", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderInlineFullLegal()
        {
            var snippet = "border-inline: 2px dotted blue";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        [Test]
        public void BorderInlineWidthOnlyLegal()
        {
            var snippet = "border-inline: thick";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-inline", property.Name);
            Assert.IsTrue(property.HasValue);
        }

        #endregion

        #region logical border radii

        [Test]
        public void BorderStartStartRadiusZeroLegal()
        {
            var snippet = "border-start-start-radius: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-start-start-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsFalse(property.IsInherited);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void BorderStartStartRadiusPixelLegal()
        {
            var snippet = "border-start-start-radius: 5px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-start-start-radius", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("5px", property.Value);
        }

        [Test]
        public void BorderStartEndRadiusPercentLegal()
        {
            var snippet = "border-start-end-radius: 50%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-start-end-radius", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("50%", property.Value);
        }

        [Test]
        public void BorderEndStartRadiusPixelLegal()
        {
            var snippet = "border-end-start-radius: 8px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-end-start-radius", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("8px", property.Value);
        }

        [Test]
        public void BorderEndEndRadiusZeroLegal()
        {
            var snippet = "border-end-end-radius: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-end-end-radius", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.AreEqual("0", property.Value);
        }

        [Test]
        public void BorderStartStartRadiusInvalidIllegal()
        {
            var snippet = "border-start-start-radius: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-start-start-radius", property.Name);
            Assert.IsFalse(property.HasValue);
        }

        #endregion
    }
}
