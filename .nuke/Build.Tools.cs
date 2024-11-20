using Nuke.Common.Tooling;

partial class Build
{
    [NuGetPackage(
        packageId: "vpk",
        packageExecutable: "vpk.dll",
        Version = "0.0.942"
    )]
    readonly Tool Vpk;
}
