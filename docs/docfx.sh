#
# build code documentations first
#
for docfx_file in $(ls ../codebase/*/*/docfx.json); do
  proj_dir=${docfx_file%docfx.json}
  tmp=${proj_dir##../codebase/}
  xref_dir="xref/api/${tmp%/}"
  xref_file="${xref_dir}/xrefmap.yml"
  #
  # link the original docs folder here to have naviation work across the combined site
  #
  eval "cp -r ../docs \"${proj_dir}.docs\""
  if [ $? -ne 0 ]; then 
    echo "error"
    exit 
  fi
  #
  # for every child library first we build its own source code documentation
  #
  docfx $docfx_file
  if [ $? -ne 0 ]; then 
    echo "error"
    exit 
  fi
  #
  # then we copy that projects' xref map to the main docs folde to be referenced in the main documentation
  #
  mkdir -p $xref_dir
  mv -f ../_site/xrefmap.yml $xref_file
  # fix api paths in exported xref file to be usabe in the context of the main doc
  sed -i -e 's/api\///g' $xref_file
  sed -i -e 's/\.docs\///g' $xref_file
  #
  # and then we cleanup
  #
  eval "rm -rf ${docfx_file%docfx.json}obj"
  rm -rf ../_site/xrefmap.yml
  rm -rf ../_site/.docs
  rm -rf "${proj_dir}.docs"
done

# build main documentation now
docfx