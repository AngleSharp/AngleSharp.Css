namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

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
        {
            _context = context;
            _media = new List<ICssMedium>();
        }

        #endregion

        #region Index

        public String this[Int32 index] => _media[index].ToCss();

        #endregion

        #region Properties

        public Int32 Length => _media.Count;

        public ICssParser Parser => _context.GetService<ICssParser>();

        public IFeatureValidatorFactory ValidatorFactory => _context.GetService<IFeatureValidatorFactory>();

        public String MediaText
        {
            get => this.ToCss();
            set => SetMediaText(value, throwOnError: true);
        }

        #endregion

        #region Methods

        public void SetMediaText(String value, Boolean throwOnError)
        {
            _media.Clear();
            var media = MediaParser.Parse(value ?? CssKeywords.All, ValidatorFactory);

            if (media != null)
            {
                _media.AddRange(media);
            }
            else if (throwOnError)
            {
                throw new DomException(DomError.Syntax);
            }

            if (_media.Count == 0)
            {
                _media.Add(new CssMedium(CssKeywords.All, inverse: true, exclusive: false));
            }
        }

        public void Add(String newMedium)
        {
            var medium = MediumParser.Parse(newMedium, ValidatorFactory) ?? throw new DomException(DomError.Syntax);
            _media.Add(medium);
        }

        public void Remove(String oldMedium)
        {
            var medium = MediumParser.Parse(oldMedium, ValidatorFactory) ?? throw new DomException(DomError.Syntax);

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

        public void Replace(IEnumerable<ICssMedium> media)
        {
            _media.Clear();
            _media.AddRange(media);
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

        public IEnumerator<ICssMedium> GetEnumerator() => _media.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
