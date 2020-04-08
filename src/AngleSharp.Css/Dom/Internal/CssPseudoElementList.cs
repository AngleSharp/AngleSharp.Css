namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class CssPseudoElementList : ICssPseudoElementList
    {
        private readonly ICssPseudoElement[] _elements;

        public CssPseudoElementList(IEnumerable<ICssPseudoElement> elements)
        {
            _elements = elements.ToArray();
        }

        public ICssPseudoElement this[Int32 index] => index >= 0 && index < _elements.Length ? _elements[index] : null;

        public ICssPseudoElement this[String type] => _elements.FirstOrDefault(m => m.Type == type);

        public Int32 Length => _elements.Length;
    }
}
