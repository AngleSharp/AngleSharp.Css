#nullable disable
namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css;
    using AngleSharp.Css.FeatureValidators;
    using AngleSharp.Css.Tests.Mocks;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssMediaPreferenceFeaturesTests
    {
        [Test]
        public void CssMediaPreferenceFeatureValidatorFactory()
        {
            Assert.IsInstanceOf<PreferenceFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.PrefersColorScheme));
            Assert.IsInstanceOf<PreferenceFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.PrefersReducedMotion));
            Assert.IsInstanceOf<PreferenceFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.PrefersReducedTransparency));
            Assert.IsInstanceOf<PreferenceFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.PrefersReducedData));
            Assert.IsInstanceOf<PreferenceFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.PrefersContrast));
            Assert.IsInstanceOf<PreferenceFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.ForcedColors));
            Assert.IsInstanceOf<PreferenceFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.DisplayMode));
            Assert.IsInstanceOf<HoverFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.Hover));
            Assert.IsInstanceOf<HoverFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.AnyHover));
            Assert.IsInstanceOf<PointerFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.Pointer));
            Assert.IsInstanceOf<PointerFeatureValidator>(CreateMediaFeatureValidator(FeatureNames.AnyPointer));
        }

        [Test]
        public void CssMediaPrefersColorSchemeValidation()
        {
            var validate = CreateValidator(FeatureNames.PrefersColorScheme, CssKeywords.Dark);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Dark)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Light)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPrefersColorSchemeIsComparedCaseInsensitively()
        {
            var validate = CreateValidator(FeatureNames.PrefersColorScheme, CssKeywords.Dark);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersColorScheme, "DARK")));
        }

        [Test]
        public void CssMediaPrefersColorSchemeIsFoundForAnUppercaseKey()
        {
            var validate = CreateValidator(FeatureNames.PrefersColorScheme, CssKeywords.Dark);
            Assert.IsTrue(validate(DeviceWith("Prefers-Color-Scheme", CssKeywords.Dark)));
        }

        [Test]
        public void CssMediaPrefersColorSchemeInBooleanContext()
        {
            var validate = CreateBooleanValidator(FeatureNames.PrefersColorScheme);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Dark)));
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Light)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.NoPreference)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPrefersReducedMotionValidation()
        {
            var validate = CreateValidator(FeatureNames.PrefersReducedMotion, CssKeywords.Reduce);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.Reduce)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.NoPreference)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPrefersReducedMotionInBooleanContext()
        {
            var validate = CreateBooleanValidator(FeatureNames.PrefersReducedMotion);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.Reduce)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.NoPreference)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPrefersReducedTransparencyValidation()
        {
            var validate = CreateValidator(FeatureNames.PrefersReducedTransparency, CssKeywords.Reduce);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersReducedTransparency, CssKeywords.Reduce)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersReducedTransparency, CssKeywords.NoPreference)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPrefersReducedTransparencyInBooleanContext()
        {
            var validate = CreateBooleanValidator(FeatureNames.PrefersReducedTransparency);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersReducedTransparency, CssKeywords.Reduce)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersReducedTransparency, CssKeywords.NoPreference)));
        }

        [Test]
        public void CssMediaPrefersReducedDataValidation()
        {
            var validate = CreateValidator(FeatureNames.PrefersReducedData, CssKeywords.Reduce);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersReducedData, CssKeywords.Reduce)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersReducedData, CssKeywords.NoPreference)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPrefersReducedDataInBooleanContext()
        {
            var validate = CreateBooleanValidator(FeatureNames.PrefersReducedData);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersReducedData, CssKeywords.Reduce)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersReducedData, CssKeywords.NoPreference)));
        }

        [Test]
        public void CssMediaPrefersContrastValidation()
        {
            var validate = CreateValidator(FeatureNames.PrefersContrast, CssKeywords.More);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersContrast, CssKeywords.More)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersContrast, CssKeywords.Less)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersContrast, CssKeywords.Custom)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPrefersContrastInBooleanContext()
        {
            var validate = CreateBooleanValidator(FeatureNames.PrefersContrast);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersContrast, CssKeywords.More)));
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersContrast, CssKeywords.Less)));
            Assert.IsTrue(validate(DeviceWith(FeatureNames.PrefersContrast, CssKeywords.Custom)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersContrast, CssKeywords.NoPreference)));
        }

        [Test]
        public void CssMediaForcedColorsValidation()
        {
            var validate = CreateValidator(FeatureNames.ForcedColors, CssKeywords.Active);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.ForcedColors, CssKeywords.Active)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.ForcedColors, CssKeywords.None)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaForcedColorsInBooleanContext()
        {
            var validate = CreateBooleanValidator(FeatureNames.ForcedColors);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.ForcedColors, CssKeywords.Active)));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.ForcedColors, CssKeywords.None)));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaDisplayModeValidation()
        {
            var validate = CreateValidator(FeatureNames.DisplayMode, "standalone");
            Assert.IsTrue(validate(DeviceWith(FeatureNames.DisplayMode, "standalone")));
            Assert.IsFalse(validate(DeviceWith(FeatureNames.DisplayMode, "browser")));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaDisplayModeInBooleanContext()
        {
            var validate = CreateBooleanValidator(FeatureNames.DisplayMode);
            Assert.IsTrue(validate(DeviceWith(FeatureNames.DisplayMode, "browser")));
            Assert.IsFalse(validate(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPreferencesAreNotReadFromADeviceWithoutTheInterface()
        {
            var device = new PlainRenderDevice();
            Assert.IsFalse(CreateValidator(FeatureNames.PrefersColorScheme, CssKeywords.Dark)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.PrefersReducedMotion, CssKeywords.Reduce)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.PrefersReducedTransparency, CssKeywords.Reduce)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.PrefersReducedData, CssKeywords.Reduce)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.PrefersContrast, CssKeywords.More)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.ForcedColors, CssKeywords.Active)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.DisplayMode, "browser")(device));
            Assert.IsFalse(CreateBooleanValidator(FeatureNames.PrefersColorScheme)(device));
        }

        [Test]
        public void CssMediaHoverKeepsItsAnswerWithoutAPreference()
        {
            Assert.IsTrue(CreateValidator(FeatureNames.Hover, CssKeywords.None)(new DefaultRenderDevice()));
            Assert.IsFalse(CreateValidator(FeatureNames.Hover, CssKeywords.Hover)(new DefaultRenderDevice()));
            Assert.IsTrue(CreateValidator(FeatureNames.Hover, CssKeywords.None)(new PlainRenderDevice()));
            Assert.IsFalse(CreateBooleanValidator(FeatureNames.Hover)(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaHoverIsTakenFromThePreference()
        {
            var device = DeviceWith(FeatureNames.Hover, CssKeywords.Hover);
            Assert.IsTrue(CreateValidator(FeatureNames.Hover, CssKeywords.Hover)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.Hover, CssKeywords.None)(device));
            Assert.IsTrue(CreateBooleanValidator(FeatureNames.Hover)(device));
            Assert.IsFalse(CreateBooleanValidator(FeatureNames.Hover)(DeviceWith(FeatureNames.Hover, CssKeywords.None)));
        }

        [Test]
        public void CssMediaAnyHoverIsTakenFromThePreference()
        {
            var device = DeviceWith(FeatureNames.AnyHover, CssKeywords.Hover);
            Assert.IsTrue(CreateValidator(FeatureNames.AnyHover, CssKeywords.Hover)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.AnyHover, CssKeywords.None)(device));
            Assert.IsTrue(CreateBooleanValidator(FeatureNames.AnyHover)(device));
            Assert.IsTrue(CreateValidator(FeatureNames.AnyHover, CssKeywords.None)(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPointerKeepsItsAnswerWithoutAPreference()
        {
            Assert.IsTrue(CreateValidator(FeatureNames.Pointer, CssKeywords.None)(new DefaultRenderDevice()));
            Assert.IsFalse(CreateValidator(FeatureNames.Pointer, CssKeywords.Fine)(new DefaultRenderDevice()));
            Assert.IsTrue(CreateValidator(FeatureNames.Pointer, CssKeywords.None)(new PlainRenderDevice()));
            Assert.IsFalse(CreateBooleanValidator(FeatureNames.Pointer)(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPointerIsTakenFromThePreference()
        {
            var device = DeviceWith(FeatureNames.Pointer, CssKeywords.Fine);
            Assert.IsTrue(CreateValidator(FeatureNames.Pointer, CssKeywords.Fine)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.Pointer, CssKeywords.Coarse)(device));
            Assert.IsTrue(CreateBooleanValidator(FeatureNames.Pointer)(device));
            Assert.IsFalse(CreateBooleanValidator(FeatureNames.Pointer)(DeviceWith(FeatureNames.Pointer, CssKeywords.None)));
        }

        [Test]
        public void CssMediaAnyPointerIsTakenFromThePreference()
        {
            var device = DeviceWith(FeatureNames.AnyPointer, CssKeywords.Coarse);
            Assert.IsTrue(CreateValidator(FeatureNames.AnyPointer, CssKeywords.Coarse)(device));
            Assert.IsFalse(CreateValidator(FeatureNames.AnyPointer, CssKeywords.Fine)(device));
            Assert.IsTrue(CreateBooleanValidator(FeatureNames.AnyPointer)(device));
            Assert.IsTrue(CreateValidator(FeatureNames.AnyPointer, CssKeywords.None)(new DefaultRenderDevice()));
        }

        [Test]
        public void CssMediaPreferenceOfAnotherFeatureIsNotUsed()
        {
            var validate = CreateValidator(FeatureNames.PrefersColorScheme, CssKeywords.Dark);
            Assert.IsFalse(validate(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.Dark)));
        }

        private static DefaultRenderDevice DeviceWith(String name, String value) => new DefaultRenderDevice
        {
            Preferences = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase)
            {
                { name, value },
            },
        };
    }
}
