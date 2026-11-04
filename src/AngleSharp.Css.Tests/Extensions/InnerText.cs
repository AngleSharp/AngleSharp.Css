#nullable disable
namespace AngleSharp.Css.Tests.Extensions
{
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class InnerTextTests
    {
        [TestCase(null, "", "")]
        [TestCase("", "", "")]
        [TestCase("test", "test", "test")]
        [TestCase("test1\ntest2\ntest3", "test1\ntest2\ntest3", "test1<br>test2<br>test3")]
        [TestCase("te\rst1\r\ntest2\ntest3\r", "te\nst1\ntest2\ntest3\n", "te<br>st1<br>test2<br>test3<br>")]
        [TestCase("te st1\nte  st2\nte   st3", "te st1\nte st2\nte st3", "te st1<br>te  st2<br>te   st3")]
        public void SetInnerText(String fixture, String expectedInnerText, String expectedHtml)
        {
            var config = Configuration.Default.WithCss();
            var doc = ("<div>sample content</div>").ToHtmlDocument(config);

            doc.Body.SetInnerText(fixture);

            Assert.AreEqual(expectedInnerText, doc.Body.GetInnerText());
            Assert.AreEqual(expectedHtml, doc.Body.InnerHtml);
        }

        // text & spaces
        [TestCase("test", "test")]
        [TestCase("  test  ", "test")]
        [TestCase("  ", "")]
        [TestCase("&nbsp;&nbsp;", "  ")] // these are non breaking spaces
        [TestCase(" &nbsp; test &nbsp; ", "  test  ")]
        [TestCase(" 1&nbsp;2 <span> 3&nbsp;4  5&nbsp;6 </span> 7&nbsp;8 ", "1 2 3 4 5 6 7 8")]
        [TestCase("<span> test 1 </span><span> test 2 </span><span> test 3 </span>", "test 1 test 2 test 3")]
        [TestCase("<span> test 1 <span></span></span>", "test 1")]
        [TestCase("test1 <br> test2 <br> test3", "test1\ntest2\ntest3")]
        // paragraph
        [TestCase("<p>test</p>", "test")]
        [TestCase("<p>test1</p><p>test2</p>", "test1\n\ntest2")]
        [TestCase("<p>test1</p>\n<p>test2</p>", "test1\n\ntest2")]
        [TestCase("<p>test1</p>\n \n <p>test2</p>", "test1\n\ntest2")]
        [TestCase("<p>test1</p>a\n \n b<p>test2</p>", "test1\n\na b\n\ntest2")]
        // block-level
        [TestCase("<div>test1</div><div>test2</div><div>test3</div>", "test1\ntest2\ntest3")]
        [TestCase(@"test1<span style=""display:block"">test2</span>test3", "test1\ntest2\ntest3")]
        // line break
        [TestCase("test1<br>test2<br>test3", "test1\ntest2\ntest3")]
        // table
        [TestCase("<table><tr><td>1</td><td>2</td></tr><tr><td>3</td><td>4</td></tr></table>", "1\t2\n3\t4")]
        [TestCase("<table><tr><td>1</td> <td>2</td></tr> <tr><td>3</td> <td>4</td></tr></table>", "1\t2\n3\t4")]
        [TestCase("<table><tr><td>1</td><td>2</td></tr><tr><td><table><tr><td>3</td><td>4</td></tr></table></td><td>5</td></tr></table>", "1\t2\n\n3\t4\n\t5")]
        // select
        [TestCase("<select><option>test1</option><option>test2</option></select>", "test1\ntest2")]
        // style visibility
        [TestCase(@"<div hidden style=""display:block"">test1<br>test2<div>test3</div></div>", "test1\ntest2\ntest3")]
        [TestCase(@"<div hidden style=""visibility:visible"">test1<br>test2<div>test3</div></div>", "test1\ntest2\ntest3")]
        [TestCase("<div hidden>test1<br>test2<div>test3</div></div>", "")]
        [TestCase(@"<div style=""display:none"">test1<br>test2<div>test3</div></div>", "")]
        [TestCase(@"<div hidden style=""display:block;visibility:hidden;"">test1<br>test2<div>test3</div></div>", "")]
        [TestCase(@"<div hidden style=""display:none;visibility:visible;"">test1<br>test2<div>test3</div></div>", "")]
        // style text-transform
        [TestCase(@"<span style=""text-transform:uppercase"">test</span>", "TEST")]
        [TestCase(@"<span style=""text-transform:lowercase"">TEST</span>", "test")]
        [TestCase("<span style=\"text-transform:capitalize\">test1 test2\ntest3</span>", "Test1 Test2 Test3")]
        [TestCase(@"<div style=""text-transform:lowercase"">TEST1<span>TEST2</span></div>", "test1test2")]
        [TestCase(@"<div style=""text-transform:lowercase"">TEST1<span style=""text-transform:uppercase"">test2</span></div>", "test1TEST2")]
        // style white-space
        [TestCase("t e  s\tt1\ntest2", "t e s t1 test2")]
        [TestCase("<span style=\"white-space:normal\">t e  s\tt1\ntest2</span>", "t e s t1 test2")]
        [TestCase("<span style=\"white-space:nowrap\">t e  s\tt1\ntest2</span>", "t e s t1 test2")]
        [TestCase("<span style=\"white-space:pre-line\">t e  s\tt1\ntest2</span>", "t e s t1\ntest2")]
        [TestCase("<span style=\"white-space:pre\">t e  s\tt1\ntest2</span>", "t e  s\tt1\ntest2")]
        [TestCase("<span style=\"white-space:pre-wrap\">t e  s\tt1\ntest2</span>", "t e  s\tt1\ntest2")]
        // no css box
        [TestCase("<textarea>test</textarea>", "")]
        [TestCase("<script>test</noscript>", "")]
        [TestCase("<style>test</style>", "")]
        [TestCase("<!-- comment -->Text<!-- comment --> with <!-- comment -->comments<!-- comment -->", "Text with comments")]
        public void GetInnerText(String fixture, String expected)
        {
            var defaultSheet = new CssDefaultStyleSheetProvider();
            defaultSheet.SetDefault("");
            var config = Configuration.Default.With(defaultSheet).WithCss();
            var doc = fixture.ToHtmlDocument(config);

            Assert.AreEqual(expected, doc.Body.GetInnerText());
        }

        [Test]
        public void SpanShouldNotHaveRemovedSpaces_Issue32()
        {
            var config = Configuration.Default.WithCss();
            var document = ("<div><div>Div with <span>a span</span> in it.</div></div>").ToHtmlDocument(config);
            var element = document.QuerySelector("div");

            Assert.AreEqual("Div with a span in it.", element.GetInnerText());
        }

        // the rendered text walks the element's own children: a shadow tree is left out, and
        // a light child only counts if it is rendered, i.e. assigned to a slot
        [TestCase("<p>shadow</p><slot></slot>", "<span>light</span>", "light")]
        [TestCase("<p>shadow</p>", "<span>light</span>", "")]
        [TestCase("<div><slot name=b></slot></div><slot></slot>", "<span slot=b>b</span><p>a</p>", "b\n\na")]
        [TestCase("<slot name=b></slot>", "<span slot=b>b</span><span>unassigned</span>", "b")]
        public void ShadowHostGivesTheTextOfItsRenderedChildren(String shadow, String light, String expected)
        {
            var document = "<div id=host></div>".ToHtmlDocument(Configuration.Default.WithCss());
            var host = document.GetElementById("host");
            host.AttachShadow().InnerHtml = shadow;
            host.InnerHtml = light;

            Assert.AreEqual(expected, host.GetInnerText());
        }

        [Test]
        public void GeneratedContentIsNotPartOfTheText()
        {
            var document = "<style>p::before { content: 'B' } p::after { content: 'A' }</style><p id=target>x<br>y</p>".ToHtmlDocument(Configuration.Default.WithCss());

            Assert.AreEqual("x\ny", document.GetElementById("target").GetInnerText());
        }

        [TestCase("<p>a</p><div style=\"display: none\">b</div><table><tr><td>1</td><td>2</td></tr></table><span style=\"text-transform: uppercase\">c</span>")]
        [TestCase("<ul><li>a<li style=\"visibility: hidden\">b<li>c</ul><pre> x\n  y </pre>")]
        public void TextOfARenderedNodeIsTheTextOfItsElement(String html)
        {
            var document = $"<div id=target>{html}</div>".ToHtmlDocument(Configuration.Default.WithCss());
            var target = document.GetElementById("target");
            var node = (ElementRenderNode)document.DefaultView.Render().Find(target);

            Assert.AreEqual(target.GetInnerText(), node.GetInnerText());
        }

        // the walk of an element releases what it has walked, the walk of a rendered node does not
        [Test]
        public void TextOfAShadowHostIsTheTextOfItsRenderedNode()
        {
            var document = "<div id=target><p>a</p><div id=host><span slot=s>b</span><table><tr><td>1</td><td>2</td></tr><tr><td>3</td></tr></table></div><p>c</p></div>".ToHtmlDocument(Configuration.Default.WithCss());
            var target = document.GetElementById("target");
            document.GetElementById("host").AttachShadow().InnerHtml = "<div><slot name=s></slot></div><ul><li><slot></slot></ul>";
            var node = (ElementRenderNode)document.DefaultView.Render().Find(target);

            Assert.AreEqual("a\n\nb\n1\t2\n3\n\nc", target.GetInnerText());
            Assert.AreEqual(target.GetInnerText(), node.GetInnerText());
        }
    }
}
