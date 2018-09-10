msbuild="../../../../submodules/btw/msbuild.sh"
paket='.paket/paket.sh'
project='Axle.FSharp'

rm -rf obj/
$paket update
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi

dotnet restore $project.fsproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
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