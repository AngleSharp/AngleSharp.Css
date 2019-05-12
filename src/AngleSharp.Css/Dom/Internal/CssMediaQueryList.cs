namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Dom.Events;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// Represents the class implementing a media query list for notifications.
    /// </summary>
    sealed class CssMediaQueryList : EventTarget, IMediaQueryList
    {
        #region Fields

        private readonly IMediaList _media;
        private Boolean _matched;

        #endregion

        #region Events

        public event DomEventHandler Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        #endregion

        #region ctor

        public CssMediaQueryList(IWindow window, IMediaList media)
        {
            _media = media;
            _matched = ComputeMatched(window);
            window.Resized += Resized;
        }

        #endregion

        #region Properties

        public String MediaText => _media.MediaText;

        public IMediaList Media => _media;

        public Boolean IsMatched => _matched;

        #endregion

        #region Helpers

        //TODO use Validate with RenderDevice
        private Boolean ComputeMatched(IWindow window) => false;

        private void Resized(Object sender, Event ev)
        {
            var window = (IWindow)sender;
            var matched = ComputeMatched(window);

            if (matched != _matched)
            {
                var eventData = new MediaQueryListEvent(EventNames.Change, false, false, _media.MediaText, matched);
                Dispatch(eventData);
            }

            _matched = matched;
        }

        #endregion
    }
}
