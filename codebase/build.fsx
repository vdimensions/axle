#nowarn "52"

#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.DotNet.Paket
nuget Fake.Net.Http
nuget Fake.Tools.Git
nuget Fake.Core.Target //"

//#load "./.fake/build.fsx/intellisense.fsx"

#load "./common.fsx"
#load "./paket.fsx"
open VDimensions.Fake.Common
open VDimensions.Fake.Paket

open Fake.Core
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Net

let paketVersion = "5.207.3"

let nugetApiKey =  Environment.environVarOrNone "NUGET_ORG_VDIMENSIONS_API_KEY"
let nugetServer = "https://www.nuget.org/api/v2/package"
let runTestsOnBuild = false

let createParams (data : list<string*string>) =
    let propertyFormat = " -p:{0}={1}"
    let sb = 
        data
        |> List.fold (fun (sb : System.Text.StringBuilder) (k, v) -> sb.AppendFormat(propertyFormat, k, v)) (System.Text.StringBuilder())
    sb.ToString()

let getVersionBuild propsFile =
    let yearSince = Xml.read true propsFile "" "" "Project/PropertyGroup/CopyrightYearSince" |> Seq.map System.Int32.Parse |> Seq.head
    let now = System.DateTime.UtcNow
    let backThen = System.DateTime(yearSince, 1, 1)
    ("VersionBuild", int((now - backThen).TotalDays).ToString())

let getVersionRevision () =
    let value = int(System.DateTime.UtcNow.TimeOfDay.TotalSeconds / 2.0).ToString()
    ("VersionRevision", value)

let customDotnetdParams = 
    (fun () ->
        let propsFile = (sprintf "%s/Axle.Common.props" (Shell.pwd()))
        let mutable p = [
            getVersionBuild propsFile
            getVersionRevision ()
        ] 
        createParams p
    )()

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
    "security/main"
    "security/cryptography"
    "application/main"
    "logging/log4net"
    "web/main"
    "web/websharper"
]

let dir_exists = DirectoryInfo.ofPath >> DirectoryInfo.exists
let rootDir = (Shell.pwd());
let do_in_root<'a, 'b> (op : 'a -> 'b) (args : 'a) =
    let dir = (Shell.pwd());
    try
        Shell.cd rootDir
        op args
    finally
        Shell.cd dir
let nupkg_map fn = 
    do_in_root (fun fn -> !!"../dist/*.nupkg" |> Seq.map fn |> List.ofSeq) fn

let msbuild command arg =
    let msbuildExe = "msbuild"
    // run "msbuild.exe"
    Command.RawCommand(msbuildExe, Arguments.OfArgs [command; arg])
    |> runRetry 0
    |> ignore

let clean () =
    let objPath = Path.combine (Shell.pwd()) "obj"
    let paketFiles = Path.combine (Shell.pwd()) "pake-files"
    Shell.rm_rf objPath
    Shell.rm_rf paketFiles
    Shell.mkdir objPath

let cleanNupkg = (fun _ -> nupkg_map (fun nupkg -> Trace.tracefn "NUPK CLEAN: %s" nupkg; Shell.rm_rf nupkg) |> ignore)

let get_git_branch() = Fake.Tools.Git.Information.getBranchName(Shell.pwd())

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

let dotnet_options = 
    (fun (op : DotNet.Options) -> 
        { op with 
            Verbosity = Some DotNet.Verbosity.Quiet
            CustomParams = Some customDotnetdParams
        })

let build op =
    let dir = Shell.pwd()
    match !!(sprintf "%s/*.fsproj" dir) |> List.ofSeq with
    | [] | [_] -> 
        Trace.tracefn "Performing dotnet build for default project in dir '%s'" dir
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
                paket 3 paketVersion "update" |> ignore
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
                paket 3 paketVersion "update" |> ignore
                dotnet_clean dotnet_options "" |> ignore
                dotnet_restore dotnet_options "" |> ignore
                dotnet_build dotnet_options "" |> ignore
                if runTestsOnBuild then dotnet_test dotnet_options "" |> ignore
            finally 
                Shell.popd()
        else 
            Trace.traceImportantfn "Project '%s' does not have tests" location
            ()
    )
    targetName

Target.create "Prepare" cleanNupkg
Target.create "Publish" (fun _ -> 
    let gitBranch = do_in_root get_git_branch ()
    match (gitBranch, nugetApiKey) with
    | ("master", Some apiKey) -> dotnet_push id nugetServer apiKey |> ignore
    | _ -> Trace.traceImportantfn "Branch '%s' will not push anything to nuget.org" gitBranch
)
open Fake.Core.TargetOperators

"Prepare" ==> "Publish"

projectLocations 
|> List.map createDynamicTarget
|> List.rev
|> List.fold (fun a b -> b ==> a |> ignore; b) "Publish"
|> ignore

Target.runOrDefaultWithArguments "Publish"
