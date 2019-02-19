#!/bin/sh
remote=$1
if [ -z "$remote" ]; then
  remote='origin'
fi
docsDir='_site'
rm -rf ../$docsDir
docfx 
cd ../
git add $docsDir
git subtree push --prefix $docsDir $remote gh-pages
git rm -rf $docsDir && rm -rf $docsDir
