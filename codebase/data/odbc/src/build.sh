paket='.paket/paket.sh'
project='Axle.Data.Odbc'
project_format='csproj'

./restore.sh

dotnet clean $project.$project_format && dotnet build $project.$project_format && dotnet pack $project.$project_format --no-build --no-restore
if [ $? -ne 0 ]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi