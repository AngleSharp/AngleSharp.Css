#nullable enable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using System;

    sealed class CssDefaultStyleSheetProvider : ICssDefaultStyleSheetProvider
    {
        #region Fields

        private ICssStyleSheet? _default;

        #endregion

        #region Properties

        public ICssStyleSheet Default => _default ?? (_default = Parse(DefaultSource));

        #endregion

        #region Methods

        public void SetDefault(ICssStyleSheet? sheet) => _default = sheet;

        public void SetDefault(String source) => SetDefault(Parse(source));

        public void AppendDefault(String source) => SetDefault(Parse($"{DefaultSource}\r\n{source}"));

        #endregion

        #region Default Stylesheet

        private static ICssStyleSheet Parse(String source)
        {
            var parser = new CssParser();
            return parser.ParseStyleSheet(source);
        }

        /// <summary>
        /// Gets the source code for the by default used base stylesheet.
        /// Taken from https://www.w3.org/TR/CSS22/sample.html, extended with
        /// the rules for HTML5 elements and form controls from
        /// https://html.spec.whatwg.org/multipage/rendering.html.
        /// </summary>
        public static readonly String DefaultSource = @"
html, address,
blockquote,
body, dd, div,
dl, dt, fieldset, form,
frame, frameset,
h1, h2, h3, h4,
h5, h6, noframes,
ol, p, ul, center,
dir, hr, menu, pre,
article, aside,
details, figcaption,
figure, footer,
header, hgroup,
legend, main, nav,
search, section  { display: block }
address,
blockquote,
center, div,
figure, figcaption,
footer, form,
header, hr,
legend, listing,
main, p,
plaintext, pre,
search, xmp,
bdi, output,
[dir=ltr i],
[dir=rtl i],
[dir=auto i]    { unicode-bidi: isolate }
li              { display: list-item }
summary         { display: list-item; list-style-type: disclosure-closed; list-style-position: inside }
details[open] > summary { list-style-type: disclosure-open }
head, area, base,
basefont, datalist,
link, meta, noembed,
param, rp, script,
style, template,
title           { display: none }
[hidden]        { display: none }
table           { display: table }
tr              { display: table-row }
thead           { display: table-header-group }
tbody           { display: table-row-group }
tfoot           { display: table-footer-group }
col             { display: table-column }
colgroup        { display: table-column-group }
td, th          { display: table-cell; padding: 1px }
caption         { display: table-caption }
th              { font-weight: bolder; text-align: center }
caption         { text-align: center }
body            { margin: 8px }
h1              { font-size: 2em; margin: .67em 0 }
h2              { font-size: 1.5em; margin: .75em 0 }
h3              { font-size: 1.17em; margin: .83em 0 }
h4, form        { margin: 1.12em 0 }
blockquote,
figure, listing,
p, plaintext,
pre, xmp,
dir, dl, menu,
ol, ul          { margin: 1em 0 }
h5              { font-size: .83em; margin: 1.5em 0 }
h6              { font-size: .75em; margin: 1.67em 0 }
h1, h2, h3, h4,
h5, h6, b,
strong          { font-weight: bolder }
blockquote,
figure          { margin-left: 40px; margin-right: 40px }
fieldset        { margin-left: 2px; margin-right: 2px; border: 2px groove rgb(192, 192, 192); padding: 0.35em 0.75em 0.625em }
i, cite, em,
var, address    { font-style: italic }
pre, tt, code,
kbd, samp       { font-family: monospace }
pre             { white-space: pre }
button, textarea,
input, select   { display: inline-block }
input, textarea,
select, button  { border: 1px solid rgb(118, 118, 118); padding: 2px 4px; background-color: rgb(255, 255, 255) }
select, button,
input[type=button],
input[type=submit],
input[type=reset],
input[type=file] { background-color: rgb(232, 232, 232) }
button,
input[type=button],
input[type=submit],
input[type=reset],
input[type=file] { padding: 2px 10px }
input[type=color] { padding: 2px }
input[type=checkbox],
input[type=radio] { padding: 0px; margin: 3px 3px 3px 4px }
input[type=radio] { border-radius: 50% }
input[type=range] { border: none; padding: 0px; background-color: transparent }
input[type=hidden] { display: none }
textarea        { white-space: pre-wrap }
big             { font-size: 1.17em }
small, sub, sup { font-size: .83em }
sub             { vertical-align: sub }
sup             { vertical-align: super }
table           { border-spacing: 2px; box-sizing: border-box; text-indent: 0 }
thead, tbody,
tfoot           { vertical-align: middle }
td, th, tr      { vertical-align: inherit }
s, strike, del  { text-decoration: line-through }
hr              { border: 1px inset; margin: 0.5em auto }
ol, ul, dir,
menu            { padding-left: 40px; list-style-type: disc; list-style-position: outside }
dd              { margin-left: 40px }
ol              { list-style-type: decimal }
ol ul, ul ol,
ul ul, ol ol    { margin-top: 0; margin-bottom: 0 }
u, ins          { text-decoration: underline }
abbr[title],
acronym[title]  { text-decoration: dotted underline }
mark            { background-color: yellow; color: black }
br::before      { content: '\A'; white-space: pre-line }
q::before       { content: open-quote }
q::after        { content: close-quote }
*::before, *::after { display: inline }
center          { text-align: center }
:link           { color: #0000ee }
:visited        { color: #551a8b }
:link, :visited { text-decoration: underline }
:focus          { outline: thin dotted invert }

/* Begin bidirectionality settings (do not change) */
BDO[DIR='ltr']  { direction: ltr; unicode-bidi: bidi-override }
BDO[DIR='rtl']  { direction: rtl; unicode-bidi: bidi-override }

[dir=ltr i]     { direction: ltr }
[dir=rtl i]     { direction: rtl }

@media print {
  h1            { page-break-before: always }
  h1, h2, h3,
  h4, h5, h6    { page-break-after: avoid }
  ul, ol, dl    { page-break-before: avoid }
}";

        #endregion
    }
}
