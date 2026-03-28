namespace AngleSharp.Performance.Css
{
    using System;
    using ExCSS;

    class ExCssParser : ITestee
    {
        public String Name => "ExCSS";

        public Type Library => typeof(StylesheetParser);

        public void Run(String source)
        {
            var parser = new StylesheetParser();
            parser.Parse(source);
        }
    }
}
