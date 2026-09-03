![logo](https://raw.githubusercontent.com/AngleSharp/AngleSharp.Css/main/header.png)

# AngleSharp.Css

[![CI](https://github.com/AngleSharp/AngleSharp.Css/actions/workflows/ci.yml/badge.svg)](https://github.com/AngleSharp/AngleSharp.Css/actions/workflows/ci.yml)
[![GitHub Tag](https://img.shields.io/github/tag/AngleSharp/AngleSharp.Css.svg?style=flat-square)](https://github.com/AngleSharp/AngleSharp.Css/releases)
[![NuGet Count](https://img.shields.io/nuget/dt/AngleSharp.Css.svg?style=flat-square)](https://www.nuget.org/packages/AngleSharp.Css/)
[![Issues Open](https://img.shields.io/github/issues/AngleSharp/AngleSharp.Css.svg?style=flat-square)](https://github.com/AngleSharp/AngleSharp.Css/issues)
[![CLA Assistant](https://cla-assistant.io/readme/badge/AngleSharp/AngleSharp.Css?style=flat-square)](https://cla-assistant.io/AngleSharp/AngleSharp.Css)

AngleSharp.Css extends the core AngleSharp library with some more powerful CSS capabilities. This repository is the home of the source for the AngleSharp.Css NuGet package.

## Basic Configuration

If you just want a configuration *that works* (as close as possible to real browsers) you should use the following code:

```cs
var config = Configuration.Default
    .WithCss(); // from AngleSharp.Css
```

This will register a parser for CSS related content. The CSS parsing options and more could be set with parameters of the `WithCss` method. Alternatively, all the (desired) parts may be registered individually as well. That mostly boils down to three elementary parts:

- A CSS parser (implementing the `ICssParser` interface, e.g., `CssParser`)
- A factory for creating CSS declarations (`IDeclarationFactory`)
- The styling service that can handle CSS documents, see `CssStylingService`

For an interactive DOM (i.e., to handle `style` attribute changes in the HTML document) an observer needs to be registered as well.

Furthermore, for some CSSOM features (e.g., media queries) a render device is required.

```cs
var config = Configuration.Default
    .WithCss()
    .WithRenderDevice(new DefaultRenderDevice
    {
        DeviceHeight = 768,
        DeviceWidth = 1024,
    });
```

If no specific `IRenderDevice` (e.g., via creating an `DefaultRenderDevice` object) instance is created a default implementation will be set.

The render device also carries the *user preferences* of the Media Queries Level 5 (and Level 4) user-preference features. `DefaultRenderDevice` implements `IRenderDevicePreferences` for that, and any custom `IRenderDevice` can implement it as well. The dictionary is keyed by the media feature name and holds the keyword that the feature should answer with:

```cs
var config = Configuration.Default
    .WithCss()
    .WithRenderDevice(new DefaultRenderDevice
    {
        Preferences = new Dictionary<String, String>
        {
            { "prefers-color-scheme", "dark" },
            { "prefers-reduced-motion", "reduce" },
        },
    });
```

With this device `@media (prefers-color-scheme: dark)` applies in the cascade and `window.MatchMedia("(prefers-color-scheme: dark)").IsMatched` is `true`. A key that is not set leaves its media feature unknown, i.e., a query using it never matches. The keys a browser would set are:

| Key | Keywords |
| --- | --- |
| `prefers-color-scheme` | `light`, `dark` |
| `prefers-reduced-motion` | `no-preference`, `reduce` |
| `prefers-reduced-transparency` | `no-preference`, `reduce` |
| `prefers-contrast` | `no-preference`, `more`, `less`, `custom` |
| `prefers-reduced-data` | `no-preference`, `reduce` |
| `forced-colors` | `none`, `active` |
| `hover`, `any-hover` | `none`, `hover` |
| `pointer`, `any-pointer` | `none`, `coarse`, `fine` |
| `display-mode` | `fullscreen`, `standalone`, `minimal-ui`, `browser` |

The value is compared to the queried keyword case insensitively, so a keyword that is newer than this library works as well. Used without a value, e.g., `@media (prefers-reduced-motion)`, the feature evaluates in a boolean context, where `no-preference` (and `none` for `forced-colors`, `hover`, `any-hover`, `pointer` and `any-pointer`) is `false`. Without a preference `hover` and `pointer` keep answering as they did before, i.e., as a device with no input mechanism.

Going a bit further it is possible to `Render` the current document. This render tree information can then be used to retrieve or other information, e.g.,

```cs
var tree = document.DefaultView.Render();
var node = tree.Find(document.QuerySelector("div"));
await node.DownloadResources();
```

The previous snippet renders the current document. Afterwards it retrieves a particular render tree node, which is related to the first found `div`. Then all (CSS introduced) resources are downloaded for the node, if visible.

## Advantages of AngleSharp.Css

The core library already contains the CSS selector parser and the most basic classes and interfaces for dealing with the CSSOM. AngleSharp.Css brings the following advantages and use cases to life:

* Correct identification of edge cases
* A live CSSOM model, i.e., callbacks and everything
* Computing the styling of certain elements
* Cascades of stylesheets
* Validation (and property-based exposure) of CSS declarations
* Responsive design considerations
* Full access to the value with different converters

The main idea behind AngleSharp.Css is to expose the CSSOM as it would be in the browser (and potentially beyond, i.e., useful for being used by editors). Originally, most of the code found here was embedded in the AngleSharp.Core library, however, due to the overhead for HTML use cases it was decided to transfer the code into its own repository.

## Features

- Feature validators (e.g., for `@supports`)
- Document functions (e.g., for `domain`)
- Pseudo elements (e.g., `::before`)
- Declarations (e.g., `display`) incl. knowledge of their values
- Dynamic DOM coupling (i.e., to react to `style` attribute changes)
- CSS custom properties (also known as CSS variables) with extensibility
- Media queries and all other commonly implemented rules
- Calculated values (i.e., `calc(20px + 50%)`)
- Window-based declaration calculations, see `window.GetComputedStyle`

## Benchmarks

The `AngleSharp.Performance.Css` project uses [BenchmarkDotNet](https://benchmarkdotnet.org/) to compare CSS parsing performance across libraries. Run the benchmarks in Release mode:

```bash
dotnet run --project src/AngleSharp.Performance.Css/AngleSharp.Performance.Css.csproj -c Release --framework net10.0
```

To run a quick smoke test instead of a full benchmark:

```bash
dotnet run --project src/AngleSharp.Performance.Css/AngleSharp.Performance.Css.csproj -c Release --framework net10.0 -- --job short
```

## Participating

Participation in the project is highly welcome. For this project the same rules as for the AngleSharp core project may be applied.

If you have any question, concern, or spot an issue then please report it before opening a pull request. An initial discussion is appreciated regardless of the nature of the problem.

Live discussions can take place in our [Gitter chat](https://gitter.im/AngleSharp/AngleSharp), which supports using GitHub accounts.

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.

For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).

## License

AngleSharp.Css is released using the MIT license. For more information see the [license file](./LICENSE).
