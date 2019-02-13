paket='.paket/paket.sh'
project='Axle.FSharp'

rm -rf paket-files/ && $paket update && rm -rf obj/ && dotnet restore $project.fsproj
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi
$paket simplify