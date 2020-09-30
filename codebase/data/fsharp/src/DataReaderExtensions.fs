namespace Axle.Data.Extensions

open System
open System.Data
open System.Runtime.CompilerServices
open Axle
open Axle.Verification

[<Extension>]
type DataReaderExtensions =
    static member private getOptional<'a>(dataReader : IDataReader, name : string) : 'a option =
        match dataReader.[name] |> null2opt with
        | Some x -> (x :?> 'a) |> Some
        | None -> None
    static member private getOptional<'a>(dataReader : IDataReader, index : int) : 'a option =
        match dataReader.[index] |> null2opt with
        | Some x -> (x :?> 'a) |> Some
        | None -> None

    [<Extension>]
    static member GetBooleanOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<bool>(dataReader, name)
    [<Extension>]
    static member GetBooleanOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<bool>(dataReader, index)

    [<Extension>]
    static member GetSByteOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<sbyte>(dataReader, name)
    [<Extension>]
    static member GetSByteOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<sbyte>(dataReader, index)

    [<Extension>]
    static member GetByteOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<byte>(dataReader, name)
    [<Extension>]
    static member GetByteOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<byte>(dataReader, index)

    [<Extension>]
    static member GetInt16Option(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<int16>(dataReader, name)
    [<Extension>]
    static member GetInt16Option(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<int16>(dataReader, index)

    [<Extension>]
    static member GetUInt16Option(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<uint16>(dataReader, name)
    [<Extension>]
    static member GetUInt16Option(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<uint16>(dataReader, index)

    [<Extension>]
    static member GetInt32Option(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<int32>(dataReader, name)
    [<Extension>]
    static member GetInt32Option(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<int32>(dataReader, index)

    [<Extension>]
    static member GetUInt32Option(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<uint32>(dataReader, name)
    [<Extension>]
    static member GetUInt32Option(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<uint32>(dataReader, index)

    [<Extension>]
    static member GetInt64Option(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<int64>(dataReader, name)
    [<Extension>]
    static member GetInt64Option(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<int64>(dataReader, index)

    [<Extension>]
    static member GetUInt64Option(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<uint64>(dataReader, name)
    [<Extension>]
    static member GetUInt64Option(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<uint64>(dataReader, index)

    [<Extension>]
    static member GetSingleOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<single>(dataReader, name)
    [<Extension>]
    static member GetSingleOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<single>(dataReader, index)

    [<Extension>]
    static member GetDoubleOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<double>(dataReader, name)
    [<Extension>]
    static member GetDoubleOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<double>(dataReader, index)

    [<Extension>]
    static member GetDecimalOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<decimal>(dataReader, name)
    [<Extension>]
    static member GetDecimalOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<decimal>(dataReader, index)

    [<Extension>]
    static member GetCharOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<char>(dataReader, name)
    [<Extension>]
    static member GetCharOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<char>(dataReader, index)

    [<Extension>]
    static member GetStringOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<string>(dataReader, name)
    [<Extension>]
    static member GetStringOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<string>(dataReader, index)

    [<Extension>]
    static member GetGuidOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<Guid>(dataReader, name)
    [<Extension>]
    static member GetGuidOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<Guid>(dataReader, index)

    [<Extension>]
    static member GetDateTimeOption(dataReader : IDataReader, NotNull "name" name : string) =
        DataReaderExtensions.getOptional<DateTime>(dataReader, name)
    [<Extension>]
    static member GetDateTimeOption(dataReader : IDataReader, index : int) =
        DataReaderExtensions.getOptional<DateTime>(dataReader, index)

