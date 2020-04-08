var target = Argument("target", "Default");
var projectName = "AngleSharp.Css";
var solutionName = "AngleSharp.Css";
var frameworks = new Dictionary<String, String>
{
    { "net46", "net46" },
    { "net461", "net461" },
    { "net472", "net472" },
    { "netstandard1.3", "netstandard1.3" },
    { "netstandard2.0", "netstandard2.0" },
};

#load tools/anglesharp.cake

RunTarget(target);
