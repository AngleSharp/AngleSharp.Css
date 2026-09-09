---
title: "Core Interfaces"
section: "AngleSharp.Css"
---
# Core Interfaces

This page summarizes the interfaces you will touch most often when building with AngleSharp.Css.

## Parsing

- `ICssParser`
: Parse stylesheets, declarations, and rules from string or stream input.

```cs
var context = BrowsingContext.New(Configuration.Default.WithCss());
var parser = context.GetService<ICssParser>();
var sheet = parser.ParseStyleSheet("h1 { color: red; }");
```

## Stylesheets And Rules

- `ICssStyleSheet`
: Root object for stylesheet operations (`Rules`, `Insert`, `RemoveAt`).
- `ICssRule`
: Base abstraction for every CSS rule.
- `ICssStyleRule`
: Style-rule specialization with `SelectorText` and `Style`.

```cs
foreach (var styleRule in sheet.Rules.OfType<ICssStyleRule>())
{
    Console.WriteLine(styleRule.SelectorText);
}
```

## Declarations And Values

- `ICssStyleDeclaration`
: Property bag for rule or inline declarations.
- `ICssValue`
: Parsed value abstraction with compute support.

```cs
var rule = sheet.GetStyleRuleWith("h1");
var value = rule.GetValueOf("color");
Console.WriteLine(value.CssText);
```

## Render And Device

- `IRenderDevice`
: Defines the device profile (viewport, DPI, color bits, category).
- `IRenderDimensions`
: Render-relevant dimensions and base font size.
- `IRenderNode`
: Node in the render tree.

```cs
var config = Configuration.Default
    .WithCss()
    .WithRenderDevice(new DefaultRenderDevice
    {
        ViewPortWidth = 1280,
        ViewPortHeight = 800,
        FontSize = 16,
    });
```

## Value Conversion Pipeline

- `IValueConverter`
: Parses tokenized input into `ICssValue`.
- `IValueAggregator`
: Splits/merges shorthand and longhand values.
- `IDeclarationFactory`
: Resolves property metadata and converter setup.

These are central for extensibility and custom property handling.

## Pseudo-Class Forcing

AngleSharp has no notion of pointer/keyboard interaction state, so pseudo-classes such as `:hover` and `:active` never match by default. `WithCss()` adds extension methods on `IElement` that let a caller force a pseudo-class to match (or not match) for a specific element, both in `Element.Matches(...)` and in computed style (`ComputeCurrentStyle()`/`ComputeDeclarations(...)`):

- `SetPseudoClass(pseudoClass, value = true)`
: Forces the given pseudo-class on (or off, with `value: false`).
- `GetPseudoClass(pseudoClass)`
: Returns the forced value, or `null` if nothing was forced for that element.
- `RemovePseudoClass(pseudoClass)`
: Clears a single forced pseudo-class, reverting to normal matching.
- `ClearPseudoClasses()`
: Clears every forced pseudo-class for the element.

```cs
using AngleSharp.Dom;

var target = document.QuerySelector("#target");
target.SetPseudoClass("hover");

Console.WriteLine(target.Matches(":hover")); // True
Console.WriteLine(target.ComputeCurrentStyle().GetPropertyValue("background-color"));

target.RemovePseudoClass("hover");
```

This applies to any pseudo-class the selector engine recognizes (`:hover`, `:active`, `:visited`, ...), works per element only (forcing a child does not propagate to its ancestors), and is not the mechanism for `:focus`: that pseudo-class already has a real, settable state in AngleSharp core, so `SetPseudoClass("focus", ...)` delegates to `IHtmlElement.DoFocus()`/`DoBlur()` instead of a separate forced flag.

## Rule Of Thumb

- Use parser and CSSOM interfaces for analysis/transforms.
- Use DOM + computed style interfaces for final style evaluation.
- Use render-tree interfaces when you need style-aware tree traversal.
