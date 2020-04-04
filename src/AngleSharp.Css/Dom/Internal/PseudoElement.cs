namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;
    using System.IO;

    /// <summary>
    /// A wrapper around an element to extend the DOM.
    /// </summary>
    sealed class PseudoElement : IElement, IPseudoElement
    {
        #region Fields

        private readonly IElement _host;
        private readonly String _name;

        #endregion

        #region ctor

        public PseudoElement(IElement host, String name)
        {
            _host = host;
            _name = name;
        }

        #endregion

        #region Properties

        public IElement AssignedSlot => _host.AssignedSlot;

        public String Slot
        {
            get => _host.Slot;
            set { }
        }

        public ISourceReference SourceReference => null;

        public IShadowRoot ShadowRoot => _host.ShadowRoot;

        public String Prefix => _host.Prefix;

        public String PseudoName => _name;

        public String LocalName => _host.LocalName;

        public String NamespaceUri => _host.NamespaceUri;

        public INamedNodeMap Attributes => _host.Attributes;

        public ITokenList ClassList => _host.ClassList;

        public String ClassName
        {
            get => _host.ClassName;
            set { }
        }

        public String Id
        {
            get => _host.Id;
            set { }
        }

        public String InnerHtml
        {
            get => String.Empty;
            set { }
        }

        public String OuterHtml
        {
            get => String.Empty;
            set { }
        }

        public String TagName => _host.TagName;

        public Boolean IsFocused => _host.IsFocused;

        public String BaseUri => _host.BaseUri;

        public Url BaseUrl => _host.BaseUrl;

        public String NodeName => _host.NodeName;

        public INodeList ChildNodes => _host.ChildNodes;

        public IDocument Owner => _host.Owner;

        public IElement ParentElement => _host.ParentElement;

        public INode Parent => _host.Parent;

        public INode FirstChild => _host.FirstChild;

        public INode LastChild => _host.LastChild;

        public INode NextSibling => _host.NextSibling;

        public INode PreviousSibling => _host.PreviousSibling;

        public NodeType NodeType => NodeType.Element;

        public String NodeValue
        {
            get => _host.NodeValue;
            set { }
        }

        public String TextContent
        {
            get => String.Empty;
            set { }
        }

        public Boolean HasChildNodes => _host.HasChildNodes;

        public IHtmlCollection<IElement> Children => _host.Children;

        public IElement FirstElementChild => _host.FirstElementChild;

        public IElement LastElementChild => _host.LastElementChild;

        public Int32 ChildElementCount => _host.ChildElementCount;

        public IElement NextElementSibling => _host.NextElementSibling;

        public IElement PreviousElementSibling => _host.PreviousElementSibling;

        public NodeFlags Flags => _host.Flags;


        #endregion

        #region Methods

        public IShadowRoot AttachShadow(ShadowRootMode mode = ShadowRootMode.Open)
        {
            return _host.AttachShadow(mode);
        }

        public void Insert(AdjacentPosition position, String html)
        {
        }

        public Boolean HasAttribute(String name)
        {
            return _host.HasAttribute(name);
        }

        public Boolean HasAttribute(String namespaceUri, String localName)
        {
            return _host.HasAttribute(namespaceUri, localName);
        }

        public String GetAttribute(String name)
        {
            return _host.GetAttribute(name);
        }

        public String GetAttribute(String namespaceUri, String localName)
        {
            return _host.GetAttribute(namespaceUri, localName);
        }

        public void SetAttribute(String name, String value)
        {
            _host.SetAttribute(name, value);
        }

        public void SetAttribute(String namespaceUri, String name, String value)
        {
            _host.SetAttribute(namespaceUri, name, value);
        }

        public Boolean RemoveAttribute(String name)
        {
            return _host.RemoveAttribute(name);
        }

        public Boolean RemoveAttribute(String namespaceUri, String localName)
        {
            return _host.RemoveAttribute(namespaceUri, localName);
        }

        public IHtmlCollection<IElement> GetElementsByClassName(String classNames)
        {
            return _host.GetElementsByClassName(classNames);
        }

        public IHtmlCollection<IElement> GetElementsByTagName(String tagName)
        {
            return _host.GetElementsByTagName(tagName);
        }

        public IHtmlCollection<IElement> GetElementsByTagNameNS(String namespaceUri, String tagName)
        {
            return _host.GetElementsByTagNameNS(namespaceUri, tagName);
        }

        public Boolean Matches(String selectors)
        {
            return _host.Matches(selectors);
        }

        public INode Clone(Boolean deep = true)
        {
            return _host.Clone(deep);
        }

        public IPseudoElement Pseudo(String pseudoElement)
        {
            return null;
        }

        public Boolean Equals(INode otherNode)
        {
            return _host.Equals(otherNode);
        }

        public DocumentPositions CompareDocumentPosition(INode otherNode)
        {
            return _host.CompareDocumentPosition(otherNode);
        }

        public void Normalize()
        {
            _host.Normalize();
        }

        public Boolean Contains(INode otherNode)
        {
            return _host.Contains(otherNode);
        }

        public Boolean IsDefaultNamespace(String namespaceUri)
        {
            return _host.IsDefaultNamespace(namespaceUri);
        }

        public String LookupNamespaceUri(String prefix)
        {
            return _host.LookupNamespaceUri(prefix);
        }

        public String LookupPrefix(String namespaceUri)
        {
            return _host.LookupPrefix(namespaceUri);
        }

        public INode AppendChild(INode child)
        {
            return _host.AppendChild(child);
        }

        public INode InsertBefore(INode newElement, INode referenceElement)
        {
            return _host.InsertBefore(newElement, referenceElement);
        }

        public INode RemoveChild(INode child)
        {
            return _host.RemoveChild(child);
        }

        public INode ReplaceChild(INode newChild, INode oldChild)
        {
            return _host.ReplaceChild(newChild, oldChild);
        }

        public void AddEventListener(String type, DomEventHandler callback = null, Boolean capture = false)
        {
            _host.AddEventListener(type, callback, capture);
        }

        public void RemoveEventListener(String type, DomEventHandler callback = null, Boolean capture = false)
        {
            _host.RemoveEventListener(type, callback, capture);
        }

        public void InvokeEventListener(Event ev)
        {
            _host.InvokeEventListener(ev);
        }

        public Boolean Dispatch(Event ev)
        {
            return _host.Dispatch(ev);
        }

        public void Append(params INode[] nodes)
        {
            _host.Append(nodes);
        }

        public void Prepend(params INode[] nodes)
        {
            _host.Prepend(nodes);
        }

        public IElement QuerySelector(String selectors)
        {
            return _host.QuerySelector(selectors);
        }

        public IHtmlCollection<IElement> QuerySelectorAll(String selectors)
        {
            return _host.QuerySelectorAll(selectors);
        }

        public void Before(params INode[] nodes)
        {
            _host.Before(nodes);
        }

        public void After(params INode[] nodes)
        {
            _host.After(nodes);
        }

        public void Replace(params INode[] nodes)
        {
            _host.Replace(nodes);
        }

        public void Remove()
        {
            _host.Remove();
        }

        public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            _host.ToHtml(writer, formatter);
        }

        public IElement Closest(String selectors)
        {
            return _host.Closest(selectors);
        }

        #endregion
    }
}
