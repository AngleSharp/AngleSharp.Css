---
title: "Supported Declarations"
section: "AngleSharp.Css"
---
# Supported Declarations

This page documents declaration/property support in AngleSharp.Css based on the registration in `DefaultDeclarationFactory` and declaration classes in `src/AngleSharp.Css/Declarations`.

## Coverage Model

- A declaration is considered supported if a declaration class exists and is registered in the default declaration factory.
- The parser can optionally keep unknown declarations (`CssParserOptions.IsIncludingUnknownDeclarations = true`), but they are treated as generic unknown properties.
- Support means parsing and round-tripping in the CSSOM; semantic browser behavior (layout/painting) is outside scope.
- Total implemented declaration names: 412.

## By CSS Area / Spec

### Backgrounds and Borders

- Primary specs: CSS Backgrounds and Borders Module.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (82):
- `background`
- `background-attachment`
- `background-blend-mode`
- `background-clip`
- `background-color`
- `background-image`
- `background-origin`
- `background-position`
- `background-position-x`
- `background-position-y`
- `background-repeat`
- `background-repeat-x`
- `background-repeat-y`
- `background-size`
- `border`
- `border-block`
- `border-block-color`
- `border-block-end`
- `border-block-end-color`
- `border-block-end-style`
- `border-block-end-width`
- `border-block-start`
- `border-block-start-color`
- `border-block-start-style`
- `border-block-start-width`
- `border-block-style`
- `border-block-width`
- `border-bottom`
- `border-bottom-color`
- `border-bottom-left-radius`
- `border-bottom-right-radius`
- `border-bottom-style`
- `border-bottom-width`
- `border-collapse`
- `border-color`
- `border-end-end-radius`
- `border-end-start-radius`
- `border-image`
- `border-image-outset`
- `border-image-repeat`
- `border-image-slice`
- `border-image-source`
- `border-image-width`
- `border-inline`
- `border-inline-color`
- `border-inline-end`
- `border-inline-end-color`
- `border-inline-end-style`
- `border-inline-end-width`
- `border-inline-start`
- `border-inline-start-color`
- `border-inline-start-style`
- `border-inline-start-width`
- `border-inline-style`
- `border-inline-width`
- `border-left`
- `border-left-color`
- `border-left-style`
- `border-left-width`
- `border-radius`
- `border-right`
- `border-right-color`
- `border-right-style`
- `border-right-width`
- `border-spacing`
- `border-start-end-radius`
- `border-start-start-radius`
- `border-style`
- `border-top`
- `border-top-color`
- `border-top-left-radius`
- `border-top-right-radius`
- `border-top-style`
- `border-top-width`
- `border-width`
- `box-decoration-break`
- `box-shadow`
- `outline`
- `outline-color`
- `outline-offset`
- `outline-style`
- `outline-width`

### Fonts and Text

- Primary specs: CSS Fonts, CSS Text, CSS Writing Modes.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (52):
- `direction`
- `font`
- `font-display`
- `font-family`
- `font-kerning`
- `font-language-override`
- `font-optical-sizing`
- `font-palette`
- `font-size`
- `font-size-adjust`
- `font-stretch`
- `font-style`
- `font-synthesis`
- `font-synthesis-small-caps`
- `font-synthesis-style`
- `font-synthesis-weight`
- `font-variant`
- `font-variation-settings`
- `font-weight`
- `hanging-punctuation`
- `hyphenate-character`
- `hyphenate-limit-chars`
- `hyphens`
- `letter-spacing`
- `line-height`
- `ruby-align`
- `ruby-overhang`
- `ruby-position`
- `tab-size`
- `text-align`
- `text-align-last`
- `text-anchor`
- `text-decoration`
- `text-decoration-color`
- `text-decoration-line`
- `text-decoration-skip-ink`
- `text-decoration-style`
- `text-decoration-thickness`
- `text-indent`
- `text-justify`
- `text-shadow`
- `text-transform`
- `text-underline-offset`
- `text-wrap`
- `text-wrap-mode`
- `text-wrap-style`
- `unicode-bidi`
- `vertical-align`
- `white-space`
- `word-break`
- `word-spacing`
- `word-wrap`

### Overflow, Scrolling, and Scroll Snap

