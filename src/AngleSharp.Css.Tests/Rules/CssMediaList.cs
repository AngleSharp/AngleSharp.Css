namespace AngleSharp.Css.Tests.Rules
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssMediaListTests
    {
        [Test]
        public void SimpleScreenMediaList()
        {
            var source = @"@media screen {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("screen"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void MediaListAtIllegal()
        {
            var source = @"@media @screen {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo("not all"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void MediaListInterrupted()
        {
            var source = @"@media screen; h1 { color: green }";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<ICssStyleRule>(sheet.Rules[0]);
            var h1 = (ICssStyleRule)sheet.Rules[0];
            Assert.That(h1.SelectorText, Is.EqualTo("h1"));
            var style = h1.Style;
            Assert.That(style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void SimpleScreenTvMediaList()
        {
            var source = @"@media screen,tv {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("screen, tv"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void SimpleScreenTvSpacesMediaList()
        {
            var source = @"@media              screen ,          tv {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("screen, tv"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void OnlyScreenTvMediaList()
        {
            var source = @"@media only screen,tv {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("only screen, tv"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void NotScreenTvMediaList()
        {
            var source = @"@media not screen,tv {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("not screen, tv"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void FeatureMinWidthMediaList()
        {
            var source = @"@media (min-width:30px) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("(min-width: 30px)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void OnlyFeatureWidthMediaListInvalid()
        {
            var source = @"@media only (width: 640px) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("not all"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void OnlyFeatureWidthScreenAndMediaList()
        {
            var source = @"@media only screen and (width: 640px) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("only screen and (width: 640px)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void NotFeatureDeviceWidthMediaList()
        {
            var source = @"@media not (device-width: 640px) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("not (device-width: 640px)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void AllFeatureMaxWidthMediaListMissingAnd()
        {
            var source = @"@media all (max-width:30px) {
    h1 { color: red }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo("not all"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void NoMediaQueryGivenSkip()
        {
            var source = @"@media {
    h1 { color: red }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo(""));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void NotNoMediaTypeOrExpressionSkip()
        {
            var source = @"@media not {
    h1 { color: red }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo("not all"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void OnlyNoMediaTypeOrExpressionSkip()
        {
            var source = @"@media only {
    h1 { color: red }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo("not all"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void MediaFeatureMissingSkip()
        {
            var source = @"@media () {
    h1 { color: red }
}";

            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo("not all"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void MediaFeatureMissingSkipReadNext()
        {
            var source = @"@media () {
    h1 { color: red }
}
h1 { color: green }";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(2));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            Assert.IsInstanceOf<ICssStyleRule>(sheet.Rules[1]);
            var style = (ICssStyleRule)sheet.Rules[1];
            Assert.That(style.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
            Assert.That(style.SelectorText, Is.EqualTo("h1"));
        }

        [Test]
        public void FeatureMaxWidthMediaListMissingConnectedAnd()
        {
            var source = @"@media (max-width:30px) (min-width:10px) {
    h1 { color: red }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo("not all"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void TvScreenMediaListMissingComma()
        {
            var source = @"@media tv screen {
    h1 { color: red }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.That(media.ConditionText, Is.EqualTo("not all"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void AllFeatureMaxWidthMediaListWithAndKeyword()
        {
            var source = @"@media all and (max-width:30px) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("all and (max-width: 30px)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void FeatureAspectRatioMediaList()
        {
            var source = @"@media (aspect-ratio: 16/9) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("(aspect-ratio: 16/9)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void PrintFeatureMaxWidthAndMinDeviceWidthMediaList()
        {
            var source = @"@media print and (max-width:30px) and (min-device-width:100px) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("print and (max-width: 30px) and (min-device-width: 100px)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void AllFeatureMinWidthAndMinDeviceWidthScreenMediaList()
        {
            var source = @"@media all and (min-width:0) and (min-device-width:100px), screen {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("all and (min-width: 0) and (min-device-width: 100px), screen"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void ImplicitAllFeatureResolutionMediaList()
        {
            var source = @"@media (resolution:72dpi) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("(resolution: 72dpi)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void ImplicitAllFeatureMinResolutionAndMaxResolutionMediaList()
        {
            var source = @"@media (min-resolution:72dpi) and (max-resolution:140dpi) {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("(min-resolution: 72dpi) and (max-resolution: 140dpi)"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void CssMediaListApiWithAppendDeleteAndTextShouldWork()
        {
            var media = new [] { "handheld", "screen", "only screen and (max-device-width: 480px)" };
            var context = BrowsingContext.New(Configuration.Default.WithCss());
		    var list = new MediaList(context);
            Assert.That(list.Length, Is.EqualTo(0));

		    list.Add(media[0]);
		    list.Add(media[1]);
		    list.Add(media[2]);

		    list.Remove(media[1]);

            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(list[0], Is.EqualTo(media[0]));
            Assert.That(list[1], Is.EqualTo(media[2]));
            Assert.That(list.MediaText, Is.EqualTo(String.Concat(media[0], ", ", media[2])));
        }

        [Test]
        public void ReplacesInvalidPartsCommaWithNotAll()
        {
            var source = @"@media (example, all,), speech {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("not all, speech"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void ReplacesInvalidPartsAmpersandWithNotAll()
        {
            var source = @"@media test&, speech {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("not all, speech"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void ReplacesUnclosedParansWithNotAll()
        {
            var source = @"@media  (example, speech {
    h1 { color: green }
}";
            var sheet = ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.That(media.Media.MediaText, Is.EqualTo("not all"));
            var list = media.Media;
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }
    }
}
