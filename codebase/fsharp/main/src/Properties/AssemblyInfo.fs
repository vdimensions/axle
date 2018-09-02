module Axle.FSharp.AssemblyInfo

open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

[<assembly: AssemblyTitle("Axle.FSharp")>]
[<assembly: AssemblyProduct("Axle.FSharp")>]
[<assembly: AssemblyDescription("Axle Framework F#-friendly Library")>]

[<assembly: AssemblyCompany("Virtual Dimensions")>]
[<assembly: AssemblyCopyright("Copyright © Virtual Dimensions 2013-2018")>]
[<assembly: AssemblyTrademark("")>]

[<assembly: AssemblyConfiguration("")>]
[<assembly: AssemblyCulture("")>]

[<assembly: ComVisible(false)>]
#if !NETSTANDARD || NETSTANDARD1_1_OR_NEWER
[<assembly: Guid("058DA5D4-BE62-4A66-AC23-DD162E113EBF")>]
#endif

[<assembly: AssemblyVersion("1.5.1.3")>]
[<assembly: AssemblyFileVersion("1.5.1.3")>]
[<assembly: AssemblyInformationalVersion("1.5.1.3")>]

do ()