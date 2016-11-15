﻿namespace AngleSharp.Css.Tests.Mocks
{
    using AngleSharp.Io;
    using AngleSharp.Scripting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class MockScriptEngine : IScriptEngine
    {
        public MockScriptEngine(Action<ScriptOptions> callback, String type = null)
        {
            Callback = callback;
            Type = type ?? "c-sharp";
        }

        public String Type
        {
            get;
            private set;
        }

        public Action<ScriptOptions> Callback
        {
            get;
            private set;
        }

        public Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel)
        {
            Callback?.Invoke(options);

            return Task.FromResult(true);
        }
    }
}
