namespace AngleSharp.Css.Painting
{
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The default rendering context of paint definitions, which draws nothing but records
    /// the operations (a display list), so that any renderer can replay them, e.g., onto a
    /// bitmap of a graphics library. It keeps the drawing state (styles, transform, path)
    /// as the canvas API does, so reading a style gives what has been set, invalid values
    /// are ignored, and hit testing works on the current path. The coordinates are CSS
    /// pixels, i.e., the device pixel ratio is left to the renderer.
    /// </summary>
    public sealed class RecordingPaintRenderingContext2D : IPaintRenderingContext2D
    {
        #region Fields

        private static readonly HashSet<String> CompositeOperations = new(StringComparer.Ordinal)
        {
            "source-over", "source-in", "source-out", "source-atop",
            "destination-over", "destination-in", "destination-out", "destination-atop",
            "lighter", "copy", "xor",
            "normal", "multiply", "screen", "overlay", "darken", "lighten", "color-dodge",
            "color-burn", "hard-light", "soft-light", "difference", "exclusion", "hue",
            "saturation", "color", "luminosity",
        };

        private static readonly HashSet<String> Repetitions = new(StringComparer.Ordinal)
        {
            "repeat", "repeat-x", "repeat-y", "no-repeat",
        };

        private readonly List<PaintCommand> _commands = new();
        private readonly Stack<DrawingState> _stack = new();
        private readonly CanvasPathBuilder _path;
        private DrawingState _state = new();

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new recording context.
        /// </summary>
        /// <param name="size">The size of the image in CSS pixels.</param>
        /// <param name="devicePixelRatio">The number of device pixels per CSS pixel.</param>
        /// <param name="settings">The options of the paint definition, if any.</param>
        public RecordingPaintRenderingContext2D(PaintSize size, Double devicePixelRatio = 1, PaintRenderingContext2DSettings? settings = null)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
            DevicePixelRatio = devicePixelRatio;
            Settings = settings ?? new PaintRenderingContext2DSettings();
            _path = new CanvasPathBuilder(_commands, new PathGeometry());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the size of the image in CSS pixels.
        /// </summary>
        public PaintSize Size { get; }

        /// <summary>
        /// Gets the number of device pixels per CSS pixel the image is drawn for.
        /// </summary>
        public Double DevicePixelRatio { get; }

        /// <summary>
        /// Gets the options of the paint definition, e.g., if the image is opaque.
        /// </summary>
        public PaintRenderingContext2DSettings Settings { get; }

        /// <summary>
        /// Gets the recorded operations in the order they have been called.
        /// </summary>
        public IReadOnlyList<PaintCommand> Commands => _commands;

        #endregion

        #region CanvasState

        /// <inheritdoc />
        public void Save()
        {
            _stack.Push(_state.Clone());
            Record("save");
        }

        /// <inheritdoc />
        public void Restore()
        {
            if (_stack.Count > 0)
            {
                _state = _stack.Pop();
                Record("restore");
            }
        }

        /// <inheritdoc />
        public void Reset()
        {
            // nothing drawn before is visible anymore, so there is nothing to replay
            _commands.Clear();
            _stack.Clear();
            _path.Geometry.Clear();
            _state = new DrawingState();
        }

        #endregion

        #region CanvasTransform

        /// <inheritdoc />
        public void Scale(Double x, Double y)
        {
            if (Finite(x, y))
            {
                _state.Transform = _state.Transform.Multiply(new PaintMatrix(x, 0, 0, y, 0, 0));
                Record("scale", x, y);
            }
        }

        /// <inheritdoc />
        public void Rotate(Double angle)
        {
            if (Finite(angle))
            {
                var cos = Math.Cos(angle);
                var sin = Math.Sin(angle);
                _state.Transform = _state.Transform.Multiply(new PaintMatrix(cos, sin, -sin, cos, 0, 0));
                Record("rotate", angle);
            }
        }

