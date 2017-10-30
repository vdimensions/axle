./paket.sh update

msbuild="../../../submodules/btw/msbuild.sh"

$msbuild Axle.Core.netstandard16.csproj
$msbuild Axle.Core.netstandard15.csproj
$msbuild Axle.Core.netstandard14.csproj
$msbuild Axle.Core.netstandard13.csproj
$msbuild Axle.Core.netstandard12.csproj
$msbuild Axle.Core.netstandard11.csproj
$msbuild Axle.Core.netstandard10.csproj

# clean-up obj directory to remove .net standard/core files (project.json) which cause issues with legacy framework version build
rm -rf obj

$msbuild Axle.Core.net45.csproj
$msbuild Axle.Core.net40-client.csproj
$msbuild Axle.Core.net35.csproj

rm -rf obj

# pack
$msbuild Axle.Core.dist.csproj