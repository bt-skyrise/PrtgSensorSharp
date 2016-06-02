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

Task("Create-NuGet-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    var nuGetPackSettings = new NuGetPackSettings {
        Id                      = "PrtgSensorSharp",
        Version                 = EnvironmentVariable("APPVEYOR_BUILD_VERSION"),
        Title                   = "PrtgSensorSharp",
        Authors                 = new[] {"Ryszard Tarajkowski"},
        Owners                  = new[] {"BT Skyrise"},
        Description             = "Creating custom PRTG sensors for .NET.",
        ProjectUrl              = new Uri("https://github.com/bt-skyrise/PrtgSensorSharp"),
        LicenseUrl              = new Uri("https://github.com/bt-skyrise/PrtgSensorSharp/blob/master/LICENSE.txt"),
        Copyright               = "Ryszard Tarajkowski 2016",
        RequireLicenseAcceptance= false,
        Symbols                 = false,
        NoPackageAnalysis       = true,
        Files                   = new[] { new NuSpecContent {Source = "bin/Release/PrtgSensorSharp.dll", Target = "lib/net452"} },
        BasePath                = "../source/PrtgSensorSharp",
        OutputDirectory         = "."
    };
    
    NuGetPack(nuGetPackSettings);
});

Task("Push-NuGet-Package")
    .IsDependentOn("Create-NuGet-Package")
    .Does(() =>
{
    var package = "./PrtgSensorSharp." + EnvironmentVariable("APPVEYOR_BUILD_VERSION") +".nupkg";
    
    NuGetPush(package, new NuGetPushSettings {
        Source = "https://nuget.org/",
        ApiKey = EnvironmentVariable("NUGET_API_KEY")
    });
});

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);
