dotnet pack $PSScriptRoot --output "build-package"
dotnet nuget push "./build-package/henry-js.nuke.build.cli.base.1.0.0.nupkg" -k $env:NUGET_API_KEY -s https://api.nuget.org/v3/index.json