#nullable disable
namespace AngleSharp.Css.Tests.Painting
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Painting;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using static CssConstructionFunctions;

    /// <summary>
    /// The registry of paint definitions (CSS Painting API 3 and 4) and the extension
    /// point for scripting engines (addModule).
    /// </summary>
    [TestFixture]
    public class PaintWorkletTests
    {
        private static PaintDefinition Noop() => new((ctx, size, props, args) => { });

        [Test]
        public void WindowAndDocumentShareThePaintWorklet()
        {
            var document = ParseDocument("<div></div>");
            var worklet = document.DefaultView.GetPaintWorklet();

            Assert.AreSame(worklet, document.GetPaintWorklet());
            Assert.AreSame(document, worklet.Document);
            Assert.AreSame(worklet, worklet.GlobalScope.Worklet);
        }

        [Test]
        public void DocumentsHaveTheirOwnPaintWorklets()
        {
            var a = ParseDocument("<div></div>");
            var b = ParseDocument("<div></div>");
            a.GetPaintWorklet().RegisterPaint("x", Noop());

            Assert.IsTrue(a.GetPaintWorklet().Contains("x"));
            Assert.IsFalse(b.GetPaintWorklet().Contains("x"));
        }

        [Test]
        public void RegisteredDefinitionCanBeLookedUp()
        {
            var worklet = ParseDocument("").GetPaintWorklet();
            var definition = Noop();
            worklet.GlobalScope.RegisterPaint("checkerboard", definition);

            Assert.AreSame(definition, worklet.Get("checkerboard"));
            Assert.IsNull(worklet.Get("Checkerboard"));
            Assert.IsNull(worklet.Get("missing"));
            CollectionAssert.AreEqual(new[] { "checkerboard" }, worklet.Names);
        }

        [Test]
        public void RegisteringTheSameNameTwiceIsAnInvalidModificationError()
        {
            var worklet = ParseDocument("").GetPaintWorklet();
            worklet.RegisterPaint("x", Noop());

            var ex = Assert.Throws<DomException>(() => worklet.RegisterPaint("x", Noop()));
            Assert.AreEqual((Int32)DomError.InvalidModification, ex.Code);
        }

        [Test]
        public void EmptyNameIsATypeError()
        {
            var worklet = ParseDocument("").GetPaintWorklet();
            Assert.Throws<ArgumentException>(() => worklet.RegisterPaint("", Noop()));
        }

        [Test]
        public void InvalidSyntaxOfInputArgumentIsATypeError()
        {
            var worklet = ParseDocument("").GetPaintWorklet();
            var definition = new PaintDefinition((ctx, size, props, args) => { }) { InputArguments = new[] { "<length>", "<nope>" } };

            Assert.Throws<ArgumentException>(() => worklet.RegisterPaint("x", definition));
            Assert.IsFalse(worklet.Contains("x"));
        }

        [Test]
        public void UnknownInputPropertiesAreIgnored()
        {
            var worklet = ParseDocument("").GetPaintWorklet();
            var definition = new PaintDefinition((ctx, size, props, args) => { })
            {
                InputProperties = new[] { "Color", "--My-Prop", "not-a-property", "color", "border-top-width" },
            };
            worklet.RegisterPaint("x", definition);

            CollectionAssert.AreEqual(new[] { "color", "--My-Prop", "border-top-width" }, worklet.GetInputProperties("x"));
        }

        [Test]
        public void DevicePixelRatioFollowsTheRenderDevice()
        {
            var config = Configuration.Default.WithCss().WithRenderDevice(new DefaultRenderDevice { Resolution = 192 });
            var document = BrowsingContext.New(config).OpenNewAsync().Result;

            Assert.AreEqual(2.0, document.GetPaintWorklet().GlobalScope.DevicePixelRatio);
            Assert.AreEqual(1.0, ParseDocument("").GetPaintWorklet().DevicePixelRatio);
        }

        [Test]
        public void AddModuleWithoutLoaderIsNotSupported()
        {
            var worklet = ParseDocument("").GetPaintWorklet();
            var task = worklet.AddModule("painter.js");

            Assert.IsTrue(task.IsFaulted);
            var ex = task.Exception.InnerException as DomException;
            Assert.IsNotNull(ex);
            Assert.AreEqual((Int32)DomError.NotSupported, ex.Code);
        }

        [Test]
        public async Task AddModuleRunsTheModuleWithTheLoader()
        {
            var loader = new FakeModuleLoader();
            var config = Configuration.Default.WithCss().With(loader);
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<div></div>").Address("https://example.com/app/index.html"));
            var worklet = document.DefaultView.GetPaintWorklet();

            await worklet.AddModule("painters/rings.js");

            Assert.AreEqual("https://example.com/app/painters/rings.js", loader.Loaded[0].Href);
            Assert.IsTrue(worklet.Contains("rings"));
        }

        [Test]
        public void AddModuleFailsWhenTheLoaderFails()
        {
            var loader = new FakeModuleLoader { Fail = true };
            var config = Configuration.Default.WithCss().With(loader);
            var document = BrowsingContext.New(config).OpenNewAsync().Result;
            var task = document.GetPaintWorklet().AddModule("https://example.com/broken.js");

            Assert.ThrowsAsync<InvalidOperationException>(() => task);
        }

        private sealed class FakeModuleLoader : IPaintWorkletModuleLoader
        {
            public List<Url> Loaded { get; } = new();

            public Boolean Fail { get; set; }

            public Task LoadAsync(PaintWorkletGlobalScope scope, Url moduleUrl, CancellationToken cancellationToken)
            {
                if (Fail)
                {
                    return Task.FromException(new InvalidOperationException("The module failed."));
                }

                Loaded.Add(moduleUrl);
                // what a script engine does when the module calls registerPaint('rings', class { ... })
                scope.RegisterPaint("rings", new PaintDefinition((ctx, size, props, args) => ctx.FillRect(0, 0, size.Width, size.Height)));
                return Task.CompletedTask;
            }
        }
    }
}
