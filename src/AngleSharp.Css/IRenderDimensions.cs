using System;

namespace AngleSharp.Css
{
    public interface IRenderDimensions
    {
        Int32 RenderWidth { get; set; }
        Int32 RenderHeight { get; set; }
        double FontSize { get; set; }
    }
}