project='Axle.Data.Sqlite.Microsoft'
project_format='csproj'

dotnet restore $project.$project_format
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi