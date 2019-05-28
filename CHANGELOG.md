# 0.13.0

(tbd)

- Added `Compute` extension for `ICssStyleDeclaration`
- Added `GetMatchingStyles` extension for `ICssRuleList`
- Added `MinifyStyleFormatter`
- Added `Prettify` and `Minify` extension methods
- Fixed border-style expansion order (#34)

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
