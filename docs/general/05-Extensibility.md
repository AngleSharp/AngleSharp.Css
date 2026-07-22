---
title: "Extensibility"
section: "AngleSharp.Css"
---
# Extensibility

AngleSharp.Css is designed to be composed through services in the AngleSharp configuration.

## Main Extension Points

- `ICssDefaultStyleSheetProvider`
: Override or append default UA-like styles.
- `IDeclarationFactory`
: Customize declaration metadata and conversion.
- `IFeatureValidatorFactory`
: Add or replace media-feature validation behavior.
- `IDocumentFunctionFactory`
: Extend document-level CSS functions.
- `IPseudoElementFactory`
: Add pseudo-element behavior.
- `IRenderDevice`
: Provide device characteristics for style computation.

## Override The Default Stylesheet

```cs
using AngleSharp;
using AngleSharp.Css;

var config = Configuration.Default.WithCss();
var context = BrowsingContext.New(config);

var provider = context.GetService<ICssDefaultStyleSheetProvider>();
provider.AppendDefault(@"
  :root { --brand-color: #0f766e; }
  button { border-radius: 8px; }
");
```

## Provide A Custom Render Device

```cs
var renderDevice = new DefaultRenderDevice
{
    Category = DeviceCategory.Screen,
    ViewPortWidth = 1440,
    ViewPortHeight = 900,
    DeviceWidth = 1440,
    DeviceHeight = 900,
    Resolution = 96,
    FontSize = 16,
};

var config = Configuration.Default
    .WithCss()
    .WithRenderDevice(renderDevice);
```

## Composition Pattern

Start from the default registrations and replace only what you need:

```cs
var config = Configuration.Default
    .WithCss(options)
    .WithRenderDevice(customDevice);
```

This preserves compatibility with the built-in pipeline while still allowing targeted customization.

## Practical Guidance

- Prefer additive customization first (append defaults, custom device).
- Replace factories only when behavior cannot be achieved through options.
- Keep custom services small and focused to simplify upgrades.
