namespace AngleSharp.Css.Tests.Styling
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPhase10PropertiesTests
    {
        [Test]
        public void CssCounterSetInitial()
        {
            var snippet = "counter-set: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-set", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssCounterSetNone()
        {
            var snippet = "counter-set: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-set", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssCounterSetSingleCounter()
        {
            var snippet = "counter-set: counter-name";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-set", property.Name);
            Assert.AreEqual("counter-name 0", property.Value);
        }

        [Test]
        public void CssCounterSetMultipleCounters()
        {
            var snippet = "counter-set: chapter section 1 page;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-set", property.Name);
            Assert.AreEqual("chapter 0 section 1 page 0", property.Value);
        }

        [Test]
        public void CssCounterSetNegativeValue()
        {
            var snippet = "counter-set: counter-name -5;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("counter-set", property.Name);
            Assert.AreEqual("counter-name -5", property.Value);
        }

        [Test]
        public void CssImageRenderingInitial()
        {
            var snippet = "image-rendering: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-rendering", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssImageRenderingAuto()
        {
            var snippet = "image-rendering: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-rendering", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssImageRenderingCrispEdges()
        {
            var snippet = "image-rendering: crisp-edges;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-rendering", property.Name);
            Assert.AreEqual("crisp-edges", property.Value);
        }

        [Test]
        public void CssImageRenderingOptimizeSpeed()
        {
            var snippet = "image-rendering: optimize-speed;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-rendering", property.Name);
            Assert.AreEqual("optimize-speed", property.Value);
        }

        [Test]
        public void CssImageRenderingOptimizeQuality()
        {
            var snippet = "image-rendering: optimize-quality;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-rendering", property.Name);
            Assert.AreEqual("optimize-quality", property.Value);
        }

        [Test]
        public void CssImageRenderingInvalid()
        {
            var snippet = "image-rendering: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-rendering", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssImageOrientationInitial()
        {
            var snippet = "image-orientation: from-image;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-orientation", property.Name);
            Assert.AreEqual("from-image", property.Value);
        }

        [Test]
        public void CssImageOrientationFromImage()
        {
            var snippet = "image-orientation: from-image;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-orientation", property.Name);
            Assert.AreEqual("from-image", property.Value);
        }

        [Test]
        public void CssImageOrientationZeroDegrees()
        {
            var snippet = "image-orientation: 0deg;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-orientation", property.Name);
            Assert.AreEqual("0deg", property.Value);
        }

        [Test]
        public void CssImageOrientationNinetyDegrees()
        {
            var snippet = "image-orientation: 90deg;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-orientation", property.Name);
            Assert.AreEqual("90deg", property.Value);
        }

        [Test]
        public void CssImageOrientationOneHundredEightyDegrees()
        {
            var snippet = "image-orientation: 180deg;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-orientation", property.Name);
            Assert.AreEqual("180deg", property.Value);
        }

        [Test]
        public void CssImageOrientationTwoHundredSeventyDegrees()
        {
            var snippet = "image-orientation: 270deg;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-orientation", property.Name);
            Assert.AreEqual("270deg", property.Value);
        }

        [Test]
        public void CssImageOrientationInvalid()
        {
            var snippet = "image-orientation: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("image-orientation", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssViewTransitionNameInitial()
        {
            var snippet = "view-transition-name: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-name", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssViewTransitionNameNone()
        {
            var snippet = "view-transition-name: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-name", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssViewTransitionNameCustom()
        {
            var snippet = "view-transition-name: custom-transition;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-name", property.Name);
            Assert.AreEqual("custom-transition", property.Value);
        }

        [Test]
        public void CssViewTransitionNameMultiple()
        {
            var snippet = "view-transition-name: transition-a, transition-b;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-name", property.Name);
            Assert.AreEqual("transition-a, transition-b", property.Value);
        }

        [Test]
        public void CssViewTransitionNameInvalid()
        {
            var snippet = "view-transition-name: 123;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-name", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssViewTransitionClassInitial()
        {
            var snippet = "view-transition-class: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-class", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssViewTransitionClassNone()
        {
            var snippet = "view-transition-class: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-class", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssViewTransitionClassCustom()
        {
            var snippet = "view-transition-class: my-class;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-class", property.Name);
            Assert.AreEqual("my-class", property.Value);
        }

        [Test]
        public void CssViewTransitionClassMultiple()
        {
            var snippet = "view-transition-class: class-a, class-b;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-class", property.Name);
            Assert.AreEqual("class-a, class-b", property.Value);
        }

        [Test]
        public void CssViewTransitionClassInvalid()
        {
            var snippet = "view-transition-class: 456;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("view-transition-class", property.Name);
            Assert.AreEqual("", property.Value);
        }
    }
}
