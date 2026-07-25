# AngleSharp.Css Documentation

We have more detailed information regarding the following subjects:

## General

- [Getting Started](general/01-Basics.md)
- [Value Model](general/02-Values.md)
- [CSSOM](general/03-CSSOM.md)
- [Core Interfaces](general/04-Core-Interfaces.md)
- [Extensibility](general/05-Extensibility.md)
- [Provided Services](general/06-Provided-Services.md)
- [Supported At-Rules](general/07-Supported-At-Rules.md)
- [Supported Declarations](general/08-Supported-Declarations.md)

## Tutorials

- [API Documentation](tutorials/01-API.md)
- [Examples](tutorials/02-Examples.md)
- [Render Tree Examples](tutorials/03-Render-Tree.md)
- [FAQ](tutorials/04-Questions.md)

## Recommended Reading Order

If you are new to AngleSharp.Css, this order gives a practical ramp-up:

1. [Getting Started](general/01-Basics.md) for setup and first working code.
2. [Examples](tutorials/02-Examples.md) for copy-paste recipes.
3. [API Documentation](tutorials/01-API.md) for deeper understanding of the CSSOM model.
4. [CSSOM](general/03-CSSOM.md) and [Core Interfaces](general/04-Core-Interfaces.md) once you need a deeper model understanding.
5. [Supported At-Rules](general/07-Supported-At-Rules.md) and [Supported Declarations](general/08-Supported-Declarations.md) when checking implementation coverage and constraints.
6. [Value Model](general/02-Values.md) and [Render Tree Examples](tutorials/03-Render-Tree.md) for style computation workflows.
7. [Extensibility](general/05-Extensibility.md) and [Provided Services](general/06-Provided-Services.md) for custom integrations.
8. [FAQ](tutorials/04-Questions.md) for common pitfalls and output customization.

## What You Can Do With AngleSharp.Css

Typical use cases include:

- Parsing and validating CSS snippets from editors, CMS content, or user input.
- Inspecting and rewriting style declarations programmatically.
- Loading real HTML pages and accessing linked stylesheets through the DOM.
- Computing final values (e.g., color, width, display) for elements.
- Building tooling: linters, migration scripts, design token extractors, or static analyzers.
