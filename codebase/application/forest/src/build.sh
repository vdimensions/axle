msbuild="../../../../submodules/btw/msbuild.sh"
project='Axle.Forest'

./restore.sh

$msbuild $project.fsproj
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
