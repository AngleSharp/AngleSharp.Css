namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css;
    using AngleSharp.Css.FeatureValidators;
    using AngleSharp.Css.Tests.Mocks;
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
            var validate = CreateValidator(FeatureNames.Width, "100px");
            var valid = validate(new MockRenderDevice { DeviceWidth = 100, DeviceHeight = 0 });
            var invalid = validate(new MockRenderDevice { DeviceWidth = 0, DeviceHeight = 0 });
            //Assert.IsTrue(valid);
            //Assert.IsFalse(invalid);
        }

        [Test]
        public void CssMediaMaxHeightValidation()
        {
            var validate = CreateValidator(FeatureNames.MaxHeight, "100px");
            var valid = validate(new MockRenderDevice { DeviceWidth = 0, DeviceHeight = 99 });
            var invalid = validate(new MockRenderDevice { DeviceWidth = 0, DeviceHeight = 101 });
            //Assert.IsTrue(valid);
            //Assert.IsFalse(invalid);
        }

        [Test]
        public void CssMediaMinDeviceWidthValidation()
        {
            var validate = CreateValidator(FeatureNames.MinDeviceWidth, "100px");
            var valid = validate(new MockRenderDevice { DeviceWidth = 10, DeviceHeight = 0 });
            var invalid = validate(new MockRenderDevice { DeviceWidth = 99, DeviceHeight = 0 });
            //Assert.IsTrue(valid);
            //Assert.IsFalse(invalid);
        }

        [Test]
        public void CssMediaAspectRatio()
        {
            var validate = CreateValidator(FeatureNames.AspectRatio, "1/1");
            var valid = validate(new MockRenderDevice { DeviceWidth = 100, DeviceHeight = 100 });
            var invalid = validate(new MockRenderDevice { DeviceWidth = 16, DeviceHeight = 9 });
            //Assert.IsTrue(valid);
            //Assert.IsFalse(invalid);
        }
    }
}
