#nullable disable
namespace AngleSharp.Css.Tests.Painting
{
    using AngleSharp.Css.Painting;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    /// <summary>
    /// The default rendering context of paint definitions, which records the operations
    /// and keeps the drawing state as the canvas API does.
    /// </summary>
    [TestFixture]
    public class RecordingContextTests
    {
        private static RecordingPaintRenderingContext2D Create() => new(new PaintSize(100, 100));

        private static String[] Commands(RecordingPaintRenderingContext2D ctx) => ctx.Commands.Select(m => m.ToString()).ToArray();

        [Test]
        public void DefaultState()
        {
            var ctx = Create();

            Assert.AreEqual("#000000", ctx.FillStyle);
            Assert.AreEqual("#000000", ctx.StrokeStyle);
            Assert.AreEqual(1.0, ctx.GlobalAlpha);
            Assert.AreEqual("source-over", ctx.GlobalCompositeOperation);
            Assert.AreEqual(1.0, ctx.LineWidth);
            Assert.AreEqual(CanvasLineCap.Butt, ctx.LineCap);
            Assert.AreEqual(CanvasLineJoin.Miter, ctx.LineJoin);
            Assert.AreEqual(10.0, ctx.MiterLimit);
            Assert.AreEqual("rgba(0, 0, 0, 0)", ctx.ShadowColor);
            Assert.IsTrue(ctx.ImageSmoothingEnabled);
            Assert.AreEqual(ImageSmoothingQuality.Low, ctx.ImageSmoothingQuality);
            CollectionAssert.IsEmpty(ctx.GetLineDash());
            CollectionAssert.IsEmpty(ctx.Commands);
            Assert.IsTrue(ctx.Settings.Alpha);
        }

        [TestCase("red", "#ff0000")]
        [TestCase("#ABC", "#aabbcc")]
        [TestCase("rgb(1 2 3 / 50%)", "rgba(1, 2, 3, 0.5)")]
        [TestCase("transparent", "rgba(0, 0, 0, 0)")]
        [TestCase("hsl(120, 100%, 25%)", "#008000")]
        public void ColorsAreSerialized(String color, String expected)
        {
            var ctx = Create();
            ctx.FillStyle = color;
            ctx.StrokeStyle = color;
            ctx.ShadowColor = color;

            Assert.AreEqual(expected, ctx.FillStyle);
            Assert.AreEqual(expected, ctx.StrokeStyle);
            Assert.AreEqual(expected, ctx.ShadowColor);
        }

        [Test]
        public void InvalidValuesAreIgnored()
        {
            var ctx = Create();
            ctx.FillStyle = "not a color";
            ctx.FillStyle = 42;
            ctx.GlobalAlpha = 2;
            ctx.GlobalAlpha = Double.NaN;
            ctx.GlobalCompositeOperation = "unknown";
            ctx.LineWidth = 0;
            ctx.LineWidth = -1;
            ctx.MiterLimit = Double.PositiveInfinity;
            ctx.ShadowBlur = -1;
            ctx.ShadowOffsetX = Double.NaN;
            ctx.SetLineDash(new[] { 1.0, -1.0 });
            ctx.FillRect(Double.NaN, 0, 1, 1);
            ctx.MoveTo(0, Double.PositiveInfinity);

            Assert.AreEqual("#000000", ctx.FillStyle);
            Assert.AreEqual(1.0, ctx.GlobalAlpha);
            Assert.AreEqual("source-over", ctx.GlobalCompositeOperation);
            Assert.AreEqual(1.0, ctx.LineWidth);
            Assert.AreEqual(10.0, ctx.MiterLimit);
            Assert.AreEqual(0.0, ctx.ShadowBlur);
            Assert.AreEqual(0.0, ctx.ShadowOffsetX);
            CollectionAssert.IsEmpty(ctx.GetLineDash());
            CollectionAssert.IsEmpty(ctx.Commands);
        }

        [Test]
        public void OperationsAreRecordedInOrder()
        {
            var ctx = Create();
            ctx.Save();
            ctx.Translate(10, 20);
            ctx.GlobalCompositeOperation = "multiply";
            ctx.LineCap = CanvasLineCap.Round;
            ctx.BeginPath();
            ctx.MoveTo(0, 0);
            ctx.LineTo(5, 5);
            ctx.Arc(5, 5, 2, 0, Math.PI, true);
            ctx.ClosePath();
            ctx.Fill(CanvasFillRule.EvenOdd);
            ctx.Stroke();
            ctx.Restore();

            CollectionAssert.AreEqual(new[]
            {
                "save()",
                "translate(10, 20)",
                "globalCompositeOperation(\"multiply\")",
                "lineCap(\"round\")",
                "beginPath()",
                "moveTo(0, 0)",
                "lineTo(5, 5)",
                "arc(5, 5, 2, 0, 3.141592653589793, true)",
                "closePath()",
                "fill(\"evenodd\")",
                "stroke()",
                "restore()",
            }, Commands(ctx));
        }

        [Test]
        public void SaveAndRestoreTheDrawingState()
        {
            var ctx = Create();
            ctx.FillStyle = "red";
            ctx.LineWidth = 4;
            ctx.Save();
            ctx.FillStyle = "blue";
            ctx.LineWidth = 8;
            ctx.Translate(50, 50);
            ctx.Restore();

            Assert.AreEqual("#ff0000", ctx.FillStyle);
            Assert.AreEqual(4.0, ctx.LineWidth);

            // the transform has been restored, too
            ctx.Rect(0, 0, 10, 10);
            Assert.IsTrue(ctx.IsPointInPath(5, 5));
        }

        [Test]
        public void RestoreWithoutSaveDoesNothing()
        {
            var ctx = Create();
            ctx.Restore();
            CollectionAssert.IsEmpty(ctx.Commands);
        }

        [Test]
        public void ResetClearsEverything()
        {
            var ctx = Create();
            ctx.FillStyle = "red";
            ctx.Save();
            ctx.Rect(0, 0, 10, 10);
            ctx.FillRect(0, 0, 10, 10);
            ctx.Reset();

            Assert.AreEqual("#000000", ctx.FillStyle);
            Assert.IsFalse(ctx.IsPointInPath(5, 5));
            CollectionAssert.IsEmpty(ctx.Commands);
        }

        [Test]
        public void LineDashWithOddCountIsRepeated()
        {
            var ctx = Create();
            ctx.SetLineDash(new[] { 1.0, 2.0, 3.0 });

            CollectionAssert.AreEqual(new[] { 1.0, 2.0, 3.0, 1.0, 2.0, 3.0 }, ctx.GetLineDash());
            Assert.AreEqual("setLineDash([1, 2, 3, 1, 2, 3])", Commands(ctx).Single());
        }

        [Test]
        public void PointInRectangle()
        {
            var ctx = Create();
            ctx.Rect(10, 10, 20, 20);

            Assert.IsTrue(ctx.IsPointInPath(15, 15));
            Assert.IsTrue(ctx.IsPointInPath(10, 10));
            Assert.IsFalse(ctx.IsPointInPath(5, 15));
            Assert.IsFalse(ctx.IsPointInPath(35, 35));
        }

        [Test]
        public void PointInCircle()
        {
            var ctx = Create();
            ctx.Arc(50, 50, 10, 0, 2 * Math.PI);

            Assert.IsTrue(ctx.IsPointInPath(50, 50));
            Assert.IsTrue(ctx.IsPointInPath(57, 57));
            Assert.IsFalse(ctx.IsPointInPath(58, 58));
        }

        [Test]
        public void PointInPathWithFillRule()
        {
            var ctx = Create();
            ctx.Rect(0, 0, 30, 30);
            ctx.Rect(10, 10, 10, 10);

            Assert.IsTrue(ctx.IsPointInPath(15, 15, CanvasFillRule.NonZero));
            Assert.IsFalse(ctx.IsPointInPath(15, 15, CanvasFillRule.EvenOdd));
            Assert.IsTrue(ctx.IsPointInPath(5, 5, CanvasFillRule.EvenOdd));
        }

        [Test]
        public void PointInPathUsesTheTransformAtTheTimeOfBuilding()
        {
            var ctx = Create();
            ctx.Translate(50, 0);
            ctx.Scale(2, 2);
            ctx.Rect(0, 0, 10, 10);
            ctx.ResetTransform();

            Assert.IsTrue(ctx.IsPointInPath(65, 15));
            Assert.IsFalse(ctx.IsPointInPath(5, 5));
        }

