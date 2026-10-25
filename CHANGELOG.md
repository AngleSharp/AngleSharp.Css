# 1.3.0

Released on Monday, October 26 2026.

- Improved the render tree to inherit declarations from the parent's style instead of copying them to every element (e.g. thousands of custom properties on `:root`)
- Improved merging declarations in the cascade, which was quadratic in the number of declarations
- Improved matching `::before` and `::after` by grouping the rules targeting pseudo-elements by the id, class, or tag name of the element they belong to, instead of matching all of them against every element
- Fixed a declaration starting with `*` (the IE7 hack `*zoom: 1`) being taken for a nested rule that swallowed the following declarations or the rest of the style sheet; it is dropped up to its semicolon
- Added `CssParserOptions.IsNotResolvingRelativeUrls` to prevent resolving the relative URLs in linked and imported style sheets

# 1.2.2

Released on Sunday, October 25 2026.

- Improved the cascade to only match the rules that can apply to an element (grouped by the id, class, or tag name of their subject)
- Improved the render tree to skip computing `::before` and `::after` without a `content` declaration
- Improved the lookup of property declarations
- Fixed stack overflow for `rem` in the `font-size` of the root element (now refers to the initial font size)
- Fixed media queries in the range syntax of Media Queries 4 (`(width >= 600px)`, `(400px < width <= 700px)`), which never matched
- Fixed media queries combined with `or`, nested conditions (`(a) and ((b) or (c))`), and `not` applying to the whole query
- Fixed media queries with math functions as values (`(width < calc(50rem + 2rem))`), which were invalid
- Fixed `var()` substitution separating tokens with comments where they could not merge (e.g. `minmax(/**/15rem/**/,1fr)`), which made grid track lists invalid
- Fixed `gap` with a single value (e.g. `gap: 8px` or `gap: var(--gap)`) not setting `column-gap`
- Fixed flow-relative properties (e.g. `padding-inline`, `margin-inline-end`, `inset-inline-start`, `inline-size`) not applying to the physical sides; they are now mapped according to `writing-mode` and `direction`, sharing one cascade with the physical properties
- Fixed `orientation` media feature to follow the viewport instead of the device
- Fixed `matchMedia` throwing for an invalid query instead of evaluating it as `not all`
- Fixed the pseudo-elements of CSS 2 written with a single colon (e.g. `li:after`) not applying to the pseudo-element
- Fixed pseudo-classes before a pseudo-element (e.g. `li:first-child::after`) being matched against the pseudo-element instead of its originating element
- Fixed the `background` shorthand not accepting `currentcolor` as its color
- Fixed `aspect-ratio` not accepting a single number (e.g. `aspect-ratio: 1.8`, meaning `1.8 / 1`)
- Fixed grid area and line names (e.g. `grid-area: leadingVisual`) and other custom identifiers (`view-transition-name`, `anchor-name`, `position-anchor`, `animation-timeline`, `font-palette`) being lowercased, so they no longer matched case-sensitive names like the ones of `grid-template-areas`
- Fixed `em` and percentage lengths throwing for an element with `font-size: 0` or an empty box (e.g. in `translate()`)
- Added cascade layers (`@layer`) to the cascade, whose rules were ignored before
- Added shadow trees to the render tree: a shadow host renders its shadow tree (styled by the shadow root's style sheets, with the host as scope for `:host`) with slots showing their assigned nodes, unless hidden
- Added `:host` rules of a shadow tree styling the shadow host in the render tree; they lose against the author rules of the host's own tree unless important
- Added the presentational hints of HTML elements to the cascade: dimension attributes (e.g. `<img width=40 height=30>`, `<td width>`) and `bgcolor`/`text`/`color` attributes map to their CSS properties, before all author rules

# 1.2.1

Released on Friday, October 23 2026.

- Fixed computed `display` not blockified for floats
- Fixed computed `display` not blockified for absolutely positioned boxes
- FIxed computed `display` not blockified for the root element
- Added support for `::placeholder`

# 1.2.0

Released on Wednesday, October 21 2026.

- Improved default stylesheet to follow the HTML Standard's rendering section for links, `mark`, `pre`, `hr`, `fieldset`, `abbr[title]`, `summary`, table cell padding, and list padding
- Improved default stylesheet to isolate `bdi`, `[dir]`, and block elements for the bidi algorithm (`unicode-bidi: isolate`)
- Updated `p`, `pre`, `blockquote`, and list margins in the default stylesheet to `1em` (from `1.12em`)
- Updated computed percentages to remain percentages instead of resolving them against the viewport, except for `font-size` (parent font size) and `line-height` (element font size)
- Updated `calc()`, `min()`, and `max()` to keep their expression in the computed value when it cannot be resolved (e.g., `calc(50% + 10px)`)
- Updated `font` shorthand to serialize font size keywords as written (e.g., `large` instead of `1.2em`)
- Fixed `em` resolving against the device font size instead of the element's font size (the parent's font size for `font-size`)
- Fixed `rem` ignoring the root element's font size
- Fixed computed `font-size` keywords (`medium`, `x-large`, `larger`, `smaller`, ...) not resolving to lengths
- Fixed computing `ex` and `ch` lengths throwing an exception
- Fixed `ComputeCurrentStyle` throwing for a `box-shadow` or `text-shadow` layer without a blur radius
- Fixed `word-break`, `overflow-wrap`, and `word-wrap` not being inherited
- Fixed computed `border-radius` shorthand (and other shorthands merging computed lists, tuples, or periodic values) being empty
- Fixed `grid: auto-flow / ...` losing `auto-flow` and `dense` expanding to the wrong `grid-auto-flow` axis
- Fixed `counters()` losing the comma and the quotes of its separator string
- Fixed `color: currentcolor` not computing to the inherited color
- Fixed `flex: none` not expanding to `0 0 auto`
- Fixed `flex` shorthand leaving omitted longhands unset instead of `1` for the factors and `0%` for the basis (e.g., `flex: 1` is `1 1 0%`)
- Fixed computed `flex` shorthand dropping longhands at their initial value (e.g., `flex-basis: 40px` reading back as `40px` instead of `0 1 40px`)
- Fixed legacy `writing-mode` values (`lr`, `lr-tb`, `rl`, `rl-tb`, `tb`, `tb-rl`) being rejected instead of mapping to `horizontal-tb` / `vertical-rl`
- Fixed `content-visibility` accepting `collapse` instead of `auto`
- Fixed `min-width`, `min-height`, and their logical counterparts rejecting `auto`
- Fixed radial gradients without a size serializing a double space
- Added `space-evenly` and the remaining CSS Box Alignment 3 keywords (`start`, `end`, `left`, `right`, `normal`, `stretch`, `self-start`, `self-end`, `legacy`, `first` / `last baseline`, `safe` / `unsafe`)
- Added support for the multi-keyword `display` syntax (e.g., `inline flex`) and `display: contents` / `ruby`
- Added support for `min()`, `max()`, and `clamp()`
- Added support for `stretch` and `fit-content()` in sizing properties
- Added support for the small, large, and dynamic viewport units (e.g., `svh`, `lvw`, `dvmin`)
- Added support for the space-separated syntax of `hsl()` and `hwb()`, `color-mix()` (interpolated in sRGB), and `light-dark()`
- Added support for percentages in `opacity`, `fill-opacity`, and `stroke-opacity`
- Added support for `currentcolor` in shadows
- Added support for three- and four-value positions (e.g., `right 10px bottom 20px`) and edge offsets in `background-position-x` / `-y`
- Added support for `image-set()` and the `x` resolution unit
- Added support for `aspect-ratio: auto <ratio>`, `grid-template-*: subgrid`, and `text-indent` with `hanging` / `each-line`
- Added support for font weights in the range 1 to 1000 (also in `@font-face`) and `font-style: oblique <angle>`
- Added support for the `jump-*` positions of `steps()` and the `linear()` easing function
- Added support for `plus-lighter`, `image-rendering: pixelated`, `background-clip: text`, `border-image-repeat: space`, `outline-style: auto`, `overflow-wrap: anywhere`, `white-space: break-spaces`, two-value `text-overflow`, and `float` / `clear` with `inline-start` / `inline-end`
- Added `font-variant-numeric`, `font-variant-ligatures`, `text-orientation`, `transform-box`, and `transition-behavior` properties

