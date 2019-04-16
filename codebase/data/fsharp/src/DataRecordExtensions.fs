namespace Axle.Data.Extensions

open System
open System.Runtime.CompilerServices
open Axle
open Axle.Verification
open Axle.Data

[<Extension>]
type DataRecordExtensions =
    static member private getOptional<'a>(record: IDbRecord, name : string) : 'a option =
        match record.[name] |> null2opt with
        | Some x -> (x :?> 'a) |> Some
        | None -> None
    static member private getOptionalField<'a>(record: IDbRecord, index : int) : 'a option =
        match record.[index] |> null2opt with
        | Some x -> (x :?> 'a) |> Some
        | None -> None

    [<Extension>]
    static member GetBooleanOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<bool>(record, name)
    [<Extension>]
    static member GetBooleanOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<bool>(record, index)

    [<Extension>]
    static member GetSByteOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<sbyte>(record, name)
    [<Extension>]
    static member GetSByteOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<sbyte>(record, index)

    [<Extension>]
    static member GetByteOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<byte>(record, name)
    [<Extension>]
    static member GetByteOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<byte>(record, index)

    [<Extension>]
    static member GetInt16Option(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<int16>(record, name)
    [<Extension>]
    static member GetInt16Option(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<int16>(record, index)

    [<Extension>]
    static member GetUInt16Option(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<uint16>(record, name)
    [<Extension>]
    static member GetUInt16Option(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<uint16>(record, index)

    [<Extension>]
    static member GetInt32Option(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<int32>(record, name)
    [<Extension>]
    static member GetInt32Option(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<int32>(record, index)

    [<Extension>]
    static member GetUInt32Option(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<uint32>(record, name)
    [<Extension>]
    static member GetUInt32Option(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<uint32>(record, index)

    [<Extension>]
    static member GetInt64Option(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<int64>(record, name)
    [<Extension>]
    static member GetInt64Option(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<int64>(record, index)

    [<Extension>]
    static member GetUInt64Option(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<uint64>(record, name)
    [<Extension>]
    static member GetUInt64Option(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<uint64>(record, index)

    [<Extension>]
    static member GetSingleOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<single>(record, name)
    [<Extension>]
    static member GetSingleOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<single>(record, index)

    [<Extension>]
    static member GetDoubleOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<double>(record, name)
    [<Extension>]
    static member GetDoubleOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<double>(record, index)

    [<Extension>]
    static member GetDecimalOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<decimal>(record, name)
    [<Extension>]
    static member GetDecimalOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<decimal>(record, index)

    [<Extension>]
    static member GetCharOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<char>(record, name)
    [<Extension>]
    static member GetCharOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<char>(record, index)

    [<Extension>]
    static member GetStringOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<string>(record, name)
    [<Extension>]
    static member GetStringOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<string>(record, index)

    [<Extension>]
    static member GetGuidOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<Guid>(record, name)
    [<Extension>]
    static member GetGuidOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<Guid>(record, index)

    [<Extension>]
    static member GetDateTimeOption(record : IDbRecord, NotNull "name" name : string) =
        DataRecordExtensions.getOptional<DateTime>(record, name)
    [<Extension>]
    static member GetDateTimeOption(record : IDbRecord, index : int) =
        DataRecordExtensions.getOptionalField<DateTime>(record, index)

