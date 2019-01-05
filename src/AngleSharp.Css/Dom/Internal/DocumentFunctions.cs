namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    sealed class DocumentFunctions : IDocumentFunctions
    {
        private readonly List<IDocumentFunction> _functions;

        public DocumentFunctions()
            : this(new List<IDocumentFunction>())
        {
        }

        public DocumentFunctions(List<IDocumentFunction> functions)
        {
            _functions = functions;
        }

        public IDocumentFunction this[Int32 index]
        {
            get { return _functions[index]; }
        }

        public Int32 Length
        {
            get { return _functions.Count; }
        }

        public IEnumerator<IDocumentFunction> GetEnumerator()
        {
            return _functions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IDocumentFunction function)
        {
            _functions.Add(function);
        }

        public void Remove(IDocumentFunction function)
        {
            _functions.Remove(function);
        }

        public void Clear()
        {
            _functions.Clear();
        }

        public void AddRange(IEnumerable<IDocumentFunction> functions)
        {
            _functions.AddRange(functions);
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_functions.Count > 0)
            {
                _functions[0].ToCss(writer, formatter);

                for (var i = 1; i < _functions.Count; i++)
                {
                    writer.Write(", ");
                    _functions[i].ToCss(writer, formatter);
                }
            }
        }
    }
}
