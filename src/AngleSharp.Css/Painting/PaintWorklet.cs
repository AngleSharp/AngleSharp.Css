namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Css.TypedOm;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The paint worklet of a document (CSS.paintWorklet), which is the registry of its
    /// paint definitions. Modules (scripts calling registerPaint()) are run by the
    /// <see cref="IPaintWorkletModuleLoader"/> of the configuration, e.g., one provided
    /// by a scripting engine. Paint definitions implemented in .NET can be registered
    /// directly, without any scripting engine.
    /// https://www.w3.org/TR/css-paint-api-1/#paint-worklet
    /// </summary>
    [DomName("Worklet")]
    public sealed class PaintWorklet
    {
        #region Fields

        private static readonly ConditionalWeakTable<IDocument, PaintWorklet> Worklets = new();

        private readonly Object _lock = new();
        private Dictionary<String, PaintRegistration> _registrations = new(StringComparer.Ordinal);

        #endregion

        #region ctor

        private PaintWorklet(IDocument document)
        {
            Document = document;
            GlobalScope = new PaintWorkletGlobalScope(this);
        }

        /// <summary>
        /// Gets the paint worklet of the document, which is created when it is first used.
        /// </summary>
        internal static PaintWorklet For(IDocument document) =>
            Worklets.GetValue(document, doc => new PaintWorklet(doc));

        /// <summary>
        /// Gets the paint worklet of the document, if it has been used.
        /// </summary>
        internal static PaintWorklet? Find(IDocument document) =>
            Worklets.TryGetValue(document, out var worklet) ? worklet : null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the document the paint worklet belongs to.
        /// </summary>
        public IDocument Document { get; }

        /// <summary>
        /// Gets the global scope modules run in.
        /// </summary>
        public PaintWorkletGlobalScope GlobalScope { get; }

        /// <summary>
        /// Gets the number of device pixels per CSS pixel, which is the resolution of the
        /// render device divided by 96 (or 1 without a render device).
        /// </summary>
        public Double DevicePixelRatio
        {
            get
            {
                var resolution = Document.Context?.GetService<IRenderDevice>()?.Resolution ?? 0;
                return resolution > 0 ? resolution / 96.0 : 1.0;
            }
        }

        /// <summary>
        /// Gets the names of the registered paint definitions.
        /// </summary>
        public IReadOnlyCollection<String> Names => _registrations.Keys.ToArray();

        #endregion

        #region Methods

        /// <summary>
        /// Loads the module, i.e., runs the script, which registers paint definitions,
        /// with the <see cref="IPaintWorkletModuleLoader"/> of the configuration.
        /// </summary>
        /// <param name="moduleUrl">The URL of the module, relative to the document.</param>
        /// <param name="cancellationToken">The token to cancel loading.</param>
        /// <returns>
        /// The task completing once the module has been run, which fails with a
        /// NotSupportedError if no module loader has been registered, or a
        /// SyntaxError if the URL is not valid.
        /// </returns>
        [DomName("addModule")]
        public Task AddModule(String moduleUrl, CancellationToken cancellationToken = default)
        {
            if (moduleUrl is null)
                throw new ArgumentNullException(nameof(moduleUrl));

            var baseUrl = Document.BaseUrl;
            var url = baseUrl is not null ? new Url(baseUrl, moduleUrl) : Url.Create(moduleUrl);

            if (url.IsInvalid)
            {
                return Task.FromException(new DomException(DomError.Syntax));
            }

            var loader = Document.Context?.GetService<IPaintWorkletModuleLoader>();

            if (loader is null)
            {
                return Task.FromException(new DomException(DomError.NotSupported));
            }

            return loader.LoadAsync(GlobalScope, url, cancellationToken);
        }

        /// <summary>
        /// Registers a paint definition under the given name (registerPaint() of the
        /// global scope), which is used by the paint() functions with this name from now
        /// on. Unknown input properties are ignored.
        /// </summary>
        /// <param name="name">The name of the paint definition.</param>
        /// <param name="definition">The paint definition.</param>
        /// <exception cref="ArgumentException">
        /// The TypeError for an empty name or a syntax of the input arguments that is not valid.
        /// </exception>
        /// <exception cref="DomException">
        /// An InvalidModificationError if the name has been registered already.
        /// </exception>
        public void RegisterPaint(String name, IPaintDefinition definition)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            if (name.Length == 0)
                throw new ArgumentException("The name of a paint definition must not be empty.", nameof(name));

            if (Contains(name))
                throw new DomException(DomError.InvalidModification);

            var registration = PaintRegistration.Create(Document.Context, name, definition);

            lock (_lock)
            {
                if (_registrations.ContainsKey(name))
                    throw new DomException(DomError.InvalidModification);

                // replaced rather than changed, as images may be drawn meanwhile
                _registrations = new Dictionary<String, PaintRegistration>(_registrations, StringComparer.Ordinal)
                {
                    [name] = registration,
                };
            }
        }

        /// <summary>
        /// Checks if a paint definition has been registered under the given name.
        /// </summary>
        /// <param name="name">The name of the paint definition.</param>
        /// <returns>True if one has been registered, otherwise false.</returns>
        public Boolean Contains(String name) => name is not null && _registrations.ContainsKey(name);

        /// <summary>
        /// Gets the paint definition registered under the given name.
        /// </summary>
        /// <param name="name">The name of the paint definition.</param>
        /// <returns>The paint definition or null if none has been registered.</returns>
        public IPaintDefinition? Get(String name) => Find(name)?.Definition;

        /// <summary>
        /// Gets the input properties of the paint definition as they have been registered,
        /// i.e., without unknown properties.
        /// </summary>
        /// <param name="name">The name of the paint definition.</param>
        /// <returns>The input properties or null if no paint definition has been registered.</returns>
        public IReadOnlyList<String>? GetInputProperties(String name) => Find(name)?.InputProperties;

        internal PaintRegistration? Find(String name) =>
            name is not null && _registrations.TryGetValue(name, out var registration) ? registration : null;

        #endregion
    }

    /// <summary>
    /// A registered paint definition with what has been read from it at registration.
    /// </summary>
    sealed class PaintRegistration
    {
        private PaintRegistration(String name, IPaintDefinition definition, String[] inputProperties, CssPropertySyntax[] argumentSyntaxes, PaintRenderingContext2DSettings contextOptions)
        {
            Name = name;
            Definition = definition;
            InputProperties = inputProperties;
            ArgumentSyntaxes = argumentSyntaxes;
            ContextOptions = contextOptions;
        }

        public static PaintRegistration Create(IBrowsingContext? context, String name, IPaintDefinition definition)
        {
            var inputProperties = new List<String>();

            foreach (var property in definition.InputProperties ?? Enumerable.Empty<String>())
            {
                if (property is not null && IsSupported(context, property))
                {
                    var normalized = TypedOmProperties.Normalize(property);

                    if (!inputProperties.Contains(normalized))
                    {
                        inputProperties.Add(normalized);
                    }
                }
            }

            var syntaxes = (definition.InputArguments ?? Enumerable.Empty<String>())
                .Select(text => CssPropertySyntax.Parse(text ?? String.Empty) ??
                    throw new ArgumentException($"The syntax '{text}' of an input argument is not valid.", nameof(definition)))
                .ToArray();
            var options = definition.ContextOptions ?? new PaintRenderingContext2DSettings();
            return new PaintRegistration(name, definition, inputProperties.ToArray(), syntaxes, new PaintRenderingContext2DSettings { Alpha = options.Alpha });
        }

        public String Name { get; }

        public IPaintDefinition Definition { get; }

        public String[] InputProperties { get; }

        public CssPropertySyntax[] ArgumentSyntaxes { get; }

        public PaintRenderingContext2DSettings ContextOptions { get; }

        private static Boolean IsSupported(IBrowsingContext? context, String property)
        {
            if (CssPropertyRegistration.IsCustomPropertyName(property))
            {
                return true;
            }

            try
            {
                TypedOmProperties.GetInfo(context, property.ToLowerInvariant());
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
