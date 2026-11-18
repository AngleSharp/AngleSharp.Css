---
title: "Provided Services"
section: "AngleSharp.Css"
---
# Provided Services

Calling `WithCss()` registers a set of CSS-related services in the AngleSharp configuration.

## What `WithCss()` Registers

From the default configuration extension:

- `ICssDefaultStyleSheetProvider` -> default stylesheet provider,
- `IFeatureValidatorFactory` -> media feature validators,
- `IDocumentFunctionFactory` -> document function support,
- `IPseudoElementFactory` -> pseudo-element construction,
- `IDeclarationFactory` -> declaration metadata + value conversion,
- `ICssParser` -> parser instance using optional `CssParserOptions`,
- `IStylingService` -> `CssStylingService` for CSS MIME handling,
- CSS observer service (`Factory.Observer`) for style mutation integration.
- `IPseudoClassSelectorFactory` -> wrapped so any recognized pseudo-class can be forced per element via `SetPseudoClass(...)` (see [Core Interfaces](04-Core-Interfaces.md)).

## Quick Service Retrieval

```cs
using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Css.Parser;

var context = BrowsingContext.New(Configuration.Default.WithCss());

var parser = context.GetService<ICssParser>();
var defaultSheets = context.GetServices<ICssDefaultStyleSheetProvider>();
var declarationFactory = context.GetService<IDeclarationFactory>();
```

## Styling Service Role

`CssStylingService` is the `IStylingService` implementation that:

- recognizes CSS MIME types,
- creates `ICssStyleSheet` objects for responses,
- delegates parsing to `ICssParser`.

This is the bridge that enables stylesheet loading from DOM resources.

## Render-Related Service

`WithRenderDevice(...)` registers `IRenderDevice`, which is consumed by:

- `GetComputedStyle(...)`,
- render-tree generation (`window.Render(...)`),
- media query feature validation.

## Optional Painting Services

`WithCss()` registers nothing for the [Painting API](10-Painting-API.md); the following services can be added to the configuration with `With(...)`:

- `IPaintWorkletModuleLoader` runs the modules given to `CSS.paintWorklet.addModule()`, e.g., with a scripting engine. Without it, `AddModule` fails with a `NotSupportedError`.
- `IPaintRenderingContextFactory` creates the rendering contexts paint definitions draw with, e.g., into a bitmap. Without it, the operations are recorded by a `RecordingPaintRenderingContext2D`.

## Practical Checks

```cs
var context = BrowsingContext.New(
    Configuration.Default
        .WithCss()
        .WithRenderDevice());

var hasParser = context.GetService<ICssParser>() is not null;
var hasDevice = context.GetService<IRenderDevice>() is not null;

Console.WriteLine($"Parser: {hasParser}, RenderDevice: {hasDevice}");
```
