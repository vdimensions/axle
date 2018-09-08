namespace Axle.Application.Forest.Resources

open System

open Forest.Templates.Raw

open Axle.Resources
open Axle.Resources.Extraction


type [<Interface>] IForestTemplateMarshaller =
    abstract member Unmarshal: name:string -> ResourceInfo -> Template option
    abstract ChainedExtractors:IResourceExtractor seq with get
    abstract Extension:string with get

type [<Interface>] IForestTemplateMarshallerRegistry =
    abstract member Register: marshaller:IForestTemplateMarshaller -> IForestTemplateMarshallerRegistry

type [<Sealed;NoComparison>] private MarshallingTemplateExtractor(marshaller:IForestTemplateMarshaller) =
    inherit AbstractResourceExtractor()
    override this.DoExtract(ctx:ResourceContext, name:string) =
        let baseResource = String.Format("{0}.{1}", name, this.Extension) |> ctx.ExtractionChain.Extract
        let result = baseResource |> marshaller.Unmarshal name
        match result with
        | Some template -> upcast TemplateResourceInfo(name, ctx.Culture, template, baseResource)
        | None -> Unchecked.defaultof<_>
    member this.ToExtractorList() = 
        (upcast this:IResourceExtractor)::List.ofSeq(marshaller.ChainedExtractors)
    member __.Extension 
        with get() = marshaller.Extension.TrimStart('.')

