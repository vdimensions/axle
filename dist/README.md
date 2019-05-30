# What is this directory used for?

This directory is used to hold nuget packages produced during build. Every project is configured to create a nuget package there when `dotnet pack` is executed.

## User workflow

If you have checked out the project, we encourage you to use this folder to resolve Axle dependencies before falling back to the official NuGet feeds. That way the different projects herein will reference the latest packages with your changes. 

## CI Builds

During a CI build the directory will contain nuget packages exclusively produced by that same CI build. The projects in this repository will resolve Axle dependencies from this directory, meaning that all Axle dependencies resolved will be a product of the current build. 

This will ensure that **the entire CI build will *fail*** if there are changes in one Axle project which result in failure to build another Axle project.
