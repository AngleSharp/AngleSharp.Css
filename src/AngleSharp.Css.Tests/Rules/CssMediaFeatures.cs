namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css;
    using AngleSharp.Css.FeatureValidators;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssMediaFeaturesTests 
    {
        [Test]
        public void CssMediaFeatureValidatorFactory()
        {
            var aspectRatio = CreateMediaFeatureValidator(FeatureNames.AspectRatio);
            Assert.IsInstanceOf<AspectRatioFeatureValidator>(aspectRatio);

            var colorIndex = CreateMediaFeatureValidator(FeatureNames.ColorIndex);
            Assert.IsInstanceOf<ColorIndexFeatureValidator>(colorIndex);

            var deviceWidth = CreateMediaFeatureValidator(FeatureNames.DeviceWidth);
            Assert.IsInstanceOf<DeviceWidthFeatureValidator>(deviceWidth);

            var monochrome = CreateMediaFeatureValidator(FeatureNames.MaxMonochrome);
            Assert.IsInstanceOf<MonochromeFeatureValidator>(monochrome);

            var illegal = CreateMediaFeatureValidator("illegal");
            Assert.IsNull(illegal);
        }

        [Test]
        public void CssMediaWidthValidation()
        {
            var width = CreateMediaFeatureValidator(FeatureNames.Width);
            Assert.Inconclusive();
            /*var check = width.TrySetValue(new Length(100, Length.Unit.Px));
            var valid = width.Validate(new RenderDevice(100, 0));
            var invalid = width.Validate(new RenderDevice(0, 0));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);*/
        }

        [Test]
        public void CssMediaMaxHeightValidation()
        {
            var height = CreateMediaFeatureValidator(FeatureNames.MaxHeight);
            Assert.Inconclusive();
            /*var check = height.TrySetValue(new Length(100, Length.Unit.Px));
            var valid = height.Validate(new RenderDevice(0, 99));
            var invalid = height.Validate(new RenderDevice(0, 101));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);*/
        }

        [Test]
        public void CssMediaMinDeviceWidthValidation()
        {
            var devwidth = CreateMediaFeatureValidator(FeatureNames.MinDeviceWidth);
            Assert.Inconclusive();
            /*var check = devwidth.TrySetValue(new Length(100, Length.Unit.Px));
            var valid = devwidth.Validate(new RenderDevice(101, 0));
            var invalid = devwidth.Validate(new RenderDevice(99, 0));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);*/
        }

        [Test]
        public void CssMediaAspectRatio()
        {
            var ratio = CreateMediaFeatureValidator(FeatureNames.AspectRatio);
            Assert.Inconclusive();
            /*var check = ratio.TrySetValue(new CssValueList(new List<ICssValue>(new ICssValue[] {
                new Number(1f, Number.Unit.Integer),
                null, //CssValue.Delimiter,
                new Number(1f, Number.Unit.Integer)
            })));
            var valid = ratio.Validate(new RenderDevice(100, 100));
            var invalid = ratio.Validate(new RenderDevice(16, 9));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);*/
        }
    }
}
