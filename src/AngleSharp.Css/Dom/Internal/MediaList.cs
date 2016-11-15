namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using Parser;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a list of media elements.
    /// </summary>
    sealed class MediaList : IMediaList
    {
        #region Fields

        private readonly IBrowsingContext _context;
        private readonly List<ICssMedium> _media;

        #endregion

        #region ctor

        internal MediaList(IBrowsingContext context)
            : this(context, Enumerable.Empty<ICssMedium>())
        {
        }

        internal MediaList(IBrowsingContext context, IEnumerable<ICssMedium> media)
        {
            _context = context;
            _media = new List<ICssMedium>(media);
        }

        #endregion

        #region Index

        public String this[Int32 index]
        {
            get { return _media[index].ToCss(); }
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _media.Count; }
        }

        public ICssParser Parser
        {
            get { return _context.GetService<ICssParser>(); }
        }

        public String MediaText
        {
            get { return this.ToCss(); }
            set
            {
                _media.Clear();

                foreach (var medium in Parser.ParseMedia(value))
                {
                    if (medium == null)
                        throw new DomException(DomError.Syntax);

                    _media.Add(medium);
                }
            }
        }

        #endregion

        #region Methods

        public void Add(String newMedium)
        {
            var medium = Parser.ParseMedium(newMedium);

            if (medium == null)
                throw new DomException(DomError.Syntax);

            _media.Add(medium);
        }

        public void Add(ICssMedium medium)
        {
            _media.Add(medium);
        }

        public void Clear()
        {
            _media.Clear();
        }

        public void AddRange(IEnumerable<ICssMedium> media)
        {
            _media.AddRange(media);
        }

        public void Remove(String oldMedium)
        {
            var medium = Parser.ParseMedium(oldMedium);

            if (medium == null)
                throw new DomException(DomError.Syntax);

            for (var i = 0; i < _media.Count; i++)
            {
                if (_media[i].Equals(medium))
                {
                    _media.RemoveAt(i);
                    return;
                }
            }

            throw new DomException(DomError.NotFound);
        }

        public void Remove(ICssMedium medium)
        {
            _media.Remove(medium);
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_media.Count > 0)
            {
                _media[0].ToCss(writer, formatter);

                for (var i = 1; i < _media.Count; i++)
                {
                    writer.Write(", ");
                    _media[i].ToCss(writer, formatter);
                }
            }
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<ICssMedium> GetEnumerator()
        {
            return _media.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
