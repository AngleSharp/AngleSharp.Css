namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    [TestFixture]
	public class CssCasesTests
	{
        private static ICssStyleSheet ParseSheet(String text)
        {
            return ParseStyleSheet(text, new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsExcludingNesting = true,
                IsToleratingInvalidSelectors = false,
            });
        }

		[Test]
        public void StyleSheetAtNamespace()
		{
			var sheet = ParseSheet(@"@namespace svg ""http://www.w3.org/2000/svg"";");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetCharsetLinebreak()
		{
			var sheet = ParseSheet(@"@charset
    ""UTF-8""
    ;");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

			foreach (var rule in sheet.Rules)
				Assert.That(((ICssCharsetRule)rule).CharacterSet, Is.EqualTo(@"UTF-8"));
		}

		[Test]
        public void StyleSheetCharset()
		{
			var sheet = ParseSheet(@"@charset ""UTF-8"";       /* Set the encoding of the style sheet to Unicode UTF-8 */
@charset 'iso-8859-15'; /* Set the encoding of the style sheet to Latin-9 (Western European languages, with euro sign) */
");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));
            Assert.That(((ICssCharsetRule)sheet.Rules[0]).CharacterSet, Is.EqualTo(@"UTF-8"));
            Assert.That(((ICssCharsetRule)sheet.Rules[1]).CharacterSet, Is.EqualTo(@"iso-8859-15"));
		}

		[Test]
        public void StyleSheetColonSpace()
		{
			var sheet = ParseSheet(@"a {
    margin  : auto;
    padding : 0;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

			foreach (var rule in sheet.Rules)
			{
				Assert.That(((ICssStyleRule)rule).SelectorText, Is.EqualTo(@"a"));
				Assert.That(((ICssStyleRule)rule).Style["margin"], Is.EqualTo(@"auto"));
				Assert.That(((ICssStyleRule)rule).Style["padding"], Is.EqualTo(@"0"));
			}
		}

		[Test]
        public void StyleSheetCommaAttribute()
		{
			var sheet = ParseSheet(@".foo[bar=""baz,quz""] {
  foobar: 123;
}

.bar,
#bar[baz=""qux,foo""],
#qux {
  foobar: 456;
}

.baz[qux="",foo""],
.baz[qux=""foo,""],
.baz[qux=""foo,bar,baz""],
.baz[qux="",foo,bar,baz,""],
.baz[qux="" , foo , bar , baz , ""] {
  foobar: 789;
}

.qux[foo='bar,baz'],
.qux[bar=""baz,foo""],
#qux[foo=""foobar""],
#qux[foo=',bar,baz, '] {
  foobar: 012;
}

#foo[foo=""""],
#foo[bar="" ""],
#foo[bar="",""],
#foo[bar="", ""],
#foo[bar="" ,""],
#foo[bar="" , ""],
#foo[baz=''],
#foo[qux=' '],
#foo[qux=','],
#foo[qux=', '],
#foo[qux=' ,'],
#foo[qux=' , '] {
  foobar: 345;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(5));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@".foo[bar=""baz,quz""]"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["foobar"], Is.EqualTo(@"123"));

            Assert.That(((ICssStyleRule)sheet.Rules[1]).SelectorText, Is.EqualTo(@".bar, #bar[baz=""qux,foo""], #qux"));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["foobar"], Is.EqualTo(@"456"));

            Assert.That(((ICssStyleRule)sheet.Rules[2]).SelectorText, Is.EqualTo(@".baz[qux="",foo""], .baz[qux=""foo,""], .baz[qux=""foo,bar,baz""], .baz[qux="",foo,bar,baz,""], .baz[qux="" , foo , bar , baz , ""]"));
            Assert.That(((ICssStyleRule)sheet.Rules[2]).Style["foobar"], Is.EqualTo(@"789"));

            Assert.That(((ICssStyleRule)sheet.Rules[3]).SelectorText, Is.EqualTo(@".qux[foo=""bar,baz""], .qux[bar=""baz,foo""], #qux[foo=""foobar""], #qux[foo="",bar,baz, ""]"));
            Assert.That(((ICssStyleRule)sheet.Rules[3]).Style["foobar"], Is.EqualTo(@"012"));

            Assert.That(((ICssStyleRule)sheet.Rules[4]).SelectorText, Is.EqualTo(@"#foo[foo=""""], #foo[bar="" ""], #foo[bar="",""], #foo[bar="", ""], #foo[bar="" ,""], #foo[bar="" , ""], #foo[baz=""""], #foo[qux="" ""], #foo[qux="",""], #foo[qux="", ""], #foo[qux="" ,""], #foo[qux="" , ""]"));
            Assert.That(((ICssStyleRule)sheet.Rules[4]).Style["foobar"], Is.EqualTo(@"345"));
		}

		[Test]
        public void StyleSheetCommaSelectorFunction()
		{
			var sheet = ParseSheet(@".foo:matches(.bar,.baz),
.foo:matches(.bar, .baz),
.foo:matches(.bar , .baz),
.foo:matches(.bar ,.baz) {
  prop: value;
}

.foo:matches(.bar,.baz,.foobar),
.foo:matches(.bar, .baz,),
.foo:matches(,.bar , .baz) {
  anotherprop: anothervalue;
}");
            Assert.That(sheet.Rules.Length, Is.EqualTo(2));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@".foo:matches(.bar, .baz), .foo:matches(.bar, .baz), .foo:matches(.bar, .baz), .foo:matches(.bar, .baz)"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["prop"], Is.EqualTo(@"value"));

            Assert.That(((ICssStyleRule)sheet.Rules[1]).SelectorText, Is.EqualTo(null));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["anotherprop"], Is.EqualTo(@"anothervalue"));
		}

		[Test]
        public void StyleSheetCommentIn()
		{
			var sheet = ParseSheet(@"a {
    color/**/: red;
    padding/*4815162342*/: 1px /**/ 2px /*13*/ 3px;
    border/*\**/: solid; font-family/*\**/: none\9;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var rule = sheet.Rules[0];

            Assert.That(((ICssStyleRule)rule).SelectorText, Is.EqualTo(@"a"));
            Assert.That(((ICssStyleRule)rule).Style["color"], Is.EqualTo(@"rgba(255, 0, 0, 1)"));
            Assert.That(((ICssStyleRule)rule).Style["padding"], Is.EqualTo(@"1px 2px 3px"));
            Assert.That(((ICssStyleRule)rule).Style["border"], Is.EqualTo(@"solid"));
            Assert.That(((ICssStyleRule)rule).Style["font-family"], Is.EqualTo("none\t"));
		}

		[Test]
        public void StyleSheetCommentUrl()
		{
			var sheet = ParseSheet(@"/* http://foo.com/bar/baz.html */
/**/

foo { /*/*/
  /* something */
  bar: baz; /* http://foo.com/bar/baz.html */
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"foo"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["bar"], Is.EqualTo(@"baz"));
		}

		[Test]
        public void StyleSheetComment()
		{
			var sheet = ParseSheet(@"/* 1 */

head, /* footer, */body/*, nav */ { /* 2 */
  /* 3 */
  /**/foo: 'bar';
  /* 4 */
} /* 5 */

/* 6 */");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"head, body"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["foo"], Is.EqualTo(@"'bar'"));
		}

		[Test]
        public void StyleSheetCustomMediaLinebreak()
		{
			var sheet = ParseSheet(@"@custom-media
    --test
    (min-width: 200px)
;");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetCustomMedia()
		{
			var sheet = ParseSheet(@"@custom-media --narrow-window (max-width: 30em);
@custom-media --wide-window screen and (min-width: 40em);
");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));
		}

		[Test]
        public void StyleSheetDocumentLinebreak()
		{
			var sheet = ParseSheet(@"@document
    url-prefix()
    {

        .test {
            color: blue;
        }

    }");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetDocument()
		{
			var sheet = ParseSheet(@"@-moz-document url-prefix() {
  /* ui above */
  .ui-select .ui-btn select {
    /* ui inside */
    opacity:.0001
  }

  .icon-spin {
    height: .9em;
  }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
		public void StyleSheetEmpty()
		{
			var sheet = ParseSheet(@"");
			Assert.That(sheet.Rules.Length, Is.EqualTo(0));
		}

		[Test]
        public void StyleSheetEscapes()
		{
			var sheet = ParseSheet(@"/* tests compressed for easy testing */
/* http://mathiasbynens.be/notes/css-escapes */
/* will match elements with class="":`("" */
.\3A \`\({}
/* will match elements with class=""1a2b3c"" */
.\31 a2b3c{}
/* will match the element with id=""#fake-id"" */
#\#fake-id{}
/* will match the element with id=""---"" */
#\---{}
/* will match the element with id=""-a-b-c-"" */
#-a-b-c-{}
/* will match the element with id=""©"" */
#©{}
/* More tests from http://mathiasbynens.be/demo/html5-id */
html{font:1.2em/1.6 Arial;}
code{font-family:Consolas;}
li code{background:rgba(255, 255, 255, .5);padding:.3em;}
li{background:orange;}
#♥{background:lime;}
#©{background:lime;}
#“‘’”{background:lime;}
#☺☃{background:lime;}
#⌘⌥{background:lime;}
#𝄞♪♩♫♬{background:lime;}
#\?{background:lime;}
#\@{background:lime;}
#\.{background:lime;}
#\3A \){background:lime;}
#\3A \`\({background:lime;}
#\31 23{background:lime;}
#\31 a2b3c{background:lime;}
#\<p\>{background:lime;}
#\<\>\<\<\<\>\>\<\>{background:lime;}
#\+\+\+\+\+\+\+\+\+\+\[\>\+\+\+\+\+\+\+\>\+\+\+\+\+\+\+\+\+\+\>\+\+\+\>\+\<\<\<\<\-\]\>\+\+\.\>\+\.\+\+\+\+\+\+\+\.\.\+\+\+\.\>\+\+\.\<\<\+\+\+\+\+\+\+\+\+\+\+\+\+\+\+\.\>\.\+\+\+\.\-\-\-\-\-\-\.\-\-\-\-\-\-\-\-\.\>\+\.\>\.{background:lime;}
#\#{background:lime;}
#\#\#{background:lime;}
#\#\.\#\.\#{background:lime;}
#\_{background:lime;}
#\.fake\-class{background:lime;}
#foo\.bar{background:lime;}
#\3A hover{background:lime;}
#\3A hover\3A focus\3A active{background:lime;}
#\[attr\=value\]{background:lime;}
#f\/o\/o{background:lime;}
#f\\o\\o{background:lime;}
#f\*o\*o{background:lime;}
#f\!o\!o{background:lime;}
#f\'o\'o{background:lime;}
#f\~o\~o{background:lime;}
#f\+o\+o{background:lime;}

/* css-parse does not yet pass this test */
/*#\{\}{background:lime;}*/");
			Assert.That(sheet.Rules.Length, Is.EqualTo(42));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@".\:\`\("));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).SelectorText, Is.EqualTo(@".\31 a2b3c"));
            Assert.That(((ICssStyleRule)sheet.Rules[2]).SelectorText, Is.EqualTo(@"#\#fake-id"));
            Assert.That(((ICssStyleRule)sheet.Rules[3]).SelectorText, Is.EqualTo(@"#---"));
            Assert.That(((ICssStyleRule)sheet.Rules[4]).SelectorText, Is.EqualTo(@"#-a-b-c-"));
            Assert.That(((ICssStyleRule)sheet.Rules[5]).SelectorText, Is.EqualTo(@"#©"));
            Assert.That(((ICssStyleRule)sheet.Rules[6]).SelectorText, Is.EqualTo(@"html"));
            Assert.That(((ICssStyleRule)sheet.Rules[6]).Style["font"], Is.EqualTo(@"1.2em / 1.6 Arial"));
            Assert.That(((ICssStyleRule)sheet.Rules[7]).SelectorText, Is.EqualTo(@"code"));
            Assert.That(((ICssStyleRule)sheet.Rules[7]).Style["font-family"], Is.EqualTo(@"Consolas"));
            Assert.That(((ICssStyleRule)sheet.Rules[8]).SelectorText, Is.EqualTo(@"li code"));
            Assert.That(((ICssStyleRule)sheet.Rules[8]).Style["background"], Is.EqualTo(@"rgba(255, 255, 255, 0.5)"));
            Assert.That(((ICssStyleRule)sheet.Rules[8]).Style["padding"], Is.EqualTo(@"0.3em"));
            Assert.That(((ICssStyleRule)sheet.Rules[9]).SelectorText, Is.EqualTo(@"li"));
            Assert.That(((ICssStyleRule)sheet.Rules[9]).Style["background"], Is.EqualTo(@"rgba(255, 165, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[10]).SelectorText, Is.EqualTo(@"#♥"));
            Assert.That(((ICssStyleRule)sheet.Rules[10]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[11]).SelectorText, Is.EqualTo(@"#©"));
            Assert.That(((ICssStyleRule)sheet.Rules[11]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[12]).SelectorText, Is.EqualTo(@"#“‘’”"));
            Assert.That(((ICssStyleRule)sheet.Rules[12]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[13]).SelectorText, Is.EqualTo(@"#☺☃"));
            Assert.That(((ICssStyleRule)sheet.Rules[13]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[14]).SelectorText, Is.EqualTo(@"#⌘⌥"));
            Assert.That(((ICssStyleRule)sheet.Rules[14]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[15]).SelectorText, Is.EqualTo(@"#𝄞♪♩♫♬"));
            Assert.That(((ICssStyleRule)sheet.Rules[15]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[16]).SelectorText, Is.EqualTo(@"#\?"));
            Assert.That(((ICssStyleRule)sheet.Rules[16]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[17]).SelectorText, Is.EqualTo(@"#\@"));
            Assert.That(((ICssStyleRule)sheet.Rules[17]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[18]).SelectorText, Is.EqualTo(@"#\."));
            Assert.That(((ICssStyleRule)sheet.Rules[18]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[19]).SelectorText, Is.EqualTo(@"#\:\)"));
            Assert.That(((ICssStyleRule)sheet.Rules[19]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[20]).SelectorText, Is.EqualTo(@"#\:\`\("));
            Assert.That(((ICssStyleRule)sheet.Rules[20]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[21]).SelectorText, Is.EqualTo(@"#\31 23"));
            Assert.That(((ICssStyleRule)sheet.Rules[21]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[22]).SelectorText, Is.EqualTo(@"#\31 a2b3c"));
            Assert.That(((ICssStyleRule)sheet.Rules[22]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[23]).SelectorText, Is.EqualTo(@"#\<p\>"));
            Assert.That(((ICssStyleRule)sheet.Rules[23]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[24]).SelectorText, Is.EqualTo(@"#\<\>\<\<\<\>\>\<\>"));
            Assert.That(((ICssStyleRule)sheet.Rules[24]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[25]).SelectorText, Is.EqualTo("#\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\[\\>\\+\\+\\+\\+\\+\\+\\+\\>\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\>\\+\\+\\+\\>\\+\\<\\<\\<\\<-\\]\\>\\+\\+\\.\\>\\+\\.\\+\\+\\+\\+\\+\\+\\+\\.\\.\\+\\+\\+\\.\\>\\+\\+\\.\\<\\<\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\+\\.\\>\\.\\+\\+\\+\\.------\\.--------\\.\\>\\+\\.\\>\\."));
            Assert.That(((ICssStyleRule)sheet.Rules[25]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[26]).SelectorText, Is.EqualTo(@"#\#"));
            Assert.That(((ICssStyleRule)sheet.Rules[26]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[27]).SelectorText, Is.EqualTo(@"#\#\#"));
            Assert.That(((ICssStyleRule)sheet.Rules[27]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[28]).SelectorText, Is.EqualTo(@"#\#\.\#\.\#"));
            Assert.That(((ICssStyleRule)sheet.Rules[28]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[29]).SelectorText, Is.EqualTo(@"#_"));
            Assert.That(((ICssStyleRule)sheet.Rules[29]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[30]).SelectorText, Is.EqualTo(@"#\.fake-class"));
            Assert.That(((ICssStyleRule)sheet.Rules[30]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[31]).SelectorText, Is.EqualTo(@"#foo\.bar"));
            Assert.That(((ICssStyleRule)sheet.Rules[31]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[32]).SelectorText, Is.EqualTo(@"#\:hover"));
            Assert.That(((ICssStyleRule)sheet.Rules[32]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[33]).SelectorText, Is.EqualTo(@"#\:hover\:focus\:active"));
            Assert.That(((ICssStyleRule)sheet.Rules[33]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[34]).SelectorText, Is.EqualTo(@"#\[attr\=value\]"));
            Assert.That(((ICssStyleRule)sheet.Rules[34]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[35]).SelectorText, Is.EqualTo(@"#f\/o\/o"));
            Assert.That(((ICssStyleRule)sheet.Rules[35]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[36]).SelectorText, Is.EqualTo(@"#f\\o\\o"));
            Assert.That(((ICssStyleRule)sheet.Rules[36]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[37]).SelectorText, Is.EqualTo(@"#f\*o\*o"));
            Assert.That(((ICssStyleRule)sheet.Rules[37]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[38]).SelectorText, Is.EqualTo(@"#f\!o\!o"));
            Assert.That(((ICssStyleRule)sheet.Rules[38]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[39]).SelectorText, Is.EqualTo(@"#f\'o\'o"));
            Assert.That(((ICssStyleRule)sheet.Rules[39]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[40]).SelectorText, Is.EqualTo(@"#f\~o\~o"));
            Assert.That(((ICssStyleRule)sheet.Rules[40]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[41]).SelectorText, Is.EqualTo(@"#f\+o\+o"));
            Assert.That(((ICssStyleRule)sheet.Rules[41]).Style["background"], Is.EqualTo(@"rgba(0, 255, 0, 1)"));
		}

		[Test]
        public void StyleSheetFontFaceLinebreak()
		{
			var sheet = ParseSheet(@"@font-face

       {
  font-family: ""Bitstream Vera Serif Bold"";
  src: url(""http://developer.mozilla.org/@api/deki/files/2934/=VeraSeBd.ttf"");
}

body {
  font-family: ""Bitstream Vera Serif Bold"", serif;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));

            Assert.That(((ICssStyleRule)sheet.Rules[1]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["font-family"], Is.EqualTo(@"""Bitstream Vera Serif Bold"", serif"));
		}

		[Test]
        public void StyleSheetFontFace()
		{
			var sheet = ParseSheet(@"@font-face {
  font-family: ""Bitstream Vera Serif Bold"";
  src: url(""http://developer.mozilla.org/@api/deki/files/2934/=VeraSeBd.ttf"");
}

body {
  font-family: ""Bitstream Vera Serif Bold"", serif;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));

            Assert.That(((ICssStyleRule)sheet.Rules[1]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["font-family"], Is.EqualTo(@"""Bitstream Vera Serif Bold"", serif"));
		}

		[Test]
        public void StyleSheetHostLinebreak()
		{
			var sheet = ParseSheet(@"@host
    {
        :scope { color: white; }
    }");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetHost()
		{
			var sheet = ParseSheet(@"@host {
  :scope {
    display: block;
  }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetImportLinebreak()
		{
			var sheet = ParseSheet(@"@import
    url(test.css)
    screen
    ;");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssImportRule)sheet.Rules[0]).Href, Is.EqualTo(@"test.css"));
		}

		[Test]
        public void StyleSheetImportMessed()
		{
			var sheet = ParseSheet(@"
   @import url(""fineprint.css"") print;
  @import url(""bluish.css"") projection, tv;
      @import 'custom.css';
  @import ""common.css"" screen, projection  ;

  @import url('landscape.css') screen and (orientation:landscape);");
			Assert.That(sheet.Rules.Length, Is.EqualTo(5));

            Assert.That(((ICssImportRule)sheet.Rules[0]).Href, Is.EqualTo(@"fineprint.css"));
            Assert.That(((ICssImportRule)sheet.Rules[0]).Media.MediaText, Is.EqualTo(@"print"));

            Assert.That(((ICssImportRule)sheet.Rules[1]).Href, Is.EqualTo(@"bluish.css"));
            Assert.That(((ICssImportRule)sheet.Rules[1]).Media.MediaText, Is.EqualTo(@"projection, tv"));

            Assert.That(((ICssImportRule)sheet.Rules[2]).Href, Is.EqualTo(@"custom.css"));
            Assert.That(((ICssImportRule)sheet.Rules[2]).Media.MediaText, Is.EqualTo(@""));

            Assert.That(((ICssImportRule)sheet.Rules[3]).Href, Is.EqualTo(@"common.css"));
            Assert.That(((ICssImportRule)sheet.Rules[3]).Media.MediaText, Is.EqualTo(@"screen, projection"));

            Assert.That(((ICssImportRule)sheet.Rules[4]).Href, Is.EqualTo(@"landscape.css"));
            Assert.That(((ICssImportRule)sheet.Rules[4]).Media.MediaText, Is.EqualTo(@"screen and (orientation: landscape)"));
		}

		[Test]
        public void StyleSheetImport()
		{
			var sheet = ParseSheet(@"@import url(""fineprint.css"") print;
@import url(""bluish.css"") projection, tv;
@import 'custom.css';
@import ""common.css"" screen, projection;
@import url('landscape.css') screen and (orientation:landscape);");
			Assert.That(sheet.Rules.Length, Is.EqualTo(5));

            Assert.That(((ICssImportRule)sheet.Rules[0]).Href, Is.EqualTo(@"fineprint.css"));
            Assert.That(((ICssImportRule)sheet.Rules[0]).Media.MediaText, Is.EqualTo(@"print"));

            Assert.That(((ICssImportRule)sheet.Rules[1]).Href, Is.EqualTo(@"bluish.css"));
            Assert.That(((ICssImportRule)sheet.Rules[1]).Media.MediaText, Is.EqualTo(@"projection, tv"));

            Assert.That(((ICssImportRule)sheet.Rules[2]).Href, Is.EqualTo(@"custom.css"));
            Assert.That(((ICssImportRule)sheet.Rules[2]).Media.MediaText, Is.EqualTo(@""));

            Assert.That(((ICssImportRule)sheet.Rules[3]).Href, Is.EqualTo(@"common.css"));
            Assert.That(((ICssImportRule)sheet.Rules[3]).Media.MediaText, Is.EqualTo(@"screen, projection"));

            Assert.That(((ICssImportRule)sheet.Rules[4]).Href, Is.EqualTo(@"landscape.css"));
            Assert.That(((ICssImportRule)sheet.Rules[4]).Media.MediaText, Is.EqualTo(@"screen and (orientation: landscape)"));
		}

		[Test]
        public void StyleSheetKeyframesAdvanced()
		{
			var sheet = ParseSheet(@"@keyframes advanced {
  top {
    opacity[sqrt]: 0;
  }

  100 {
    opacity: 0.5;
  }

  bottom {
    opacity: 1;
  }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetKeyframesComplex()
		{
			var sheet = ParseSheet(@"@keyframes foo {
  0% { top: 0; left: 0 }
  30.50% { top: 50px }
  .68% ,
  72%
      , 85% { left: 50px }
  100% { top: 100px; left: 100% }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetKeyframesLinebreak()
		{
			var sheet = ParseSheet(@"@keyframes
    test
    {
        from { opacity: 1; }
        to { opacity: 0; }
    }
");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetKeyframesMessed()
		{
			var sheet = ParseSheet(@"@keyframes fade {from
  {opacity: 0;
     }
to
  {
     opacity: 1;}}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetKeyframesVendor()
		{
			var sheet = ParseSheet(@"@-webkit-keyframes fade {
  from { opacity: 0 }
  to { opacity: 1 }
}
");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetKeyframes()
		{
			var sheet = ParseSheet(@"@keyframes fade {
  /* from above */
  from {
    /* from inside */
    opacity: 0;
  }

  /* to above */
  to {
    /* to inside */
    opacity: 1;
  }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetMediaLinebreak()
		{
			var sheet = ParseSheet(@"@media

(
    min-width: 300px
)
{
    .test { width: 100px; }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
            var rule = (ICssMediaRule)sheet.Rules[0];

            Assert.That(rule.Media.MediaText, Is.EqualTo(@"(min-width: 300px)"));
            Assert.That(rule.Rules.Length, Is.EqualTo(1));

            var subrule = rule.Rules[0];
            Assert.That(((ICssStyleRule)subrule).SelectorText, Is.EqualTo(@".test"));
            Assert.That(((ICssStyleRule)subrule).Style["width"], Is.EqualTo(@"100px"));
		}

		[Test]
        public void StyleSheetMediaMessed()
		{
			var sheet = ParseSheet(@"@media screen, projection{ html

  {
background: #fffef0;
    color:#300;
  }
  body

{
    max-width: 35em;
    margin: 0 auto;


}
  }

@media print
{
              html {
              background: #fff;
              color: #000;
              }
              body {
              padding: 1in;
              border: 0.5pt solid #666;
              }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));

            {
                var rule = sheet.Rules[0];
                Assert.That(((ICssMediaRule)rule).Media.MediaText, Is.EqualTo(@"screen, projection"));
                Assert.That(((ICssMediaRule)rule).Rules.Length, Is.EqualTo(2));

                {
                    var subrule = ((ICssMediaRule)rule).Rules[0];
                    Assert.That(((ICssStyleRule)subrule).SelectorText, Is.EqualTo(@"html"));
                    Assert.That(((ICssStyleRule)subrule).Style["background"], Is.EqualTo(@"rgba(255, 254, 240, 1)"));
                    Assert.That(((ICssStyleRule)subrule).Style["color"], Is.EqualTo(@"rgba(51, 0, 0, 1)"));
                }

                {
                    var subrule = ((ICssMediaRule)rule).Rules[1];
                    Assert.That(((ICssStyleRule)subrule).SelectorText, Is.EqualTo(@"body"));
                    Assert.That(((ICssStyleRule)subrule).Style["max-width"], Is.EqualTo(@"35em"));
                    Assert.That(((ICssStyleRule)subrule).Style["margin"], Is.EqualTo(@"0 auto"));
                }
            }

            {
                var rule = sheet.Rules[1];
                Assert.That(((ICssMediaRule)rule).Media.MediaText, Is.EqualTo(@"print"));
                Assert.That(((ICssMediaRule)rule).Rules.Length, Is.EqualTo(2));

                {
                    var subrule = ((ICssMediaRule)rule).Rules[0];
                    Assert.That(((ICssStyleRule)subrule).SelectorText, Is.EqualTo(@"html"));
                    Assert.That(((ICssStyleRule)subrule).Style["background"], Is.EqualTo(@"rgba(255, 255, 255, 1)"));
                    Assert.That(((ICssStyleRule)subrule).Style["color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));
                }

                {
                    var subrule = ((ICssMediaRule)rule).Rules[1];
                    Assert.That(((ICssStyleRule)subrule).SelectorText, Is.EqualTo(@"body"));
                    Assert.That(((ICssStyleRule)subrule).Style["padding"], Is.EqualTo(@"1in"));
                    Assert.That(((ICssStyleRule)subrule).Style["border"], Is.EqualTo(@"0.5pt solid rgba(102, 102, 102, 1)"));
                }
            }
		}

		[Test]
        public void StyleSheetMedia()
		{
			var sheet = ParseSheet(@"@media screen, projection {
  /* html above */
  html {
    /* html inside */
    background: #fffef0;
    color: #300;
  }

  /* body above */
  body {
    /* body inside */
    max-width: 35em;
    margin: 0 auto;
  }
}

@media print {
  html {
    background: #fff;
    color: #000;
  }
  body {
    padding: 1in;
    border: 0.5pt solid #666;
  }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));

            Assert.That(((ICssMediaRule)sheet.Rules[0]).Media.MediaText, Is.EqualTo(@"screen, projection"));
            Assert.That(((ICssMediaRule)sheet.Rules[0]).Rules.Length, Is.EqualTo(2));

            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[0]).SelectorText, Is.EqualTo(@"html"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[0]).Style["background"], Is.EqualTo(@"rgba(255, 254, 240, 1)"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[0]).Style["color"], Is.EqualTo(@"rgba(51, 0, 0, 1)"));

            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[1]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[1]).Style["max-width"], Is.EqualTo(@"35em"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[1]).Style["margin"], Is.EqualTo(@"0 auto"));

            Assert.That(((ICssMediaRule)sheet.Rules[1]).Media.MediaText, Is.EqualTo(@"print"));
			Assert.That(((ICssMediaRule)sheet.Rules[1]).Rules.Length, Is.EqualTo(2));

            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[0]).SelectorText, Is.EqualTo(@"html"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[0]).Style["background"], Is.EqualTo(@"rgba(255, 255, 255, 1)"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[0]).Style["color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));

            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[1]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[1]).Style["padding"], Is.EqualTo(@"1in"));
            Assert.That(((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[1]).Style["border"], Is.EqualTo(@"0.5pt solid rgba(102, 102, 102, 1)"));
		}

		[Test]
        public void StyleSheetMessedUp()
		{
			var sheet = ParseSheet(@"body { foo
  :
  'bar' }

   body{foo:bar;bar:baz}
   body
   {
     foo
     :
     bar
     ;
     bar
     :
     baz
     }
");
			Assert.That(sheet.Rules.Length, Is.EqualTo(3));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["foo"], Is.EqualTo(@"'bar'"));

            Assert.That(((ICssStyleRule)sheet.Rules[1]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["foo"], Is.EqualTo(@"bar"));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["bar"], Is.EqualTo(@"baz"));

            Assert.That(((ICssStyleRule)sheet.Rules[2]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)sheet.Rules[2]).Style["foo"], Is.EqualTo(@"bar"));
            Assert.That(((ICssStyleRule)sheet.Rules[2]).Style["bar"], Is.EqualTo(@"baz"));
		}

		[Test]
        public void StyleSheetNamespaceLinebreak()
		{
			var sheet = ParseSheet(@"@namespace
    ""http://www.w3.org/1999/xhtml""
    ;");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetNamespace()
		{
			var sheet = ParseSheet(@"@namespace ""http://www.w3.org/1999/xhtml"";
@namespace svg ""http://www.w3.org/2000/svg"";");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));
		}

		[Test]
        public void StyleSheetNoSemi()
		{
			var sheet = ParseSheet(@"
tobi loki jane {
  are: 'all';
  the-species: called ""ferrets""
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

			foreach (var rule in sheet.Rules)
			{
				Assert.That(((ICssStyleRule)rule).SelectorText, Is.EqualTo(@"tobi loki jane"));
				Assert.That(((ICssStyleRule)rule).Style["are"], Is.EqualTo(@"'all'"));
				Assert.That(((ICssStyleRule)rule).Style["the-species"], Is.EqualTo(@"called ""ferrets"""));
			}
		}

		[Test]
        public void StyleSheetPageLinebreak()
		{
			var sheet = ParseSheet(@"@page
    toc
    {
        color: black;
    }");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetPagedMedia()
		{
			var sheet = ParseSheet(@"/* toc above */
@page toc, index:blank {
  /* toc inside */
  color: green;
}

@page {
  font-size: 16pt;
  color: #f00;
}

@page :left {
  margin-left: 5cm;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(3));

            var page1 = sheet.Rules[0] as ICssPageRule;
            var page2 = sheet.Rules[1] as ICssPageRule;
            var page3 = sheet.Rules[2] as ICssPageRule;

            Assert.That(page1.Style.Length, Is.EqualTo(1));
            Assert.That(page1.Style["color"], Is.EqualTo("rgba(0, 128, 0, 1)"));

            Assert.That(page2.Style.Length, Is.EqualTo(2));
            Assert.That(page2.Style["font-size"], Is.EqualTo("16pt"));
            Assert.That(page2.Style["color"], Is.EqualTo("rgba(255, 0, 0, 1)"));

            Assert.That(page3.Style.Length, Is.EqualTo(1));
            Assert.That(page3.Style["margin-left"], Is.EqualTo("5cm"));
		}

		[Test]
        public void StyleSheetProps()
		{
			var sheet = ParseSheet(@"
tobi loki jane {
  are: 'all';
  the-species: called ""ferrets"";
  *even: 'ie crap';
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"tobi loki jane"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["are"], Is.EqualTo(@"'all'"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["the-species"], Is.EqualTo(@"called ""ferrets"""));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["*even"], Is.EqualTo(@"'ie crap'"));
		}

		[Test]
        public void StyleSheetQuoteEscape()
		{
			var sheet = ParseSheet(@"p[qwe=""a\"",b""] { color: red }
");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"p[qwe=""a\"",b""]"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["color"], Is.EqualTo(@"rgba(255, 0, 0, 1)"));
		}

		[Test]
        public void StyleSheetQuoted()
		{
			var sheet = ParseSheet(@"body {
  background: url('some;stuff;here') 50% 50% no-repeat;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"body"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["background"], Is.EqualTo(@"url(""some;stuff;here"") center no-repeat"));
		}

		[Test]
        public void StyleSheetRule()
		{
			var sheet = ParseSheet(@"foo {
  bar: 'baz';
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"foo"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["bar"], Is.EqualTo(@"'baz'"));
		}

		[Test]
        public void StyleSheetRules()
		{
			var sheet = ParseSheet(@"tobi {
  name: 'tobi';
  age: 2;
}

loki {
  name: 'loki';
  age: 1;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(2));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"tobi"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["name"], Is.EqualTo(@"'tobi'"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["age"], Is.EqualTo(@"2"));

            Assert.That(((ICssStyleRule)sheet.Rules[1]).SelectorText, Is.EqualTo(@"loki"));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["name"], Is.EqualTo(@"'loki'"));
            Assert.That(((ICssStyleRule)sheet.Rules[1]).Style["age"], Is.EqualTo(@"1"));
		}

		[Test]
        public void StyleSheetSelectors()
		{
			var sheet = ParseSheet(@"foo,
bar,
baz {
  color: black;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

            Assert.That(((ICssStyleRule)sheet.Rules[0]).SelectorText, Is.EqualTo(@"foo, bar, baz"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));
		}

		[Test]
        public void StyleSheetSupportsLinebreak()
		{
			var sheet = ParseSheet(@"@supports
    (display: flex)
    {
        .test { display: flex; }
    }");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetSupports()
		{
			var sheet = ParseSheet(@"@supports (display: flex) or (display: box) {
  /* flex above */
  .flex {
    /* flex inside */
    display: box;
    display: flex;
  }

  div {
    something: else;
  }
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));
		}

		[Test]
        public void StyleSheetWtf()
		{
			var sheet = ParseSheet(@".wtf {
  *overflow-x: hidden;
  //max-height: 110px;
  #height: 18px;
}");
			Assert.That(sheet.Rules.Length, Is.EqualTo(1));

			foreach (var rule in sheet.Rules)
			{
				Assert.That(((ICssStyleRule)rule).SelectorText, Is.EqualTo(@".wtf"));
				Assert.That(((ICssStyleRule)rule).Style["*overflow-x"], Is.EqualTo(@"hidden"));
				Assert.That(((ICssStyleRule)rule).Style["//max-height"], Is.EqualTo(@"110px"));
				Assert.That(((ICssStyleRule)rule).Style["#height"], Is.EqualTo(@"18px"));
			}
		}

        [Test]
        public void StyleSheetUnicodeEscapeLiteral()
        {
            var sheet = ParseSheet(@"h1 { background-color: \000062
lack; }");
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["background-color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void StyleSheetUnicodeEscapeVarious()
        {
            var sheet = ParseSheet("h1 { background-color: \\000062\r\nlack; color: \\000062\tlack; border-color: \\000062\nlack; outline-color: \\000062 lack }");
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["background-color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["border-color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["outline-color"], Is.EqualTo(@"rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void StyleSheetUnicodeEscapeLeadingSingleCarriageReturn()
        {
            var sheet = ParseSheet("h1 { background-image: \\000075\r\nrl('foo') }");
            Assert.That(((ICssStyleRule)sheet.Rules[0]).Style["background-image"], Is.EqualTo("url(\"foo\")"));
        }
    }
}