namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.TypedOm;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The rendering context of a paint definition, which is a subset of the one of a
    /// canvas: there is no text, no access to the pixels, and only images given as CSS
    /// values (CssImageValue) can be drawn. Its implementation decides what drawing
    /// produces, e.g., recorded operations or a bitmap.
    /// https://www.w3.org/TR/css-paint-api-1/#paintrenderingcontext2d
    /// </summary>
    [DomName("PaintRenderingContext2D")]
    public interface IPaintRenderingContext2D : ICanvasPath
    {
        #region CanvasState

        /// <summary>
        /// Pushes the drawing state (transform, clip, styles) onto the stack.
        /// </summary>
        [DomName("save")]
        void Save();

        /// <summary>
        /// Pops the drawing state from the stack, if there is one.
        /// </summary>
        [DomName("restore")]
        void Restore();

        /// <summary>
        /// Resets the context: clears the image, the path, the stack and the state.
        /// </summary>
        [DomName("reset")]
        void Reset();

        #endregion

        #region CanvasTransform

        /// <summary>
        /// Scales the current transform.
        /// </summary>
        [DomName("scale")]
        void Scale(Double x, Double y);

        /// <summary>
        /// Rotates the current transform clockwise by the angle (in radians).
        /// </summary>
        [DomName("rotate")]
        void Rotate(Double angle);

        /// <summary>
        /// Translates the current transform.
        /// </summary>
        [DomName("translate")]
        void Translate(Double x, Double y);

        /// <summary>
        /// Multiplies the current transform with the given matrix.
        /// </summary>
        [DomName("transform")]
        void Transform(Double a, Double b, Double c, Double d, Double e, Double f);

        /// <summary>
        /// Replaces the current transform with the given matrix.
        /// </summary>
        [DomName("setTransform")]
        void SetTransform(Double a, Double b, Double c, Double d, Double e, Double f);

        /// <summary>
        /// Resets the current transform to the identity.
        /// </summary>
        [DomName("resetTransform")]
        void ResetTransform();

        #endregion

        #region CanvasCompositing

        /// <summary>
        /// Gets or sets the alpha applied to drawing, from 0 to 1.
        /// </summary>
        [DomName("globalAlpha")]
        Double GlobalAlpha { get; set; }

        /// <summary>
        /// Gets or sets the composite operation or blend mode, e.g., source-over.
        /// </summary>
        [DomName("globalCompositeOperation")]
        String GlobalCompositeOperation { get; set; }

        #endregion

        #region CanvasImageSmoothing

        /// <summary>
        /// Gets or sets if scaled images are smoothed.
        /// </summary>
        [DomName("imageSmoothingEnabled")]
        Boolean ImageSmoothingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the quality of smoothing scaled images.
        /// </summary>
        [DomName("imageSmoothingQuality")]
        ImageSmoothingQuality ImageSmoothingQuality { get; set; }

        #endregion

        #region CanvasFillStrokeStyles

        /// <summary>
        /// Gets or sets the style of strokes: a color (as a string, serialized when read),
        /// a <see cref="CanvasGradient"/>, or a <see cref="CanvasPattern"/>.
        /// </summary>
        [DomName("strokeStyle")]
        Object StrokeStyle { get; set; }

        /// <summary>
        /// Gets or sets the style of fills: a color (as a string, serialized when read),
        /// a <see cref="CanvasGradient"/>, or a <see cref="CanvasPattern"/>.
        /// </summary>
        [DomName("fillStyle")]
        Object FillStyle { get; set; }

        /// <summary>
        /// Creates a linear gradient along the line between both points.
        /// </summary>
        [DomName("createLinearGradient")]
        CanvasGradient CreateLinearGradient(Double x0, Double y0, Double x1, Double y1);

        /// <summary>
        /// Creates a radial gradient between both circles.
        /// </summary>
        /// <exception cref="AngleSharp.Dom.DomException">An IndexSizeError for a negative radius.</exception>
        [DomName("createRadialGradient")]
        CanvasGradient CreateRadialGradient(Double x0, Double y0, Double r0, Double x1, Double y1, Double r1);

        /// <summary>
        /// Creates a conic gradient around the point.
        /// </summary>
        [DomName("createConicGradient")]
        CanvasGradient CreateConicGradient(Double startAngle, Double x, Double y);

        /// <summary>
        /// Creates a pattern of the image, which is repeated as given by the repetition
        /// (repeat, repeat-x, repeat-y, no-repeat, or empty for repeat).
        /// </summary>
        /// <exception cref="AngleSharp.Dom.DomException">A SyntaxError for an unknown repetition.</exception>
        [DomName("createPattern")]
        CanvasPattern? CreatePattern(CssImageValue image, String? repetition);

        #endregion

        #region CanvasShadowStyles

        /// <summary>
        /// Gets or sets the horizontal offset of shadows.
        /// </summary>
        [DomName("shadowOffsetX")]
        Double ShadowOffsetX { get; set; }

        /// <summary>
        /// Gets or sets the vertical offset of shadows.
        /// </summary>
        [DomName("shadowOffsetY")]
        Double ShadowOffsetY { get; set; }

        /// <summary>
        /// Gets or sets the blur of shadows.
        /// </summary>
        [DomName("shadowBlur")]
        Double ShadowBlur { get; set; }

        /// <summary>
        /// Gets or sets the color of shadows (serialized when read).
        /// </summary>
        [DomName("shadowColor")]
        String ShadowColor { get; set; }

        #endregion

        #region CanvasRect

        /// <summary>
        /// Clears the rectangle to transparent black.
        /// </summary>
        [DomName("clearRect")]
        void ClearRect(Double x, Double y, Double w, Double h);

        /// <summary>
        /// Fills the rectangle with the fill style.
        /// </summary>
        [DomName("fillRect")]
        void FillRect(Double x, Double y, Double w, Double h);

        /// <summary>
        /// Strokes the rectangle with the stroke style.
        /// </summary>
        [DomName("strokeRect")]
        void StrokeRect(Double x, Double y, Double w, Double h);

        #endregion

        #region CanvasDrawPath

        /// <summary>
        /// Empties the current path.
        /// </summary>
        [DomName("beginPath")]
        void BeginPath();

        /// <summary>
        /// Fills the current path with the fill style.
        /// </summary>
        [DomName("fill")]
        void Fill(CanvasFillRule fillRule = CanvasFillRule.NonZero);

        /// <summary>
        /// Fills the given path with the fill style.
        /// </summary>
        [DomName("fill")]
        void Fill(Path2D path, CanvasFillRule fillRule = CanvasFillRule.NonZero);

        /// <summary>
        /// Strokes the current path with the stroke style.
        /// </summary>
        [DomName("stroke")]
        void Stroke();

        /// <summary>
        /// Strokes the given path with the stroke style.
        /// </summary>
        [DomName("stroke")]
        void Stroke(Path2D path);

        /// <summary>
        /// Intersects the clipping region with the current path.
        /// </summary>
        [DomName("clip")]
        void Clip(CanvasFillRule fillRule = CanvasFillRule.NonZero);

        /// <summary>
        /// Intersects the clipping region with the given path.
        /// </summary>
        [DomName("clip")]
        void Clip(Path2D path, CanvasFillRule fillRule = CanvasFillRule.NonZero);

        /// <summary>
        /// Checks if the point (unaffected by the current transform) is inside the current path.
        /// </summary>
        [DomName("isPointInPath")]
        Boolean IsPointInPath(Double x, Double y, CanvasFillRule fillRule = CanvasFillRule.NonZero);

        /// <summary>
        /// Checks if the point (unaffected by the current transform) is inside the given path.
        /// </summary>
        [DomName("isPointInPath")]
        Boolean IsPointInPath(Path2D path, Double x, Double y, CanvasFillRule fillRule = CanvasFillRule.NonZero);

        /// <summary>
        /// Checks if the point (unaffected by the current transform) is on the stroke of the current path.
        /// </summary>
        [DomName("isPointInStroke")]
        Boolean IsPointInStroke(Double x, Double y);

        /// <summary>
        /// Checks if the point (unaffected by the current transform) is on the stroke of the given path.
        /// </summary>
        [DomName("isPointInStroke")]
        Boolean IsPointInStroke(Path2D path, Double x, Double y);

        #endregion

        #region CanvasDrawImage

        /// <summary>
        /// Draws the image at its natural size at the given point.
        /// </summary>
        [DomName("drawImage")]
        void DrawImage(CssImageValue image, Double dx, Double dy);

        /// <summary>
        /// Draws the image into the given rectangle.
        /// </summary>
        [DomName("drawImage")]
        void DrawImage(CssImageValue image, Double dx, Double dy, Double dw, Double dh);

        /// <summary>
        /// Draws the source rectangle of the image into the destination rectangle.
        /// </summary>
        [DomName("drawImage")]
        void DrawImage(CssImageValue image, Double sx, Double sy, Double sw, Double sh, Double dx, Double dy, Double dw, Double dh);

        #endregion

        #region CanvasPathDrawingStyles

        /// <summary>
        /// Gets or sets the width of lines.
        /// </summary>
        [DomName("lineWidth")]
        Double LineWidth { get; set; }

        /// <summary>
        /// Gets or sets the shape of the ends of lines.
        /// </summary>
        [DomName("lineCap")]
        CanvasLineCap LineCap { get; set; }

        /// <summary>
        /// Gets or sets the shape of the corners of lines.
        /// </summary>
        [DomName("lineJoin")]
        CanvasLineJoin LineJoin { get; set; }

        /// <summary>
        /// Gets or sets the limit of the ratio of the miter length to half the line width.
        /// </summary>
        [DomName("miterLimit")]
        Double MiterLimit { get; set; }

        /// <summary>
        /// Sets the dash pattern of lines, which is repeated to have an even count.
        /// </summary>
        [DomName("setLineDash")]
        void SetLineDash(IEnumerable<Double> segments);

        /// <summary>
        /// Gets the dash pattern of lines.
        /// </summary>
        [DomName("getLineDash")]
        Double[] GetLineDash();

        /// <summary>
        /// Gets or sets the offset of the dash pattern.
        /// </summary>
        [DomName("lineDashOffset")]
        Double LineDashOffset { get; set; }

        #endregion
    }
}
