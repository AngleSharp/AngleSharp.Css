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
