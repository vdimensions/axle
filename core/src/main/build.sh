if [ -z "./packages" ]; then
  ./paket.sh update
fi

msbuild="../../../submodules/btw/msbuild.sh"

# clean-up obj directory to remove .net standard/core files (project.json) which cause issues with legacy framework version build
rm -rf obj

netsframeworkProjects=( \
"Axle.Core.net45.csproj" \
"Axle.Core.net40-client.csproj" \
"Axle.Core.net35.csproj" \
);

for p in "${netsframeworkProjects[@]}"; do
  $msbuild $p
done

netstandardProjects=( \
"Axle.Core.netstandard16.csproj" \
"Axle.Core.netstandard15.csproj" \
"Axle.Core.netstandard14.csproj" \
"Axle.Core.netstandard13.csproj" \
"Axle.Core.netstandard12.csproj" \
"Axle.Core.netstandard11.csproj" \
"Axle.Core.netstandard10.csproj" \
);

for p in "${netstandardProjects[@]}"; do
  rm -rf obj
  dotnet restore $p
  $msbuild $p
done

rm -rf obj

# pack
$msbuild Axle.Core.dist.csproj