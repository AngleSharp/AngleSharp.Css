namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using BenchmarkDotNet.Attributes;
    using System.Collections.Generic;
    using System;
    using System.Text;

    [MemoryDiagnoser]
    [MediumRunJob]
    public class InnerTextBenchmarks
    {
        private IElement _realWorldRoot = null!;
        private IElement _selectorHeavyRoot = null!;

        [GlobalSetup]
        public void Setup()
        {
            var config = Configuration.Default.WithCss();
            var context = BrowsingContext.New(config);

            var realWorldDocument = context.OpenAsync(req => req.Content(BuildFixture())).GetAwaiter().GetResult();
            _realWorldRoot = realWorldDocument.QuerySelector("#root")!;

            var selectorHeavyDocument = context.OpenAsync(req => req.Content(BuildSelectorHeavyFixture())).GetAwaiter().GetResult();
            _selectorHeavyRoot = selectorHeavyDocument.QuerySelector("#root")!;
        }

        [Benchmark(Baseline = true)]
        public Int32 GetInnerText_RealWorld_Length()
        {
            return _realWorldRoot.GetInnerText().Length;
        }

        [Benchmark]
        public Int32 GetInnerText_SelectorHeavy_Length()
        {
            return _selectorHeavyRoot.GetInnerText().Length;
        }

        [Benchmark]
        public Int32 Experimental_RealWorld_Length()
        {
            return GetInnerTextExperimental(_realWorldRoot).Length;
        }

        [Benchmark]
        public Int32 Experimental_SelectorHeavy_Length()
        {
            return GetInnerTextExperimental(_selectorHeavyRoot).Length;
        }

        private static String BuildFixture()
        {
            var sb = new StringBuilder();
            sb.Append("<!doctype html><html><head><style>");
            sb.Append("body{font-family:sans-serif;} ");
            sb.Append(".hidden{display:none;} .invisible{visibility:hidden;} ");
            sb.Append(".block{display:block;} .row{display:table-row;} .cell{display:table-cell;} ");
            sb.Append(".caps{text-transform:uppercase;} .keep{white-space:pre-wrap;} ");
            sb.Append(".normal{white-space:normal;} .preline{white-space:pre-line;} ");
            sb.Append("</style></head><body><main id='root'>");

            for (var i = 0; i < 180; i++)
            {
                sb.Append("<section>");
                sb.Append("<h2>Section ").Append(i).Append("</h2>");
                sb.Append("<p class='normal'>Lorem ipsum dolor sit amet, consectetur adipiscing elit ").Append(i).Append(".</p>");
                sb.Append("<div><span class='caps'>headline ").Append(i).Append("</span><span> details ").Append(i).Append(".</span></div>");
                sb.Append("<div class='keep'>line A\nline B\nline C ").Append(i).Append("</div>");
                sb.Append("<div class='preline'>a   b\n c\t d ").Append(i).Append("</div>");

                sb.Append("<table><tbody>");

                for (var r = 0; r < 4; r++)
                {
                    sb.Append("<tr class='row'>");

                    for (var c = 0; c < 6; c++)
                    {
                        sb.Append("<td class='cell'>R").Append(r).Append("C").Append(c).Append("-").Append(i).Append("</td>");
                    }

                    sb.Append("</tr>");
                }

                sb.Append("</tbody></table>");
                sb.Append("<ul><li>alpha ").Append(i).Append("</li><li>beta ").Append(i).Append("</li><li>gamma ").Append(i).Append("</li></ul>");
                sb.Append("<div hidden>hidden via attribute ").Append(i).Append("</div>");
                sb.Append("<div class='hidden'>hidden via css ").Append(i).Append("</div>");
                sb.Append("<div class='invisible'>hidden via visibility ").Append(i).Append("</div>");
                sb.Append("<p>tail ").Append(i).Append(" <br> tail2 ").Append(i).Append("</p>");
                sb.Append("</section>");
            }

            sb.Append("</main></body></html>");
            return sb.ToString();
        }

        private static String BuildSelectorHeavyFixture()
        {
            var sb = new StringBuilder();
            sb.Append("<!doctype html><html><head><style>");

            for (var i = 0; i < 1200; i++)
            {
                var bucket = i % 16;
                var level = i % 12;
                sb.Append("#root .bucket-").Append(bucket)
                    .Append(" .level-").Append(level)
                    .Append(" .item-").Append(i)
                    .Append("[data-k='").Append(i % 13).Append("']")
                    .Append("{display:block;white-space:normal;text-transform:none;}");
                sb.Append("#root .bucket-").Append(bucket)
                    .Append(" .level-").Append(level)
                    .Append(" .item-").Append(i)
                    .Append(" > span.mark-").Append(i % 7)
                    .Append("{white-space:pre-line;}");
            }

            sb.Append(".hidden{display:none;} .nowrap{white-space:nowrap;} .caps{text-transform:uppercase;}");
            sb.Append("</style></head><body><main id='root'>");

            for (var s = 0; s < 220; s++)
            {
                sb.Append("<section class='bucket-").Append(s % 16).Append("'>");

                for (var l = 0; l < 8; l++)
                {
                    sb.Append("<div class='level-").Append(l % 12).Append("'>");

                    for (var i = 0; i < 7; i++)
                    {
                        var id = s * 56 + l * 7 + i;
                        sb.Append("<article class='item-").Append(id % 1200)
                            .Append("' data-k='").Append(id % 13).Append("'>");
                        sb.Append("<span class='mark-").Append(id % 7).Append("'>alpha ").Append(id).Append("</span>");
                        sb.Append("<span class='nowrap'> beta ").Append(id).Append("  gamma</span>");
                        sb.Append("<span class='caps'> delta ").Append(id).Append("</span>");
                        sb.Append("<div hidden>hidden ").Append(id).Append("</div>");
                        sb.Append("<div class='hidden'>also hidden ").Append(id).Append("</div>");
                        sb.Append("<p>tail ").Append(id).Append(" <br> tail2 ").Append(id).Append("</p>");
                        sb.Append("</article>");
                    }

                    sb.Append("</div>");
                }

                sb.Append("</section>");
            }

            sb.Append("</main></body></html>");
            return sb.ToString();
        }

        private static String GetInnerTextExperimental(IElement element)
        {
            var sb = new StringBuilder();
            CollectText(element, sb);
            return NormalizeWhitespace(sb.ToString());
        }

        private static void CollectText(INode node, StringBuilder sb)
        {
            if (node is IText text)
            {
                sb.Append(text.Data);
                return;
            }

            if (node is IHtmlBreakRowElement)
            {
                sb.Append('\n');
                return;
            }

            if (node is IElement element && IsExcludedNode(element.NodeName))
            {
                return;
            }

            foreach (var child in node.ChildNodes)
            {
                CollectText(child, sb);
            }
        }

        private static String NormalizeWhitespace(String input)
        {
            var sb = new StringBuilder(input.Length);
            var inWhitespace = true;

            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (Char.IsWhiteSpace(c))
                {
                    if (!inWhitespace)
                    {
                        sb.Append(' ');
                        inWhitespace = true;
                    }

                    continue;
                }

                sb.Append(c);
                inWhitespace = false;
            }

            if (sb.Length > 0 && sb[sb.Length - 1] == ' ')
            {
                sb.Length--;
            }

            return sb.ToString();
        }

        private static Boolean IsExcludedNode(String nodeName)
        {
            return nodeName switch
            {
                "CANVAS" or "COL" or "COLGROUP" or "DETAILS" or "FRAME" or "FRAMESET" or "IFRAME" or "IMG" or "INPUT" or "LINK" or "METER" or "PROGRESS" or "TEMPLATE" or "TEXTAREA" or "VIDEO" or "WBR" or "SCRIPT" or "STYLE" or "NOSCRIPT" => true,
                _ => false,
            };
        }
    }
}