        /// <inheritdoc />
        public void Translate(Double x, Double y)
        {
            if (Finite(x, y))
            {
                _state.Transform = _state.Transform.Multiply(new PaintMatrix(1, 0, 0, 1, x, y));
                Record("translate", x, y);
            }
        }

        /// <inheritdoc />
        public void Transform(Double a, Double b, Double c, Double d, Double e, Double f)
        {
            if (Finite(a, b, c, d, e, f))
            {
                _state.Transform = _state.Transform.Multiply(new PaintMatrix(a, b, c, d, e, f));
                Record("transform", a, b, c, d, e, f);
            }
        }

        /// <inheritdoc />
        public void SetTransform(Double a, Double b, Double c, Double d, Double e, Double f)
        {
            if (Finite(a, b, c, d, e, f))
            {
                _state.Transform = new PaintMatrix(a, b, c, d, e, f);
                Record("setTransform", a, b, c, d, e, f);
            }
        }

        /// <inheritdoc />
        public void ResetTransform()
        {
            _state.Transform = PaintMatrix.Identity;
            Record("resetTransform");
        }

        #endregion

        #region CanvasCompositing

        /// <inheritdoc />
        public Double GlobalAlpha
        {
            get => _state.GlobalAlpha;
            set
            {
                if (Finite(value) && value >= 0 && value <= 1)
                {
                    _state.GlobalAlpha = value;
                    Record("globalAlpha", value);
                }
            }
        }

        /// <inheritdoc />
        public String GlobalCompositeOperation
        {
            get => _state.GlobalCompositeOperation;
            set
            {
                if (value is not null && CompositeOperations.Contains(value))
                {
                    _state.GlobalCompositeOperation = value;
                    Record("globalCompositeOperation", value);
                }
            }
        }

        #endregion

        #region CanvasImageSmoothing

        /// <inheritdoc />
        public Boolean ImageSmoothingEnabled
        {
            get => _state.ImageSmoothingEnabled;
            set
            {
                _state.ImageSmoothingEnabled = value;
                Record("imageSmoothingEnabled", value);
            }
        }

        /// <inheritdoc />
        public ImageSmoothingQuality ImageSmoothingQuality
        {
            get => _state.ImageSmoothingQuality;
            set
            {
                if (value <= ImageSmoothingQuality.High)
                {
                    _state.ImageSmoothingQuality = value;
                    Record("imageSmoothingQuality", value);
                }
            }
        }

        #endregion

        #region CanvasFillStrokeStyles

        /// <inheritdoc />
        public Object StrokeStyle
        {
            get => _state.StrokeStyle;
            set
            {
                var style = NormalizeStyle(value);

                if (style is not null)
                {
                    _state.StrokeStyle = style;
                    Record("strokeStyle", style);
                }
            }
        }

        /// <inheritdoc />
        public Object FillStyle
        {
            get => _state.FillStyle;
            set
            {
                var style = NormalizeStyle(value);

                if (style is not null)
                {
                    _state.FillStyle = style;
                    Record("fillStyle", style);
                }
            }
        }

        /// <inheritdoc />
        public CanvasGradient CreateLinearGradient(Double x0, Double y0, Double x1, Double y1)
        {
            if (!Finite(x0, y0, x1, y1))
                throw new ArgumentException("The coordinates must be finite.");

            return new CanvasGradient(CanvasGradientKind.Linear, x0, y0, x1, y1);
        }

        /// <inheritdoc />
        public CanvasGradient CreateRadialGradient(Double x0, Double y0, Double r0, Double x1, Double y1, Double r1)
        {
            if (!Finite(x0, y0, r0, x1, y1, r1))
                throw new ArgumentException("The coordinates must be finite.");

            if (r0 < 0 || r1 < 0)
                throw new DomException(DomError.IndexSizeError);

            return new CanvasGradient(CanvasGradientKind.Radial, x0, y0, r0, x1, y1, r1);
        }

