using System.IO;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Octokit;
using System;
using System.Linq;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.DotNet;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

public class BuildContext : FrostingContext
{    
    public string ArtifactsFolder { get; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        ArtifactsFolder = Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//nugets"));
    }
}

[TaskName("PushTcOpenGroupPackages task")]
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