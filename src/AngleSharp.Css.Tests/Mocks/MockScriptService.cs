﻿namespace AngleSharp.Css.Tests.Mocks
{
    using AngleSharp.Scripting;
    using System;

    sealed class MockScriptService<T> : IScriptingProvider
        where T : IScriptEngine
    {
        private readonly T _engine;

        public MockScriptService(T engine)
        {
            _engine = engine;
        }

        public IScriptEngine GetEngine(String mimeType)
        {
            return _engine;
        }
    }
}
