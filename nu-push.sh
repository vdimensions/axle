cd dist
for file in $(ls *.nupkg); do
  nuget push $file -Source http://localhost/nuget.vdimensions.com/ VDimensions
  echo "----------------"
  nuget push $file -Source http://nuget.vdimensions.net/nuget-repo/ VDimensions
  echo "----------------"
done
