project='Axle.Core'

./restore.sh

dotnet clean $project.csproj && dotnet build $project.csproj && dotnet pack $project.csproj --no-build
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi