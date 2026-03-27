namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssBackgroundPropertyTests
    {
        [Test]
        public void CssBackgroundSizeCoverTest()
        {
            var snippet = "background-size : cover";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("cover"));
        }

        [Test]
        public void CssBackgroundSizeContainTest()
        {
            var snippet = "background-size : contain";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("contain"));
        }

        [Test]
        public void CssBackgroundAttachmentScrollLegal()
        {
            var snippet = "background-attachment : scroll";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-attachment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("scroll"));
        }

        [Test]
        public void CssBackgroundAttachmentInitialLegal()
        {
            var snippet = "background-attachment : initial";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-attachment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("initial"));
        }

        [Test]
        public void CssBackgroundAttachmentFixedUppercaseLegal()
        {
            var snippet = "background-attachment : Fixed ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-attachment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fixed"));
        }

        [Test]
        public void CssBackgroundAttachmentFixedLocalLegal()
        {
            var snippet = "background-attachment : fixed  ,  local ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-attachment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fixed, local"));
        }

        [Test]
        public void CssBackgroundAttachmentFixedLocalScrollScrollLegal()
        {
            var snippet = "background-attachment : fixed  ,  local,scroll,scroll ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-attachment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("fixed, local, scroll, scroll"));
        }

        [Test]
        public void CssBackgroundAttachmentNoneIllegal()
        {
            var snippet = "background-attachment : none ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-attachment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBackgroundClipPaddingBoxUppercaseLegal()
        {
            var snippet = "background-clip : Padding-Box ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("padding-box"));
        }

        [Test]
        public void CssBackgroundClipPaddingBoxBorderBoxLegal()
        {
            var snippet = "background-clip : Padding-Box, border-box ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("padding-box, border-box"));
        }

        [Test]
        public void CssBackgroundClipContentBoxLegal()
        {
            var snippet = "background-clip : content-box";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-clip"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("content-box"));
        }

        [Test]
        public void CssBackgroundColorTealLegal()
        {
            var snippet = "background-color : teal";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 128, 128, 1)"));
        }

        [Test]
        public void CssBackgroundColorRgbLegal()
        {
            var snippet = "background-color : rgb(255  ,  255  ,  128)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 255, 128, 1)"));
        }

        [Test]
        public void CssBackgroundColorHslaLegal()
        {
            var snippet = "background-color : hsla(50, 33%, 25%, 0.75)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(85, 78, 42, 0.75)"));
        }

        [Test]
        public void CssBackgroundColorTransparentLegal()
        {
            var snippet = "background-color : Transparent";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 0, 0, 0)"));
        }

        [Test]
        public void CssBackgroundColorHexLegal()
        {
            var snippet = "background-color : #bbff00";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(187, 255, 0, 1)"));
        }

        [Test]
        public void CssBackgroundColorMultipleIllegal()
        {
            var snippet = "background-color : #bbff00, transparent, red, #ff00ff";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBackgroundImageNoneLegal()
        {
            var snippet = "background-image: NONE";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssBackgroundImageUrlAndNoneLegal()
        {
            var snippet = "background-image: url(\"img/sprites.svg?v=1bc768be1b3c\"),none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"img/sprites.svg?v=1bc768be1b3c\"), none"));
        }

        [Test]
        public void CssBackgroundImageUrlLegal()
        {
            var snippet = "background-image: url(image.png)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\")"));
        }

        [Test]
        public void CssBackgroundImageUrlAbsoluteLegal()
        {
            var snippet = "background-image: url(http://www.example.com/images/bck.png)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"http://www.example.com/images/bck.png\")"));
        }

        [Test]
        public void CssBackgroundImageUrlsLegal()
        {
            var snippet = "background-image: url(image.png),url('bla.png')";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\"), url(\"bla.png\")"));
        }

        [Test]
        public void CssBackgroundImageUrlNoneUrlLegal()
        {
            var snippet = "background-image: url(image.png),none, url(foo.gif)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"image.png\"), none, url(\"foo.gif\")"));
        }

        [Test]
        public void CssBackgroundOriginContentBoxLegal()
        {
            var snippet = "background-origin: CONTENT-BOX";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("content-box"));
        }

        [Test]
        public void CssBackgroundOriginContentBoxPaddingBoxLegal()
        {
            var snippet = "background-origin: CONTENT-BOX, Padding-Box";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("content-box, padding-box"));
        }

        [Test]
        public void CssBackgroundOriginBorderBoxLegal()
        {
            var snippet = "background-origin: border-box";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-origin"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("border-box"));
        }

        [Test]
        public void CssBackgroundPositionTopLegal()
        {
            var snippet = "background-position: top";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("top"));
        }

        [Test]
        public void CssBackgroundPositionPercentPercentLegal()
        {
            var snippet = "background-position: 25% 75%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("25% 75%"));
        }

        [Test]
        public void CssBackgroundPositionCenterPercentLegal()
        {
            var snippet = "background-position: center 75%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("50% 75%"));
        }

        [Test]
        public void CssBackgroundPositionRightLengthBottomLengthIllegal()
        {
            var snippet = "background-position: right 20px bottom 20px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBackgroundPositionLengthLengthCenterMultipleLegal()
        {
            var snippet = "background-position: 10px 20px, center";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("10px 20px, center"));
        }

        [Test]
        public void CssBackgroundPositionZeroMultipleLegal()
        {
            var snippet = "background-position: 0 0, 0 0";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("left top, left top"));
        }

        [Test]
        public void CssBackgroundRepeatRepeatXLegal()
        {
            var snippet = "background-repeat: repeat-x";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat-x"));
        }

        [Test]
        public void CssBackgroundRepeatRepeatYLegal()
        {
            var snippet = "background-repeat: repeat-y";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat-y"));
        }

        [Test]
        public void CssBackgroundRepeatRepeatLegal()
        {
            var snippet = "background-repeat: REPEAT";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat"));
        }

        [Test]
        public void CssBackgroundRepeatRoundLegal()
        {
            var snippet = "background-repeat: rounD";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("round"));
        }

        [Test]
        public void CssBackgroundRepeatRepeatSpaceLegal()
        {
            var snippet = "background-repeat: repeat space";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat space"));
        }

        [Test]
        public void CssBackgroundRepeatRepeatXSpaceIllegal()
        {
            var snippet = "background-repeat: repeat-x space";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBackgroundRepeatRepeatXRepeatYMultipleLegal()
        {
            var snippet = "background-repeat: repeat-X, repeat-Y";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat-x, repeat-y"));
        }

        [Test]
        public void CssBackgroundRepeatSpaceRoundLegal()
        {
            var snippet = "background-repeat: space round";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("space round"));
        }

        [Test]
        public void CssBackgroundRepeatNoRepeatRepeatXIllegal()
        {
            var snippet = "background-repeat: no-repeat repeat-x";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssBackgroundRepeatRepeatRepeatNoRepeatRepeatLegal()
        {
            var snippet = "background-repeat: repeat repeat, no-repeat repeat";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-repeat"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("repeat, repeat-y"));
        }

        [Test]
        public void CssBackgroundSizeLengthLegal()
        {
            var snippet = "background-size: 2em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("2em"));
        }

        [Test]
        public void CssBackgroundSizePercentLegal()
        {
            var snippet = "background-size: 20%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("20%"));
        }

        [Test]
        public void CssBackgroundSizeAutoAutoLegal()
        {
            var snippet = "background-size: auto auto";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssBackgroundSizeAutoLengthLegal()
        {
            var snippet = "background-size: auto 50px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto 50px"));
        }

        [Test]
        public void CssBackgroundSizeLengthLengthLegal()
        {
            var snippet = "background-size: 25px 50px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("25px 50px"));
        }

        [Test]
        public void CssBackgroundSizePercentPercentLegal()
        {
            var snippet = "background-size: 50% 50%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("50% 50%"));
        }

        [Test]
        public void CssBackgroundSizeAutoUppercaseLegal()
        {
            var snippet = "background-size: AUTO";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("auto"));
        }

        [Test]
        public void CssBackgroundSizeCoverLegal()
        {
            var snippet = "background-size: cover";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("cover"));
        }

        [Test]
        public void CssBackgroundSizeContainCoverMultipleLegal()
        {
            var snippet = "background-size: contain,cover";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("contain, cover"));
        }

        [Test]
        public void CssBackgroundSizeContainLengthAutoPercentLegal()
        {
            var snippet = "background-size: contain,100px,auto,20%";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-size"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("contain, 100px, auto, 20%"));
        }

        [Test]
        public void CssBackgroundRedLegal()
        {
            var snippet = "background: red";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssBackgroundNoneLegal()
        {
            var snippet = "background: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssBackgroundNoneColoredLegal()
        {
            var snippet = "background: none rgb(1, 2, 3)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none rgba(1, 2, 3, 1)"));
        }

        [Test]
        public void CssBackgroundWhiteImageLegal()
        {
            var snippet = "background: white url(\"pendant.png\");";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"pendant.png\") rgba(255, 255, 255, 1)"));
        }

        [Test]
        public void CssBackgroundImageLegal()
        {
            var snippet = "background: url(\"topbanner.png\") #00d repeat-y fixed";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"topbanner.png\") repeat-y fixed rgba(0, 0, 221, 1)"));
        }

        [Test]
        public void CssBackgroundWithoutColorLegal()
        {
            var snippet = "background: url(\"img_tree.png\") no-repeat right top";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"img_tree.png\") right top no-repeat"));
        }

        [Test]
        public void CssBackgroundImageDataUrlLegal()
        {
            var url = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC";
            var snippet = "background-image: url('" + url + "')";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"" + url + "\")"));
        }

        [Test]
        public void CssBackgroundImageLinearGradientLegal()
        {
            var source = "background-image: linear-gradient(to right, rgba(255, 0, 0, 1), rgba(0, 0, 255, 1))";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);

            var expected = "linear-gradient(90deg, rgba(255, 0, 0, 1), rgba(0, 0, 255, 1))";
            Assert.That(property.Value, Is.EqualTo(expected));
        }

        [Test]
        public void CssBackgroundImageNotParsed_Issue66()
        {
            var source = "background-image: linear-gradient(top,#FFFFFF,#FFFFFF,#f8f8f8,#eeeeee)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);

            var expected = "linear-gradient(0deg, rgba(255, 255, 255, 1), rgba(255, 255, 255, 1), rgba(248, 248, 248, 1), rgba(238, 238, 238, 1))";
            Assert.That(property.Value, Is.EqualTo(expected));
        }

        [Test]
        public void CssBackgroundPositionSlashSizeLegal()
        {
            var snippet = "background: center / cover";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("background"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("center / cover"));
        }
    }
}

