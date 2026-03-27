namespace AngleSharp.Performance.Css
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        static void Main(String[] args)
        {
            var samplesDir = Path.Combine(AppContext.BaseDirectory, "Samples");
            var stylesheets = new FileTests()
                .IncludeFromDirectory(samplesDir);

            var parsers = new List<ITestee>
            {
                new AngleSharpParser(),
                new ExCssParser(),
                new CsCssParser(),
            };

            var testsuite = new TestSuite(parsers, stylesheets.Tests, new Output(), new Warmup())
            {
                NumberOfRepeats = 5,
                NumberOfReRuns = 1,
            };

            testsuite.Run();
        }
    }
}
