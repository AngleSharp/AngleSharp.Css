---
title: "Getting Started"
section: "AngleSharp.Css"
---
# Getting Started

## Requirements

You need to have AngleSharp installed already. This can be done via NuGet:

```ps1
Install-Package AngleSharp
```

## Getting AngleSharp.Css over NuGet

The simplest way of integrating AngleSharp.Css to your project is by using NuGet. You can install AngleSharp.Css by opening the package manager console (PM) and typing in the following statement:

```ps1
Install-Package AngleSharp.Css
```

You can also use the graphical library package manager ("Manage NuGet Packages for Solution"). Searching for "AngleSharp.Css" in the official NuGet online feed will find this library.

## Setting up AngleSharp.Css

To use AngleSharp.Css, add it to the `Configuration` coming from AngleSharp.

If you want a browser-like baseline setup, start with:

```cs
var config = Configuration.Default
    .WithCss(); // from AngleSharp.Css
```

This registers CSS services, including the parser and CSSOM integration.

You can also provide parser options directly:

```cs
using AngleSharp;
using AngleSharp.Css.Parser;

var options = new CssParserOptions
{
    IsIncludingUnknownDeclarations = true,
    IsIncludingUnknownRules = true,
    IsToleratingInvalidSelectors = true,
};

var config = Configuration.Default.WithCss(options);
```

## First Practical Workflows

### 1. Parse a stylesheet from a string

Use this for validation, analysis, or simple transforms.

```cs
using AngleSharp;
using AngleSharp.Css.Parser;

var context = BrowsingContext.New(Configuration.Default.WithCss());
var parser = context.GetService<ICssParser>();

var sheet = parser.ParseStyleSheet("h1 { color: red; } @media (max-width: 600px) { h1 { color: blue; } }");

Console.WriteLine(sheet.Rules.Length);
```

### 2. Parse asynchronously (string or stream)

Useful when CSS comes from files, uploads, or network sources.

```cs
using AngleSharp;
using AngleSharp.Css.Parser;
using System.Text;

var context = BrowsingContext.New(Configuration.Default.WithCss());
var parser = context.GetService<ICssParser>();

var css = "p { margin: 1rem; }";
var sheetFromString = await parser.ParseStyleSheetAsync(css);

await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(css));
var sheetFromStream = await parser.ParseStyleSheetAsync(stream);
```

### 3. Load HTML and access linked stylesheets

Use resource loading if you want `<link rel=\"stylesheet\">` and `@import` to be fetched.

```cs
using AngleSharp;
using AngleSharp.Io;

var config = Configuration.Default
    .WithPageRequester(enableResourceLoading: true)
    .WithCss();

var context = BrowsingContext.New(config);
var document = await context.OpenAsync("https://example.org");

Console.WriteLine($"Style sheets discovered: {document.StyleSheets.Length}");
```

### 4. Use DOM inline styles

Use this for automation scenarios where you patch styles directly.

```cs
using AngleSharp;
using AngleSharp.Css.Dom;
using AngleSharp.Dom;

var context = BrowsingContext.New(Configuration.Default.WithCss());
var document = await context.OpenAsync(req => req.Content("<div id='box'></div>"));

var box = document.QuerySelector("#box");
var style = box.GetStyle();

style.SetProperty("display", "grid");
style.SetProperty("gap", "12px");

Console.WriteLine(box.GetAttribute("style"));
```

## Custom Registration (Advanced)

If needed, all parts can be registered manually. The core pieces are:

- A CSS parser (implementing the `ICssParser` interface, e.g., `CssParser`)
- A factory for creating CSS declarations (`IDeclarationFactory`)
- The styling service that can handle CSS documents, see `CssStylingService`

For some features (for example media-query dependent behavior), add a render device:

```cs
var config = Configuration.Default
    .WithCss()
    .WithRenderDevice(new DefaultRenderDevice
    {
        DeviceHeight = 768,
        DeviceWidth = 1024,
    });
```

If no specific `IRenderDevice` is supplied, a default implementation is used.
