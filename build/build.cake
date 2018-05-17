var configuration = "Release";

Task("Build")
    .Does(() =>
    {
		var solutionPath = "../source";

        DotNetCoreRestore(solutionPath);

        DotNetCoreClean(solutionPath, new DotNetCoreCleanSettings 
        {
            Configuration = configuration
        });
 
        DotNetCoreBuild(solutionPath, new DotNetCoreBuildSettings
        {
            Configuration = configuration
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var testPath = "../source/PrtgSensorSharp.Tests"; 

		DotNetCoreTest(testPath, new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true
        });
    });

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
	{
        CleanDirectories("./artifacts");

        var version = EnvironmentVariable("APPVEYOR_BUILD_VERSION") ?? "1.0.0";
        var path = "../source/PrtgSensorSharp";

		DotNetCorePack(path, new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = "./artifacts",
            NoBuild = true,
            NoRestore = true,
            ArgumentCustomization = args => {
                return args
                    .Append("/p:Version={0} ", version)
                    .Append("/p:AssemblyVersion={0} ", version)
                    .Append("/p:FileVersion={0} ", version);
            }
        });
	});

Task("Default")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

RunTarget(Argument("target", "Default"));