# 1.1.4

Released on Tuesday, October 20 2026.

- Improved default stylesheet with HTML5 elements, non-rendered elements, and form control styles
- Updated computed zero lengths to serialize with their unit (e.g., `0px`)
- Updated cascade to always order author rules after default stylesheet rules
- Updated `font-variant` to be a shorthand for `font-variant-caps`
- Fixed rules matching the host being applied to its `::before` / `::after` pseudo-elements
- Fixed computed `font-weight` to resolve `bolder` / `lighter` against the parent's weight
- Fixed computed `border-*-radius` to keep percentages instead of resolving them to pixels
- Fixed computed `background-position` and `mask-position` to keep percentages instead of resolving them against the viewport
- Fixed computed `background-position` and `background-repeat` being empty
- Fixed computed `border-image-slice` serializing missing sides as `NaNpx`
- Fixed computing a unitless `border-image-width` or `border-image-outset` throwing
- Fixed `border-image-outset` dropping unitless numbers
- Fixed `text-align-last` not being inherited
- Added support for `display: flow-root`
- Added support for double-position color stops in gradients (e.g., `red 0 50%`)
- Added support for filter functions in `backdrop-filter`
- Added support for multiple layers in `mask-size`, `mask-repeat`, and `mask-position`
- Added support for percentages in `font-stretch`
- Added `font-variant-caps` property

