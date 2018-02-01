if [ -z "./packages" ]; then
  ./paket.sh update
fi

msbuild="../../../submodules/btw/msbuild.sh"

# clean-up obj directory to remove .net standard/core files (project.json) which cause issues with legacy framework version builds
rm -rf obj/
dotnet restore Axle.Resources.Java.csproj
$msbuild Axle.Resources.Java.csproj > buildlog.txt
