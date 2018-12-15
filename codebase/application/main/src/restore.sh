paket='.paket/paket.sh'
project='Axle.Application'

$paket update
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi

rm -rf obj/
dotnet restore $project.csproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
$paket simplify