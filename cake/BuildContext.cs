using Cake.Core;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class BuildContext : FrostingContext
{
    public string ArtifactsFolder { get; }

    public string SourceDirectory { get; } 

    public void Clean()
    {
        // Clean directories
        Directory.EnumerateDirectories(SourceDirectory, "bin", SearchOption.AllDirectories)
        .Concat(Directory.EnumerateDirectories(SourceDirectory, "obj", SearchOption.AllDirectories))
        .Concat(Directory.EnumerateDirectories(SourceDirectory, "_Boot", SearchOption.AllDirectories))
        .Concat(Directory.EnumerateDirectories(SourceDirectory, "_CompileInfo", SearchOption.AllDirectories))
        .Concat(Directory.EnumerateDirectories(SourceDirectory, "_generated", SearchOption.AllDirectories))
        .Concat(Directory.EnumerateDirectories(SourceDirectory, "_meta", SearchOption.AllDirectories))
        .ToList().ForEach(dir => { Directory.Delete(dir, true); });
    }

    public IEnumerable<string> Libraries { get; } = new List<string> 
    {
       // "TcoAbstractions",
        "TcoUtilities",
        "TcoCore",        
        "TcoData",
        "TcoDrivesBeckhoff",
        "TcoElements",
        "TcoInspectors",
        "TcoIo",
        "TcoPneumatics",
        "TcoUtilities"
    };
    
    public string GetLibraryRootFolder(string library)
    {
        return Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//src", library));
    }

    public string GetLibraryFilteredSolution(string library)
    {
        return Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//src", library, $"{library}.slnf"));
    }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        ArtifactsFolder = Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//nugets"));
        SourceDirectory = Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//src"));
    }


}
