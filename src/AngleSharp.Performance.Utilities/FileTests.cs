namespace AngleSharp.Performance
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public sealed class FileTests
    {
        readonly List<ITest> _tests;

        public FileTests()
        {
            _tests = new List<ITest>();
        }

        public List<ITest> Tests => _tests;

        public FileTests IncludeFromDirectory(string directory, string pattern = "*.css")
        {
            var files = Directory.GetFiles(directory, pattern);
            Array.Sort(files);

            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var source = File.ReadAllText(file);
                _tests.Add(new StandardTest(name, source));
            }

            return this;
        }

        public FileTests Include(params string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                var name = Path.GetFileNameWithoutExtension(filePath);
                var source = File.ReadAllText(filePath);
                _tests.Add(new StandardTest(name, source));
            }

            return this;
        }
    }
}
