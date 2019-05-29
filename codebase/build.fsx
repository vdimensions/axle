#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.DotNet.Paket
nuget Fake.Net.Http
nuget Fake.Core.Target //"

//#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Net

let paketVersion = "5.207.3"

let nugetApiKey =  Environment.environVarOrFail "NUGET_ORG_VDIMENSIONS_API_KEY"
let nugetServer = "https://www.nuget.org/api/v2/package"

let projectLocations = [
    "core/main"
    "core/fsharp"
    "resources/main"
    "resources/java"
    "resources/yaml"
    "data/main"
    "data/fsharp"
    //"data/resources"
    "data/sql_client"
    "data/odbc"
    "data/oledb"
    "data/npgsql"
    "data/mysql"
    "data/sqlite"
    "caching/main"
    "logging/log4net"
    "security/main"
    "security/cryptography"
    "application/main"
    "web/main"
    "web/websharper"
]

let dir_exists = DirectoryInfo.ofPath >> DirectoryInfo.exists

let nupkg_map fn = 
    !!"../dist/*.nupkg"
    |> Seq.map fn
    |> List.ofSeq

let paket command =
    let paketEndpoint = sprintf "https://github.com/fsprojects/Paket/releases/download/%s/paket.exe" paketVersion
    let paketDir = Path.combine (Shell.pwd()) ".paket"
    let paketExe = Path.combine paketDir "paket.exe"
    if not (File.exists paketExe) then
        Trace.trace "downloading Paket"
        Http.downloadFile paketExe paketEndpoint
        |> ignore
    else
        Trace.trace "Paket already exists"
    Trace.trace "Installing dependencies"
    // run "paket.exe"
    Command.RawCommand(paketExe, Arguments.OfArgs [command])
    |> CreateProcess.fromCommand
    // use mono if linux
    |> CreateProcess.withFramework
    // throw an error if the process fails
    |> CreateProcess.ensureExitCode
    |> Proc.run
    |> ignore
    ()

let msbuild command arg =
    let msbuildExe = "msbuild"
    // run "msbuild.exe"
    Command.RawCommand(msbuildExe, Arguments.OfArgs [command; arg])
    |> CreateProcess.fromCommand
    // use mono if linux
    |> CreateProcess.withFramework
    // throw an error if the process fails
    |> CreateProcess.ensureExitCode
    |> Proc.run
    |> ignore
    ()

let clean () =
    let objPath = Path.combine (Shell.pwd()) "obj"
    let paketFiles = Path.combine (Shell.pwd()) "pake-files"
    Shell.rm_rf objPath
    Shell.rm_rf paketFiles
    Shell.mkdir objPath

let cleanNupkg = (fun _ -> nupkg_map (fun nupkg -> Trace.tracefn "NUPK CLEAN: %s" nupkg; Shell.rm_rf nupkg) |> ignore)


open Fake.DotNet
open Fake.DotNet.NuGet

let dotnet_sdk = lazy DotNet.install DotNet.Versions.Release_2_1_4
//let inline dotnet op = DotNet.exec (DotNet.Options.lift dotnet_sdk.Value) op
let inline dotnet op cmd = DotNet.exec op cmd
let inline dotnet_clean op arg = dotnet op "clean" arg
let inline dotnet_restore op arg = dotnet op "restore" arg
let inline dotnet_build op arg = dotnet op "build" arg
let inline dotnet_pack op arg = dotnet op "pack" arg
let inline dotnet_test op arg = dotnet op "test" arg
let inline dotnet_nuget op arg = dotnet op "nuget" arg
//    DotNet.exec (DotNet.Options.lift dotnet_sdk.Value) "restore" arg
//let inline build arg =
//    DotNet.exec dotnet "build" arg

let dotnet_push op server apiKey =
    let result =
        nupkg_map (
            fun nupkg ->
                Trace.tracefn "Publishing nuget package: %s" nupkg
                //(nupkg, dotnet_nuget op (sprintf "push %s --source %s --api-key %s" nupkg server apiKey))
                //nupkg
                nupkg, (dotnet_nuget op (sprintf "push %s --source %s --api-key %s" nupkg server apiKey))
            )
        |> Seq.filter (fun (_, p) -> p.ExitCode <> 0)
        |> List.ofSeq

    match result with
    | [] -> ()
    | failedAssemblies ->
        failedAssemblies
        |> List.map (fun (nuget, proc) -> sprintf "Failed to push NuGet package '%s'. Process finished with exit code %d." nuget proc.ExitCode)
        |> String.concat System.Environment.NewLine
        |> exn
        |> raise

let dotnet_options = (fun (op : DotNet.Options) -> { op with Verbosity = Some DotNet.Verbosity.Quiet })

let build op =
    let dir = Shell.pwd()
    match !!(sprintf "%s/*.fsproj" dir) |> List.ofSeq with
    | [] | [_] -> 
        Trace.tracefn "Performing dotnet build for all projects in %s" dir
        dotnet_build op "" |> ignore
    //| [singleFsProj] ->
    //    Trace.tracefn "Performing msbuild Build for %s in %s" singleFsProj dir
    //    // this is an F# project. dotnet build fails to produce valid metadata in the dll, we should use MSBuild instead (but not dotnet msbuild).
    //    msbuild "Build" singleFsProj
    | _ ->
        invalidOp "Multiple project files are not supported by this script. Please, make sure you have a single msbuild file in the directory of the given project."

let createDynamicTarget location =
    let targetName = location
    Target.create targetName (fun _ ->
        let codeDir = sprintf "%s/src" location
        let testsDir = sprintf "%s/tests" location
        if (dir_exists codeDir) then
            Shell.pushd (codeDir)
            let dir = Shell.pwd()
            try
                Trace.tracefn "Project to build %s" dir
                clean ()
                paket "update" |> ignore
                dotnet_clean dotnet_options "" |> ignore
                dotnet_restore dotnet_options "" |> ignore
                build dotnet_options
                dotnet_pack dotnet_options "" |> ignore
                ()
            finally 
                Shell.popd()
        else ()

        if (dir_exists testsDir) then
            Shell.pushd (testsDir)
            let dir = Shell.pwd()
            try
                Trace.tracefn "Test project to build %s" dir
                clean ()
                paket "update" |> ignore
                dotnet_clean dotnet_options "" |> ignore
                dotnet_restore dotnet_options "" |> ignore
                dotnet_build dotnet_options "" |> ignore
                dotnet_test dotnet_options "" |> ignore
                ()
            finally 
                Shell.popd()
        else 
            Trace.traceImportantfn "Project '%s' does not have tests" location
            ()
    )
    targetName



Target.create "Prepare" cleanNupkg
Target.create "Default" cleanNupkg
Target.create "Publish" (fun _ -> ())//(fun _ -> dotnet_push dotnet_options nugetServer nugetApiKey)

open Fake.Core.TargetOperators

"Prepare" ==> "Publish" ==> "Default"

projectLocations 
|> List.map createDynamicTarget
|> List.fold (fun a b -> Trace.tracefn "target %s" a; b ==> a) "Publish"

Target.runOrDefault "Default"