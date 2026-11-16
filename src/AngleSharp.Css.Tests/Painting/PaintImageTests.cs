#nullable disable
namespace AngleSharp.Css.Tests.Painting
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Painting;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static CssConstructionFunctions;

    /// <summary>
    /// Drawing the images of paint() functions with registered paint definitions
    /// (CSS Painting API 6).
    /// </summary>
    [TestFixture]
    public class PaintImageTests
    {
        private static readonly PaintSize Size = new(100, 50);

        private static RecordingPaintRenderingContext2D Draw(PaintImage image) =>
            (RecordingPaintRenderingContext2D)image.Context;

        [Test]
        public void RegisteredPainterDrawsTheImage()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(box)\"></div>");
            document.GetPaintWorklet().RegisterPaint("box", new PaintDefinition((ctx, size, props, args) =>
            {
                ctx.FillStyle = "red";
                ctx.FillRect(0, 0, size.Width, size.Height);
            }));

            var image = document.GetElementById("target").DrawPaintImages("background-image", Size).Single();

            Assert.IsNotNull(image);
            Assert.AreEqual("box", image.Name);
            Assert.AreSame(Size, image.Size);
            Assert.AreEqual(1.0, image.DevicePixelRatio);
            CollectionAssert.AreEqual(new[] { "fillStyle(\"#ff0000\")", "fillRect(0, 0, 100, 50)" }, Draw(image).Commands.Select(m => m.ToString()));
        }

        [Test]
        public void MissingPainterGivesAnInvalidImage()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(missing), paint(box)\"></div>");
            document.GetPaintWorklet().RegisterPaint("box", new PaintDefinition((ctx, size, props, args) => { }));

            var images = document.GetElementById("target").DrawPaintImages("background-image", Size);

            Assert.AreEqual(2, images.Count);
            Assert.IsNull(images[0]);
            Assert.IsNotNull(images[1]);
        }

        [Test]
        public void NoPaintWorkletGivesAnInvalidImage()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(box)\"></div>");
            var images = document.GetElementById("target").DrawPaintImages("background-image", Size);

            Assert.AreEqual(1, images.Count);
            Assert.IsNull(images[0]);
        }

        [Test]
        public void PropertyWithoutPaintFunctionGivesNoImages()
        {
            var document = ParseDocument("<div id=target style=\"background-image: url(a.png)\"></div>");
            document.GetPaintWorklet().RegisterPaint("box", new PaintDefinition((ctx, size, props, args) => { }));

            CollectionAssert.IsEmpty(document.GetElementById("target").DrawPaintImages("background-image", Size));
            CollectionAssert.IsEmpty(document.GetElementById("target").DrawPaintImages("border-image-source", Size));
        }

        [Test]
        public void PainterRegisteredLaterIsUsedForLaterDrawing()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(late)\"></div>");
            var element = document.GetElementById("target");

            Assert.IsNull(element.DrawPaintImages("background-image", Size)[0]);
            document.GetPaintWorklet().RegisterPaint("late", new PaintDefinition((ctx, size, props, args) => { }));
            Assert.IsNotNull(element.DrawPaintImages("background-image", Size)[0]);
        }

        [Test]
        public void ThrowingPainterGivesAnInvalidImage()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(broken)\"></div>");
            document.GetPaintWorklet().RegisterPaint("broken", new PaintDefinition((ctx, size, props, args) => ctx.Arc(0, 0, -1, 0, 1)));

            Assert.IsNull(document.GetElementById("target").DrawPaintImages("background-image", Size)[0]);
        }

        [Test]
        public void PainterReadsItsInputProperties()
        {
            var document = ParseDocument("<style>div { color: blue; --gap: 4px; border-top-width: 3px; border-top-style: solid }</style><div id=target style=\"background-image: paint(x)\"></div>");
            var seen = new Dictionary<String, String>();
            var size = 0;
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) =>
            {
                size = props.Size;
                seen["color"] = props.Get("color")?.ToString();
                seen["--gap"] = props.Get("--gap")?.ToString();
                seen["border-top-width"] = props.Get("border-top-width")?.ToString();
                seen["opacity"] = props.Get("opacity")?.ToString();
                seen["has-opacity"] = props.Has("opacity").ToString();
            })
            {
                InputProperties = new[] { "color", "--gap", "border-top-width" },
            });

            Assert.IsNotNull(document.GetElementById("target").DrawPaintImages("background-image", Size)[0]);
            Assert.AreEqual("rgba(0, 0, 255, 1)", seen["color"]);
            Assert.AreEqual("4px", seen["--gap"]);
            Assert.AreEqual("3px", seen["border-top-width"]);
            Assert.IsNull(seen["opacity"]);
            Assert.AreEqual("False", seen["has-opacity"]);
            Assert.AreEqual(3, size);
        }

        [Test]
        public void InputPropertiesHaveTheirInitialOrInheritedValues()
        {
            var document = ParseDocument("<section style=\"color: green\"><div id=target style=\"background-image: paint(x)\"></div></section>");
            var seen = new Dictionary<String, String>();
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) =>
            {
                seen["color"] = props.Get("color")?.ToString();
                seen["opacity"] = props.Get("opacity")?.ToString();
            })
            {
                InputProperties = new[] { "color", "opacity" },
            });

            document.GetElementById("target").DrawPaintImages("background-image", Size);

            Assert.AreEqual("rgba(0, 128, 0, 1)", seen["color"]);
            Assert.AreEqual("1", seen["opacity"]);
        }

        [Test]
        public void RegisteredCustomInputPropertyIsTyped()
        {
            var document = ParseDocument("<div id=target style=\"--radius: 2em; font-size: 10px; background-image: paint(x)\"></div>");
            document.DefaultView.RegisterProperty(new PropertyDefinition { Name = "--radius", Syntax = "<length>", Inherits = false, InitialValue = "0px" });
            var radius = default(CssStyleValue);
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => radius = props.Get("--radius"))
            {
                InputProperties = new[] { "--radius" },
            });

            document.GetElementById("target").DrawPaintImages("background-image", Size);

            Assert.IsInstanceOf<CssUnitValue>(radius);
            Assert.AreEqual(20.0, ((CssUnitValue)radius).Value);
            Assert.AreEqual("px", ((CssUnitValue)radius).Unit);
        }

        [Test]
        public void ArgumentsAreTypedByTheirSyntax()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(x, 10px, red, a b c)\"></div>");
            var arguments = default(IReadOnlyList<CssStyleValue>);
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => arguments = args)
            {
                InputArguments = new[] { "<length>", "<color>", "*" },
            });

            Assert.IsNotNull(document.GetElementById("target").DrawPaintImages("background-image", Size)[0]);
            Assert.AreEqual(3, arguments.Count);
            Assert.IsInstanceOf<CssUnitValue>(arguments[0]);
            Assert.AreEqual("10px", arguments[0].ToString());
            Assert.AreEqual("rgba(255, 0, 0, 1)", arguments[1].ToString());
            Assert.IsInstanceOf<CssUnparsedValue>(arguments[2]);
            Assert.AreEqual("a b c", arguments[2].ToString());
        }

        [TestCase("paint(x, red)")]
        [TestCase("paint(x, 10px, 20px)")]
        [TestCase("paint(x)")]
        public void ArgumentsNotMatchingTheirSyntaxGiveAnInvalidImage(String value)
        {
            var document = ParseDocument($"<div id=target style=\"background-image: {value}\"></div>");
            var called = false;
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => called = true)
            {
                InputArguments = new[] { "<length>" },
            });

            Assert.IsNull(document.GetElementById("target").DrawPaintImages("background-image", Size)[0]);
            Assert.IsFalse(called);
        }

        [Test]
        public void ArgumentsWithoutInputArgumentsAreUnparsed()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(x, 10px, foo(1, 2))\"></div>");
            var arguments = default(IReadOnlyList<CssStyleValue>);
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => arguments = args));

            document.GetElementById("target").DrawPaintImages("background-image", Size);

            Assert.AreEqual(2, arguments.Count);
            Assert.IsTrue(arguments.All(m => m is CssUnparsedValue));
            Assert.AreEqual("10px", arguments[0].ToString());
            Assert.AreEqual("foo(1, 2)", arguments[1].ToString());
        }

        [Test]
        public void ArgumentsWithVariablesAreSubstituted()
        {
            var document = ParseDocument("<div id=target style=\"--w: 6px; background-image: paint(x, var(--w))\"></div>");
            var arguments = default(IReadOnlyList<CssStyleValue>);
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => arguments = args)
            {
                InputArguments = new[] { "<length>" },
            });

            document.GetElementById("target").DrawPaintImages("background-image", Size);

            Assert.AreEqual("6px", arguments[0].ToString());
        }

        [Test]
        public void ContextOptionsArePassedToTheContext()
        {
            var document = ParseDocument("<div id=target style=\"background-image: paint(x)\"></div>");
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => { })
            {
                ContextOptions = new PaintRenderingContext2DSettings { Alpha = false },
            });

            var image = document.GetElementById("target").DrawPaintImages("background-image", Size)[0];

            Assert.IsFalse(Draw(image).Settings.Alpha);
        }

        [Test]
        public void CustomContextFactoryIsUsed()
        {
            var factory = new FakeContextFactory();
            var config = Configuration.Default.WithCss().With(factory).WithRenderDevice(new DefaultRenderDevice { Resolution = 288 });
            var document = BrowsingContext.New(config).OpenAsync(m => m.Content("<div id=target style=\"background-image: paint(x)\"></div>")).Result;
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => ctx.FillRect(1, 2, 3, 4)));

            var image = document.GetElementById("target").DrawPaintImages("background-image", Size)[0];

            Assert.AreSame(factory.Created, image.Context);
            Assert.AreEqual(3.0, image.DevicePixelRatio);
            Assert.AreEqual(3.0, factory.Created.DevicePixelRatio);
            Assert.AreEqual("fillRect(1, 2, 3, 4)", factory.Created.Commands.Single().ToString());
        }

        [Test]
        public void PaintImageOfRenderTreeNode()
        {
            var document = ParseDocument("<style>p { background-image: paint(x); color: red }</style><p>Hello</p>");
            var color = default(String);
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => color = props.Get("color").ToString())
            {
                InputProperties = new[] { "color" },
            });
            var tree = document.DefaultView.Render();
            var node = Find(tree, "p");

            var image = node.DrawPaintImages("background-image", Size).Single();

            Assert.IsNotNull(image);
            Assert.AreEqual("rgba(255, 0, 0, 1)", color);
        }

        [Test]
        public void DrawingAPaintValueDirectly()
        {
            var document = ParseDocument("<div id=target></div>");
            document.GetPaintWorklet().RegisterPaint("x", new PaintDefinition((ctx, s, props, args) => ctx.StrokeRect(0, 0, s.Width, s.Height)));
            var paint = ParseDeclaration("border-image-source: paint(x)").RawValue.GetPaintValues().Single();

            var image = document.GetElementById("target").DrawPaintImage(paint, new PaintSize(8, 8));

            Assert.AreSame(paint, image.Source);
            Assert.AreEqual("strokeRect(0, 0, 8, 8)", Draw(image).Commands.Single().ToString());
        }

        [Test]
        public void PaintSizeMustBeNonNegativeAndFinite()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PaintSize(-1, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PaintSize(1, Double.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PaintSize(Double.PositiveInfinity, 1));
        }

        private static RenderTree.ElementRenderNode Find(RenderTree.IRenderNode node, String tag)
        {
            if (node is RenderTree.ElementRenderNode element && element.Ref.LocalName == tag)
            {
                return element;
            }

            return node.Children.Select(child => Find(child, tag)).FirstOrDefault(m => m is not null);
        }

        private sealed class FakeContextFactory : IPaintRenderingContextFactory
        {
            public RecordingPaintRenderingContext2D Created { get; private set; }

            public IPaintRenderingContext2D Create(PaintSize size, Double devicePixelRatio, PaintRenderingContext2DSettings settings) =>
                Created = new RecordingPaintRenderingContext2D(size, devicePixelRatio, settings);
        }
    }
}
