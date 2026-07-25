---
title: "Supported Rules"
section: "AngleSharp.Css"
---
# Supported At-Rules

This page documents the at-rules recognized by the AngleSharp.Css parser (`CssBuilder.AtRuleMap`) and the corresponding CSSOM rule implementations.

## Coverage Model

- A rule is considered supported if the parser maps its name to a dedicated `Create*` path in `CssBuilder`.
- Unknown at-rules can still be kept as `CssUnknownRule` when `CssParserOptions.IsIncludingUnknownRules = true`.
- Support here means parsing and CSSOM representation. Runtime behavior (rendering/layout) is outside scope.

## Supported At-Rules

| At-rule | Primary spec(s) | Implementation notes | Potential limitations |
| --- | --- | --- | --- |
| `@media` | Media Queries | Parsed into `CssMediaRule` with nested rule list. | Media query validation follows parser behavior and can normalize unsupported syntax. |
| `@supports` | CSS Conditional Rules | Parsed into `CssSupportsRule` with nested rule list. | Condition text parsing is syntax-driven; unsupported condition branches may fail to materialize as a rule. |
| `@import` | CSS Cascading and Inheritance | Parsed into `CssImportRule` (`href` + media text). | Modern import modifiers (e.g. richer layered/supports forms) are not modeled as dedicated properties. |
| `@charset` | CSS Syntax | Parsed into `CssCharsetRule`. | Only string charset payload is modeled. |
| `@namespace` | CSS Namespaces | Parsed into `CssNamespaceRule` (prefix + namespace URI). | URI assignment path is URL-token centric; some alternate serializations may not round-trip identically. |
| `@page` | Paged Media | Parsed into `CssPageRule` with declaration block. | Selector validation is parser-tolerant depending on options (`IsToleratingInvalidSelectors`). |
| `@font-face` | CSS Fonts | Parsed into `CssFontFaceRule`. | Descriptor handling is limited to a fixed subset in `ContainedProperties` (`font-family`, `src`, `font-style`, `font-weight`, `font-stretch`, `unicode-range`, `font-variant`). |
| `@keyframes` | CSS Animations | Parsed into `CssKeyframesRule` with `CssKeyframeRule` children. | Keyframe content is declaration parsing only; semantic animation engine behavior is outside scope. |
| `@container` | CSS Containment / Container Queries | Parsed into `CssContainerRule` with nested rules. | Condition text parsing is syntax-driven; unsupported condition forms may be rejected. |
| `@layer` | CSS Cascade Layers | Parsed into `CssLayerRule` (statement and block forms). | Layer ordering semantics beyond parsed structure are not enforced by a rendering engine here. |
| `@scope` | CSS Scoping | Parsed into `CssScopeRule` with nested rules. | Scope prelude is stored as text; advanced selector semantics depend on parser support. |
| `@property` | CSS Properties and Values API | Parsed into `CssPropertyRule` with descriptor storage. | Descriptors are generic properties (`CssDescriptorRule`) without spec-specific descriptor type validation. |
| `@starting-style` | CSS Transitions Level 2 | Parsed into `CssStartingStyleRule` with nested rules. | Behavior-level transition semantics are not evaluated. |
| `@view-transition` | View Transitions | Parsed into `CssViewTransitionRule` with descriptor storage. | Descriptors are stored generically, without dedicated descriptor schema validation. |
| `@position-try` | CSS Anchor Positioning | Parsed into `CssPositionTryRule` with declaration block. | No layout fallback engine; parsing/serialization only. |
| `@font-palette-values` | CSS Fonts Level 4 | Parsed into `CssFontPaletteValuesRule` with descriptor storage. | Descriptors are generic and not schema-validated per descriptor name/value. |
| `@color-profile` | CSS Color | Parsed into `CssColorProfileRule` with descriptor storage. | Descriptors are generic and not schema-validated per descriptor name/value. |
| `@counter-style` | CSS Counter Styles | Parsed into `CssCounterStyleRule`. | Current implementation stores the style name, but descriptor declarations are effectively not materialized (empty contained-property set). |
| `@font-feature-values` | CSS Fonts | Parsed into `CssFontFeatureValuesRule`. | Family name is captured, but the inner descriptor block is skipped (not populated into declarations). |
| `@viewport` | Device Adaptation (legacy) | Parsed into `CssViewportRule`. | Supports a fixed legacy descriptor subset (`min/max-width`, `min/max-height`, `zoom` family, `orientation`). |
| `@document` | Non-standard / vendor historical | Parsed into `CssDocumentRule` with nested rules. | Non-standard rule; interoperability expectations should be conservative. |

## Notes

- `CssRuleType` includes additional historical values such as region-style, but only the rules listed above are currently mapped by parser at-rule dispatch.
- If you need strict acceptance/rejection behavior for unknown at-rules, configure `CssParserOptions.IsIncludingUnknownRules` accordingly.
