namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// Keeps the registrations in effect for a document (see CssPropertyRegistry.ForDocument)
    /// until something they were collected from changes. Checking that costs a few reads, not
    /// a walk over the DOM or the rules:
    /// - the DOM (inserted or removed style elements, edited style text, changed attributes
    ///   such as media or disabled) by the mutation version of the document,
    /// - the CSSOM (inserted or removed rules, changed @property descriptors) by a version
    ///   that these changes advance (see Invalidate),
    /// - the style sheets of the style elements (e.g., a link that loaded), whether they are
    ///   disabled, the adopted and the default style sheets, and the registrations of script
    ///   by their references,
    /// - the conditions (@media, @supports, and the media of a sheet) around @property rules,
    ///   which depend on the device, by evaluating them again.
    /// </summary>
    static class CssPropertyRegistryCache
    {
        #region Fields

        private static readonly ConditionalWeakTable<IDocument, Holder> Entries = new();
        private static Int64 _version;

        #endregion

        #region Methods

        /// <summary>
        /// Marks the registrations of all documents as outdated, e.g., as a rule that may hold
        /// a @property rule (but not a style rule) was inserted.
        /// </summary>
        public static void Invalidate() => Interlocked.Increment(ref _version);

        /// <summary>
        /// Gets the registrations in effect for the document, which are only collected again
        /// if something they depend on changed. The registry must not be modified.
        /// </summary>
        public static CssPropertyRegistry Get(IDocument document)
        {
            var context = document.Context;
            var device = context?.GetService<IRenderDevice>() ?? DefaultDevice;
            var defaults = GetDefaultSheets(context);
            var scripted = CssScriptPropertyRegistrations.Get(document);

            // the cache needs the mutation version, which only AngleSharp's own document has
            if (document is not Document dom)
            {
                return Collect(document, device, defaults);
            }

            var holder = Entries.GetValue(document, _ => new Holder());
            var entry = holder.Entry;

            if (entry is not null && entry.IsCurrent(dom, device, defaults, scripted))
            {
                return entry.Registry;
            }

            // read before collecting: a change made meanwhile makes the entry outdated
            var version = Interlocked.Read(ref _version);
            var mutationVersion = dom.MutationVersion;
            var owners = document.GetDescendants().OfType<ILinkStyle>().ToArray();
            var ownerSheets = Array.ConvertAll(owners, m => m.Sheet);
            var adopted = document.GetAdoptedStyleSheets().ToArray();
            var registry = Collect(document, device, defaults);
            var sheets = defaults.Concat(ownerSheets.OfType<ICssStyleSheet>()).Concat(adopted);
            holder.Entry = new Entry(version, mutationVersion, device, defaults, scripted, owners, ownerSheets, adopted, GetGates(sheets), registry);
            return registry;
        }

        #endregion

        #region Helpers

        private static readonly IRenderDevice DefaultDevice = new DefaultRenderDevice();

        private static ICssStyleSheet[] GetDefaultSheets(IBrowsingContext? context)
        {
            var providers = context?.GetServices<ICssDefaultStyleSheetProvider>() ?? Enumerable.Empty<ICssDefaultStyleSheetProvider>();
            return providers.Select(m => m.Default).Where(m => m is not null).ToArray();
        }

        private static CssPropertyRegistry Collect(IDocument document, IRenderDevice device, ICssStyleSheet[] defaults)
        {
            var collection = new StyleCollection(defaults, document.GetEffectiveStyleSheets(), device, document: document);
            return collection.GetRegistrations();
        }

        /// <summary>
        /// Gets the conditions the @property rules of the sheets are under, exactly the ones
        /// the style collection follows: the media of a sheet, @media, and @supports.
        /// </summary>
        private static Gate[] GetGates(IEnumerable<ICssStyleSheet> sheets)
        {
            var gates = new List<Gate>();

            foreach (var sheet in sheets)
            {
                if (CollectGates(sheet.Rules, gates) && sheet.Media.Length > 0)
                {
                    var media = sheet.Media;
                    gates.Add(new Gate(device => media.Validate(device)));
                }
            }

            return gates.ToArray();
        }

        /// <returns>True if the rules hold a @property rule, also within nested rules.</returns>
        private static Boolean CollectGates(ICssRuleList rules, List<Gate> gates)
        {
            var found = false;

            foreach (var rule in rules)
            {
                switch (rule)
                {
                    case ICssPropertyRule:
                        found = true;
                        break;
                    case ICssMediaRule media when CollectGates(media.Rules, gates):
                        gates.Add(new Gate(device => media.IsValid(device)));
                        found = true;
                        break;
                    case ICssSupportsRule supports when CollectGates(supports.Rules, gates):
                        gates.Add(new Gate(device => supports.IsValid(device)));
                        found = true;
                        break;
                    case ICssLayerRule layer when !layer.IsStatement && CollectGates(layer.Rules, gates):
                        found = true;
                        break;
                }
            }

            return found;
        }

        #endregion

        #region Entry

        private sealed class Holder
        {
            private Entry? _entry;

            public Entry? Entry
            {
                get => Volatile.Read(ref _entry);
                set => Volatile.Write(ref _entry, value);
            }
        }

        private readonly struct Gate
        {
            private readonly Func<IRenderDevice, Boolean> _evaluate;

            public Gate(Func<IRenderDevice, Boolean> evaluate)
            {
                _evaluate = evaluate;
                Result = null;
            }

            private Gate(Func<IRenderDevice, Boolean> evaluate, Boolean result)
            {
                _evaluate = evaluate;
                Result = result;
            }

            public Boolean? Result { get; }

            public Gate Evaluate(IRenderDevice device) => new(_evaluate, _evaluate(device));

            public Boolean IsUnchanged(IRenderDevice device) => _evaluate(device) == Result;
        }

        private sealed class Entry
        {
            private readonly Int64 _version;
            private readonly Int64 _mutationVersion;
            private readonly IRenderDevice _device;
            private readonly ICssStyleSheet[] _defaults;
            private readonly CssPropertyRegistry? _scripted;
            private readonly ILinkStyle[] _owners;
            private readonly IStyleSheet?[] _ownerSheets;
            private readonly Boolean[] _ownerDisabled;
            private readonly ICssStyleSheet[] _adopted;
            private readonly Boolean[] _adoptedDisabled;
            private readonly Gate[] _gates;

            public Entry(Int64 version, Int64 mutationVersion, IRenderDevice device, ICssStyleSheet[] defaults, CssPropertyRegistry? scripted,
                ILinkStyle[] owners, IStyleSheet?[] ownerSheets, ICssStyleSheet[] adopted, Gate[] gates, CssPropertyRegistry registry)
            {
                _version = version;
                _mutationVersion = mutationVersion;
                _device = device;
                _defaults = defaults;
                _scripted = scripted;
                _owners = owners;
                _ownerSheets = ownerSheets;
                _ownerDisabled = Array.ConvertAll(ownerSheets, m => m?.IsDisabled ?? false);
                _adopted = adopted;
                _adoptedDisabled = Array.ConvertAll(adopted, m => m.IsDisabled);
                _gates = Array.ConvertAll(gates, m => m.Evaluate(device));
                Registry = registry;
            }

            public CssPropertyRegistry Registry { get; }

            public Boolean IsCurrent(Document document, IRenderDevice device, ICssStyleSheet[] defaults, CssPropertyRegistry? scripted)
            {
                if (_version != Interlocked.Read(ref CssPropertyRegistryCache._version) ||
                    _mutationVersion != document.MutationVersion ||
                    !ReferenceEquals(_device, device) ||
                    !ReferenceEquals(_scripted, scripted) ||
                    !SameSheets(_defaults, defaults))
                {
                    return false;
                }

                for (var i = 0; i < _owners.Length; i++)
                {
                    var sheet = _owners[i].Sheet;

                    if (!ReferenceEquals(sheet, _ownerSheets[i]) || (sheet?.IsDisabled ?? false) != _ownerDisabled[i])
                    {
                        return false;
                    }
                }

                var adopted = document.GetAdoptedStyleSheets();

                if (adopted.Count != _adopted.Length)
                {
                    return false;
                }

                for (var i = 0; i < _adopted.Length; i++)
                {
                    if (!ReferenceEquals(adopted[i], _adopted[i]) || _adopted[i].IsDisabled != _adoptedDisabled[i])
                    {
                        return false;
                    }
                }

                foreach (var gate in _gates)
                {
                    if (!gate.IsUnchanged(device))
                    {
                        return false;
                    }
                }

                return true;
            }

            private static Boolean SameSheets(ICssStyleSheet[] a, ICssStyleSheet[] b)
            {
                if (a.Length != b.Length)
                {
                    return false;
                }

                for (var i = 0; i < a.Length; i++)
                {
                    if (!ReferenceEquals(a[i], b[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        #endregion
    }
}