# 1.1.3

Released on Thursday, October 15 2026.

- Improved pseudo-class matching to skip a per-element `ConditionalWeakTable` probe when no state has ever been forced via `SetPseudoClass`
- Fixed box-shadow serialization dropping zero blur before nonzero spread (#249) @PrzemyslawKlys

# 1.1.2

Released on Friday, September 11 2026.

- Fixed issue with calculation of CSS grid style
- Fixed default value of CSS gradients
- Fixed issue with Point2D style computations
- Fixed issue with style computation of `border-image-slice` declarations
- Fixed handling of `::before` and `::after` w.r.t. `content` declarations
- Added support for more `text-overflow` keywords
- Added `alignment-baseline` declaration
- Added `baseline-shift` declaration
- Added `color-interpolation-filters` declaration
- Added `clip-path` and `clip-rule` declarations
- Added `dominant-baseline` declaration
- Added `fill-opacity` and `fill-rule` declarations
- Added `marker-start`, `marker-mid`, and `marker-end` declarations
- Added `mask` declaration
- Added `text-underline-position` declaration
- Added `writing-mode` declaration

# 1.1.1

Released on Wednesday, September 9 2026.

- Fixed unresolved converter re-entry (#243) @sebastienros
- Fixed issue with computation of `transform` functions
- Fixed computed `animation` longhands retaining unresolved `initial` values
- Fixed percentage `border-radius` components resolving against the wrong axis
- Fixed default list styling for unordered lists
- Added shorthand decomposition and `clip` support for `overflow`
- Added explicit pseudo class handling via `SetPseudoClass`
- Added support for parsing `filter` declarations

# 1.1.0

Released on Saturday, September 5 2026.

- Improved evaluation of comma-separated media queries (#230)
- Fixed wrong media feature used for scripting (#233)
- Fixed wrong rientation and scan evaluation (#232)
- Fixed `not <known media type>` is always false (#231)
- Fixed `calc()` computations in AoT-compiled applications (#236) @sebastienros
- Fixed usage of `calc()` with unitless scaling (multiplication / division)
- Fixed case-sensitive matching of `and` / `or` in `@supports` and `from` / `to` in `@keyframes` (#240) @meziantou
- Fixed operator associativity and result units in `calc()` (#239) @meziantou
- Fixed unquoted and invalid `url()` handling (#238) @meziantou
- Fixed handling of invalid keyframe selectors
- Fixed stackoverflow due to cyclic CSS variables (#241)
- Added optional CSSOM compliant color seralization (#229) @lahma
- Added user-preference media features to the render device (#235) @lahma
- Added media query list evaluation using `IRenderDevice` (#228) @lahma

# 1.0.2

Released on Friday, August 21 2026.

- Fixed `oklab`/`oklch` usage with plain number as first argument (#225) @scasteran-jw

# 1.0.1

Released on Friday, July 31 2026.

- Fixed NRE in `mask-image` with gradients (#218)
- Improved cascade resolution performance (#220) @lahma

# 1.0.0

Released on Sunday, July 26 2026.

- First stable version

# 0.18.0

Released on Saturday, July 25 2026.

- Updated to use AngleSharp minimum version of 1.5 (#150)
- Updated media parsing to media L4 spec (#133)
- Updated naming of CSS values (e.g., `Color` to `CssColorValue`)
- Fixed issue when updating shorthands with invalid values (#129)
- Fixed issue with appended EOF character in `CssText` (#123)
- Fixed missing semicolon in `@page` rule (#135)
- Fixed integer serialization of keyframe stops (#128)
- Fixed ordering of rows and columns in `grid` and `grid-gap` (#137)
- Fixed inclusion of CSS from stylesheets (#116, #140)
- Fixed style empty if `text-align` is `start` (#151)
- Fixed computation of priority in CSS rules using multi selector
- Fixed `GetInnerText` multi-line / text node behavior (#155) @Seyden
- Fixed computation of relative (`em`) values to absolute (`px`) for `Length` (#136)
- Added further compactification of CSS tuples (#89, #93)
- Added new value types `CssPercentageValue`, `CssNumberValue`, and `CssIntegerValue`
- Added support for CSS nesting in style rules (#148)
- Added resolution of CSS variable names (#62)
- Added support for 8-digit hex color codes (#132)
- Added support for `margin-block` and `margin-inline` declarations
- Added support for `padding-block` and `padding-inline` declarations
- Added missing box model declarations (e.g., `aspect-ratio`, `block-size`, `overflow-clip-margin`)
- Added missing logical declarations (e.g., `inset`, `border-inline`, `border-end-start-radius`)
- Added missing alignment declarations (e.g., `justify-items`, `place-content`)
- Added missing animations declarations (e.g., `animation-composition`, `animation-delay-start`)
- Added missing scroll declarations (e.g., `scroll-behavior`, `overscroll-behavior`)
- Added missing fonts declarations (e.g., `font-display`, `font-palette`, `font-synthesis`)
- Added missing color declarations (e.g., `color-scheme`, `accent-color`)
- Added missing compositing declarations (e.g., `backdrop-filter`, `isolation`)
- Added missing shape declarations (e.g., `shape-outside`, `shape-rendering`)
- Added missing anchor positioning declarations (e.g., `anchor-name`, `anchor-scope`, `position-anchor`)
- Added missing text declarations (e.g., `text-underline-offset`, `tab-size`, `initial-letter`)
- Added missing masking declarations (e.g., `mask-image`, `mask-border-source`)
- Added missing auxiliary declarations (e.g., `counter-set`, `image-rendering`, `view-transition-name`)
- Added missing containment declarations (e.g., `contain`, `will-change`)
- Added more CSSOM possibilities and helpers (#6)
- Added more rules such as `@view-transition` or `@layer`
- Added parts of recent color spec update such as `rgb` with spaces (#131)
- Added now Color L4 parsing with `hsl`, `hwb`, `lab`, `lch`, `oklab`, and `oklch`
- Added support for recent CSS `list-type` values (#152)
- Added option to preserve comments in CSS stylesheet (#99)
- Added support for CSS container queries

# 0.17.0

Released on Sunday, January 15 2023.

- Updated build system to use NUKE instead of CAKE
- Dropped .NET Framework 4.6
- Updated to use AngleSharp 0.17
- Updated micro parser API to be public (#111)
- Fixed casing issue with color, timing, and gradient functions (#109)
- Fixed parsing of `skew` (#101)
- Fixed shorthand properties using `inherit` being omitted (#100)
- Added support for `@counter-style` (#102)
- Added support for `@font-feature-values` (#102)
- Added support for `conic-gradient` (#101)
- Added support for `scroll-snap-type` declaration (#91)

# 0.16.4

Released on Tuesday, February 15 2022.

- Fixed issue with wrong AngleSharp version used for build (#103)

# 0.16.3

Released on Wednesday, January 5 2022.

- Fixed issue with `text-shadow` missing the color part (#97)
- Fixed support for `IsToleratingInvalidSelectors` parser option (#98)
- Added `Color.UseHex` to change color output format (#96)

# 0.16.2

Released on Thursday, November 4 2021.

- Fixed issue with unbalanced grid areas and rows (#82)
- Fixed small numbers to be transformed into negative exponentials (#80)
- Fixed issue handling exponential unit values (#79)
- Fixed `InvalidCastException` in template merging with CSS variables (#83)

# 0.16.1

Released on Wednesday, August 11 2021.

- Fixed issue with certain letter combinations in data URIs (#76)
- Added support for `xxx-large` keyword in `font-size` (#77)

# 0.16.0

Released on Sunday, June 13 2021.

- Updated to use AngleSharp 0.16

# 0.15.1

Release on Sunday, June 6 2021.

- Extended values for the `width` declaration (#69)

# 0.15.0

Released on Thursday, April 22 2021.

- Added `fill` declaration (#59)
- Added support for legacy `linear-gradient` syntax (#66)
- Fixed parsing of `background: none` (#65)
- Fixed issues with brackets being removed for `calc` expressions (#67)
- Fixed issue throwing in `grid-template: none` serializations (#63, #68)
- Dropped support for the .NET Standard 1.3 target

# 0.14.2

Released on Thursday, June 11 2020.

- Fixed issue for representation of `counter()` and `counters()` (#58)
- Fixed parsing of numerical font-weight in font shorthand (#57)

# 0.14.1

Released on Monday, May 4 2020.

- Updated `ComputeDeclarations` and `ComputeCascadedStyle` to support `IEnumerable<ICssStyleRule>` (#53)

# 0.14.0

Released on Tuesday, April 7 2020.

- Added a way to compute relative dimensions (#3)
- Added render tree information incl. utilities (#4)
- Fixed issue with empty content (#42)
- Added debugger display attribute to CSS rules (#43)
- Fixed handling of CSS gradients (#45)
- Improved CSS string representation of gradients (#46)
- Exposed `IStyleCollection` for CSS info (#51)
- Fixed analysis issue with potential null selector (#52)
- Added support for the .NET Framework 4.6.1 target
- Added support for the .NET Framework 4.7.2 target

# 0.13.0

Released on Friday, September 6 2019.

- Added `Compute` extension for `ICssStyleDeclaration`
- Added `GetMatchingStyles` extension for `ICssRuleList`
- Added `MinifyStyleFormatter`
- Added `Prettify` and `Minify` extension methods
- Fixed border-style expansion order (#34)
- Fixed text-decoration expansion order (#35)
- Fixed missing whitespace in `GetInnerText()` (#32)

# 0.12.1

Released on Wednesday, May 15 2019.

- Binary version fix
- Fixed indexers are missing a `DomAccessorAttribute` (#30)
- Added `cssRules` alias: `rules` (#29)

# 0.12.0

Released on Sunday, May 12 2019.

- Reference latest AngleSharp
- Fixed empty value when removing properties (#14)
- Returns `null` in `GetStyle` if CSS not configured (#15)
- Added `pointer-events` and fixed border recombination (#16)
- Fixed `stroke-width` value without unit (#18)
- Fixed exception when not providing an `IRenderDevice` (#20)
- Fixed missing `CssStylingService.Default` in cascade (#21)
- Added extension helper `SetStyle` to modify all styles of many elements (#22)
- Fixed expansion of shorthand properties to longhand (#23)
- Opened CSSOM, e.g., declared `ICssFunctionValue` `public` (#24)
- Introduced special converter for the `src` declaration (#25)
- Fixed bug regarding CSS grid serialization (#27)
- Added `DefaultRenderDevice` implementation

# 0.10.1

Released on Monday, January 7 2019.

- Reference latest AngleSharp
- Updated reference to `System.Encoding.CodePages` (#13)

# 0.10.0

Released on Friday, January 4 2019.

- Initial release