- Primary specs: CSS Overflow, CSS Scroll Snap, CSSOM View.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (47):
- `overflow`
- `overflow-anchor`
- `overflow-clip-margin`
- `overflow-wrap`
- `overflow-x`
- `overflow-y`
- `overscroll-behavior`
- `overscroll-behavior-block`
- `overscroll-behavior-inline`
- `overscroll-behavior-x`
- `overscroll-behavior-y`
- `scroll-behavior`
- `scroll-margin`
- `scroll-margin-block`
- `scroll-margin-block-end`
- `scroll-margin-block-start`
- `scroll-margin-bottom`
- `scroll-margin-inline`
- `scroll-margin-inline-end`
- `scroll-margin-inline-start`
- `scroll-margin-left`
- `scroll-margin-right`
- `scroll-margin-top`
- `scroll-padding`
- `scroll-padding-block`
- `scroll-padding-block-end`
- `scroll-padding-block-start`
- `scroll-padding-bottom`
- `scroll-padding-inline`
- `scroll-padding-inline-end`
- `scroll-padding-inline-start`
- `scroll-padding-left`
- `scroll-padding-right`
- `scroll-padding-top`
- `scroll-snap-stop`
- `scroll-snap-type`
- `scrollbar-arrow-color`
- `scrollbar-base-color`
- `scrollbar-color`
- `scrollbar-dark-shadow-color`
- `scrollbar-face-color`
- `scrollbar-gutter`
- `scrollbar-highlight-color`
- `scrollbar-shadow-color`
- `scrollbar-track-color`
- `scrollbar-width`
- `scrollbar3d-light-color`

### Box Model and Sizing

- Primary specs: CSS Box Model, CSS Sizing.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (36):
- `aspect-ratio`
- `block-size`
- `box-sizing`
- `height`
- `inline-size`
- `margin`
- `margin-block`
- `margin-block-end`
- `margin-block-start`
- `margin-bottom`
- `margin-inline`
- `margin-inline-end`
- `margin-inline-start`
- `margin-left`
- `margin-right`
- `margin-top`
- `max-block-size`
- `max-height`
- `max-inline-size`
- `max-width`
- `min-block-size`
- `min-height`
- `min-inline-size`
- `min-width`
- `padding`
- `padding-block`
- `padding-block-end`
- `padding-block-start`
- `padding-bottom`
- `padding-inline`
- `padding-inline-end`
- `padding-inline-start`
- `padding-left`
- `padding-right`
- `padding-top`
- `width`

### Animations, Transitions, and Transforms

- Primary specs: CSS Animations, CSS Transitions, CSS Transforms, View Transitions.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (29):
- `animation`
- `animation-composition`
- `animation-delay`
- `animation-direction`
- `animation-duration`
- `animation-fill-mode`
- `animation-iteration-count`
- `animation-name`
- `animation-play-state`
- `animation-range`
- `animation-range-end`
- `animation-range-start`
- `animation-timeline`
- `animation-timing-function`
- `backface-visibility`
- `perspective`
- `perspective-origin`
- `rotate`
- `scale`
- `transform`
- `transform-origin`
- `transform-style`
- `transition`
- `transition-delay`
- `transition-duration`
- `transition-property`
- `transition-timing-function`
- `translate`
- `view-transition-class`

### Tables, Fragmentation, and Multi-column

- Primary specs: CSS2 Tables, CSS Fragmentation, CSS Multi-column Layout.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (20):
- `break-after`
- `break-before`
- `break-inside`
- `caption-side`
- `column-count`
- `column-fill`
- `column-rule`
- `column-rule-color`
- `column-rule-style`
- `column-rule-width`
- `column-span`
- `column-width`
- `columns`
- `empty-cells`
- `orphans`
- `page-break-after`
- `page-break-before`
- `page-break-inside`
- `table-layout`
- `widows`

### Compositing, Masking, and Effects

- Primary specs: Filter Effects, Compositing and Blending, CSS Masking.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (20):
- `backdrop-filter`
- `clip`
- `mask-border`
- `mask-border-mode`
- `mask-border-outset`
- `mask-border-repeat`
- `mask-border-slice`
- `mask-border-source`
- `mask-border-width`
- `mask-clip`
- `mask-composite`
- `mask-image`
- `mask-mode`
- `mask-origin`
- `mask-position`
- `mask-repeat`
- `mask-size`
- `mask-type`
- `mix-blend-mode`
- `opacity`

