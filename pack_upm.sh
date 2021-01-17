dirs=(
    "codebase/application/main"
    "codebase/application/unity"
    "codebase/caching/main"
    "codebase/configuration/main"
    "codebase/core/main"
    "codebase/resources/main"
    "codebase/resources/properties"
    "codebase/resources/yaml"
    "codebase/text.documents/main"
    "codebase/text.documents/properties"
    "codebase/text.documents/xml"
    "codebase/text.documents/yaml"
)
base_dir=`pwd`
version="1.10.2"
registry="http://192.168.1.178:8373"

for dir in ${dirs[@]}; do
  cd "$base_dir/$dir"
  npm version $version
  npm publish --registry $registry
  cd $base_dir
done