if [ -z "./packages" ]; then
  ./paket.sh update
fi

msbuild="../../../submodules/btw/msbuild.sh"

# clean-up obj directory to remove .net standard/core files (project.json) which cause issues with legacy framework version builds
rm -rf obj/
dotnet restore Axle.Core.csproj
$msbuild Axle.Core.csproj /tv:15.0
# pack
#$msbuild Axle.Core.dist.csproj
