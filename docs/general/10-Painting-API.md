---
title: "Painting API"
section: "AngleSharp.Css"
---
# Painting API

AngleSharp.Css implements the [CSS Painting API Level 1](https://www.w3.org/TR/css-paint-api-1/) in the `AngleSharp.Css.Painting` namespace. A *paint definition*, registered under a name, draws the images of the `paint()` functions that use this name, e.g., a background:

```css
.element {
    --stripe-color: tomato;
    background-image: paint(stripes, 8px);
}
```

The API is split into three parts, none of which needs a scripting engine:

1. **CSS** - `paint()` is parsed, kept in the value model, computed, and serialized, whether a paint definition exists or not.
2. **Registry and drawing** - the paint worklet of a document holds the paint definitions and draws the images for elements.
3. **Extension points** - interfaces that a scripting engine (e.g., AngleSharp.Js) or an application implements to run paint worklet modules or to draw into a real bitmap.

## The `paint()` Function

`paint( <ident>, <declaration-value>? )` is an `<image>`, so it is accepted wherever images are: `background-image`, `background`, `border-image-source`, `border-image`, `mask-image`, `list-style-image`, `image-set()`, and custom properties registered with the syntax `<image>`.

```cs
var property = style.GetProperty("background-image");
var paint = property.RawValue.GetPaintValues().Single(); // CssPaintValue

paint.PainterName;  // "stripes"
paint.Arguments;    // [8px], each one as written (split at the top-level commas)
paint.CssText;      // "paint(stripes, 8px)"
```

- The arguments are kept as written, since only the paint definition knows their syntax (its `InputArguments`). `var()` in the arguments is substituted like anywhere else.
- The computed value is as specified.
- In the Typed OM, `paint()` is a `CssImageValue`.
- `GetPaintValues()` finds the `paint()` functions in any value, e.g., in the layers of a `background-image` or the options of an `image-set()`.

Parsing never depends on a paint definition: a `paint()` with an unknown name is a valid value, it is just drawn as an invalid image (i.e., transparent).

## Registering Paint Definitions

The paint worklet of a document (`CSS.paintWorklet`) is `window.GetPaintWorklet()` or `document.GetPaintWorklet()`. A paint definition implements `IPaintDefinition`, or uses `PaintDefinition` with a callback:

```cs
using AngleSharp.Css.Dom;
using AngleSharp.Css.Painting;
using AngleSharp.Css.TypedOm;

var worklet = document.DefaultView.GetPaintWorklet();

worklet.RegisterPaint("stripes", new PaintDefinition((ctx, size, properties, arguments) =>
{
    var width = ((CssUnitValue)arguments[0]).Value;
    ctx.FillStyle = properties.Get("--stripe-color")?.ToString() ?? "black";

    for (var x = 0.0; x < size.Width; x += 2 * width)
    {
        ctx.FillRect(x, 0, width, size.Height);
    }
})
{
    InputProperties = new[] { "--stripe-color" },
    InputArguments = new[] { "<length>" },
    ContextOptions = new PaintRenderingContext2DSettings { Alpha = true },
});
```

| Member | Meaning |
|--------|---------|
| `InputProperties` | The properties of the element given to `Paint` (standard or custom). Unknown properties are ignored. Read once at registration. |
| `InputArguments` | The syntax of each argument (as for `@property`, e.g., `<length>`, `<color>+`, `*`). Without any, the arguments are given as `CssUnparsedValue`. Read once at registration. |
| `ContextOptions` | `Alpha = false` asks for an opaque image. |
| `Paint(ctx, size, properties, arguments)` | Draws the image. `properties` is a `IStylePropertyMapReadOnly` of the computed values of the input properties (typed according to their registration, e.g., a `--radius` registered as `<length>` is a `CssUnitValue` in pixels); `arguments` are typed by `InputArguments`. |

`RegisterPaint` follows the specification: an empty name or an invalid syntax of an input argument is a `TypeError` (`ArgumentException`), registering a name twice is an `InvalidModificationError` (`DomException`). The same method is on `worklet.GlobalScope`, which is what a module sees as `registerPaint()`. `worklet.Get(name)`, `Contains(name)`, `Names`, and `GetInputProperties(name)` tell what has been registered.

Each document has its own paint worklet, which only lives as long as the document.

## Drawing Paint Images

AngleSharp.Css does no layout, so whoever draws the image (e.g., a renderer that knows the box of the element) gives its size in CSS pixels:

```cs
var element = document.QuerySelector(".element");

// all paint() functions in the computed value of the property
IReadOnlyList<PaintImage?> images = element.DrawPaintImages("background-image", new PaintSize(200, 100));

// a single paint() function, optionally with a known computed style
PaintImage? image = element.DrawPaintImage(paint, new PaintSize(200, 100));

// or for a node of the render tree, using its computed style
var images2 = renderNode.DrawPaintImages("background-image", new PaintSize(200, 100));
```

A `PaintImage` gives the `Source` (`CssPaintValue`), `Name`, `Size`, `DevicePixelRatio`, and the `Context` that was drawn with, which holds the output.

The image is invalid, i.e., `null`, if:

- no paint definition has been registered under its name (a definition registered later is used by later drawing),
- the arguments do not match `InputArguments` (a different count or a value not matching its syntax),
- `Paint` throws.

An invalid image is meant to be drawn as transparent black. None of these cases affects parsing or computing styles.

## Rendering Contexts

`Paint` draws with an `IPaintRenderingContext2D`, the subset of the canvas 2D context the specification allows: state (`Save`, `Restore`, `Reset`), transforms, compositing, image smoothing, fill and stroke styles (colors, `CanvasGradient`, `CanvasPattern`), shadows, rectangles, paths (`ICanvasPath`, `Path2D`), clipping, hit testing (`IsPointInPath`, `IsPointInStroke`), `DrawImage` (of `CssImageValue`s), and line styles. There is no text and no access to pixels.

### The Default: Recording

Without further configuration, the context is a `RecordingPaintRenderingContext2D`, which draws nothing but records the operations as `PaintCommand`s, a display list any renderer can replay:

```cs
var recording = (RecordingPaintRenderingContext2D)image.Context;

foreach (var command in recording.Commands)
{
    // e.g., fillStyle("#ff6347"), fillRect(0, 0, 8, 100), ...
    Console.WriteLine(command);
    // command.Name: "fillRect", command.Arguments: [0.0, 0.0, 8.0, 100.0]
}
```

- Commands are named like the canvas operations (a setter is recorded under the name of its property, e.g., `fillStyle`), with validated and normalized arguments: colors are serialized (`#rrggbb` or `rgba(r, g, b, a)`), and calls the canvas API ignores (e.g., non-finite coordinates, a negative `lineWidth`, an unknown composite operation) are not recorded.
- The drawing state works as in a canvas, so reading `FillStyle` gives what has been set, `Save` / `Restore` apply, and `Reset` clears the recording.
- Paths drawn with `Fill(path)`, `Stroke(path)`, or `Clip(path)` are recorded as snapshots, and gradients are recorded by reference.
- Hit testing works on the flattened geometry of the path (curves and arcs are approximated). `IsPointInStroke` considers the line width but not joins, caps, or dashes. The SVG path data of `new Path2D("M 0 0 ...")` is kept as a command named `d` and not used for hit testing.
- Coordinates are CSS pixels, i.e., the `DevicePixelRatio` is left to the renderer replaying the commands.

### Custom Rendering Contexts

To draw into a real bitmap (e.g., with SkiaSharp or System.Drawing), register an `IPaintRenderingContextFactory`:

```cs
public sealed class SkiaPaintContextFactory : IPaintRenderingContextFactory
{
    public IPaintRenderingContext2D Create(PaintSize size, Double devicePixelRatio, PaintRenderingContext2DSettings settings) =>
        new SkiaPaintRenderingContext(size, devicePixelRatio, settings.Alpha);
}

var config = Configuration.Default
    .WithCss()
    .With(new SkiaPaintContextFactory());
```

A new context is created for each image, and `PaintImage.Context` is that context, so the bitmap is obtained by casting it back. The device pixel ratio is the `Resolution` of the `IRenderDevice` divided by 96 (or 1 without a render device), as is `devicePixelRatio` of the global scope.

## Integrating a Scripting Engine

In a browser, paint definitions are classes of a script module loaded with `CSS.paintWorklet.addModule(url)`. AngleSharp.Css does not depend on any scripting engine; instead, `AddModule` hands the module to the `IPaintWorkletModuleLoader` of the configuration:

```cs
public interface IPaintWorkletModuleLoader
{
    Task LoadAsync(PaintWorkletGlobalScope scope, Url moduleUrl, CancellationToken cancellationToken);
}
```

Without a loader, `AddModule` fails with a `NotSupportedError` (`DomException`), while paint definitions can still be registered directly. A loader, e.g., one provided by AngleSharp.Js, would:

1. Fetch the module (`moduleUrl` is already resolved against the base URL of the document; `scope.Document.Context` gives the browsing context to load it with).
2. Run it in a new script global that exposes `registerPaint(name, class)` and `devicePixelRatio` of the `PaintWorkletGlobalScope` (both carry `[DomName]` attributes for that purpose).
3. For each `registerPaint(name, class)` call, read the static `inputProperties`, `inputArguments`, and `contextOptions` of the class, and register an `IPaintDefinition` adapter with `scope.RegisterPaint(name, adapter)`. Its `Paint` constructs (or reuses) an instance of the class and calls its `paint(ctx, size, properties, args)` method, passing the .NET objects, which are annotated with `[DomName]` (`PaintRenderingContext2D`, `PaintSize`, `StylePropertyMapReadOnly`, `CSSStyleValue`, `Path2D`, `CanvasGradient`, ...).
4. Complete the task once the module has run, or fail it, e.g., for a syntax error of the module.

```cs
var config = Configuration.Default
    .WithCss()
    .With(new MyScriptPaintWorkletLoader());

await document.DefaultView.GetPaintWorklet().AddModule("painters/stripes.js");
```

Exceptions thrown by a paint definition while drawing make the image invalid, which is how errors of script painters are expected to surface, too.

## Mapping to the Specification

| Specification | AngleSharp.Css |
|---------------|----------------|
| `paint()` | `CssPaintValue` (`PainterName`, `Arguments`) |
| `CSS.paintWorklet` | `window.GetPaintWorklet()` / `document.GetPaintWorklet()` (`PaintWorklet`) |
| `paintWorklet.addModule(url)` | `PaintWorklet.AddModule(url)`, run by `IPaintWorkletModuleLoader` |
| `PaintWorkletGlobalScope` | `PaintWorkletGlobalScope` (`RegisterPaint`, `DevicePixelRatio`) |
| paint definition (class) | `IPaintDefinition` / `PaintDefinition` |
| `PaintSize` | `PaintSize` |
| `PaintRenderingContext2D` | `IPaintRenderingContext2D`, by default `RecordingPaintRenderingContext2D` |
| `PaintRenderingContext2DSettings` | `PaintRenderingContext2DSettings` |
| draw a paint image | `DrawPaintImage` / `DrawPaintImages` (`PaintImage`, `null` for an invalid image) |

Not supported: invalidating and repainting (the caller draws when it needs an image), several global scopes per worklet, and `getTransform()` / `DOMMatrix` arguments of the canvas API.
