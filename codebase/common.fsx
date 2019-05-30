namespace VDimensions.Fake

open Fake.Core
open Fake.IO

module Common =
    let rec runRetry retries cmd =
        try
            cmd
            |> CreateProcess.fromCommand
            // use mono if linux
            |> CreateProcess.withFramework
            // throw an error if the process fails
            |> CreateProcess.ensureExitCode
            |> Proc.run
        with
        | e -> 
            if retries > 0 
            then runRetry (retries - 1) cmd
            else raise e