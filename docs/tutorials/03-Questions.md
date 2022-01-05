---
title: "Questions"
section: "AngleSharp.Css"
---
# Frequently Asked Questions

## How to change the color output?

By default, AngleSharp.Css uses `rgba()` for the serialization of `Color`. To change this you can set

```cs
Color.UseHex = true;
```

which will automatically use hex for all non-transparent colors. All other colors would still be represented via the `rgba()` function.

So you'd get:

```cs
Color.UseHex = true;
var color1 = new Color(65, 12, 48);
// color1.CssText = #410C30
var color2 = new Color(65, 12, 48, 10);
// color2.CssText = rgba(65, 12, 48, 0.04)
```
