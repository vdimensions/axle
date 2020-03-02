msbuild="../../../../submodules/btw/msbuild.sh"
project_name='Axle.FSharp'
project_ext='.fsproj'
project=${project_name}${project_ext}

./restore.sh

dotnet clean ${project} && ${msbuild} ${project} && dotnet pack ${project} --no-build
if [[ $? -ne 0 ]]; then
  read -rsp "Press [Enter] to quit"
  echo ""
  exit
fi