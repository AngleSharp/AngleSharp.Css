#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class MediaPreferencesTests
    {
        [Test]
        public void MatchMediaPrefersColorSchemeDarkIsMatchedWhenDarkIsPreferred()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Dark));
            Assert.IsTrue(window.MatchMedia("(prefers-color-scheme: dark)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersColorSchemeDarkIsNotMatchedWhenLightIsPreferred()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Light));
            Assert.IsFalse(window.MatchMedia("(prefers-color-scheme: dark)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersColorSchemeDarkIsNotMatchedWithoutAnyPreference()
        {
            var window = CreateWindow(new DefaultRenderDevice());
            Assert.IsFalse(window.MatchMedia("(prefers-color-scheme: dark)").IsMatched);
        }

        [Test]
        public void MatchMediaNotPrefersColorSchemeDarkIsMatchedWhenLightIsPreferred()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Light));
            Assert.IsTrue(window.MatchMedia("not (prefers-color-scheme: dark)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersColorSchemeDarkIsMatchedForAThirdPartyDevice()
        {
            var window = CreateWindow(new PreferringRenderDevice(FeatureNames.PrefersColorScheme, CssKeywords.Dark));
            Assert.IsTrue(window.MatchMedia("(prefers-color-scheme: dark)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersColorSchemeDarkIsNotMatchedForADeviceWithoutPreferences()
        {
            var window = CreateWindow(new PlainRenderDevice());
            Assert.IsFalse(window.MatchMedia("(prefers-color-scheme: dark)").IsMatched);
        }

        [Test]
        public void MatchMediaScreenAndPrefersColorSchemeDarkIsMatchedWhenDarkIsPreferred()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Dark));
            Assert.IsTrue(window.MatchMedia("screen and (prefers-color-scheme: dark)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersColorSchemeInBooleanContextIsMatchedWhenSet()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.PrefersColorScheme, CssKeywords.Dark));
            Assert.IsTrue(window.MatchMedia("(prefers-color-scheme)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersColorSchemeInBooleanContextIsNotMatchedWithoutAnyPreference()
        {
            var window = CreateWindow(new DefaultRenderDevice());
            Assert.IsFalse(window.MatchMedia("(prefers-color-scheme)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersReducedMotionIsMatchedWhenReduceIsPreferred()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.Reduce));
            Assert.IsTrue(window.MatchMedia("(prefers-reduced-motion: reduce)").IsMatched);
            Assert.IsTrue(window.MatchMedia("(prefers-reduced-motion)").IsMatched);
        }

        [Test]
        public void MatchMediaPrefersReducedMotionIsNotMatchedWhenNoPreferenceIsSet()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.NoPreference));
            Assert.IsFalse(window.MatchMedia("(prefers-reduced-motion: reduce)").IsMatched);
            Assert.IsFalse(window.MatchMedia("(prefers-reduced-motion)").IsMatched);
        }

        [Test]
        public void MatchMediaForcedColorsIsMatchedWhenActive()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.ForcedColors, CssKeywords.Active));
            Assert.IsTrue(window.MatchMedia("(forced-colors: active)").IsMatched);
            Assert.IsTrue(window.MatchMedia("(forced-colors)").IsMatched);
        }

        [Test]
        public void MatchMediaHoverIsMatchedFromThePreference()
        {
            var window = CreateWindow(DeviceWith(FeatureNames.Hover, CssKeywords.Hover));
            Assert.IsTrue(window.MatchMedia("(hover: hover)").IsMatched);
            Assert.IsFalse(window.MatchMedia("(hover: none)").IsMatched);
        }

        [Test]
        public void PrefersReducedMotionMediaRuleIsAppliedInTheCascade()
        {
            var document = CreateDocument(DeviceWith(FeatureNames.PrefersReducedMotion, CssKeywords.Reduce));
            var style = document.QuerySelector("div").ComputeCurrentStyle();
            Assert.AreEqual("rgba(0, 128, 0, 1)", style.GetColor());
        }

        [Test]
        public void PrefersReducedMotionMediaRuleIsSkippedWithoutThePreference()
        {
            var document = CreateDocument(new DefaultRenderDevice());
            var style = document.QuerySelector("div").ComputeCurrentStyle();
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetColor());
        }

        [Test]
        public void PrefersReducedMotionMediaRuleIsSkippedForADeviceWithoutPreferences()
        {
            var document = CreateDocument(new PlainRenderDevice());
            var style = document.QuerySelector("div").ComputeCurrentStyle();
            Assert.AreEqual("rgba(255, 0, 0, 1)", style.GetColor());
        }

        private static DefaultRenderDevice DeviceWith(String name, String value) => new DefaultRenderDevice
        {
            Preferences = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase)
            {
                { name, value },
            },
        };

        private static IDocument CreateDocument(IRenderDevice device)
        {
            var source = @"<!doctype html><style>
div { color: red }
@media (prefers-reduced-motion: reduce) { div { color: green } }
</style><div></div>";
            var config = Configuration.Default.WithCss().WithRenderDevice(device);
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument(source);
        }

        private static IWindow CreateWindow(IRenderDevice device)
        {
            var config = Configuration.Default.WithCss().WithRenderDevice(device);
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument("<!doctype html><title>Example</title>");
            return document.DefaultView;
        }
    }
}
