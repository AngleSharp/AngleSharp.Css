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
- `IRenderDevicePreferences`
: Provide the user preferences answering the user-preference media features.

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

## Provide The User Preferences

Beside the dimensions a render device carries the user preferences, which answer the user-preference media features. `DefaultRenderDevice` implements `IRenderDevicePreferences` for that; a custom `IRenderDevice` can implement it as well and is picked up the same way.

```cs
var renderDevice = new DefaultRenderDevice
{
    Preferences = new Dictionary<String, String>
    {
        { "prefers-color-scheme", "dark" },
        { "prefers-reduced-motion", "reduce" },
    },
};
```

The dictionary is keyed by the media feature name and holds the keyword the feature answers with. A key that is not set leaves its media feature unknown, i.e., a query using it never matches.

| Key | Keywords |
| --- | --- |
| `prefers-color-scheme` | `light`, `dark` |
| `prefers-reduced-motion` | `no-preference`, `reduce` |
| `prefers-reduced-transparency` | `no-preference`, `reduce` |
| `prefers-contrast` | `no-preference`, `more`, `less`, `custom` |
| `prefers-reduced-data` | `no-preference`, `reduce` |
| `forced-colors` | `none`, `active` |
| `hover`, `any-hover` | `none`, `hover` |
| `pointer`, `any-pointer` | `none`, `coarse`, `fine` |
| `display-mode` | `fullscreen`, `standalone`, `minimal-ui`, `browser` |

The value is compared to the queried keyword case insensitively, so a keyword that is newer than this library works as well. Used without a value, e.g., `@media (prefers-reduced-motion)`, the feature evaluates in a boolean context, where `no-preference` (and `none` for `forced-colors`, `hover`, `any-hover`, `pointer` and `any-pointer`) is `false`. Without a preference `hover` and `pointer` keep answering as they did before, i.e., as a device with no input mechanism.

## Expose The CSSOM To Scripts Without Reflection

A scripting engine such as AngleSharp.Js finds the CSSOM by reflecting over its DOM attributes by default. A trimmed or natively compiled (AoT) application can instead hand the engine the registration that is generated for this library at build time, `AngleSharp.Css.CssDom.Register` (a `DomRegistration` of AngleSharp). It declares the CSSOM types, the `CSS` namespace and the members the library adds to the DOM, such as `element.style` or `getComputedStyle`:

```cs
var config = Configuration.Default
    .WithCss()
    .WithJs(CssDom.Register);
```

The registration describes the version of AngleSharp.Css in use, so it never has to be kept in sync by hand. Several libraries are combined with `DomRegistrations.Combine`.

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
