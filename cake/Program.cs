using System.IO;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Octokit;
using System;
using System.Linq;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.MSBuild;


public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

//[TaskName("Clean task")]
//public sealed class Clean : FrostingTask<BuildContext>
//{
//    public override void Run(BuildContext context)
//    {
//        context.Clean();
//    }
//}

//[TaskName("BuildLibraries task")]
//[IsDependentOn(typeof(Clean))]
//public sealed class BuildLibraries : FrostingTask<BuildContext>
//{
//    public override void Run(BuildContext context)
//    {
//        foreach (var solution in context.Libraries.Select(p => context.GetLibraryFilteredSolution(p)))
//        {            
//            context.MSBuild(solution, new MSBuildSettings() { Configuration = "Release" });
//        }
//    }
//}

[TaskName("PushTcOpenGroupPackages task")]
// [IsDependentOn(typeof(BuildLibraries))]
public sealed class PushTcOpenGroupPackages : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {        
        foreach (var nugetFile in Directory.EnumerateFiles(context.ArtifactsFolder, "*.nupkg").Select(p => new FileInfo(p)))
        {
            context.DotNetNuGetPush(nugetFile.FullName, new Cake.Common.Tools.DotNet.NuGet.Push.DotNetNuGetPushSettings()
            {
                Source = "https://nuget.pkg.github.com/TcOpenGroup/index.json",
                ApiKey = System.Environment.GetEnvironmentVariable("TC_OPEN_GROUP_USER_PAT"),  
                SkipDuplicate = true
            });
        }
    }
}

[TaskName("Release task")]
[IsDependentOn(typeof(PushTcOpenGroupPackages))]
public sealed class ReleaseTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        
        {
            var githubToken = context.Environment.GetEnvironmentVariable("TC_OPEN_GROUP_USER_PAT");
            var githubClient = new GitHubClient(new ProductHeaderValue("TcOpen"));
            githubClient.Credentials = new Credentials(githubToken);

            var doesExists = githubClient.Repository.Release.GetAll("TcOpenGroup", "TcOpen", ApiOptions.None).Result.Any(p => p.Name == GitVersionInformation.SemVer);
            if (!doesExists)
            {
                var release = githubClient.Repository.Release.Create(
                    "TcOpenGroup",
                    "TcOpen",
                    new NewRelease($"{GitVersionInformation.SemVer}")
                    {
                        Name = $"{GitVersionInformation.SemVer}",
                        TargetCommitish = GitVersionInformation.Sha,
                        Body = $"Release v{GitVersionInformation.SemVer}",
                        Draft = true,
                        Prerelease = true
                    }
                ).Result;
            }

            //foreach (var artifact in Directory.EnumerateFiles(context.ArtifactsFolder, "*.nupkg").Select(p => new FileInfo(p)))
            //{
            //    var asset = new ReleaseAssetUpload(artifact.Name, "application/nupkg", new StreamReader(artifact.FullName).BaseStream, TimeSpan.FromSeconds(3600));
            //    githubClient.Repository.Release.UploadAsset(release, asset).Wait();
            //}            
        }
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(ReleaseTask))]
public class DefaultTask : FrostingTask
{
}