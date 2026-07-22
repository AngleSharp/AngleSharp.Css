---
title: "Types of Values"
section: "AngleSharp.Css"
---
# Types of Values

For details see [CSS2 specification](https://www.w3.org/TR/CSS2/cascade.html#value-stages).

CSS values move through multiple stages. In practical tooling, understanding these stages helps you decide what to inspect and when.

## Value Stages At A Glance

- Specified values
: What was written in CSS (or inline style), usually still close to source form.
- Computed values
: Result after cascade + inheritance + defaulting. This is what most style analysis tools want.
- Used values
: Values after layout-dependent resolution (for example percentages resolved against parent sizes).
- Actual values
: Final rendered value after device constraints (pixels on a real device).

## Practical Rule Of Thumb

- If you are writing a linter or migration tool, inspect specified values.
- If you are checking "what style wins", inspect computed values.
- If you need final geometry, combine computed style with layout information from your runtime.

## Example: Inline Specified Value vs. Computed Value

```cs
using AngleSharp;
using AngleSharp.Css.Dom;
using AngleSharp.Dom;

var html = @"<!doctype html>
<style>
	.card { color: rgb(255, 0, 0); font-size: 20px; }
</style>
<div class='card' id='target' style='font-size: 1.5rem'>Hello</div>";

var context = BrowsingContext.New(Configuration.Default.WithCss());
var document = await context.OpenAsync(req => req.Content(html));

var element = document.QuerySelector("#target");

// Specified inline declaration (only from style="...")
var inlineStyle = element.GetStyle();
var specifiedFontSize = inlineStyle.GetPropertyValue("font-size");

// Computed style after cascade and inheritance
var computedStyle = element.ComputeStyle();
var computedColor = computedStyle.GetPropertyValue("color");
var computedFontSize = computedStyle.GetPropertyValue("font-size");

Console.WriteLine($"Specified inline font-size: {specifiedFontSize}");
Console.WriteLine($"Computed color: {computedColor}");
Console.WriteLine($"Computed font-size: {computedFontSize}");
```

## Common Pitfalls

- Shorthand values (e.g., `margin`, `background`) are decomposed internally to longhands.
- Variables (`var(--x)`) may defer full resolution until cascade context is available.
- Comparing raw source strings is often misleading; compare parsed or computed values instead.
