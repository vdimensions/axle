namespace Axle.Verification

open System

[<AutoOpen>]
[<CompiledName("VerificationModule")>]
module VerificationModule =
    let inline private argref name arg = 
        Axle.Verification.Verifier.VerifyArgument(arg, name)

    let inline (|NotNull|) name arg = 
        argref name arg |> Verifier.IsNotNull

    let inline (|NotEmpty|) name (arg:string) = 
        argref name arg |> StringVerifier.IsNotEmpty
    let inline (|NotNullOrEmpty|) name (arg:string) = 
        argref name arg |> StringVerifier.IsNotNullOrEmpty

    let inline (|NotAbstract|) name (arg:Type) = 
        argref name arg |> TypeVerifier.IsNotAbstract