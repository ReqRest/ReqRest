using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    
    public static int Main () => Execute<Build>(x => x.FullBuild);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath ArtifactsDirectory => RootDirectory / ".artifacts";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            DotNetClean(s => s .SetProject(Solution));
            EnsureCleanDirectory(ArtifactsDirectory);
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Clean, Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetLogger("trx")
                .SetLogOutput(true)
                .SetResultsDirectory(ArtifactsDirectory / "tests"));
        });

    Target InstallInheritDoc => _ => _
        .Executes(() => 
        {
            // "dotnet tool update" will install if the tool is not found.
            // Not using "dotnet tool install", because that would return a non-zero exit code if the
            // tool is already installed.
            DotNetToolUpdate(s => s
                .EnableGlobal()
                .SetArgumentConfigurator(c => c.Add("--ignore-failed-sources"))
                .SetPackageName("InheritDocTool"));
        });

    Target RunInheritDoc => _ => _
        .DependsOn(InstallInheritDoc, Compile)
        .Executes(() => 
        {
            ProcessTasks
                .StartProcess("InheritDoc", arguments: $"--overwrite --base {Solution.Directory}")
                .AssertZeroExitCode();
        });

    Target Pack => _=> _
        .DependsOn(Test, RunInheritDoc)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetOutputDirectory(ArtifactsDirectory / "packages"));
        });

    Target FullBuild => _ => _
        .DependsOn(Pack);

}
