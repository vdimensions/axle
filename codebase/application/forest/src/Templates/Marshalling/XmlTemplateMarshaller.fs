namespace Axle.Application.Forest.Resources.Marshalling

open System

open Forest.Templates.Xml

open Axle.Resources.Xml
open Axle.Resources.Xml.Extraction


type [<Sealed;NoComparison>] XmlTemplateMarshaller() =
    interface IForestTemplateMarshaller with
        member __.ResolveResourceName templateName =
            String.Format("{0}.xml", templateName)
        member __.Unmarshal name resource =
            match resource with
            | :? XDocumentResourceInfo as res -> res.Value |> XmlTemplateParser().ParseXml name |> Some
            | _ -> None
        member __.ChainedExtractors 
            with get() = seq { yield upcast XDocumentExtractor() }