        /// <inheritdoc />
        public CanvasGradient CreateConicGradient(Double startAngle, Double x, Double y)
        {
            if (!Finite(startAngle, x, y))
                throw new ArgumentException("The coordinates must be finite.");

            return new CanvasGradient(CanvasGradientKind.Conic, startAngle, x, y);
        }

        /// <inheritdoc />
        public CanvasPattern? CreatePattern(CssImageValue image, String? repetition)
        {
            if (image is null)
                throw new ArgumentNullException(nameof(image));

            var mode = String.IsNullOrEmpty(repetition) ? "repeat" : repetition!;

            if (!Repetitions.Contains(mode))
                throw new DomException(DomError.Syntax);

            return new CanvasPattern(image, mode);
        }

        #endregion

        #region CanvasShadowStyles

        /// <inheritdoc />
        public Double ShadowOffsetX
        {
            get => _state.ShadowOffsetX;
            set
            {
                if (Finite(value))
                {
                    _state.ShadowOffsetX = value;
                    Record("shadowOffsetX", value);
                }
            }
        }

        /// <inheritdoc />
        public Double ShadowOffsetY
        {
            get => _state.ShadowOffsetY;
            set
            {
                if (Finite(value))
                {
                    _state.ShadowOffsetY = value;
                    Record("shadowOffsetY", value);
                }
            }
        }

        /// <inheritdoc />
        public Double ShadowBlur
        {
            get => _state.ShadowBlur;
            set
            {
                if (Finite(value) && value >= 0)
                {
                    _state.ShadowBlur = value;
                    Record("shadowBlur", value);
                }
            }
        }

        /// <inheritdoc />
        public String ShadowColor
        {
            get => _state.ShadowColor;
            set
            {
                var color = CanvasColor.Normalize(value);

                if (color is not null)
                {
                    _state.ShadowColor = color;
                    Record("shadowColor", color);
                }
            }
        }

        #endregion

        #region CanvasRect

        /// <inheritdoc />
        public void ClearRect(Double x, Double y, Double w, Double h)
        {
            if (Finite(x, y, w, h))
            {
                Record("clearRect", x, y, w, h);
            }
        }

        /// <inheritdoc />
        public void FillRect(Double x, Double y, Double w, Double h)
        {
            if (Finite(x, y, w, h))
            {
                Record("fillRect", x, y, w, h);
            }
        }

        /// <inheritdoc />
        public void StrokeRect(Double x, Double y, Double w, Double h)
        {
            if (Finite(x, y, w, h))
            {
                Record("strokeRect", x, y, w, h);
            }
        }

        #endregion

        #region CanvasDrawPath

        /// <inheritdoc />
        public void BeginPath()
        {
            _path.Geometry.Clear();
            Record("beginPath");
        }

        /// <inheritdoc />
        public void Fill(CanvasFillRule fillRule = CanvasFillRule.NonZero) => Record("fill", fillRule);

        /// <inheritdoc />
        public void Fill(Path2D path, CanvasFillRule fillRule = CanvasFillRule.NonZero) =>
            Record("fill", Snapshot(path), fillRule);

        /// <inheritdoc />
        public void Stroke() => Record("stroke");

        /// <inheritdoc />
        public void Stroke(Path2D path) => Record("stroke", Snapshot(path));

        /// <inheritdoc />
        public void Clip(CanvasFillRule fillRule = CanvasFillRule.NonZero) => Record("clip", fillRule);

        /// <inheritdoc />
        public void Clip(Path2D path, CanvasFillRule fillRule = CanvasFillRule.NonZero) =>
            Record("clip", Snapshot(path), fillRule);

        /// <inheritdoc />
        public Boolean IsPointInPath(Double x, Double y, CanvasFillRule fillRule = CanvasFillRule.NonZero) =>
            Finite(x, y) && _path.Geometry.Contains(new PaintPoint(x, y), fillRule);

