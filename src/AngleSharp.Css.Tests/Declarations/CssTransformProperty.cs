namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssTransformPropertyTests
    {
        [Test]
        public void CssPerspectiveNoneUppercaseLegal()
        {
            var snippet = "perspective:  NONE ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssPerspectiveLengthPixelLegal()
        {
            var snippet = "perspective:  20px  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("20px"));
        }

        [Test]
        public void CssPerspectiveLengthEmLegal()
        {
            var snippet = "perspective:  3.5em  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3.5em"));
        }

        [Test]
        public void CssPerspectiveZeroLegal()
        {
            var snippet = "perspective:  0  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0"));
        }

        [Test]
        public void CssPerspectivePercentIllegal()
        {
            var snippet = "perspective:  10%  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssPerspectiveOriginZeroLegal()
        {
            var snippet = "perspective-origin:  0  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("left"));
        }

        [Test]
        public void CssPerspectiveOriginLengthLegal()
        {
            var snippet = "perspective-origin:  20px  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("20px"));
        }

        [Test]
        public void CssPerspectiveOriginLeftLegal()
        {
            var snippet = "perspective-origin:  left  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("left"));
        }

        [Test]
        public void CssPerspectiveOriginPercentLegal()
        {
            var snippet = "perspective-origin:  15%  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15%"));
        }

        [Test]
        public void CssPerspectiveOriginPercentPercentLegal()
        {
            var snippet = "perspective-origin:  15% 25% ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("15% 25%"));
        }

        [Test]
        public void CssPerspectiveOriginLeftCenterLegal()
        {
            var snippet = "perspective-origin:  left center ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("left"));
        }

        [Test]
        public void CssPerspectiveOriginRightBottomLegal()
        {
            var snippet = "perspective-origin:  right BOTTOM ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("right bottom"));
        }

        [Test]
        public void CssPerspectiveOriginTopCenterLegal()
        {
            var snippet = "perspective-origin:  top center ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("perspective-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("top"));
        }

        [Test]
        public void CssTransformStylePreserve3DLegal()
        {
            var snippet = "transform-style:  preserve-3d ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-style"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("preserve-3d"));
        }

        [Test]
        public void CssTransformStyleNoneIllegal()
        {
            var snippet = "transform-style:  none ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssTransformOriginXOffsetLegal()
        {
            var snippet = "transform-origin:  2px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2px"));
        }

        [Test]
        public void CssTransformOriginXOffsetKeywordLegal()
        {
            var snippet = "transform-origin:  bottom ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("bottom"));
        }

        [Test]
        public void CssTransformOriginYOffsetLegal()
        {
            var snippet = "transform-origin:  3cm 2px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3cm 2px"));
        }

        [Test]
        public void CssTransformOriginYOffsetXKeywordLegal()
        {
            var snippet = "transform-origin:  2px left";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0 2px"));
        }

        [Test]
        public void CssTransformOriginXKeywordYOffsetLegal()
        {
            var snippet = "transform-origin:  left 2px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0 2px"));
        }

        [Test]
        public void CssTransformOriginXKeywordYKeywordLegal()
        {
            var snippet = "transform-origin:  right top ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("right top"));
        }

        [Test]
        public void CssTransformOriginYKeywordXKeywordLegal()
        {
            var snippet = "transform-origin:  top  right ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("right top"));
        }

        [Test]
        public void CssTransformOriginXYZLegal()
        {
            var snippet = "transform-origin:  2px 30% 10px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2px 30% 10px"));
        }

        [Test]
        public void CssTransformOriginYXKeywordZLegal()
        {
            var snippet = "transform-origin:  2px left 10px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0 2px 10px"));
        }

        [Test]
        public void CssTransformOriginXKeywordYZLegal()
        {
            var snippet = "transform-origin:  left 5px -3px ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0 5px -3px"));
        }

        [Test]
        public void CssTransformOriginXKeywordYKeywordZLegal()
        {
            var snippet = "transform-origin:  right bottom 2cm ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("right bottom 2cm"));
        }

        [Test]
        public void CssTransformOriginYKeywordXKeywordZLegal()
        {
            var snippet = "transform-origin:  bottom  right  2cm ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("right bottom 2cm"));
        }

        [Test]
        public void CssTransformNoneLegal()
        {
            var snippet = "transform:  none ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssTransformMatrixLegal()
        {
            var snippet = "transform:  matrix(1.0, 2.0, 3.0, 4.0, 5.0, 6.0) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("matrix(1, 2, 3, 4, 5, 6)"));
        }

        [Test]
        public void CssTransformTranslateLegal()
        {
            var snippet = "transform:  translate(12px, 50%) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("translate(12px, 50%)"));
        }

        [Test]
        public void CssTransformTranslateXLegal()
        {
            var snippet = "transform:  translateX(2em) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("translateX(2em)"));
        }

        [Test]
        public void CssTransformTranslateYLegal()
        {
            var snippet = "transform:  translateY(3in) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("translateY(3in)"));
        }

        [Test]
        public void CssTransformScaleLegal()
        {
            var snippet = "transform:  scale(2, 0.5) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("scale(2, 0.5)"));
        }

        [Test]
        public void CssTransformScaleXLegal()
        {
            var snippet = "transform:  scaleX(0.1) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("scaleX(0.1)"));
        }

        [Test]
        public void CssTransformScaleYLegal()
        {
            var snippet = "transform:  scaleY(1.5) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("scaleY(1.5)"));
        }

        [Test]
        public void CssTransformRotateLegal()
        {
            var snippet = "transform:  rotate(0.5turn) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rotate(0.5turn)"));
        }

        [Test]
        public void CssTransformSkewXLegal()
        {
            var snippet = "transform:  skewX(  30deg  ) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("skewX(30deg)"));
        }

        [Test]
        public void CssTransformSkewYLegal()
        {
            var snippet = "transform:  skewY(  1.07rad  ) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("skewY(1.07rad)"));
        }

        [Test]
        public void CssTransformSkewLegal_Issue101()
        {
            var snippet = "transform:  skew(20deg) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("skew(20deg)"));
        }

        [Test]
        public void CssTransformMultipleLegal()
        {
            var snippet = "transform:  translate(50%, 50%) rotate(45deg) scale(1.5)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("translate(50%, 50%) rotate(45deg) scale(1.5)"));
        }

        [Test]
        public void CssTransformMatrix3dLegal()
        {
            var snippet = "transform:  matrix3d(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssTransformTranslate3dLegal()
        {
            var snippet = "transform:  translate3d(12px, 50%, 3em)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssTransformTranslateZLegal()
        {
            var snippet = "transform:  translateZ(2px)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssTransformScale3dLegal()
        {
            var snippet = "transform:  scale3d(2.5, 1.2, 0.3)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssTransformScaleZLegal()
        {
            var snippet = "transform:  scaleZ(0.3)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssTransformRotate3dLegal()
        {
            var snippet = "transform:  rotate3d(1, 2.0, 3.0, 10deg)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }

        [Test]
        public void CssTransformRotateXLegal()
        {
            var snippet = "transform:  rotateX(10deg)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That("rotateX(10deg)", Is.EqualTo(property.Value));
        }

        [Test]
        public void CssTransformRotateYLegal()
        {
            var snippet = "transform:  rotateY(10deg)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That("rotateY(10deg)", Is.EqualTo(property.Value));
        }

        [Test]
        public void CssTransformRotateZLegal()
        {
            var snippet = "transform: rotateZ(10deg)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That("rotateZ(10deg)", Is.EqualTo(property.Value));
        }

        [Test]
        public void CssTransformPerspectiveLegal()
        {
            var snippet = "transform: perspective(17px)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("transform"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
        }
    }
}
