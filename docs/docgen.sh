#!/bin/sh
remote=$1
if [ -z "$remote" ]; then
  remote='origin'
fi
docsDir='_site'
currentBranch=$(git symbolic-ref --short HEAD)

echo "Generating dedicated branch for documentation"
git checkout -B docgen

rm -rf ../$docsDir
docfx 
cd ../

echo "Committing generated documentation in brach 'docgen'"
git add $docsDir -f && git commit -m "Pushing docfx generated documentation"
#git subtree push --prefix $docsDir $remote gh-pages

#echo "Pushing subtree with documentation from $remote:docgen into $remote:gh-pages branch"
#git push $remote `git subtree split --prefix $docsDir docgen 2> /dev/null`:gh-pages --force
git branch -D gh-pages
git subtree push --prefix $docsDir gh-pages && git checkout gh-pages
#git branch gh-pages && git checkout gh-pages
git push $remote gh-pages --force

echo "Restoring current branch $currentBranch"
git checkout $currentBranch

echo "Deleting docgen branch"
git branch -D docgen
 