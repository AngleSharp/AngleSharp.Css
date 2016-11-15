![logo](https://raw.githubusercontent.com/AngleSharp/AngleSharp.Css/master/header.png)

AngleSharp.Css
====================

AngleSharp.Css extends the core AngleSharp library with some more powerful CSS capabilities. This repository is the home of the source for the AngleSharp.Css NuGet package.

Current status of this repository:

[![Build status](https://img.shields.io/appveyor/ci/FlorianRappl/AngleSharp-Css.svg?style=flat-square)](https://ci.appveyor.com/project/FlorianRappl/AngleSharp-Css)
[![Issues open](https://img.shields.io/github/issues/AngleSharp/AngleSharp.Css.svg?style=flat-square)](https://github.com/AngleSharp/AngleSharp.Css/issues)

:warning: **WARNING** :warning:

The library is currently under construction and is not ready yet. The initial version will be released soon. This message will then be removed.

Advantages of AngleSharp.Css
----------------------------

The core library already contains the CSS selector parser and the most basic classes and interfaces for dealing with the CSSOM. AngleSharp.Css brings the following advantages and use cases to life:

* Correct identification of edge cases
* A live CSSOM model, i.e., callbacks and everything
* Computing the styling of certain elements
* Cascades of stylesheets
* Validation (and property-based exposure) of CSS declarations
* Responsive design considerations

The main idea behind AngleSharp.Css is to expose the CSSOM as it would be in the browser (and potentially beyond, i.e., useful for being used by editors). Originally, most of the code found here was embedded in the AngleSharp.Core library, however, due to the overhead for HTML use cases it was decided to transfer the code into its own repository.

Participating
-------------

Participation in the project is highly welcome. For this project the same rules as for the AngleSharp core project may be applied.

If you have any question, concern, or spot an issue then please report it before opening a pull request. An initial discussion is appreciated regardless of the nature of the problem.

Some legal stuff
----------------

The MIT License (MIT)

Copyright (c) 2016 AngleSharp

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
