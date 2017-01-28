namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System.Collections.Generic;

    interface IValueDistributor
    {
        IEnumerable<ICssProperty> Distribute();
    }
}
