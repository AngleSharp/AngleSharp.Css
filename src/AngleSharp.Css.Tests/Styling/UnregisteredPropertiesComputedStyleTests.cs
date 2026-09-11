#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    /// <summary>
    /// A diff between <c>Constants/PropertyNames.cs</c> (455 constants) and every property name
    /// actually registered in <c>Factories/DefaultDeclarationFactory.cs</c> (419 registrations)
    /// turned up 37 gaps. Of those, the 14 covered here also have a public, misleading
    /// <c>Get&lt;X&gt;()</c> accessor in <c>Dom/StyleDeclarationExtensions.cs</c> that promises a
    /// real value but - with no backing <c>Declaration</c> class and no factory registration -
    /// always silently returns an empty string regardless of what was authored, mirroring the
    /// exact <c>text-overflow</c> gap already found and fixed (see
    /// <see cref="TextOverflowComputedStyleTests"/>). Found while auditing AngleSharp.Css for a
    /// downstream renderer after that fix landed.
    ///
    /// The remaining 23 of the 37 gaps are deliberately not covered here: <c>max-zoom</c>/
    /// <c>min-zoom</c>/<c>orientation</c>/<c>scroll-snap-align</c>/<c>user-zoom</c> have no
    /// accessor at all (nothing publicly promises support, so there is no misleading API to fix),
    /// and <c>accelerator</c>/<c>behavior</c>/<c>ime-mode</c>/<c>layout-grid</c> (and its four
    /// sub-properties)/<c>zoom</c>/<c>clip-top</c>/<c>clip-right</c>/<c>clip-bottom</c>/
    /// <c>clip-left</c>/<c>glyph-orientation-horizontal</c>/<c>glyph-orientation-vertical</c> are
    /// legacy Trident-only extensions or deprecated SVG 1.1 properties with no modern-browser
    /// relevance - not worth a registered declaration.
    ///
    /// Each test asserts the value a real declaration would compute, so each currently fails
    /// (the actual computed value is empty) until that property gets a real
    /// <c>XDeclaration.cs</c> registered in <c>DefaultDeclarationFactory</c>, mirroring
    /// <c>TextOverflowDeclaration</c>'s own shape.
    /// </summary>
    [TestFixture]
    public class UnregisteredPropertiesComputedStyleTests
    {
        private static String Compute(String declarationCss, String propertyName)
        {
            var document = ParseDocument($"<div id=target style=\"{declarationCss}\"></div>");
            var target = document.GetElementById("target");
            return target.ComputeCurrentStyle().GetPropertyValue(propertyName);
        }

        [Test]
        public void WritingModeIsRecognizedAndComputed()
        {
            Assert.AreEqual("vertical-rl", Compute("writing-mode: vertical-rl;", "writing-mode"));
        }

        [Test]
        public void ClipPathIsRecognizedAndComputed()
        {
            Assert.AreEqual("circle(50%)", Compute("clip-path: circle(50%);", "clip-path"));
        }

        [Test]
        public void MaskIsRecognizedAndComputed()
        {
            Assert.AreEqual("url(\"#m\")", Compute("mask: url(#m);", "mask"));
        }

        [Test]
        public void FillRuleIsRecognizedAndComputed()
        {
            Assert.AreEqual("evenodd", Compute("fill-rule: evenodd;", "fill-rule"));
        }

        [Test]
        public void FillOpacityIsRecognizedAndComputed()
        {
            Assert.AreEqual("0.5", Compute("fill-opacity: 0.5;", "fill-opacity"));
        }

        [Test]
        public void ClipRuleIsRecognizedAndComputed()
        {
            Assert.AreEqual("evenodd", Compute("clip-rule: evenodd;", "clip-rule"));
        }

        [Test]
        public void MarkerStartIsRecognizedAndComputed()
        {
            Assert.AreEqual("url(\"#a\")", Compute("marker-start: url(#a);", "marker-start"));
        }

        [Test]
        public void MarkerMidIsRecognizedAndComputed()
        {
            Assert.AreEqual("url(\"#a\")", Compute("marker-mid: url(#a);", "marker-mid"));
        }

        [Test]
        public void MarkerEndIsRecognizedAndComputed()
        {
            Assert.AreEqual("url(\"#a\")", Compute("marker-end: url(#a);", "marker-end"));
        }

        [Test]
        public void TextUnderlinePositionIsRecognizedAndComputed()
        {
            Assert.AreEqual("under", Compute("text-underline-position: under;", "text-underline-position"));
        }

        [Test]
        public void BaselineShiftIsRecognizedAndComputed()
        {
            Assert.AreEqual("sub", Compute("baseline-shift: sub;", "baseline-shift"));
        }

        [Test]
        public void DominantBaselineIsRecognizedAndComputed()
        {
            Assert.AreEqual("middle", Compute("dominant-baseline: middle;", "dominant-baseline"));
        }

        [Test]
        public void AlignmentBaselineIsRecognizedAndComputed()
        {
            Assert.AreEqual("middle", Compute("alignment-baseline: middle;", "alignment-baseline"));
        }

        [Test]
        public void ColorInterpolationFiltersIsRecognizedAndComputed()
        {
            Assert.AreEqual("sRGB", Compute("color-interpolation-filters: sRGB;", "color-interpolation-filters"));
        }
    }
}