        [Test]
        public void PointInPath2DUsesTheCurrentTransform()
        {
            var ctx = Create();
            var path = new Path2D();
            path.Rect(0, 0, 10, 10);
            ctx.Translate(50, 50);

            Assert.IsTrue(ctx.IsPointInPath(path, 55, 55));
            Assert.IsFalse(ctx.IsPointInPath(path, 5, 5));
        }

        [Test]
        public void PointInRoundedRectangle()
        {
            var ctx = Create();
            ctx.RoundRect(0, 0, 40, 40, 10);

            Assert.IsTrue(ctx.IsPointInPath(20, 20));
            Assert.IsTrue(ctx.IsPointInPath(10, 1));
            Assert.IsFalse(ctx.IsPointInPath(1, 1));
            Assert.IsFalse(ctx.IsPointInPath(39, 39));
        }

        [Test]
        public void PointInRoundedRectangleWithNegativeDimensions()
        {
            var ctx = Create();
            ctx.RoundRect(40, 0, -40, 40, 0, 20, 0, 0);

            Assert.IsFalse(ctx.IsPointInPath(1, 1));
            Assert.IsTrue(ctx.IsPointInPath(19, 1));

            ctx.Reset();
            ctx.RoundRect(0, 40, 40, -40, 0, 0, 0, 20);

            Assert.IsFalse(ctx.IsPointInPath(1, 1));
            Assert.IsTrue(ctx.IsPointInPath(19, 1));
        }

        [Test]
        public void PointInRoundedCornerOfArcTo()
        {
            var ctx = Create();
            ctx.MoveTo(0, 0);
            ctx.ArcTo(40, 0, 40, 40, 20);
            ctx.LineTo(40, 40);
            ctx.LineTo(0, 40);

            Assert.IsTrue(ctx.IsPointInPath(20, 20));
            Assert.IsTrue(ctx.IsPointInPath(30, 5));
            Assert.IsFalse(ctx.IsPointInPath(39, 1));
        }

        [Test]
        public void PointInStroke()
        {
            var ctx = Create();
            ctx.MoveTo(0, 50);
            ctx.LineTo(100, 50);
            ctx.LineWidth = 10;

            Assert.IsTrue(ctx.IsPointInStroke(50, 54));
            Assert.IsFalse(ctx.IsPointInStroke(50, 56));
            Assert.IsFalse(ctx.IsPointInPath(50, 60));
        }

        [Test]
        public void PointInStrokeOfCurve()
        {
            var ctx = Create();
            ctx.MoveTo(0, 0);
            ctx.QuadraticCurveTo(50, 100, 100, 0);
            ctx.BezierCurveTo(100, 50, 50, 50, 0, 0);

            Assert.IsTrue(ctx.IsPointInStroke(50, 50));
            Assert.IsFalse(ctx.IsPointInStroke(50, 80));
        }

        [Test]
        public void NegativeRadiusIsAnIndexSizeError()
        {
            var ctx = Create();

            Assert.AreEqual((Int32)DomError.IndexSizeError, Assert.Throws<DomException>(() => ctx.Arc(0, 0, -1, 0, 1)).Code);
            Assert.AreEqual((Int32)DomError.IndexSizeError, Assert.Throws<DomException>(() => ctx.ArcTo(0, 0, 1, 1, -1)).Code);
            Assert.AreEqual((Int32)DomError.IndexSizeError, Assert.Throws<DomException>(() => ctx.Ellipse(0, 0, 1, -1, 0, 0, 1)).Code);
            Assert.AreEqual((Int32)DomError.IndexSizeError, Assert.Throws<DomException>(() => ctx.CreateRadialGradient(0, 0, -1, 0, 0, 1)).Code);
        }

