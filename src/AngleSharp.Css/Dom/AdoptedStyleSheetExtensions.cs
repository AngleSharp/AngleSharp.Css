namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Provides adopted stylesheet access for documents.
    /// </summary>
    [DomExposed("Document")]
    public static class DocumentAdoptedStyleSheetExtensions
    {
        /// <summary>
        /// Gets the adopted stylesheets of the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The live adopted stylesheet collection.</returns>
        [DomName("adoptedStyleSheets")]
        [DomAccessor(Accessors.Getter)]
        public static IList<ICssStyleSheet> GetAdoptedStyleSheets(this IDocument document) =>
            AdoptedStyleSheetStore.Get(document);

        /// <summary>
        /// Sets the adopted stylesheets of the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="sheets">The stylesheets to adopt.</param>
        [DomName("adoptedStyleSheets")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAdoptedStyleSheets(this IDocument document, IEnumerable<ICssStyleSheet> sheets) =>
            AdoptedStyleSheetStore.Set(document, sheets);
    }

    /// <summary>
    /// Provides adopted stylesheet access for shadow roots.
    /// </summary>
    [DomExposed("ShadowRoot")]
    public static class ShadowRootAdoptedStyleSheetExtensions
    {
        /// <summary>
        /// Gets the adopted stylesheets of the shadow root.
        /// </summary>
        /// <param name="shadowRoot">The shadow root.</param>
        /// <returns>The live adopted stylesheet collection.</returns>
        [DomName("adoptedStyleSheets")]
        [DomAccessor(Accessors.Getter)]
        public static IList<ICssStyleSheet> GetAdoptedStyleSheets(this IShadowRoot shadowRoot) =>
            AdoptedStyleSheetStore.Get(shadowRoot);

        /// <summary>
        /// Sets the adopted stylesheets of the shadow root.
        /// </summary>
        /// <param name="shadowRoot">The shadow root.</param>
        /// <param name="sheets">The stylesheets to adopt.</param>
        [DomName("adoptedStyleSheets")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAdoptedStyleSheets(this IShadowRoot shadowRoot, IEnumerable<ICssStyleSheet> sheets) =>
            AdoptedStyleSheetStore.Set(shadowRoot, sheets);

        internal static IDocument GetOwnerDocument(IShadowRoot shadowRoot) =>
            shadowRoot.Host.Owner ?? throw new InvalidOperationException("The shadow root's host must be associated with a document.");
    }

    /// <summary>
    /// Provides the effective stylesheet sequence for each style root.
    /// </summary>
    public static class EffectiveStyleSheetExtensions
    {
        /// <summary>
        /// Gets a document's own stylesheets followed by its adopted stylesheets.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The stylesheets in cascade order.</returns>
        public static IEnumerable<ICssStyleSheet> GetEffectiveStyleSheets(this IDocument document) =>
            document.GetStyleSheets().OfType<ICssStyleSheet>().Concat(document.GetAdoptedStyleSheets());

        /// <summary>
        /// Gets a shadow root's own stylesheets followed by its adopted stylesheets.
        /// </summary>
        /// <param name="shadowRoot">The shadow root.</param>
        /// <returns>The stylesheets in cascade order.</returns>
        public static IEnumerable<ICssStyleSheet> GetEffectiveStyleSheets(this IShadowRoot shadowRoot) =>
            shadowRoot.StyleSheets.OfType<ICssStyleSheet>().Concat(shadowRoot.GetAdoptedStyleSheets());
    }

    static class AdoptedStyleSheetStore
    {
        private static readonly ConditionalWeakTable<Object, AdoptedStyleSheetList> _collections = new();

        public static IList<ICssStyleSheet> Get(Object root) =>
            _collections.GetValue(root, key => new AdoptedStyleSheetList(key));

        public static void Set(Object root, IEnumerable<ICssStyleSheet> sheets)
        {
            ((AdoptedStyleSheetList)Get(root)).Replace(sheets);
        }
    }

    sealed class AdoptedStyleSheetList : IList<ICssStyleSheet>
    {
        private readonly Object _root;
        private readonly List<ICssStyleSheet> _sheets = new();

        public AdoptedStyleSheetList(Object root)
        {
            _root = root;
        }

        public ICssStyleSheet this[Int32 index]
        {
            get => _sheets[index];
            set
            {
                Validate(value);
                _sheets[index] = value;
            }
        }

        public Int32 Count => _sheets.Count;

        public Boolean IsReadOnly => false;

        public void Add(ICssStyleSheet item)
        {
            Validate(item);
            _sheets.Add(item);
        }

        public void Clear() => _sheets.Clear();

        public Boolean Contains(ICssStyleSheet item) => _sheets.Contains(item);

        public void CopyTo(ICssStyleSheet[] array, Int32 arrayIndex) => _sheets.CopyTo(array, arrayIndex);

        public IEnumerator<ICssStyleSheet> GetEnumerator() => _sheets.GetEnumerator();

        public Int32 IndexOf(ICssStyleSheet item) => _sheets.IndexOf(item);

        public void Insert(Int32 index, ICssStyleSheet item)
        {
            Validate(item);
            _sheets.Insert(index, item);
        }

        public Boolean Remove(ICssStyleSheet item) => _sheets.Remove(item);

        public void RemoveAt(Int32 index) => _sheets.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Replace(IEnumerable<ICssStyleSheet> sheets)
        {
            if (sheets is null)
            {
                throw new ArgumentNullException(nameof(sheets));
            }

            var replacement = sheets.ToList();

            foreach (var sheet in replacement)
            {
                Validate(sheet);
            }

            _sheets.Clear();
            _sheets.AddRange(replacement);
        }

        private void Validate(ICssStyleSheet sheet)
        {
            if (sheet is not CssStyleSheet { IsConstructed: true } constructedSheet ||
                !ReferenceEquals(constructedSheet.OwnerDocument, GetOwnerDocument()))
            {
                throw new DomException("NotAllowedError");
            }
        }

        private IDocument GetOwnerDocument() => _root is IDocument document
            ? document
            : _root is IShadowRoot shadowRoot
                ? ShadowRootAdoptedStyleSheetExtensions.GetOwnerDocument(shadowRoot)
                : throw new InvalidOperationException("The style root must be a document or shadow root.");
    }
}
