---
title: "Typed OM"
section: "AngleSharp.Css"
---
# Typed OM

AngleSharp.Css implements the [CSS Typed Object Model Level 1](https://www.w3.org/TR/css-typed-om-1/) in the `AngleSharp.Css.TypedOm` namespace. Instead of strings, values are given as objects: a length is a `CssUnitValue` with a value and a unit, a `calc()` is a tree of math values, and a `transform` is a list of transform components.

The Typed OM is a view of the CSSOM, not a copy of it. Typed values are created (reified) from the values the CSSOM already parsed whenever they are requested, and values written through a style map are validated and stored by the converter of the property, exactly as `SetProperty` does. The existing CSSOM interfaces are unchanged.

## Style Property Maps

```cs
using AngleSharp.Css.Dom;
using AngleSharp.Css.TypedOm;

var element = document.QuerySelector("div");

// the inline style, i.e., the style attribute (attributeStyleMap)
var map = element.GetAttributeStyleMap();
map.Set("width", CssHelpers.Px(100));
map.Set("height", "calc(100% - 2em)");
var width = (CssUnitValue)map.Get("width"); // 100, "px"

// the declarations of a style rule (styleMap)
var ruleMap = styleRule.GetStyleMap();

// the computed style (computedStyleMap()), which is read-only
var computed = element.ComputedStyleMap();
var fontSize = computed.Get("font-size");
```

- `Get` gives the (first) value, `GetAll` all values of a list-valued property (e.g., the images of `background-image`), `Has` and `Size` tell what is declared.
- `Set` and `Append` take typed values or CSS text. Multiple values (and `Append`) are only allowed for list-valued properties. An invalid value throws an `ArgumentException` (the `TypeError` of the specification) and leaves the declarations unchanged.
- The priority (`!important`) of a declaration is kept when it is set.
- `GetStyleMap()` is available for any `ICssStyleDeclaration`, and `GetReadOnlyStyleMap()` gives a read-only view.

## Values

| CSS value | Typed value |
|-----------|-------------|
| `10px`, `50%`, `2`, `1s` | `CssUnitValue` |
| `calc()`, `min()`, `max()`, `clamp()` | `CssMathSum`, `CssMathProduct`, `CssMathNegate`, `CssMathInvert`, `CssMathMin`, `CssMathMax`, `CssMathClamp` |
| `auto`, `block`, `inherit` | `CssKeywordValue` |
| custom properties, values with `var()` | `CssUnparsedValue` (strings and `CssVariableReferenceValue`) |
| `transform` | `CssTransformValue` of `CssTranslate`, `CssRotate`, `CssScale`, `CssSkew`, `CssSkewX`, `CssSkewY`, `CssPerspective`, `CssMatrixComponent` |
| `url()`, gradients | `CssImageValue` |
| positions, e.g., of `object-position` | `CssPositionValue` |
| anything else, e.g., colors or shorthands | `CssStyleValue` |

A value without a more specific representation is a plain `CssStyleValue`, which only knows its serialization and can only be set for the property it was obtained for.

Values are parsed with `CssStyleValue.Parse(property, text)` / `ParseAll`, or `CssNumericValue.Parse(text)` for numeric values. An `ICssProperty` or `ICssValue` of the CSSOM is turned into a typed value with `ToStyleValue()`. Every typed value is serialized with `ToString()`.

## Numeric Values

The factories of the `CSS` namespace are on `CssHelpers`, e.g., `CssHelpers.Px(10)`, `CssHelpers.Percent(50)`, or `CssHelpers.Number(2)`. A `Double` converts implicitly to a number.

```cs
var a = CssHelpers.Px(10).Add(CssHelpers.Px(5));      // 15px
var b = CssHelpers.Px(10).Add(CssHelpers.Em(1));      // calc(10px + 1em)
var c = CssHelpers.Px(10) * 2;                        // 20px
var d = CssHelpers.In(1).To("px");                    // 96px
var e = CssNumericValue.Parse("calc(1in + 1em + 1px)").ToSum("px", "em"); // calc(97px + 1em)
var t = b.Type();                                     // length: 1
```

- `Add`, `Sub`, `Mul`, `Div`, `Min`, and `Max` give a `CssUnitValue` when the units allow it, otherwise a math value. Incompatible types (e.g., a length and a time) throw an `ArgumentException`.
- `To` and `ToSum` convert between compatible absolute units (lengths, angles, times, frequencies, resolutions). Relative units such as `em`, `%`, or `vw` are kept as they are, as they can only be resolved during layout.
- `Equals` compares values structurally.
- `CssTransformValue.ToMatrix()` gives the `TransformMatrix` of a transform with absolute units.

## Registered Custom Properties

A custom property can be registered with a valid `@property` rule or with `CSS.registerProperty()` (`window.RegisterProperty(new PropertyDefinition { ... })`), see the [CSS Properties and Values API Level 1](https://www.w3.org/TR/css-properties-values-api-1/). Both kinds of registration share one representation, `ICssPropertyRegistration`, which the cascade, the computed style, and the Typed OM use alike.

```cs
using AngleSharp.Css.Dom;
using AngleSharp.Css.TypedOm;

// the registrations in effect for the document (script wins over @property for a name)
var registration = document.GetRegisteredProperty("--gap");
var all = document.GetRegisteredProperties();

registration.Syntax;           // "<length>"
registration.Inherits;         // false
registration.Origin;           // Rule (@property) or Script (CSS.registerProperty)
registration.IsList;           // true for e.g. "<length>#" or "<length>+"
registration.InitialValue;     // "0px"
registration.RawInitialValue;  // the CSSOM value, e.g., a CssLengthValue
registration.RawInitialValue.ToStyleValue(registration.Name); // CssUnitValue 0px
registration.Match("10px");    // the CSSOM value matching the syntax, or null

// the registration a single @property rule makes, whether it is in effect or not
var own = propertyRule.GetRegistration(); // null if the rule is not valid
```

The registrations always reflect the current style sheets, but they are only collected again after something they depend on changed: the DOM (e.g., an added or removed `<style>`, its text, or its `media` attribute), the rules (e.g., an inserted rule or a changed descriptor of a `@property` rule), the loaded, disabled, adopted, or default style sheets, the registrations of script, or the result of a condition (`@media`, `@supports`, the media of a sheet) around a `@property` rule, e.g., after the viewport of the render device changed. Reading them, e.g., the value of a custom property from a style map, therefore costs a few checks, not a walk over the style sheets. A `@property` rule follows the conditions of enclosing rules (e.g., `@media`) like a style rule does.

In the style maps, a registered custom property behaves like a property with the grammar of its syntax:

- `Get` and `GetAll` type the value according to the syntax, also for declared values, e.g., `--gap: 2em` is a `CssUnitValue` of `2em` in the `attributeStyleMap` and of `32px` in the `computedStyleMap()`. A list syntax (`<length>#`, `<length>+`) gives one value per item.
- A value that has references (e.g., `var(--x)`), does not match the syntax (it is invalid at computed-value time), or is registered with the universal syntax `*` stays a `CssUnparsedValue`, like the value of an unregistered custom property.
- `Set` and `Append` throw an `ArgumentException` for a value that does not match the syntax, unless it is a CSS-wide keyword or has references. Only a list syntax is list-valued, i.e., takes multiple values or `Append`.
- `CssStyleValue.Parse(property, text, context)` uses the registrations of the context's active document.

The style maps of an element use the registrations of its document, the map of a style rule those of the document owning the style sheet (or else of the active document of the context).

## CSSOM and Typed OM

The CSSOM and the Typed OM are two representations of the same declarations, and the Typed OM never holds values of its own:

| Representation | Obtained by | Purpose |
|----------------|-------------|---------|
| Text | `GetPropertyValue`, `ICssProperty.Value`, `ICssValue.CssText` | Serialization, unchanged by the Typed OM |
| CSSOM value (`ICssValue`) | `ICssProperty.RawValue`, `ICssPropertyRegistration.Match` | The result of the converters of a property (or the syntax of a registration), which the cascade and computation use |
| Typed value (`CssStyleValue`) | style maps, `ToStyleValue()`, `CssStyleValue.Parse` | The Typed OM, reified from the CSSOM value |

- Reading: a typed value is reified from the CSSOM value of a declaration. Nothing is parsed again, unless the declaration only has text (e.g., a used value of a computed style).
- Writing: a typed value is serialized and parsed by the converter of the property (or matched against the syntax of a registration), exactly like a value set with `SetProperty`. The CSSOM therefore never sees a value it could not have parsed itself, and its serialization does not change.
- The CSSOM stays unaware of registrations for declared values: `SetProperty("--gap", "red")` keeps the value (custom properties accept any value at parse time), which is then invalid at computed-value time and reified as `CssUnparsedValue`. Only the Typed OM `Set` validates against the syntax, as the specification demands.

## Specification Coverage

How the CSSOM relates to the [CSS Properties and Values API Level 1](https://www.w3.org/TR/css-properties-values-api-1/):

| Specification | AngleSharp.Css |
|---------------|----------------|
| `@property` rule, `CSSPropertyRule` (`name`, `syntax`, `inherits`, `initialValue`) | `ICssPropertyRule` (`Name`, `Syntax`, `Inherits`, `InitialValue`, plus `IsValid`) |
| `CSS.registerProperty(PropertyDefinition)` | `RegisterProperty` on `IWindow` / `IDocument` with `PropertyDefinition`, `SyntaxError` / `InvalidModificationError` as `DomException` |
| Registered custom property (the registration map of the document) | `ICssPropertyRegistration`, `GetRegisteredProperties()` / `GetRegisteredProperty(name)`, `GetRegistration()` of a rule |
| Syntax strings and matching | `ICssPropertyRegistration.Syntax`, `Match`, `IsList`, using the existing value converters |
| Computed value, inheritance, initial value | Computed style (`ComputeCurrentStyle`, `GetComputedStyle`, render tree) |
| Typed OM reification of registered properties | Style maps and `CssStyleValue.Parse` |
| Animation of registered properties | Not applicable, as there is no animation engine |

## Limitations

- The computed style map is a snapshot of the computed style at the time of the call.
- Colors and other values beyond the Typed OM Level 1 are given as plain `CssStyleValue`.
- Custom properties are given as `CssUnparsedValue`, except for registered ones, which are typed according to their syntax, e.g., a `CssUnitValue` for `<length>` (see above).
- `ToStyleValue()` of an `ICssProperty` or `ICssValue` knows no document, i.e., the declared value of a registered custom property is a `CssUnparsedValue` there; use a style map, or match the value with the registration first.