### Visual Formatting and Positioning

- Primary specs: CSS2 Visual Formatting Model, CSS Positioned Layout.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (17):
- `bottom`
- `clear`
- `display`
- `float`
- `inset`
- `inset-block`
- `inset-block-end`
- `inset-block-start`
- `inset-inline`
- `inset-inline-end`
- `inset-inline-start`
- `left`
- `position`
- `right`
- `top`
- `visibility`
- `z-index`

### Lists, Counters, and Generated Content

- Primary specs: CSS Lists and Counters, Generated Content for Paged Media.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (15):
- `bookmark-label`
- `bookmark-level`
- `bookmark-state`
- `content`
- `counter-increment`
- `counter-reset`
- `counter-set`
- `footnote-display`
- `footnote-policy`
- `list-style`
- `list-style-image`
- `list-style-position`
- `list-style-type`
- `quotes`
- `running`

### Box Alignment

- Primary specs: CSS Box Alignment.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (12):
- `align-content`
- `align-items`
- `align-self`
- `column-gap`
- `gap`
- `justify-content`
- `justify-items`
- `justify-self`
- `place-content`
- `place-items`
- `place-self`
- `row-gap`

### Grid Layout

- Primary specs: CSS Grid Layout.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (17):
- `grid-area`
- `grid-auto-columns`
- `grid-auto-flow`
- `grid-auto-rows`
- `grid-column`
- `grid-column-end`
- `grid-column-gap`
- `grid-column-start`
- `grid-gap`
- `grid-row`
- `grid-row-end`
- `grid-row-gap`
- `grid-row-start`
- `grid-template`
- `grid-template-areas`
- `grid-template-columns`
- `grid-template-rows`

### Containment and Container Queries

- Primary specs: CSS Containment and Container Queries.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (10):
- `contain`
- `contain-intrinsic-block-size`
- `contain-intrinsic-height`
- `contain-intrinsic-inline-size`
- `contain-intrinsic-size`
- `contain-intrinsic-width`
- `container`
- `container-name`
- `container-type`
- `content-visibility`

### SVG and Graphics Presentation

- Primary specs: SVG/CSS Presentation Attributes.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (10):
- `image-rendering`
- `shape-rendering`
- `stroke`
- `stroke-dasharray`
- `stroke-dashoffset`
- `stroke-linecap`
- `stroke-linejoin`
- `stroke-miterlimit`
- `stroke-opacity`
- `stroke-width`

### Flexible Box Layout

- Primary specs: CSS Flexible Box Layout.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (8):
- `flex`
- `flex-basis`
- `flex-direction`
- `flex-flow`
- `flex-grow`
- `flex-shrink`
- `flex-wrap`
- `order`

### Anchor Positioning and Fallbacks

- Primary specs: CSS Anchor Positioning.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (7):
- `anchor-name`
- `anchor-scope`
- `position-anchor`
- `position-area`
- `position-try-fallbacks`
- `position-try-order`
- `position-visibility`

### User Interface

- Primary specs: CSS Basic User Interface.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (6):
- `accent-color`
- `appearance`
- `caret-color`
- `cursor`
- `touch-action`
- `user-select`

### Color and Color Adjustment

- Primary specs: CSS Color, CSS Color Adjustment.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (4):
- `color`
- `color-scheme`
- `forced-color-adjust`
- `print-color-adjust`

### Other / Legacy / Vendor

- Primary specs: Legacy / draft / vendor-oriented extensions.
- Potential limitations: Validation depth follows the registered converter; unsupported grammar branches are rejected or normalized by parser behavior.
- Implemented declarations (20):
- `fill`
- `grid`
- `image-orientation`
- `initial-letter`
- `initial-letter-align`
- `isolation`
- `line-break`
- `object-fit`
- `object-position`
- `pointer-events`
- `resize`
- `shape-image-threshold`
- `shape-margin`
- `shape-outside`
- `src`
- `string-set`
- `unicode-range`
- `view-transition-name`
- `white-space-collapse`
- `will-change`

