msbuild="../../../../submodules/btw/msbuild.sh"
paket='.paket/paket.sh'
project='Axle.Application'

#./restore.sh

$msbuild $project.csproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
$msbuild $project.dist.csproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi