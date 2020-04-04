namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;

    sealed class CssPseudoElement : ICssPseudoElement
    {
        private readonly IPseudoElement _host;
        private readonly String _type;
        private readonly ICssStyleDeclaration _style;

        public CssPseudoElement(IPseudoElement host, String type, ICssStyleDeclaration style)
        {
            _host = host;
            _type = type;
            _style = style;
        }

        String ICssPseudoElement.Type => _type;

        ICssStyleDeclaration ICssPseudoElement.Style => _style;

        void IEventTarget.AddEventListener(string type, DomEventHandler callback, bool capture) => _host.AddEventListener(type, callback, capture);

        bool IEventTarget.Dispatch(Event ev) => _host.Dispatch(ev);

        void IEventTarget.InvokeEventListener(Event ev) => _host.InvokeEventListener(ev);

        void IEventTarget.RemoveEventListener(string type, DomEventHandler callback, bool capture) => _host.RemoveEventListener(type, callback, capture);
    }
}
