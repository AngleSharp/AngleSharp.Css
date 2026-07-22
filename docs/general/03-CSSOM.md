---
title: "CSSOM"
section: "AngleSharp.Css"
---
# CSSOM

The CSS Object Model (CSSOM) in AngleSharp.Css gives you structured access to stylesheet rules, declarations, and values.

## Why CSSOM Matters

Use CSSOM when you need to:

- inspect selectors and declarations without string parsing,
- transform CSS safely (e.g., migrate properties),
- build reports or linters,
- connect DOM elements with computed style data.

## Core Object Graph

A typical traversal looks like this:

- `ICssStyleSheet` -> stylesheet root,
- `ICssRule` / `ICssStyleRule` -> individual rules,
- `ICssStyleDeclaration` -> declaration block,
- `ICssValue` -> parsed property values.

## Practical: Parse And Traverse Rules

```cs
using AngleSharp;
using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;

var context = BrowsingContext.New(Configuration.Default.WithCss());
var parser = context.GetService<ICssParser>();

var sheet = parser.ParseStyleSheet(@"
  .btn { padding: 8px 12px; border-radius: 4px; }
  @media (max-width: 640px) { .btn { width: 100%; } }
");

foreach (var rule in sheet.Rules)
{
    if (rule is ICssStyleRule styleRule)
    {
        Console.WriteLine($"Selector: {styleRule.SelectorText}");
        Console.WriteLine($"Declaration count: {styleRule.Style.Length}");
    }
    else
    {
        Console.WriteLine($"Rule type: {rule.Type}");
    }
}
```

## Practical: Modify A Stylesheet Rule

```cs
using AngleSharp.Css.Dom;

var styleRule = sheet.Rules.OfType<ICssStyleRule>()
    .FirstOrDefault(r => r.SelectorText == ".btn");

if (styleRule is not null)
{
    styleRule.Style.SetProperty("background-color", "#0f766e");
    styleRule.Style.SetProperty("color", "white");
}

Console.WriteLine(sheet.ToCss());
```

## CSSOM And The DOM

When you have a document, CSSOM can be used with computed styles:

```cs
var document = await context.OpenAsync(req => req.Content("<style>.x{color:red}</style><p class='x'>Hi</p>"));
var element = document.QuerySelector(".x");
var computed = element.ComputeStyle();

Console.WriteLine(computed.GetPropertyValue("color"));
```

## Tips

- Prefer `ICssStyleRule` over generic `ICssRule` when you need selectors and declarations.
- Use computed styles for cascade/inheritance results, not only inline style.
- Avoid raw string matching for values when semantic comparison is required.