        /// <inheritdoc />
        public Boolean IsPointInPath(Path2D path, Double x, Double y, CanvasFillRule fillRule = CanvasFillRule.NonZero)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var transform = _state.Transform;
            return Finite(x, y) && transform.IsInvertible && path.Geometry.Contains(transform.Invert().Apply(x, y), fillRule);
        }

        /// <inheritdoc />
        public Boolean IsPointInStroke(Double x, Double y) =>
            Finite(x, y) && _path.Geometry.StrokeContains(new PaintPoint(x, y), _state.LineWidth * _state.Transform.Scale);

        /// <inheritdoc />
        public Boolean IsPointInStroke(Path2D path, Double x, Double y)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var transform = _state.Transform;
            return Finite(x, y) && transform.IsInvertible && path.Geometry.StrokeContains(transform.Invert().Apply(x, y), _state.LineWidth);
        }

        #endregion

        #region CanvasDrawImage

        /// <inheritdoc />
        public void DrawImage(CssImageValue image, Double dx, Double dy)
        {
            if (image is null)
                throw new ArgumentNullException(nameof(image));

            if (Finite(dx, dy))
            {
                Record("drawImage", image, dx, dy);
            }
        }

        /// <inheritdoc />
        public void DrawImage(CssImageValue image, Double dx, Double dy, Double dw, Double dh)
        {
            if (image is null)
                throw new ArgumentNullException(nameof(image));

            if (Finite(dx, dy, dw, dh))
            {
                Record("drawImage", image, dx, dy, dw, dh);
            }
        }

        /// <inheritdoc />
        public void DrawImage(CssImageValue image, Double sx, Double sy, Double sw, Double sh, Double dx, Double dy, Double dw, Double dh)
        {
            if (image is null)
                throw new ArgumentNullException(nameof(image));

            if (Finite(sx, sy, sw, sh, dx, dy, dw, dh))
            {
                Record("drawImage", image, sx, sy, sw, sh, dx, dy, dw, dh);
            }
        }

        #endregion

        #region CanvasPathDrawingStyles

        /// <inheritdoc />
        public Double LineWidth
        {
            get => _state.LineWidth;
            set
            {
                if (Finite(value) && value > 0)
                {
                    _state.LineWidth = value;
                    Record("lineWidth", value);
                }
            }
        }

        /// <inheritdoc />
        public CanvasLineCap LineCap
        {
            get => _state.LineCap;
            set
            {
                if (value <= CanvasLineCap.Square)
                {
                    _state.LineCap = value;
                    Record("lineCap", value);
                }
            }
        }

        /// <inheritdoc />
        public CanvasLineJoin LineJoin
        {
            get => _state.LineJoin;
            set
            {
                if (value <= CanvasLineJoin.Miter)
                {
                    _state.LineJoin = value;
                    Record("lineJoin", value);
                }
            }
        }

        /// <inheritdoc />
        public Double MiterLimit
        {
            get => _state.MiterLimit;
            set
            {
                if (Finite(value) && value > 0)
                {
                    _state.MiterLimit = value;
                    Record("miterLimit", value);
                }
            }
        }

        /// <inheritdoc />
        public void SetLineDash(IEnumerable<Double> segments)
        {
            if (segments is null)
                throw new ArgumentNullException(nameof(segments));

            var list = segments.ToList();

            if (list.Any(m => !Finite(m) || m < 0))
            {
                return;
            }

            if (list.Count % 2 == 1)
            {
                list.AddRange(list.ToArray());
            }

            _state.LineDash = list.ToArray();
            Record("setLineDash", (Double[])_state.LineDash.Clone());
        }

        /// <inheritdoc />
        public Double[] GetLineDash() => (Double[])_state.LineDash.Clone();

        /// <inheritdoc />
        public Double LineDashOffset
        {
            get => _state.LineDashOffset;
            set
            {
                if (Finite(value))
                {
                    _state.LineDashOffset = value;
                    Record("lineDashOffset", value);
                }
            }
        }

        #endregion

        #region CanvasPath

        /// <inheritdoc />
        public void ClosePath() => _path.ClosePath();

        /// <inheritdoc />
        public void MoveTo(Double x, Double y) => _path.MoveTo(x, y, _state.Transform);

        /// <inheritdoc />
        public void LineTo(Double x, Double y) => _path.LineTo(x, y, _state.Transform);

        /// <inheritdoc />
        public void QuadraticCurveTo(Double cpx, Double cpy, Double x, Double y) =>
            _path.QuadraticCurveTo(cpx, cpy, x, y, _state.Transform);

        /// <inheritdoc />
        public void BezierCurveTo(Double cp1x, Double cp1y, Double cp2x, Double cp2y, Double x, Double y) =>
            _path.BezierCurveTo(cp1x, cp1y, cp2x, cp2y, x, y, _state.Transform);

        /// <inheritdoc />
        public void ArcTo(Double x1, Double y1, Double x2, Double y2, Double radius) =>
            _path.ArcTo(x1, y1, x2, y2, radius, _state.Transform);

        /// <inheritdoc />
        public void Rect(Double x, Double y, Double w, Double h) => _path.Rect(x, y, w, h, _state.Transform);

        /// <inheritdoc />
        public void RoundRect(Double x, Double y, Double w, Double h, params Double[] radii) =>
            _path.RoundRect(x, y, w, h, radii, _state.Transform);

        /// <inheritdoc />
        public void Arc(Double x, Double y, Double radius, Double startAngle, Double endAngle, Boolean counterclockwise = false) =>
            _path.Arc(x, y, radius, startAngle, endAngle, counterclockwise, _state.Transform);

        /// <inheritdoc />
        public void Ellipse(Double x, Double y, Double radiusX, Double radiusY, Double rotation, Double startAngle, Double endAngle, Boolean counterclockwise = false) =>
            _path.Ellipse(x, y, radiusX, radiusY, rotation, startAngle, endAngle, counterclockwise, _state.Transform);

        #endregion

        #region Helpers

        private void Record(String name, params Object[] arguments) => _commands.Add(new PaintCommand(name, arguments));

        private static Boolean Finite(params Double[] values) => CanvasPathBuilder.Finite(values);

        // a path may change after it has been drawn, which does not change the drawing
        private static Path2D Snapshot(Path2D path) => new(path ?? throw new ArgumentNullException(nameof(path)));

        private static Object? NormalizeStyle(Object? value) => value switch
        {
            String text => CanvasColor.Normalize(text),
            CanvasGradient or CanvasPattern => value,
            _ => null,
        };

        private sealed class DrawingState
        {
            public PaintMatrix Transform { get; set; } = PaintMatrix.Identity;

            public Object FillStyle { get; set; } = CanvasColor.Black;

            public Object StrokeStyle { get; set; } = CanvasColor.Black;

            public Double GlobalAlpha { get; set; } = 1;

            public String GlobalCompositeOperation { get; set; } = "source-over";

            public Boolean ImageSmoothingEnabled { get; set; } = true;

            public ImageSmoothingQuality ImageSmoothingQuality { get; set; } = ImageSmoothingQuality.Low;

            public Double LineWidth { get; set; } = 1;

            public CanvasLineCap LineCap { get; set; } = CanvasLineCap.Butt;

            public CanvasLineJoin LineJoin { get; set; } = CanvasLineJoin.Miter;

            public Double MiterLimit { get; set; } = 10;

            public Double[] LineDash { get; set; } = Array.Empty<Double>();

            public Double LineDashOffset { get; set; }

            public Double ShadowOffsetX { get; set; }

            public Double ShadowOffsetY { get; set; }

            public Double ShadowBlur { get; set; }

            public String ShadowColor { get; set; } = CanvasColor.TransparentBlack;

            public DrawingState Clone() => (DrawingState)MemberwiseClone();
        }

        #endregion
    }
}
