#addin "Newtonsoft.Json"
#addin "Cake.Powershell&version=0.3.5"
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
    .Does(() =>
{
    NuGetRestore(solutionFile);
});


Task("Build")
    .IsDependentOn("Clean")
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
    
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
