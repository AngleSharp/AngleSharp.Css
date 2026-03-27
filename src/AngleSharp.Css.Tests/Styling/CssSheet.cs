namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using NUnit.Framework;
    using System;
    using System.IO;
    using static CssConstructionFunctions;
    using static ValueConverters;

    [TestFixture]
    public class CssSheetTests
    {
        [Test]
        public void CssSheetOnEofDuringRuleWithoutSemicolon()
        {
            var sheet = ParseStyleSheet(@"
h1 {
 color: red;
 font-weight: bold");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.That(h1.SelectorText, Is.EqualTo("h1"));
            Assert.That(h1.Style.GetColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(h1.Style.GetFontWeight(), Is.EqualTo("bold"));
        }

        [Test]
        public void CssSheet1WithDoubleMarkedCommentFromIssue93()
        {
            var sheet = ParseStyleSheet(@"
            /**special css**/
            .dis-none { display: none;}
            .dis { display: block; }
            /*common css*/
            .dis2 { display: block; }
            ");
            var css = sheet.ToCss();
            Assert.That(sheet.Rules.Length, Is.EqualTo(3));
            Assert.That(sheet.Rules[0].CssText, Is.EqualTo(".dis-none { display: none }"));
            Assert.That(sheet.Rules[1].CssText, Is.EqualTo(".dis { display: block }"));
            Assert.That(sheet.Rules[2].CssText, Is.EqualTo(".dis2 { display: block }"));
        }

        [Test]
        public void CssSheet2WithDoubleMarkedCommentFromIssue93()
        {
            var sheet = ParseStyleSheet(@"
            /**special css**/
            .dis-none { display: none;}
            .dis { display: block; }
            ");
            var css = sheet.ToCss();
            Assert.That(sheet.Rules.Length, Is.EqualTo(2));
            Assert.That(sheet.Rules[0].CssText, Is.EqualTo(".dis-none { display: none }"));
            Assert.That(sheet.Rules[1].CssText, Is.EqualTo(".dis { display: block }"));
        }

        [Test]
        public void CssSheetSerializeListStyleNone()
        {
            var cssSrc = ".T1 {list-style:NONE}";
            var expected = ".T1 { list-style: none }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.ToCss();
            Assert.That(cssText, Is.EqualTo(expected));
        }

        [Test]
        public void CssSheetSerializeBorder1pxOutset()
        {
            var cssSrc = ".T2 { border:1px  outset }";
            var expected = ".T2 { border: 1px outset }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.ToCss();
            Assert.That(cssText, Is.EqualTo(expected));
        }

        [Test]
        public void CssSheetSerializeBorder1pxSolidWithColor()
        {
            var cssSrc = "#rule1 { border: 1px solid #BBCCEB; border-top: none }";
            var expected = "#rule1 { border-top: none; border-right: 1px solid rgba(187, 204, 235, 1); border-bottom: 1px solid rgba(187, 204, 235, 1); border-left: 1px solid rgba(187, 204, 235, 1) }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.ToCss();
            Assert.That(cssText, Is.EqualTo(expected));
        }

        [Test]
        public void CssSheetSerializeBackgroundWithUrlPositionRepeatX()
        {
            var cssSrc = "#rule2 { background:url(/_static/img/bx_tile.gif) top left repeat-x; }";
            var expected = "#rule2 { background: url(\"/_static/img/bx_tile.gif\") left top repeat-x }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.ToCss();
            Assert.That(cssText, Is.EqualTo(expected));
        }

        [Test]
        public void CssSheetIgnoreVendorPrefixes()
        {
            var css = @".something {
  -o-border-radius: 5px;
  -webkit-border-radius: 5px;
  border-radius: 5px;
  display: -webkit-box;
  display: -webkit-flex;
  display: -ms-flexbox;
  display: flex;
  background: -webkit-linear-gradient(red, green);
  background: linear-gradient(red, green);
}";
            var stylesheet = ParseStyleSheet(css);
            Assert.That(stylesheet.Rules.Length, Is.EqualTo(1));
            var style = stylesheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.That(style.Style.Length, Is.EqualTo(15));
        }

        [Test]
        public void CssSheetSimpleStyleRuleStringification()
        {
            var css = @"html { font-family: sans-serif }";
            var stylesheet = ParseStyleSheet(css);
            Assert.That(stylesheet.Rules.Length, Is.EqualTo(1));
            var rule = stylesheet.Rules[0];
            Assert.IsInstanceOf<CssStyleRule>(rule);
            Assert.That(rule.CssText, Is.EqualTo(css));
        }

        [Test]
        public void CssSheetCloseStringsEndOfLine()
        {
            var sheet = ParseStyleSheet(@"p {
        color: green;
        font-family: 'Courier New Times
        color: red;
        color: green;
      }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.That(p.Style.Length, Is.EqualTo(1));
            Assert.That(p.SelectorText, Is.EqualTo("p"));
            Assert.That(p.Style[0], Is.EqualTo("color"));
            Assert.That(p.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
            Assert.That(p.Style.GetFontFamily(), Is.EqualTo(""));
        }

        [Test]
        public void CssSheetOnEofDuringRuleWithinString()
        {
            var sheet = ParseStyleSheet(@"
#something {
 content: 'hi there");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var id = sheet.Rules[0] as ICssStyleRule;
            Assert.That(id.SelectorText, Is.EqualTo("#something"));
            Assert.That(id.Style.GetContent(), Is.EqualTo("\"hi there\""));
        }

        [Test]
        public void CssSheetOnEofDuringAtMediaRuleWithinString()
        {
            var sheet = ParseStyleSheet(@"  @media screen {
    p:before { content: 'Hello");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = sheet.Rules[0] as CssMediaRule;
            Assert.That(media.Media.MediaText, Is.EqualTo("screen"));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(media.Rules[0]);
            var p = media.Rules[0] as ICssStyleRule;
            Assert.That(p.SelectorText, Is.EqualTo("p:before"));
            Assert.That(p.Style.GetContent(), Is.EqualTo("\"Hello\""));
        }

        [Test]
        public void CssSheetDoIgnoreUnknownPropertyByDefault()
        {
            var sheet = ParseStyleSheet(@"h1 { color: red; rotation: 70minutes }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.That(h1.SelectorText, Is.EqualTo("h1"));
            Assert.That(h1.Style.Length, Is.EqualTo(1));
            Assert.That(h1.Style[0], Is.EqualTo("color"));
            Assert.That(h1.Style.GetColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssSheetNotIgnoreUnknownPropertyViaOptions()
        {
            var sheet = ParseStyleSheet(@"h1 { color: red; rotation: 70minutes }", new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
            });
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.That(h1.SelectorText, Is.EqualTo("h1"));
            Assert.That(h1.Style.Length, Is.EqualTo(2));
            Assert.That(h1.Style[0], Is.EqualTo("color"));
            Assert.That(h1.Style.GetColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(h1.Style[1], Is.EqualTo("rotation"));
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedAtKeyword()
        {
            var sheet = ParseStyleSheet(@"p @here {color: red}");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That((sheet.Rules[0] as ICssStyleRule).SelectorText, Is.EqualTo(null));
        }

        [Test]
        public void CssSheetInvalidStatementAtRuleUnexpectedAtKeyword()
        {
            var sheet = ParseStyleSheet(@"@foo @bar;");
            Assert.That(sheet.Rules.Length, Is.EqualTo(0));
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightBrace()
        {
            var sheet = ParseStyleSheet(@"}} {{ - }}");
            Assert.That(sheet.Rules.Length, Is.EqualTo(0));
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightBraceWithValidQualifiedRule()
        {
            var sheet = ParseStyleSheet(@"}} {{ - }}
#hi { color: green; }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.NotNull(style);
            Assert.That(style.SelectorText, Is.EqualTo("#hi"));
            Assert.That(style.Style.Length, Is.EqualTo(1));
            Assert.That(style.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightParenthesis()
        {
            var sheet = ParseStyleSheet(@") ( {} ) p {color: red }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(0));
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightParenthesisWithValidQualifiedRule()
        {
            var sheet = ParseStyleSheet(@") {} p {color: green }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.NotNull(style);
            Assert.That(style.SelectorText, Is.EqualTo("p"));
            Assert.That(style.Style.Length, Is.EqualTo(1));
            Assert.That(style.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssSheetIgnoreUnknownAtRule()
        {
            var sheet = ParseStyleSheet(@"@three-dee {
  @background-lighting {
    azimuth: 30deg;
    elevation: 190deg;
  }
  h1 { color: red }
}
h1 { color: blue }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.That(h1.SelectorText, Is.EqualTo("h1"));
            Assert.That(h1.Style.Length, Is.EqualTo(1));
            Assert.That(h1.Style[0], Is.EqualTo("color"));
            Assert.That(h1.Style.GetColor(), Is.EqualTo("rgba(0, 0, 255, 1)"));
        }

        [Test]
        public void CssSheetKeepValidValueFloat()
        {
            var sheet = ParseStyleSheet(@"img { float: left }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.That(img.SelectorText, Is.EqualTo("img"));
            Assert.That(img.Style.Length, Is.EqualTo(1));
            Assert.That(img.Style[0], Is.EqualTo("float"));
            Assert.That(img.Style.GetFloat(), Is.EqualTo("left"));
        }

        [Test]
        public void CssSheetIgnoreInvalidValueFloat()
        {
            var sheet = ParseStyleSheet(@"img { float: left here }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.That(img.SelectorText, Is.EqualTo("img"));
            Assert.That(img.Style.Length, Is.EqualTo(0));
            Assert.That(img.Style.GetFloat(), Is.EqualTo(""));
        }

        [Test]
        public void CssSheetIgnoreInvalidValueBackground()
        {
            var sheet = ParseStyleSheet(@"img { background: ""red"" }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.That(img.SelectorText, Is.EqualTo("img"));
            Assert.That(img.Style.Length, Is.EqualTo(0));
            Assert.That(img.Style.GetBackground(), Is.EqualTo(""));
        }

        [Test]
        public void CssSheetIgnoreInvalidValueBorderWidth()
        {
            var sheet = ParseStyleSheet(@"img { border-width: 3 }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.That(img.SelectorText, Is.EqualTo("img"));
            Assert.That(img.Style.Length, Is.EqualTo(0));
        }

        [Test]
        public void CssSheetWellformedDeclaration()
        {
            var sheet = ParseStyleSheet(@"p { color:green; }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.That(p.SelectorText, Is.EqualTo("p"));
            Assert.That(p.Style.Length, Is.EqualTo(1));
            Assert.That(p.Style[0], Is.EqualTo("color"));
            Assert.That(p.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssSheetMalformedDeclarationMissingColon()
        {
            var sheet = ParseStyleSheet(@"p { color:green; color }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.That(p.SelectorText, Is.EqualTo("p"));
            Assert.That(p.Style.Length, Is.EqualTo(1));
            Assert.That(p.Style[0], Is.EqualTo("color"));
            Assert.That(p.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssSheetMalformedDeclarationMissingColonWithRecovery()
        {
            var sheet = ParseStyleSheet(@"p { color:red;   color; color:green }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.That(p.SelectorText, Is.EqualTo("p"));
            Assert.That(p.Style.Length, Is.EqualTo(1));
            Assert.That(p.Style[0], Is.EqualTo("color"));
            Assert.That(p.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssSheetMalformedDeclarationMissingValue()
        {
            var sheet = ParseStyleSheet(@"p { color:green; color: }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.That(p.SelectorText, Is.EqualTo("p"));
            Assert.That(p.Style.Length, Is.EqualTo(1));
            Assert.That(p.Style[0], Is.EqualTo("color"));
            Assert.That(p.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssSheetMalformedDeclarationUnexpectedTokens()
        {
            var sheet = ParseStyleSheet(@"p { color:green; color{;color:maroon} }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.That(p.SelectorText, Is.EqualTo("p"));
            Assert.That(p.Style.Length, Is.EqualTo(1));
            Assert.That(p.Style[0], Is.EqualTo("color"));
            Assert.That(p.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssSheetMalformedDeclarationUnexpectedTokensWithRecovery()
        {
            var sheet = ParseStyleSheet(@"p { color:red;   color{;color:maroon}; color:green }");
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.That(p.SelectorText, Is.EqualTo("p"));
            Assert.That(p.Style.Length, Is.EqualTo(1));
            Assert.That(p.Style[0], Is.EqualTo("color"));
            Assert.That(p.Style.GetColor(), Is.EqualTo("rgba(0, 128, 0, 1)"));
        }

        [Test]
        public void CssCreateValueListConformal()
        {
            var valueString = "24px 12px 6px";
            var converter = LengthConverter.Periodic();
            var value = converter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssCreateValueListNonConformal()
        {
            var valueString = "  24px  12px 6px  13px ";
            var converter = LengthConverter.Periodic();
            var value = converter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssCreateValueListEmpty()
        {
            var valueString = "";
            var value = FontFamiliesConverter.Convert(valueString);
            Assert.IsNull(value);
        }

        [Test]
        public void CssCreateValueListSpaces()
        {
            var valueString = "  ";
            var value = FontFamiliesConverter.Convert(valueString);
            Assert.IsNull(value);
        }

        [Test]
        public void CssCreateValueListIllegal()
        {
            var valueString = " , ";
            var value = FontFamiliesConverter.Convert(valueString);
            Assert.IsNull(value);
        }

        [Test]
        public void CssCreateMultipleValues()
        {
            var valueString = "Arial, Verdana, Helvetica, Sans-Serif";
            var value = FontFamiliesConverter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssCreateMultipleValuesNonConformal()
        {
            var valueString = "  Arial  ,  Verdana  ,Helvetica,Sans-Serif   ";
            var value = FontFamiliesConverter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorBlack()
        {
            var valueString = "#000000";
            var value = ColorConverter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorRed()
        {
            var valueString = "#FF0000";
            var value = ColorConverter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorMixedShort()
        {
            var valueString = "#07C";
            var value = ColorConverter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorGreenShort()
        {
            var valueString = "#00F";
            var value = ColorConverter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorRedShort()
        {
            var valueString = "#F00";
            var value = ColorConverter.Convert(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssRgbaFunction()
        {
            var names = new[] { "border-top-color", "border-right-color", "border-bottom-color", "border-left-color" };
            var decls = ParseDeclarations("border-color: rgba(82, 168, 236, 0.8)");
            Assert.IsNotNull(decls);
            Assert.That(decls.Length, Is.EqualTo(4));

            for (int i = 0; i < decls.Length; i++)
            {
                var propertyName = decls[i];
                var property = decls.GetProperty(propertyName);
                Assert.That(property.Name, Is.EqualTo(names[i]));
                Assert.That(property.Name, Is.EqualTo(propertyName));
                Assert.That(property.IsImportant, Is.False);
                Assert.That(property.Value, Is.EqualTo("rgba(82, 168, 236, 0.8)"));
            }
        }

        [Test]
        public void CssMarginAll()
        {
            var names = new[] { "margin-top", "margin-right", "margin-bottom", "margin-left" };
            var decls = ParseDeclarations("margin: 20px;");
            Assert.IsNotNull(decls);
            Assert.That(decls.Length, Is.EqualTo(4));

            for (int i = 0; i < decls.Length; i++)
            {
                var propertyName = decls[i];
                var decl = decls.GetProperty(propertyName);
                Assert.That(decl.Name, Is.EqualTo(names[i]));
                Assert.That(decl.Name, Is.EqualTo(propertyName));
                Assert.That(decl.IsImportant, Is.False);
                Assert.That(decl.Value, Is.EqualTo("20px"));
            }
        }

        [Test]
        public void CssSeveralFontFamily()
        {
            var prop = ParseDeclaration("font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif");
            Assert.That(prop.Name, Is.EqualTo("font-family"));
            Assert.That(prop.IsImportant, Is.False);
            Assert.That(prop.Value, Is.EqualTo("\"Helvetica Neue\", Helvetica, Arial, sans-serif"));
        }

        [Test]
        public void CssFontWithSlashAndContent()
        {
            var decl = ParseDeclarations("font: bold 1em/2em monospace; content: \" (\" attr(href) \")\"");
            Assert.IsNotNull(decl);
            Assert.That(decl.Length, Is.EqualTo(8));

            Assert.That(decl.GetPropertyValue("font"), Is.EqualTo("bold 1em / 2em monospace"));

            var content = decl.GetProperty("content");
            Assert.That(content.Name, Is.EqualTo("content"));
            Assert.That(content.IsImportant, Is.False);
            Assert.That(content.Value, Is.EqualTo("\" (\" attr(href) \")\""));
        }

        [Test]
        public void CssBackgroundWebkitGradientIsInvalid()
        {
            var background = ParseDeclaration("background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #FFA84C), color-stop(100%, #FF7B0D))");
            Assert.That(background.HasValue, Is.False);
        }

        [Test]
        public void CssBackgroundColorRgba()
        {
            var background = ParseDeclaration("background-color: rgba(255, 123, 13, 1)");
            Assert.That(background.Name, Is.EqualTo("background-color"));
            Assert.That(background.IsImportant, Is.False);
            Assert.That(background.Value, Is.EqualTo("rgba(255, 123, 13, 1)"));
        }

        [Test]
        public void CssFontWithFraction()
        {
            var font = ParseDeclaration("font:bold 40px/1.13 'PT Sans Narrow', sans-serif");
            Assert.That(font.Name, Is.EqualTo("font"));
            Assert.That(font.IsImportant, Is.False);
        }

        [Test]
        public void CssTextShadow()
        {
            var textShadow = ParseDeclaration("text-shadow: 0 0 10px #000");
            Assert.That(textShadow.Name, Is.EqualTo("text-shadow"));
            Assert.That(textShadow.IsImportant, Is.False);
        }

        [Test]
        public void CssBackgroundWithImage()
        {
            var background = ParseDeclaration("background:url(../images/ribbon.svg) no-repeat");
            Assert.That(background.Name, Is.EqualTo("background"));
            Assert.That(background.IsImportant, Is.False);
        }

        [Test]
        public void CssContentWithCounter()
        {
            var content = ParseDeclaration("content:counter(paging, decimal-leading-zero)");
            Assert.That(content.Name, Is.EqualTo("content"));
            Assert.That(content.IsImportant, Is.False);
        }

        [Test]
        public void CssBackgroundColorRgb()
        {
            var backgroundColor = ParseDeclaration("background-color: rgb(245, 0, 111)");
            Assert.That(backgroundColor.Name, Is.EqualTo("background-color"));
            Assert.That(backgroundColor.IsImportant, Is.False);
        }

        [Test]
        public void CssImportSheet()
        {
            var rule = "@import url(fonts.css);";
            var decl = ParseRule(rule);
            Assert.IsNotNull(decl);
            Assert.IsInstanceOf<CssImportRule>(decl);
            var importRule = (CssImportRule)decl;
            Assert.That(importRule.Href, Is.EqualTo("fonts.css"));
        }

        [Test]
        public void CssContentEscaped()
        {
            var content = ParseDeclaration("content:'\005E'");
            Assert.That(content.Name, Is.EqualTo("content"));
            Assert.That(content.IsImportant, Is.False);
        }

        [Test]
        public void CssContentCounter()
        {
            var content = ParseDeclaration("content:counter(list)'.'");
            Assert.That(content.Name, Is.EqualTo("content"));
            Assert.That(content.IsImportant, Is.False);
        }

        [Test]
        public void CssTransformTranslate()
        {
            var transform = ParseDeclaration("transform:translateY(-50%)");
            Assert.That(transform.Name, Is.EqualTo("transform"));
            Assert.That(transform.IsImportant, Is.False);
        }

        [Test]
        public void CssBoxShadowMultiline()
        {
            var boxShadow = ParseDeclaration(@"
        box-shadow:
			0 0 0 10px rgba(60, 61, 64, 0.6),
			0 0 50px #3C3D40;");
            Assert.That(boxShadow.Name, Is.EqualTo("box-shadow"));
            Assert.That(boxShadow.IsImportant, Is.False);
        }

        [Test]
        public void CssDisplayBlock()
        {
            var display = ParseDeclaration("display:block");
            Assert.That(display.Name, Is.EqualTo("display"));
            Assert.That(display.IsImportant, Is.False);
            Assert.That(display.Value, Is.EqualTo("block"));
        }

        [Test]
        public void CssSheetWithDataUrlAsBackgroundImage()
        {
            var sheet = ParseStyleSheet(".App_Header_ .logo { background-image: url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC\"); background-size: 71px 28px; background-position: 0 19px; width: 71px; }");
            Assert.IsNotNull(sheet);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var rule = sheet.Rules[0] as CssStyleRule;
            Assert.IsNotNull(rule);
            Assert.That(rule.Style.Length, Is.EqualTo(5));
            Assert.That(rule.SelectorText, Is.EqualTo(".App_Header_ .logo"));
            var decl = rule.Style as ICssStyleDeclaration;
            Assert.That(decl.GetBackgroundImage(), Is.EqualTo("url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC\")"));
            Assert.That(decl.GetBackgroundSize(), Is.EqualTo("71px 28px"));
            Assert.That(decl.GetBackgroundPosition(), Is.EqualTo("0 19px"));
            Assert.That(decl.GetWidth(), Is.EqualTo("71px"));
        }

        [Test]
        public void CssSheetFromStreamWeirdBytesLeadingToInfiniteLoop()
        {
            var bs = new Byte[8];
            bs[0] = 239;
            bs[1] = 187;
            bs[2] = 191;
            bs[3] = 117;
            bs[4] = 43;
            bs[5] = 63;
            bs[6] = 63;
            bs[7] = 63;

            using (var memoryStream = new MemoryStream(bs, false))
            {
                var sheet = ParseStyleSheet(memoryStream);
            }
        }

        [Test]
        public void CssSheetFromStreamOnlyZerosAvailable()
        {
            var bs = new Byte[7180];

            using (var memoryStream = new MemoryStream(bs, false))
            {
                var sheet = ParseStyleSheet(memoryStream);
                Assert.IsNotNull(sheet);
                Assert.That(sheet.Rules.Length, Is.EqualTo(0));
            }
        }

        [Test]
        public void CssSheetFromStringWithQuestionMarksLeadingToInfiniteLoop()
        {
            var sheet = ParseStyleSheet("U+???\0");
            Assert.IsNotNull(sheet);
            Assert.That(sheet.Rules.Length, Is.EqualTo(0));
        }

        [Test]
        public void CssDefaultSheetSupportsRoundTripping()
        {
            var originalSourceCode = @"p.info {
	font-family: arial, sans-serif;
	line-height: 150%;
	margin-left: 2em;
	padding: 1em;
	border: 3px solid red;
	background-color: #f89;
	display: inline-block;
}
p.info span {
	font-weight: bold;
}
p.info span::after {
	content: ': ';
}";
            var initialSheet = ParseStyleSheet(originalSourceCode);
            var initialSourceCode = initialSheet.ToCss();
            var finalSheet = ParseStyleSheet(initialSourceCode);
            var finalSourceCode = finalSheet.ToCss();
            Assert.That(finalSourceCode, Is.EqualTo(initialSourceCode));
            Assert.That(finalSheet.Rules.Length, Is.EqualTo(initialSheet.Rules.Length));
        }

        [Test]
        public void CssParseSheetWithStyleMediaAndStyleRule()
        {
            var sheet = ParseStyleSheet(@".mobile,.tablet{display:none;} @media only screen and(max-width:51.875em){.tablet{display:block;}} .disp {display:block;}");
            Assert.That(sheet.Rules.Length, Is.EqualTo(3));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Style));
            Assert.That(sheet.Rules[1].Type, Is.EqualTo(CssRuleType.Media));
            Assert.That(sheet.Rules[2].Type, Is.EqualTo(CssRuleType.Style));
        }

        [Test]
        public void CssParseSheetWithMediaAndTwoStyleRules()
        {
            var sheet = ParseStyleSheet(@"@media only screen and(max-width:51.875em){.tablet{display:block;}} .mobile,.tablet{display:none;} .disp {display:block;}");
            Assert.That(sheet.Rules.Length, Is.EqualTo(3));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Media));
            Assert.That(sheet.Rules[1].Type, Is.EqualTo(CssRuleType.Style));
            Assert.That(sheet.Rules[2].Type, Is.EqualTo(CssRuleType.Style));
        }

        [Test]
        public void CssParseSheetWithTwoStyleAndMediaRule()
        {
            var sheet = ParseStyleSheet(@".mobile,.tablet{display:none;} .disp {display:block;} @media only screen and(max-width:51.875em){.tablet{display:block;}}");
            Assert.That(sheet.Rules.Length, Is.EqualTo(3));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Style));
            Assert.That(sheet.Rules[1].Type, Is.EqualTo(CssRuleType.Style));
            Assert.That(sheet.Rules[2].Type, Is.EqualTo(CssRuleType.Media));
        }

        [Test]
        public void CssParseImportStatementWithNoMediaTextFollowedByStyle()
        {
            var src = "@import url(import3.css); p { color : #f00; }";
            var sheet = ParseStyleSheet(src);
            Assert.That(sheet.Rules.Length, Is.EqualTo(2));
            var import = sheet.Rules[0] as ICssImportRule;
            var style = sheet.Rules[1] as ICssStyleRule;
            Assert.IsNotNull(import);
            Assert.IsNotNull(style);
            Assert.That(import.Media.Length, Is.EqualTo(0));
            Assert.That(import.Media.MediaText, Is.EqualTo(""));
            Assert.That(import.Href, Is.EqualTo("import3.css"));
            Assert.That(style.Selector.Text, Is.EqualTo("p"));
            Assert.That(style.Style.Length, Is.EqualTo(1));
        }

        [Test]
        public void CssParseMediaRuleWithInvalidMediumEntities()
        {
            var src = "@media only screen and (min--moz-device-pixel-ratio:1.5),only screen and (-o-min-device-pixel-ratio:3/2),only screen and (-webkit-min-device-pixel-ratio:1.5),only screen and (min-device-pixel-ratio:1.5){.favicon{background-image:url('../img/favicons-sprite32.png?v=1b9547cf9cee3350a5b4875951e3e552');background-size:16px 5634px}}";
            var sheet = ParseStyleSheet(src);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.IsNotNull(media);
            Assert.That(media.Media.Length, Is.EqualTo(4));
            Assert.That(media.Rules.Length, Is.EqualTo(1));
        }

        [Test]
        public void CssParseStyleWithInvalidSurrogatePair()
        {
            var src = @"span.berschrift2Zchn
{mso-style-name:""\00DCberschrift 2 Zchn"";
mso-style-priority:9;
mso-style-link:""\00DCberschrift 2"";
font-family:""Cambria"",""serif"";
color:#4F81BD;
font-weight:bold;}";
            var sheet = ParseStyleSheet(src);
            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.That(style.SelectorText, Is.EqualTo("span.berschrift2Zchn"));
            Assert.That(style.Style.Length, Is.EqualTo(3));
        }

        [Test]
        public void CssParseMsViewPortWithoutOptions()
        {
            var css = "@-ms-viewport{width:device-width} .dsip { display: block; }";
            var doc = ParseStyleSheet(css);
            var result = doc.ToCss();
            Assert.That(result, Is.EqualTo(".dsip { display: block }"));
        }

        [Test]
        public void CssParseMsViewPortWithUnknownRules()
        {
            var options = new CssParserOptions()
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true
            };
            var css = "@-ms-viewport{width:device-width} .dsip { display: block; }";
            var doc = ParseStyleSheet(css, options);
            var result = doc.ToCss();
            Assert.That(result, Is.EqualTo("@-ms-viewport{width:device-width}" + Environment.NewLine + ".dsip { display: block }"));
        }

        [Test]
        public void CssParseMediaAndMsViewPortWithoutOptions()
        {
            var css = "@media screen and (max-width: 400px) {  @-ms-viewport { width: 320px; }  }  .dsip { display: block; }";
            var doc = ParseStyleSheet(css);
            var result = doc.ToCss();
            Assert.That(result, Is.EqualTo("@media screen and (max-width: 400px) { }" + Environment.NewLine + ".dsip { display: block }"));
        }

        [Test]
        public void CssParseMediaAndMsViewPortWithUnknownRules()
        {
            var options = new CssParserOptions()
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true
            };
            var css = "@media screen and (max-width: 400px) {  @-ms-viewport { width: 320px; }  }  .dsip { display: block; }";
            var doc = ParseStyleSheet(css, options);
            var result = doc.ToCss();
            Assert.That(result, Is.EqualTo("@media screen and (max-width: 400px) { @-ms-viewport { width: 320px; } }" + Environment.NewLine + ".dsip { display: block }"));
        }

        [Test]
        public void CssStyleSheetInsertAndDeleteShouldWork()
        {
            var s = ParseStyleSheet(String.Empty);
            Assert.That(s.Rules.Length, Is.EqualTo(0));

            s.Insert("a {color: blue}", 0);
            Assert.That(s.Rules.Length, Is.EqualTo(1));

            s.Insert("a *:first-child, a img {border: none}", 1);
            Assert.That(s.Rules.Length, Is.EqualTo(2));

            s.RemoveAt(1);
            Assert.That(s.Rules.Length, Is.EqualTo(1));

            s.RemoveAt(0);
            Assert.That(s.Rules.Length, Is.EqualTo(0));
        }

        [Test]
        public void CssStyleSheetShouldIgnoreHtmlCommentTokens()
        {
            var parser = new CssParser();
            var source = "<!-- body { font-family: Verdana } div.hidden { display: none } -->";
            var sheet = parser.ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(2));

            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Style));
            var body = sheet.Rules[0] as ICssStyleRule;
            Assert.That(body.SelectorText, Is.EqualTo("body"));
            Assert.That(body.Style.Length, Is.EqualTo(1));
            Assert.That(body.Style.GetFontFamily(), Is.EqualTo("Verdana"));

            Assert.That(sheet.Rules[1].Type, Is.EqualTo(CssRuleType.Style));
            var div = sheet.Rules[1] as ICssStyleRule;
            Assert.That(div.SelectorText, Is.EqualTo("div.hidden"));
            Assert.That(div.Style.Length, Is.EqualTo(1));
            Assert.That(div.Style.GetDisplay(), Is.EqualTo("none"));
        }

        [Test]
        public void CssStyleSheetShouldExpandBorderColorCorrectly_Issue23()
        {
            var parser = new CssParser();
            var source = "body { border-color: red }";
            var sheet = parser.ParseStyleSheet(source);

            Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            Assert.That(sheet.Rules[0].Type, Is.EqualTo(CssRuleType.Style));

            var body = sheet.Rules[0] as ICssStyleRule;
            Assert.That(body.Style.CssText, Is.EqualTo("border-color: rgba(255, 0, 0, 1)"));
            Assert.That(body.Style.GetBorderColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(body.Style.GetBorderLeftColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(body.Style.GetBorderRightColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(body.Style.GetBorderTopColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(body.Style.GetBorderBottomColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssStyleSheetShouldCollapseBorderColorCorrectly_Issue23()
        {
            var parser = new CssParser();
            var source = "body { border-color: red }";
            var sheet = parser.ParseStyleSheet(source);

            var body = sheet.Rules[0] as ICssStyleRule;
            body.Style.SetBorderLeftColor("blue");
            body.Style.SetBorderRightColor("blue");
            Assert.That(body.Style.CssText, Is.EqualTo("border-color: rgba(255, 0, 0, 1) rgba(0, 0, 255, 1)"));
            Assert.That(body.Style.GetBorderColor(), Is.EqualTo("rgba(255, 0, 0, 1) rgba(0, 0, 255, 1)"));
            Assert.That(body.Style.GetBorderLeftColor(), Is.EqualTo("rgba(0, 0, 255, 1)"));
            Assert.That(body.Style.GetBorderRightColor(), Is.EqualTo("rgba(0, 0, 255, 1)"));
            Assert.That(body.Style.GetBorderTopColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
            Assert.That(body.Style.GetBorderBottomColor(), Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssStyleSheetShouldCollapseFullBorderCorrectly_Issue23()
        {
            var parser = new CssParser();
            var source = "body { border: 1px  solid  red }";
            var sheet = parser.ParseStyleSheet(source);

            var body = sheet.Rules[0] as ICssStyleRule;
            Assert.That(body.Style.CssText, Is.EqualTo("border: 1px solid rgba(255, 0, 0, 1)"));
            body.Style.SetBorderLeftColor("blue");
            body.Style.SetBorderTopWidth("medium");
            Assert.That(body.Style.CssText, Is.EqualTo("border-top: 3px solid rgba(255, 0, 0, 1); border-right: 1px solid rgba(255, 0, 0, 1); border-bottom: 1px solid rgba(255, 0, 0, 1); border-left: 1px solid rgba(0, 0, 255, 1)"));
            Assert.That(body.Style.GetBorderColor(), Is.EqualTo("rgba(255, 0, 0, 1) rgba(255, 0, 0, 1) rgba(255, 0, 0, 1) rgba(0, 0, 255, 1)"));
            Assert.That(body.Style.GetBorderWidth(), Is.EqualTo("3px 1px 1px"));
            Assert.That(body.Style.GetBorderStyle(), Is.EqualTo("solid"));
        }

        [Test]
        public void CssStyleSheetInsertShouldSetParentStyleSheetCorrectly()
        {
            var s = ParseStyleSheet(String.Empty);
            s.Insert("a {color: blue}", 0);
            Assert.That(s.Rules[0].Owner, Is.EqualTo(s));
        }

        [Test]
        public void GetImageRefOfACertainDeclarationFromSheet()
        {
            var s = ParseStyleSheet("body { background: url(http://example.com/foo.png) no-repeat }");
            var rule = s.GetStyleRuleWith("body");
            var url = rule.GetValueOf("background-image").AsUrl();
            Assert.That(url, Is.EqualTo("http://example.com/foo.png"));
        }

        [Test]
        public void GetBorderRightColorOfACertainDeclarationFromSheet()
        {
            var s = ParseStyleSheet("p > a { border: 1px solid red }");
            var rule = s.GetStyleRuleWith("p > a");
            var color = rule.GetValueOf("border-right-color").AsRgba();
            Assert.That(color, Is.EqualTo(0x00_00_ff_ff));
        }

        [Test]
        public void CssColorFunctionsMixAllShouldWork()
        {
            var parser = new CssParser();
            var source = @"
.rgbNumber { color: rgb(255, 128, 0); }
.rgbPercent { color: rgb(100%, 50%, 0%); }
.rgbaNumber { color: rgba(255, 128, 0, 0.0); }
.rgbaPercent { color: rgba(100%, 50%, 0%, 0.0); }
.hsl { color: hsl(120, 100%, 50%); }
.hslAngle { color: hsl(120deg, 100%, 50%); }
.hsla { color: hsla(120, 100%, 50%, 0.25); }
.hslaAngle { color: hsla(120deg, 100%, 50%, 0.25); }
.grayNumber { color: gray(128); }
.grayPercent { color: gray(50%); }
.grayPercentAlpha { color: gray(50%, 0.5); }
.hwb { color: hwb(120, 60%, 20%); }
.hwbAngle { color: hwb(120deg, 60%, 20%); }
.hwbAlpha { color: hwb(120, 10%, 50%, 0.5); }
.hwbAngleAlpha { color: hwb(120deg, 10%, 50%, 0.5); }";
            var sheet = parser.ParseStyleSheet(source);
            Assert.That(sheet.Rules.Length, Is.EqualTo(15));

            var rgbNumber = (sheet.Rules[0] as ICssStyleRule).Style.GetColor();
            var rgbPercent = (sheet.Rules[1] as ICssStyleRule).Style.GetColor();
            var rgbaNumber = (sheet.Rules[2] as ICssStyleRule).Style.GetColor();
            var rgbaPercent = (sheet.Rules[3] as ICssStyleRule).Style.GetColor();
            var hsl = (sheet.Rules[4] as ICssStyleRule).Style.GetColor();
            var hslAngle = (sheet.Rules[5] as ICssStyleRule).Style.GetColor();
            var hsla = (sheet.Rules[6] as ICssStyleRule).Style.GetColor();
            var hslaAngle = (sheet.Rules[7] as ICssStyleRule).Style.GetColor();
            var grayNumber = (sheet.Rules[8] as ICssStyleRule).Style.GetColor();
            var grayPercent = (sheet.Rules[9] as ICssStyleRule).Style.GetColor();
            var grayPercentAlpha = (sheet.Rules[10] as ICssStyleRule).Style.GetColor();
            var hwb = (sheet.Rules[11] as ICssStyleRule).Style.GetColor();
            var hwbAngle = (sheet.Rules[12] as ICssStyleRule).Style.GetColor();
            var hwbAlpha = (sheet.Rules[13] as ICssStyleRule).Style.GetColor();
            var hwbAngleAlpha = (sheet.Rules[14] as ICssStyleRule).Style.GetColor();

            Assert.IsNotNull(rgbNumber);
            Assert.IsNotNull(rgbPercent);
            Assert.IsNotNull(rgbaPercent);
            Assert.IsNotNull(hsl);
            Assert.IsNotNull(hslAngle);
            Assert.IsNotNull(hsla);
            Assert.IsNotNull(hslaAngle);
            Assert.IsNotNull(grayNumber);
            Assert.IsNotNull(grayPercent);
            Assert.IsNotNull(grayPercentAlpha);
            Assert.IsNotNull(hwb);
            Assert.IsNotNull(hwbAngle);
            Assert.IsNotNull(hwbAlpha);
            Assert.IsNotNull(hwbAngleAlpha);
        }
    }
}