        [Test]
        public void RoundRectRadiiAreValidated()
        {
            var ctx = Create();

            Assert.Throws<ArgumentOutOfRangeException>(() => ctx.RoundRect(0, 0, 10, 10, 1, 2, 3, 4, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => ctx.RoundRect(0, 0, 10, 10, Array.Empty<Double>()));
            Assert.Throws<ArgumentOutOfRangeException>(() => ctx.RoundRect(0, 0, 10, 10, -1));
            ctx.RoundRect(0, 0, 10, 10, 1, 2);
            Assert.AreEqual("roundRect(0, 0, 10, 10, [1, 2])", Commands(ctx).Single());
        }

        [Test]
        public void GradientAsFillStyle()
        {
            var ctx = Create();
            var gradient = ctx.CreateLinearGradient(0, 0, 100, 0);
            gradient.AddColorStop(0, "red");
            gradient.AddColorStop(1, "rgba(0, 0, 255, 0.5)");
            ctx.FillStyle = gradient;

            Assert.AreSame(gradient, ctx.FillStyle);
            Assert.AreEqual(CanvasGradientKind.Linear, gradient.Kind);
            CollectionAssert.AreEqual(new[] { 0.0, 0.0, 100.0, 0.0 }, gradient.Parameters);
            CollectionAssert.AreEqual(new[] { "#ff0000", "rgba(0, 0, 255, 0.5)" }, gradient.ColorStops.Select(m => m.Color));
            Assert.AreSame(gradient, ctx.Commands.Single().Arguments[0]);
        }

        [Test]
        public void GradientColorStopsAreValidated()
        {
            var gradient = Create().CreateConicGradient(0, 50, 50);

            Assert.AreEqual((Int32)DomError.IndexSizeError, Assert.Throws<DomException>(() => gradient.AddColorStop(1.5, "red")).Code);
            Assert.AreEqual((Int32)DomError.Syntax, Assert.Throws<DomException>(() => gradient.AddColorStop(0.5, "nope")).Code);
            CollectionAssert.IsEmpty(gradient.ColorStops);
        }

        [Test]
        public void PatternOfImage()
        {
            var ctx = Create();
            var image = (CssImageValue)CssStyleValue.Parse("background-image", "url(a.png)");

            Assert.AreEqual("repeat", ctx.CreatePattern(image, null).Repetition);
            Assert.AreEqual("repeat", ctx.CreatePattern(image, "").Repetition);
            Assert.AreEqual("no-repeat", ctx.CreatePattern(image, "no-repeat").Repetition);
            Assert.AreEqual((Int32)DomError.Syntax, Assert.Throws<DomException>(() => ctx.CreatePattern(image, "space")).Code);
        }

        [Test]
        public void DrawImageIsRecorded()
        {
            var ctx = Create();
            var image = (CssImageValue)CssStyleValue.Parse("background-image", "url(a.png)");
            ctx.DrawImage(image, 1, 2);
            ctx.DrawImage(image, 1, 2, 3, 4);
            ctx.DrawImage(image, 1, 2, 3, 4, 5, 6, 7, 8);

            Assert.AreEqual(3, ctx.Commands.Count);
            Assert.AreSame(image, ctx.Commands[0].Arguments[0]);
            Assert.AreEqual(9, ctx.Commands[2].Arguments.Count);
        }

        [Test]
        public void DrawnPathIsASnapshot()
        {
            var ctx = Create();
            var path = new Path2D();
            path.MoveTo(0, 0);
            ctx.Fill(path);
            path.LineTo(10, 10);

            var drawn = (Path2D)ctx.Commands.Single().Arguments[0];
            Assert.AreEqual("Path2D(moveTo(0, 0))", drawn.ToString());
            Assert.AreEqual(2, path.Commands.Count);
        }

        [Test]
        public void Path2DCanBeCopiedAndCombined()
        {
            var a = new Path2D();
            a.Rect(0, 0, 10, 10);
            var b = new Path2D(a);
            b.Rect(20, 20, 10, 10);
            var c = new Path2D("M 0 0 L 10 10");
            c.AddPath(b);
            var ctx = Create();

            Assert.AreEqual(1, a.Commands.Count);
            Assert.AreEqual(2, b.Commands.Count);
            Assert.AreEqual("d(\"M 0 0 L 10 10\")", c.Commands[0].ToString());
            Assert.IsTrue(ctx.IsPointInPath(c, 25, 25));
            Assert.IsFalse(ctx.IsPointInPath(a, 25, 25));
        }
    }
}
