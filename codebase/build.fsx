#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.DotNet.Paket
nuget Fake.Net.Http
nuget Fake.Core.Target //"

//#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.IO
open Fake.Net

let paketVersion = "5.207.3"

let projectLocations = [
    "core/main"
    "core/fsharp"
]

let dir_exists = DirectoryInfo.ofPath >> DirectoryInfo.exists

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

let clean () =
    let objPath = Path.combine (Shell.pwd()) "obj"
    let paketFiles = Path.combine (Shell.pwd()) "pake-files"
    Shell.rm_rf objPath
    Shell.rm_rf paketFiles
    Shell.mkdir objPath


open Fake.DotNet

let dotnet_sdk = lazy DotNet.install DotNet.Versions.Release_2_1_4
//let inline dotnet op = DotNet.exec (DotNet.Options.lift dotnet_sdk.Value) op
let inline dotnet op = DotNet.exec id op
let inline dotnet_clean arg = dotnet "clean" arg
let inline dotnet_restore arg = dotnet "restore" arg
let inline dotnet_build arg = dotnet "build" arg
let inline dotnet_pack arg = dotnet "pack" arg
let inline dotnet_test arg = dotnet "test" arg
//    DotNet.exec (DotNet.Options.lift dotnet_sdk.Value) "restore" arg
//let inline build arg =
//    DotNet.exec dotnet "build" arg


Target.create "Build" (fun _ ->
    for pl in projectLocations do
        let codeDir = sprintf "%s/src" pl
        if (dir_exists codeDir) then
            Shell.pushd (codeDir)
            let dir = Shell.pwd()
            try
                Trace.trace (sprintf "Poject to build %s" dir)
                clean ()
                paket "update"
                dotnet_clean ""
                dotnet_restore ""
                dotnet_build ""
                dotnet_pack ""
                ()
            finally 
                Shell.popd()
        else ()
)

Target.create "Test" (fun _ ->
    for pl in projectLocations do
        let codeDir = sprintf "%s/tests" pl
        if (dir_exists codeDir) then
            Shell.pushd (codeDir)
            let dir = Shell.pwd()
            try
                Trace.trace (sprintf "Poject to build %s" dir)
                clean ()
                paket "update"
                dotnet_clean ""
                dotnet_restore ""
                dotnet_build ""
                dotnet_test ""
                ()
            finally 
                Shell.popd()
        else ()
)

Target.create "Default" (fun _ ->
    ()
)

open Fake.Core.TargetOperators

"Build" ==> "Test" ==> "Default"

Target.runOrDefault "Default"