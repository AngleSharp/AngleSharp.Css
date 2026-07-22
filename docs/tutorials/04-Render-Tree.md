---
title: "Render Tree"
section: "AngleSharp.Css"
---
# Render Tree Examples

This tutorial shows practical workflows using the document render tree.

## What The Render Tree Gives You

The render tree combines DOM nodes with style information:

- each `ElementRenderNode` contains `SpecifiedStyle` and `ComputedStyle`,
- children preserve structure for style-aware traversal,
- helper extensions allow visibility checks and resource download.

## 1. Build A Render Tree

```cs
using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Css.RenderTree;

var config = Configuration.Default
    .WithCss()
    .WithRenderDevice();

var document = "<style>.box { color: green; font-size: 1.5rem; }</style><div class='box'>Hello</div>"
    .ToHtmlDocument(config);

var tree = document.DefaultView!.Render();
var box = document.QuerySelector(".box")!;
var boxNode = tree.Find(box) as ElementRenderNode;

Console.WriteLine(boxNode!.ComputedStyle.GetColor());
Console.WriteLine(boxNode.ComputedStyle.GetFontSize());
```

## 2. Compare Specified And Computed Style

```cs
var document = "<style>.parent { color: green; } .child { color: inherit; }</style><div class='parent'><span class='child'>Item</span></div>"
    .ToHtmlDocument(Configuration.Default.WithCss().WithRenderDevice());

var tree = document.DefaultView!.Render();
var child = document.QuerySelector(".child")!;
var childNode = tree.Find(child) as ElementRenderNode;

Console.WriteLine(childNode!.SpecifiedStyle.GetColor());
Console.WriteLine(childNode.ComputedStyle.GetColor());
```

## 3. Traverse Visible Nodes Only

```cs
using AngleSharp.Css.RenderTree;

var stack = new Stack<IRenderNode>();
stack.Push(tree);

while (stack.Count > 0)
{
    var current = stack.Pop();

    if (!current.IsVisible())
    {
        continue;
    }

    if (current is ElementRenderNode elementNode)
    {
        Console.WriteLine(elementNode.Ref.TagName);
    }

    foreach (var childNode in current.Children)
    {
        stack.Push(childNode);
    }
}
```

## 4. Download Referenced CSS Resources

`DownloadResources()` walks the visible render subtree and fetches resource URLs used by:

- `background-image`,
- `border-image-source`,
- `list-style-image`,
- `cursor`.

```cs
using AngleSharp;
using AngleSharp.Css.RenderTree;
using AngleSharp.Io;

var loaderOptions = new LoaderOptions
{
    IsResourceLoadingEnabled = true,
};

var config = Configuration.Default
    .WithDefaultLoader(loaderOptions)
    .WithRenderDevice()
    .WithCss();

var document = "<style>div { background-image: url('https://example.com/bg.png'); }</style><div></div>"
    .ToHtmlDocument(config);

var tree = document.DefaultView!.Render();
await tree.DownloadResources();
```

## 5. Render With A Custom Device Profile

```cs
var device = new DefaultRenderDevice
{
    Category = DeviceCategory.Screen,
    ViewPortWidth = 1024,
    ViewPortHeight = 768,
    FontSize = 16,
};

var tree = document.DefaultView!.Render(device);
```

## Notes

- Render-tree APIs are useful for analysis and tooling scenarios, not full browser layout.
- For final user-visible rendering, you still need a rendering engine.
