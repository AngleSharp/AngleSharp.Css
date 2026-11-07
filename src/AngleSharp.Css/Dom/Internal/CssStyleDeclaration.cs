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
    using System.Threading;

    /// <summary>
    /// Represents a single CSS declaration block.
    /// </summary>
    sealed class CssStyleDeclaration : ICssStyleDeclaration
    {
        #region Fields

        private List<ICssProperty> _declarations;
        // where the declaration of a name is; null as long as there are just a few, which are
        // found by a scan (see Find): there is a declaration for the computed and the specified
        // style of every element, which live as long as the render tree
        private Dictionary<String, Int32> _declarationIndex;
        private readonly IBrowsingContext _context;
        private Boolean _reportsInitialValues;
        private Func<String, String> _usedValues;
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
            _context = context;
        }

        /// <summary>
        /// Creates the declarations with room for the given number of entries, e.g. for
        /// a cascade that knows how many declarations its matching rules bring.
        /// </summary>
        internal CssStyleDeclaration(IBrowsingContext context, Int32 capacity)
        {
            _declarations = new List<ICssProperty>(capacity);
            _declarationIndex = capacity > MaxUnindexed ? new Dictionary<String, Int32>(capacity, StringComparer.Ordinal) : null;
            _context = context;
        }

        /// <summary>
        /// Gives back the room of a declaration created with more capacity than it ended up
        /// using (see the constructor), e.g. a cascade where many declarations replace each
        /// other. It lives as long as the render tree, and would be copied with all its room
        /// by every garbage collection meanwhile.
        /// </summary>
        internal void TrimExcess()
        {
            if (_snapshot is null && _declarations.Capacity > 2 * _declarations.Count + 8)
            {
                _declarations.TrimExcess();
                _declarationIndex = _declarations.Count > MaxUnindexed ? CreateIndex(_declarations, _declarationIndex) : null;
            }
        }

        public CssStyleDeclaration(ICssRule parent)
            : this(parent.Owner?.Context)
        {
            _parent = parent;
        }

        // shares the lists of the source until either of the two is modified
        private CssStyleDeclaration(CssStyleDeclaration source)
        {
            _snapshot = source.GetSnapshot();
            _declarations = source._declarations;
            _declarationIndex = source._declarationIndex;
            _inherited = source._inherited;
            _forced = source._forced;
            _context = source._context;
            RootFontSize = source.RootFontSize;
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
                _declarationIndex = null;

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

        public String GetPropertyValue(String propertyName)
        {
            var used = _usedValues?.Invoke(propertyName);

            if (used != null)
            {
                return used;
            }

            var value = GetPropertyValueCore(propertyName);

            if (String.IsNullOrEmpty(value) && _reportsInitialValues)
            {
                return GetInitialValue(propertyName);
            }

            return value ?? String.Empty;
        }

        /// <summary>
        /// Gets the value as GetProperty would, without creating the empty property of an
        /// undeclared longhand: a renderer asks for many properties that most elements never
        /// declare, such as counter-set.
        /// </summary>
        private String GetPropertyValueCore(String name)
        {
            name = name.StartsWith("--", StringComparison.Ordinal) ? name : name.ToLowerFast();
            var declaration = GetDeclaration(name) ?? GetPropertyFromShorthand(name);

            if (declaration is not null)
            {
                return declaration.Value;
            }

            // only a shorthand can be put together from its longhands
            if (_context.GetDeclarationInfo(name).Longhands.Length == 0)
            {
                return String.Empty;
            }

            return GetPropertyShorthand(name)?.Value;
        }

        /// <summary>
        /// Gets the copy script sees of a computed style, where nothing is left empty (CSSOM 9):
        /// a script compares with the initial values, as with position !== 'static'. Layout on
        /// the other hand has to know what was not declared at all.
        /// </summary>
        internal CssStyleDeclaration WithInitialValues(Func<String, String> usedValues) =>
            new CssStyleDeclaration(this) { _reportsInitialValues = true, _usedValues = usedValues };

        private String GetInitialValue(String propertyName)
        {
            var info = _context.GetDeclarationInfo(propertyName);
            var initial = info?.InitialValue?.CssText;

            if (!String.IsNullOrEmpty(initial))
            {
                return initial;
            }

            //  a shorthand tells it by its longhands, as with overflow
            var longhands = info?.Longhands;

            if (longhands == null || longhands.Length == 0)
            {
                return String.Empty;
            }

            var values = new System.Collections.Generic.List<String>();

            foreach (var longhand in longhands)
            {
                var value = GetPropertyValue(longhand);

                if (!values.Contains(value))
                {
                    values.Add(value);
                }
            }

            return String.Join(" ", values);
        }

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
            _declarations.Add(declaration);
            Indexed(declaration.Name, _declarations.Count - 1);
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

            return _inherited is not null && MayInherit(name) ? Snapshot.Inherit(_inherited, _forced, name) : null;
        }

        /// <summary>
        /// Whether a declaration with the given name may come from the parent: only an inherited
        /// property, or one inheriting explicitly. Everything else would walk up the ancestors
        /// just to drop what it finds, which a computed style is asked for constantly (e.g. for
        /// every undeclared non-inherited longhand and its shorthands, by a renderer).
        /// </summary>
        private Boolean MayInherit(String name) =>
            (_forced is not null && _forced.Contains(name)) ||
            (_context.GetDeclarationInfo(name).Flags & PropertyFlags.Inherited) == PropertyFlags.Inherited;

        /// <summary>
        /// Sets the given declaration as an own one, replacing the own declaration with the
        /// same name, if any. An inherited declaration with the same name is shadowed, hence
        /// in contrast to RemoveProperty the inherited declarations do not need to be merged.
        /// </summary>
        internal void ReplaceDeclaration(ICssProperty declaration)
        {
            EnsureWritable();
            var index = IndexOf(declaration.Name, _declarations.Count);

            if (index >= 0)
            {
                _declarations[index] = declaration;
            }
            else
            {
                _declarations.Add(declaration);
                Indexed(declaration.Name, _declarations.Count - 1);
            }
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
        /// Creates a copy of the current declarations that later modifications of this
        /// one do not change, e.g. for children inheriting from it later on. Nothing is
        /// copied until either of the two is modified.
        /// </summary>
        internal CssStyleDeclaration Freeze() => new(this);

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
                _declarationIndex = _declarationIndex is null ? null : new Dictionary<String, Int32>(_declarationIndex, StringComparer.Ordinal);
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
                var index = CreateIndex(_declarations, _declarationIndex);
                Snapshot.Merge(declarations, index, _inherited.GetDeclarations(), _forced);
                _declarations = declarations;
                _declarationIndex = declarations.Count > MaxUnindexed ? index : null;
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
            // the names looked up through this snapshot, with the nearest declaration found:
            // the first _nearestCount entries are set (see AddNearest)
            private NearestEntry[] _nearest;
            private Int32 _nearestCount;

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
                if (forced is null || !forced.Contains(name))
                {
                    // the nearest declaration decides, and a non-inheritable one is not taken
                    var nearest = parent.Nearest(name);
                    return nearest is not null && nearest.CanBeInherited ? nearest : null;
                }

                // a non-inheritable declaration is only taken if forced on every level
                var allForced = true;

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

                if (Volatile.Read(ref _merged) is null)
                {
                    // resolve top-down without recursion, as the chain is as deep as the DOM
                    var pending = new Stack<Snapshot>();

                    for (var current = this; current._parent is not null && Volatile.Read(ref current._merged) is null; current = current._parent)
                    {
                        pending.Push(current);
                    }

                    while (pending.Count > 0)
                    {
                        var current = pending.Pop();
                        var declarations = new List<ICssProperty>(current._declarations);
                        var index = CreateIndex(current._declarations, current._index);
                        Merge(declarations, index, current._parent.GetDeclarations(), current._forced);
                        // a computed style may be read from several threads: the first list wins
                        Interlocked.CompareExchange(ref current._merged, declarations, null);
                    }
                }

                return Volatile.Read(ref _merged);
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

            /// <summary>
            /// Finds the nearest declaration with the given name, this snapshot's own or an
            /// ancestor's. The chain is as deep as the DOM, and a name declared nowhere (e.g.
            /// visibility) would be looked up through all of it for every element, so the
            /// answer is kept on the snapshots passed: they never change.
            /// </summary>
            private ICssProperty Nearest(String name)
            {
                var result = default(ICssProperty);
                var stop = default(Snapshot);

                for (var current = this; current is not null; current = current._parent)
                {
                    result = current.FindOwn(name);

                    if (result is not null || current.TryGetNearest(name, out result))
                    {
                        stop = current;
                        break;
                    }
                }

                for (var current = this; current != stop; current = current._parent)
                {
                    current.AddNearest(name, result);
                }

                return result;
            }

            private Boolean TryGetNearest(String name, out ICssProperty declaration)
            {
                // the count is published after the entries it covers (see AddNearest)
                var count = Volatile.Read(ref _nearestCount);
                var entries = Volatile.Read(ref _nearest);

                if (entries is not null)
                {
                    count = Math.Min(count, entries.Length);

                    for (var i = 0; i < count; i++)
                    {
                        if (String.Equals(entries[i].Name, name, StringComparison.Ordinal))
                        {
                            declaration = entries[i].Declaration;
                            return true;
                        }
                    }
                }

                declaration = null;
                return false;
            }

            private void AddNearest(String name, ICssProperty declaration)
            {
                // a computed style may be read from several threads: readers never lock, as an
                // entry is written before the count including it is published, and an array is
                // only replaced by a larger copy (instead of a copy per name, which made looking
                // up the few dozen names of a renderer quadratic per element)
                lock (this)
                {
                    var entries = _nearest;
                    var count = _nearestCount;

                    for (var i = 0; i < count; i++)
                    {
                        if (String.Equals(entries[i].Name, name, StringComparison.Ordinal))
                        {
                            return;
                        }
                    }

                    if (entries is null || count == entries.Length)
                    {
                        var grown = new NearestEntry[Math.Max(8, count * 2)];

                        if (count > 0)
                        {
                            Array.Copy(entries, grown, count);
                        }

                        grown[count] = new NearestEntry(name, declaration);
                        Volatile.Write(ref _nearest, grown);
                    }
                    else
                    {
                        entries[count] = new NearestEntry(name, declaration);
                    }

                    Volatile.Write(ref _nearestCount, count + 1);
                }
            }

            private readonly struct NearestEntry
            {
                public NearestEntry(String name, ICssProperty declaration)
                {
                    Name = name;
                    Declaration = declaration;
                }

                public String Name { get; }

                // null if no snapshot up the chain declares the name
                public ICssProperty Declaration { get; }
            }

            private ICssProperty FindOwn(String name)
            {
                var i = CssStyleDeclaration.Find(_declarations, _index, name, _declarations.Count);

                if (i >= 0)
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

            return _inherited is not null && MayInherit(name) ? Snapshot.Inherit(_inherited, _forced, name) : null;
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

            var index = Find(_declarations, _declarationIndex, propertyName, _declarations.Count);

            if (index >= 0 && _declarations[index].Name.Is(propertyName))
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
                        _declarationIndex?.Remove(newdecl.Name);
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
                // compacted in place: only the entries that moved need a new index, the index
                // is not cleared (it may have room for thousands of entries, e.g., the custom
                // properties of :root) and built again for every rule replacing a declaration
                var write = 0;

                for (var read = 0; read < _declarations.Count; read++)
                {
                    var declaration = _declarations[read];

                    if (declaration is not null)
                    {
                        if (write != read)
                        {
                            _declarations[write] = declaration;

                            if (_declarationIndex is not null)
                            {
                                _declarationIndex[declaration.Name] = write;
                            }
                        }
                        else if (read >= count && _declarationIndex is not null)
                        {
                            _declarationIndex[declaration.Name] = write;
                        }

                        write++;
                    }
                }

                _declarations.RemoveRange(write, _declarations.Count - write);

                if (_declarationIndex is null && _declarations.Count > MaxUnindexed)
                {
                    RebuildIndex();
                }
            }
            else if (_declarationIndex is not null)
            {
                for (var i = count; i < _declarations.Count; i++)
                {
                    _declarationIndex[_declarations[i].Name] = i;
                }
            }
            else if (_declarations.Count > MaxUnindexed)
            {
                RebuildIndex();
            }
        }

        /// <summary>
        /// Gets the index of the own declaration with the given name among the
        /// first count entries, or -1 if there is none.
        /// </summary>
        private Int32 IndexOf(String name, Int32 count)
        {
            var index = Find(_declarations, _declarationIndex, name, count);

            if (index >= 0)
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
            var index = Find(_declarations, _declarationIndex, property.Name, _declarations.Count);

            if (index >= 0)
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

            _declarations.Add(property);
            Indexed(property.Name, _declarations.Count - 1);
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

        // below, a scan of the declarations is about as fast as an index, which is a lot larger
        private const Int32 MaxUnindexed = 16;

        private void RebuildIndex()
        {
            if (_declarations.Count <= MaxUnindexed)
            {
                _declarationIndex = null;
                return;
            }

            var index = _declarationIndex ?? new Dictionary<String, Int32>(_declarations.Count, StringComparer.Ordinal);
            index.Clear();

            for (var i = 0; i < _declarations.Count; i++)
            {
                index[_declarations[i].Name] = i;
            }

            _declarationIndex = index;
        }

        /// <summary>
        /// Takes the declaration just added at the given position into the index, which is
        /// created once there are too many to scan.
        /// </summary>
        private void Indexed(String name, Int32 position)
        {
            if (_declarationIndex is not null)
            {
                _declarationIndex[name] = position;
            }
            else if (_declarations.Count > MaxUnindexed)
            {
                RebuildIndex();
            }
        }

        /// <summary>
        /// Finds the position of the declaration with the given name among the first count
        /// ones: by the index, or by a scan when there is none. The index has the last
        /// position set for a name, hence the scan starts at the end.
        /// </summary>
        private static Int32 Find(List<ICssProperty> declarations, Dictionary<String, Int32> index, String name, Int32 count)
        {
            if (index is not null)
            {
                return index.TryGetValue(name, out var position) && position < count ? position : -1;
            }

            for (var i = Math.Min(count, declarations.Count) - 1; i >= 0; i--)
            {
                var declaration = declarations[i];

                if (declaration is not null && String.Equals(declaration.Name, name, StringComparison.Ordinal))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Creates a copy of the index (with room for more), or an index of the declarations
        /// if there is none.
        /// </summary>
        private static Dictionary<String, Int32> CreateIndex(List<ICssProperty> declarations, Dictionary<String, Int32> index)
        {
            if (index is not null)
            {
                return new Dictionary<String, Int32>(index, StringComparer.Ordinal);
            }

            var created = new Dictionary<String, Int32>(declarations.Count, StringComparer.Ordinal);

            for (var i = 0; i < declarations.Count; i++)
            {
                if (declarations[i] is { } declaration)
                {
                    created[declaration.Name] = i;
                }
            }

            return created;
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
