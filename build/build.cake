var target = Argument("target", "Default");

Task("Restore-NuGet-Packages")
    .Does(() =>
    {
        NuGetRestore("../PrtgExeScriptSensor/PrtgExeScriptSensor.sln");
    });

Task("Clean")
    .Does(() =>
    {
        CleanDirectories("../PrtgExeScriptSensor/**/bin");
        CleanDirectories("../PrtgExeScriptSensor/**/obj");
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        MSBuild("../PrtgExeScriptSensor/PrtgExeScriptSensor.sln", new MSBuildSettings
        {
            Configuration = "Release",
            Verbosity = Verbosity.Quiet
        });
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        NUnit3("../PrtgExeScriptSensor/PrtgExeScriptSensor.Tests/bin/Release/PrtgExeScriptSensor.Tests.dll");
    });

// todo: push nuget package
// todo: versioning

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);
