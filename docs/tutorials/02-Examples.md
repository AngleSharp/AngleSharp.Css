---
title: "Examples"
section: "AngleSharp.Css"
---
# Example Code

This page collects practical recipes for every-day AngleSharp.Css usage.

## 1. Parse CSS And List Selectors

```cs
using AngleSharp;
using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;

var css = @"
  .btn { border-radius: 4px; }
  #layout main > article { max-width: 70ch; }
";

var context = BrowsingContext.New(Configuration.Default.WithCss());
var parser = context.GetService<ICssParser>();
var sheet = parser.ParseStyleSheet(css);

foreach (var rule in sheet.Rules.OfType<ICssStyleRule>())
{
	Console.WriteLine(rule.SelectorText);
}
```

## 2. Find A Rule And Read A Property

```cs
using AngleSharp.Css.Dom;

var sheet = parser.ParseStyleSheet("p > a { border: 1px solid red }");
var rule = sheet.GetStyleRuleWith("p>a");
var color = rule.GetValueOf("border-right-color").AsRgba();

Console.WriteLine(color); // e.g., RGBA packed representation
```

## 3. Parse HTML, Then Edit Inline Style

```cs
using AngleSharp;
using AngleSharp.Css.Dom;

var html = "<div id='card' style='padding: 8px'>Card</div>";
var context = BrowsingContext.New(Configuration.Default.WithCss());
var document = await context.OpenAsync(req => req.Content(html));

var card = document.QuerySelector("#card");
var style = card.GetStyle();

style.SetProperty("padding", "12px");
style.SetProperty("border", "1px solid #d0d7de");

Console.WriteLine(card.GetAttribute("style"));
```

## 4. Read Computed Style For Assertions

Use this pattern in tests or style-auditing tools.

```cs
using AngleSharp;

var html = @"<!doctype html>
<style>
  .chip { color: rgb(255, 255, 255); background-color: rgb(39, 174, 96); }
</style>
<span class='chip' id='chip'>ok</span>";

var context = BrowsingContext.New(Configuration.Default.WithCss());
var document = await context.OpenAsync(req => req.Content(html));

var chip = document.QuerySelector("#chip");
var computed = chip.ComputeStyle();

Console.WriteLine(computed.GetPropertyValue("color"));
Console.WriteLine(computed.GetPropertyValue("background-color"));
```

## 5. Parse CSS Asynchronously From Stream

```cs
using AngleSharp;
using AngleSharp.Css.Parser;
using System.Text;

var css = ".panel { margin: 1rem; }";
await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(css));

var context = BrowsingContext.New(Configuration.Default.WithCss());
var parser = context.GetService<ICssParser>();
var sheet = await parser.ParseStyleSheetAsync(stream);

Console.WriteLine(sheet.Rules.Length);
```

## 6. Load External Stylesheets Through The Loader

```cs
using AngleSharp;
using AngleSharp.Io;

var config = Configuration.Default
	.WithPageRequester(enableResourceLoading: true)
	.WithCss();

var context = BrowsingContext.New(config);
var document = await context.OpenAsync("https://example.org");

foreach (var styleSheet in document.StyleSheets)
{
	Console.WriteLine(styleSheet.Href ?? "<inline>");
}
```

## 7. Keep Unknown Declarations For Diagnostics

Useful if you are building compatibility reports and do not want unknown declarations dropped.

```cs
using AngleSharp;
using AngleSharp.Css.Parser;

var options = new CssParserOptions
{
	IsIncludingUnknownDeclarations = true,
	IsIncludingUnknownRules = true,
};

var config = Configuration.Default.WithCss(options);
var context = BrowsingContext.New(config);
var parser = context.GetService<ICssParser>();

var sheet = parser.ParseStyleSheet(".x { --token: 4px; unknown-prop: ???; }");
Console.WriteLine(sheet.Rules.Length);
```

## 8. Batch Update Inline Properties

```cs
var el = document.QuerySelector("#status");
var style = el.GetStyle();

var updates = new Dictionary<string, string>
{
	["display"] = "inline-flex",
	["align-items"] = "center",
	["gap"] = "6px",
};

foreach (var pair in updates)
{
	style.SetProperty(pair.Key, pair.Value);
}
```

## 9. Typical Console Tool Skeleton

```cs
using AngleSharp;
using AngleSharp.Css.Parser;

var context = BrowsingContext.New(Configuration.Default.WithCss());
var parser = context.GetService<ICssParser>();

foreach (var file in Directory.EnumerateFiles("./styles", "*.css", SearchOption.AllDirectories))
{
	var css = await File.ReadAllTextAsync(file);
	var sheet = await parser.ParseStyleSheetAsync(css);
	Console.WriteLine($"{file}: {sheet.Rules.Length} rules");
}
```

## 10. Preserve Comments When Serializing

AngleSharp.Css can preserve parsed CSS comment trivia when you serialize the stylesheet again.

```cs
using AngleSharp;
using AngleSharp.Css.Dom;
using System.Linq;

var source = "/* before */ h1 { color: red; /* keep */ }";
var context = BrowsingContext.New(Configuration.Default.WithCss());
var document = await context.OpenAsync(req => req.Content($"<style>{source}</style>"));

var sheet = document.StyleSheets.OfType<ICssStyleSheet>().First();
var serialized = sheet.ToCss(new CssSerializationOptions { PreserveComments = true });

Console.WriteLine(serialized);
```

This preserves comments, but not their exact original positions in every case. Depending on where a comment was placed, it may be moved when the stylesheet is serialized again.

## 11. Where To Go Next

- Read [API Documentation](01-API.md) for deeper CSSOM details.
- Read [Render Tree Examples](03-Render-Tree.md) for style-aware tree traversal and resource download workflows.
- Read [FAQ](04-Questions.md) for serializer behavior and common edge cases.
