#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a single CSS declaration block.
    /// </summary>
    sealed class CssStyleDeclaration : ICssStyleDeclaration
    {
        #region Fields

        private List<ICssProperty> _declarations;
        private Dictionary<String, Int32> _declarationIndex;
        private readonly IBrowsingContext _context;
        private ICssRule _parent;
        private Boolean _updating;

        // The declarations inherited from the parent are not copied, but looked up in
        // a snapshot of the parent until the full list is requested (see Materialize).
        private Snapshot _inherited;
        // Names of own declarations (e.g. "color: inherit") replaced by the parent's.
        private HashSet<String> _forced;
        // The snapshot of this declaration handed out to children; while it exists the
        // lists above are shared with it and must be copied before they are modified.
        private Snapshot _snapshot;

        #endregion

        #region Events

        public event Action<String> Changed;

        #endregion

        #region ctor

        public CssStyleDeclaration(IBrowsingContext context)
        {
            _declarations = new List<ICssProperty>();
            _declarationIndex = new Dictionary<String, Int32>(StringComparer.Ordinal);
            _context = context;
        }

        public CssStyleDeclaration(ICssRule parent)
            : this(parent.Owner?.Context)
        {
            _parent = parent;
        }

        #endregion

        #region Index

        public String this[Int32 index]
        {
            get
            {
                Materialize();
                return _declarations[index]?.Name;
            }
        }

        public String this[String name] => GetPropertyValue(name);

        #endregion

        #region Properties

        public IEnumerable<ICssProperty> Declarations
        {
            get
            {
                Materialize();
                return _declarations;
            }
        }

        public String CssText
        {
            get => this.ToCss();
            set { Update(value); RaiseChanged(); }
        }

        public Boolean IsReadOnly => _context == null;

        public Int32 Length
        {
            get
            {
                Materialize();
                return _declarations.Count;
            }
        }

        public ICssRule Parent => _parent;

        /// <summary>
        /// Gets or sets the computed font size (in px) of the root element, which is carried
        /// along from computed style to computed style to resolve rem units.
        /// </summary>
        internal Double? RootFontSize { get; set; }

        #endregion

        #region Methods

        public ICssProperty GetProperty(String name)
        {
            name = name.StartsWith("--", StringComparison.Ordinal) ? name : name.ToLowerFast();
            var declaration = GetDeclaration(name);

            if (declaration is not null)
            {
                return declaration;
            }

            var related = GetPropertyFromShorthand(name);

            if (related is not null)
            {
                return related;
            }

            return GetPropertyShorthand(name);
        }

        public void SetParent(ICssRule parent) => _parent = parent;

        public void Update(String value)
        {
            if (IsReadOnly)
                throw new DomException(DomError.NoModificationAllowed);

            if (!_updating)
            {
                EnsureWritable();
                _inherited = null;
                _forced = null;
                _declarations.Clear();
                _declarationIndex.Clear();

                if (!String.IsNullOrEmpty(value))
                {
                    var parser = _context.GetService<ICssParser>();
                    var decl = parser?.ParseDeclaration(value);

                    if (decl != null)
                    {
                        _declarations.AddRange(decl);
                        RebuildIndex();
                    }
                }
            }
        }

        private ICssProperty TryCreateShorthand(String shorthandName, IEnumerable<String> serialized, List<String> usedProperties, Boolean force)
        {
            var factory = _context.GetDeclarationFactory();
            var shorthand = factory.Create(shorthandName);
            var requiredProperties = shorthand.Longhands;

            if (requiredProperties.Length > 0)
            {
                var values = new ICssValue[requiredProperties.Length];
                var important = 0;
                var count = 0;

                for (var i = 0; i < values.Length; i++)
                {
                    var name = requiredProperties[i];
                    var propInfo = factory.Create(name);
                    var property = propInfo.Longhands.Length > 0 ?
                        TryCreateShorthand(name, serialized, usedProperties, force) :
                        FindUnserializedLonghand(name, serialized);

                    if (property?.Value is not null)
                    {
                        usedProperties.Add(name);
                        count = count + 1;
                        important = important + (property.IsImportant ? 1 : 0);
                        values[i] = property.RawValue;
                    }
                }

                var valid = count == values.Length && (important == 0 || important == count);
                var result = force || valid ? _context.CreateShorthand(shorthandName, values, important != 0) : null;
                return force || result?.RawValue != null ? result : null;
            }

            return _context.CreateProperty(shorthandName);
        }

        public String ToCssBlock(IStyleFormatter formatter)
        {
            var list = new List<ICssProperty>();
            var serialized = new List<String>();
            var factory = _context.GetDeclarationFactory();

            foreach (var declaration in Declarations)
            {
                var property = declaration.Name;

                if (!serialized.Contains(property))
                {
                    var info = factory.Create(property);
                    var shorthands = info.Shorthands;

                    if (shorthands.Any())
                    {
                        var sortedShorthands = shorthands.OrderByDescending(shorthand => factory.Create(property).Longhands.Length);

                        foreach (var shorthandName in sortedShorthands)
                        {
                            var usedProperties = new List<String>();
                            var shorthand = TryCreateShorthand(shorthandName, serialized, usedProperties, false);

                            if (shorthand is not null)
                            {
                                list.Add(shorthand);

                                foreach (var name in usedProperties)
                                {
                                    serialized.Add(name);
                                }

                                break;
                            }
                        }
                    }

                    if (!serialized.Contains(property))
                    {
                        serialized.Add(property);
                        list.Add(declaration);
                    }
                }
            }

            return formatter.BlockDeclarations(list);
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter) =>
            writer.Write(ToCssBlock(formatter).Trim(' ', '\t', '\r', '\n', '{', '}'));

        public String RemoveProperty(String propertyName)
        {
            if (!IsReadOnly)
            {
                var value = GetPropertyValue(propertyName);
                RemovePropertyByName(propertyName);
                RaiseChanged();
                return value;
            }

            throw new DomException(DomError.NoModificationAllowed);
        }

        public String GetPropertyPriority(String propertyName)
        {
            var property = GetProperty(propertyName);

            if (property == null || !property.IsImportant)
            {
                var info = _context.GetDeclarationInfo(propertyName);
                var longhands = info.Longhands;

                if (longhands.Length == 0)
                {
                    return String.Empty;
                }

                foreach (var longhand in longhands)
                {
                    if (!GetPropertyPriority(longhand).Isi(CssKeywords.Important))
                    {
                        return String.Empty;
                    }
                }
            }

            return CssKeywords.Important;
        }

        public String GetPropertyValue(String propertyName) =>
            GetProperty(propertyName)?.Value ?? String.Empty;

        public void SetPropertyValue(String propertyName, String propertyValue) =>
            SetProperty(propertyName, propertyValue);

        public void SetPropertyPriority(String propertyName, String priority)
        {
            if (IsReadOnly)
                throw new DomException(DomError.NoModificationAllowed);

            if (String.IsNullOrEmpty(priority) || priority.Isi(CssKeywords.Important))
            {
                var info = _context.GetDeclarationInfo(propertyName);
                var important = !String.IsNullOrEmpty(priority);
                var mappings = info.GetMappings();

                foreach (var mapping in mappings)
                {
                    var property = GetProperty(mapping);

                    if (property != null)
                    {
                        property.IsImportant = important;
                    }
                }
            }
        }

        public void SetProperty(String propertyName, String propertyValue, String priority = null)
        {
            if (IsReadOnly)
                throw new DomException(DomError.NoModificationAllowed);
            
            if (!String.IsNullOrEmpty(propertyValue))
            {
                if (priority is null || priority.Isi(CssKeywords.Important))
                {
                    // Deliberately not seeded from any existing declaration of the same
                    // name: assigning Value below always replaces the raw value, so
                    // reading the old one only costs a lookup. For a shorthand that
                    // lookup is not even a lookup - it rebuilds the shorthand from its
                    // longhands just to throw the result away.
                    var property = _context.CreateProperty(propertyName);

                    if (property is not null)
                    {
                        property.Value = propertyValue;

                        if (property.RawValue is not null)
                        {
                            property.IsImportant = priority is not null;
                            Materialize();
                            SetProperty(property);
                            RaiseChanged();
                        }
                    }
                }
            }
            else
            {
                RemoveProperty(propertyName);
            }
        }

        public void AddProperty(ICssProperty declaration)
        {
            EnsureWritable();
            _declarationIndex[declaration.Name] = _declarations.Count;
            _declarations.Add(declaration);
        }

        public void RemoveProperty(ICssProperty declaration)
        {
            var index = _declarations.IndexOf(declaration);

            if (index < 0 && _inherited is not null)
            {
                // the declaration may only be inherited
                Materialize();
                index = _declarations.IndexOf(declaration);
            }

            if (index >= 0)
            {
                EnsureWritable();
                _declarations.RemoveAt(index);
                RebuildIndex();
            }
        }

        #endregion

        #region Internal Methods

        internal Boolean HasImportantDeclarations()
        {
            Materialize();

            foreach (var declaration in _declarations)
            {
                if (declaration.IsImportant)
                {
                    return true;
                }
            }

            return false;
        }

        internal void SetDeclarations(IEnumerable<ICssProperty> decls) =>
            ChangeDeclarations(decls, m => false, (o, n) => !o.IsImportant || n.IsImportant);

        internal void UpdateDeclarations(IEnumerable<ICssProperty> decls) =>
            ChangeDeclarations(decls, m => !m.CanBeInherited, (o, n) => o.IsInherited && !IsExplicitInitial(o));

        /// <summary>
        /// Gets the declaration with the given (normalized) name, if any. In contrast
        /// to GetProperty no shorthand / longhand is derived from the other declarations.
        /// </summary>
        internal ICssProperty GetDeclaration(String name)
        {
            var index = IndexOf(name, _declarations.Count);

            if (index >= 0)
            {
                return _declarations[index];
            }

            return _inherited is not null ? Snapshot.Inherit(_inherited, _forced, name) : null;
        }

        /// <summary>
        /// Gets the declarations that are not just inherited from the parent, i.e., the
        /// own ones and the ones that explicitly inherit (e.g., "color: inherit").
        /// </summary>
        internal IEnumerable<ICssProperty> GetLocalDeclarations()
        {
            if (_inherited is null || _forced is null)
            {
                return _declarations;
            }

            var local = new List<ICssProperty>(_declarations);

            foreach (var name in _forced)
            {
                local.Add(Snapshot.Inherit(_inherited, _forced, name));
            }

            return local;
        }

        /// <summary>
        /// Same as UpdateDeclarations with the declarations of the parent, but without
        /// copying them: the inherited declarations are looked up in the parent instead,
        /// until the full list is requested. The own declarations need to be complete.
        /// </summary>
        internal void InheritFrom(ICssStyleDeclaration parent)
        {
            if (parent is not CssStyleDeclaration style)
            {
                UpdateDeclarations(parent);
                return;
            }

            Materialize();
            EnsureWritable();
            var snapshot = style.GetSnapshot();
            var removed = false;

            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.IsInherited && !IsExplicitInitial(declaration) && snapshot.Find(declaration.Name) is not null)
                {
                    _declarations[i] = null;
                    (_forced ??= new HashSet<String>(StringComparer.Ordinal)).Add(declaration.Name);
                    removed = true;
                }
            }

            if (removed)
            {
                _declarations.RemoveAll(m => m is null);
                RebuildIndex();
            }

            _inherited = snapshot;
        }

        /// <summary>
        /// Makes this declaration inherit the same declarations as the given one,
        /// except the local ones (see GetLocalDeclarations), which need to be added.
        /// </summary>
        internal void InheritLike(CssStyleDeclaration style)
        {
            if (style._inherited is not null)
            {
                Materialize();
                EnsureWritable();
                _inherited = style._inherited;
            }
        }

        // IsInherited also holds for an explicit `initial` on an inherited property,
        // which must still resolve to the initial value rather than the parent's.
        private static Boolean IsExplicitInitial(ICssProperty property) =>
            property.RawValue is not null && property.RawValue.CssText.Is(CssKeywords.Initial);

        #endregion

        #region Inheritance

        private Snapshot GetSnapshot() => _snapshot ??= new Snapshot(_declarations, _declarationIndex, _inherited, _forced);

        /// <summary>
        /// Copies the lists if they are shared with a snapshot, such that the snapshot
        /// (i.e., what children inherited) is not changed by modifications.
        /// </summary>
        private void EnsureWritable()
        {
            if (_snapshot is not null)
            {
                _declarations = new List<ICssProperty>(_declarations);
                _declarationIndex = new Dictionary<String, Int32>(_declarationIndex, StringComparer.Ordinal);
                _forced = _forced is null ? null : new HashSet<String>(_forced, StringComparer.Ordinal);
                _snapshot = null;
            }
        }

        /// <summary>
        /// Merges the inherited declarations into the own ones, which yields the same
        /// list as UpdateDeclarations with the declarations of the parent would have.
        /// </summary>
        private void Materialize()
        {
            if (_inherited is not null)
            {
                var declarations = new List<ICssProperty>(_declarations);
                var index = new Dictionary<String, Int32>(_declarationIndex, StringComparer.Ordinal);
                Snapshot.Merge(declarations, index, _inherited.GetDeclarations(), _forced);
                _declarations = declarations;
                _declarationIndex = index;
                _inherited = null;
                _forced = null;
                _snapshot = null;
            }
        }

        /// <summary>
        /// An immutable view of the declarations of a parent.
        /// </summary>
        private sealed class Snapshot
        {
            private readonly List<ICssProperty> _declarations;
            private readonly Dictionary<String, Int32> _index;
            private readonly Snapshot _parent;
            private readonly HashSet<String> _forced;
            private List<ICssProperty> _merged;

            public Snapshot(List<ICssProperty> declarations, Dictionary<String, Int32> index, Snapshot parent, HashSet<String> forced)
            {
                _declarations = declarations;
                _index = index;
                _parent = parent;
                _forced = forced;
            }

            /// <summary>
            /// Finds the declaration with the given name, including the inherited ones.
            /// </summary>
            public ICssProperty Find(String name) => FindOwn(name) ?? (_parent is not null ? Inherit(_parent, _forced, name) : null);

            /// <summary>
            /// Gets the declaration that a child with the given forced names inherits
            /// from the given parent.
            /// </summary>
            public static ICssProperty Inherit(Snapshot parent, HashSet<String> forced, String name)
            {
                // a non-inheritable declaration is only taken if forced on every level
                var allForced = forced is not null && forced.Contains(name);

                for (var current = parent; current is not null; current = current._parent)
                {
                    var declaration = current.FindOwn(name);

                    if (declaration is not null)
                    {
                        return allForced || declaration.CanBeInherited ? declaration : null;
                    }

                    allForced = allForced && current._forced is not null && current._forced.Contains(name);
                }

                return null;
            }

            /// <summary>
            /// Gets the full list of declarations, including the inherited ones.
            /// </summary>
            public List<ICssProperty> GetDeclarations()
            {
                if (_parent is null)
                {
                    return _declarations;
                }

                if (_merged is null)
                {
                    // resolve top-down without recursion, as the chain is as deep as the DOM
                    var pending = new Stack<Snapshot>();

                    for (var current = this; current._parent is not null && current._merged is null; current = current._parent)
                    {
                        pending.Push(current);
                    }

                    while (pending.Count > 0)
                    {
                        var current = pending.Pop();
                        var declarations = new List<ICssProperty>(current._declarations);
                        var index = new Dictionary<String, Int32>(current._index, StringComparer.Ordinal);
                        Merge(declarations, index, current._parent.GetDeclarations(), current._forced);
                        current._merged = declarations;
                    }
                }

                return _merged;
            }

            public static void Merge(List<ICssProperty> declarations, Dictionary<String, Int32> index, List<ICssProperty> inherited, HashSet<String> forced)
            {
                foreach (var declaration in inherited)
                {
                    var name = declaration.Name;

                    if (!index.ContainsKey(name) && (declaration.CanBeInherited || (forced is not null && forced.Contains(name))))
                    {
                        index[name] = declarations.Count;
                        declarations.Add(declaration);
                    }
                }
            }

            private ICssProperty FindOwn(String name)
            {
                if (_index.TryGetValue(name, out var i) && i < _declarations.Count)
                {
                    var declaration = _declarations[i];

                    if (declaration is not null && declaration.Name.Is(name))
                    {
                        return declaration;
                    }
                }

                return null;
            }
        }

        #endregion

        #region Helpers

        private ICssProperty GetPropertyShorthand(String name) =>
            TryCreateShorthand(name, Enumerable.Empty<String>(), new List<String>(), true);

        /// <summary>
        /// Gets the first declaration with the given name, unless that name was
        /// already serialized. Equivalent to filtering all declarations by the
        /// serialized set first, since only declarations with exactly this name
        /// can ever be returned.
        /// </summary>
        private ICssProperty FindUnserializedLonghand(String name, IEnumerable<String> serialized)
        {
            if (serialized is ICollection<String> collection)
            {
                if (collection.Count > 0 && collection.Contains(name))
                {
                    return null;
                }
            }
            else if (serialized.Contains(name))
            {
                return null;
            }

            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.Name == name)
                {
                    return declaration;
                }
            }

            return _inherited is not null ? Snapshot.Inherit(_inherited, _forced, name) : null;
        }

        private void SetProperty(ICssProperty property)
        {
            if (property.IsShorthand)
            {
                SetShorthand(property);
            }
            else
            {
                SetLonghand(property);
            }
        }

        private void RemovePropertyByName(String propertyName)
        {
            propertyName = propertyName.StartsWith("--", StringComparison.Ordinal) ? propertyName : propertyName.ToLowerFast();
            var info = _context.GetDeclarationInfo(propertyName);
            var longhands = info.Longhands;
            Materialize();
            EnsureWritable();

            if (_declarationIndex.TryGetValue(propertyName, out var index) && index < _declarations.Count &&
                _declarations[index].Name.Is(propertyName))
            {
                _declarations.RemoveAt(index);
                RebuildIndex();
            }
            else
            {
                for (var i = 0; i < _declarations.Count; i++)
                {
                    var declaration = _declarations[i];

                    if (declaration.Name.Is(propertyName))
                    {
                        _declarations.RemoveAt(i);
                        RebuildIndex();
                        break;
                    }
                }
            }

            foreach (var longhand in longhands)
            {
                RemovePropertyByName(longhand);
            }
        }

        private void ChangeDeclarations(IEnumerable<ICssProperty> decls, Predicate<ICssProperty> defaultSkip, Func<ICssProperty, ICssProperty, Boolean> removeExisting)
        {
            if (decls is null)
            {
                return;
            }

            Materialize();
            EnsureWritable();

            // Replaced declarations are moved to the end. Instead of removing them one by one
            // (shifting the list and invalidating the index each time) they are cleared and
            // the list is compacted once, which keeps merging linear in the number of entries.
            var count = _declarations.Count;
            var removed = false;

            foreach (var newdecl in decls)
            {
                var skip = defaultSkip(newdecl);
                var idx = IndexOf(newdecl.Name, count);

                if (idx >= 0)
                {
                    if (removeExisting.Invoke(_declarations[idx], newdecl))
                    {
                        _declarations[idx] = null;
                        _declarationIndex.Remove(newdecl.Name);
                        removed = true;
                        skip = false;
                    }
                    else
                    {
                        skip = true;
                    }
                }

                if (!skip)
                {
                    _declarations.Add(newdecl);
                }
            }

            if (removed)
            {
                _declarations.RemoveAll(m => m is null);
                RebuildIndex();
            }
            else
            {
                for (var i = count; i < _declarations.Count; i++)
                {
                    _declarationIndex[_declarations[i].Name] = i;
                }
            }
        }

        /// <summary>
        /// Gets the index of the own declaration with the given name among the
        /// first count entries, or -1 if there is none.
        /// </summary>
        private Int32 IndexOf(String name, Int32 count)
        {
            if (_declarationIndex.TryGetValue(name, out var index) && index < count)
            {
                var declaration = _declarations[index];

                if (declaration is not null && declaration.Name.Is(name))
                {
                    return index;
                }
            }

            return -1;
        }

        private void SetLonghand(ICssProperty property)
        {
            EnsureWritable();

            if (_declarationIndex.TryGetValue(property.Name, out var index) && index < _declarations.Count)
            {
                var declaration = _declarations[index];

                if (declaration.Name.Is(property.Name))
                {
                    if (declaration.IsImportant && !property.IsImportant)
                    {
                        return;
                    }

                    _declarations[index] = property;
                    return;
                }
            }

            _declarationIndex[property.Name] = _declarations.Count;
            _declarations.Add(property);
        }

        private void SetShorthand(ICssProperty shorthand)
        {
            if ((_context.GetDeclarationInfo(shorthand.Name).Flags & PropertyFlags.PreserveShorthand) == PropertyFlags.PreserveShorthand)
            {
                SetLonghand(shorthand);
                return;
            }

            var properties = _context.CreateLonghands(shorthand);

            if (properties is not null)
            {
                foreach (var property in properties)
                {
                    SetProperty(property);
                }
            }
        }

        private ICssProperty GetPropertyFromShorthand(String name)
        {
            var info = _context.GetDeclarationInfo(name);
            var factory = _context.GetDeclarationFactory();

            foreach (var shorthandName in info.Shorthands)
            {
                var shorthand = GetDeclaration(shorthandName);

                if (shorthand is not null)
                {
                    var shorthandInfo = factory.Create(shorthandName);
                    var rawValue = shorthand.RawValue ?? shorthandInfo.Converter.Convert(new StringSource(shorthand.Value));

                    if (rawValue is not null)
                    {
                        var values = shorthandInfo.Expand(factory, rawValue);
                        var longhandIndex = Array.IndexOf(shorthandInfo.Longhands, name);

                        if (values is not null && longhandIndex >= 0 && longhandIndex < values.Length)
                        {
                            return new CssProperty(name, info.Converter, info.Flags, values[longhandIndex], shorthand.IsImportant);
                        }
                    }
                }
            }

            return null;
        }

        private void RebuildIndex()
        {
            _declarationIndex.Clear();

            for (var i = 0; i < _declarations.Count; i++)
            {
                _declarationIndex[_declarations[i].Name] = i;
            }
        }

        private void RaiseChanged()
        {
            if (!_updating)
            {
                _updating = true;
                Changed?.Invoke(CssText);
                _updating = false;
            }
        }

        #endregion

        #region Interface implementation

        public IEnumerator<ICssProperty> GetEnumerator() => Declarations.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
