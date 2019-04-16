namespace Axle.Data.Extensions

open System
open System.Data
open System.Runtime.CompilerServices
open Axle
open Axle.Verification

[<Extension>]
type DataRowExtensions =
    static member private getOptional<'a>(dataRow : DataRow, name : string) : 'a option =
        match dataRow.[name] |> null2opt with
        | Some x -> (x :?> 'a) |> Some
        | None -> None
    static member private getOptional<'a>(dataRow : DataRow, index : int) : 'a option =
        match dataRow.[index] |> null2opt with
        | Some x -> (x :?> 'a) |> Some
        | None -> None

    [<Extension>]
    static member GetBooleanOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<bool>(dataRow, name)
    [<Extension>]
    static member GetBooleanOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<bool>(dataRow, index)

    [<Extension>]
    static member GetSByteOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<sbyte>(dataRow, name)
    [<Extension>]
    static member GetSByteOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<sbyte>(dataRow, index)

    [<Extension>]
    static member GetByteOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<byte>(dataRow, name)
    [<Extension>]
    static member GetByteOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<byte>(dataRow, index)

    [<Extension>]
    static member GetInt16Option(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<int16>(dataRow, name)
    [<Extension>]
    static member GetInt16Option(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<int16>(dataRow, index)

    [<Extension>]
    static member GetUInt16Option(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<uint16>(dataRow, name)
    [<Extension>]
    static member GetUInt16Option(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<uint16>(dataRow, index)

    [<Extension>]
    static member GetInt32Option(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<int32>(dataRow, name)
    [<Extension>]
    static member GetInt32Option(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<int32>(dataRow, index)

    [<Extension>]
    static member GetUInt32Option(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<uint32>(dataRow, name)
    [<Extension>]
    static member GetUInt32Option(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<uint32>(dataRow, index)

    [<Extension>]
    static member GetInt64Option(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<int64>(dataRow, name)
    [<Extension>]
    static member GetInt64Option(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<int64>(dataRow, index)

    [<Extension>]
    static member GetUInt64Option(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<uint64>(dataRow, name)
    [<Extension>]
    static member GetUInt64Option(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<uint64>(dataRow, index)

    [<Extension>]
    static member GetSingleOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<single>(dataRow, name)
    [<Extension>]
    static member GetSingleOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<single>(dataRow, index)

    [<Extension>]
    static member GetDoubleOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<double>(dataRow, name)
    [<Extension>]
    static member GetDoubleOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<double>(dataRow, index)

    [<Extension>]
    static member GetDecimalOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<decimal>(dataRow, name)
    [<Extension>]
    static member GetDecimalOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<decimal>(dataRow, index)

    [<Extension>]
    static member GetCharOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<char>(dataRow, name)
    [<Extension>]
    static member GetCharOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<char>(dataRow, index)

    [<Extension>]
    static member GetStringOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<string>(dataRow, name)
    [<Extension>]
    static member GetStringOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<string>(dataRow, index)

    [<Extension>]
    static member GetGuidOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<Guid>(dataRow, name)
    [<Extension>]
    static member GetGuidOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<Guid>(dataRow, index)

    [<Extension>]
    static member GetDateTimeOption(dataRow : DataRow, NotNull "name" name : string) =
        DataRowExtensions.getOptional<DateTime>(dataRow, name)
    [<Extension>]
    static member GetDateTimeOption(dataRow : DataRow, index : int) =
        DataRowExtensions.getOptional<DateTime>(dataRow, index)

