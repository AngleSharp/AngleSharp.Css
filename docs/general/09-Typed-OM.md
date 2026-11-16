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

## Limitations

- The computed style map is a snapshot of the computed style at the time of the call.
- Colors and other values beyond the Typed OM Level 1 are given as plain `CssStyleValue`.
- Custom properties are given as `CssUnparsedValue`, except for the computed values of registered ones (`@property`), which are typed according to their syntax, e.g., a `CssUnitValue` for `<length>`.
