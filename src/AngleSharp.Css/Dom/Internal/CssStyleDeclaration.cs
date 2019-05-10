namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
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

        private readonly List<ICssProperty> _declarations;
        private readonly IBrowsingContext _context;
        private ICssRule _parent;
        private Boolean _updating;

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

        public CssStyleDeclaration(ICssRule parent)
            : this(parent.Owner?.Context)
        {
            _parent = parent;
        }

        #endregion

        #region Index

        public String this[Int32 index] => _declarations[index]?.Name;

        public String this[String name] => GetPropertyValue(name);

        #endregion

        #region Properties

        public IEnumerable<ICssProperty> Declarations => _declarations;

        public String CssText
        {
            get => this.ToCss();
            set { Update(value); RaiseChanged(); }
        }

        public Boolean IsReadOnly => _context == null;

        public Int32 Length => Declarations.Count();

        public ICssRule Parent => _parent;

        #endregion

        #region Methods

        public void SetParent(ICssRule parent)
        {
            _parent = parent;
        }
        
        public void Update(String value)
        {
            if (IsReadOnly)
                throw new DomException(DomError.NoModificationAllowed);

            if (!_updating)
            {
                _declarations.Clear();

                if (!String.IsNullOrEmpty(value))
                {
                    var parser = _context.GetService<ICssParser>();
                    var decl = parser?.ParseDeclaration(value);

                    if (decl != null)
                    {
                        _declarations.AddRange(decl);
                    }
                }
            }
        }

        private ICssProperty TryCreateShorthand(String shorthandName, IEnumerable<String> serialized, List<String> usedProperties)
        {
            var longhands = Declarations.Where(m => !serialized.Contains(m.Name)).ToList();
            var shorthand = _context.GetDeclarationInfo(shorthandName);
            var requiredProperties = shorthand.Longhands;
            var values = new ICssValue[requiredProperties.Length];
            var important = 0;
            var count = 0;
            var aggregator = shorthand.Converter as IValueAggregator;

            for (var i = 0; i < values.Length; i++)
            {
                var name = requiredProperties[i];
                var propInfo = _context.GetDeclarationInfo(name);
                var property = propInfo.Longhands.Any() ?
                    TryCreateShorthand(name, serialized, usedProperties) :
                    longhands.Where(m => m.Name == name).FirstOrDefault();

                if (property != null)
                {
                    usedProperties.Add(name);
                    count = count + 1;
                    important = important + (property.IsImportant ? 1 : 0);
                    values[i] = property.RawValue;
                }
            }

            if (count == values.Length && aggregator != null && (important == 0 || important == count))
            {
                var value = aggregator.Merge(values);

                if (value != null)
                {
                    return CreateNewProperty(shorthandName, value, important != 0);
                }
            }

            return null;
        }

        public String ToCssBlock(IStyleFormatter formatter)
        {
            var list = new List<ICssProperty>();
            var serialized = new List<String>();

            foreach (var declaration in Declarations)
            {
                var property = declaration.Name;

                if (!serialized.Contains(property))
                {
                    var info = _context.GetDeclarationInfo(property);
                    var shorthands = info.Shorthands;

                    if (shorthands.Any())
                    {
                        var sortedShorthands = shorthands.OrderByDescending(shorthand => _context.GetDeclarationInfo(property).Longhands.Length);

                        foreach (var shorthandName in sortedShorthands)
                        {
                            var usedProperties = new List<String>();
                            var shorthand = TryCreateShorthand(shorthandName, serialized, usedProperties);

                            if (shorthand != null)
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

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var block = ToCssBlock(formatter);
            writer.Write(block.Trim(' ', '\t', '\r', '\n', '{', '}'));
        }

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
            var property = GetProperty(propertyName);

            if (property == null)
            {
                var value = GetShorthandInfo(propertyName).Value;

                if (value != null)
                {
                    return value.CssText;
                }

                return String.Empty;
            }

            return property.Value;
        }

        public void SetPropertyValue(String propertyName, String propertyValue)
        {
            SetProperty(propertyName, propertyValue);
        }

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
                if (priority == null || priority.Isi(CssKeywords.Important))
                {
                    var property = CreateProperty(propertyName);

                    if (property != null)
                    {
                        property.Value = propertyValue;
                        property.IsImportant = priority != null;
                        SetProperty(property);
                        RaiseChanged();
                    }
                }
            }
            else
            {
                RemoveProperty(propertyName);
            }
        }

        #endregion

        #region Internal Methods

        internal ICssProperty GetProperty(String name)
        {
            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.Name.Isi(name))
                {
                    return declaration;
                }
            }

            return GetPropertyShorthand(name);
        }

        private ICssProperty GetPropertyShorthand(String name)
        {
            var result = GetShorthandInfo(name);
            var decl = result.Declaration;
            return new CssProperty(name, decl.Converter, decl.Flags, result.Value, result.IsImportant);
        }

        internal void SetDeclarations(IEnumerable<ICssProperty> decls) =>
            ChangeDeclarations(decls, m => false, (o, n) => !o.IsImportant || n.IsImportant);

        internal void UpdateDeclarations(IEnumerable<ICssProperty> decls) =>
            ChangeDeclarations(decls, m => !m.CanBeInherited, (o, n) => o.IsInherited);

        #endregion

        #region Helpers

        struct ShorthandInfo
        {
            public DeclarationInfo Declaration;
            public ICssValue Value;
            public Boolean IsImportant;
        }

        private ShorthandInfo GetShorthandInfo(String propertyName)
        {
            var info = _context.GetDeclarationInfo(propertyName);
            var important = false;

            if (info.Converter is IValueAggregator aggregator)
            {
                var declarations = info.Longhands;
                var values = new ICssValue[declarations.Length];

                for (var i = 0; i < values.Length; i++)
                {
                    var prop = GetProperty(declarations[i]);

                    if (prop != null)
                    {
                        var value = prop.RawValue;
                        important = important || prop.IsImportant;

                        if (value is CssChildValue child)
                        {
                            return new ShorthandInfo
                            {
                                Value = child.Parent,
                                IsImportant = important,
                                Declaration = info,
                            };
                        }

                        values[i] = value;
                    }
                }

                if (values.Any(m => m != null))
                {
                    var value = aggregator.Merge(values);

                    if (value != null)
                    {
                        return new ShorthandInfo
                        {
                            Value = value,
                            IsImportant = important,
                            Declaration = info,
                        };
                    }
                }
            }

            return new ShorthandInfo
            {
                Value = null,
                IsImportant = important,
                Declaration = info,
            };
        }

        private ICssProperty CreateProperty(String propertyName) =>
            GetProperty(propertyName) ?? _context.CreateProperty(propertyName);

        private ICssProperty CreateNewProperty(String propertyName, ICssValue value, Boolean important = false)
        {
            var info = _context.GetDeclarationInfo(propertyName);
            return new CssProperty(propertyName, info.Converter, info.Flags, value, important);
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
            var info = _context.GetDeclarationInfo(propertyName);
            var longhands = info.Longhands;

            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.Name.Is(propertyName))
                {
                    _declarations.RemoveAt(i);
                    break;
                }
            }

            foreach (var longhand in longhands)
            {
                RemovePropertyByName(longhand);
            }
        }

        private void ChangeDeclarations(IEnumerable<ICssProperty> decls, Predicate<ICssProperty> defaultSkip, Func<ICssProperty, ICssProperty, Boolean> removeExisting)
        {
            var declarations = new List<ICssProperty>();

            foreach (var newdecl in decls)
            {
                var skip = defaultSkip(newdecl);

                for (var i = 0; i < _declarations.Count; i++)
                {
                    var olddecl = _declarations[i];

                    if (olddecl.Name.Is(newdecl.Name))
                    {
                        if (removeExisting.Invoke(olddecl, newdecl))
                        {
                            _declarations.RemoveAt(i);
                        }
                        else
                        {
                            skip = true;
                        }

                        break;
                    }
                }

                if (!skip)
                {
                    declarations.Add(newdecl);
                }
            }

            _declarations.AddRange(declarations);
        }

        private void SetLonghand(ICssProperty property)
        {
            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.Name.Is(property.Name))
                {
                    _declarations[i] = property;
                    return;
                }
            }

            _declarations.Add(property);
        }

        private void SetShorthand(ICssProperty shorthand)
        {
            var info = _context.GetDeclarationInfo(shorthand.Name);
            var properties = info.CreateLonghands(shorthand.RawValue, (name, value) =>
            {
                return CreateNewProperty(name, value, shorthand.IsImportant);
            });

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    property.IsImportant = shorthand.IsImportant;
                    SetProperty(property);
                }
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

        public IEnumerator<ICssProperty> GetEnumerator()
        {
            return Declarations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
