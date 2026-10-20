namespace AngleSharp.Css.Values;

using AngleSharp.Css.Dom;
using System;

interface IInheritingComputeContext
{
    ICssValue? InheritedValue(String name);
}
