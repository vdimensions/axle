namespace Axle.Application.Forest.Resources

open System.Globalization

open Forest.Templates.Raw

open Axle.Application.Forest.Resources.Marshalling
open Axle.Resources
open Axle.Resources.Extraction


module internal ResourceManagerIntegration =
    [<Literal>]
    let BundleName = "ForestTemplates"

    type [<Sealed;NoComparison>] private XmlTemplateValueExtractor(marshallers:IForestTemplateMarshaller list) =
        inherit AbstractResourceExtractor()
        override __.DoExtract(ctx:ResourceContext, name:string) =
            let rec extractRec (extrs: IForestTemplateMarshaller list) =
                match extrs with
                | [] -> Unchecked.defaultof<ResourceInfo>
                | x::xs ->
                    let resolveBaseResource: string -> ResourceInfo = x.ResolveResourceName >> ctx.ExtractionChain.Extract
                    let result = 
                        name 
                        |> resolveBaseResource
                        |> x.Unmarshal name
                    match result with
                    | Some template -> upcast TemplateResourceInfo(name, ctx.Culture, template, name |> resolveBaseResource)
                    | None -> extractRec xs
            extractRec marshallers
    
    let createExtractors (marshallers:IForestTemplateMarshaller seq) : IResourceExtractor list = 
        let extractors = marshallers |> Seq.collect (fun m -> m.ChainedExtractors)
        upcast XmlTemplateValueExtractor(List.ofSeq marshallers)::List.ofSeq extractors

type [<Sealed>] ResourceTemplateProvider(rm:ResourceManager) =
    interface ITemplateProvider with
        member __.Load name =
            let bundle = ResourceManagerIntegration.BundleName
            let culture = CultureInfo.InvariantCulture
            let template = rm.Load<Template>(bundle, name, culture)
            if obj.ReferenceEquals(template, null) then raise <| ResourceNotFoundException(name, bundle, culture)
            template