namespace Axle.Verification

open System

[<AutoOpen>]
[<CompiledName("VerificationModule")>]
module VerificationModule =
    let inline argref<'T> name (arg:'T) = Axle.Verification.Verifier.VerifyArgument<'T>(arg, name)
    let inline private argderef<'T> (argref:Axle.Verification.ArgumentReference<'T>) = argref.Value
    let inline private argdo (fn:(ArgumentReference<'T> -> ArgumentReference<'U>)) name = 
        argref<'T> name >> fn >> argderef<'U>

    let inline (|NotNull|) name arg = arg |> argdo Verifier.IsNotNull name
    let inline (|NotEmpty|) name (arg:string) = arg |> argdo StringVerifier.IsNotEmpty name
    let inline (|NotNullOrEmpty|) name (arg:string) = arg |> argdo StringVerifier.IsNotNullOrEmpty name
    let inline (|NotAbstract|) name (arg:Type) = arg |> argdo TypeVerifier.IsNotAbstract name