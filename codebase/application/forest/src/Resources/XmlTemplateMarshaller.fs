namespace Axle.Application.Forest.Resources

open Forest.Templates.Xml

open Axle.Resources.Xml
open Axle.Resources.Xml.Extraction


type [<Sealed;NoComparison>] XmlTemplateMarshaller() =
    interface IForestTemplateMarshaller with
        member __.Extension with get() = "xml"
        member __.Unmarshal name resource =
            match resource with
            | :? XDocumentResourceInfo as res -> res.Value |> XmlTemplateParser().ParseXml name |> System.Nullable
            | _ -> System.Nullable<_>()
        member __.ChainedExtractors 
            with get() = seq { yield upcast XDocumentExtractor() }