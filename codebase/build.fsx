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
    "resources/main"
    "resources/java"
    "resources/yaml"
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

let createDynamicTarget location =
    let targetName = location
    Target.create targetName (fun _ ->
        let codeDir = sprintf "%s/src" location
        let testsDir = sprintf "%s/tests" location
        if (dir_exists codeDir) then
            Shell.pushd (codeDir)
            let dir = Shell.pwd()
            try
                Trace.trace (sprintf "Project to build %s" dir)
                clean ()
                paket "update" |> ignore
                dotnet_clean "" |> ignore
                dotnet_restore "" |> ignore
                dotnet_build "" |> ignore
                dotnet_pack "" |> ignore
                ()
            finally 
                Shell.popd()
        else ()

        if (dir_exists testsDir) then
            Shell.pushd (testsDir)
            let dir = Shell.pwd()
            try
                Trace.trace (sprintf "Test project to build %s" dir)
                clean ()
                paket "update" |> ignore
                dotnet_clean "" |> ignore
                dotnet_restore "" |> ignore
                dotnet_build "" |> ignore
                dotnet_test "" |> ignore
                ()
            finally 
                Shell.popd()
        else ()
    )
    targetName

Target.create "Default" (fun _ -> ())

open Fake.Core.TargetOperators

projectLocations 
|> List.map createDynamicTarget
|> List.fold (fun a b -> Trace.trace (sprintf "target %s" a); b ==> a) "Default"

Target.runOrDefault "Default"