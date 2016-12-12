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

        private readonly List<ICssProperty> _declarations;
        private readonly IBrowsingContext _context;
        private ICssRule _parent;
        private Boolean _updating;

        #endregion

        #region Events

        public event Action<String> Changed;

        #endregion

        #region ctor

        public CssStyleDeclaration()
        {
            _declarations = new List<ICssProperty>();
        }

        public CssStyleDeclaration(IBrowsingContext context)
            : this()
        {
            _context = context;
        }

        public CssStyleDeclaration(ICssRule parent)
            : this(parent.Owner.Context)
        {
            _parent = parent;
        }

        #endregion

        #region Index

        public String this[Int32 index]
        {
            get { return _declarations[index]?.Name; }
        }

        public String this[String name]
        {
            get { return GetPropertyValue(name); }
        }

        #endregion

        #region Properties

        public IEnumerable<ICssProperty> Declarations
        {
            get { return _declarations; }
        }

        public String CssText
        {
            get { return this.ToCss(); }
            set { Update(value); RaiseChanged(); }
        }

        public Boolean IsReadOnly
        {
            get { return _context == null; }
        }

        public Int32 Length
        {
            get { return Declarations.Count(); }
        }

        public ICssRule Parent
        {
            get { return _parent; }
        }

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
                    var decl = parser.ParseDeclaration(value);

                    if (decl != null)
                    {
                        _declarations.AddRange(decl);
                    }
                }
            }
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var list = new List<IStyleFormattable>();
            var serialized = new List<String>();
            //var factory = _context.GetFactory<ICssPropertyFactory>();

            foreach (var declaration in Declarations)
            {
                var property = declaration.Name;

                if (serialized.Contains(property))
                    continue;

                //var shorthands = factory.GetShorthands(property);

                //if (shorthands.Any())
                //{
                //    var longhands = Declarations.Where(m => !serialized.Contains(m.Name)).ToList();

                //    foreach (var shorthandName in shorthands.OrderByDescending(m => factory.GetLonghands(m).Count()))
                //    {
                //        var shorthand = factory.CreateShorthand(shorthandName);
                //        var properties = factory.GetLonghands(shorthandName).ToArray();
                //        var currentLonghands = longhands.Where(m => properties.Contains(m.Name)).ToArray();

                //        if (currentLonghands.Length == 0)
                //            continue;

                //        var important = currentLonghands.Count(m => m.IsImportant);

                //        if (important > 0 && important != currentLonghands.Length)
                //            continue;

                //        if (properties.Length != currentLonghands.Length)
                //            continue;
                        
                //        var value = shorthand.Stringify(currentLonghands);

                //        if (String.IsNullOrEmpty(value))
                //            continue;

                //        list.Add(CssStyleFormatter.Instance.Declaration(shorthandName, value, important != 0));

                //        foreach (var longhand in currentLonghands)
                //        {
                //            serialized.Add(longhand.Name);
                //            longhands.Remove(longhand);
                //        }
                //    }
                //}

                if (serialized.Contains(property))
                    continue;

                serialized.Add(property);
                list.Add(declaration);
            }
            
            writer.Write(formatter.BlockDeclarations(list).Trim(' ', '\t', '\r', '\n', '{', '}'));
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
                //var factory = _context.GetFactory<ICssPropertyFactory>();

                //if (factory.IsShorthand(propertyName))
                //{
                //    var longhands = factory.GetLonghands(propertyName);

                //    foreach (var longhand in longhands)
                //    {
                //        if (!GetPropertyPriority(longhand).Isi(CssKeywords.Important))
                //        {
                //            return String.Empty;
                //        }
                //    }

                //    return CssKeywords.Important;
                //}

                return String.Empty;
            }

            return CssKeywords.Important;
        }

        public String GetPropertyValue(String propertyName)
        {
            var property = GetProperty(propertyName);

            if (property == null)
            {
                //var factory = _context.GetFactory<ICssPropertyFactory>();

                //if (factory.IsShorthand(propertyName))
                //{
                //    var shortHand = factory.CreateShorthand(propertyName);
                //    var declarations = factory.GetLonghands(propertyName);
                //    var properties = new List<ICssProperty>();

                //    foreach (var declaration in declarations)
                //    {
                //        property = GetProperty(declaration);

                //        if (property == null)
                //        {
                //            return String.Empty;
                //        }

                //        properties.Add(property);
                //    }

                //    return shortHand.Stringify(properties.ToArray());
                //}

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
                //var factory = _context.GetFactory<ICssPropertyFactory>();
                var important = !String.IsNullOrEmpty(priority);
                //var mappings = factory.IsShorthand(propertyName) ? 
                //    factory.GetLonghands(propertyName) : 
                //    Enumerable.Repeat(propertyName, 1);

                //foreach (var mapping in mappings)
                //{
                    var property = GetProperty(propertyName);//mapping);

                    if (property != null)
                    {
                        property.IsImportant = important;
                    }
                //}
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

            return null;
        }

        internal void SetDeclarations(IEnumerable<ICssProperty> decls)
        {
            ChangeDeclarations(decls, m => false, (o, n) => !o.IsImportant || n.IsImportant);
        }

        internal void UpdateDeclarations(IEnumerable<ICssProperty> decls)
        {
            ChangeDeclarations(decls, m => !m.CanBeInherited, (o, n) => o.IsInherited);
        }

        #endregion

        #region Helpers

        private ICssProperty CreateProperty(String propertyName)
        {
            return GetProperty(propertyName) ?? _context.CreateProperty(propertyName);
        }

        private void SetProperty(ICssProperty property)
        {
            //if (property is CssShorthandProperty)
            //{
            //    SetShorthand((CssShorthandProperty)property);
            //}
            //else
            //{
                SetLonghand(property);
            //}
        }

        private void RemovePropertyByName(String propertyName)
        {
            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.Name.Is(propertyName))
                {
                    _declarations.RemoveAt(i);
                    break;
                }
            }

            //var factory = _context.GetFactory<ICssPropertyFactory>();

            //if (factory.IsShorthand(propertyName))
            //{
            //    var longhands = factory.GetLonghands(propertyName);

            //    foreach (var longhand in longhands)
            //    {
            //        RemovePropertyByName(longhand);
            //    }
            //}
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
            //var factory = _context.GetFactory<ICssPropertyFactory>();
            //var properties = factory.CreateLonghandsFor(shorthand.Name);
            //shorthand.Export(properties);

            //foreach (var property in properties)
            //{
            //    SetLonghand(property);
            //}
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
