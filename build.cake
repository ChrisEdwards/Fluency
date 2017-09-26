#addin "Newtonsoft.Json"
#addin "Cake.Powershell&version=0.3.5"
#tool "nuget:?package=xunit.runner.console&version=2.2.0"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Debug");
var name = Argument<string>("name", "Fluency");
var verbosity = Argument<string>("verbosity", "Minimal");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var solutionDir = Directory("./src");
var solutionFile = solutionDir + File("Fluency.NET.sln");
var buildDir = solutionDir + Directory("bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////


Task("Clean")
    .Does(() =>
{
    var path = solutionDir.Path;
    Information("Cleaning build output", path);
    CleanDirectories(path + "/**/bin/" + configuration);
    CleanDirectories(path + "/**/obj/" + configuration);
});


Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});


Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild(solutionFile, settings => settings
        .SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Quiet));
});


Task("Run-Unit-Tests")
    .Does(() =>
{
    var testAssemblies = GetFiles("./test/**/bin/" + configuration + "/*.Tests.dll");
    XUnit2(testAssemblies, new XUnit2Settings {
        Parallelism = ParallelismOption.All,
        HtmlReport = false,
        NoAppDomain = true
    });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests");
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
