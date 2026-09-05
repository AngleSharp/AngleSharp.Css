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

## Custom Properties At Computed-Value Time

Custom properties are resolved only during style computation, for each element before
they are inherited. `GetDeclarations`, `ComputeExplicitStyle`, `ComputeCascadedStyle`,
and render-tree `SpecifiedStyle` retain the original variable expressions. Computed
results are separate declarations and do not rewrite stylesheet or inline values.

During computation, an inherited
alias keeps the parent's resolved value; changing its dependencies on a child does not
resolve that alias again. A declaration explicitly matching both elements is resolved
locally on each element.

Following [CSS Variables dependency-cycle rules](https://drafts.csswg.org/css-variables-1/#cycles),
every property in a cycle becomes guaranteed-invalid, including cycles through unused
fallbacks. A consuming `var(--name, fallback)` can recover from an invalid or missing
custom property. Without a usable fallback, the consuming declaration uses its inherited
or initial value, not an earlier declaration from the cascade. A valid custom-property
value that does not match the consumer's grammar does not trigger the `var()` fallback.

Dependency analysis and fallback substitution are iterative, including deeply nested
fallbacks. The public parser still represents nested `var()` fallbacks as `CssVarValue`
objects, and direct `CssReferenceValue.Compute` calls honor the supplied `References`
array, including subsequent changes to its entries.

Expanded values during style computation are limited to 1,048,576 UTF-16 code units (including token
separators) to bound exponential substitution; an expansion exceeding this limit is
invalid at computed-value time. Property-specific parsing, unit conversion, and layout
support still determine which resolved values can be used by a consuming property.
