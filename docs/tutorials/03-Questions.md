---
title: "Questions"
section: "AngleSharp.Css"
---
# Frequently Asked Questions

## How to change the color output?

By default, AngleSharp.Css uses `rgba()` for the serialization of `Color`. To change this you can set

```cs
Color.UseHex = true;
```

which will automatically use hex for all non-transparent colors. All other colors would still be represented via the `rgba()` function.

So you'd get:

```cs
Color.UseHex = true;
var color1 = new Color(65, 12, 48);
// color1.CssText = #410C30
var color2 = new Color(65, 12, 48, 10);
// color2.CssText = rgba(65, 12, 48, 0.04)
```

## Why is my linked stylesheet not loaded?

Most commonly, resource loading is not enabled. For external stylesheets, configure a requester and enable resource loading.

```cs
using AngleSharp;
using AngleSharp.Io;

var config = Configuration.Default
	.WithPageRequester(enableResourceLoading: true)
	.WithCss();

var context = BrowsingContext.New(config);
var document = await context.OpenAsync("https://example.org");
```

## Why do I only see inline values, not final values?

`GetStyle()` returns inline declarations. To inspect the resolved result after cascade and inheritance, use computed style.

```cs
var inline = element.GetStyle().GetPropertyValue("color");
var computed = element.ComputeStyle().GetPropertyValue("color");
```

## How can I preserve unknown CSS for diagnostics?

Enable parser options to keep unknown rules and declarations.

```cs
using AngleSharp.Css.Parser;

var options = new CssParserOptions
{
	IsIncludingUnknownDeclarations = true,
	IsIncludingUnknownRules = true,
};

var config = Configuration.Default.WithCss(options);
```

## How can I parse CSS asynchronously?

Use `ParseStyleSheetAsync` with either text or stream input.

```cs
var parser = context.GetService<ICssParser>();
var sheetA = await parser.ParseStyleSheetAsync("h1 { color: red; }");
var sheetB = await parser.ParseStyleSheetAsync(stream);
```
