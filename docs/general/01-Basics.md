---
title: "Getting Started"
section: "AngleSharp.Css"
---
# Getting Started

## Requirements

AngleSharp.Css comes currently in two flavors: on Windows for .NET 4.6 and in general targetting .NET Standard 2.0 platforms.

Most of the features of the library do not require .NET 4.6, which means you could create your own fork and modify it to work with previous versions of the .NET-Framework.

You need to have AngleSharp installed already. This could be done via NuGet:

```ps1
Install-Package AngleSharp
```

## Getting AngleSharp.Css over NuGet

The simplest way of integrating AngleSharp.Css to your project is by using NuGet. You can install AngleSharp.Css by opening the package manager console (PM) and typing in the following statement:

```ps1
Install-Package AngleSharp.Css
```

You can also use the graphical library package manager ("Manage NuGet Packages for Solution"). Searching for "AngleSharp.Css" in the official NuGet online feed will find this library.

## Setting up AngleSharp.Css

To use AngleSharp.Css you need to add it to your `Configuration` coming from AngleSharp itself.

If you just want a configuration *that works* (as close as possible to real browsers) you should use the following code:

```cs
var config = Configuration.Default
    .WithCss(); // from AngleSharp.Css
```

This will register a parser for CSS related content. The CSS parsing options and more could be set with parameters of the `WithCss` method.

Alternatively, all the (desired) parts may be registered individually as well. That mostly boils down to three elementary parts:

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
