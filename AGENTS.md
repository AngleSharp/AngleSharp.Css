# AGENTS.md

This guide helps coding agents become productive quickly in the AngleSharp.Css repository.

## 1) What this repository is

- AngleSharp.Css is the CSS extension package for AngleSharp.
- It provides CSS parsing, CSSOM objects, declaration/value conversion, stylesheet integration, and render tree support.
- Main code lives in `src/AngleSharp.Css`.
- Tests live in `src/AngleSharp.Css.Tests`.

## 2) Fast start commands

From repository root:

```bash
# Full build + unit tests through Fallout (Linux/macOS)
./build.sh

# Full build + unit tests through Fallout (Windows PowerShell)
./build.ps1

# Run tests directly
dotnet test src/AngleSharp.Css.Tests/AngleSharp.Css.Tests.csproj

# Build solution directly
dotnet build src/AngleSharp.Css.sln

# Run CSS performance benchmark (Release)
dotnet run --project src/AngleSharp.Performance.Css/AngleSharp.Performance.Css.csproj -c Release --framework net10.0

# Short benchmark smoke run
dotnet run --project src/AngleSharp.Performance.Css/AngleSharp.Performance.Css.csproj -c Release --framework net10.0 -- --job short
```

Notes:

- CI invokes `./build.sh -AngleSharpVersion 1.5.0` on Linux and `./build.ps1` on Windows.
- Treat warnings as errors is enabled in `src/Directory.Build.props`.

## 3) Build and target framework facts

- Library project: `src/AngleSharp.Css/AngleSharp.Css.csproj`
  - Targets: `netstandard2.0;net8.0;net10.0`
  - Additional Windows-only targets: `net462;net472`
- Test project: `src/AngleSharp.Css.Tests/AngleSharp.Css.Tests.csproj`
  - Targets: `net8.0`
- Fallout entrypoint: `build/Build.cs`
  - Default target chain runs unit tests.

## 4) Repository map (where to change what)

- `src/AngleSharp.Css/Parser`
  - Front-end parser (`CssParser`), tokenizer, builder, and micro parsers.
  - If parsing/tokenization behavior changes, start here.

- `src/AngleSharp.Css/Declarations`
  - One static class per declaration (name, converter, initial value, flags).
  - Example pattern: `DisplayDeclaration.cs`.

- `src/AngleSharp.Css/Factories/DefaultDeclarationFactory.cs`
  - Central registration table that wires declaration metadata.
  - New declaration support usually needs an entry here.

- `src/AngleSharp.Css/ValueConverters.cs` and `src/AngleSharp.Css/Converters`
  - Converter composition and reusable converter building blocks.
  - New value grammar often starts with a converter addition or composition.

- `src/AngleSharp.Css/Values`
  - CSS value object model (primitives, composites, function values, tuples/lists).

- `src/AngleSharp.Css/Dom` and `src/AngleSharp.Css/Dom/Internal`
  - Public CSSOM interfaces/enums and internal implementations of rules, declarations, and sheets.

- `src/AngleSharp.Css/Constants`
  - Canonical names and keyword constants (`PropertyNames`, `RuleNames`, `CssKeywords`, etc.).

- `src/AngleSharp.Css/RenderTree`
  - Render tree construction and render node/value computation.

- `src/AngleSharp.Css/FeatureValidators`
  - Validators used for media feature / supports-style checks.

- `src/AngleSharp.Css.Tests`
  - Test suites grouped by concern: declarations, rules, parsing, values, styling, extensions.

## 5) Common change playbooks

### Add or adjust a CSS declaration/property

1. Add or update declaration metadata class in `src/AngleSharp.Css/Declarations`.
2. Ensure canonical property name exists in `src/AngleSharp.Css/Constants/PropertyNames.cs`.
3. Wire declaration in `src/AngleSharp.Css/Factories/DefaultDeclarationFactory.cs`.
4. Add tests in `src/AngleSharp.Css.Tests/Declarations` and/or related top-level declaration test files.
5. Validate computed/style integration with tests in `src/AngleSharp.Css.Tests/Styling` when behavior impacts cascade/computation.

### Add or adjust value grammar

1. Extend converter composition in `src/AngleSharp.Css/ValueConverters.cs` and/or converter implementations in `src/AngleSharp.Css/Converters`.
2. Add/update specific value objects in `src/AngleSharp.Css/Values` if needed.
3. If grammar requires function or token-level parser support, update `src/AngleSharp.Css/Parser/Micro`.
4. Add tests under `src/AngleSharp.Css.Tests/Values` and declaration tests that consume the grammar.

### Add or adjust at-rules / CSSOM rule behavior

1. Public contract changes in `src/AngleSharp.Css/Dom` interfaces.
2. Implementation changes in `src/AngleSharp.Css/Dom/Internal/Rules`.
3. Parser/builder adjustments in `src/AngleSharp.Css/Parser`.
4. Rule-focused tests in `src/AngleSharp.Css.Tests/Rules`.

### Render-tree-related changes

1. Update computation/building in `src/AngleSharp.Css/RenderTree`.
2. Confirm integration in `src/AngleSharp.Css/Extensions/WindowExtensions.cs` and styling flows.
3. Add or update tests in `src/AngleSharp.Css.Tests/Styling`.

## 6) Test strategy for agents

- Prefer targeted test runs while iterating:

```bash
dotnet test src/AngleSharp.Css.Tests/AngleSharp.Css.Tests.csproj --filter FullyQualifiedName~CssProperty
```

- Before finishing, run full tests for confidence:

```bash
dotnet test src/AngleSharp.Css.Tests/AngleSharp.Css.Tests.csproj
```

- If changing multi-target-sensitive code (conditional behavior, APIs), also run full build via Fallout script to mimic CI flow.

## 7) Style and safety expectations

- Follow `.editorconfig` (spaces, indent size 4 for `.cs`, LF endings).
- Preserve existing namespace/file organization; this repo is convention-heavy.
- Avoid broad refactors when making focused feature fixes.
- Keep public API changes intentional; many files under `Dom` are effectively surface area.
- Existing code mixes nullable contexts (`#nullable enable` and `#nullable disable`); do not normalize unrelated files.
- In `src/AngleSharp.Css/ValueConverters.cs`, avoid static field initializers that reference converters declared later in the same file. Forward references can capture `null` during type initialization and only fail at runtime.

## 8) Documentation and onboarding references

- Root overview: `README.md`
- Extended docs index: `docs/README.md`
- Suggested docs learning order is documented in `docs/README.md`.

## 9) Quick pre-PR checklist for agents

1. Did I update all required wiring points (constants, declaration metadata, factory registration, parser/converter where needed)?
2. Did I add focused tests in the closest matching test area?
3. Does `dotnet test src/AngleSharp.Css.Tests/AngleSharp.Css.Tests.csproj` pass?
4. If CI-relevant build behavior changed, did I run `./build.sh` (or `./build.ps1` on Windows)?
5. Did I avoid unrelated formatting churn and preserve established structure?
