var target = Argument("target", "Default");

Task("Restore-NuGet-Packages")
    .Does(() =>
    {
        NuGetRestore("../source/PrtgSensorSharp.sln");
    });

Task("Clean")
    .Does(() =>
    {
        CleanDirectories("../source/**/bin");
        CleanDirectories("../source/**/obj");
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        MSBuild("../source/PrtgSensorSharp.sln", new MSBuildSettings
        {
            Configuration = "Release",
            Verbosity = Verbosity.Quiet
        });
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        NUnit3("../source/PrtgSensorSharp.Tests/bin/Release/PrtgSensorSharp.Tests.dll");
    });

// todo: push nuget package
// todo: versioning

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);
