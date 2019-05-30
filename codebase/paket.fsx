namespace VDimensions.Fake

#load "./common.fsx"

open VDimensions.Fake.Common

open Fake.Core
open Fake.IO
open Fake.Net

module Paket =
    let paket retryCount paketVersion command =
        let rc = if (retryCount < 0) then 0 else retryCount
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
        |> runRetry rc
        |> ignore